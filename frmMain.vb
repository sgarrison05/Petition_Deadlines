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
    Private Const DATA_FILE As String = DATA_FOLDER & "\PDeadlines.txt"
    Private Const HOLIDAY_FILE As String = DATA_FOLDER & "\Holidays.txt"

    Private Const DATE_FORMAT As String = "yyyy-MM-dd"
    Private Const FIELD_COUNT As Integer = 8

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
        ElseIf calDaysLeft = 5 Then
            lblStatusValue.Text = "Due this Week"
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

                If calDaysLeft < 2 Then
                    item.ForeColor = Color.Firebrick
                ElseIf calDaysLeft <= 5 Then
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

#Region "Persistence (sequential tab-delimited text file)"

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

                Dim f As String() = line.Split(ControlChars.Tab)
                If f.Length < FIELD_COUNT Then Continue For

                Dim rec As New PetitionRecord()
                rec.Id = f(0)
                rec.ChildName = Unescape(f(1))
                rec.Charge = Unescape(f(2))

                Dim initialDate As Date
                If Not Date.TryParseExact(f(3), DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, initialDate) Then Continue For
                rec.InitialDate = initialDate

                If f(4) <> "" Then
                    Dim subsequentDate As Date
                    If Date.TryParseExact(f(4), DATE_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, subsequentDate) Then
                        rec.SubsequentDate = subsequentDate
                    End If
                End If

                Dim workingDays As Integer
                If Not Integer.TryParse(f(5), NumberStyles.Integer, CultureInfo.InvariantCulture, workingDays) Then Continue For
                If workingDays < 1 Then Continue For
                rec.WorkingDays = workingDays

                rec.Notes = Unescape(f(7))

                ' The stored deadline (f(6)) is informational; recompute so holiday changes take effect.
                rec.Deadline = AddWorkingDays(rec.InitialDate, rec.WorkingDays)

                _records.Add(rec)
            Next
        Catch ex As Exception
            MessageBox.Show("Could not read the data file " & DATA_FILE & "." & vbCrLf & vbCrLf & ex.Message,
                            "Petition Deadlines", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Rewrites the whole file (required for sequential storage) via a temp file so a failed write cannot corrupt it.
    Private Function WriteAllRecords() As Boolean
        Dim tempFile As String = DATA_FILE & ".tmp"
        Try
            If Not Directory.Exists(DATA_FOLDER) Then Directory.CreateDirectory(DATA_FOLDER)

            Dim lines As New List(Of String)()
            For Each rec As PetitionRecord In _records
                Dim subsequentText As String = ""
                If rec.SubsequentDate.HasValue Then
                    subsequentText = rec.SubsequentDate.Value.ToString(DATE_FORMAT, CultureInfo.InvariantCulture)
                End If

                lines.Add(String.Join(vbTab, New String() {
                    rec.Id,
                    Escape(rec.ChildName),
                    Escape(rec.Charge),
                    rec.InitialDate.ToString(DATE_FORMAT, CultureInfo.InvariantCulture),
                    subsequentText,
                    rec.WorkingDays.ToString(CultureInfo.InvariantCulture),
                    rec.Deadline.ToString(DATE_FORMAT, CultureInfo.InvariantCulture),
                    Escape(rec.Notes)
                }))
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

    ' Keeps each record on exactly one line with exactly one tab between fields.
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
