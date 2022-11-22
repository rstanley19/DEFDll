Option Explicit On 

Imports System.Globalization
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.Text
Imports System.Drawing
Imports System.io

Imports Leadtools
Imports Leadtools.Codecs
Imports Leadtools.WinForms

Public Class FrmInvOutsideFreight
    Inherits System.Windows.Forms.Form


    ' Items Detail
    Private ItemsTable As New DataTable("Est")
    Private DataSetItems As New DataSet()

    Private Parms As Object

    Private CustomerName As String
    Private Linked As Integer

    Private VendorInvoiceImage As Integer
    Private VendorInvoiceImageLocation As Integer

    Private BillToCustomer As Integer
    Private FreightOnlyInvoice As Boolean

    Private CurrentPage As Int32
    Private CurrentPageImageFormat As String
    Private CurrentPagePixelFormat As String

    Public wrkDocumentNbr As Int32
    Private wrkImageLocation As Int32
    Private wrkDocType As Int32

    Private BlUsedBy As Int32

    'document List
    Dim DocsTable As New DataTable("Docs")
    Dim DataSetDocs As New DataSet()

    ' display document
    Dim srcFileName As String
    Private saveZoom As decimal = 0.5

    Dim HoldInProgress As Boolean

    Dim DocumentCell As Infragistics.Win.UltraWinGrid.UltraGridCell

    Private CurrentDocRow As Int32

    ' document entry edit
    ' reference to cell containing bad input
    Private c_cellBadCell As Infragistics.Win.UltraWinGrid.UltraGridCell

    Private codecs As RasterCodecs
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents UltraGridMisc As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabError As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TxtinvDollar As Infragistics.Win.UltraWinEditors.UltraCurrencyEditor
    Friend WithEvents comboBoxDocType As Infragistics.Win.UltraWinEditors.UltraComboEditor

    Private InvFreight_SoInvlDATA As ADODB.Recordset
    Friend WithEvents CmdFlag As System.Windows.Forms.Button
    Friend WithEvents cbVendor As UltraCombo
    Friend WithEvents LabEstimated As Windows.Forms.Label
    Friend WithEvents Label3 As Windows.Forms.Label
    Public Event PayInvoice(BackRow As Infragistics.Win.UltraWinGrid.UltraGridRow)
    Private myRow As Infragistics.Win.UltraWinGrid.UltraGridRow

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        RasterCodecs.Startup()
        codecs = New RasterCodecs()

        Dim DSDocTypes As DataSet ' Direct Ship Vendors
        ' Direct Ship Vendors

        DSDocTypes = New DataSet
        Dim DSDocTable As New DataTable("DSDocTable")
        DSDocTable.Columns.Add("Key", GetType(Integer))
        DSDocTable.Columns.Add("Type", GetType(String))

        DSDocTypes.Tables.Add(DSDocTable)
        Dim Daily_QryData As ADODB.Recordset
        Daily_QryData = connAS400.Execute("SELECT * FROM daily.potablp WHERE tatabl = 'DOC' order by tatkey", Parms, -1)
        While Not Daily_QryData.EOF
            Dim DSDocRow As DataRow = DSDocTable.NewRow
            DSDocRow("Key") = Daily_QryData.Fields("TaTkey").Value
            DSDocRow("Type") = Daily_QryData.Fields("TaDesc").Value
            DSDocTable.Rows.Add(DSDocRow)
            Daily_QryData.MoveNext()
        End While
        comboBoxDocType.DataSource = DSDocTypes.Tables("DSDocTable")
        comboBoxDocType.ValueMember = DSDocTypes.Tables("DSDocTable").Columns("Key").Caption
        comboBoxDocType.DisplayMember = DSDocTypes.Tables("DSDocTable").Columns("Type").Caption
        comboBoxDocType.Text = ""

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
    Friend WithEvents LabStatus As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents LabInvoice As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxDocument As System.Windows.Forms.GroupBox
    Friend WithEvents UltraGridDocs As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents PanelImagePages As System.Windows.Forms.Panel
    Friend WithEvents PictureBoxMoveToFaxQueue As System.Windows.Forms.PictureBox
    Friend WithEvents CmdDeleteDocPage As System.Windows.Forms.Button
    Friend WithEvents PictureBoxPrinter As System.Windows.Forms.PictureBox
    Friend WithEvents LabPages As System.Windows.Forms.Label
    Friend WithEvents LabOf As System.Windows.Forms.Label
    Friend WithEvents LabPage As System.Windows.Forms.Label
    Friend WithEvents PictureBoxImageRight As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxImageLeft As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxRotate As System.Windows.Forms.PictureBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    Private WithEvents PictureBox2 As Leadtools.WinForms.RasterImageViewer
    Friend WithEvents UltraDockManager1 As Infragistics.Win.UltraWinDock.UltraDockManager
    Friend WithEvents _FrmInvBillingUnpinnedTabAreaLeft As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmInvBillingUnpinnedTabAreaRight As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmInvBillingUnpinnedTabAreaTop As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmInvBillingUnpinnedTabAreaBottom As Infragistics.Win.UltraWinDock.UnpinnedTabArea
    Friend WithEvents _FrmInvBillingAutoHideControl As Infragistics.Win.UltraWinDock.AutoHideControl
    Friend WithEvents TreeViewSource As System.Windows.Forms.TreeView
    Friend WithEvents WindowDockingArea1 As Infragistics.Win.UltraWinDock.WindowDockingArea
    Friend WithEvents DockableWindow1 As Infragistics.Win.UltraWinDock.DockableWindow
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TxtComment As System.Windows.Forms.TextBox
    Friend WithEvents WindowDockingArea2 As Infragistics.Win.UltraWinDock.WindowDockingArea
    Friend WithEvents DockableWindow2 As Infragistics.Win.UltraWinDock.DockableWindow
    Friend WithEvents LabCustomer As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtDate As UltraWinEditors.UltraDateTimeEditor
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtRef As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmdPay As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents LinkLabelComment As System.Windows.Forms.LinkLabel
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents ListBoxNotes As System.Windows.Forms.ListBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RasterMagnifyGlass1 As Leadtools.WinForms.RasterMagnifyGlass = New Leadtools.WinForms.RasterMagnifyGlass()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("", -1)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInvOutsideFreight))
        Dim DockAreaPane1 As Infragistics.Win.UltraWinDock.DockAreaPane = New Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, New System.Guid("8c1ae0d1-26f3-4db9-866f-79f6d9b7e4f5"))
        Dim DockableControlPane1 As Infragistics.Win.UltraWinDock.DockableControlPane = New Infragistics.Win.UltraWinDock.DockableControlPane(New System.Guid("6354d6ee-c735-4202-9f48-8cedab697de8"), New System.Guid("8c1ae0d1-26f3-4db9-866f-79f6d9b7e4f5"), -1, New System.Guid("00000000-0000-0000-0000-000000000000"), -1)
        Dim DockAreaPane2 As Infragistics.Win.UltraWinDock.DockAreaPane = New Infragistics.Win.UltraWinDock.DockAreaPane(Infragistics.Win.UltraWinDock.DockedLocation.Floating, New System.Guid("cbcc3d59-40f1-4b56-9e96-83433e72ad11"))
        Dim DockableControlPane2 As Infragistics.Win.UltraWinDock.DockableControlPane = New Infragistics.Win.UltraWinDock.DockableControlPane(New System.Guid("b4fb208e-8ad1-4ea8-9b19-2f7c191de22a"), New System.Guid("cbcc3d59-40f1-4b56-9e96-83433e72ad11"), -1, New System.Guid("00000000-0000-0000-0000-000000000000"), -1)
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.TreeViewSource = New System.Windows.Forms.TreeView()
        Me.TxtComment = New System.Windows.Forms.TextBox()
        Me.GroupBoxDocument = New System.Windows.Forms.GroupBox()
        Me.TxtinvDollar = New Infragistics.Win.UltraWinEditors.UltraCurrencyEditor()
        Me.CmdFlag = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtDate = New Infragistics.Win.UltraWinEditors.UltraDateTimeEditor()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtRef = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmdPay = New System.Windows.Forms.Button()
        Me.LinkLabelComment = New System.Windows.Forms.LinkLabel()
        Me.PictureBox2 = New Leadtools.WinForms.RasterImageViewer()
        Me.LabStatus = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.LabInvoice = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabEstimated = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabCustomer = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.UltraGridDocs = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.comboBoxDocType = New Infragistics.Win.UltraWinEditors.UltraComboEditor()
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
        Me.PrintDocument1 = New System.Drawing.Printing.PrintDocument()
        Me.UltraDockManager1 = New Infragistics.Win.UltraWinDock.UltraDockManager(Me.components)
        Me._FrmInvBillingUnpinnedTabAreaLeft = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmInvBillingUnpinnedTabAreaRight = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmInvBillingUnpinnedTabAreaTop = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmInvBillingUnpinnedTabAreaBottom = New Infragistics.Win.UltraWinDock.UnpinnedTabArea()
        Me._FrmInvBillingAutoHideControl = New Infragistics.Win.UltraWinDock.AutoHideControl()
        Me.WindowDockingArea1 = New Infragistics.Win.UltraWinDock.WindowDockingArea()
        Me.DockableWindow1 = New Infragistics.Win.UltraWinDock.DockableWindow()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.WindowDockingArea2 = New Infragistics.Win.UltraWinDock.WindowDockingArea()
        Me.DockableWindow2 = New Infragistics.Win.UltraWinDock.DockableWindow()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.ListBoxNotes = New System.Windows.Forms.ListBox()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LabError = New System.Windows.Forms.Label()
        Me.UltraGridMisc = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.GroupBoxDocument.SuspendLayout()
        CType(Me.TxtinvDollar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.UltraGridDocs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.comboBoxDocType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelImagePages.SuspendLayout()
        CType(Me.PictureBoxMoveToFaxQueue, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxPrinter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxImageRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxImageLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxRotate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.WindowDockingArea1.SuspendLayout()
        Me.DockableWindow1.SuspendLayout()
        Me.WindowDockingArea2.SuspendLayout()
        Me.DockableWindow2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.UltraGridMisc, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TreeViewSource
        '
        Me.TreeViewSource.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeViewSource.LineColor = System.Drawing.Color.Empty
        Me.TreeViewSource.Location = New System.Drawing.Point(0, 19)
        Me.TreeViewSource.Name = "TreeViewSource"
        Me.TreeViewSource.Size = New System.Drawing.Size(177, 174)
        Me.TreeViewSource.TabIndex = 131
        Me.TreeViewSource.Visible = False
        '
        'TxtComment
        '
        Me.TxtComment.Location = New System.Drawing.Point(0, 16)
        Me.TxtComment.MaxLength = 80
        Me.TxtComment.Name = "TxtComment"
        Me.TxtComment.Size = New System.Drawing.Size(439, 20)
        Me.TxtComment.TabIndex = 132
        Me.TxtComment.Visible = False
        '
        'GroupBoxDocument
        '
        Me.GroupBoxDocument.Controls.Add(Me.TxtinvDollar)
        Me.GroupBoxDocument.Controls.Add(Me.CmdFlag)
        Me.GroupBoxDocument.Controls.Add(Me.Label2)
        Me.GroupBoxDocument.Controls.Add(Me.TxtDate)
        Me.GroupBoxDocument.Controls.Add(Me.Label9)
        Me.GroupBoxDocument.Controls.Add(Me.TxtRef)
        Me.GroupBoxDocument.Controls.Add(Me.Label8)
        Me.GroupBoxDocument.Controls.Add(Me.CmdPay)
        Me.GroupBoxDocument.Controls.Add(Me.LinkLabelComment)
        Me.GroupBoxDocument.Location = New System.Drawing.Point(425, 40)
        Me.GroupBoxDocument.Name = "GroupBoxDocument"
        Me.GroupBoxDocument.Size = New System.Drawing.Size(257, 136)
        Me.GroupBoxDocument.TabIndex = 1
        Me.GroupBoxDocument.TabStop = False
        '
        'TxtinvDollar
        '
        Me.TxtinvDollar.AutoSize = False
        Me.TxtinvDollar.Location = New System.Drawing.Point(107, 65)
        Me.TxtinvDollar.MaskInput = "$-n,nnn,nnn.nn"
        Me.TxtinvDollar.MaxValue = New Decimal(New Integer() {9999999, 0, 0, 0})
        Me.TxtinvDollar.MinValue = New Decimal(New Integer() {9999999, 0, 0, -2147483648})
        Me.TxtinvDollar.Name = "TxtinvDollar"
        Me.TxtinvDollar.Size = New System.Drawing.Size(88, 21)
        Me.TxtinvDollar.TabIndex = 2
        '
        'CmdFlag
        '
        Me.CmdFlag.ForeColor = System.Drawing.Color.White
        Me.CmdFlag.Location = New System.Drawing.Point(14, 91)
        Me.CmdFlag.Name = "CmdFlag"
        Me.CmdFlag.Size = New System.Drawing.Size(87, 24)
        Me.CmdFlag.TabIndex = 3
        Me.CmdFlag.TabStop = False
        Me.CmdFlag.Text = "Flag as paid"
        Me.CmdFlag.Visible = False
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(19, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 156
        Me.Label2.Text = "Invoice Total"
        '
        'TxtDate
        '
        Me.TxtDate.Location = New System.Drawing.Point(107, 39)
        Me.TxtDate.Name = "TxtDate"
        Me.TxtDate.Size = New System.Drawing.Size(88, 21)
        Me.TxtDate.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(19, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 16)
        Me.Label9.TabIndex = 154
        Me.Label9.Text = "Invoice Date"
        '
        'TxtRef
        '
        Me.TxtRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtRef.Location = New System.Drawing.Point(107, 15)
        Me.TxtRef.MaxLength = 10
        Me.TxtRef.Name = "TxtRef"
        Me.TxtRef.Size = New System.Drawing.Size(88, 20)
        Me.TxtRef.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(19, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 16)
        Me.Label8.TabIndex = 151
        Me.Label8.Text = "Reference #"
        '
        'CmdPay
        '
        Me.CmdPay.ForeColor = System.Drawing.Color.White
        Me.CmdPay.Location = New System.Drawing.Point(107, 91)
        Me.CmdPay.Name = "CmdPay"
        Me.CmdPay.Size = New System.Drawing.Size(40, 24)
        Me.CmdPay.TabIndex = 4
        Me.CmdPay.TabStop = False
        Me.CmdPay.Text = "Pay"
        '
        'LinkLabelComment
        '
        Me.LinkLabelComment.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabelComment.LinkColor = System.Drawing.Color.White
        Me.LinkLabelComment.Location = New System.Drawing.Point(174, 97)
        Me.LinkLabelComment.Name = "LinkLabelComment"
        Me.LinkLabelComment.Size = New System.Drawing.Size(56, 16)
        Me.LinkLabelComment.TabIndex = 5
        Me.LinkLabelComment.TabStop = True
        Me.LinkLabelComment.Text = "Comment"
        Me.LinkLabelComment.VisitedLinkColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        '
        'PictureBox2
        '
        Me.PictureBox2.AnimateFloater = True
        Me.PictureBox2.AnimateRegion = True
        Me.PictureBox2.AutoDisposeImages = True
        Me.PictureBox2.AutoResetScaleFactor = True
        Me.PictureBox2.AutoResetScrollPosition = True
        Me.PictureBox2.AutoScroll = True
        Me.PictureBox2.BindingData = Nothing
        Me.PictureBox2.BindingLoadBitsPerPixel = 24
        Me.PictureBox2.BindingRasterCodecs = Nothing
        Me.PictureBox2.BindingSaveBitsPerPixel = 24
        Me.PictureBox2.BindingSaveImageFormat = Leadtools.RasterImageFormat.Jpeg
        Me.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PictureBox2.DoubleBuffer = True
        Me.PictureBox2.EnableScrollingInterface = False
        Me.PictureBox2.EnableTimer = False
        Me.PictureBox2.FloaterImage = Nothing
        Me.PictureBox2.FloaterPosition = New System.Drawing.Point(0, 0)
        Me.PictureBox2.FloaterVisible = True
        Me.PictureBox2.FrameColor = System.Drawing.Color.Black
        Me.PictureBox2.FrameShadowColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PictureBox2.FrameShadowSize = New System.Drawing.SizeF(0!, 0!)
        Me.PictureBox2.FramesIsPartOfImage = True
        Me.PictureBox2.FrameSize = New System.Drawing.SizeF(0!, 0!)
        Me.PictureBox2.HorizontalAlignMode = Leadtools.RasterPaintAlignMode.Near
        Me.PictureBox2.Image = Nothing
        Me.PictureBox2.InteractiveMode = Leadtools.WinForms.RasterViewerInteractiveMode.None
        Me.PictureBox2.InteractiveRegionCombineMode = Leadtools.RasterRegionCombineMode.[Set]
        Me.PictureBox2.InteractiveRegionType = Leadtools.WinForms.RasterViewerInteractiveRegionType.Rectangle
        Me.PictureBox2.Location = New System.Drawing.Point(0, 32)
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
        Me.PictureBox2.MagnifyGlass = RasterMagnifyGlass1
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.ScaleFactor = 1.0R
        Me.PictureBox2.ScrollPosition = New System.Drawing.Point(0, 0)
        Me.PictureBox2.Size = New System.Drawing.Size(720, 216)
        Me.PictureBox2.SizeMode = Leadtools.RasterPaintSizeMode.Normal
        Me.PictureBox2.SmallScrollChangeRatio = 20
        Me.PictureBox2.SourceRectangle = New System.Drawing.Rectangle(0, 0, 0, 0)
        Me.PictureBox2.TabIndex = 0
        Me.PictureBox2.TabStop = False
        Me.PictureBox2.Text = "PictureBox2"
        Me.PictureBox2.UseDpi = False
        Me.PictureBox2.VerticalAlignMode = Leadtools.RasterPaintAlignMode.Near
        '
        'LabStatus
        '
        Me.LabStatus.BackColor = System.Drawing.Color.Red
        Me.LabStatus.ForeColor = System.Drawing.Color.White
        Me.LabStatus.Location = New System.Drawing.Point(408, 8)
        Me.LabStatus.Name = "LabStatus"
        Me.LabStatus.Size = New System.Drawing.Size(104, 16)
        Me.LabStatus.TabIndex = 97
        Me.LabStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LabStatus.Visible = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(16, 8)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(288, 24)
        Me.Label7.TabIndex = 77
        Me.Label7.Text = "Detail Verification Screen for Invoice #"
        '
        'LabInvoice
        '
        Me.LabInvoice.BackColor = System.Drawing.Color.Transparent
        Me.LabInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabInvoice.ForeColor = System.Drawing.Color.White
        Me.LabInvoice.Location = New System.Drawing.Point(304, 8)
        Me.LabInvoice.Name = "LabInvoice"
        Me.LabInvoice.Size = New System.Drawing.Size(88, 24)
        Me.LabInvoice.TabIndex = 79
        Me.LabInvoice.Text = "LabInvoice"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabEstimated)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cbVendor)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.LabCustomer)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(407, 136)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'LabEstimated
        '
        Me.LabEstimated.ForeColor = System.Drawing.Color.White
        Me.LabEstimated.Location = New System.Drawing.Point(75, 76)
        Me.LabEstimated.Name = "LabEstimated"
        Me.LabEstimated.Size = New System.Drawing.Size(326, 16)
        Me.LabEstimated.TabIndex = 148
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(9, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 33)
        Me.Label3.TabIndex = 147
        Me.Label3.Text = "Estimated Freight"
        '
        'cbVendor
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cbVendor.DisplayLayout.Appearance = Appearance1
        Me.cbVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cbVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cbVendor.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cbVendor.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.cbVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cbVendor.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.cbVendor.DisplayLayout.MaxColScrollRegions = 1
        Me.cbVendor.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cbVendor.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cbVendor.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.cbVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cbVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.cbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cbVendor.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cbVendor.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cbVendor.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.cbVendor.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cbVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cbVendor.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cbVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cbVendor.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.cbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cbVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cbVendor.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
        Me.cbVendor.Location = New System.Drawing.Point(75, 43)
        Me.cbVendor.Name = "cbVendor"
        Me.cbVendor.Size = New System.Drawing.Size(326, 22)
        Me.cbVendor.TabIndex = 145
        Me.cbVendor.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cbVendor, "Use DOF table to add / remove vendors")
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(9, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 143
        Me.Label1.Text = "Customer"
        '
        'LabCustomer
        '
        Me.LabCustomer.ForeColor = System.Drawing.Color.White
        Me.LabCustomer.Location = New System.Drawing.Point(75, 16)
        Me.LabCustomer.Name = "LabCustomer"
        Me.LabCustomer.Size = New System.Drawing.Size(326, 16)
        Me.LabCustomer.TabIndex = 141
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(9, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 16)
        Me.Label10.TabIndex = 142
        Me.Label10.Text = "Vendor"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.UltraGridDocs)
        Me.GroupBox5.Location = New System.Drawing.Point(687, 40)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(217, 136)
        Me.GroupBox5.TabIndex = 109
        Me.GroupBox5.TabStop = False
        '
        'UltraGridDocs
        '
        Appearance13.BackColor = System.Drawing.Color.Transparent
        Appearance13.ForeColor = System.Drawing.Color.Black
        Me.UltraGridDocs.DisplayLayout.Appearance = Appearance13
        UltraGridBand1.ColHeadersVisible = False
        Me.UltraGridDocs.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.UltraGridDocs.Location = New System.Drawing.Point(7, 13)
        Me.UltraGridDocs.Name = "UltraGridDocs"
        Me.UltraGridDocs.Size = New System.Drawing.Size(201, 120)
        Me.UltraGridDocs.TabIndex = 0
        Me.UltraGridDocs.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.comboBoxDocType)
        Me.GroupBox3.Controls.Add(Me.PictureBox2)
        Me.GroupBox3.ForeColor = System.Drawing.Color.Black
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(831, 248)
        Me.GroupBox3.TabIndex = 117
        Me.GroupBox3.TabStop = False
        '
        'comboBoxDocType
        '
        Me.comboBoxDocType.Location = New System.Drawing.Point(3, 9)
        Me.comboBoxDocType.MaxLength = 30
        Me.comboBoxDocType.Name = "comboBoxDocType"
        Me.comboBoxDocType.Size = New System.Drawing.Size(269, 21)
        Me.comboBoxDocType.TabIndex = 3
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
        Me.PanelImagePages.Location = New System.Drawing.Point(837, 144)
        Me.PanelImagePages.Name = "PanelImagePages"
        Me.PanelImagePages.Size = New System.Drawing.Size(56, 104)
        Me.PanelImagePages.TabIndex = 125
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
        '
        'CmdDeleteDocPage
        '
        Me.CmdDeleteDocPage.AllowDrop = True
        Me.CmdDeleteDocPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdDeleteDocPage.Image = CType(resources.GetObject("CmdDeleteDocPage.Image"), System.Drawing.Image)
        Me.CmdDeleteDocPage.Location = New System.Drawing.Point(0, 80)
        Me.CmdDeleteDocPage.Name = "CmdDeleteDocPage"
        Me.CmdDeleteDocPage.Size = New System.Drawing.Size(24, 20)
        Me.CmdDeleteDocPage.TabIndex = 38
        Me.CmdDeleteDocPage.TabStop = False
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
        Me.LabPages.TabIndex = 19
        Me.LabPages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabOf
        '
        Me.LabOf.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabOf.ForeColor = System.Drawing.Color.White
        Me.LabOf.Location = New System.Drawing.Point(17, 30)
        Me.LabOf.Name = "LabOf"
        Me.LabOf.Size = New System.Drawing.Size(16, 16)
        Me.LabOf.TabIndex = 18
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
        Me.LabPage.TabIndex = 17
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
        '
        'PrintDocument1
        '
        '
        'UltraDockManager1
        '
        DockAreaPane1.DefaultPaneSettings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.DefaultPaneSettings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.DefaultPaneSettings.AllowDockRight = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.DefaultPaneSettings.AllowDockTop = Infragistics.Win.DefaultableBoolean.[False]
        DockAreaPane1.DockedBefore = New System.Guid("cbcc3d59-40f1-4b56-9e96-83433e72ad11")
        DockAreaPane1.FloatingLocation = New System.Drawing.Point(44, 44)
        DockableControlPane1.Closed = True
        DockableControlPane1.Control = Me.TreeViewSource
        DockableControlPane1.OriginalControlBounds = New System.Drawing.Rectangle(32, 168, 168, 176)
        DockableControlPane1.Size = New System.Drawing.Size(100, 100)
        DockableControlPane1.Text = "Source"
        DockAreaPane1.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane1})
        DockAreaPane1.Size = New System.Drawing.Size(177, 193)
        DockAreaPane2.FloatingLocation = New System.Drawing.Point(116, 124)
        DockableControlPane2.Control = Me.TxtComment
        DockableControlPane2.OriginalControlBounds = New System.Drawing.Rectangle(312, 48, 280, 20)
        DockableControlPane2.Settings.AllowClose = Infragistics.Win.DefaultableBoolean.[False]
        DockableControlPane2.Settings.AllowDockBottom = Infragistics.Win.DefaultableBoolean.[False]
        DockableControlPane2.Settings.AllowDockLeft = Infragistics.Win.DefaultableBoolean.[False]
        DockableControlPane2.Settings.AllowDockRight = Infragistics.Win.DefaultableBoolean.[False]
        DockableControlPane2.Settings.AllowDockTop = Infragistics.Win.DefaultableBoolean.[False]
        DockableControlPane2.Size = New System.Drawing.Size(100, 100)
        DockableControlPane2.Text = "Enter comment and press enter"
        DockAreaPane2.Panes.AddRange(New Infragistics.Win.UltraWinDock.DockablePaneBase() {DockableControlPane2})
        DockAreaPane2.Size = New System.Drawing.Size(439, 40)
        Me.UltraDockManager1.DockAreas.AddRange(New Infragistics.Win.UltraWinDock.DockAreaPane() {DockAreaPane1, DockAreaPane2})
        Me.UltraDockManager1.HostControl = Me
        '
        '_FrmInvBillingUnpinnedTabAreaLeft
        '
        Me._FrmInvBillingUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me._FrmInvBillingUnpinnedTabAreaLeft.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmInvBillingUnpinnedTabAreaLeft.Location = New System.Drawing.Point(0, 0)
        Me._FrmInvBillingUnpinnedTabAreaLeft.Name = "_FrmInvBillingUnpinnedTabAreaLeft"
        Me._FrmInvBillingUnpinnedTabAreaLeft.Owner = Me.UltraDockManager1
        Me._FrmInvBillingUnpinnedTabAreaLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmInvBillingUnpinnedTabAreaLeft.Size = New System.Drawing.Size(0, 581)
        Me._FrmInvBillingUnpinnedTabAreaLeft.TabIndex = 126
        '
        '_FrmInvBillingUnpinnedTabAreaRight
        '
        Me._FrmInvBillingUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right
        Me._FrmInvBillingUnpinnedTabAreaRight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmInvBillingUnpinnedTabAreaRight.Location = New System.Drawing.Point(909, 0)
        Me._FrmInvBillingUnpinnedTabAreaRight.Name = "_FrmInvBillingUnpinnedTabAreaRight"
        Me._FrmInvBillingUnpinnedTabAreaRight.Owner = Me.UltraDockManager1
        Me._FrmInvBillingUnpinnedTabAreaRight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmInvBillingUnpinnedTabAreaRight.Size = New System.Drawing.Size(0, 581)
        Me._FrmInvBillingUnpinnedTabAreaRight.TabIndex = 127
        '
        '_FrmInvBillingUnpinnedTabAreaTop
        '
        Me._FrmInvBillingUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top
        Me._FrmInvBillingUnpinnedTabAreaTop.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmInvBillingUnpinnedTabAreaTop.Location = New System.Drawing.Point(0, 0)
        Me._FrmInvBillingUnpinnedTabAreaTop.Name = "_FrmInvBillingUnpinnedTabAreaTop"
        Me._FrmInvBillingUnpinnedTabAreaTop.Owner = Me.UltraDockManager1
        Me._FrmInvBillingUnpinnedTabAreaTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmInvBillingUnpinnedTabAreaTop.Size = New System.Drawing.Size(909, 0)
        Me._FrmInvBillingUnpinnedTabAreaTop.TabIndex = 128
        '
        '_FrmInvBillingUnpinnedTabAreaBottom
        '
        Me._FrmInvBillingUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me._FrmInvBillingUnpinnedTabAreaBottom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmInvBillingUnpinnedTabAreaBottom.Location = New System.Drawing.Point(0, 581)
        Me._FrmInvBillingUnpinnedTabAreaBottom.Name = "_FrmInvBillingUnpinnedTabAreaBottom"
        Me._FrmInvBillingUnpinnedTabAreaBottom.Owner = Me.UltraDockManager1
        Me._FrmInvBillingUnpinnedTabAreaBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmInvBillingUnpinnedTabAreaBottom.Size = New System.Drawing.Size(909, 0)
        Me._FrmInvBillingUnpinnedTabAreaBottom.TabIndex = 129
        '
        '_FrmInvBillingAutoHideControl
        '
        Me._FrmInvBillingAutoHideControl.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FrmInvBillingAutoHideControl.Location = New System.Drawing.Point(0, 0)
        Me._FrmInvBillingAutoHideControl.Name = "_FrmInvBillingAutoHideControl"
        Me._FrmInvBillingAutoHideControl.Owner = Me.UltraDockManager1
        Me._FrmInvBillingAutoHideControl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FrmInvBillingAutoHideControl.Size = New System.Drawing.Size(0, 0)
        Me._FrmInvBillingAutoHideControl.TabIndex = 130
        '
        'WindowDockingArea1
        '
        Me.WindowDockingArea1.Controls.Add(Me.DockableWindow1)
        Me.WindowDockingArea1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WindowDockingArea1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WindowDockingArea1.Location = New System.Drawing.Point(4, 4)
        Me.WindowDockingArea1.Name = "WindowDockingArea1"
        Me.WindowDockingArea1.Owner = Me.UltraDockManager1
        Me.WindowDockingArea1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.WindowDockingArea1.Size = New System.Drawing.Size(177, 193)
        Me.WindowDockingArea1.TabIndex = 0
        '
        'DockableWindow1
        '
        Me.DockableWindow1.Controls.Add(Me.TreeViewSource)
        Me.DockableWindow1.Location = New System.Drawing.Point(0, 0)
        Me.DockableWindow1.Name = "DockableWindow1"
        Me.DockableWindow1.Owner = Me.UltraDockManager1
        Me.DockableWindow1.Size = New System.Drawing.Size(439, 40)
        Me.DockableWindow1.TabIndex = 135
        '
        'WindowDockingArea2
        '
        Me.WindowDockingArea2.Controls.Add(Me.DockableWindow2)
        Me.WindowDockingArea2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WindowDockingArea2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WindowDockingArea2.Location = New System.Drawing.Point(4, 4)
        Me.WindowDockingArea2.Name = "WindowDockingArea2"
        Me.WindowDockingArea2.Owner = Me.UltraDockManager1
        Me.WindowDockingArea2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.WindowDockingArea2.Size = New System.Drawing.Size(439, 40)
        Me.WindowDockingArea2.TabIndex = 0
        '
        'DockableWindow2
        '
        Me.DockableWindow2.Controls.Add(Me.TxtComment)
        Me.DockableWindow2.Location = New System.Drawing.Point(0, 0)
        Me.DockableWindow2.Name = "DockableWindow2"
        Me.DockableWindow2.Owner = Me.UltraDockManager1
        Me.DockableWindow2.Size = New System.Drawing.Size(439, 40)
        Me.DockableWindow2.TabIndex = 136
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(0, 304)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(904, 272)
        Me.TabControl1.TabIndex = 133
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.PanelImagePages)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(896, 246)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Image"
        '
        'TabPage6
        '
        Me.TabPage6.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.TabPage6.Controls.Add(Me.ListBoxNotes)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(896, 246)
        Me.TabPage6.TabIndex = 2
        Me.TabPage6.Text = "Notes"
        '
        'ListBoxNotes
        '
        Me.ListBoxNotes.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ListBoxNotes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBoxNotes.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.ListBoxNotes.Location = New System.Drawing.Point(8, 8)
        Me.ListBoxNotes.Name = "ListBoxNotes"
        Me.ListBoxNotes.Size = New System.Drawing.Size(592, 238)
        Me.ListBoxNotes.TabIndex = 10
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
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.LabError)
        Me.GroupBox2.Controls.Add(Me.UltraGridMisc)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Black
        Me.GroupBox2.Location = New System.Drawing.Point(558, 177)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(346, 121)
        Me.GroupBox2.TabIndex = 134
        Me.GroupBox2.TabStop = False
        '
        'LabError
        '
        Me.LabError.BackColor = System.Drawing.Color.Red
        Me.LabError.ForeColor = System.Drawing.Color.White
        Me.LabError.Location = New System.Drawing.Point(5, 101)
        Me.LabError.Name = "LabError"
        Me.LabError.Size = New System.Drawing.Size(334, 17)
        Me.LabError.TabIndex = 154
        Me.LabError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LabError.Visible = False
        '
        'UltraGridMisc
        '
        Appearance14.ForeColor = System.Drawing.Color.Black
        Me.UltraGridMisc.DisplayLayout.Appearance = Appearance14
        Me.UltraGridMisc.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGridMisc.Location = New System.Drawing.Point(6, 13)
        Me.UltraGridMisc.Name = "UltraGridMisc"
        Me.UltraGridMisc.Size = New System.Drawing.Size(334, 85)
        Me.UltraGridMisc.TabIndex = 0
        '
        'FrmInvOutsideFreight
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(3, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(909, 581)
        Me.Controls.Add(Me._FrmInvBillingAutoHideControl)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBoxDocument)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LabStatus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.LabInvoice)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me._FrmInvBillingUnpinnedTabAreaTop)
        Me.Controls.Add(Me._FrmInvBillingUnpinnedTabAreaBottom)
        Me.Controls.Add(Me._FrmInvBillingUnpinnedTabAreaLeft)
        Me.Controls.Add(Me._FrmInvBillingUnpinnedTabAreaRight)
        Me.ForeColor = System.Drawing.Color.Black
        Me.KeyPreview = True
        Me.Name = "FrmInvOutsideFreight"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pay Outside Freight Invoice"
        Me.GroupBoxDocument.ResumeLayout(False)
        Me.GroupBoxDocument.PerformLayout()
        CType(Me.TxtinvDollar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        CType(Me.UltraGridDocs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.comboBoxDocType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelImagePages.ResumeLayout(False)
        CType(Me.PictureBoxMoveToFaxQueue, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxPrinter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxImageRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxImageLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxRotate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDockManager1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.WindowDockingArea1.ResumeLayout(False)
        Me.DockableWindow1.ResumeLayout(False)
        Me.WindowDockingArea2.ResumeLayout(False)
        Me.DockableWindow2.ResumeLayout(False)
        Me.DockableWindow2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.UltraGridMisc, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Load "

    Private Sub FrmInvFreight_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RasterCodecs.Shutdown()
    End Sub

    Private Sub cbVendor_InitializeLayout(sender As Object, e As InitializeLayoutEventArgs) Handles cbVendor.InitializeLayout
        With cbVendor
            .DropDownWidth = 0
            .DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ExtendLastColumn
        End With
    End Sub


    Public Sub ShowMe(ByVal PassInvoice As Integer, ByVal PassImgLo As Integer, ByVal PassDocNbr As Integer, ByVal PassDocType As Integer, ByVal PassInvoiceDate As Long, inRow As Infragistics.Win.UltraWinGrid.UltraGridRow, inVendorTable As DataTable)

        'LabVendor.Text = PassVendor & " - " & PassVendorName
        myRow = inRow

        cbVendor.DataSource = inVendorTable
        cbVendor.ValueMember = inVendorTable.Columns("Vendor").Caption
        cbVendor.DisplayMember = inVendorTable.Columns("Name").Caption
        cbVendor.Text = ""

        Dim ThisInvoiceDate As Date
        If Not PassInvoiceDate = 0 Then
            ThisInvoiceDate = GetDateFromNbr(PassInvoiceDate)
        Else
            ThisInvoiceDate = Now
        End If

        Me.TxtDate.MaxDate = DateAdd(DateInterval.Month, 1, Now)
        Me.TxtDate.MinDate = DateAdd(DateInterval.Year, -1, Now)
        If ThisInvoiceDate < TxtDate.MinDate Then
            TxtDate.MinDate = ThisInvoiceDate
        End If


        With UltraDockManager1
            .PaneFromControl(TxtComment).Close(True)
            .Visible = False
        End With

        Dim Parms As Object

        BuildEmptyDataSetInvoiceCharges()

        If Not PassInvoice = 0 Then
            BuildThisInvoice(PassInvoice, ThisInvoiceDate)
        End If

        Dim Daily_QryData As ADODB.Recordset



        'Daily_QryData = connAS400.Execute("select * from daily.soapdmp where dmven# = " & ApVendor & " and dmref# = " & Quoted(ApReference) & " order by dmline ", Parms, -1)
        'LoadDataSetInvoiceCharges((Daily_QryData))
        UltraGridMisc.DataSource = InvoiceChargesDataSet
        UltraGridMisc.Visible = True

        If Not PassInvoice = 0 Then
            Daily_QryData = connAS400.Execute("select daily.soblfxl3.*, daily.imfoldp.imdir from daily.soblfxl3 left join daily.imfoldp on bximlo = imkey where bxinv# = " & PassInvoice & " order by bxdate, bxkey ", Parms, -1)
            LoadDataSetDocs((Daily_QryData))
            UltraGridDocs.DataSource = DataSetDocs
        Else
            VendorInvoiceImage = PassDocNbr
            VendorInvoiceImageLocation = PassImgLo
        End If

        If PassDocNbr Then
            wrkDocumentNbr = PassDocNbr
            wrkImageLocation = PassImgLo
            wrkDocType = PassDocType
            comboBoxDocType.Text = GetDocType(PassDocType)
            comboBoxDocType.Visible = False
            'Temporary Lead fix

            Dim Folder As String = ""

            If Not PassImgLo = 0 Then
                'need to get folder from imfoldp file
                Dim myImfoldp As New iSeries.ImFoldp
                With myImfoldp
                    .Retrieve(PassImgLo)
                    If Not .found Then
                        ' should already exist but just in case
                        .imKey = PassImgLo
                        .Create()
                    End If
                End With

                Folder = myImfoldp.imDir
                If Not Directory.Exists(Folder) Then
                    Directory.CreateDirectory(Folder)
                End If
            End If

            GetImage(PassDocNbr, True, False, Folder)
            'GetImage(Format(PassDocNbr, "000000000"), True, True, Folder)
            PanelImagePages.Visible = True

            'With PictureBox2
            '    .Image = myFrmDirectshipQueue.PictureBox1.Image.Clone
            '    .PaintProperties = myFrmFreightQueue.PictureBox1.PaintProperties
            '    .ScaleFactor = myFrmFreightQueue.PictureBox1.ScaleFactor
            'End With
            LabPage.Text = 1
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default

        Me.Show()
    End Sub

    Private Sub FrmInvBilling_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Dim MyInt As Integer
        MyInt = Asc(e.KeyChar)
        'If Asc(e.KeyChar) = 43 Or Asc(e.KeyChar) = 13 Then
        If Asc(e.KeyChar) = 13 Then
            System.Windows.Forms.SendKeys.Send("{TAB}")
            e.Handled = True
        End If
        'If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
        '    UpdateDoc()

        'End If
    End Sub

#End Region

#Region "Build this invoice"
    Private Sub BuildThisInvoice(ByVal InInvoice As Integer, ByVal inInvoiceDate As Date)
        LabInvoice.Text = InInvoice

        UltraGridMisc.DataSource = Nothing

        TxtRef.Text = ""
        TxtDate.Value = inInvoiceDate


        Dim mySoInvp As New iSeries.soInvp(InInvoice)


        With mySoInvp

            '' see if paid in soinvl file

            BillToCustomer = .soBlto
            If .soStat = "T" Then
                FreightOnlyInvoice = True
            Else
                FreightOnlyInvoice = False
            End If

            LabEstimated.Text = Format(.soOfrt, "currency")

            Dim InvoiceChargesRow As DataRow = InvoiceChargesTable.NewRow

            InvoiceChargesRow("Desc") = "Outside Freight"
            InvoiceChargesRow("Amount") = .soOfrt
            InvoiceChargesRow("G/L") = Format(604003206, "#### ## ###")
            InvoiceChargesRow("Delete") = "Delete"

            InvoiceChargesTable.Rows.Add(InvoiceChargesRow)

            Dim myMsCustp As New iSeries.msCustp(.soAcc_, False)
            With myMsCustp
                If .found Then
                    LabCustomer.Text = Format(.cmZip) & " - " & .cmName
                Else
                    LabCustomer.Text = "Not Found"
                End If
            End With

        End With
    End Sub

#End Region

#Region "Payment"

    Private Sub CmdPay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdPay.Click


        If Not EditOk() = True Then Exit Sub

        If Not EditReferenceOk(TxtRef.Text) = True Then Exit Sub

        If Not WriteApFiles(TxtRef.Text, TxtDate.Value) Then
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        RaiseEvent PayInvoice(myRow)

        Me.Close()
    End Sub

#End Region

#Region "write Ap"

    Private Function WriteApFiles(ByVal inReference As String, ByVal inInvDate As Date) As Boolean

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            Dim Parms As Object
            Dim Daily_GenericDATA As ADODB.Recordset
            Dim CallGeneric As New ADODB.Command

            ' set paid info in soinvp
            With CallGeneric
                .ActiveConnection = connAS400
                .CommandText = "update daily.soinvl set " &
                "( soofrp )" &
                "= " &
                "(" & TxtinvDollar.Value & ") " &
                "where soinv# = " & Convert.ToInt32(LabInvoice.Text)
                .Prepared = False
                .Execute()
            End With

            Dim Daily_SoHstlUpd As New ADODB.Command()
            With Daily_SoHstlUpd
                Dim note As String = ""
                note = "Outside Frt Pd " & TxtinvDollar.Value & "-" & cbVendor.Value & "-" & Trim(inReference)
                If Len(note) > 80 Then note = Mid(note, 1, 80)

                .ActiveConnection = connAS400
                .CommandText = "insert into daily.sohstl " &
                    "( ihinv#," &
                    " ihdate," &
                    " ihtime," &
                    " ihuser," &
                    " ihsyst," &
                    " ihnote)" &
                    "Values " &
                    "(" & Convert.ToInt32(LabInvoice.Text) & ", " &
                      Format(Now(), "yyyyMMdd") & ", " &
                      Format(Now(), "HHmm") & ", " &
                    Quoted(UserId) & ", " &
                    "' ', " &
                    Quoted(note) & ")"
                .Prepared = False
                .Execute()
            End With

            Dim mySoApDhP As New iSeries.soApDhp
            With mySoApDhP
                .dhVen_ = cbVendor.Value
                .dhRef_ = inReference
                .dhtot_ = TxtinvDollar.Value
                .dhIndt = Format(TxtDate.Value, "yyyyMMdd")
                .dhRcvd = Format(Now, "yyyyMMdd")
                .dhImag = VendorInvoiceImage
                .dhImlo = VendorInvoiceImageLocation
                .dhStat = "R"
                .dhDtpd = 0
                .dhChk_ = 0
                .Create()
            End With

            If Not UltraGridMisc.Rows.Count = 0 Then

                Dim pRow As Infragistics.Win.UltraWinGrid.UltraGridRow = UltraGridMisc.Rows(0)
                Dim NextLine As Integer = 0

                Do Until pRow Is Nothing

                    If Not pRow.Cells("Amount").Value = 0 Then
                        NextLine += 1
                        ' write miscellaneous

                        Dim mySoApDmP As New iSeries.soApDmp
                        With mySoApDmP
                            .dmVen_ = cbVendor.Value
                            .dmRef_ = inReference
                            .dmLine = pRow.Index + 1
                            .dmDesc = pRow.Cells("Desc").Value
                            .dmAmt = pRow.Cells("Amount").Value
                            Dim gl As String = pRow.Cells("G/L").Value
                            gl = gl.Replace(".", "")
                            gl = gl.Replace(" ", "")
                            .dmGL_ = gl
                            .Create()
                        End With

                    End If

                    pRow = pRow.GetSibling(SiblingRow.Next)
                Loop
            End If

        Catch ex As Exception
            MsgBox("Error writing Outside Freight payment.  This is a critical error. Contact Rick Stanley. Error = " & ex.ToString)
        End Try

        '        myFrmFreightQueue.UpdateInvoiceInDataSet(Convert.ToInt32(LabInvoice.Text), LabPayment.Text, Format(Now, "MM/dd/yy"))

        Return True
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Function

#End Region

#Region "Edit"
    Private Function EditOk() As Boolean
        Dim NoError As Boolean = True
        Dim WrkTotal As decimal

        If cbVendor.Value = 0 Then
            MsgBox("Vendor must be selected", MsgBoxStyle.OkOnly, "A/P error")
            Return False
        End If

        Dim Parms As Object
        Dim Daily_GenericDATA As ADODB.Recordset

        If TxtDate.Text > Now Then
            MsgBox("Invalid date.  Can't be greater than today's date.", MsgBoxStyle.OkOnly, "A/P error")
            Return False
        End If

        For Each pRow As Infragistics.Win.UltraWinGrid.UltraGridRow In UltraGridMisc.Rows
            pRow.Cells("G/L").Appearance.BackColor = Color.White
            WrkTotal += decimal.round(pRow.Cells("Amount").Value, 2)
            If Not pRow.Cells("Amount").Value = 0 Then
                ' edit G/L
                Dim gl As String = pRow.Cells("G/L").Value
                gl = gl.Replace(".", "")
                gl = gl.Replace(" ", "")
                If gl.Length < 9 Then
                    gl = gl.PadRight(9, "0")
                End If
                Daily_GenericDATA = connAS400.Execute("select mgldes from parafiles.glpmstcy where mconum = 1 and mdivno = " & gl.Substring(4, 2) & " and maccno = " & gl.Substring(0, 4) & " and msubac = " & gl.Substring(6, 3), Parms, -1)

                If Daily_GenericDATA.EOF Then
                    pRow.Cells("G/L").Appearance.BackColor = Color.Red
                    If NoError Then
                        MsgBox("G/L code not in the G/L master file", MsgBoxStyle.OkOnly, "A/P error")
                    End If
                    NoError = False
                End If

            End If

            pRow = pRow.GetSibling(SiblingRow.Next)
        Next

        WrkTotal = decimal.round(WrkTotal, 2)

        If Not WrkTotal = TxtinvDollar.Value Then
            If Math.Abs(decimal.round(TxtinvDollar.Value - WrkTotal, 2)) < 1 Then
                If MsgBox("Total to be paid is with $1.  Do you want to add a miscellaneous entry to balance this invoice?", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.Yes Then
                    Dim InvoiceChargesRow As DataRow = InvoiceChargesTable.NewRow
                    InvoiceChargesRow("Desc") = "Invoice adjustment"
                    InvoiceChargesRow("Amount") = decimal.round(TxtinvDollar.Value - WrkTotal, 2)
                    InvoiceChargesRow("G/L") = 801003206
                    InvoiceChargesRow("Delete") = "Delete"
                    InvoiceChargesTable.Rows.Add(InvoiceChargesRow)
                End If
            Else
                MsgBox("Total to pay does not match the total of this invoice.", MsgBoxStyle.OkOnly, "A/P error")
            End If
            NoError = False
        End If

        Return NoError

    End Function

    Private Function EditReferenceOk(ByVal InReference As String) As Boolean
        If Len(Trim(InReference)) = 0 Then
            MsgBox("Vendor Reference # must be entered.", MsgBoxStyle.OkOnly, "A/P error")
            Return False
        End If

        If Len(Trim(InReference)) > 10 Then
            MsgBox("Vendor Reference # can not be more than 10 characters long.", MsgBoxStyle.OkOnly, "A/P error")
            Return False
        End If

        Dim Parms As Object

        Dim Daily_QryData As ADODB.Recordset
        Daily_QryData = connAS400.Execute("SELECT Count(*) as RecordCountValue from daily.soapdhp where dhven# = " & cbVendor.Value & " and dhref# = " & Quoted(InReference), Parms, -1)

        If Not Daily_QryData.Fields("Recordcountvalue").Value = 0 Then
            MsgBox("Invoice " & InReference & " already on file for Vendor " & cbVendor.Value, MsgBoxStyle.OkOnly, "A/P error")
            TxtRef.Focus()
            Return False
        End If


        ' see if invoice already on file

        Daily_QryData = connAS400.Execute("SELECT Count(*) as RecordCountValue from parafiles.apppayl1 where apcono = 1 and apvnno =" & cbVendor.Value & " and apinvn = '" & InReference & "'", Parms, -1)

        If Not Daily_QryData.Fields("Recordcountvalue").Value = 0 Then
            MsgBox("Invoice " & InReference & " for Vendor " & cbVendor.Value & " already in A/P file", MsgBoxStyle.OkOnly, "A/P error")
            TxtRef.Focus()
            Return False
        End If

        Return True

    End Function


#End Region

#Region "Additional comments"

    Private Sub LinkLabelComment_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelComment.LinkClicked
        HoldInProgress = False
        TxtComment.Text = ""
        With UltraDockManager1
            .PaneFromControl(TxtComment).Show()
            .Visible = True
            TxtComment.Visible = True
            TxtComment.Focus()
        End With
    End Sub


    Private Sub TxtComment_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtComment.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim Daily_SoHstlUpd As New ADODB.Command()
            With Daily_SoHstlUpd
                .ActiveConnection = connAS400
                .CommandText = "insert into daily.sohstl " &
                "( ihinv#," &
                " ihdate," &
                " ihtime," &
                " ihuser," &
                " ihsyst," &
                " ihnote)" &
                "Values " &
                "(" & LabInvoice.Text & ", " &
                  Format(Now(), "yyyyMMdd") & ", " &
                  Format(Now(), "HHmm") & ", " &
                "'" & UserId & "', " &
                "'F', " &
                Quoted(TxtComment.Text) & ")"
                .Prepared = False
                .Execute()
            End With
            With UltraDockManager1
                .PaneFromControl(TxtComment).Close()
                .Visible = False
            End With
            Me.Cursor = System.Windows.Forms.Cursors.Default

        ElseIf e.KeyChar = Microsoft.VisualBasic.ChrW(27) Then
            With UltraDockManager1
                .PaneFromControl(TxtComment).Close()
                .Visible = False
            End With

        End If

    End Sub


#End Region

#Region "Load Documents for this invoice"
    Public Sub LoadDataSetDocs(ByRef rs As ADODB.Recordset)
        VendorInvoiceImage = 0
        VendorInvoiceImageLocation = 0

        BuildEmptyDataSetDocs()

        'Load the data
        While Not rs.EOF

            Dim DocsRow As DataRow = DocsTable.NewRow
            If rs.Fields("bxtype").Value = 26 Then
                VendorInvoiceImage = rs.Fields("bxkey").Value
                VendorInvoiceImageLocation = rs.Fields("bximlo").Value
            End If
            DocsRow("Key") = rs.Fields("bxkey").Value
            DocsRow("imlo") = rs.Fields("bximlo").Value

            If IsDBNull(rs.Fields("imdir").Value) Then
                DocsRow("Dir") = ""
            Else
                DocsRow("Dir") = rs.Fields("imdir").Value
            End If

            DocsRow("Type#") = rs.Fields("bxtype").Value
            DocsRow("Type") = GetDocType(rs.Fields("bxtype").Value)
            If rs.Fields("bxdata").Value = "Y" Then
                DocsRow("Data") = True
            Else
                DocsRow("Data") = False
            End If
            DocsTable.Rows.Add(DocsRow)
            rs.MoveNext()
        End While

        If VendorInvoiceImage = 0 Then
            MsgBox("There is no image identified as a Vendor Invoice.", MsgBoxStyle.OkOnly, "A/P error")
        End If

    End Sub

    Public Sub BuildEmptyDataSetDocs()
        DataSetDocs.Dispose()
        DocsTable.Dispose()

        DocsTable = Nothing
        DataSetDocs = Nothing

        DocsTable = New DataTable("Docs")
        DataSetDocs = New DataSet()

        GC.Collect()

        ' product table
        Dim colwork As New DataColumn("Key", GetType(Integer))
        DocsTable.Columns.Add(colwork)

        DocsTable.Columns.Add("Dir", GetType(String))
        DocsTable.Columns.Add("imlo", GetType(Integer))

        colwork = New DataColumn("Type", GetType(String))
        DocsTable.Columns.Add(colwork)

        colwork = New DataColumn("Type#", GetType(Integer))
        DocsTable.Columns.Add(colwork)

        colwork = New DataColumn("Data", GetType(Boolean))
        DocsTable.Columns.Add(colwork)
        ' build dataset
        DataSetDocs.Tables.Add(DocsTable)
    End Sub

#End Region

#Region "Display document"


    Private Sub UltraGridDocs_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles UltraGridDocs.MouseUp
        ' If UpdatingDocument Then UpdateDoc()

        ' declare objects to get value from cell and display 
        Dim mouseupUIElement As Infragistics.Win.UIElement
        Dim mouseupCell As Infragistics.Win.UltraWinGrid.UltraGridCell

        ' retrieve the UIElement from the location of the MouseUp 
        mouseupUIElement = UltraGridDocs.DisplayLayout.UIElement.ElementFromPoint(New Point(e.X, e.Y))
        If mouseupUIElement Is Nothing Then Exit Sub

        ' retrieve the Cell from the UIElement 
        mouseupCell = mouseupUIElement.GetContext(GetType(Infragistics.Win.UltraWinGrid.UltraGridCell))

        ' if there is a cell object reference, set to active cell and edit 
        If mouseupCell Is Nothing Then Exit Sub
        mouseupCell.Row.Selected = True
        'DisplayFaxUltraGrid(UltraGridUnassignedFaxes)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If Not mouseupCell.Row.Cells("Key").Value = 0 Then
            DocumentCell = mouseupCell

            comboBoxDocType.Visible = False
            wrkDocumentNbr = mouseupCell.Row.Cells("Key").Value
            wrkImageLocation = mouseupCell.Row.Cells("imlo").Value
            wrkDocType = mouseupCell.Row.Cells("Type#").Value
            CurrentDocRow = mouseupCell.Row.Index
            comboBoxDocType.Text = mouseupCell.Row.Cells("Type").Value
            GetImage(mouseupCell.Row.Cells("Key").Value, True, False, mouseupCell.Row.Cells("Dir").Value)
            'GetImage(Format(mouseupCell.Row.Cells("Key").Value, "000000000"), True, False, mouseupCell.Row.Cells("Dir").Value)
            comboBoxDocType.Visible = True
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub GetImage(ByRef InFile As Long, ByRef InRefreshData As Boolean, ByVal AlreadyHaveDocument As Boolean, ByVal inDir As String)

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        ' Try

        srcFileName = inDir & InFile & ".tif"

        If Not File.Exists(srcFileName) Then
            srcFileName = inDir & Format(InFile, "000000000") & ".tif"
        End If

        LabPages.Text = 1

        Dim codecs As RasterCodecs = New RasterCodecs()
        Dim info As CodecsImageInfo = codecs.GetInformation(srcFileName, True)

        CurrentPageImageFormat = info.Format
        CurrentPagePixelFormat = info.BitsPerPixel
        LabPages.Text = info.TotalPages

        If Not AlreadyHaveDocument Then

            LoadPage(LabPages.Text)


        End If
        'Catch
        'MsgBox("Error opening document", MsgBoxStyle.OkOnly, "")
        'End Try


        If InRefreshData Then
            CmdDeleteDocPage.Visible = True
            PictureBoxMoveToFaxQueue.Visible = True
            Dim Parms As Object
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub LoadPage(ByVal inPage As Int32)

        LabPage.Text = inPage

        Dim PaintProp As RasterPaintProperties

        PaintProp = PictureBox2.PaintProperties
        PaintProp.PaintDisplayMode = RasterPaintDisplayModeFlags.ScaleToGray Or RasterPaintDisplayModeFlags.Bicubic
        PictureBox2.PaintProperties = PaintProp

        Dim RasImage As RasterImage

        RasImage = codecs.Load(srcFileName, 0, 0, inPage, inPage)

        Dim GrayScale As New ImageProcessing.GrayscaleCommand
        GrayScale.BitsPerPixel = 8
        GrayScale.Run(RasImage)

        PictureBox2.Image = RasImage

        PictureBox2.AutoScroll = True
        PanelImagePages.Visible = True
        PictureBox2.ScaleFactor = saveZoom
    End Sub

    Private Sub PictureBox2_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox2.MouseDown
        If e.Button = MouseButtons.Left Then
            PictureBox2.ScaleFactor += 0.1
        Else
            PictureBox2.ScaleFactor -= 0.1
        End If
        saveZoom = PictureBox2.ScaleFactor
    End Sub

    Private Sub PictureBoxImageLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxImageLeft.Click
        If CurrentDocRow = 0 Then
            CurrentDocRow = UltraGridDocs.Rows.Count - 1
        Else
            CurrentDocRow -= 1
        End If
        wrkDocType = UltraGridDocs.Rows(CurrentDocRow).Cells("Type#").Value
        comboBoxDocType.Text = GetDocType(UltraGridDocs.Rows(CurrentDocRow).Cells("Type#").Value)
        GetImage(UltraGridDocs.Rows(CurrentDocRow).Cells("Key").Value, False, False, UltraGridDocs.Rows(CurrentDocRow).Cells("Dir").Value)
        'GetImage(Format(UltraGridDocs.Rows(CurrentDocRow).Cells("Key").Value, "000000000"), False, False, UltraGridDocs.Rows(CurrentDocRow).Cells("Dir").Value)
    End Sub

    Private Sub PictureBoxImageRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxImageRight.Click
        If CurrentDocRow = UltraGridDocs.Rows.Count - 1 Then
            CurrentDocRow = 0
        Else
            CurrentDocRow += 1
        End If
        wrkDocType = UltraGridDocs.Rows(CurrentDocRow).Cells("Type#").Value
        comboBoxDocType.Text = GetDocType(UltraGridDocs.Rows(CurrentDocRow).Cells("Type#").Value)
        GetImage(UltraGridDocs.Rows(CurrentDocRow).Cells("Key").Value, False, False, UltraGridDocs.Rows(CurrentDocRow).Cells("Dir").Value)
        'GetImage(Format(UltraGridDocs.Rows(CurrentDocRow).Cells("Key").Value, "000000000"), False, False, UltraGridDocs.Rows(CurrentDocRow).Cells("Dir").Value)
    End Sub

    Private Sub PictureBoxPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxPrinter.Click

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Try

            CurrentPage = Convert.ToInt32(LabPage.Text)
            PrintDocument1.Print()

        Catch ex As Exception
            MsgBox("Error trying to open the document in routine PictureBoxPrinter_Click " & ex.ToString, MsgBoxStyle.OkOnly, "")
        End Try

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim instance As New RasterImagePrinter
            instance.Print(PictureBox2.Image, 1, e)

        Catch ex As Exception
            MsgBox("error printing", MsgBoxStyle.OkOnly, "")
        End Try
    End Sub

    Private Sub PictureBoxRotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxRotate.Click
        Try
            Dim command As Leadtools.ImageProcessing.RotateCommand = New Leadtools.ImageProcessing.RotateCommand()
            command.Angle = -9000
            command.FillColor = New RasterColor(Color.White)
            command.Flags = Leadtools.ImageProcessing.RotateCommandFlags.Resize
            command.Run(PictureBox2.Image)
            If iSeriesIP = "192.168.0.100" Or iSeriesIP = "PORTS" Then
                codecs.Save(PictureBox2.Image, srcFileName, CurrentPageImageFormat, CurrentPagePixelFormat, 1, 1, 0, CodecsSavePageMode.Replace)
            Else
                MsgBox("A document would be saved here.  Since you are not in the live system this document is not being saved.", MsgBoxStyle.OkOnly, "Notification")
            End If
        Catch
        End Try

    End Sub


    Private Sub UltraGridDocs_InitializeRow(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeRowEventArgs) Handles UltraGridDocs.InitializeRow
        If e.Row.Cells("Data").Value = True Then
            e.Row.Cells("Type").Appearance.Image = ImageList1.Images(10)
            e.Row.Cells("Type").Appearance.ImageHAlign = HAlign.Right
        End If
    End Sub

    Private Sub UltraGridDocs_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridDocs.InitializeLayout
        With UltraGridDocs
            .DisplayLayout.Bands(0).Columns("Key").Hidden = True
            .DisplayLayout.Bands(0).Columns("Dir").Hidden = True
            .DisplayLayout.Bands(0).Columns("Imlo").Hidden = True
            .DisplayLayout.Bands(0).Columns("Type#").Hidden = True
            .DisplayLayout.Bands(0).Columns("Type").Width = 150
            .DisplayLayout.Bands(0).Columns("Data").Hidden = True

            Dim i As Int32
            For i = 0 To .DisplayLayout.Bands(0).Columns.Count - 1
                .DisplayLayout.Bands(0).Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

            ' Set the behaviour of tab keys in the UltraGrid.
            .DisplayLayout.TabNavigation = TabNavigation.NextControlOnLastCell
        End With
    End Sub

    Private Sub ComboBoxDocType_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBoxDocType.SelectionChanged

        If Not comboBoxDocType.Visible Then Exit Sub

        If comboBoxDocType.Value Is Nothing Then Exit Sub

        Dim CallGeneric As New ADODB.Command()
        With CallGeneric
            .ActiveConnection = connAS400
            .CommandText = "update daily.soblfxp set " &
            "( bxtype)" &
            " = " &
            "(" & comboBoxDocType.Value & ") " &
            "where bxkey = " & wrkDocumentNbr
            .Prepared = False
            .Execute()
        End With

        DocumentCell.Row.Cells("Type#").Value = comboBoxDocType.Value
        DocumentCell.Row.Cells("Type").Value = comboBoxDocType.Text

    End Sub

#End Region

#Region "Delete or restore image"
    Private Sub CmdDeleteDocPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDeleteDocPage.Click
        If wrkDocType < 201 Or wrkDocType > 203 Then
            MsgBox("You can only remove Direct Ship document types?", MsgBoxStyle.OkOnly, "Remove page")
            Exit Sub
        End If

        Dim response As Int32
        response = MsgBox("Are you sure you want to permanently delete this document?", MsgBoxStyle.YesNo, "Remove page")
        If response = 6 Then
            DeleteThisDoc()
        End If
    End Sub

    Private Sub DeleteThisDoc()
        Dim CallGeneric As New ADODB.Command()
        With CallGeneric
            .ActiveConnection = connAS400
            .CommandText = "delete from daily.soblfxl3 where bxkey = " & wrkDocumentNbr
            .Prepared = False
            .Execute()

            '.ActiveConnection = connAS400
            '.CommandText = "delete from daily.soaqdol where dokey = " & wrkDocumentNbr
            '.Prepared = False
            '.Execute()
        End With
        'changed 06/30/2020 to not delete the document (it could be being used by ap)
        'Kill(srcFileName)

        Try
            Dim StrExpr As String = "Key = '" & wrkDocumentNbr & "'"
            Dim foundRows As DataRow() = DocsTable.Select(StrExpr)

            If Not (foundRows Is Nothing) Then
                Dim r As DataRow
                For Each r In foundRows
                    r.Delete()
                Next r
            End If
        Catch
        End Try

        comboBoxDocType.Visible = False
        PanelImagePages.Visible = False
        PictureBox2.Image = Nothing
    End Sub

    Private Sub UltraGridDocs_BeforeRowsDeleted(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs) Handles UltraGridDocs.BeforeRowsDeleted
        ' Stop the grid from displaying it's message box.
        e.DisplayPromptMsg = False
    End Sub

    Private Sub PictureBoxMoveToFaxQueue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBoxMoveToFaxQueue.Click
        If wrkDocType < 201 Or wrkDocType > 203 Then
            MsgBox("You can only requeue Direct Ship document types?", MsgBoxStyle.OkOnly, "Remove page")
            Exit Sub
        End If

        Dim response As Int32
        response = MsgBox("Are you sure you want to delete this document from this invoice and place on the Direct Ship scan queue?", MsgBoxStyle.YesNo, "Remove page")
        If response = 6 Then
            If iSeriesIP = "192.168.0.100" Or iSeriesIP = "PORTS" Then
                codecs.Save(PictureBox2.Image, "\\ppc-data\share1\images\box\DirectShip\" & Format(wrkDocumentNbr, "000000000") & ".tif", CurrentPageImageFormat, CurrentPagePixelFormat, 1, 1, 0, CodecsSavePageMode.Append)
            Else
                MsgBox("A document would be saved here.  Since you are not in the live system this document is not being saved.", MsgBoxStyle.OkOnly, "Notification")
            End If

            DeleteThisDoc()
            MsgBox("Document has been placed on the Direct Ship scan queue as " & Format(wrkDocumentNbr, "000000000") & ".tif", MsgBoxStyle.OkOnly, "Restore document")
        End If

    End Sub
#End Region


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab.Text = "Notes" Then
            Static lastInvoice As Integer
            If lastInvoice = LabInvoice.Text Or
            LabInvoice.Text = 0 Then
                Exit Sub
            End If

            lastInvoice = LabInvoice.Text
            Dim Parms As Object
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            Dim Daily_QryData As ADODB.Recordset
            Daily_QryData = connAS400.Execute("select * from daily.sohstl where ihinv# = " & LabInvoice.Text, Parms, -1)
            LoadListBoxNotes((ListBoxNotes), (Daily_QryData))
            Me.Cursor = Windows.Forms.Cursors.Default

        End If
    End Sub

#Region "Miscellaneous Charge"

    Public InvoiceChargesTable As New DataTable("InvoiceCharges")
    Public InvoiceChargesDataSet As New DataSet()

    Public Sub LoadDataSetInvoiceCharges(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetInvoiceCharges()

        'Load the data
        While Not rs.EOF

            Dim InvoiceChargesRow As DataRow = InvoiceChargesTable.NewRow

            InvoiceChargesRow("Desc") = rs.Fields("dmdesc").Value
            InvoiceChargesRow("Amount") = rs.Fields("dmamt").Value
            InvoiceChargesRow("G/L") = Format(rs.Fields("dmgl#").Value, "#### ## ###")
            InvoiceChargesRow("Delete") = "Delete"

            InvoiceChargesTable.Rows.Add(InvoiceChargesRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSetInvoiceCharges()

        InvoiceChargesTable.Columns.Add("Desc", GetType(String))
        InvoiceChargesTable.Columns.Add("Amount", GetType(decimal))
        InvoiceChargesTable.Columns.Add("G/L", GetType(String))
        InvoiceChargesTable.Columns.Add("Delete", GetType(String))

        ' build dataset
        InvoiceChargesDataSet.Tables.Add(InvoiceChargesTable)

    End Sub

    Private Sub UpdateIt()
        Try

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            UpdateCharges()

        Catch ex As Exception
            MsgBox("Error found when saving invoice.  Error is " & ex.ToString, MsgBoxStyle.OkOnly, "")
        End Try

        Me.Close()
    End Sub

    Private Sub UpdateCharges()

        Dim Daily_GenericUpd As New ADODB.Command()
        With Daily_GenericUpd
            .ActiveConnection = connAS400
            .CommandText = "delete from daily.soapdmp " &
            "where dmven# = " & cbVendor.Value & " and dmref# = " & Quoted(TxtRef.Text)
            .Prepared = False
            .Execute()
        End With

        If UltraGridMisc.Rows.Count = 0 Then Exit Sub

        Dim pRow As Infragistics.Win.UltraWinGrid.UltraGridRow = UltraGridMisc.Rows(0)
        Do Until pRow Is Nothing

            Dim gl As String = pRow.Cells("G/L").Value
            gl = gl.Replace(".", "")
            gl = gl.Replace(" ", "")


            Dim mySoapDmp As New iSeries.soApDmp
            With mySoapDmp
                .dmVen_ = cbVendor.Value
                .dmRef_ = TxtRef.Text
                .dmLine = pRow.Index + 1
                .dmDesc = pRow.Cells("Desc").Value
                .dmAmt = pRow.Cells("Amount").Value
                .dmGL_ = gl
            End With

            pRow = pRow.GetSibling(SiblingRow.Next)
        Loop

    End Sub

    Private Function EditAdditionalCharge() As Boolean
        Dim Parms As Object
        Dim ErrorFound As Boolean = False

        If UltraGridMisc.Rows.Count = 0 Then Return False

        Dim pRow As Infragistics.Win.UltraWinGrid.UltraGridRow = UltraGridMisc.Rows(0)
        Do Until pRow Is Nothing
            If Len(Trim(pRow.Cells("Amount").Value)) > 0 Or
            Len(Trim(pRow.Cells("Amount").Value)) > 0 Then

                If pRow.Cells("Amount").Value Is System.DBNull.Value Then
                    pRow.Cells("Amount").Value = 0
                End If

                ' Edit GL
                Dim gl As String = pRow.Cells("G/L").Value
                gl = gl.Replace(".", "")
                gl = gl.Replace(" ", "")
                If gl.Length < 9 Then
                    gl = gl.PadRight(9, "0")
                End If
                ' change sql011txt and run sql011 when the fiscal year changes
                Dim Daily_GenericDATA As ADODB.Recordset
                Daily_GenericDATA = connAS400.Execute("select mgldes from parafiles.glpmstcy where mconum = 1 and mdivno = " & gl.Substring(4, 2) & " and maccno = " & gl.Substring(0, 4) & " and msubac = " & gl.Substring(6, 3), Parms, -1)

                If Daily_GenericDATA.EOF Then
                    UpdateError("Invalid G/L")
                    pRow.Cells("G/L").Appearance.BackColor = Color.Red
                    ErrorFound = True
                Else
                    pRow.Cells("G/L").Appearance.BackColor = Color.White
                End If
            End If


            pRow = pRow.GetSibling(SiblingRow.Next)
        Loop

        Return ErrorFound

    End Function

    Private Sub UpdateError(ByVal inError As String)
        If LabError.Text = "" Then
            LabError.Text = inError
        End If
    End Sub

    Private Sub UltraGridMisc_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridMisc.InitializeLayout

        With UltraGridMisc.DisplayLayout
            '.Bands(0).Columns("WasProduct").Hidden = True
            '.Bands(0).Columns("WasOrdered").Hidden = True
            '.Bands(0).Columns("Oct").Width = 30
            '.Bands(0).Columns("Oct").CellActivation = Infragistics.Win.UltraWinGrid.Activation.Disabled
            '           .Bands(0).Columns("Desc").FieldLen = 25
            .Bands(0).Columns("Desc").Width = 110
            .Bands(0).Columns("Amount").Width = 60
            .Bands(0).Columns("Amount").Format = ".00"
            .Bands(0).Columns("Delete").Width = 45
            .Bands(0).Columns("Delete").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Bands(0).Columns("Delete").Header.Caption = " "
            .Bands(0).Columns("Delete").CellAppearance.BackColor = System.Drawing.Color.LightGray

            .Bands(0).Columns("G/L").MaskInput = "####.##.###"
            .Bands(0).Columns("G/L").MaskDataMode = UltraWinMaskedEdit.MaskMode.Raw
            .Bands(0).Columns("G/L").MaskDisplayMode = UltraWinMaskedEdit.MaskMode.IncludeBoth
            .Bands(0).Columns("G/L").MaskClipMode = UltraWinMaskedEdit.MaskMode.IncludeBoth
            '            .Bands(0).Columns("G/L").PadChar = "0"
            '.Bands(0).Columns("G/L").PromptChar = " "


        End With
    End Sub

    Private Sub UltraGridMisc_BeforeRowsDeleted(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs) Handles UltraGridMisc.BeforeRowsDeleted
        ' Stop the grid from displaying it's message box.
        e.DisplayPromptMsg = False
    End Sub

    Private Sub UltraGridMisc_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles UltraGridMisc.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim InvoiceChargesRow As DataRow = InvoiceChargesTable.NewRow

            InvoiceChargesRow("Desc") = ""
            InvoiceChargesRow("Amount") = 0
            InvoiceChargesRow("G/L") = 0
            InvoiceChargesRow("Delete") = "Delete"
            InvoiceChargesTable.Rows.Add(InvoiceChargesRow)

        End If
    End Sub

    Private Sub UltraGridMisc_ClickCellButton(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridMisc.ClickCellButton
        Dim Daily_GenericUpd As New ADODB.Command()

        Select Case e.Cell.Column.Key
            'Select Case Convert.ToInt64(e.Cell.Column.Index)
            Case Is = "Delete"
                e.Cell.Row.Delete()
                'Dim StrExpr As String = "Product = " & UltraGridMisc.ActiveRow.Cells("Product").Value
                'Dim foundRows As DataRow() = InvoiceChargesDataSet.Tables("InvoiceCharges").Select(StrExpr)

                'If Not (foundRows Is Nothing) Then
                '    Dim r As DataRow
                '    For Each r In foundRows
                '        ' add variable % check

                '        If Not r("WasProduct") = 0 Then
                '            ' delete
                '            'With Daily_GenericUpd
                '            '    .ActiveConnection = connAS400
                '            '    .CommandText = "delete from daily.soprodl " & _
                '            '    "where sdinv# = " & Convert.ToInt32(LabInvoice.Text) & _
                '            '    " and sdpcod = " & r("WasProduct")
                '            '    .Prepared = False
                '            '    .Execute()
                '            'End With
                '        End If

                '        '        UpdateDataSetFromConfirm(r("Invoice"))

                '        Dim wrkNote As String
                '        wrkNote = "Product = " & r("WasProduct") & " deleted"

                '        'If Not r("Product") = r("wasProduct") And Not r("wasProduct") = 0 Then
                '        '    Dim CallQry As New ADODB.Command()
                '        '    CallQry.ActiveConnection = connAS400
                '        '    CallQry.Prepared = False
                '        '    CallQry.CommandText = ("Call object.sor282 ('" _
                '        '    & LabInvoice.Text & "', '" _
                '        '    & Convert.ToString(r("wasProduct")) & _
                '        '    "', 'D')")
                '        '    CallQry.Execute()
                '        'End If
                '        InvoiceChargesTable.Rows.Remove(r)
                '    Next r
                'End If


        End Select

    End Sub

    Private Sub FrmInvAddChg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            UltraGridMisc.UpdateData()
            LabError.Text = ""
            If EditAdditionalCharge() = True Then
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            UpdateIt()
        End If
    End Sub

#End Region

    'Private Sub CmdFlag_Click(sender As System.Object, e As System.EventArgs) Handles CmdFlag.Click
    '    If MsgBox("Do you want to flag this invoice as completely paid without actually entering a payment?", MsgBoxStyle.YesNo, "Confirmation") = MsgBoxResult.No Then Exit Sub

    '    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

    '    Dim Daily_SoHstlUpd As New ADODB.Command()
    '    With Daily_SoHstlUpd
    '        Dim note As String = ""
    '        note = "D.S. manually removed from D.S. queue "
    '        If Len(note) > 80 Then note = Mid(note, 1, 80)

    '        .ActiveConnection = connAS400
    '        .CommandText = "insert into daily.sohstl " & _
    '        "( ihinv#," & _
    '        " ihdate," & _
    '        " ihtime," & _
    '        " ihuser," & _
    '        " ihsyst," & _
    '        " ihnote)" & _
    '        "Values " & _
    '        "(" & Convert.ToInt32(LabInvoice.Text) & ", " & _
    '          Format(Now(), "yyyyMMdd") & ", " & _
    '          Format(Now(), "HHmm") & ", " & _
    '        Quoted(UserId) & ", " & _
    '        "' ', " & _
    '        Quoted(note) & ")"
    '        .Prepared = False
    '        .Execute()
    '    End With

    '    For Each iRow As Infragistics.Win.UltraWinGrid.UltraGridRow In UltraGridItems.Rows
    '        Dim mySoOrddp As New SalesOrder2013.iSeries.soOrddp
    '        With mySoOrddp
    '            .UpdatePaidComplete(Convert.ToInt32(LabInvoice.Text), iRow.Cells("Line").Value, iRow.Cells("Complete").Value)
    '        End With
    '    Next

    '    Me.Cursor = System.Windows.Forms.Cursors.Default

    '    RaiseEvent PayInvoice(myRow)

    '    Me.Close()
    'End Sub


End Class
