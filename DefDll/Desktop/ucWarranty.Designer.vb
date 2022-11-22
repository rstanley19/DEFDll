<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucWarranty
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
        Me.UltraGridWarranty = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.CmdCreate = New System.Windows.Forms.Button()
        CType(Me.UltraGridWarranty, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'UltraGridWarranty
        '
        Me.UltraGridWarranty.AllowDrop = True
        Me.UltraGridWarranty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraGridWarranty.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridWarranty.Location = New System.Drawing.Point(3, 32)
        Me.UltraGridWarranty.Name = "UltraGridWarranty"
        Me.UltraGridWarranty.Size = New System.Drawing.Size(1569, 694)
        Me.UltraGridWarranty.TabIndex = 4
        '
        'CmdCreate
        '
        Me.CmdCreate.Location = New System.Drawing.Point(3, 3)
        Me.CmdCreate.Name = "CmdCreate"
        Me.CmdCreate.Size = New System.Drawing.Size(132, 23)
        Me.CmdCreate.TabIndex = 6
        Me.CmdCreate.Text = "Create Warranty"
        Me.CmdCreate.UseVisualStyleBackColor = True
        '
        'ucWarranty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CmdCreate)
        Me.Controls.Add(Me.UltraGridWarranty)
        Me.Name = "ucWarranty"
        Me.Size = New System.Drawing.Size(1587, 742)
        CType(Me.UltraGridWarranty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraGridWarranty As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents CmdCreate As Windows.Forms.Button
End Class
