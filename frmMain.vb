Option Strict On
Option Explicit On
Option Infer On

Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Public Class frmMain

#Region "Constants and fields"

    Private Const DATA_FOLDER As String = "D:\Temp\PetitionDeadlines"
    Private Const DATA_FILE As String = DATA_FOLDER & "\PetitionDeadlines.txt"
    Private Const HOLIDAY_FILE As String = DATA_FOLDER & "\Holidays.txt"

    Private Const DATE_FORMAT As String = "MM-dd-yyyy"
    Private Const LEGACY_FIELD_COUNT As Integer = 8   ' Old tab-delimited format

    ' Fixed-width column layout of the data file (two spaces between columns; notes run to the end of the line).
    Private Const GAP As Integer = 2
    Private Const GAP_TEXT As String = "  "
    Private Const W_ID As Integer = 32
    Private Const W_NAME As Integer = 40
    Private Const W_CHARGE As Integer = 50
    Private Const W_INITIAL As Integer = 10
    Private Const W_SUBSEQUENT As Integer = 10
    Private Const W_DAYS As Integer = 4
    Private Const W_DEADLINE As Integer = 10

    Private Const POS_ID As Integer = 0
    Private Const POS_NAME As Integer = POS_ID + W_ID + GAP
    Private Const POS_CHARGE As Integer = POS_NAME + W_NAME + GAP
    Private Const POS_INITIAL As Integer = POS_CHARGE + W_CHARGE + GAP
    Private Const POS_SUBSEQUENT As Integer = POS_INITIAL + W_INITIAL + GAP
    Private Const POS_DAYS As Integer = POS_SUBSEQUENT + W_SUBSEQUENT + GAP
    Private Const POS_DEADLINE As Integer = POS_DAYS + W_DAYS + GAP
    Private Const POS_NOTES As Integer = POS_DEADLINE + W_DEADLINE + GAP

    ' One saved case. Deadline is recomputed every time records are loaded.
    Private Class PetitionRecord
        Public Property Id As String = ""
        Public Property ChildName As String = ""
        Public Property Charge As String = ""
        Public Property InitialDate As Date
        Public Property SubsequentDate As Date?
        Public Property WorkingDays As Integer
        Public Property Deadline As Date
        Public Property Notes As String = ""
    End Class

    Private ReadOnly _records As New List(Of PetitionRecord)()
    Private ReadOnly _holidays As New HashSet(Of Date)()

    Private _currentId As String = ""      ' Id of the saved record loaded in the form ("" = new record)
    Private _loading As Boolean = False    ' Suppresses event handlers while the form is populated in code
    Private _lastRefreshDate As Date = Date.Today

#End Region

#Region "Form events"

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Entries cannot be longer than their file columns, so nothing is ever truncated.
        txtName.MaxLength = W_NAME
        txtCharge.MaxLength = W_CHARGE

        Try
            If Not Directory.Exists(DATA_FOLDER) Then Directory.CreateDirectory(DATA_FOLDER)
        Catch ex As Exception
            MessageBox.Show("Could not create the data folder " & DATA_FOLDER & "." & vbCrLf & vbCrLf & ex.Message,
                            "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try

        LoadHolidays()
        LoadRecords()
        ClearForm()
        RefreshList()
    End Sub

    ' If the app was left open overnight, bring the remaining-day counts up to date.
    Private Sub frmMain_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If _lastRefreshDate <> Date.Today Then
            RefreshList()
            If _currentId <> "" Then
                Dim deadline As Date
                If TryCalculate(deadline, False) Then ShowResults(deadline)
            End If
        End If
    End Sub

    ' Any change to the inputs that drive the deadline makes the displayed results stale.
    Private Sub InputsChanged(sender As Object, e As EventArgs) Handles dtpInitial.ValueChanged, nudDays.ValueChanged
        If _loading Then Return
        ResetResults()
    End Sub

    Private Sub lvRecords_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvRecords.SelectedIndexChanged
        If _loading Then Return
        If lvRecords.SelectedItems.Count = 0 Then Return

        Dim id As String = CStr(lvRecords.SelectedItems(0).Tag)
        Dim rec As PetitionRecord = _records.FirstOrDefault(Function(r) r.Id = id)
        If rec IsNot Nothing Then LoadRecordIntoForm(rec)
    End Sub

#End Region

#Region "Button handlers"

    Private Sub btnCalculate_Click(sender As Object, e As EventArgs) Handles btnCalculate.Click
        Dim deadline As Date
        If TryCalculate(deadline, True) Then ShowResults(deadline)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim childName As String = txtName.Text.Trim()
        Dim charge As String = txtCharge.Text.Trim()

        If childName = "" Then
            MessageBox.Show("Please enter the child's name.", "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtName.Focus()
            Return
        End If
        If charge = "" Then
            MessageBox.Show("Please enter the charge.", "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtCharge.Focus()
            Return
        End If

        Dim deadline As Date
        If Not TryCalculate(deadline, True) Then Return
        ShowResults(deadline)

        ' Update the loaded record if there is one; otherwise create a new one.
        Dim rec As PetitionRecord = Nothing
        If _currentId <> "" Then
            Dim id As String = _currentId
            rec = _records.FirstOrDefault(Function(r) r.Id = id)
        End If
        If rec Is Nothing Then
            rec = New PetitionRecord()
            rec.Id = Guid.NewGuid().ToString("N")
            _records.Add(rec)
        End If

        rec.ChildName = childName
        rec.Charge = charge
        rec.InitialDate = dtpInitial.Value.Date
        If dtpSubsequent.Checked Then
            rec.SubsequentDate = dtpSubsequent.Value.Date
        Else
            rec.SubsequentDate = Nothing
        End If
        rec.WorkingDays = CInt(nudDays.Value)
        rec.Deadline = deadline
        rec.Notes = txtNotes.Text

        If Not WriteAllRecords() Then
            LoadRecords()   ' Resynchronize memory with what is actually on disk
            RefreshList()
            Return
        End If

        _currentId = rec.Id
        RefreshList()
        UpdateButtons()
        MessageBox.Show("Record saved.", "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ClearForm()
        txtName.Focus()
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
        txtName.Focus()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _currentId = "" Then
            MessageBox.Show("Click a saved record in the list first, then press Delete.",
                            "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim id As String = _currentId
        Dim rec As PetitionRecord = _records.FirstOrDefault(Function(r) r.Id = id)
        If rec Is Nothing Then
            ClearForm()
            Return
        End If

        Dim answer As DialogResult = MessageBox.Show(
            "Delete the record for " & rec.ChildName & " (" & rec.Charge & ")?" & vbCrLf & vbCrLf & "This cannot be undone.",
            "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
        If answer <> DialogResult.Yes Then Return

        _records.Remove(rec)

        If Not WriteAllRecords() Then
            LoadRecords()
            RefreshList()
            Return
        End If

        ClearForm()
        RefreshList()
    End Sub

#End Region

#Region "Calculation"

    ' Validates the inputs and computes the deadline. showMessages = False for silent background recalculation.
    Private Function TryCalculate(ByRef deadline As Date, showMessages As Boolean) As Boolean
        Dim initialDate As Date = dtpInitial.Value.Date
        Dim workingDays As Integer = CInt(nudDays.Value)

        If dtpSubsequent.Checked AndAlso dtpSubsequent.Value.Date < initialDate Then
            If showMessages Then
                MessageBox.Show("The subsequent detention date cannot be earlier than the initial detention date.",
                                "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Information)
                dtpSubsequent.Focus()
            End If
            Return False
        End If

        deadline = AddWorkingDays(initialDate, workingDays)
        Return True
    End Function

    ' The start date is day 0. Day 1 is the next working day (Mon-Fri, excluding listed holidays).
    Private Function AddWorkingDays(startDate As Date, workingDays As Integer) As Date
        Dim d As Date = startDate.Date
        Dim counted As Integer = 0
        Do While counted < workingDays
            d = d.AddDays(1)
            If IsWorkingDay(d) Then counted += 1
        Loop
        Return d
    End Function

    ' Working days after fromDate up to and including toDate. Negative when toDate is in the past.
    Private Function WorkingDaysBetween(fromDate As Date, toDate As Date) As Integer
        Dim startDate As Date = fromDate.Date
        Dim endDate As Date = toDate.Date
        If endDate = startDate Then Return 0

        Dim direction As Integer = If(endDate > startDate, 1, -1)
        Dim count As Integer = 0
        Dim d As Date = startDate
        Do While d <> endDate
            d = d.AddDays(direction)
            If IsWorkingDay(d) Then count += 1
        Loop
        Return count * direction
    End Function

    Private Function IsWorkingDay(d As Date) As Boolean
        If d.DayOfWeek = DayOfWeek.Saturday OrElse d.DayOfWeek = DayOfWeek.Sunday Then Return False
        If _holidays.Contains(d.Date) Then Return False
        Return True
    End Function

    Private Sub ShowResults(deadline As Date)
        Dim today As Date = Date.Today
        Dim workDaysLeft As Integer = WorkingDaysBetween(today, deadline)
        Dim calDaysLeft As Integer = CInt((deadline.Date - today).TotalDays)

        lblDeadlineValue.Text = deadline.ToString("dddd, MMM d, yyyy", CultureInfo.CurrentCulture)
        lblWorkValue.Text = workDaysLeft.ToString(CultureInfo.CurrentCulture)
        lblCalValue.Text = calDaysLeft.ToString(CultureInfo.CurrentCulture)

        If calDaysLeft < 0 Then
            lblStatusValue.Text = "OVERDUE by " & Math.Abs(calDaysLeft).ToString(CultureInfo.CurrentCulture) &
                                  If(Math.Abs(calDaysLeft) = 1, " day", " days")
            lblStatusValue.ForeColor = Color.Firebrick
        ElseIf calDaysLeft = 0 Then
            lblStatusValue.Text = "DUE TODAY"
            lblStatusValue.ForeColor = Color.DarkOrange
        Else
            lblStatusValue.Text = "Open"
            lblStatusValue.ForeColor = Color.ForestGreen
        End If
    End Sub

    Private Sub ResetResults()
        lblDeadlineValue.Text = ""
        lblWorkValue.Text = ""
        lblCalValue.Text = ""
        lblStatusValue.Text = ""
        lblStatusValue.ForeColor = SystemColors.ControlText
    End Sub

#End Region

#Region "Form helpers"

    Private Sub ClearForm()
        _loading = True
        Try
            txtName.Clear()
            txtCharge.Clear()
            txtNotes.Clear()
            dtpInitial.Value = Date.Today
            dtpSubsequent.Value = Date.Today
            dtpSubsequent.Checked = False
            nudDays.Value = 15D
            ResetResults()
            _currentId = ""
            lvRecords.SelectedItems.Clear()
        Finally
            _loading = False
        End Try
        UpdateButtons()
    End Sub

    Private Sub LoadRecordIntoForm(rec As PetitionRecord)
        _loading = True
        Try
            txtName.Text = rec.ChildName
            txtCharge.Text = rec.Charge
            txtNotes.Text = rec.Notes
            dtpInitial.Value = rec.InitialDate
            If rec.SubsequentDate.HasValue Then
                dtpSubsequent.Value = rec.SubsequentDate.Value
                dtpSubsequent.Checked = True
            Else
                dtpSubsequent.Value = Date.Today
                dtpSubsequent.Checked = False
            End If
            nudDays.Value = CDec(Math.Max(CInt(nudDays.Minimum), Math.Min(CInt(nudDays.Maximum), rec.WorkingDays)))
            _currentId = rec.Id
        Finally
            _loading = False
        End Try

        ShowResults(rec.Deadline)
        UpdateButtons()
    End Sub

    Private Sub UpdateButtons()
        btnDelete.Enabled = (_currentId <> "")
    End Sub

    Private Sub RefreshList()
        _lastRefreshDate = Date.Today
        Dim today As Date = Date.Today

        _loading = True
        Try
            lvRecords.BeginUpdate()
            lvRecords.Items.Clear()

            For Each rec As PetitionRecord In _records.OrderBy(Function(r) r.Deadline).ThenBy(Function(r) r.ChildName)
                Dim workDaysLeft As Integer = WorkingDaysBetween(today, rec.Deadline)
                Dim calDaysLeft As Integer = CInt((rec.Deadline.Date - today).TotalDays)

                Dim item As New ListViewItem(rec.ChildName)
                item.SubItems.Add(rec.Charge)
                item.SubItems.Add(rec.Deadline.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture))
                item.SubItems.Add(workDaysLeft.ToString(CultureInfo.CurrentCulture))
                item.SubItems.Add(calDaysLeft.ToString(CultureInfo.CurrentCulture))
                item.Tag = rec.Id

                If calDaysLeft < 0 Then
                    item.ForeColor = Color.Firebrick
                ElseIf calDaysLeft = 0 Then
                    item.ForeColor = Color.DarkOrange
                End If

                lvRecords.Items.Add(item)
                If rec.Id = _currentId Then item.Selected = True
            Next
        Finally
            lvRecords.EndUpdate()
            _loading = False
        End Try
    End Sub

#End Region

#Region "Persistence (sequential fixed-width text file)"

    ' Optional file: one date per line. Blank lines and lines starting with # or ' are ignored.
    Private Sub LoadHolidays()
        _holidays.Clear()
        If Not File.Exists(HOLIDAY_FILE) Then Return

        Try
            For Each line As String In File.ReadAllLines(HOLIDAY_FILE)
                Dim text As String = line.Trim()
                If text = "" OrElse text.StartsWith("#") OrElse text.StartsWith("'") Then Continue For

                Dim d As Date
                If Date.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, d) Then
                    _holidays.Add(d.Date)
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Could not read the holiday file." & vbCrLf & vbCrLf & ex.Message,
                            "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub LoadRecords()
        _records.Clear()
        If Not File.Exists(DATA_FILE) Then Return

        Try
            For Each line As String In File.ReadAllLines(DATA_FILE, Encoding.UTF8)
                If line.Trim() = "" Then Continue For

                ' Skip the header row ("ID ...") and the dashed separator row.
                If line.StartsWith("ID") OrElse line.StartsWith("--") Then Continue For

                Dim rec As PetitionRecord
                If line.Contains(vbTab) Then
                    rec = ParseLegacyLine(line)    ' Older tab-delimited format; rewritten as fixed-width on next save
                Else
                    rec = ParseFixedLine(line)
                End If

                If rec IsNot Nothing Then _records.Add(rec)
            Next
        Catch ex As Exception
            MessageBox.Show("Could not read the data file " & DATA_FILE & "." & vbCrLf & vbCrLf & ex.Message,
                            "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ParseFixedLine(line As String) As PetitionRecord
        If line.Length < POS_DEADLINE + W_DEADLINE Then Return Nothing

        Dim notesText As String = ""
        If line.Length > POS_NOTES Then notesText = line.Substring(POS_NOTES)

        Return BuildRecord(Field(line, POS_ID, W_ID),
                           Field(line, POS_NAME, W_NAME),
                           Field(line, POS_CHARGE, W_CHARGE),
                           Field(line, POS_INITIAL, W_INITIAL),
                           Field(line, POS_SUBSEQUENT, W_SUBSEQUENT),
                           Field(line, POS_DAYS, W_DAYS),
                           Unescape(notesText))
    End Function

    Private Function ParseLegacyLine(line As String) As PetitionRecord
        Dim f As String() = line.Split(ControlChars.Tab)
        If f.Length < LEGACY_FIELD_COUNT Then Return Nothing

        Return BuildRecord(f(0), Unescape(f(1)), Unescape(f(2)), f(3), f(4), f(5), Unescape(f(7)))
    End Function

    ' Validates the raw text fields and builds a record, or returns Nothing if the line is unusable.
    Private Function BuildRecord(id As String, childName As String, charge As String,
                                 initialText As String, subsequentText As String,
                                 daysText As String, notes As String) As PetitionRecord

        Dim initialDate As Date
        If Not Date.TryParseExact(initialText, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, initialDate) Then Return Nothing

        Dim workingDays As Integer
        If Not Integer.TryParse(daysText, NumberStyles.Integer, CultureInfo.InvariantCulture, workingDays) Then Return Nothing
        If workingDays < 1 Then Return Nothing

        Dim rec As New PetitionRecord()
        rec.Id = If(id = "", Guid.NewGuid().ToString("N"), id)
        rec.ChildName = childName
        rec.Charge = charge
        rec.InitialDate = initialDate
        rec.WorkingDays = workingDays
        rec.Notes = notes

        If subsequentText <> "" Then
            Dim subsequentDate As Date
            If Date.TryParseExact(subsequentText, DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, subsequentDate) Then
                rec.SubsequentDate = subsequentDate
            End If
        End If

        ' The deadline is not read from the file; it is recomputed so holiday changes take effect.
        rec.Deadline = AddWorkingDays(rec.InitialDate, rec.WorkingDays)
        Return rec
    End Function

    ' Reads one fixed-width column, tolerating lines that were shortened by an editor trimming trailing spaces.
    Private Shared Function Field(line As String, start As Integer, width As Integer) As String
        If start >= line.Length Then Return ""
        Dim length As Integer = Math.Min(width, line.Length - start)
        Return line.Substring(start, length).Trim()
    End Function

    ' Pads (or truncates) text to an exact column width. Tabs and line breaks become spaces.
    Private Shared Function Fit(text As String, width As Integer) As String
        If text Is Nothing Then text = ""
        text = text.Replace(vbCr, " ").Replace(vbLf, " ").Replace(vbTab, " ")
        If text.Length > width Then text = text.Substring(0, width)
        Return text.PadRight(width)
    End Function

    Private Shared Function HeaderLine() As String
        Return Fit("ID", W_ID) & GAP_TEXT &
               Fit("CHILD NAME", W_NAME) & GAP_TEXT &
               Fit("CHARGE", W_CHARGE) & GAP_TEXT &
               Fit("INITIAL", W_INITIAL) & GAP_TEXT &
               Fit("SUBSEQUENT", W_SUBSEQUENT) & GAP_TEXT &
               Fit("DAYS", W_DAYS) & GAP_TEXT &
               Fit("DEADLINE", W_DEADLINE) & GAP_TEXT &
               "NOTES"
    End Function

    Private Shared Function SeparatorLine() As String
        Return New String("-"c, W_ID) & GAP_TEXT &
               New String("-"c, W_NAME) & GAP_TEXT &
               New String("-"c, W_CHARGE) & GAP_TEXT &
               New String("-"c, W_INITIAL) & GAP_TEXT &
               New String("-"c, W_SUBSEQUENT) & GAP_TEXT &
               New String("-"c, W_DAYS) & GAP_TEXT &
               New String("-"c, W_DEADLINE) & GAP_TEXT &
               New String("-"c, 5)
    End Function

    Private Shared Function BuildLine(rec As PetitionRecord) As String
        Dim subsequentText As String = ""
        If rec.SubsequentDate.HasValue Then
            subsequentText = rec.SubsequentDate.Value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture)
        End If

        Dim sb As New StringBuilder()
        sb.Append(Fit(rec.Id, W_ID)).Append(GAP_TEXT)
        sb.Append(Fit(rec.ChildName, W_NAME)).Append(GAP_TEXT)
        sb.Append(Fit(rec.Charge, W_CHARGE)).Append(GAP_TEXT)
        sb.Append(Fit(rec.InitialDate.ToString(DATE_FORMAT, CultureInfo.InvariantCulture), W_INITIAL)).Append(GAP_TEXT)
        sb.Append(Fit(subsequentText, W_SUBSEQUENT)).Append(GAP_TEXT)
        sb.Append(rec.WorkingDays.ToString(CultureInfo.InvariantCulture).PadLeft(W_DAYS)).Append(GAP_TEXT)
        sb.Append(Fit(rec.Deadline.ToString(DATE_FORMAT, CultureInfo.InvariantCulture), W_DEADLINE)).Append(GAP_TEXT)
        sb.Append(Escape(rec.Notes))
        Return sb.ToString()
    End Function

    ' Rewrites the whole file (required for sequential storage) via a temp file so a failed write cannot corrupt it.
    Private Function WriteAllRecords() As Boolean
        Dim tempFile As String = DATA_FILE & ".tmp"
        Try
            If Not Directory.Exists(DATA_FOLDER) Then Directory.CreateDirectory(DATA_FOLDER)

            Dim lines As New List(Of String)()
            lines.Add(HeaderLine())
            lines.Add(SeparatorLine())
            For Each rec As PetitionRecord In _records
                lines.Add(BuildLine(rec))
            Next

            File.WriteAllLines(tempFile, lines, Encoding.UTF8)
            File.Copy(tempFile, DATA_FILE, True)
            File.Delete(tempFile)
            Return True

        Catch ex As Exception
            MessageBox.Show("Could not write the data file " & DATA_FILE & "." & vbCrLf & vbCrLf & ex.Message,
                            "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' Used for the notes column: keeps each record on exactly one line.
    Private Shared Function Escape(text As String) As String
        If text Is Nothing Then Return ""
        Dim result As String = text.Replace("\", "\\")
        result = result.Replace(vbCrLf, "\n")
        result = result.Replace(vbCr, "\n")
        result = result.Replace(vbLf, "\n")
        result = result.Replace(vbTab, "\t")
        Return result
    End Function

    Private Shared Function Unescape(text As String) As String
        Dim sb As New StringBuilder()
        Dim i As Integer = 0
        Do While i < text.Length
            Dim c As Char = text(i)
            If c = "\"c AndAlso i + 1 < text.Length Then
                Dim nextChar As Char = text(i + 1)
                Select Case nextChar
                    Case "n"c
                        sb.Append(vbCrLf)
                    Case "t"c
                        sb.Append(vbTab)
                    Case "\"c
                        sb.Append("\"c)
                    Case Else
                        sb.Append(c).Append(nextChar)
                End Select
                i += 2
            Else
                sb.Append(c)
                i += 1
            End If
        Loop
        Return sb.ToString()
    End Function

#End Region

End Class
