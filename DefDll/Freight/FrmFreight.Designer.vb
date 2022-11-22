<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFreight
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridInvoices = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.ultraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.UltraGridCustomerLoads = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl9 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.llHedging = New System.Windows.Forms.LinkLabel()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl7 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabPageControl8 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraToolTipManager1 = New Infragistics.Win.UltraWinToolTip.UltraToolTipManager(Me.components)
        Me.CmdRefresh = New System.Windows.Forms.Button()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.TxtTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtFrom = New System.Windows.Forms.DateTimePicker()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraDockManager1 = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmCreditCardInvoicesAutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.UltraGridInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.UltraGridCustomerLoads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.UltraTabPageControl5.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraGridInvoices)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1449, 723)
        '
        'UltraGridInvoices
        '
        Me.UltraGridInvoices.AllowDrop = True
        Me.UltraGridInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BackColor = System.Drawing.Color.Wheat
        Appearance1.BackColor2 = System.Drawing.Color.Wheat
        Me.UltraGridInvoices.DisplayLayout.Appearance = Appearance1
        Me.UltraGridInvoices.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridInvoices.Location = New System.Drawing.Point(0, 3)
        Me.UltraGridInvoices.Name = "UltraGridInvoices"
        Me.UltraGridInvoices.Size = New System.Drawing.Size(1445, 720)
        Me.UltraGridInvoices.TabIndex = 5
        '
        'ultraTabPageControl3
        '
        Me.ultraTabPageControl3.Enabled = False
        Me.ultraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.ultraTabPageControl3.Name = "ultraTabPageControl3"
        Me.ultraTabPageControl3.Size = New System.Drawing.Size(788, 509)
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.Panel5)
        Me.UltraTabPageControl4.Enabled = False
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(788, 509)
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.UltraGridCustomerLoads)
        Me.Panel5.Location = New System.Drawing.Point(0, 1)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(788, 431)
        Me.Panel5.TabIndex = 48
        '
        'UltraGridCustomerLoads
        '
        Me.UltraGridCustomerLoads.AllowDrop = True
        Appearance2.BackColor = System.Drawing.Color.Wheat
        Appearance2.BackColor2 = System.Drawing.Color.Wheat
        Me.UltraGridCustomerLoads.DisplayLayout.Appearance = Appearance2
        Me.UltraGridCustomerLoads.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridCustomerLoads.Location = New System.Drawing.Point(0, 34)
        Me.UltraGridCustomerLoads.Name = "UltraGridCustomerLoads"
        Me.UltraGridCustomerLoads.Size = New System.Drawing.Size(789, 370)
        Me.UltraGridCustomerLoads.TabIndex = 47
        Me.UltraGridCustomerLoads.Visible = False
        '
        'UltraTabPageControl9
        '
        Me.UltraTabPageControl9.Controls.Add(Me.Panel6)
        Me.UltraTabPageControl9.Enabled = False
        Me.UltraTabPageControl9.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl9.Name = "UltraTabPageControl9"
        Me.UltraTabPageControl9.Size = New System.Drawing.Size(788, 509)
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.Controls.Add(Me.llHedging)
        Me.Panel6.Location = New System.Drawing.Point(0, 1)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(788, 431)
        Me.Panel6.TabIndex = 48
        '
        'llHedging
        '
        Me.llHedging.AutoSize = True
        Me.llHedging.LinkColor = System.Drawing.Color.SaddleBrown
        Me.llHedging.Location = New System.Drawing.Point(3, 15)
        Me.llHedging.Name = "llHedging"
        Me.llHedging.Size = New System.Drawing.Size(115, 13)
        Me.llHedging.TabIndex = 49
        Me.llHedging.TabStop = True
        Me.llHedging.Text = "Hedging  (Click to add)"
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(788, 406)
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(788, 406)
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.UltraGrid1)
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(788, 406)
        '
        'UltraGrid1
        '
        Me.UltraGrid1.AllowDrop = True
        Appearance3.BackColor = System.Drawing.Color.Wheat
        Appearance3.BackColor2 = System.Drawing.Color.Wheat
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance3
        Me.UltraGrid1.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(-1, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(789, 406)
        Me.UltraGrid1.TabIndex = 46
        Me.UltraGrid1.Visible = False
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(788, 406)
        '
        'UltraTabPageControl7
        '
        Me.UltraTabPageControl7.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl7.Name = "UltraTabPageControl7"
        Me.UltraTabPageControl7.Size = New System.Drawing.Size(901, 487)
        '
        'UltraTabPageControl8
        '
        Me.UltraTabPageControl8.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl8.Name = "UltraTabPageControl8"
        Me.UltraTabPageControl8.Size = New System.Drawing.Size(788, 406)
        '
        'UltraToolTipManager1
        '
        Me.UltraToolTipManager1.ContainingControl = Me
        '
        'CmdRefresh
        '
        Me.CmdRefresh.Location = New System.Drawing.Point(1, 12)
        Me.CmdRefresh.Name = "CmdRefresh"
        Me.CmdRefresh.Size = New System.Drawing.Size(75, 23)
        Me.CmdRefresh.TabIndex = 0
        Me.CmdRefresh.Text = "Refresh"
        Me.CmdRefresh.UseVisualStyleBackColor = True
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 41)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1453, 749)
        Me.UltraTabControl1.TabIndex = 4
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Invoices"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1449, 723)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(787, 375)
        '
        'TxtTo
        '
        Me.TxtTo.Location = New System.Drawing.Point(320, 12)
        Me.TxtTo.Name = "TxtTo"
        Me.TxtTo.Size = New System.Drawing.Size(200, 20)
        Me.TxtTo.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(288, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 16)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "To"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtFrom
        '
        Me.TxtFrom.Location = New System.Drawing.Point(82, 12)
        Me.TxtFrom.Name = "TxtFrom"
        Me.TxtFrom.Size = New System.Drawing.Size(200, 20)
        Me.TxtFrom.TabIndex = 1
        '
        'UltraDockManager1
        '
        Me.UltraDockManager1.HostControl = Me
        '
        '_FrmCreditCardInvoicesUnpinnedTabAreaLeft
        '
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.Location = New System.Drawing.Point(0, 0)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.Name = "_FrmCreditCardInvoicesUnpinnedTabAreaLeft"
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.Owner = Me.UltraDockManager1
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.Size = New System.Drawing.Size(0, 790)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft.TabIndex = 63
        '
        '_FrmCreditCardInvoicesUnpinnedTabAreaRight
        '
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.Location = New System.Drawing.Point(1454, 0)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.Name = "_FrmCreditCardInvoicesUnpinnedTabAreaRight"
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.Owner = Me.UltraDockManager1
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 790)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaRight.TabIndex = 64
        '
        '_FrmCreditCardInvoicesUnpinnedTabAreaTop
        '
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.Location = New System.Drawing.Point(0, 0)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.Name = "_FrmCreditCardInvoicesUnpinnedTabAreaTop"
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.Owner = Me.UltraDockManager1
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.Size = New System.Drawing.Size(1454, 0)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaTop.TabIndex = 65
        '
        '_FrmCreditCardInvoicesUnpinnedTabAreaBottom
        '
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.Location = New System.Drawing.Point(0, 790)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.Name = "_FrmCreditCardInvoicesUnpinnedTabAreaBottom"
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.Owner = Me.UltraDockManager1
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.Size = New System.Drawing.Size(1454, 0)
        Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom.TabIndex = 66
        '
        '_FrmCreditCardInvoicesAutoHideControl
        '
        Me._FrmCreditCardInvoicesAutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmCreditCardInvoicesAutoHideControl.Location = New System.Drawing.Point(0, 0)
        Me._FrmCreditCardInvoicesAutoHideControl.Name = "_FrmCreditCardInvoicesAutoHideControl"
        Me._FrmCreditCardInvoicesAutoHideControl.Owner = Me.UltraDockManager1
        Me._FrmCreditCardInvoicesAutoHideControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmCreditCardInvoicesAutoHideControl.Size = New System.Drawing.Size(0, 0)
        Me._FrmCreditCardInvoicesAutoHideControl.TabIndex = 67
        '
        'FrmFreight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1454, 790)
        Me.Controls.Add(Me._FrmCreditCardInvoicesAutoHideControl)
        Me.Controls.Add(Me.TxtTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtFrom)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CmdRefresh)
        Me.Controls.Add(Me._FrmCreditCardInvoicesUnpinnedTabAreaTop)
        Me.Controls.Add(Me._FrmCreditCardInvoicesUnpinnedTabAreaBottom)
        Me.Controls.Add(Me._FrmCreditCardInvoicesUnpinnedTabAreaLeft)
        Me.Controls.Add(Me._FrmCreditCardInvoicesUnpinnedTabAreaRight)
        Me.Name = "FrmFreight"
        Me.Text = "Freight Analysis"
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.UltraGridInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        CType(Me.UltraGridCustomerLoads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl9.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.UltraTabPageControl5.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ultraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl9 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl7 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl8 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents UltraGridCustomerLoads As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents UltraToolTipManager1 As Infragistics.Win.UltraWinToolTip.UltraToolTipManager
    Friend WithEvents llHedging As System.Windows.Forms.LinkLabel
    Friend WithEvents CmdRefresh As System.Windows.Forms.Button
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridInvoices As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents TxtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents _FrmCreditCardInvoicesAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl
    Friend WithEvents UltraDockManager1 As Infragistics.Win.UltraWinDock.UltraDockManager
    Friend WithEvents _FrmCreditCardInvoicesUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmCreditCardInvoicesUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmCreditCardInvoicesUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmCreditCardInvoicesUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea

End Class
