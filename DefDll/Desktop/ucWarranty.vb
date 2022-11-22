Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging
Imports System.IO

Public Class ucWarranty

    Dim myAccount As Integer
    Dim myucCustomer As ucCustomer

    Public Sub New(inAccount As Integer, inucCustomer As ucCustomer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Cursor = Windows.Forms.Cursors.WaitCursor
        myAccount = inAccount
        myucCustomer = inucCustomer

        BuildValueLists()

        RefreshWarranty()

        Cursor = Windows.Forms.Cursors.Default

    End Sub

    Private Sub CmdRefresh_Click(sender As Object, e As EventArgs)
        Cursor = Windows.Forms.Cursors.WaitCursor
        RefreshWarranty()
        Cursor = Windows.Forms.Cursors.Default
    End Sub

#Region "Warranty"

    Private WarrantyTable As New DataTable("Warranty")

    Private Sub RefreshWarranty()
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object

        Dim mySql As String = "SELECT * " &
            "from daily.mswarnp "

        mySql &= "where account = " & myAccount

        mySql &= " order by StartDate desc"

        Daily_QryData = connAS400.Execute(mySql, Parms, -1)
        LoadDataWarranty(Daily_QryData)
        UltraGridWarranty.DataSource = WarrantyTable

    End Sub

    Public Sub LoadDataWarranty(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetWarranty()

        'Load the data
        While Not rs.EOF

            Dim WarrantyRow As DataRow = WarrantyTable.NewRow
            WarrantyRow("ID") = rs.Fields("ID").Value
            WarrantyRow("Vendor") = rs.Fields("Vendor").Value
            WarrantyRow("Item") = rs.Fields("Item").Value
            WarrantyRow("Serial") = rs.Fields("Serial").Value

            If Not rs.Fields("StartDate").Value = 0 Then WarrantyRow("Start") = GetDateFromNbr(rs.Fields("StartDate").Value)
            If Not rs.Fields("EndDate").Value = 0 Then WarrantyRow("End") = GetDateFromNbr(rs.Fields("EndDate").Value)
            WarrantyRow("Length") = rs.Fields("LengthOfWarranty").Value
            WarrantyRow("Period") = rs.Fields("WarrantyPeriod").Value
            WarrantyRow("PO") = rs.Fields("PO").Value

            WarrantyTable.Rows.Add(WarrantyRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSetWarranty()
        WarrantyTable.Dispose()
        WarrantyTable = Nothing
        WarrantyTable = New DataTable("Warranty")

        GC.Collect()

        WarrantyTable.Columns.Add("ID", GetType(Long))
        WarrantyTable.Columns.Add("Vendor", GetType(Integer))
        WarrantyTable.Columns.Add("Item", GetType(String))
        WarrantyTable.Columns.Add("Serial", GetType(String))
        WarrantyTable.Columns.Add("Start", GetType(Date))
        WarrantyTable.Columns.Add("End", GetType(Date))
        WarrantyTable.Columns.Add("Length", GetType(Integer))
        WarrantyTable.Columns.Add("Period", GetType(String))
        WarrantyTable.Columns.Add("PO", GetType(String))
    End Sub

    Private Sub UltraGridWarranty_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridWarranty.ClickCellButton

        Select Case e.Cell.Column.Key
            Case Is = "Item"
                MaintainWarranty(e.Cell.Row.Cells("Id").Value, e.Cell.Row)
        End Select

    End Sub

    Private Sub UltraGridWarranty_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridWarranty.InitializeLayout

        AddFilterToGrid(e)
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

        e.Layout.Appearance.FontData.SizeInPoints = 8

        With UltraGridWarranty.DisplayLayout.Bands(0)
            .Columns("Item").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Item").Width = 250
            .Columns("Vendor").Width = 250
            .Columns("Serial").Width = 100
            .Columns("ID").Hidden = True
            .Columns("Vendor").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList
            .Columns("Vendor").ValueList = VendorValueList
            .Columns("Period").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList
            .Columns("Period").ValueList = PeriodValueList
            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

#End Region

#Region "Show Customer"
    Private Sub CmdCreate_Click(sender As System.Object, e As System.EventArgs) Handles CmdCreate.Click
        MaintainWarranty(0, Nothing)
    End Sub

    Private Sub MaintainWarranty(inId As Long, inRow As Infragistics.Win.UltraWinGrid.UltraGridRow)

        Try

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim mucWarrantyMaintain As ucWarrantyMaintain

            Static KeyCounter As Long
            KeyCounter += 1

            myucCustomer.UltraTabControl2.Tabs.Add("W" & KeyCounter.ToString, "Maintain Warranty")

            If Not inId = 0 Then
                mucWarrantyMaintain = New ucWarrantyMaintain("W" & KeyCounter.ToString, inId, myAccount, inRow, VendorValueList)
            Else
                mucWarrantyMaintain = New ucWarrantyMaintain("W" & KeyCounter.ToString, inId, myAccount, inRow, VendorValueList)
            End If

            myucCustomer.UltraTabControl2.Tabs("W" & KeyCounter.ToString).TabPage.Controls.Add(mucWarrantyMaintain)
            myucCustomer.UltraTabControl2.Tabs("W" & KeyCounter.ToString).Selected = True
            AddHandler mucWarrantyMaintain.CloseMe, AddressOf TaskWasClosed
            AddHandler mucWarrantyMaintain.AddWarranty, AddressOf AddWarranty

            mucWarrantyMaintain.Size = myucCustomer.UltraTabControl2.Size
            mucWarrantyMaintain.Anchor = 15

        Catch ex As Exception
            If UserId = "RICKS" Then MsgBox(ex.ToString)
        End Try

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub TaskWasClosed(ByVal tag As String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        myucCustomer.UltraTabControl2.Tabs(tag).Selected = True
        myucCustomer.UltraTabControl2.Tabs.Remove(myucCustomer.UltraTabControl2.Tabs(tag))
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub AddWarranty(BackmsWarnp As iSeries.msWarnp)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim WarrantyRow As DataRow = WarrantyTable.NewRow
        With BackmsWarnp
            WarrantyRow("ID") = .ID
            WarrantyRow("Vendor") = .Vendor
            WarrantyRow("Item") = .Item
            WarrantyRow("Serial") = .Serial
            If Not .StartDate = 0 Then WarrantyRow("Start") = GetDateFromNbr(.StartDate)
            If Not .EndDate = 0 Then WarrantyRow("End") = GetDateFromNbr(.EndDate)
            WarrantyRow("Length") = .LengthOfWarranty
            WarrantyRow("Period") = .WarrantyPeriod
            WarrantyRow("PO") = .PO

        End With


        WarrantyTable.Rows.Add(WarrantyRow)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CloseMyOrder(ByVal tabkey As String, ReturnToTab As Integer)
        Try
            myucCustomer.UltraTabControl2.Tabs.Remove(myucCustomer.UltraTabControl2.Tabs(tabkey))
            myucCustomer.UltraTabControl2.Tabs(ReturnToTab).Selected = True
        Catch
        End Try
    End Sub

#End Region

#Region "Build Value Lists"
    Public VendorValueList As Infragistics.Win.ValueList
    Public PeriodValueList As Infragistics.Win.ValueList
    Private Sub BuildValueLists()
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object
        If Not connAS400.State = 1 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iseriesip & ";", UserId, UserPassword)
        End If

        Daily_QryData = connAS400.Execute("SELECT * FROM daily.potablp " &
                                          "left join parafiles.appvnd on tatkey = vndnum " &
                                          "WHERE tatabl = 'WAR' order by tatkey", Parms, -1)

        VendorValueList = New Infragistics.Win.ValueList()
        VendorValueList.ValueListItems.Clear()

        While Not Daily_QryData.EOF
            VendorValueList.ValueListItems.Add(Daily_QryData.Fields("TaTkey").Value, Daily_QryData.Fields("vndmnm").Value)
            Daily_QryData.MoveNext()
        End While

        PeriodValueList = New Infragistics.Win.ValueList()
        PeriodValueList.ValueListItems.Clear()
        PeriodValueList.ValueListItems.Add("", "")
        PeriodValueList.ValueListItems.Add("D", "Days")
        PeriodValueList.ValueListItems.Add("W", "Weeks")
        PeriodValueList.ValueListItems.Add("M", "Months")
        PeriodValueList.ValueListItems.Add("Y", "Years")
    End Sub

#End Region
End Class
