<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucInvoices
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
        Me.TxtFrom = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.TxtTo = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmdRefresh = New System.Windows.Forms.Button()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraGridInvoices = New Infragistics.Win.UltraWinGrid.UltraGrid()
        CType(Me.TxtFrom, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGridInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtFrom
        '
        Me.TxtFrom.DateTime = New Date(2018, 3, 21, 0, 0, 0, 0)
        Me.TxtFrom.Location = New System.Drawing.Point(56, 3)
        Me.TxtFrom.Name = "TxtFrom"
        Me.TxtFrom.Size = New System.Drawing.Size(110, 21)
        Me.TxtFrom.TabIndex = 0
        Me.TxtFrom.Value = New Date(2018, 3, 21, 0, 0, 0, 0)
        '
        'TxtTo
        '
        Me.TxtTo.DateTime = New Date(2018, 3, 21, 0, 0, 0, 0)
        Me.TxtTo.Location = New System.Drawing.Point(201, 3)
        Me.TxtTo.Name = "TxtTo"
        Me.TxtTo.Size = New System.Drawing.Size(110, 21)
        Me.TxtTo.TabIndex = 1
        Me.TxtTo.Value = New Date(2018, 3, 21, 0, 0, 0, 0)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(175, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(20, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "To"
        '
        'CmdRefresh
        '
        Me.CmdRefresh.Location = New System.Drawing.Point(334, 3)
        Me.CmdRefresh.Name = "CmdRefresh"
        Me.CmdRefresh.Size = New System.Drawing.Size(75, 23)
        Me.CmdRefresh.TabIndex = 2
        Me.CmdRefresh.Text = "Refresh"
        Me.CmdRefresh.UseVisualStyleBackColor = True
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
        'UltraGridInvoices
        '
        Me.UltraGridInvoices.AllowDrop = True
        Me.UltraGridInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraGridInvoices.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridInvoices.Location = New System.Drawing.Point(3, 32)
        Me.UltraGridInvoices.Name = "UltraGridInvoices"
        Me.UltraGridInvoices.Size = New System.Drawing.Size(1581, 694)
        Me.UltraGridInvoices.TabIndex = 4
        '
        'ucInvoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UltraGridInvoices)
        Me.Controls.Add(Me.CmdRefresh)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtTo)
        Me.Controls.Add(Me.TxtFrom)
        Me.Name = "ucInvoices"
        Me.Size = New System.Drawing.Size(1587, 742)
        CType(Me.TxtFrom, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGridInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtFrom As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents TxtTo As Infragistics.Win.UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmdRefresh As System.Windows.Forms.Button
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraGridInvoices As Infragistics.Win.UltraWinGrid.UltraGrid
End Class
