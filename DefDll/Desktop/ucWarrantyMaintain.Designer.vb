<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucWarrantyMaintain
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim ValueListItem4 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem9 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem10 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem18 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Dim ValueListItem1 As Infragistics.Win.ValueListItem = New Infragistics.Win.ValueListItem()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraGridNotes = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.TxtNote = New System.Windows.Forms.RichTextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cbWarrantyPeriod = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cbVendor = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
        Me.TxtItem = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtSerial = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtStartDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtEndDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtLengthOfWarranty = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.SaveAndCloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseWithoutSavingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TxtPO = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.UltraGridNotes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbWarrantyPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtStartDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtEndDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtLengthOfWarranty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1113, 304)
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(1113, 304)
        '
        'UltraGridNotes
        '
        Me.UltraGridNotes.AllowDrop = True
        Me.UltraGridNotes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraGridNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridNotes.Location = New System.Drawing.Point(3, 405)
        Me.UltraGridNotes.Name = "UltraGridNotes"
        Me.UltraGridNotes.Size = New System.Drawing.Size(856, 321)
        Me.UltraGridNotes.TabIndex = 8
        '
        'TxtNote
        '
        Me.TxtNote.Location = New System.Drawing.Point(865, 32)
        Me.TxtNote.Name = "TxtNote"
        Me.TxtNote.Size = New System.Drawing.Size(706, 694)
        Me.TxtNote.TabIndex = 9
        Me.TxtNote.Text = ""
        '
        'CmdSave
        '
        Me.CmdSave.Location = New System.Drawing.Point(865, 3)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Size = New System.Drawing.Size(75, 23)
        Me.CmdSave.TabIndex = 10
        Me.CmdSave.Text = "Save Note"
        Me.CmdSave.UseVisualStyleBackColor = True
        '
        'cbWarrantyPeriod
        '
        ValueListItem4.DataValue = ""
        ValueListItem4.DisplayText = "Unknown"
        ValueListItem9.DataValue = "D"
        ValueListItem9.DisplayText = "Days"
        ValueListItem10.DataValue = "W"
        ValueListItem10.DisplayText = "Weeks"
        ValueListItem18.DataValue = "M"
        ValueListItem18.DisplayText = "Months"
        ValueListItem1.DataValue = "Y"
        ValueListItem1.DisplayText = "Years"
        Me.cbWarrantyPeriod.Items.AddRange(New Infragistics.Win.ValueListItem() {ValueListItem4, ValueListItem9, ValueListItem10, ValueListItem18, ValueListItem1})
        Me.cbWarrantyPeriod.LimitToList = True
        Me.cbWarrantyPeriod.Location = New System.Drawing.Point(97, 179)
        Me.cbWarrantyPeriod.MaxLength = 30
        Me.cbWarrantyPeriod.Name = "cbWarrantyPeriod"
        Me.cbWarrantyPeriod.Size = New System.Drawing.Size(118, 21)
        Me.cbWarrantyPeriod.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(48, 35)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(45, 13)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Supplier"
        '
        'cbVendor
        '
        Me.cbVendor.Location = New System.Drawing.Point(97, 31)
        Me.cbVendor.MaxLength = 30
        Me.cbVendor.Name = "cbVendor"
        Me.cbVendor.Size = New System.Drawing.Size(398, 21)
        Me.cbVendor.TabIndex = 0
        '
        'TxtItem
        '
        Me.TxtItem.Location = New System.Drawing.Point(97, 61)
        Me.TxtItem.MaxLength = 50
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(398, 20)
        Me.TxtItem.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(66, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Item"
        '
        'TxtSerial
        '
        Me.TxtSerial.Location = New System.Drawing.Point(97, 90)
        Me.TxtSerial.MaxLength = 50
        Me.TxtSerial.Name = "TxtSerial"
        Me.TxtSerial.Size = New System.Drawing.Size(398, 20)
        Me.TxtSerial.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(60, 93)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Serial"
        '
        'TxtStartDate
        '
        Me.TxtStartDate.DateTime = New Date(2019, 9, 4, 0, 0, 0, 0)
        Me.TxtStartDate.Location = New System.Drawing.Point(97, 149)
        Me.TxtStartDate.Name = "TxtStartDate"
        Me.TxtStartDate.Size = New System.Drawing.Size(144, 21)
        Me.TxtStartDate.TabIndex = 4
        Me.TxtStartDate.Value = New Date(2019, 9, 4, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(64, 153)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Start"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(67, 243)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "End"
        '
        'TxtEndDate
        '
        Me.TxtEndDate.DateTime = New Date(2019, 9, 4, 0, 0, 0, 0)
        Me.TxtEndDate.Location = New System.Drawing.Point(97, 239)
        Me.TxtEndDate.Name = "TxtEndDate"
        Me.TxtEndDate.Size = New System.Drawing.Size(144, 21)
        Me.TxtEndDate.TabIndex = 7
        Me.TxtEndDate.Value = New Date(2019, 9, 4, 0, 0, 0, 0)
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(31, 183)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 35
        Me.Label5.Text = "Time Frame"
        '
        'TxtLengthOfWarranty
        '
        Me.TxtLengthOfWarranty.Location = New System.Drawing.Point(97, 209)
        Me.TxtLengthOfWarranty.MaskInput = "nnn"
        Me.TxtLengthOfWarranty.MaxValue = 999
        Me.TxtLengthOfWarranty.MinValue = 0
        Me.TxtLengthOfWarranty.Name = "TxtLengthOfWarranty"
        Me.TxtLengthOfWarranty.PromptChar = Global.Microsoft.VisualBasic.ChrW(32)
        Me.TxtLengthOfWarranty.Size = New System.Drawing.Size(45, 21)
        Me.TxtLengthOfWarranty.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(53, 213)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Length"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveAndCloseToolStripMenuItem, Me.CloseWithoutSavingToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1587, 24)
        Me.MenuStrip1.TabIndex = 38
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'SaveAndCloseToolStripMenuItem
        '
        Me.SaveAndCloseToolStripMenuItem.Name = "SaveAndCloseToolStripMenuItem"
        Me.SaveAndCloseToolStripMenuItem.Size = New System.Drawing.Size(98, 20)
        Me.SaveAndCloseToolStripMenuItem.Text = "Save and Close"
        '
        'CloseWithoutSavingToolStripMenuItem
        '
        Me.CloseWithoutSavingToolStripMenuItem.Name = "CloseWithoutSavingToolStripMenuItem"
        Me.CloseWithoutSavingToolStripMenuItem.Size = New System.Drawing.Size(130, 20)
        Me.CloseWithoutSavingToolStripMenuItem.Text = "Close without Saving"
        '
        'TxtPO
        '
        Me.TxtPO.Location = New System.Drawing.Point(97, 120)
        Me.TxtPO.MaxLength = 25
        Me.TxtPO.Name = "TxtPO"
        Me.TxtPO.Size = New System.Drawing.Size(398, 20)
        Me.TxtPO.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(24, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Customer PO"
        '
        'ucWarrantyMaintain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TxtPO)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtLengthOfWarranty)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtEndDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtStartDate)
        Me.Controls.Add(Me.cbWarrantyPeriod)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cbVendor)
        Me.Controls.Add(Me.TxtItem)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtSerial)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.CmdSave)
        Me.Controls.Add(Me.TxtNote)
        Me.Controls.Add(Me.UltraGridNotes)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "ucWarrantyMaintain"
        Me.Size = New System.Drawing.Size(1587, 742)
        CType(Me.UltraGridNotes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbWarrantyPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtStartDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtEndDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtLengthOfWarranty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraGridNotes As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents TxtNote As Windows.Forms.RichTextBox
    Friend WithEvents CmdSave As Windows.Forms.Button
    Friend WithEvents cbWarrantyPeriod As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents Label13 As Windows.Forms.Label
    Friend WithEvents cbVendor As Infragistics.Win.UltraWinEditors.UltraComboEditor
    Friend WithEvents TxtItem As Windows.Forms.TextBox
    Friend WithEvents Label3 As Windows.Forms.Label
    Friend WithEvents TxtSerial As Windows.Forms.TextBox
    Friend WithEvents Label4 As Windows.Forms.Label
    Friend WithEvents TxtStartDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label1 As Windows.Forms.Label
    Friend WithEvents Label2 As Windows.Forms.Label
    Friend WithEvents TxtEndDate As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label5 As Windows.Forms.Label
    Friend WithEvents TxtLengthOfWarranty As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents Label6 As Windows.Forms.Label
    Friend WithEvents MenuStrip1 As Windows.Forms.MenuStrip
    Friend WithEvents SaveAndCloseToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseWithoutSavingToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents TxtPO As Windows.Forms.TextBox
    Friend WithEvents Label7 As Windows.Forms.Label
End Class
