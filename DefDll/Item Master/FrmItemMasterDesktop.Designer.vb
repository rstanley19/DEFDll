<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmItemMasterDesktop
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
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmdGetItem = New System.Windows.Forms.Button()
        Me.TxtItem = New Infragistics.Win.UltraWinEditors.UltraNumericEditor()
        Me.UltraTabPageControl11 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.cbDeleted = New System.Windows.Forms.CheckBox()
        Me.cbActive = New System.Windows.Forms.CheckBox()
        Me.llrefresh = New System.Windows.Forms.LinkLabel()
        Me.LabBuilding = New System.Windows.Forms.Label()
        Me.UltraGridSearch = New Infragistics.Win.UltraWinGrid.UltraGrid()
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
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TxtItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl11.SuspendLayout()
        CType(Me.UltraGridSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.UltraGridCustomerLoads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl9.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.UltraTabPageControl5.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1653, 792)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.CmdGetItem)
        Me.GroupBox1.Controls.Add(Me.TxtItem)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(290, 74)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(42, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Item"
        '
        'CmdGetItem
        '
        Me.CmdGetItem.Location = New System.Drawing.Point(178, 24)
        Me.CmdGetItem.Name = "CmdGetItem"
        Me.CmdGetItem.Size = New System.Drawing.Size(69, 23)
        Me.CmdGetItem.TabIndex = 1
        Me.CmdGetItem.Text = "Go"
        Me.CmdGetItem.UseVisualStyleBackColor = True
        '
        'TxtItem
        '
        Me.TxtItem.Location = New System.Drawing.Point(97, 25)
        Me.TxtItem.MaskInput = "nnnnn"
        Me.TxtItem.MaxValue = 999996
        Me.TxtItem.MinValue = 0
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(66, 21)
        Me.TxtItem.TabIndex = 0
        '
        'UltraTabPageControl11
        '
        Me.UltraTabPageControl11.Controls.Add(Me.cbDeleted)
        Me.UltraTabPageControl11.Controls.Add(Me.cbActive)
        Me.UltraTabPageControl11.Controls.Add(Me.llrefresh)
        Me.UltraTabPageControl11.Controls.Add(Me.LabBuilding)
        Me.UltraTabPageControl11.Controls.Add(Me.UltraGridSearch)
        Me.UltraTabPageControl11.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl11.Name = "UltraTabPageControl11"
        Me.UltraTabPageControl11.Size = New System.Drawing.Size(1653, 792)
        '
        'cbDeleted
        '
        Me.cbDeleted.AutoSize = True
        Me.cbDeleted.Location = New System.Drawing.Point(191, 15)
        Me.cbDeleted.Name = "cbDeleted"
        Me.cbDeleted.Size = New System.Drawing.Size(63, 17)
        Me.cbDeleted.TabIndex = 15
        Me.cbDeleted.Text = "Deleted"
        Me.cbDeleted.UseVisualStyleBackColor = True
        '
        'cbActive
        '
        Me.cbActive.AutoSize = True
        Me.cbActive.Checked = True
        Me.cbActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbActive.Location = New System.Drawing.Point(112, 15)
        Me.cbActive.Name = "cbActive"
        Me.cbActive.Size = New System.Drawing.Size(56, 17)
        Me.cbActive.TabIndex = 14
        Me.cbActive.Text = "Active"
        Me.cbActive.UseVisualStyleBackColor = True
        '
        'llrefresh
        '
        Me.llrefresh.AutoSize = True
        Me.llrefresh.Enabled = False
        Me.llrefresh.LinkColor = System.Drawing.Color.MidnightBlue
        Me.llrefresh.Location = New System.Drawing.Point(2, 16)
        Me.llrefresh.Name = "llrefresh"
        Me.llrefresh.Size = New System.Drawing.Size(86, 13)
        Me.llrefresh.TabIndex = 0
        Me.llrefresh.TabStop = True
        Me.llrefresh.Text = "Refresh Item List"
        '
        'LabBuilding
        '
        Me.LabBuilding.AutoSize = True
        Me.LabBuilding.Location = New System.Drawing.Point(14, 48)
        Me.LabBuilding.Name = "LabBuilding"
        Me.LabBuilding.Size = New System.Drawing.Size(171, 13)
        Me.LabBuilding.TabIndex = 13
        Me.LabBuilding.Text = "Building items.  Please be patient..."
        Me.LabBuilding.Visible = False
        '
        'UltraGridSearch
        '
        Me.UltraGridSearch.AllowDrop = True
        Me.UltraGridSearch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Appearance1.BackColor = System.Drawing.Color.Wheat
        Appearance1.BackColor2 = System.Drawing.Color.Wheat
        Me.UltraGridSearch.DisplayLayout.Appearance = Appearance1
        Me.UltraGridSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridSearch.Location = New System.Drawing.Point(2, 35)
        Me.UltraGridSearch.Name = "UltraGridSearch"
        Me.UltraGridSearch.Size = New System.Drawing.Size(1543, 617)
        Me.UltraGridSearch.TabIndex = 5
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
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl11)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, -1)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1657, 818)
        Me.UltraTabControl1.TabIndex = 0
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Home"
        Appearance4.ForeColor = System.Drawing.Color.Red
        UltraTab3.Appearance = Appearance4
        UltraTab3.Key = "Search"
        UltraTab3.TabPage = Me.UltraTabPageControl11
        UltraTab3.Text = "Search"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab3})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1653, 792)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(787, 375)
        '
        'FrmItemMasterDesktop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1658, 810)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Name = "FrmItemMasterDesktop"
        Me.Text = "Item Master Maintenance"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.TxtItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl11.ResumeLayout(False)
        Me.UltraTabPageControl11.PerformLayout()
        CType(Me.UltraGridSearch, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CmdGetItem As System.Windows.Forms.Button
    Friend WithEvents TxtItem As Infragistics.Win.UltraWinEditors.UltraNumericEditor
    Friend WithEvents UltraTabPageControl11 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridSearch As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents LabBuilding As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents llrefresh As System.Windows.Forms.LinkLabel
    Friend WithEvents cbDeleted As Windows.Forms.CheckBox
    Friend WithEvents cbActive As Windows.Forms.CheckBox
End Class
