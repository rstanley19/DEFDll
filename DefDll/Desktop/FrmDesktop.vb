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

Public Class FrmDesktop


    'Implements IComparer

    'Private c_cellBadCell As Infragistics.Win.UltraWinGrid.UltraGridCell
    'Private myCustomer As Long
    Private mySiteRow As Infragistics.Win.UltraWinGrid.UltraGridRow

    ' Public Event ShowInvoice(ByVal OutInvoice As Integer)

    Dim UpdateThread As Thread
    Dim UpdateThreadStart As New ThreadStart(AddressOf QueryDataBase)
    Dim CallDataBindToDataGrid As New MethodInvoker(AddressOf Me.DataBindToDataGrid)

    Dim UpdateThreadValueList As Thread
    Dim UpdateThreadStartValueList As New ThreadStart(AddressOf QueryDataBaseForValueLists)

    Protected dockingManager As Crownwood.DotNetMagic.Docking.DockingManager
    Private FloatingMenu As Content
    Private WithEvents myucAgingInquiry As SalesOrder2013.ucAgingInquiry
    Private WithEvents myucCustomerInquiry As ucCustomerInquiry

    Public Sub New(inUserId As String, inUserName As String, inUserPassword As String, inSmtp As String, inPassApplicationProductNameToDLL As String, inPassApplicationStartupPathToDLL As String, inPrivateUserFolder As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        UserId = inUserId
        UserPassword = inUserPassword
        UserName = inUserName
        myPrivateUserFolder = inPrivateUserFolder

        If connAS400.State = 0 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iseriesip & ";", UserId, UserPassword)
        End If

        PassApplicationProductNameToDLL = inPassApplicationProductNameToDLL
        PassApplicationStartupPathToDLL = inPassApplicationStartupPathToDLL

        myBinAppSettings = Nothing
        myBinAppSettings = New BinAppSettings

        smtp = inSmtp

        Dim mySetHandshake As New SalesOrder2013.Class1
        mySetHandshake.SetHandShake(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder, iSeriesIP)

        Dim mymsSlmp As New SalesOrder2013.iSeries.msSlmp(inUserId)
        With mymsSlmp
            If .found Then
                SalesmanInitials = .slInit
                SalesmanNumber = .slSlm_
            Else
                SalesmanInitials = ""
                SalesmanNumber = 0
            End If
        End With

        Atalasoft.Imaging.Codec.RegisteredDecoders.Decoders.Add(New Atalasoft.Imaging.Codec.Pdf.PdfDecoder)
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
            connAS400.Open("Provider=IBMDA400;Data Source=" & iseriesip & ";", UserId, UserPassword)
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

    Private Sub Check_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TxtShipTo.KeyPress, TxtInvoice.KeyPress, TxtWarranty.KeyPress, TxtItem.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            If sender.name = "TxtInvoice" Then
                ExistingOrdersGo()
            ElseIf sender.name = "TxtShipTo" Then
                NewOrder()
            ElseIf sender.name = "TxtWarranty" Or sender.name = "TxtItem" Then
                ShowWarantySearch(TxtWarranty.Text, TxtItem.Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub CmdExistingOrdersGo_Click(sender As System.Object, e As System.EventArgs) Handles CmdExistingOrdersGo.Click
        ExistingOrdersGo()
    End Sub

    Private Sub ExistingOrdersGo()
        Dim mySoInvp As New SalesOrder2013.iSeries.soInvp(TxtInvoice.Value)
        If mySoInvp.found = False Then
            MsgBox("Invoice # not on file", MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Dim myMscustp As New iSeries.msCustp(mySoInvp.soAcc_, False)

        ShowCustomer(myMscustp, mySoInvp.soInv_, 0)
    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl1.ActiveTabChanged

    End Sub
#End Region

#Region "Customer search "


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
        connAS400.Open("Provider=IBMDA400;Data Source=" & iseriesip & ";", UserId, UserPassword)

        Dim myIn As String = " where a.cmdel in ("

        If cbActive.Checked Then
            myIn &= Quoted("")
        End If

        If cbDeleted.Checked Then
            If Not myIn = " where a.cmdel in (" Then myIn &= ", "
            myIn &= Quoted("D")
        End If

        If cbQuoteOnly.Checked Then
            If Not myIn = " where a.cmdel in (" Then myIn &= ", "
            myIn &= Quoted("Q")
        End If

        If cbCreditHold.Checked Then
            If Not myIn = " where a.cmdel in (" Then myIn &= ", "
            myIn &= Quoted("C")
        End If

        If Not myIn = " where a.cmdel in (" Then
            myIn &= ") "
        Else
            myIn = ""
        End If

        Dim mySql As String = "SELECT a.cmclrk as Clerk, a.cmname as Customer, a.cmcity as CustomerCity, trim(a.cmadr1) || ', ' || trim(a.cmadr2) as CustomerAddress, " &
                        "a.cmtel# as CustomerTelephone, a.cmacc# as Account, a.cmzip as CustomerZip, " &
                        "a.cmctyp as CustomerType, a.cmdel, a.cmst as CustomerState, b.cmacc# as BillTo, b.cmname as BillToName, ifnull(n.profile, 0) as profile, kfacc#, " &
                        "ifnull(s.slslnm, 'Unknown') as FuelSalesman, ifnull(sd.slslnm, 'Unknown') as DefSalesman " &
                        "from daily.mscustp as a left join daily.mscustp as b on a.cmblto = b.cmacc# " &
                        "left join (select count(*) as profile, snkey from daily.sonotep " &
                        "where sntype in ('CustPro', 'CustNote') group by snkey) as n on snkey = a.cmacc# " &
                        "left join daily.mskeepl on kfacc# = a.cmacc# " &
                        "left join daily.msslmp as s on a.cmslm# = s.slslm# " &
                        "left join daily.msslmp as sd on a.cmslmd = sd.slslm# " & myIn &
                        "order by a.cmacc#"

        Daily_QryData = connAS400.Execute(mySql, Parms, -1)
        LoadDataWholesale(Daily_QryData)

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

    Public Sub LoadDataWholesale(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF

            Dim SearchRow As DataRow = SearchTable.NewRow
            SearchRow("Account") = rs.Fields("Account").Value
            SearchRow("Name") = rs.Fields("Customer").Value
            SearchRow("City") = rs.Fields("CustomerCity").Value
            SearchRow("St") = rs.Fields("CustomerState").Value
            SearchRow("Zip") = rs.Fields("CustomerZip").Value
            SearchRow("Address") = rs.Fields("CustomerAddress").Value
            SearchRow("Phone") = rs.Fields("CustomerTelephone").Value
            Select Case rs.Fields("CustomerType").Value
                Case Is = "P"
                    SearchRow("Type") = "Wholesale"
                Case Is = "H"
                    SearchRow("Type") = "Home Heat"
                Case Is = "B"
                    SearchRow("Type") = "Commercial"
                Case Else
                    SearchRow("Type") = "Unknown"
            End Select

            SearchRow("Bill To") = rs.Fields("BillTo").Value
            SearchRow("Bill TO Name") = rs.Fields("BillToName").Value

            If rs.Fields("cmdel").Value = "D" Then
                SearchRow("Deleted") = "Deleted"
            ElseIf rs.Fields("cmdel").Value = "Q" Then
                SearchRow("Deleted") = "Quote Only"
            ElseIf rs.Fields("cmdel").Value = "C" Then
                SearchRow("Deleted") = "Credit Hold"
            Else
                SearchRow("Deleted") = ""
            End If

            If rs.Fields("Profile").Value > 0 Then
                SearchRow("Profile") = True
            Else
                SearchRow("Profile") = False
            End If

            SearchRow("Fuel") = rs.Fields("FuelSalesman").Value
            SearchRow("Def") = rs.Fields("DefSalesman").Value
            SearchRow("Clerk") = rs.Fields("Clerk").Value
            SearchRow("Tanks") = "Tanks"
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

        SearchTable.Columns.Add("Account", GetType(Integer))
        SearchTable.Columns.Add("Bill To", GetType(Integer))
        SearchTable.Columns.Add("Type", GetType(String))
        SearchTable.Columns.Add("Name", GetType(String))
        SearchTable.Columns.Add("City", GetType(String))
        SearchTable.Columns.Add("St", GetType(String))
        SearchTable.Columns.Add("Zip", GetType(Integer))
        SearchTable.Columns.Add("Address", GetType(String))
        SearchTable.Columns.Add("Phone", GetType(Int64))
        SearchTable.Columns.Add("Bill To Name", GetType(String))
        SearchTable.Columns.Add("Fuel", GetType(String))
        SearchTable.Columns.Add("Def", GetType(String))
        SearchTable.Columns.Add("Deleted", GetType(String))
        SearchTable.Columns.Add("Tanks", GetType(String))
        SearchTable.Columns.Add("Clerk", GetType(String))
        SearchTable.Columns.Add("Profile", GetType(Integer))

        ' build dataset
        DataSetSearch.Tables.Add(SearchTable)

    End Sub

    Private Sub UltraGridSearch_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridSearch.ClickCellButton

        Select Case e.Cell.Column.Key
            Case Is = "Account"
                If e.Cell.Row.Cells("Deleted").Value = "Y" Then
                    MsgBox("Sorry the customer has been deleted.  You can't place a new sales order for this customer.", MsgBoxStyle.OkOnly, "Error")
                    Exit Sub
                End If

                Dim myMscustp As New iSeries.msCustp(e.Cell.Row.Cells("Account").Value, False)
                If myMscustp.found = True Then
                    ShowCustomer(myMscustp, 0, 1)
                Else
                    MsgBox("Sorry the customer is not found", MsgBoxStyle.OkOnly, "Error")
                End If
            Case Is = "Tanks"
                ShowTanks(e.Cell.Row.Cells("Account").Value, e.Cell.Row.Cells("Name").Value)
            Case Is = "Name"
                ShowCustomerProfile(e.Cell.Row.Cells("Account").Value)
        End Select

    End Sub


    Private Sub UltraGridSearch_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridSearch.InitializeLayout

        ' FILTER ROW FUNCTIONALITY RELATED ULTRAGRID SETTINGS
        ' ----------------------------------------------------------------------------------
        ' Enable the the filter row user interface by setting the FilterUIType to FilterRow.
        e.Layout.Override.FilterUIType = FilterUIType.FilterRow

        ' FilterEvaluationTrigger specifies when UltraGrid applies the filter criteria typed 
        ' into a filter row. Default is OnCellValueChange which will cause the UltraGrid to
        ' re-filter the data as soon as the user modifies the value of a filter cell.
        e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange

        ' By default the UltraGrid selects the type of the filter operand editor based on
        ' the column's DataType. For DateTime and boolean columns it uses the column's editors.
        ' For other column types it uses the Combo. You can explicitly specify the operand
        ' editor style by setting the FilterOperandStyle on the override or the individual
        ' columns.
        'e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;

        ' By default UltraGrid displays user interface for selecting the filter operator. 
        ' You can set the FilterOperatorLocation to hide this user interface. This
        ' property is available on column as well so it can be controlled on a per column
        ' basis. Default is WithOperand. This property is exposed off the column as well.
        e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand

        ' By default the UltraGrid uses StartsWith as the filter operator. You use
        ' the FilterOperatorDefaultValue property to specify a different filter operator
        ' to use. This is the default or the initial filter operator value of the cells
        ' in filter row. If filter operator user interface is enabled (FilterOperatorLocation
        ' is not set to None) then that ui will be initialized to the value of this
        ' property. The user can then change the operator as he/she chooses via the operator
        ' drop down.
        e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.Contains

        ' FilterOperatorDropDownItems property can be used to control the options provided
        ' to the user for selecting the filter operator. By default UltraGrid bases 
        ' what operator options to provide on the column's data type. This property is
        ' avaibale on the column as well.
        'e.Layout.Override.FilterOperatorDropDownItems = FilterOperatorDropDownItems.All;

        ' By default UltraGrid displays a clear button in each cell of the filter row
        ' as well as in the row selector of the filter row. When the user clicks this
        ' button the associated filter criteria is cleared. You can use the 
        ' FilterClearButtonLocation property to control if and where the filter clear
        ' buttons are displayed.
        e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell

        ' Appearance of the filter row can be controlled using the FilterRowAppearance proeprty.
        'e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow

        ' You can use the FilterRowPrompt to display a prompt in the filter row. By default
        ' UltraGrid does not display any prompt in the filter row.
        'e.Layout.Override.FilterRowPrompt = "Click here to filter data..."

        ' You can use the FilterRowPromptAppearance to change the appearance of the prompt.
        ' By default the prompt is transparent and uses the same fore color as the filter row.
        ' You can make it non-transparent by setting the appearance' BackColorAlpha property 
        ' or by setting the BackColor to a desired value.
        ' e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Alpha.Opaque

        ' By default the prompt is spread across multiple cells if it's bigger than the
        ' first cell. You can confine the prompt to a particular cell by setting the
        ' SpecialRowPromptField property off the band to the key of a column.
        'e.Layout.Bands[0].SpecialRowPromptField = e.Layout.Bands[0].Columns[0].Key;

        ' Display a separator between the filter row other rows. SpecialRowSeparator property 
        ' can be used to display separators between various 'special' rows, including for the
        ' filter row. This property is a flagged enum property so it can take multiple values.
        e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow

        ' You can control the appearance of the separator using the SpecialRowSeparatorAppearance
        ' property.
        ' e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(233, 242, 199)
        ' ----------------------------------------------------------------------------------

        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

        e.Layout.Appearance.FontData.SizeInPoints = 8

        With UltraGridSearch.DisplayLayout.Bands(0)

            '        .Columns("Date").SortIndicator = SortIndicator.Ascending
            .Columns("Account").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button

            .Columns("Tanks").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Name").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button

            .Columns("Account").Width = 70
            .Columns("Bill To").Width = 70

            .Columns("Type").Width = 75
            .Columns("Name").Width = 200
            .Columns("Tanks").Width = 50
            .Columns("Bill To Name").Width = 200
            .Columns("Address").Width = 200
            .Columns("City").Width = 75
            .Columns("St").Width = 30
            .Columns("Phone").Width = 100
            .Columns("Phone").Format = "(###)###-####"
            .Columns("Deleted").Width = 65
            .Columns("Profile").Hidden = True

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

    Private Sub UltraGridSearch_InitializeRow(sender As Object, e As Infragistics.Win.UltraWinGrid.InitializeRowEventArgs) Handles UltraGridSearch.InitializeRow
        If e.Row.Cells("Deleted").Value = "Deleted" Then
            e.Row.Appearance.ForeColor = System.Drawing.Color.Red
        ElseIf e.Row.Cells("Deleted").Value = "Quote Only" Then
            e.Row.Appearance.ForeColor = System.Drawing.Color.Purple
        Else
            e.Row.Appearance.ForeColor = System.Drawing.Color.Black
        End If
        If e.Row.Cells("Profile").Value = 0 Then
            e.Row.Cells("Name").Appearance.ForeColor = System.Drawing.Color.Black
        Else
            e.Row.Cells("Name").Appearance.ForeColor = System.Drawing.Color.Green
        End If

    End Sub

    Private Sub UltraGridSearch_MouseEnterElement(ByVal sender As Object, ByVal e As Infragistics.Win.UIElementEventArgs) Handles UltraGridSearch.MouseEnterElement
        ' tool tip over cell 
        Dim acell As UltraGridCell = e.Element.GetContext(
GetType(Infragistics.Win.UltraWinGrid.UltraGridCell))

        If Not acell Is Nothing Then
            Select Case acell.Column.Key
                Case Is = "Account"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click to view customer.")
                Case Is = "Invoices"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click to view invoices for this account.")
                Case Is = "Aging"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click to view Aging for the bill to.")
                Case Is = "Name"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click to view the customer profile.")
            End Select

            Exit Sub

        End If

        ' tool tip over column header 
        Dim aColumnHeader As Infragistics.Win.UltraWinGrid.ColumnHeader = e.Element.GetContext(
GetType(Infragistics.Win.UltraWinGrid.ColumnHeader))

        If Not aColumnHeader Is Nothing Then

            Select Case aColumnHeader.Column.Key
                Case Is = "Account"
                    ToolTip1.Active = True
                    ToolTip1.SetToolTip(sender, "Click an account to view the customer")
            End Select
            Exit Sub

        End If

    End Sub

#End Region

#Region "Show Customer"
    Private Sub CmdNewOrder_Click(sender As System.Object, e As System.EventArgs) Handles CmdNewOrder.Click
        NewOrder()
    End Sub

    Private Sub NewOrder()
        Dim myMscustp As New iSeries.msCustp(TxtShipTo.Value, False)
        If myMscustp.found = True Then
            ShowCustomer(myMscustp, 0, 0)
        Else
            MsgBox("Sorry the customer is not found", MsgBoxStyle.OkOnly, "Error")
        End If

    End Sub

    Private Sub ShowCustomer(inMscustp As iSeries.msCustp, inOrderNbr As Integer, inReturnToTab As Integer)

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Static KeyCounter As Long
        KeyCounter += 1


        UltraTabControl1.Tabs.Add("A" & KeyCounter.ToString, "Account " & inMscustp.cmAcc_ & " " & inMscustp.cmName)

        Dim myucCustomer As New ucCustomer("A" & KeyCounter.ToString, inMscustp, inReturnToTab)
        'myucOrderDetails.ShowIt()
        AddHandler myucCustomer.CloseMe, AddressOf ClosemyucCustomer

        UltraTabControl1.Tabs("A" & KeyCounter.ToString).TabPage.Controls.Add(myucCustomer)
        UltraTabControl1.Tabs("A" & KeyCounter.ToString).Selected = True
        UltraTabControl1.Tabs("A" & KeyCounter.ToString).TabPage.AutoScroll = True
        'AddHandler myucOrderDetails.ShowSiteDate, AddressOf ShowSiteDate

        myucCustomer.Size = UltraTabControl1.Size
        myucCustomer.Anchor = 15

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ClosemyucCustomer(TabKey As String, ReturnToTab As Integer)
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

        If Trim(inSerial) = "" And Trim(inItem) = "" Then
            MsgBox("You have to enter a serial # or an item # or both to do a search", MsgBoxStyle.OkOnly, "Error")
            Exit Sub
        End If

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Static KeyCounter As Long
        KeyCounter += 1

        UltraTabControl1.Tabs.Add("S" & KeyCounter.ToString, "Serial - " & inSerial & " Item - " & inItem)

        Dim myucWarrantyInquiry As New ucWarrantyInquiry("S" & KeyCounter.ToString, inSerial, inItem, UltraTabControl1.Tabs.Count - 1)
        myucWarrantyInquiry.ShowIt()

        UltraTabControl1.Tabs("S" & KeyCounter.ToString).TabPage.Controls.Add(myucWarrantyInquiry)
        UltraTabControl1.Tabs("S" & KeyCounter.ToString).Selected = True
        AddHandler myucWarrantyInquiry.CloseMe, AddressOf ClosemyucCustomer
        AddHandler myucWarrantyInquiry.ShowCustomer, AddressOf ShowCustomer

        myucWarrantyInquiry.Size = UltraTabControl1.Size
        myucWarrantyInquiry.Anchor = 15

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

#End Region

#Region "Customer Profile"
    Private Sub ShowCustomerProfile(InAccount As Long)

        If dockingManager Is Nothing Then dockingManager = New DockingManager(Me, VisualStyle.IDE2005)
        'dockingManager.Contents.Clear()
        If Not dockingManager.Contents("Customer Inquiry") Is Nothing Then dockingManager.Contents.Remove(dockingManager.Contents("Customer Inquiry"))

        myucCustomerInquiry = Nothing

        If myucCustomerInquiry Is Nothing Then
            myucCustomerInquiry = New ucCustomerInquiry(InAccount)
        End If

        With myucCustomerInquiry

            If dockingManager.Contents("Customer Inquiry") Is Nothing Then

                'AddHandler myThumbRightClickMenu.OptionClicked, AddressOf OptionClicked
                FloatingMenu = dockingManager.Contents.Add(myucCustomerInquiry, "Customer Inquiry")
                FloatingMenu.FloatingSize = New System.Drawing.Size(.Width + 20, .Height + 20)

                'If Cursor.Position.Y + FloatingMenu.FloatingSize.Height >  Then
                'FloatingMenu.DisplayLocation = New System.Drawing.Point(e.X, e.Y)
                FloatingMenu.DisplayLocation = New System.Drawing.Point(20, 20)
                FloatingMenu.CloseButton = False
                FloatingMenu.HideButton = False
                FloatingMenu.CaptionBar = False
                ' AddHandler dockingManager.ContentHiding, AddressOf ControlWasClosed

                Dim wc As WindowContent = dockingManager.AddContentWithState(FloatingMenu, Crownwood.DotNetMagic.Docking.State.Floating)
                dockingManager.AllowFloating = True
                dockingManager.BackColor = Color.SteelBlue
                dockingManager.InactiveTextColor = Color.White
                dockingManager.ActiveColor = Color.SteelBlue
                dockingManager.ActiveTextColor = Color.White
                dockingManager.ResizeBarColor = Color.SteelBlue
                '                dockingManager.InnerControl = myThumbRightClickMenu

            End If
            ' myucChangeCustomer.ShowIt()
            dockingManager.ShowContent(dockingManager.Contents("Customer Inquiry"))
        End With
    End Sub

    Private Sub myucCustomerInquiry_CloseMe() Handles myucCustomerInquiry.CloseMe
        dockingManager.HideContent(dockingManager.Contents("Customer Inquiry"))
    End Sub
#End Region

#Region "Show Tanks"
    Private Sub ShowTanks(InAccount As Long, inName As String)
        If dockingManager Is Nothing Then dockingManager = New DockingManager(Me, VisualStyle.IDE2005)
        'dockingManager.Contents.Clear()
        If Not dockingManager.Contents("Tanks") Is Nothing Then dockingManager.Contents.Remove(dockingManager.Contents("Tanks"))

        Dim myCustomerTanks As New CustomerMaintenance.ucCustomerTanks(InAccount, inName, UserId, UserName, UserPassword)

        With myCustomerTanks


            If dockingManager.Contents("Tanks") Is Nothing Then

                'AddHandler myThumbRightClickMenu.OptionClicked, AddressOf OptionClicked
                FloatingMenu = dockingManager.Contents.Add(myCustomerTanks, "Tanks")
                FloatingMenu.FloatingSize = New System.Drawing.Size(.Width + 20, .Height + 50)

                'If Cursor.Position.Y + FloatingMenu.FloatingSize.Height >  Then
                'FloatingMenu.DisplayLocation = New System.Drawing.Point(e.X, e.Y)
                FloatingMenu.DisplayLocation = New System.Drawing.Point(20, 20)
                FloatingMenu.CloseButton = False
                FloatingMenu.HideButton = False
                FloatingMenu.CaptionBar = False
                AddHandler myCustomerTanks.CloseMe, AddressOf TanksWasClosed
                AddHandler myCustomerTanks.PassBackFound, AddressOf PassBackTanksFound

                Dim wc As WindowContent = dockingManager.AddContentWithState(FloatingMenu, Crownwood.DotNetMagic.Docking.State.Floating)
                dockingManager.AllowFloating = True
                dockingManager.BackColor = Color.SteelBlue
                dockingManager.InactiveTextColor = Color.White
                dockingManager.ActiveColor = Color.SteelBlue
                dockingManager.ActiveTextColor = Color.White
                dockingManager.ResizeBarColor = Color.SteelBlue
                '                dockingManager.InnerControl = myThumbRightClickMenu

            End If
            ' myucChangeCustomer.ShowIt()
            dockingManager.ShowContent(dockingManager.Contents("Tanks"))
        End With
    End Sub

    Private Sub TanksWasClosed()
        dockingManager.HideContent(dockingManager.Contents("Tanks"))
    End Sub

    Private Sub PassBackTanksFound(inTanksFound As Boolean)
        dockingManager.HideContent(dockingManager.Contents("Tanks"))
    End Sub

    Private Sub CmdWarrantySearch_Click(sender As Object, e As EventArgs) Handles CmdWarrantySearch.Click
        ShowWarantySearch(TxtWarranty.Text, TxtItem.Text)
    End Sub


#End Region


End Class
