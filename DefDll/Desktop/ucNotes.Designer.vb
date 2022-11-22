<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucNotes
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
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraGridNotes = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.TxtNote = New System.Windows.Forms.RichTextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        CType(Me.UltraGridNotes, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.UltraGridNotes.Location = New System.Drawing.Point(3, 32)
        Me.UltraGridNotes.Name = "UltraGridNotes"
        Me.UltraGridNotes.Size = New System.Drawing.Size(856, 694)
        Me.UltraGridNotes.TabIndex = 4
        '
        'TxtNote
        '
        Me.TxtNote.Location = New System.Drawing.Point(865, 32)
        Me.TxtNote.Name = "TxtNote"
        Me.TxtNote.Size = New System.Drawing.Size(706, 694)
        Me.TxtNote.TabIndex = 5
        Me.TxtNote.Text = ""
        '
        'CmdSave
        '
        Me.CmdSave.Location = New System.Drawing.Point(865, 3)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.Size = New System.Drawing.Size(75, 23)
        Me.CmdSave.TabIndex = 6
        Me.CmdSave.Text = "Save Note"
        Me.CmdSave.UseVisualStyleBackColor = True
        '
        'ucNotes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CmdSave)
        Me.Controls.Add(Me.TxtNote)
        Me.Controls.Add(Me.UltraGridNotes)
        Me.Name = "ucNotes"
        Me.Size = New System.Drawing.Size(1587, 742)
        CType(Me.UltraGridNotes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraGridNotes As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents TxtNote As Windows.Forms.RichTextBox
    Friend WithEvents CmdSave As Windows.Forms.Button
End Class
