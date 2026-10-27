<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        grpCase = New System.Windows.Forms.GroupBox()
        nudDays = New System.Windows.Forms.NumericUpDown()
        lblDays = New System.Windows.Forms.Label()
        dtpSubsequent = New System.Windows.Forms.DateTimePicker()
        lblSubsequent = New System.Windows.Forms.Label()
        dtpInitial = New System.Windows.Forms.DateTimePicker()
        lblInitial = New System.Windows.Forms.Label()
        txtCharge = New System.Windows.Forms.TextBox()
        lblCharge = New System.Windows.Forms.Label()
        txtName = New System.Windows.Forms.TextBox()
        lblName = New System.Windows.Forms.Label()
        grpResults = New System.Windows.Forms.GroupBox()
        lblStatusValue = New System.Windows.Forms.Label()
        lblStatusCaption = New System.Windows.Forms.Label()
        lblCalValue = New System.Windows.Forms.Label()
        lblCalCaption = New System.Windows.Forms.Label()
        lblWorkValue = New System.Windows.Forms.Label()
        lblWorkCaption = New System.Windows.Forms.Label()
        lblDeadlineValue = New System.Windows.Forms.Label()
        lblDeadlineCaption = New System.Windows.Forms.Label()
        grpNotes = New System.Windows.Forms.GroupBox()
        txtNotes = New System.Windows.Forms.TextBox()
        btnCalculate = New System.Windows.Forms.Button()
        btnSave = New System.Windows.Forms.Button()
        btnClear = New System.Windows.Forms.Button()
        btnDelete = New System.Windows.Forms.Button()
        lblRecords = New System.Windows.Forms.Label()
        lvRecords = New System.Windows.Forms.ListView()
        colName = New System.Windows.Forms.ColumnHeader()
        colCharge = New System.Windows.Forms.ColumnHeader()
        colDeadline = New System.Windows.Forms.ColumnHeader()
        colWork = New System.Windows.Forms.ColumnHeader()
        colCal = New System.Windows.Forms.ColumnHeader()
        grpCase.SuspendLayout()
        CType(nudDays, ComponentModel.ISupportInitialize).BeginInit()
        grpResults.SuspendLayout()
        grpNotes.SuspendLayout()
        SuspendLayout()
        ' 
        ' grpCase
        ' 
        grpCase.Controls.Add(nudDays)
        grpCase.Controls.Add(lblDays)
        grpCase.Controls.Add(dtpSubsequent)
        grpCase.Controls.Add(lblSubsequent)
        grpCase.Controls.Add(dtpInitial)
        grpCase.Controls.Add(lblInitial)
        grpCase.Controls.Add(txtCharge)
        grpCase.Controls.Add(lblCharge)
        grpCase.Controls.Add(txtName)
        grpCase.Controls.Add(lblName)
        grpCase.Location = New System.Drawing.Point(12, 12)
        grpCase.Name = "grpCase"
        grpCase.Size = New System.Drawing.Size(440, 215)
        grpCase.TabIndex = 0
        grpCase.TabStop = False
        grpCase.Text = "Case Information"
        ' 
        ' nudDays
        ' 
        nudDays.Location = New System.Drawing.Point(180, 164)
        nudDays.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        nudDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        nudDays.Name = "nudDays"
        nudDays.Size = New System.Drawing.Size(70, 23)
        nudDays.TabIndex = 9
        nudDays.Value = New Decimal(New Integer() {2, 0, 0, 0})
        ' 
        ' lblDays
        ' 
        lblDays.AutoSize = True
        lblDays.Location = New System.Drawing.Point(12, 167)
        lblDays.Name = "lblDays"
        lblDays.Size = New System.Drawing.Size(162, 15)
        lblDays.TabIndex = 8
        lblDays.Text = "Working Days to File Petition:"
        ' 
        ' dtpSubsequent
        ' 
        dtpSubsequent.Checked = False
        dtpSubsequent.Format = System.Windows.Forms.DateTimePickerFormat.Short
        dtpSubsequent.Location = New System.Drawing.Point(180, 130)
        dtpSubsequent.Name = "dtpSubsequent"
        dtpSubsequent.ShowCheckBox = True
        dtpSubsequent.Size = New System.Drawing.Size(130, 23)
        dtpSubsequent.TabIndex = 7
        ' 
        ' lblSubsequent
        ' 
        lblSubsequent.AutoSize = True
        lblSubsequent.Location = New System.Drawing.Point(12, 133)
        lblSubsequent.Name = "lblSubsequent"
        lblSubsequent.Size = New System.Drawing.Size(154, 15)
        lblSubsequent.TabIndex = 6
        lblSubsequent.Text = "Subsequent Detention Date:"
        ' 
        ' dtpInitial
        ' 
        dtpInitial.Format = System.Windows.Forms.DateTimePickerFormat.Short
        dtpInitial.Location = New System.Drawing.Point(180, 96)
        dtpInitial.Name = "dtpInitial"
        dtpInitial.Size = New System.Drawing.Size(130, 23)
        dtpInitial.TabIndex = 5
        ' 
        ' lblInitial
        ' 
        lblInitial.AutoSize = True
        lblInitial.Location = New System.Drawing.Point(12, 99)
        lblInitial.Name = "lblInitial"
        lblInitial.Size = New System.Drawing.Size(121, 15)
        lblInitial.TabIndex = 4
        lblInitial.Text = "Initial Detention Date:"
        ' 
        ' txtCharge
        ' 
        txtCharge.Location = New System.Drawing.Point(180, 62)
        txtCharge.MaxLength = 150
        txtCharge.Name = "txtCharge"
        txtCharge.Size = New System.Drawing.Size(240, 23)
        txtCharge.TabIndex = 3
        ' 
        ' lblCharge
        ' 
        lblCharge.AutoSize = True
        lblCharge.Location = New System.Drawing.Point(12, 65)
        lblCharge.Name = "lblCharge"
        lblCharge.Size = New System.Drawing.Size(48, 15)
        lblCharge.TabIndex = 2
        lblCharge.Text = "Charge:"
        ' 
        ' txtName
        ' 
        txtName.Location = New System.Drawing.Point(180, 28)
        txtName.MaxLength = 100
        txtName.Name = "txtName"
        txtName.Size = New System.Drawing.Size(240, 23)
        txtName.TabIndex = 1
        ' 
        ' lblName
        ' 
        lblName.AutoSize = True
        lblName.Location = New System.Drawing.Point(12, 31)
        lblName.Name = "lblName"
        lblName.Size = New System.Drawing.Size(81, 15)
        lblName.TabIndex = 0
        lblName.Text = "Child's Name:"
        ' 
        ' grpResults
        ' 
        grpResults.Controls.Add(lblStatusValue)
        grpResults.Controls.Add(lblStatusCaption)
        grpResults.Controls.Add(lblCalValue)
        grpResults.Controls.Add(lblCalCaption)
        grpResults.Controls.Add(lblWorkValue)
        grpResults.Controls.Add(lblWorkCaption)
        grpResults.Controls.Add(lblDeadlineValue)
        grpResults.Controls.Add(lblDeadlineCaption)
        grpResults.Location = New System.Drawing.Point(12, 235)
        grpResults.Name = "grpResults"
        grpResults.Size = New System.Drawing.Size(440, 150)
        grpResults.TabIndex = 1
        grpResults.TabStop = False
        grpResults.Text = "Results"
        ' 
        ' lblStatusValue
        ' 
        lblStatusValue.Font = New System.Drawing.Font("Segoe UI", 10F, Drawing.FontStyle.Bold)
        lblStatusValue.Location = New System.Drawing.Point(200, 116)
        lblStatusValue.Name = "lblStatusValue"
        lblStatusValue.Size = New System.Drawing.Size(230, 20)
        lblStatusValue.TabIndex = 7
        ' 
        ' lblStatusCaption
        ' 
        lblStatusCaption.AutoSize = True
        lblStatusCaption.Location = New System.Drawing.Point(12, 118)
        lblStatusCaption.Name = "lblStatusCaption"
        lblStatusCaption.Size = New System.Drawing.Size(42, 15)
        lblStatusCaption.TabIndex = 6
        lblStatusCaption.Text = "Status:"
        ' 
        ' lblCalValue
        ' 
        lblCalValue.Font = New System.Drawing.Font("Segoe UI", 10F, Drawing.FontStyle.Bold)
        lblCalValue.Location = New System.Drawing.Point(200, 86)
        lblCalValue.Name = "lblCalValue"
        lblCalValue.Size = New System.Drawing.Size(230, 20)
        lblCalValue.TabIndex = 5
        ' 
        ' lblCalCaption
        ' 
        lblCalCaption.AutoSize = True
        lblCalCaption.Location = New System.Drawing.Point(12, 88)
        lblCalCaption.Name = "lblCalCaption"
        lblCalCaption.Size = New System.Drawing.Size(145, 15)
        lblCalCaption.TabIndex = 4
        lblCalCaption.Text = "Calendar Days Remaining:"
        ' 
        ' lblWorkValue
        ' 
        lblWorkValue.Font = New System.Drawing.Font("Segoe UI", 10F, Drawing.FontStyle.Bold)
        lblWorkValue.Location = New System.Drawing.Point(200, 56)
        lblWorkValue.Name = "lblWorkValue"
        lblWorkValue.Size = New System.Drawing.Size(230, 20)
        lblWorkValue.TabIndex = 3
        ' 
        ' lblWorkCaption
        ' 
        lblWorkCaption.AutoSize = True
        lblWorkCaption.Location = New System.Drawing.Point(12, 58)
        lblWorkCaption.Name = "lblWorkCaption"
        lblWorkCaption.Size = New System.Drawing.Size(143, 15)
        lblWorkCaption.TabIndex = 2
        lblWorkCaption.Text = "Working Days Remaining:"
        ' 
        ' lblDeadlineValue
        ' 
        lblDeadlineValue.Font = New System.Drawing.Font("Segoe UI", 10F, Drawing.FontStyle.Bold)
        lblDeadlineValue.Location = New System.Drawing.Point(200, 26)
        lblDeadlineValue.Name = "lblDeadlineValue"
        lblDeadlineValue.Size = New System.Drawing.Size(230, 20)
        lblDeadlineValue.TabIndex = 1
        ' 
        ' lblDeadlineCaption
        ' 
        lblDeadlineCaption.AutoSize = True
        lblDeadlineCaption.Location = New System.Drawing.Point(12, 28)
        lblDeadlineCaption.Name = "lblDeadlineCaption"
        lblDeadlineCaption.Size = New System.Drawing.Size(100, 15)
        lblDeadlineCaption.TabIndex = 0
        lblDeadlineCaption.Text = "Petition Deadline:"
        ' 
        ' grpNotes
        ' 
        grpNotes.Controls.Add(txtNotes)
        grpNotes.Location = New System.Drawing.Point(12, 393)
        grpNotes.Name = "grpNotes"
        grpNotes.Size = New System.Drawing.Size(440, 235)
        grpNotes.TabIndex = 2
        grpNotes.TabStop = False
        grpNotes.Text = "Notes"
        ' 
        ' txtNotes
        ' 
        txtNotes.AcceptsReturn = True
        txtNotes.Location = New System.Drawing.Point(12, 24)
        txtNotes.Multiline = True
        txtNotes.Name = "txtNotes"
        txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        txtNotes.Size = New System.Drawing.Size(416, 196)
        txtNotes.TabIndex = 0
        ' 
        ' btnCalculate
        ' 
        btnCalculate.Location = New System.Drawing.Point(470, 12)
        btnCalculate.Name = "btnCalculate"
        btnCalculate.Size = New System.Drawing.Size(108, 32)
        btnCalculate.TabIndex = 3
        btnCalculate.Text = "Calculate"
        btnCalculate.UseVisualStyleBackColor = True
        ' 
        ' btnSave
        ' 
        btnSave.Location = New System.Drawing.Point(584, 12)
        btnSave.Name = "btnSave"
        btnSave.Size = New System.Drawing.Size(108, 32)
        btnSave.TabIndex = 4
        btnSave.Text = "Save"
        btnSave.UseVisualStyleBackColor = True
        ' 
        ' btnClear
        ' 
        btnClear.Location = New System.Drawing.Point(698, 12)
        btnClear.Name = "btnClear"
        btnClear.Size = New System.Drawing.Size(108, 32)
        btnClear.TabIndex = 5
        btnClear.Text = "Clear"
        btnClear.UseVisualStyleBackColor = True
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New System.Drawing.Point(812, 12)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New System.Drawing.Size(108, 32)
        btnDelete.TabIndex = 6
        btnDelete.Text = "Delete"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' lblRecords
        ' 
        lblRecords.AutoSize = True
        lblRecords.Location = New System.Drawing.Point(470, 58)
        lblRecords.Name = "lblRecords"
        lblRecords.Size = New System.Drawing.Size(203, 15)
        lblRecords.TabIndex = 7
        lblRecords.Text = "Saved Records (click a row to load it):"
        ' 
        ' lvRecords
        ' 
        lvRecords.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {colName, colCharge, colDeadline, colWork, colCal})
        lvRecords.FullRowSelect = True
        lvRecords.GridLines = True
        lvRecords.Location = New System.Drawing.Point(470, 78)
        lvRecords.MultiSelect = False
        lvRecords.Name = "lvRecords"
        lvRecords.Size = New System.Drawing.Size(458, 550)
        lvRecords.TabIndex = 8
        lvRecords.UseCompatibleStateImageBehavior = False
        lvRecords.View = System.Windows.Forms.View.Details
        ' 
        ' colName
        ' 
        colName.Text = "Name"
        colName.Width = 105
        ' 
        ' colCharge
        ' 
        colCharge.Text = "Charge"
        colCharge.Width = 115
        ' 
        ' colDeadline
        ' 
        colDeadline.Text = "Deadline"
        colDeadline.Width = 85
        ' 
        ' colWork
        ' 
        colWork.Text = "Work Days"
        colWork.Width = 75
        ' 
        ' colCal
        ' 
        colCal.Text = "Cal. Days"
        colCal.Width = 70
        ' 
        ' frmMain
        ' 
        AcceptButton = btnCalculate
        AutoScaleDimensions = New System.Drawing.SizeF(7F, 15F)
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(940, 640)
        Controls.Add(lvRecords)
        Controls.Add(lblRecords)
        Controls.Add(btnDelete)
        Controls.Add(btnClear)
        Controls.Add(btnSave)
        Controls.Add(btnCalculate)
        Controls.Add(grpNotes)
        Controls.Add(grpResults)
        Controls.Add(grpCase)
        Font = New System.Drawing.Font("Segoe UI", 9F)
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        MaximizeBox = False
        Name = "frmMain"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "Petition Deadlines for Detention"
        grpCase.ResumeLayout(False)
        grpCase.PerformLayout()
        CType(nudDays, ComponentModel.ISupportInitialize).EndInit()
        grpResults.ResumeLayout(False)
        grpResults.PerformLayout()
        grpNotes.ResumeLayout(False)
        grpNotes.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub

    Friend WithEvents grpCase As System.Windows.Forms.GroupBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents lblCharge As System.Windows.Forms.Label
    Friend WithEvents txtCharge As System.Windows.Forms.TextBox
    Friend WithEvents lblInitial As System.Windows.Forms.Label
    Friend WithEvents dtpInitial As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSubsequent As System.Windows.Forms.Label
    Friend WithEvents dtpSubsequent As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDays As System.Windows.Forms.Label
    Friend WithEvents nudDays As System.Windows.Forms.NumericUpDown
    Friend WithEvents grpResults As System.Windows.Forms.GroupBox
    Friend WithEvents lblDeadlineCaption As System.Windows.Forms.Label
    Friend WithEvents lblDeadlineValue As System.Windows.Forms.Label
    Friend WithEvents lblWorkCaption As System.Windows.Forms.Label
    Friend WithEvents lblWorkValue As System.Windows.Forms.Label
    Friend WithEvents lblCalCaption As System.Windows.Forms.Label
    Friend WithEvents lblCalValue As System.Windows.Forms.Label
    Friend WithEvents lblStatusCaption As System.Windows.Forms.Label
    Friend WithEvents lblStatusValue As System.Windows.Forms.Label
    Friend WithEvents grpNotes As System.Windows.Forms.GroupBox
    Friend WithEvents txtNotes As System.Windows.Forms.TextBox
    Friend WithEvents btnCalculate As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lblRecords As System.Windows.Forms.Label
    Friend WithEvents lvRecords As System.Windows.Forms.ListView
    Friend WithEvents colName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCharge As System.Windows.Forms.ColumnHeader
    Friend WithEvents colDeadline As System.Windows.Forms.ColumnHeader
    Friend WithEvents colWork As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCal As System.Windows.Forms.ColumnHeader

End Class
