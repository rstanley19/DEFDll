Option Explicit On 
'Option Strict On

Imports System.Globalization
Imports System.IO
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinToolbars

Imports Leadtools
Imports Leadtools.Codecs
Imports Leadtools.WinForms
Imports System.Drawing


Public Class FrmOutsideFreightQueue
    Inherits System.Windows.Forms.Form

    ' Vendor detail
    Private VendorTable As New DataTable("Vendor")

    Private Daily_QryData As ADODB.Recordset
    Private Parms As Object

    Private CurrentPage As Int32
    Private saveZoom As decimal = 0.5
    Private CurrentPageImageFormat As String
    Private CurrentPagePixelFormat As String
    Private CurrentFtpFilP As String

    Private DocumentFrom As String

    Private srcFileName As String
    Private BillingCarrier As Integer

    'Billing Sheet
    Dim InvoiceTable As New DataTable("Invoices")
    Dim DataSetBilling As New DataSet()

    ' used for Scan files
    Dim ScanTable As New DataTable("Scan")
    Dim DataSetScan As New DataSet()

    Private MaintenanceOnly As Boolean

    Private wrkUnmatched As Int32
    Private wrkUnverified As Int32
    Private MiscellaneousInvoice As Boolean = False

    Private codecs As RasterCodecs

    Dim WithEvents myFrmOutsideFreightDocumentType As FrmOutsideFreightDocumentType
    Dim WithEvents myFrmInvOutsideFreight As FrmInvOutsideFreight

#Region " Windows Form Designer generated code "
    '    Dim myFrmInvoice As New FrmInvoice()
#Region " Private Declarations"


#End Region

    Public Sub New()


        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        RasterCodecs.Startup()
        codecs = New RasterCodecs()
        LoadVendor()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents FrmMenu_Fill_Panel As System.Windows.Forms.Panel
    Friend WithEvents FrmMenu_Fill_Panel_Fill_Panel As System.Windows.Forms.Panel
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents panel1_Fill_Panel As System.Windows.Forms.Panel
    Friend WithEvents LabDate As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LabUser As System.Windows.Forms.Label
    Friend WithEvents GrpDispatch As System.Windows.Forms.GroupBox
    Friend WithEvents CmdRefresh As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As Leadtools.WinForms.RasterImageViewer
    Friend WithEvents PanelImagePages As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxPrinter As System.Windows.Forms.PictureBox
    Friend WithEvents LabPages As System.Windows.Forms.Label
    Friend WithEvents LabOf As System.Windows.Forms.Label
    Friend WithEvents LabPage As System.Windows.Forms.Label
    Friend WithEvents PictureBoxImageRight As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxImageLeft As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxRotate As System.Windows.Forms.PictureBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Friend WithEvents CmdDeleteDocPage As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents PictureBoxMoveToFaxQueue As System.Windows.Forms.PictureBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents UltraGridScan As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraDockManager1 As Infragistics.Win.UltraWinDock.UltraDockManager
    Friend WithEvents DockableWindow4 As Infragistics.Win.UltraWinDock.DockableWindow
    Friend WithEvents WindowDockingArea3 As Infragistics.Win.UltraWinDock.WindowDockingArea
    Friend WithEvents _FrmBillingQueueUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmBillingQueueUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmBillingQueueUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmBillingQueueUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmBillingQueueAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl
    Friend WithEvents UltraGridInvoices As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RasterMagnifyGlass1 As Leadtools.WinForms.RasterMagnifyGlass = New Leadtools.WinForms.RasterMagnifyGlass()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmOutsideFreightQueue))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim DockAreaPane1 As Infragistics.Win.UltraWinDock.DockAreaPane = New Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, New System.Guid("b61308f3-e0e0-47fd-9446-3cd0e0c6c63b"))
        Dim DockableControlPane1 As Infragistics.Win.UltraWinDock.DockableControlPane = New Infragistics.Win.UltraWinDock.DockableControlPane(New System.Guid("48e94b16-b70c-48f6-bf73-f7656346e2f4"), New System.Guid("b61308f3-e0e0-47fd-9446-3cd0e0c6c63b"), -1, New System.Guid("00000000-0000-0000-0000-000000000000"), -1)
        Me.PictureBox1 = New Leadtools.WinForms.RasterImageViewer()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.panel1_Fill_Panel = New System.Windows.Forms.Panel()
        Me.LabUser = New System.Windows.Forms.Label()
        Me.LabDate = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.FrmMenu_Fill_Panel = New System.Windows.Forms.Panel()
        Me.FrmMenu_Fill_Panel_Fill_Panel = New System.Windows.Forms.Panel()
        Me.GrpDispatch = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.UltraGridScan = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.PanelImagePages = New System.Windows.Forms.Panel()
        Me.PictureBoxMoveToFaxQueue = New System.Windows.Forms.PictureBox()
        Me.CmdDeleteDocPage = New System.Windows.Forms.Button()
        Me.PictureBoxPrinter = New System.Windows.Forms.PictureBox()
        Me.LabPages = New System.Windows.Forms.Label()
        Me.LabOf = New System.Windows.Forms.Label()
        Me.LabPage = New System.Windows.Forms.Label()
        Me.PictureBoxImageRight = New System.Windows.Forms.PictureBox()
        Me.PictureBoxImageLeft = New System.Windows.Forms.PictureBox()
        Me.PictureBoxRotate = New System.Windows.Forms.PictureBox()
        Me.CmdRefresh = New System.Windows.Forms.Button()
        Me.UltraGridInvoices = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.UltraDockManager1 = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components)
        Me.DockableWindow4 = New Infragistics.Win.UltraWinDock.DockableWindow()
        Me.WindowDockingArea3 = New Infragistics.Win.UltraWinDock.WindowDockingArea()
        Me._FrmBillingQueueUnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmBillingQueueUnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmBillingQueueUnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmBillingQueueUnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmBillingQueueAutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.panel1.SuspendLayout()
        Me.panel1_Fill_Panel.SuspendLayout()
        Me.FrmMenu_Fill_Panel.SuspendLayout()
        Me.FrmMenu_Fill_Panel_Fill_Panel.SuspendLayout()
        Me.GrpDispatch.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.UltraGridScan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelImagePages.SuspendLayout()
        CType(Me.PictureBoxMoveToFaxQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPrinter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxImageRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxImageLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxRotate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGridInvoices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DockableWindow4.SuspendLayout()
        Me.WindowDockingArea3.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.AnimateFloater = True
        Me.PictureBox1.AnimateRegion = True
        Me.PictureBox1.AutoDisposeImages = True
        Me.PictureBox1.AutoResetScaleFactor = True
        Me.PictureBox1.AutoResetScrollPosition = True
        Me.PictureBox1.AutoScroll = True
        Me.PictureBox1.BindingData = Nothing
        Me.PictureBox1.BindingLoadBitsPerPixel = 24
        Me.PictureBox1.BindingRasterCodecs = Nothing
        Me.PictureBox1.BindingSaveBitsPerPixel = 24
        Me.PictureBox1.BindingSaveImageFormat = Leadtools.RasterImageFormat.Jpeg
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PictureBox1.DoubleBuffer = True
        Me.PictureBox1.EnableScrollingInterface = False
        Me.PictureBox1.EnableTimer = False
        Me.PictureBox1.FloaterImage = Nothing
        Me.PictureBox1.FloaterPosition = New System.Drawing.Point(0, 0)
        Me.PictureBox1.FloaterVisible = True
        Me.PictureBox1.FrameColor = System.Drawing.Color.Black
        Me.PictureBox1.FrameShadowColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PictureBox1.FrameShadowSize = New System.Drawing.SizeF(0!, 0!)
        Me.PictureBox1.FramesIsPartOfImage = True
        Me.PictureBox1.FrameSize = New System.Drawing.SizeF(0!, 0!)
        Me.PictureBox1.HorizontalAlignMode = Leadtools.RasterPaintAlignMode.Near
        Me.PictureBox1.Image = Nothing
        Me.PictureBox1.InteractiveMode = Leadtools.WinForms.RasterViewerInteractiveMode.None
        Me.PictureBox1.InteractiveRegionCombineMode = Leadtools.RasterRegionCombineMode.[Set]
        Me.PictureBox1.InteractiveRegionType = Leadtools.WinForms.RasterViewerInteractiveRegionType.Rectangle
        Me.PictureBox1.Location = New System.Drawing.Point(0, 16)
        RasterMagnifyGlass1.Border3DStyle = System.Windows.Forms.Border3DStyle.Raised
        RasterMagnifyGlass1.BorderColor = System.Drawing.Color.Black
        RasterMagnifyGlass1.BorderWidth = 1
        RasterMagnifyGlass1.Crosshair = Leadtools.WinForms.RasterMagnifyGlassCrosshair.Fine
        RasterMagnifyGlass1.CrosshairColor = System.Drawing.Color.Black
        RasterMagnifyGlass1.CrosshairWidth = 1
        RasterMagnifyGlass1.RoundRectangleEllipseSize = New System.Drawing.Size(20, 20)
        RasterMagnifyGlass1.ScaleFactor = 2.0!
        RasterMagnifyGlass1.Shape = Leadtools.WinForms.RasterMagnifyGlassShape.Rectangle
        RasterMagnifyGlass1.Size = New System.Drawing.Size(150, 150)
        Me.PictureBox1.MagnifyGlass = RasterMagnifyGlass1
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.ScaleFactor = 1.0R
        Me.PictureBox1.ScrollPosition = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Size = New System.Drawing.Size(334, 268)
        Me.PictureBox1.SizeMode = Leadtools.RasterPaintSizeMode.Normal
        Me.PictureBox1.SmallScrollChangeRatio = 20
        Me.PictureBox1.SourceRectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        Me.PictureBox1.TabIndex = 36
        Me.PictureBox1.Text = "PictureBox1"
        Me.PictureBox1.UseDpi = False
        Me.PictureBox1.VerticalAlignMode = Leadtools.RasterPaintAlignMode.Near
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panel1.Controls.Add(Me.panel1_Fill_Panel)
        Me.panel1.Location = New System.Drawing.Point(0, 10)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(848, 56)
        Me.panel1.TabIndex = 3
        '
        'panel1_Fill_Panel
        '
        Me.panel1_Fill_Panel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.panel1_Fill_Panel.Controls.Add(Me.LabUser)
        Me.panel1_Fill_Panel.Controls.Add(Me.LabDate)
        Me.panel1_Fill_Panel.Controls.Add(Me.label1)
        Me.panel1_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default
        Me.panel1_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Left
        Me.panel1_Fill_Panel.Location = New System.Drawing.Point(0, 0)
        Me.panel1_Fill_Panel.Name = "panel1_Fill_Panel"
        Me.panel1_Fill_Panel.Size = New System.Drawing.Size(792, 56)
        Me.panel1_Fill_Panel.TabIndex = 0
        '
        'LabUser
        '
        Me.LabUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.LabUser.Location = New System.Drawing.Point(280, 24)
        Me.LabUser.Name = "LabUser"
        Me.LabUser.Size = New System.Drawing.Size(232, 16)
        Me.LabUser.TabIndex = 4
        Me.LabUser.Text = "User"
        Me.LabUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabDate
        '
        Me.LabDate.BackColor = System.Drawing.Color.Transparent
        Me.LabDate.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.LabDate.Location = New System.Drawing.Point(280, 8)
        Me.LabDate.Name = "LabDate"
        Me.LabDate.Size = New System.Drawing.Size(232, 16)
        Me.LabDate.TabIndex = 3
        Me.LabDate.Text = "Date / Time"
        Me.LabDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Font = New System.Drawing.Font("Times New Roman", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.label1.Location = New System.Drawing.Point(8, 0)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(256, 48)
        Me.label1.TabIndex = 0
        Me.label1.Text = "FuelTrak"
        '
        'FrmMenu_Fill_Panel
        '
        Me.FrmMenu_Fill_Panel.Controls.Add(Me.FrmMenu_Fill_Panel_Fill_Panel)
        Me.FrmMenu_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default
        Me.FrmMenu_Fill_Panel.Location = New System.Drawing.Point(0, 0)
        Me.FrmMenu_Fill_Panel.Name = "FrmMenu_Fill_Panel"
        Me.FrmMenu_Fill_Panel.Size = New System.Drawing.Size(800, 557)
        Me.FrmMenu_Fill_Panel.TabIndex = 0
        '
        'FrmMenu_Fill_Panel_Fill_Panel
        '
        Me.FrmMenu_Fill_Panel_Fill_Panel.Controls.Add(Me.GrpDispatch)
        Me.FrmMenu_Fill_Panel_Fill_Panel.Controls.Add(Me.panel1)
        Me.FrmMenu_Fill_Panel_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default
        Me.FrmMenu_Fill_Panel_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FrmMenu_Fill_Panel_Fill_Panel.Location = New System.Drawing.Point(0, 0)
        Me.FrmMenu_Fill_Panel_Fill_Panel.Name = "FrmMenu_Fill_Panel_Fill_Panel"
        Me.FrmMenu_Fill_Panel_Fill_Panel.Size = New System.Drawing.Size(800, 557)
        Me.FrmMenu_Fill_Panel_Fill_Panel.TabIndex = 0
        '
        'GrpDispatch
        '
        Me.GrpDispatch.BackColor = System.Drawing.Color.SteelBlue
        Me.GrpDispatch.Controls.Add(Me.TabControl1)
        Me.GrpDispatch.Controls.Add(Me.PanelImagePages)
        Me.GrpDispatch.Controls.Add(Me.CmdRefresh)
        Me.GrpDispatch.Controls.Add(Me.UltraGridInvoices)
        Me.GrpDispatch.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpDispatch.Location = New System.Drawing.Point(8, 72)
        Me.GrpDispatch.Name = "GrpDispatch"
        Me.GrpDispatch.Size = New System.Drawing.Size(784, 480)
        Me.GrpDispatch.TabIndex = 0
        Me.GrpDispatch.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Right
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(6, 344)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(618, 136)
        Me.TabControl1.TabIndex = 41
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.UltraGridScan)
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(591, 128)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Scan"
        '
        'UltraGridScan
        '
        Me.UltraGridScan.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridScan.Location = New System.Drawing.Point(-8, -4)
        Me.UltraGridScan.Name = "UltraGridScan"
        Me.UltraGridScan.Size = New System.Drawing.Size(352, 132)
        Me.UltraGridScan.TabIndex = 0
        '
        'PanelImagePages
        '
        Me.PanelImagePages.Controls.Add(Me.PictureBoxMoveToFaxQueue)
        Me.PanelImagePages.Controls.Add(Me.CmdDeleteDocPage)
        Me.PanelImagePages.Controls.Add(Me.PictureBoxPrinter)
        Me.PanelImagePages.Controls.Add(Me.LabPages)
        Me.PanelImagePages.Controls.Add(Me.LabOf)
        Me.PanelImagePages.Controls.Add(Me.LabPage)
        Me.PanelImagePages.Controls.Add(Me.PictureBoxImageRight)
        Me.PanelImagePages.Controls.Add(Me.PictureBoxImageLeft)
        Me.PanelImagePages.Controls.Add(Me.PictureBoxRotate)
        Me.PanelImagePages.Location = New System.Drawing.Point(720, 376)
        Me.PanelImagePages.Name = "PanelImagePages"
        Me.PanelImagePages.Size = New System.Drawing.Size(56, 104)
        Me.PanelImagePages.TabIndex = 4
        Me.PanelImagePages.Visible = False
        '
        'PictureBoxMoveToFaxQueue
        '
        Me.PictureBoxMoveToFaxQueue.Image = CType(resources.GetObject("PictureBoxMoveToFaxQueue.Image"), System.Drawing.Image)
        Me.PictureBoxMoveToFaxQueue.Location = New System.Drawing.Point(29, 80)
        Me.PictureBoxMoveToFaxQueue.Name = "PictureBoxMoveToFaxQueue"
        Me.PictureBoxMoveToFaxQueue.Size = New System.Drawing.Size(20, 20)
        Me.PictureBoxMoveToFaxQueue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxMoveToFaxQueue.TabIndex = 39
        Me.PictureBoxMoveToFaxQueue.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBoxMoveToFaxQueue, "Move to a different fax queue")
        '
        'CmdDeleteDocPage
        '
        Me.CmdDeleteDocPage.AllowDrop = True
        Me.CmdDeleteDocPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdDeleteDocPage.Image = CType(resources.GetObject("CmdDeleteDocPage.Image"), System.Drawing.Image)
        Me.CmdDeleteDocPage.Location = New System.Drawing.Point(0, 80)
        Me.CmdDeleteDocPage.Name = "CmdDeleteDocPage"
        Me.CmdDeleteDocPage.Size = New System.Drawing.Size(24, 20)
        Me.CmdDeleteDocPage.TabIndex = 3
        '
        'PictureBoxPrinter
        '
        Me.PictureBoxPrinter.Image = CType(resources.GetObject("PictureBoxPrinter.Image"), System.Drawing.Image)
        Me.PictureBoxPrinter.Location = New System.Drawing.Point(27, 48)
        Me.PictureBoxPrinter.Name = "PictureBoxPrinter"
        Me.PictureBoxPrinter.Size = New System.Drawing.Size(20, 24)
        Me.PictureBoxPrinter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxPrinter.TabIndex = 20
        Me.PictureBoxPrinter.TabStop = False
        '
        'LabPages
        '
        Me.LabPages.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPages.ForeColor = System.Drawing.Color.White
        Me.LabPages.Location = New System.Drawing.Point(36, 30)
        Me.LabPages.Name = "LabPages"
        Me.LabPages.Size = New System.Drawing.Size(16, 16)
        Me.LabPages.TabIndex = 2
        Me.LabPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabOf
        '
        Me.LabOf.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabOf.ForeColor = System.Drawing.Color.White
        Me.LabOf.Location = New System.Drawing.Point(17, 30)
        Me.LabOf.Name = "LabOf"
        Me.LabOf.Size = New System.Drawing.Size(16, 16)
        Me.LabOf.TabIndex = 1
        Me.LabOf.Text = "of"
        Me.LabOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabPage
        '
        Me.LabPage.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPage.ForeColor = System.Drawing.Color.White
        Me.LabPage.Location = New System.Drawing.Point(1, 30)
        Me.LabPage.Name = "LabPage"
        Me.LabPage.Size = New System.Drawing.Size(16, 16)
        Me.LabPage.TabIndex = 0
        Me.LabPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBoxImageRight
        '
        Me.PictureBoxImageRight.Image = CType(resources.GetObject("PictureBoxImageRight.Image"), System.Drawing.Image)
        Me.PictureBoxImageRight.Location = New System.Drawing.Point(27, 0)
        Me.PictureBoxImageRight.Name = "PictureBoxImageRight"
        Me.PictureBoxImageRight.Size = New System.Drawing.Size(20, 24)
        Me.PictureBoxImageRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxImageRight.TabIndex = 15
        Me.PictureBoxImageRight.TabStop = False
        '
        'PictureBoxImageLeft
        '
        Me.PictureBoxImageLeft.Image = CType(resources.GetObject("PictureBoxImageLeft.Image"), System.Drawing.Image)
        Me.PictureBoxImageLeft.Location = New System.Drawing.Point(0, 0)
        Me.PictureBoxImageLeft.Name = "PictureBoxImageLeft"
        Me.PictureBoxImageLeft.Size = New System.Drawing.Size(20, 24)
        Me.PictureBoxImageLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxImageLeft.TabIndex = 16
        Me.PictureBoxImageLeft.TabStop = False
        '
        'PictureBoxRotate
        '
        Me.PictureBoxRotate.Image = CType(resources.GetObject("PictureBoxRotate.Image"), System.Drawing.Image)
        Me.PictureBoxRotate.Location = New System.Drawing.Point(0, 48)
        Me.PictureBoxRotate.Name = "PictureBoxRotate"
        Me.PictureBoxRotate.Size = New System.Drawing.Size(20, 24)
        Me.PictureBoxRotate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxRotate.TabIndex = 37
        Me.PictureBoxRotate.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBoxRotate, "Rotate 90 degrees")
        '
        'CmdRefresh
        '
        Me.CmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.CmdRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdRefresh.Location = New System.Drawing.Point(8, 12)
        Me.CmdRefresh.Name = "CmdRefresh"
        Me.CmdRefresh.Size = New System.Drawing.Size(56, 20)
        Me.CmdRefresh.TabIndex = 0
        Me.CmdRefresh.Text = "Refresh"
        Me.CmdRefresh.UseVisualStyleBackColor = False
        '
        'UltraGridInvoices
        '
        Me.UltraGridInvoices.AllowDrop = True
        Appearance1.BackColor = System.Drawing.Color.Transparent
        Appearance1.BackColor2 = System.Drawing.Color.Transparent
        Me.UltraGridInvoices.DisplayLayout.Appearance = Appearance1
        Me.UltraGridInvoices.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridInvoices.Location = New System.Drawing.Point(6, 40)
        Me.UltraGridInvoices.Name = "UltraGridInvoices"
        Me.UltraGridInvoices.Size = New System.Drawing.Size(770, 304)
        Me.UltraGridInvoices.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.Label5.Location = New System.Drawing.Point(401, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 16)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Delivery Date"
        '
        'ToolTip1
        '
        Me.ToolTip1.Active = False
        '
        'PrintDocument1
        '
        '
        'UltraDockManager1
        '
        DockAreaPane1.ChildPaneStyle = Infragistics.Win.UltraWinDock.ChildPaneStyle.TabGroup
        DockAreaPane1.FloatingLocation = New System.Drawing.Point(327, 88)
        DockableControlPane1.Closed = True
        DockableControlPane1.Control = Me.PictureBox1
        DockableControlPane1.OriginalControlBounds = New System.Drawing.Rectangle(152, 392, 552, 80)
        DockableControlPane1.Size = New System.Drawing.Size(334, 284)
        DockableControlPane1.Text = "Document"
        DockAreaPane1.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane1})
        DockAreaPane1.Settings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.Settings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.Settings.AllowDockRight = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.Size = New System.Drawing.Size(334, 284)
        Me.UltraDockManager1.DockAreas.AddRange(New Infragistics.Win.UltraWinDock.DockAreaPane() {DockAreaPane1})
        Me.UltraDockManager1.HostControl = Me
        Me.UltraDockManager1.Visible = False
        '
        'DockableWindow4
        '
        Me.DockableWindow4.Controls.Add(Me.PictureBox1)
        Me.DockableWindow4.Location = New System.Drawing.Point(-10000, 0)
        Me.DockableWindow4.Name = "DockableWindow4"
        Me.DockableWindow4.Owner = Me.UltraDockManager1
        Me.DockableWindow4.Size = New System.Drawing.Size(334, 284)
        Me.DockableWindow4.TabIndex = 6
        '
        'WindowDockingArea3
        '
        Me.WindowDockingArea3.Controls.Add(Me.DockableWindow4)
        Me.WindowDockingArea3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WindowDockingArea3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WindowDockingArea3.Location = New System.Drawing.Point(4, 4)
        Me.WindowDockingArea3.Name = "WindowDockingArea3"
        Me.WindowDockingArea3.Owner = Me.UltraDockManager1
        Me.WindowDockingArea3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.WindowDockingArea3.Size = New System.Drawing.Size(334, 284)
        Me.WindowDockingArea3.TabIndex = 0
        '
        '_FrmBillingQueueUnpinnedTabAreaLeft
        '
        Me._FrmBillingQueueUnpinnedTabAreaLeft.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me._FrmBillingQueueUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me._FrmBillingQueueUnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmBillingQueueUnpinnedTabAreaLeft.Location = New System.Drawing.Point(0, 0)
        Me._FrmBillingQueueUnpinnedTabAreaLeft.Name = "_FrmBillingQueueUnpinnedTabAreaLeft"
        Me._FrmBillingQueueUnpinnedTabAreaLeft.Owner = Me.UltraDockManager1
        Me._FrmBillingQueueUnpinnedTabAreaLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmBillingQueueUnpinnedTabAreaLeft.Size = New System.Drawing.Size(0, 557)
        Me._FrmBillingQueueUnpinnedTabAreaLeft.TabIndex = 1
        '
        '_FrmBillingQueueUnpinnedTabAreaTop
        '
        Me._FrmBillingQueueUnpinnedTabAreaTop.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me._FrmBillingQueueUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top
        Me._FrmBillingQueueUnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmBillingQueueUnpinnedTabAreaTop.Location = New System.Drawing.Point(0, 0)
        Me._FrmBillingQueueUnpinnedTabAreaTop.Name = "_FrmBillingQueueUnpinnedTabAreaTop"
        Me._FrmBillingQueueUnpinnedTabAreaTop.Owner = Me.UltraDockManager1
        Me._FrmBillingQueueUnpinnedTabAreaTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmBillingQueueUnpinnedTabAreaTop.Size = New System.Drawing.Size(800, 0)
        Me._FrmBillingQueueUnpinnedTabAreaTop.TabIndex = 3
        '
        '_FrmBillingQueueUnpinnedTabAreaBottom
        '
        Me._FrmBillingQueueUnpinnedTabAreaBottom.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me._FrmBillingQueueUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me._FrmBillingQueueUnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmBillingQueueUnpinnedTabAreaBottom.Location = New System.Drawing.Point(0, 557)
        Me._FrmBillingQueueUnpinnedTabAreaBottom.Name = "_FrmBillingQueueUnpinnedTabAreaBottom"
        Me._FrmBillingQueueUnpinnedTabAreaBottom.Owner = Me.UltraDockManager1
        Me._FrmBillingQueueUnpinnedTabAreaBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmBillingQueueUnpinnedTabAreaBottom.Size = New System.Drawing.Size(800, 0)
        Me._FrmBillingQueueUnpinnedTabAreaBottom.TabIndex = 4
        '
        '_FrmBillingQueueUnpinnedTabAreaRight
        '
        Me._FrmBillingQueueUnpinnedTabAreaRight.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me._FrmBillingQueueUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right
        Me._FrmBillingQueueUnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmBillingQueueUnpinnedTabAreaRight.Location = New System.Drawing.Point(800, 0)
        Me._FrmBillingQueueUnpinnedTabAreaRight.Name = "_FrmBillingQueueUnpinnedTabAreaRight"
        Me._FrmBillingQueueUnpinnedTabAreaRight.Owner = Me.UltraDockManager1
        Me._FrmBillingQueueUnpinnedTabAreaRight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmBillingQueueUnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 557)
        Me._FrmBillingQueueUnpinnedTabAreaRight.TabIndex = 2
        '
        '_FrmBillingQueueAutoHideControl
        '
        Me._FrmBillingQueueAutoHideControl.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me._FrmBillingQueueAutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmBillingQueueAutoHideControl.Location = New System.Drawing.Point(0, 0)
        Me._FrmBillingQueueAutoHideControl.Name = "_FrmBillingQueueAutoHideControl"
        Me._FrmBillingQueueAutoHideControl.Owner = Me.UltraDockManager1
        Me._FrmBillingQueueAutoHideControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmBillingQueueAutoHideControl.Size = New System.Drawing.Size(0, 0)
        Me._FrmBillingQueueAutoHideControl.TabIndex = 5
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "")
        Me.ImageList1.Images.SetKeyName(1, "")
        Me.ImageList1.Images.SetKeyName(2, "")
        Me.ImageList1.Images.SetKeyName(3, "")
        Me.ImageList1.Images.SetKeyName(4, "")
        Me.ImageList1.Images.SetKeyName(5, "")
        Me.ImageList1.Images.SetKeyName(6, "")
        Me.ImageList1.Images.SetKeyName(7, "")
        Me.ImageList1.Images.SetKeyName(8, "")
        Me.ImageList1.Images.SetKeyName(9, "")
        Me.ImageList1.Images.SetKeyName(10, "")
        '
        'FrmOutsideFreightQueue
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(800, 557)
        Me.Controls.Add(Me._FrmBillingQueueAutoHideControl)
        Me.Controls.Add(Me.FrmMenu_Fill_Panel)
        Me.Controls.Add(Me._FrmBillingQueueUnpinnedTabAreaTop)
        Me.Controls.Add(Me._FrmBillingQueueUnpinnedTabAreaBottom)
        Me.Controls.Add(Me._FrmBillingQueueUnpinnedTabAreaLeft)
        Me.Controls.Add(Me._FrmBillingQueueUnpinnedTabAreaRight)
        Me.Name = "FrmOutsideFreightQueue"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Outside Freight Queue"
        Me.panel1.ResumeLayout(False)
        Me.panel1_Fill_Panel.ResumeLayout(False)
        Me.FrmMenu_Fill_Panel.ResumeLayout(False)
        Me.FrmMenu_Fill_Panel_Fill_Panel.ResumeLayout(False)
        Me.GrpDispatch.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.UltraGridScan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelImagePages.ResumeLayout(False)
        CType(Me.PictureBoxMoveToFaxQueue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPrinter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxImageRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxImageLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxRotate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGridInvoices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DockableWindow4.ResumeLayout(False)
        Me.WindowDockingArea3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub ShowIt(ByVal PassMaintenanceOnly As Boolean)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        MaintenanceOnly = PassMaintenanceOnly
        'BuildFtp()

        With UltraDockManager1
            .ControlPanes(0).Closed = True
            .DockAreas(0).Closed = True
            PanelImagePages.Visible = False
        End With

        LabDate.Text = Now().ToString("dddd, MMMM dd, yyyy")
        '      DateTimePickerOrdered.Text = Now()
        Me.Cursor = System.Windows.Forms.Cursors.Default

        Me.Show()
    End Sub

    Private Sub FrmDirectShipQueue_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RasterCodecs.Shutdown()
    End Sub

    Private Sub FrmMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'add dockable window instead of flyout
        With UltraDockManager1
            .PaneFromControl(PictureBox1).Close(True)
            .Visible = False
            .DockAreas(0).Size = New System.Drawing.Size(720, 294)
            .DockAreas(0).FloatingLocation = New System.Drawing.Point(2, 291)
        End With
        LabUser.Text = "Hello " & UserName
        RefreshMe()
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#Region "Build Direct Ship Queue"

    Private Sub myFrmOutsideFreightDocumentType_ShowInvFreight(PassImgLo As Integer, PassDocNbr As Integer, PassDocType As Integer, PassDeleteThisPage As Boolean) Handles myFrmOutsideFreightDocumentType.ShowInvFreight
        If Not PassDocNbr = 0 Then
            myFrmInvOutsideFreight = New FrmInvOutsideFreight()
            myFrmInvOutsideFreight.ShowMe(Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Invoice").Value), PassImgLo, PassDocNbr, PassDocType, 0, UltraGridInvoices.ActiveRow, VendorTable)
        End If

        If PassDeleteThisPage Then
            DeleteThisPage()
        End If
    End Sub

    Private Sub myFrmInvOutsideFreight_PayInvoice(BackRow As Infragistics.Win.UltraWinGrid.UltraGridRow) Handles myFrmInvOutsideFreight.PayInvoice
        BackRow.Delete()
    End Sub

    Private Sub CmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        RefreshMe()
    End Sub

    Private Sub RefreshMe()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim Rcds As Object
        Dim Parms As Object


        Daily_QryData = connAS400.Execute("SELECT soinv#, soofrt, SODDAT, CMNAME, ifnull(CMCITY, 'Unknown') as cmcity, ifnull(CMST, '??') as cmst, soacc#, sostat, ifnull(docs, 0) as docs " &
        "FROM daily.soinvp  
        left join daily.mscustp on soacc# = cmacc# 
        left join (select count(*) as docs, bxinv# from daily.soblfxp where bxtype in (26) " &
        "group by bxinv#) as d on bxinv# = soinv# " &
        "WHERE not sodel = 'D' and sostat = 'I' and soinv = 'Y' and not soofrt = 0 and soofrp = 0 
        GROUP BY soinv#, soofrt, soddat, cmname, cmcity, cmst, soacc#, sostat, docs", Parms, -1)
        LoadDataSetInvoices(Daily_QryData, False)

        UltraGridInvoices.DataSource = DataSetBilling
        Me.Cursor = System.Windows.Forms.Cursors.Default

        LoadDataSetScan()
        UltraGridScan.DataSource = DataSetScan
        UltraGridScan.Text = "(" & UltraGridScan.Rows.Count() & ") Unassigned Scans"
    End Sub

    'Private Sub UpdateDataSet(ByVal invoice As Integer, ByVal Carrier1 As Integer, ByVal Carrier2 As Integer)
    '    Dim StrExpr As String = "Invoice = '" & invoice & "'"
    '    Dim foundRows As DataRow() = DataSetBilling.Tables("Invoices").Select(StrExpr)

    '    If Not (foundRows Is Nothing) Then
    '        Dim r As DataRow
    '        For Each r In foundRows
    '            r("Car 1") = GetMsCarrlName(Carrier1)
    '            r("carrier #") = Carrier1
    '            'r("Car 2") = GetMsCarrlName(Carrier2)
    '        Next r
    '    End If

    'End Sub

    Function EditAllSourced(ByVal prow As Infragistics.Win.UltraWinGrid.UltraGridRow) As Boolean

        Dim cRow As Infragistics.Win.UltraWinGrid.UltraGridRow = prow.GetChild(ChildRow.First)
        Do Until cRow Is Nothing

            Dim aRow As Infragistics.Win.UltraWinGrid.UltraGridRow = cRow.GetChild(ChildRow.First)
            Do Until aRow Is Nothing

                If Len(Trim(Convert.ToString(aRow.Cells("source").Value))) = 0 Then
                    Return False
                End If

                aRow = aRow.GetSibling(SiblingRow.Next)
            Loop

            cRow = cRow.GetSibling(SiblingRow.Next)
        Loop

        Return True
    End Function

#End Region

#Region "Build Invoices"
    Public Sub LoadDataSetInvoices(ByRef rs As ADODB.Recordset, ByVal UpdateOneInvoice As Boolean)

        BuildEmptyDataSetInvoices()

        'Load the data
        While Not rs.EOF

            Dim InvoiceRow As DataRow = InvoiceTable.NewRow
            InvoiceRow("Invoice") = rs.Fields("soinv#").Value
            InvoiceRow("Freight") = rs.Fields("soofrt").Value

            InvoiceRow("Docs") = rs.Fields("Docs").Value
            InvoiceRow("Doc") = "Assign"
            'InvoiceRow("Reference") = " "
            InvoiceRow("date") = GetDateFromNbr(rs.Fields("soddat").Value)

            InvoiceRow("Account") = rs.Fields("soacc#").Value
            If Not IsDBNull(rs.Fields("cmname").Value) Then
                InvoiceRow("Customer") = rs.Fields("cmname").Value
            Else
                InvoiceRow("Customer") = "Invalid"
            End If

            InvoiceRow("City") = rs.Fields("cmcity").Value
            InvoiceRow("ST") = rs.Fields("cmst").Value

            If rs.Fields("SoStat").Value = "I" Then
                InvoiceRow("Billed") = "Yes"
            Else
                InvoiceRow("Billed") = "No"
            End If

            InvoiceTable.Rows.Add(InvoiceRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSetInvoices()
        InvoiceTable.Dispose()

        InvoiceTable = Nothing
        DataSetBilling = Nothing

        InvoiceTable = New DataTable("Invoices")
        DataSetBilling = New DataSet()

        GC.Collect()

        ' build empty invoice table

        InvoiceTable.Columns.Add("Invoice", GetType(Integer))
        InvoiceTable.Columns.Add("Freight", GetType(decimal))
        InvoiceTable.Columns.Add("Billed", GetType(String))
        InvoiceTable.Columns.Add("Date", GetType(Date))
        InvoiceTable.Columns.Add("Customer", GetType(String))
        InvoiceTable.Columns.Add("City", GetType(String))
        InvoiceTable.Columns.Add("ST", GetType(String))
        InvoiceTable.Columns.Add("Account", GetType(Integer))
        InvoiceTable.Columns.Add("Doc", GetType(String))
        InvoiceTable.Columns.Add("Docs", GetType(Integer))

        ' build dataset
        DataSetBilling.Tables.Add(InvoiceTable)

    End Sub

#End Region

#Region "Show Page"

    Private Sub LoadPage(ByVal inPage As Int32)

        LabPage.Text = inPage

        Dim PaintProp As RasterPaintProperties

        PaintProp = PictureBox1.PaintProperties
        PaintProp.PaintDisplayMode = RasterPaintDisplayModeFlags.ScaleToGray Or RasterPaintDisplayModeFlags.Bicubic
        PictureBox1.PaintProperties = PaintProp

        Dim RasImage As RasterImage

        RasImage = codecs.Load(srcFileName, 0, 0, inPage, inPage)

        Dim GrayScale As New ImageProcessing.GrayscaleCommand
        GrayScale.BitsPerPixel = 8
        GrayScale.Run(RasImage)

        PictureBox1.Image = RasImage


        PictureBox1.AutoScroll = True
        With UltraDockManager1
            .PaneFromControl(PictureBox1).Show()
            .Visible = True
        End With

        PanelImagePages.Visible = True

        If Convert.ToInt32(LabPage.Text) > 1 Then
            PictureBoxImageLeft.Visible = True
        Else
            PictureBoxImageLeft.Visible = False
        End If

        If Convert.ToInt32(LabPage.Text) < Convert.ToInt32(LabPages.Text) Then
            PictureBoxImageRight.Visible = True
        Else
            PictureBoxImageRight.Visible = False
        End If
        PictureBox1.ScaleFactor = saveZoom
    End Sub

    Private Sub PictureBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.DoubleClick
        PictureBox1.ScaleFactor = 0.5
        saveZoom = PictureBox1.ScaleFactor
    End Sub

    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown
        If e.Button = MouseButtons.Left Then
            PictureBox1.ScaleFactor += 0.1
        Else
            PictureBox1.ScaleFactor -= 0.1
        End If
        saveZoom = PictureBox1.ScaleFactor
    End Sub

    Private Sub PictureBoxImageLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxImageLeft.Click
        LoadPage(LabPage.Text - 1)
    End Sub

    Private Sub PictureBoxImageRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxImageRight.Click
        LoadPage(LabPage.Text + 1)
    End Sub

    Private Sub PictureBoxRotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxRotate.Click
        Try
            Dim command As Leadtools.ImageProcessing.RotateCommand = New Leadtools.ImageProcessing.RotateCommand()
            command.Angle = -9000
            command.FillColor = New RasterColor(Color.White)
            command.Flags = Leadtools.ImageProcessing.RotateCommandFlags.Resize
            command.Run(PictureBox1.Image)
            'codecs.Save(PictureBox1.Image, srcFileName, CurrentPageImageFormat, CurrentPagePixelFormat, 1, 1, 0, CodecsSavePageMode.Replace)
        Catch
        End Try
    End Sub

    Private Sub PictureBoxPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxPrinter.Click

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try

            CurrentPage = Convert.ToInt32(LabPage.Text)
            PrintDocument1.Print()

        Catch
            MsgBox("Error opening document", MsgBoxStyle.OkOnly, "")
        End Try

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim instance As New RasterImagePrinter
            instance.Print(PictureBox1.Image, 1, e)
        Catch ex As Exception
            MsgBox("error printing", MsgBoxStyle.OkOnly, "")
        End Try
    End Sub

#End Region

#Region "Build Scan Queue"

    Public Sub LoadDataSetScan()

        BuildEmptyDataSetScan()

        Dim myDir As String = "\\ppc-data\share1\images\box\OutsideFreight\"

        Dim myFile As String = Dir(myDir & "*.*")   ' Retrieve the first entry.

        Do While myFile <> ""   ' Start the loop.
            Dim DelRow As DataRow = ScanTable.NewRow
            DelRow("Document") = myFile

            Try
                Dim codecs As RasterCodecs = New RasterCodecs()
                Dim info As CodecsImageInfo = codecs.GetInformation(myDir & myFile, True)
                DelRow("Pages") = info.TotalPages
            Catch
            End Try
            DelRow("Delete") = "Delete"
            ScanTable.Rows.Add(DelRow)

            myFile = Dir()   ' Get next entry.
        Loop

    End Sub

    Public Sub BuildEmptyDataSetScan()
        DataSetScan.Dispose()
        ScanTable.Dispose()
        ScanTable = Nothing
        DataSetScan = Nothing

        ScanTable = New DataTable("Scan")
        DataSetScan = New DataSet()

        GC.Collect()

        ' build empty Scan table
        Dim colwork As New DataColumn("Document", GetType(String))
        ScanTable.Columns.Add(colwork)

        colwork = New DataColumn("Pages", GetType(String))
        ScanTable.Columns.Add(colwork)
        ScanTable.Columns.Add("Delete", GetType(String))

        ' build dataset
        DataSetScan.Tables.Add(ScanTable)

    End Sub

    Private Sub UltraGridScan_ClickCellButton(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridScan.ClickCellButton
        Dim Daily_GenericUpd As New ADODB.Command

        Select Case e.Cell.Column.Key
            'Select Case Convert.ToInt64(e.Cell.Column.Index)
            Case Is = "Delete"
                If MsgBox("Are you sure you want to permanently remove this document?  This can not be undone.", MsgBoxStyle.YesNo, "Confirm") = MsgBoxResult.No Then Exit Sub
                File.Delete("\\ppc-data\share1\images\box\OutsideFreight\" & e.Cell.Row.Cells("Document").Value)
                e.Cell.Row.Delete()

        End Select

    End Sub

    Private Sub UltraGridScan_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridScan.InitializeLayout
        With UltraGridScan
            .DisplayLayout.Bands(0).Columns("Delete").Width = 60
            .DisplayLayout.Bands(0).Columns("Delete").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button

            Dim i As Int32
            For i = 0 To .DisplayLayout.Bands(0).Columns.Count - 1
                .DisplayLayout.Bands(0).Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i
            .DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortMulti
        End With
    End Sub

    Private Sub UltraGridScan_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles UltraGridScan.MouseUp
        ' declare objects to get value from cell and display 
        Dim mouseupUIElement As Infragistics.Win.UIElement
        Dim mouseupCell As Infragistics.Win.UltraWinGrid.UltraGridCell

        ' retrieve the UIElement from the location of the MouseUp 
        mouseupUIElement = UltraGridScan.DisplayLayout.UIElement.ElementFromPoint(New Point(e.X, e.Y))
        If mouseupUIElement Is Nothing Then Exit Sub
        ' retrieve the Cell from the UIElement 
        mouseupCell = mouseupUIElement.GetContext(GetType(Infragistics.Win.UltraWinGrid.UltraGridCell))

        ' if there is a cell object reference, set to active cell and edit 
        If mouseupCell Is Nothing Then Exit Sub
        mouseupCell.Row.Selected = True
        DisplayScanUltraGrid(UltraGridScan)

    End Sub


    Private Sub DisplayScanUltraGrid(ByVal InGrid As UltraGrid)
        ' Get the cursor position ÷
        Dim point As Point
        point = System.Windows.Forms.Cursor.Position

        ' Convert to the grid's client coordinates ÷
        point = InGrid.PointToClient(point)

        ' declare objects to get value from cell and display
        Dim mouseupUIElement As Infragistics.Win.UIElement
        Dim mouseupCell As Infragistics.Win.UltraWinGrid.UltraGridCell

        ' retrieve the UIElement from the location of the MouseUp
        mouseupUIElement = InGrid.DisplayLayout.UIElement.ElementFromPoint(point)
        If mouseupUIElement Is Nothing Then Exit Sub

        ' retrieve the Cell from the UIElement
        mouseupCell = mouseupUIElement.GetContext(GetType(Infragistics.Win.UltraWinGrid.UltraGridCell))

        If mouseupCell Is Nothing Then Exit Sub

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        GetImageScan(mouseupCell.Row.Cells("Document").Value)

        ' position grid of unbilled invoices to this account #
        Dim intRow As Int32 = 0
        Dim gridRow As Infragistics.Win.UltraWinGrid.UltraGridRow


        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub GetImageScan(ByVal inFile As String)
        Dim myFile As String = inFile
        If Len(myFile) > 60 Then myFile = Mid(myFile, 1, 60)

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        DocumentFrom = "Scan"
        Try
            Dim Parms As Object
            Dim Generic_QryData As ADODB.Recordset
            Generic_QryData = connAS400.Execute("select dsuser from Daily.sodsscl where dsfile = '" & myFile & "'", Parms, -1)

            If Not Generic_QryData.EOF Then
                'see if file is in use
                If Not Len(Trim(Generic_QryData.Fields("dsuser").Value)) = 0 And
                        Not Generic_QryData.Fields("dsuser").Value = UserId Then
                    Dim response As Integer
                    response = MsgBox("File is in use by " & Generic_QryData.Fields("dsuser").Value & ".  Do you want to continue?", MsgBoxStyle.YesNo, "File in use")
                    If Not response = 6 Then Exit Sub
                End If
                'Flag that you are using the file
                Dim CallGeneric As New ADODB.Command
                With CallGeneric
                    .ActiveConnection = connAS400
                    .CommandText = "update daily.sodsscl set " &
                    "dsuser" &
                    " = " &
                    Quoted(UserId) & "" &
                    " where dsfile = '" & myFile & "'"
                    .Prepared = False
                    .Execute()
                End With
            Else
                'Flag that you are using the file
                Dim CallGeneric As New ADODB.Command
                With CallGeneric
                    .ActiveConnection = connAS400
                    .CommandText = "insert into daily.sodsscp " &
             "( dsfile," &
            " dsdate," &
            " dsuser) " &
            "Values " &
            "(" & Quoted(myFile) & ", " &
              Format(Now(), "yyyyMMdd") & ", " &
            Quoted(UserId) & ")"
                    .Prepared = False
                    .Execute()
                End With
            End If
            CurrentFtpFilP = inFile
            srcFileName = "\\ppc-data\share1\images\box\OutsideFreight\" & inFile

            Dim codecs As RasterCodecs = New RasterCodecs()
            Dim info As CodecsImageInfo = codecs.GetInformation(srcFileName, True)
            CurrentPageImageFormat = info.Format
            CurrentPagePixelFormat = info.BitsPerPixel
            LabPages.Text = info.TotalPages

            LoadPage(1)
        Catch ex As Exception
            MsgBox("Error opening document " & ex.ToString, MsgBoxStyle.OkOnly, "")
        End Try

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub UltraGridScan_BeforeRowsDeleted(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs) Handles UltraGridScan.BeforeRowsDeleted
        ' Stop the grid from displaying it's message box.
        e.DisplayPromptMsg = False
    End Sub

#End Region

#Region "Document Section"

    Private Sub CmdDeleteDocPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDeleteDocPage.Click
        Dim response As Int32
        response = MsgBox("Are you sure you want to permanently delete this page?", MsgBoxStyle.YesNo, "Remove page")
        If response = 6 Then
            DeleteThisPage()
        End If
    End Sub

    Public Sub DeleteThisPage()
        Try

            If Convert.ToInt32(LabPages.Text) = 1 Then
                If DocumentFrom = "Scan" Then
                    UltraGridScan.ActiveRow.Delete()
                    UltraGridScan.Text = "(" & UltraGridScan.Rows.Count() & ") Unassigned Scans"
                Else

                End If
                Try
                    Kill(srcFileName)
                Catch
                End Try
            Else
                Try
                    codecs.DeletePage(srcFileName, LabPage.Text)
                Catch
                End Try
            End If
            LabPages.Text -= 1
            If Convert.ToInt32(LabPages.Text) < 1 Then
                PictureBox1.AutoScroll = True
                ' PictureBox1.Image = Nothing
                With UltraDockManager1
                    .PaneFromControl(PictureBox1).Close()
                    .Visible = False
                End With
                PanelImagePages.Visible = False
            Else
                LoadPage(LabPage.Text)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub myFrmOutsideFreightDocumentType_CopyThisPage(PassDocumentNbr As Integer, PassImageLocation As Integer) Handles myFrmOutsideFreightDocumentType.CopyThisPage
        Try

            'need to get folder from imfoldp file
            Dim myImfoldp As New iSeries.ImFoldp
            With myImfoldp
                .Retrieve(PassImageLocation)
                If Not .found Then
                    ' should already exist but just in case
                    .imKey = PassImageLocation
                    .Create()
                End If
            End With

            Dim Folder As String = myImfoldp.imDir
            If Not Directory.Exists(Folder) Then
                Directory.CreateDirectory(Folder)
            End If

            If iSeriesIP = "192.168.0.100" Or iSeriesIP = "PORTS" Then
                codecs.Save(PictureBox1.Image, Folder & PassDocumentNbr & ".tif", RasterImageFormat.TifLzw, CurrentPagePixelFormat, 1, 1, 0, CodecsSavePageMode.Append)
            Else
                MsgBox("A document would be saved here.  Since you are not in the live system this document is not being saved.", MsgBoxStyle.OkOnly, "Notification")
            End If
        Catch
            MsgBox("There was an error copying this page.  Please make sure this document got assigned.", MsgBoxStyle.OkOnly, "Copy this page")
        End Try
    End Sub
#End Region


#Region "Invoices Grid"

    Private Sub UltraGridInvoices_BeforeRowsDeleted(sender As Object, e As Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs) Handles UltraGridInvoices.BeforeRowsDeleted
        e.DisplayPromptMsg = False
    End Sub

    Private Sub UltraGridInvoices_InitializeRow(sender As Object, e As Infragistics.Win.UltraWinGrid.InitializeRowEventArgs) Handles UltraGridInvoices.InitializeRow
        Try
            Select Case e.Row.Band.Index
                ' invoice 
                Case Is = 0

                    ' Docs
                    If e.Row.Cells("Docs").Value > 0 Then
                        e.Row.Cells("Doc").Appearance.BackColor = System.Drawing.Color.Thistle
                    End If

            End Select
        Catch
        End Try
    End Sub

    Private Sub UltraGridInvoices_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridInvoices.InitializeLayout
        AddFilterToGrid(e)        ' ----------------------------------------------------------------------------------

        With UltraGridInvoices.DisplayLayout.Bands(0)
            .Columns("Account").Hidden = True
            .Columns("Docs").Hidden = True
            .Columns("Freight").Format = "$###,###.00"
            .Columns("Freight").CellAppearance.TextHAlign = HAlign.Right
            .Columns("Customer").Width = 170
            .Columns("Invoice").Width = 65
            .Columns("Billed").Width = 45
            .Columns("Date").Width = 70
            .Columns("Doc").Width = 50
            .Columns("ST").Width = 40
            .Columns("Invoice").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Doc").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Date").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button

            Dim i As Int32
            For i = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i
        End With

        With UltraGridInvoices.DisplayLayout
            .Override.CellClickAction = CellClickAction.CellSelect
        End With

    End Sub

    Private Sub UltraGridInvoices_ClickCellButton(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridInvoices.ClickCellButton
        Select Case e.Cell.Column.Key
            'Select Case Convert.ToInt64(e.Cell.Column.Index)
            Case Is = "Invoice"
                Dim mySetHandshake As New Billing2013.Class1

                mySetHandshake.SetHandShake(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder, iSeriesIP)
                Dim myFrmInvoice As New Billing2013.FrmInvoice(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder)
                '           AddHandler myFrmInvoice.ShowTransportationInvoice, AddressOf myShowTransportationInvoice
                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                myFrmInvoice.ShowMe(Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Invoice").Value))
                Me.Cursor = System.Windows.Forms.Cursors.Default
                '            Case Is = 2
            Case Is = "Doc"
                'If Not UltraGridInvoices.ActiveRow.Cells("Original#").Value = 0 Then
                '    MsgBox("This is a rebill or a credit.  The original invoice # is " & UltraGridInvoices.ActiveRow.Cells("Original#").Value, MsgBoxStyle.OkOnly)
                '    Exit Sub
                'End If

                MiscellaneousInvoice = False
                myFrmOutsideFreightDocumentType = New FrmOutsideFreightDocumentType
                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                myFrmOutsideFreightDocumentType.ShowMe(Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Invoice").Value), Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Account").Value), UltraGridInvoices.ActiveRow.Cells("Customer").Value)
                Me.Cursor = System.Windows.Forms.Cursors.Default
            Case Is = "Date"

                Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
                'If Not Len(Trim(UltraGridInvoices.ActiveRow.Cells("Pd Date").Value)) = 0 Then
                '    'inquiry
                '    Dim myFrmInvFreightInq As New FrmInvFreightInq
                '    myFrmInvFreightInq.ShowMe(Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Invoice").Value))
                'Else
                'pay
                myFrmInvOutsideFreight = New FrmInvOutsideFreight
                myFrmInvOutsideFreight.ShowMe(Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Invoice").Value), 0, 0, 0, 0, UltraGridInvoices.ActiveRow, VendorTable)
                'End If

                Me.Cursor = System.Windows.Forms.Cursors.Default
                'Case Is = "Remove"
                '    RemoveInvoice(Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Invoice").Value), Convert.ToInt32(UltraGridInvoices.ActiveRow.Cells("Paid").Value))
        End Select

    End Sub
#End Region

    '#Region "Remove Invoice"
    '    Private Sub RemoveInvoice(ByVal InInvoice As Integer, ByVal inPaid As Integer)
    '        Dim Reference As String
    '        Dim InvoiceDate As Integer
    '        Dim Carrier As Integer
    '        Dim Payment As decimal

    '        Dim Parms As Object
    '        Dim Daily_GenericDATA As ADODB.Recordset
    '        Dim CallGeneric As New ADODB.Command()

    '        If Not inPaid = 0 Then
    '            ' get frt invoice record
    '            Daily_GenericDATA = connAS400.Execute("select * from daily.soapfhl where fhinv# = " & InInvoice, Parms, -1)
    '            If Daily_GenericDATA.EOF Then
    '                MsgBox("No payment record found to delete", MsgBoxStyle.OkOnly, "Remove Invoice")
    '                Exit Sub
    '            End If

    '            Reference = Daily_GenericDATA.Fields("fhref#").Value
    '            Carrier = Daily_GenericDATA.Fields("fhcar#").Value
    '            Payment = Daily_GenericDATA.Fields("fhfrt$").Value

    '            ' get frt Header record
    '            Daily_GenericDATA = connAS400.Execute("select * from daily.soapftl where ftcar# = " & Carrier & " and ftref# = '" & Reference & "'", Parms, -1)
    '            If Daily_GenericDATA.EOF Then
    '                MsgBox("No payment record found to delete", MsgBoxStyle.OkOnly, "Remove Invoice")
    '                Exit Sub
    '            End If

    '            If Not Daily_GenericDATA.Fields("ftstat").Value = "R" Then
    '                MsgBox("Payment record has already been selected for payment", MsgBoxStyle.OkOnly, "Remove Invoice")
    '                Exit Sub
    '            End If

    '            InvoiceDate = Daily_GenericDATA.Fields("ftindt").Value

    '            Dim response As Integer
    '            response = MsgBox("Are you sure you want to delete this Payment?", MsgBoxStyle.YesNo, "Confirm")
    '            If response = 7 Then
    '                Exit Sub
    '            End If


    '            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

    '            ' save soapfhl total to see if you should delete or update soapftl
    '            Dim Daily_QryData As ADODB.Recordset
    '            Daily_QryData = connAS400.Execute("SELECT sum(fhfrt$) as Total from daily.soapfhl where fhcar# = " & Carrier & " and fhref# = '" & Reference & "'" & " and fhinv# = " & InInvoice, Parms, -1)
    '            Dim SoapfhlTotal As decimal = Daily_QryData.Fields("Total").Value
    '            ' get soapftl total to see if you should delete or update it
    '            Daily_QryData = connAS400.Execute("SELECT sum(ftfrt$) as Total from daily.soapftl where ftcar# = " & Carrier & " and ftref# = '" & Reference & "'", Parms, -1)
    '            Dim SoapftlTotal As decimal = Daily_QryData.Fields("Total").Value


    '            ' delete soapfhp records
    '            With CallGeneric
    '                .ActiveConnection = connAS400
    '                .CommandText = "delete from daily.soapfhp where " &
    '                " fhcar# = " & Carrier &
    '                " and fhref# = '" & Reference & "' " &
    '                " and fhinv# = " & InInvoice
    '                .Prepared = False
    '                .Execute()
    '            End With

    '            If SoapftlTotal = SoapfhlTotal Then
    '                ' soapftp
    '                With CallGeneric
    '                    .ActiveConnection = connAS400
    '                    .CommandText = "delete from daily.soapftp where " &
    '                         " ftcar# = " & Carrier &
    '                         " and ftref# = '" & Reference & "' " &
    '                         " and ftstat = 'R'"
    '                    .Prepared = False
    '                    .Execute()
    '                End With
    '            Else
    '                ' set total amount in soapftl
    '                With CallGeneric
    '                    .ActiveConnection = connAS400
    '                    .CommandText = "update daily.soapftl set " &
    '                    "( ftfrt$) " &
    '                    "= " &
    '                    "(" & (SoapftlTotal - SoapfhlTotal) & ")" &
    '                    " where ftcar# = " & Carrier &
    '                    " and ftref# = '" & Reference & "' "
    '                    .Prepared = False
    '                    .Execute()
    '                End With
    '            End If


    '            ' soapmsp
    '            With CallGeneric
    '                .ActiveConnection = connAS400
    '                .CommandText = "delete from daily.soapmsp where " &
    '                " mmcar# = " & Carrier &
    '                " and mmref# = '" & Reference & "'" &
    '                " and mminv# = " & InInvoice
    '                .Prepared = False
    '                .Execute()
    '            End With
    '        End If

    '        ' set paid info in soinvp
    '        With CallGeneric
    '            .ActiveConnection = connAS400
    '            .CommandText = "update daily.soinvl set " &
    '            "( sofrtd," &
    '            " sofrt$)" &
    '            "= " &
    '            "(0, " &
    '            "0) " &
    '            "where soinv# = " & InInvoice
    '            .Prepared = False
    '            .Execute()
    '        End With

    '        Dim Daily_SoHstlUpd As New ADODB.Command()
    '        With Daily_SoHstlUpd
    '            .ActiveConnection = connAS400
    '            .CommandText = "insert into daily.sohstl " &
    '            "( ihinv#," &
    '            " ihdate," &
    '            " ihtime," &
    '            " ihuser," &
    '            " ihsyst," &
    '            " ihnote)" &
    '            "Values " &
    '            "(" & InInvoice & ", " &
    '              Format(Now(), "yyyyMMdd") & ", " &
    '              Format(Now(), "HHmm") & ", " &
    '            "'" & UserId & "', " &
    '            "'F', " &
    '            "'Frt Payment removed')"
    '            .Prepared = False
    '            .Execute()
    '        End With
    '        UltraGridInvoices.ActiveRow.Cells("Paid").Value = 0
    '        UltraGridInvoices.ActiveRow.Cells("Pd Date").Value = ""
    '        Me.Cursor = System.Windows.Forms.Cursors.Default

    '    End Sub
    '#End Region

    '#Region "Copy to Clipboard"
    '    Private Sub CmdCopyToClipboard_Click(sender As System.Object, e As System.EventArgs) Handles CmdCopyToClipboard.Click
    '        Dim myString As String = ""

    '        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In UltraGridInvoices.Rows.GetFilteredInNonGroupByRows
    '            myString &= String.Format("{0,-5} | {1,-30} | {2,-6} | {3,-3} | {4,-10} | {5,-25} | {6,-20} | {7,-2}", row.Cells("Vendor #").Value,
    '            Mid(row.Cells("Vendor").Value, 1, 30), row.Cells("Invoice").Value, row.Cells("Billed").Value, Format(row.Cells("Date").Value, "short date"),
    '            Mid(row.Cells("Customer").Value, 1, 25), Mid(row.Cells("City").Value, 1, 20), row.Cells("ST").Value) & vbCrLf
    '        Next
    '        System.Windows.Forms.Clipboard.SetText(myString)
    '        MsgBox("Data has been copied to the clipboard.  You can paste this text into an email or anywhere it is needed.", MsgBoxStyle.OkOnly, "Notification")

    '    End Sub
    '#End Region

    '#Region "Print"
    '    Private Sub CmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles CmdPrint.Click
    '        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
    '        Dim myFrmCrystalViewer As New FrmCrystalViewer("Direct Ship Queue", PrintRunSheet)
    '        myFrmCrystalViewer.Show()
    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '    Private Function PrintRunSheet() As CrystalDecisions.CrystalReports.Engine.ReportDocument

    '        Dim myTable As DataTable = New DataTable("DirectShip")
    '        With myTable
    '            .Columns.Add("Vendor#", GetType(Integer))
    '            .Columns.Add("Vendor", GetType(String))
    '            .Columns.Add("Invoice", GetType(Integer))
    '            .Columns.Add("Billed", GetType(String))
    '            .Columns.Add("Date", GetType(Date))
    '            .Columns.Add("Customer", GetType(String))
    '            .Columns.Add("City", GetType(String))
    '            .Columns.Add("State", GetType(String))
    '        End With

    '        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In UltraGridInvoices.Rows.GetFilteredInNonGroupByRows
    '            Dim InvoiceRow As DataRow = myTable.NewRow
    '            InvoiceRow("Vendor#") = row.Cells("Vendor #").Value
    '            InvoiceRow("Vendor") = row.Cells("Vendor").Value
    '            InvoiceRow("Invoice") = row.Cells("Invoice").Value
    '            InvoiceRow("Billed") = row.Cells("Billed").Value
    '            InvoiceRow("Date") = row.Cells("Date").Value
    '            InvoiceRow("Customer") = row.Cells("Customer").Value
    '            InvoiceRow("City") = row.Cells("City").Value
    '            InvoiceRow("State") = row.Cells("ST").Value
    '            myTable.Rows.Add(InvoiceRow)
    '        Next

    '        'Get the Report Location
    '        Dim strReportPath As String = PassApplicationStartupPathToDLL & "\RPTFiles" & "\rpDirectShip.rpt"

    '        'Check file exists
    '        If Not IO.File.Exists(strReportPath) Then
    '            Throw (New Exception("Unable to locate report file:" & vbCrLf & strReportPath))
    '        End If

    '        Dim rptDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    '        rptDocument.Load(strReportPath)

    '        rptDocument.SetDataSource(myTable)

    '        Return rptDocument

    '    End Function

    '#End Region

#Region "Vendor"
    Public Sub LoadVendor()
        ' Vendor table
        VendorTable.Columns.Add("Vendor", GetType(Integer))
        VendorTable.Columns.Add("Name", GetType(String))


        Dim QryData As ADODB.Recordset
        QryData = Nothing
        QryData = connAS400.Execute("select tatkey, vndmnm from daily.potablp 
        left join parafiles.appvnd on tatkey = vndnum
        where tatabl = 'DOF' order by vndmnm", Parms, -1)


        'Load the data
        While Not QryData.EOF
            Dim VendorRow As DataRow = VendorTable.NewRow

            VendorRow("Vendor") = QryData.Fields("tatkey").Value
            VendorRow("Name") = QryData.Fields("vndmnm").Value

            VendorTable.Rows.Add(VendorRow)
            QryData.MoveNext()
        End While

        QryData.Close()

    End Sub


#End Region
End Class
