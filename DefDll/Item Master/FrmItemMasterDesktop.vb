Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.IO
Imports System.Text.Encoding
Imports System.Text
Imports System.Windows.Forms
Imports System.Threading
Imports System.Drawing
Imports Crownwood.DotNetMagic.Common
Imports Crownwood.DotNetMagic.Docking

Public Class FrmItemMasterDesktop


    'Implements IComparer

    'Private c_cellBadCell As Infragistics.Win.UltraWinGrid.UltraGridCell
    'Private myItem As Long
    Private mySiteRow As Infragistics.Win.UltraWinGrid.UltraGridRow

    ' Public Event ShowInvoice(ByVal OutInvoice As Integer)

    Dim UpdateThread As Thread
    Dim UpdateThreadStart As New ThreadStart(AddressOf QueryDataBase)
    Dim CallDataBindToDataGrid As New MethodInvoker(AddressOf Me.DataBindToDataGrid)

    Dim UpdateThreadValueList As Thread
    Dim UpdateThreadStartValueList As New ThreadStart(AddressOf QueryDataBaseForValueLists)
    Dim Parms As Object

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        GetVendors()
        GetClass()
        GetCategory()

    End Sub

#Region "Screen Handling"


    Private Sub FrmSalesContract_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'If connAS400.State = 1 Then
        '    connAS400.Close()
        'End If

    End Sub

    Private Sub FrmSalesContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If Not connAS400.State = 1 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
        End If

        'Dim myConvertQuantity As New SalesOrder2013.iSeries.ConvertQuantity
        'myConvertQuantity.GetQuantity(1516, "CS", 3)
        'MsgBox(myConvertQuantity.Quantity)
        'MsgBox(myConvertQuantity.Conversion)

        Dim NewDate As Date = DateAdd(DateInterval.Month, -1, Now)

        Me.LabBuilding.Visible = True
        Me.UltraGridSearch.Visible = False
        llrefresh.Enabled = False

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.UltraTabControl1.Tabs(0).Appearance.ForeColor = Drawing.Color.FromArgb(3, 137, 180)

        UpdateThread = New Thread(UpdateThreadStart)
        UpdateThread.IsBackground = True
        UpdateThread.Name = "UpdateThread"
        UpdateThread.Start()

        UpdateThreadValueList = New Thread(UpdateThreadStartValueList)
        UpdateThreadValueList.IsBackground = True
        UpdateThreadValueList.Name = "UpdateThreadValueList"
        UpdateThreadValueList.Start()
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged

    End Sub
#End Region

#Region "Item search "
    Private Sub llrefresh_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llrefresh.LinkClicked
        llrefresh.Enabled = False
        Me.LabBuilding.Visible = True
        Me.UltraGridSearch.Visible = False

        UpdateThread = New Thread(UpdateThreadStart)
        UpdateThread.IsBackground = True
        UpdateThread.Name = "UpdateThread"
        UpdateThread.Start()
    End Sub

    Private Sub CloseMySearch(ByVal tabkey As String)
        UltraTabControl1.Tabs.Remove(UltraTabControl1.Tabs(tabkey))
    End Sub

    Private SearchTable As New DataTable("Search")
    Private DataSetSearch As New DataSet

    Private Sub QueryDataBase()

        Dim connAS400 As New ADODB.Connection
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object
        connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)

        Dim myIn As String = " where imstat in ("

        If cbActive.Checked Then
            myIn &= Quoted("")
        End If

        If cbDeleted.Checked Then
            If Not myIn = " where a.cmdel in (" Then myIn &= ", "
            myIn &= Quoted("D")
        End If

        If Not myIn = " where a.cmdel in (" Then
            myIn &= ") "
        Else
            myIn = ""
        End If

        Dim mySql As String = "SELECT imstat, imitem, imdes1, imstdc, imavgc,  imavgf, imqtoh, imvend, ifnull(vndmnm, '') as vndmnm
        from daily.minvtp 
        left join parafiles.appvnd on imvend = vndnum" & myIn &
        " order by imdes1"

        Daily_QryData = connAS400.Execute(mySql, Parms, -1)
        LoadDataItem(Daily_QryData)

        connAS400.Close()
        Try
            Me.BeginInvoke(CallDataBindToDataGrid)
        Catch
        End Try

    End Sub

    Public Sub DataBindToDataGrid()
        UltraGridSearch.DataSource = DataSetSearch
        Me.LabBuilding.Visible = False
        Me.UltraGridSearch.Visible = True
        Me.UltraTabControl1.Tabs(1).Appearance.ForeColor = Drawing.Color.FromArgb(3, 137, 180)
        llrefresh.Enabled = True

    End Sub

    Public Sub LoadDataItem(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF

            Dim SearchRow As DataRow = SearchTable.NewRow
            SearchRow("Status") = rs.Fields("imstat").Value
            If rs.Fields("imstat").Value = "D" Then SearchRow("Status") = "Deleted"
            SearchRow("Item") = rs.Fields("imitem").Value
            SearchRow("Description") = rs.Fields("imdes1").Value
            SearchRow("Standard Cost") = rs.Fields("imstdc").Value
            SearchRow("Average Cost") = rs.Fields("imavgc").Value
            SearchRow("Average Freight") = rs.Fields("imavgf").Value
            SearchRow("On Hand") = rs.Fields("imqtoh").Value
            SearchRow("Vendor #") = rs.Fields("imvend").Value
            SearchRow("Vendor Name") = rs.Fields("vndmnm").Value
            SearchTable.Rows.Add(SearchRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSet()
        SearchTable.Dispose()

        SearchTable = Nothing
        DataSetSearch = Nothing

        SearchTable = New DataTable("Search")
        DataSetSearch = New DataSet

        GC.Collect()

        SearchTable.Columns.Add("Status", GetType(String))
        SearchTable.Columns.Add("Item", GetType(Integer))
        SearchTable.Columns.Add("Description", GetType(String))
        SearchTable.Columns.Add("Standard Cost", GetType(Decimal))
        SearchTable.Columns.Add("Average Cost", GetType(Decimal))
        SearchTable.Columns.Add("Average Freight", GetType(Decimal))
        SearchTable.Columns.Add("On Hand", GetType(Decimal))
        SearchTable.Columns.Add("Vendor #", GetType(Integer))
        SearchTable.Columns.Add("Vendor Name", GetType(String))

        ' build dataset
        DataSetSearch.Tables.Add(SearchTable)

    End Sub

    Private Sub UltraGridSearch_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridSearch.ClickCellButton

        Select Case e.Cell.Column.Key
            Case Is = "Item"

                Dim myMinvtp As New SharedDll.Iseries.mInvtp(e.Cell.Row.Cells("Item").Value)
                If myMinvtp.found = True Then
                    ShowItem(1, e.Cell.Row.Cells("Item").Value, e.Cell.Row, myMinvtp)
                Else
                    MsgBox("Sorry the Item is not found", MsgBoxStyle.OkOnly, "Error")
                End If

        End Select

    End Sub


    Private Sub UltraGridSearch_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridSearch.InitializeLayout

        AddFilterToGrid(e)

        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

        e.Layout.Appearance.FontData.SizeInPoints = 8

        With UltraGridSearch.DisplayLayout.Bands(0)

            '        .Columns("Date").SortIndicator = SortIndicator.Ascending
            .Columns("Item").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Item").Width = 70

            .Columns("Status").Width = 70
            .Columns("Description").Width = 200
            .Columns("Vendor Name").Width = 200

            .Columns("Standard Cost").Format = "$###,###.000"
            .Columns("Standard Cost").CellAppearance.TextHAlign = HAlign.Right
            .Columns("Average Cost").Format = "$###,###.000"
            .Columns("Average Cost").CellAppearance.TextHAlign = HAlign.Right
            .Columns("Average Freight").Format = "$###,###.000"
            .Columns("Average Freight").CellAppearance.TextHAlign = HAlign.Right
            .Columns("On Hand").Format = "###,###.000"
            .Columns("On Hand").CellAppearance.TextHAlign = HAlign.Right

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

    Private Sub UltraGridSearch_InitializeRow(sender As Object, e As Infragistics.Win.UltraWinGrid.InitializeRowEventArgs) Handles UltraGridSearch.InitializeRow
        If e.Row.Cells("Status").Value = "Deleted" Then
            e.Row.Appearance.ForeColor = System.Drawing.Color.Red
        Else
            e.Row.Appearance.ForeColor = System.Drawing.Color.Black
        End If

    End Sub

    Private Sub UltraGridSearch_MouseEnterElement(ByVal sender As Object, ByVal e As Infragistics.Win.UIElementEventArgs) Handles UltraGridSearch.MouseEnterElement
        ' tool tip over cell 
        Dim acell As UltraGridCell = e.Element.GetContext(
GetType(Infragistics.Win.UltraWinGrid.UltraGridCell))

        If Not acell Is Nothing Then
            Select Case acell.Column.Key
                Case Is = "Item"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click to view item.")
            End Select

            Exit Sub

        End If

        ' tool tip over column header 
        Dim aColumnHeader As Infragistics.Win.UltraWinGrid.ColumnHeader = e.Element.GetContext(
GetType(Infragistics.Win.UltraWinGrid.ColumnHeader))

        If Not aColumnHeader Is Nothing Then

            Select Case aColumnHeader.Column.Key
                Case Is = "Item"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click an item # to view the item")
            End Select
            Exit Sub

        End If

    End Sub

#End Region

#Region "Show Item"
    Private Sub Check_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtItem.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If sender.name = "TxtItem" Then
                NewItem()
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub CmdNewItem_Click(sender As System.Object, e As System.EventArgs) Handles CmdGetItem.Click
        NewItem()
    End Sub

    Private Sub NewItem()
        If TxtItem.Value = 0 Then
            MsgBox("Item # must be entered", MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Dim myMinvtp As New SharedDll.Iseries.mInvtp(TxtItem.Value)
        If myMinvtp.found = True Then
            ShowItem(0, TxtItem.Value, Nothing, myMinvtp)
        Else
            If MsgBox("Item # " & TxtItem.Value & " does not exist do you want to create this item?", MsgBoxStyle.OkOnly, "Confirm") = MsgBoxResult.No Then Exit Sub
            ShowItem(0, TxtItem.Value, Nothing)
        End If

    End Sub

    Private Sub ShowItem(inReturnToTab As Integer, inItem As Integer, inRow As Infragistics.Win.UltraWinGrid.UltraGridRow, Optional inMinvtp As SharedDll.Iseries.mInvtp = Nothing)

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Static KeyCounter As Long
        KeyCounter += 1

        If Not inMinvtp Is Nothing Then
            UltraTabControl1.Tabs.Add("A" & KeyCounter.ToString, "Item " & inMinvtp.ImItem & " " & inMinvtp.imDes1)
        Else
            UltraTabControl1.Tabs.Add("A" & KeyCounter.ToString, "Item " & inItem & " " & "New Item")
        End If


        Dim myucItem As New ucItem("A" & KeyCounter.ToString, inReturnToTab, DSVendorsTable, inItem, inRow, DSClassTable, DSCategoryTable, inMinvtp)
        'myucItemDetails.ShowIt()
        AddHandler myucItem.CloseMe, AddressOf ClosemyucItem

        UltraTabControl1.Tabs("A" & KeyCounter.ToString).TabPage.Controls.Add(myucItem)
        UltraTabControl1.Tabs("A" & KeyCounter.ToString).Selected = True
        UltraTabControl1.Tabs("A" & KeyCounter.ToString).TabPage.AutoScroll = True
        'AddHandler myucItemDetails.ShowSiteDate, AddressOf ShowSiteDate

        myucItem.Size = UltraTabControl1.Size
        myucItem.Anchor = 15

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ClosemyucItem(TabKey As String, ReturnToTab As Integer)
        'MsgBox("Handles before Dispose " & Process.GetCurrentProcess.HandleCount)
        For Each childCtl As Windows.Forms.Control In UltraTabControl1.Tabs(TabKey).TabPage.Controls
            DisposeOfAllControls(childCtl)
        Next
        UltraTabControl1.Tabs.Remove(UltraTabControl1.Tabs(TabKey))
        GC.Collect()
        'MsgBox("Handles After Dispose " & Process.GetCurrentProcess.HandleCount)
        UltraTabControl1.Tabs(ReturnToTab).Selected = True

    End Sub

#End Region

#Region "Show Warranty Search"
    Private Sub ShowWarantySearch(inSerial As String, inItem As String)

        'If Trim(inSerial) = "" And Trim(inItem) = "" Then
        '    MsgBox("You have to enter a serial # or an item # or both to do a search", MsgBoxStyle.OkOnly, "Error")
        '    Exit Sub
        'End If

        'Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        'Static KeyCounter As Long
        'KeyCounter += 1

        'UltraTabControl1.Tabs.Add("S" & KeyCounter.ToString, "Serial - " & inSerial & " Item - " & inItem)

        'Dim myucWarrantyInquiry As New ucWarrantyInquiry("S" & KeyCounter.ToString, inSerial, inItem, UltraTabControl1.Tabs.Count - 1)
        'myucWarrantyInquiry.ShowIt()

        'UltraTabControl1.Tabs("S" & KeyCounter.ToString).TabPage.Controls.Add(myucWarrantyInquiry)
        'UltraTabControl1.Tabs("S" & KeyCounter.ToString).Selected = True
        'AddHandler myucWarrantyInquiry.CloseMe, AddressOf ClosemyucItem
        'AddHandler myucWarrantyInquiry.ShowItem, AddressOf ShowItem

        'myucWarrantyInquiry.Size = UltraTabControl1.Size
        'myucWarrantyInquiry.Anchor = 15

        'Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

#End Region

    '#Region "Item Profile"
    '    Private Sub ShowItemProfile(InAccount As Long)

    '        If dockingManager Is Nothing Then dockingManager = New DockingManager(Me, VisualStyle.IDE2005)
    '        'dockingManager.Contents.Clear()
    '        If Not dockingManager.Contents("Item Inquiry") Is Nothing Then dockingManager.Contents.Remove(dockingManager.Contents("Item Inquiry"))

    '        myucItemInquiry = Nothing

    '        If myucItemInquiry Is Nothing Then
    '            myucItemInquiry = New ucItemInquiry(InAccount)
    '        End If

    '        With myucItemInquiry

    '            If dockingManager.Contents("Item Inquiry") Is Nothing Then

    '                'AddHandler myThumbRightClickMenu.OptionClicked, AddressOf OptionClicked
    '                FloatingMenu = dockingManager.Contents.Add(myucItemInquiry, "Item Inquiry")
    '                FloatingMenu.FloatingSize = New System.Drawing.Size(.Width + 20, .Height + 20)

    '                'If Cursor.Position.Y + FloatingMenu.FloatingSize.Height >  Then
    '                'FloatingMenu.DisplayLocation = New System.Drawing.Point(e.X, e.Y)
    '                FloatingMenu.DisplayLocation = New System.Drawing.Point(20, 20)
    '                FloatingMenu.CloseButton = False
    '                FloatingMenu.HideButton = False
    '                FloatingMenu.CaptionBar = False
    '                ' AddHandler dockingManager.ContentHiding, AddressOf ControlWasClosed

    '                Dim wc As WindowContent = dockingManager.AddContentWithState(FloatingMenu, Crownwood.DotNetMagic.Docking.State.Floating)
    '                dockingManager.AllowFloating = True
    '                dockingManager.BackColor = Color.SteelBlue
    '                dockingManager.InactiveTextColor = Color.White
    '                dockingManager.ActiveColor = Color.SteelBlue
    '                dockingManager.ActiveTextColor = Color.White
    '                dockingManager.ResizeBarColor = Color.SteelBlue
    '                '                dockingManager.InnerControl = myThumbRightClickMenu

    '            End If
    '            ' myucChangeItem.ShowIt()
    '            dockingManager.ShowContent(dockingManager.Contents("Item Inquiry"))
    '        End With
    '    End Sub

    '    Private Sub myucItemInquiry_CloseMe() Handles myucItemInquiry.CloseMe
    '        dockingManager.HideContent(dockingManager.Contents("Item Inquiry"))
    '    End Sub
    '#End Region

    '#Region "Show Tanks"
    '    Private Sub ShowTanks(InAccount As Long, inName As String)
    '        If dockingManager Is Nothing Then dockingManager = New DockingManager(Me, VisualStyle.IDE2005)
    '        'dockingManager.Contents.Clear()
    '        If Not dockingManager.Contents("Tanks") Is Nothing Then dockingManager.Contents.Remove(dockingManager.Contents("Tanks"))

    '        Dim myItemTanks As New ItemMaintenance.ucItemTanks(InAccount, inName, UserId, UserName, UserPassword)

    '        With myItemTanks


    '            If dockingManager.Contents("Tanks") Is Nothing Then

    '                'AddHandler myThumbRightClickMenu.OptionClicked, AddressOf OptionClicked
    '                FloatingMenu = dockingManager.Contents.Add(myItemTanks, "Tanks")
    '                FloatingMenu.FloatingSize = New System.Drawing.Size(.Width + 20, .Height + 50)

    '                'If Cursor.Position.Y + FloatingMenu.FloatingSize.Height >  Then
    '                'FloatingMenu.DisplayLocation = New System.Drawing.Point(e.X, e.Y)
    '                FloatingMenu.DisplayLocation = New System.Drawing.Point(20, 20)
    '                FloatingMenu.CloseButton = False
    '                FloatingMenu.HideButton = False
    '                FloatingMenu.CaptionBar = False
    '                AddHandler myItemTanks.CloseMe, AddressOf TanksWasClosed
    '                AddHandler myItemTanks.PassBackFound, AddressOf PassBackTanksFound

    '                Dim wc As WindowContent = dockingManager.AddContentWithState(FloatingMenu, Crownwood.DotNetMagic.Docking.State.Floating)
    '                dockingManager.AllowFloating = True
    '                dockingManager.BackColor = Color.SteelBlue
    '                dockingManager.InactiveTextColor = Color.White
    '                dockingManager.ActiveColor = Color.SteelBlue
    '                dockingManager.ActiveTextColor = Color.White
    '                dockingManager.ResizeBarColor = Color.SteelBlue
    '                '                dockingManager.InnerControl = myThumbRightClickMenu

    '            End If
    '            ' myucChangeItem.ShowIt()
    '            dockingManager.ShowContent(dockingManager.Contents("Tanks"))
    '        End With
    '    End Sub

    '    Private Sub TanksWasClosed()
    '        dockingManager.HideContent(dockingManager.Contents("Tanks"))
    '    End Sub

    '    Private Sub PassBackTanksFound(inTanksFound As Boolean)
    '        dockingManager.HideContent(dockingManager.Contents("Tanks"))
    '    End Sub

    '    Private Sub CmdWarrantySearch_Click(sender As Object, e As EventArgs)
    '        ShowWarantySearch(TxtWarranty.Text, TxtItem.Text)
    '    End Sub


    '#End Region

    Dim DSVendorsTable As DataTable
    Private Sub GetVendors()
        ' Item Master Vendors
        Dim rs As ADODB.Recordset
        rs = connAS400.Execute("SELECT vvvnd#, vndmnm FROM daily.vespecp " &
        "left join parafiles.appvnd on vvvnd# = vndnum " &
        "WHERE vvspec = 'IT' order by vndmnm", Parms, -1)

        DSVendorsTable = New DataTable("DSVendorsTable")
        DSVendorsTable.Columns.Add("Vendor", GetType(Integer))
        DSVendorsTable.Columns.Add("Name", GetType(String))

        While Not rs.EOF
            Dim DSVendorsRow As DataRow = DSVendorsTable.NewRow
            DSVendorsRow("Vendor") = rs.Fields("vvvnd#").Value
            DSVendorsRow("Name") = rs.Fields("vndmnm").Value
            DSVendorsTable.Rows.Add(DSVendorsRow)
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing
    End Sub

    Dim DSClassTable As DataTable
    Private Sub GetClass()
        ' Item Master Vendors
        Dim rs As ADODB.Recordset
        rs = connAS400.Execute("SELECT tatkey, tadesc FROM daily.potablp 
        WHERE tatabl  = 'ICL' order by tadesc", Parms, -1)

        DSClassTable = New DataTable("DSClassTable")
        DSClassTable.Columns.Add("Class", GetType(String))
        DSClassTable.Columns.Add("Description", GetType(String))

        Dim DSClassRow As DataRow = DSClassTable.NewRow
        DSClassRow("Class") = ""
        DSClassRow("Description") = "Not Selected"
        DSClassTable.Rows.Add(DSClassRow)

        While Not rs.EOF
            DSClassRow = DSClassTable.NewRow
            DSClassRow("Class") = rs.Fields("tatkey").Value
            DSClassRow("Description") = rs.Fields("tatkey").Value & "-" & rs.Fields("tadesc").Value
            DSClassTable.Rows.Add(DSClassRow)
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing
    End Sub

    Dim DSCategoryTable As DataTable
    Private Sub GetCategory()
        ' Item Master Vendors
        Dim rs As ADODB.Recordset
        rs = connAS400.Execute("SELECT tatkey, tadesc FROM daily.potablp 
        WHERE tatabl  = 'ICA' order by tadesc", Parms, -1)

        DSCategoryTable = New DataTable("DSCategoryTable")
        DSCategoryTable.Columns.Add("Category", GetType(String))
        DSCategoryTable.Columns.Add("Description", GetType(String))

        Dim DSCategoryRow As DataRow = DSCategoryTable.NewRow
        DSCategoryRow("Category") = ""
        DSCategoryRow("Description") = "Not Selected"
        DSCategoryTable.Rows.Add(DSCategoryRow)

        While Not rs.EOF
            DSCategoryRow = DSCategoryTable.NewRow
            DSCategoryRow("Category") = rs.Fields("tatkey").Value
            DSCategoryRow("Description") = rs.Fields("tatkey").Value & "-" & rs.Fields("tadesc").Value
            DSCategoryTable.Rows.Add(DSCategoryRow)
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing
    End Sub


End Class
