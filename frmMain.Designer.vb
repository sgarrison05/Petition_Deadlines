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
        Me.grpCase = New System.Windows.Forms.GroupBox()
        Me.nudDays = New System.Windows.Forms.NumericUpDown()
        Me.lblDays = New System.Windows.Forms.Label()
        Me.dtpSubsequent = New System.Windows.Forms.DateTimePicker()
        Me.lblSubsequent = New System.Windows.Forms.Label()
        Me.dtpInitial = New System.Windows.Forms.DateTimePicker()
        Me.lblInitial = New System.Windows.Forms.Label()
        Me.txtCharge = New System.Windows.Forms.TextBox()
        Me.lblCharge = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.grpResults = New System.Windows.Forms.GroupBox()
        Me.lblStatusValue = New System.Windows.Forms.Label()
        Me.lblStatusCaption = New System.Windows.Forms.Label()
        Me.lblCalValue = New System.Windows.Forms.Label()
        Me.lblCalCaption = New System.Windows.Forms.Label()
        Me.lblWorkValue = New System.Windows.Forms.Label()
        Me.lblWorkCaption = New System.Windows.Forms.Label()
        Me.lblDeadlineValue = New System.Windows.Forms.Label()
        Me.lblDeadlineCaption = New System.Windows.Forms.Label()
        Me.grpNotes = New System.Windows.Forms.GroupBox()
        Me.txtNotes = New System.Windows.Forms.TextBox()
        Me.btnCalculate = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.lblRecords = New System.Windows.Forms.Label()
        Me.lvRecords = New System.Windows.Forms.ListView()
        Me.colName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCharge = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDeadline = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colWork = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.grpCase.SuspendLayout()
        CType(Me.nudDays, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpResults.SuspendLayout()
        Me.grpNotes.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpCase
        '
        Me.grpCase.Controls.Add(Me.nudDays)
        Me.grpCase.Controls.Add(Me.lblDays)
        Me.grpCase.Controls.Add(Me.dtpSubsequent)
        Me.grpCase.Controls.Add(Me.lblSubsequent)
        Me.grpCase.Controls.Add(Me.dtpInitial)
        Me.grpCase.Controls.Add(Me.lblInitial)
        Me.grpCase.Controls.Add(Me.txtCharge)
        Me.grpCase.Controls.Add(Me.lblCharge)
        Me.grpCase.Controls.Add(Me.txtName)
        Me.grpCase.Controls.Add(Me.lblName)
        Me.grpCase.Location = New System.Drawing.Point(12, 12)
        Me.grpCase.Name = "grpCase"
        Me.grpCase.Size = New System.Drawing.Size(440, 215)
        Me.grpCase.TabIndex = 0
        Me.grpCase.TabStop = False
        Me.grpCase.Text = "Case Information"
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(12, 31)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(73, 15)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Child's Name:"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(180, 28)
        Me.txtName.MaxLength = 100
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(240, 23)
        Me.txtName.TabIndex = 1
        '
        'lblCharge
        '
        Me.lblCharge.AutoSize = True
        Me.lblCharge.Location = New System.Drawing.Point(12, 65)
        Me.lblCharge.Name = "lblCharge"
        Me.lblCharge.Size = New System.Drawing.Size(47, 15)
        Me.lblCharge.TabIndex = 2
        Me.lblCharge.Text = "Charge:"
        '
        'txtCharge
        '
        Me.txtCharge.Location = New System.Drawing.Point(180, 62)
        Me.txtCharge.MaxLength = 150
        Me.txtCharge.Name = "txtCharge"
        Me.txtCharge.Size = New System.Drawing.Size(240, 23)
        Me.txtCharge.TabIndex = 3
        '
        'lblInitial
        '
        Me.lblInitial.AutoSize = True
        Me.lblInitial.Location = New System.Drawing.Point(12, 99)
        Me.lblInitial.Name = "lblInitial"
        Me.lblInitial.Size = New System.Drawing.Size(150, 15)
        Me.lblInitial.TabIndex = 4
        Me.lblInitial.Text = "Initial Detention Date:"
        '
        'dtpInitial
        '
        Me.dtpInitial.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpInitial.Location = New System.Drawing.Point(180, 96)
        Me.dtpInitial.Name = "dtpInitial"
        Me.dtpInitial.Size = New System.Drawing.Size(130, 23)
        Me.dtpInitial.TabIndex = 5
        '
        'lblSubsequent
        '
        Me.lblSubsequent.AutoSize = True
        Me.lblSubsequent.Location = New System.Drawing.Point(12, 133)
        Me.lblSubsequent.Name = "lblSubsequent"
        Me.lblSubsequent.Size = New System.Drawing.Size(150, 15)
        Me.lblSubsequent.TabIndex = 6
        Me.lblSubsequent.Text = "Subsequent Detention Date:"
        '
        'dtpSubsequent
        '
        Me.dtpSubsequent.Checked = False
        Me.dtpSubsequent.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpSubsequent.Location = New System.Drawing.Point(180, 130)
        Me.dtpSubsequent.Name = "dtpSubsequent"
        Me.dtpSubsequent.ShowCheckBox = True
        Me.dtpSubsequent.Size = New System.Drawing.Size(130, 23)
        Me.dtpSubsequent.TabIndex = 7
        '
        'lblDays
        '
        Me.lblDays.AutoSize = True
        Me.lblDays.Location = New System.Drawing.Point(12, 167)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.Size = New System.Drawing.Size(150, 15)
        Me.lblDays.TabIndex = 8
        Me.lblDays.Text = "Working Days to File Petition:"
        '
        'nudDays
        '
        Me.nudDays.Location = New System.Drawing.Point(180, 164)
        Me.nudDays.Maximum = New Decimal(New Integer() {365, 0, 0, 0})
        Me.nudDays.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudDays.Name = "nudDays"
        Me.nudDays.Size = New System.Drawing.Size(70, 23)
        Me.nudDays.TabIndex = 9
        Me.nudDays.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'grpResults
        '
        Me.grpResults.Controls.Add(Me.lblStatusValue)
        Me.grpResults.Controls.Add(Me.lblStatusCaption)
        Me.grpResults.Controls.Add(Me.lblCalValue)
        Me.grpResults.Controls.Add(Me.lblCalCaption)
        Me.grpResults.Controls.Add(Me.lblWorkValue)
        Me.grpResults.Controls.Add(Me.lblWorkCaption)
        Me.grpResults.Controls.Add(Me.lblDeadlineValue)
        Me.grpResults.Controls.Add(Me.lblDeadlineCaption)
        Me.grpResults.Location = New System.Drawing.Point(12, 235)
        Me.grpResults.Name = "grpResults"
        Me.grpResults.Size = New System.Drawing.Size(440, 150)
        Me.grpResults.TabIndex = 1
        Me.grpResults.TabStop = False
        Me.grpResults.Text = "Results"
        '
        'lblDeadlineCaption
        '
        Me.lblDeadlineCaption.AutoSize = True
        Me.lblDeadlineCaption.Location = New System.Drawing.Point(12, 28)
        Me.lblDeadlineCaption.Name = "lblDeadlineCaption"
        Me.lblDeadlineCaption.Size = New System.Drawing.Size(85, 15)
        Me.lblDeadlineCaption.TabIndex = 0
        Me.lblDeadlineCaption.Text = "Petition Deadline:"
        '
        'lblDeadlineValue
        '
        Me.lblDeadlineValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDeadlineValue.Location = New System.Drawing.Point(200, 26)
        Me.lblDeadlineValue.Name = "lblDeadlineValue"
        Me.lblDeadlineValue.Size = New System.Drawing.Size(230, 20)
        Me.lblDeadlineValue.TabIndex = 1
        '
        'lblWorkCaption
        '
        Me.lblWorkCaption.AutoSize = True
        Me.lblWorkCaption.Location = New System.Drawing.Point(12, 58)
        Me.lblWorkCaption.Name = "lblWorkCaption"
        Me.lblWorkCaption.Size = New System.Drawing.Size(130, 15)
        Me.lblWorkCaption.TabIndex = 2
        Me.lblWorkCaption.Text = "Working Days Remaining:"
        '
        'lblWorkValue
        '
        Me.lblWorkValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblWorkValue.Location = New System.Drawing.Point(200, 56)
        Me.lblWorkValue.Name = "lblWorkValue"
        Me.lblWorkValue.Size = New System.Drawing.Size(230, 20)
        Me.lblWorkValue.TabIndex = 3
        '
        'lblCalCaption
        '
        Me.lblCalCaption.AutoSize = True
        Me.lblCalCaption.Location = New System.Drawing.Point(12, 88)
        Me.lblCalCaption.Name = "lblCalCaption"
        Me.lblCalCaption.Size = New System.Drawing.Size(130, 15)
        Me.lblCalCaption.TabIndex = 4
        Me.lblCalCaption.Text = "Calendar Days Remaining:"
        '
        'lblCalValue
        '
        Me.lblCalValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblCalValue.Location = New System.Drawing.Point(200, 86)
        Me.lblCalValue.Name = "lblCalValue"
        Me.lblCalValue.Size = New System.Drawing.Size(230, 20)
        Me.lblCalValue.TabIndex = 5
        '
        'lblStatusCaption
        '
        Me.lblStatusCaption.AutoSize = True
        Me.lblStatusCaption.Location = New System.Drawing.Point(12, 118)
        Me.lblStatusCaption.Name = "lblStatusCaption"
        Me.lblStatusCaption.Size = New System.Drawing.Size(42, 15)
        Me.lblStatusCaption.TabIndex = 6
        Me.lblStatusCaption.Text = "Status:"
        '
        'lblStatusValue
        '
        Me.lblStatusValue.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblStatusValue.Location = New System.Drawing.Point(200, 116)
        Me.lblStatusValue.Name = "lblStatusValue"
        Me.lblStatusValue.Size = New System.Drawing.Size(230, 20)
        Me.lblStatusValue.TabIndex = 7
        '
        'grpNotes
        '
        Me.grpNotes.Controls.Add(Me.txtNotes)
        Me.grpNotes.Location = New System.Drawing.Point(12, 393)
        Me.grpNotes.Name = "grpNotes"
        Me.grpNotes.Size = New System.Drawing.Size(440, 235)
        Me.grpNotes.TabIndex = 2
        Me.grpNotes.TabStop = False
        Me.grpNotes.Text = "Notes"
        '
        'txtNotes
        '
        Me.txtNotes.AcceptsReturn = True
        Me.txtNotes.Location = New System.Drawing.Point(12, 24)
        Me.txtNotes.Multiline = True
        Me.txtNotes.Name = "txtNotes"
        Me.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNotes.Size = New System.Drawing.Size(416, 196)
        Me.txtNotes.TabIndex = 0
        '
        'btnCalculate
        '
        Me.btnCalculate.Location = New System.Drawing.Point(470, 12)
        Me.btnCalculate.Name = "btnCalculate"
        Me.btnCalculate.Size = New System.Drawing.Size(108, 32)
        Me.btnCalculate.TabIndex = 3
        Me.btnCalculate.Text = "Calculate"
        Me.btnCalculate.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(584, 12)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 32)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(698, 12)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(108, 32)
        Me.btnClear.TabIndex = 5
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(812, 12)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(108, 32)
        Me.btnDelete.TabIndex = 6
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'lblRecords
        '
        Me.lblRecords.AutoSize = True
        Me.lblRecords.Location = New System.Drawing.Point(470, 58)
        Me.lblRecords.Name = "lblRecords"
        Me.lblRecords.Size = New System.Drawing.Size(240, 15)
        Me.lblRecords.TabIndex = 7
        Me.lblRecords.Text = "Saved Records (click a row to load it):"
        '
        'lvRecords
        '
        Me.lvRecords.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colName, Me.colCharge, Me.colDeadline, Me.colWork, Me.colCal})
        Me.lvRecords.FullRowSelect = True
        Me.lvRecords.GridLines = True
        Me.lvRecords.HideSelection = False
        Me.lvRecords.Location = New System.Drawing.Point(470, 78)
        Me.lvRecords.MultiSelect = False
        Me.lvRecords.Name = "lvRecords"
        Me.lvRecords.Size = New System.Drawing.Size(458, 550)
        Me.lvRecords.TabIndex = 8
        Me.lvRecords.UseCompatibleStateImageBehavior = False
        Me.lvRecords.View = System.Windows.Forms.View.Details
        '
        'colName
        '
        Me.colName.Text = "Name"
        Me.colName.Width = 105
        '
        'colCharge
        '
        Me.colCharge.Text = "Charge"
        Me.colCharge.Width = 115
        '
        'colDeadline
        '
        Me.colDeadline.Text = "Deadline"
        Me.colDeadline.Width = 85
        '
        'colWork
        '
        Me.colWork.Text = "Work Days"
        Me.colWork.Width = 75
        '
        'colCal
        '
        Me.colCal.Text = "Cal. Days"
        Me.colCal.Width = 70
        '
        'frmMain
        '
        Me.AcceptButton = Me.btnCalculate
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(940, 640)
        Me.Controls.Add(Me.lvRecords)
        Me.Controls.Add(Me.lblRecords)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnCalculate)
        Me.Controls.Add(Me.grpNotes)
        Me.Controls.Add(Me.grpResults)
        Me.Controls.Add(Me.grpCase)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Petition Deadlines for Detention"
        Me.grpCase.ResumeLayout(False)
        Me.grpCase.PerformLayout()
        CType(Me.nudDays, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpResults.ResumeLayout(False)
        Me.grpResults.PerformLayout()
        Me.grpNotes.ResumeLayout(False)
        Me.grpNotes.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
