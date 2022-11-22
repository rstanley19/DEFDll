Imports System.IO
Imports system.Messaging
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid

Public Class ucWarrantyInquiry
    Private myTabkey As String
    Private mySerial As String
    Private myItem As String
    Private myReturnToTab As Integer


    Public Event CloseMe(ByVal TabKey As String, ReturnToTab As Integer)
    Public Event ShowCustomer(inMscustp As iSeries.msCustp, inOrderNbr As Integer, inReturnToTab As Integer)


    Public Sub New(ByVal inTabKey As String, inSerial As String, inItem As String, inReturnToTab As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.TxtTo
        myTabkey = inTabKey
        mySerial = inSerial
        myItem = inItem
        myReturnToTab = inReturnToTab
        BuildValueLists()

    End Sub

    Public Sub ShowIt()
        RebuildCustomers()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        RaiseEvent CloseMe(myTabkey, 0)
    End Sub

#Region "customers"
    Private WarrantyTable As New DataTable("Warranty")

    Private Sub RebuildCustomers()
        Dim mySqlWhere As String = ""
        If Not Trim(mySerial) = "" And Not Trim(myItem) = "" Then
            mySqlWhere = " WHERE upper(item) like " & Quoted("%" & (myItem.ToUpper) & "%") & " and upper(serial) like " & Quoted("%" & (mySerial.ToUpper) & "%") & " "
        ElseIf Not Trim(mySerial) = "" Then
            mySqlWhere = " WHERE  upper(serial) like " & Quoted("%" & (mySerial.ToUpper) & "%") & " "
        Else
            mySqlWhere = " WHERE  upper(item) like " & Quoted("%" & (myItem.ToUpper) & "%") & " "
        End If

        Dim connAS400 As New ADODB.Connection
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object
        connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)

        Daily_QryData = connAS400.Execute("SELECT daily.mswarnp.*, cmname, vndmnm " &
                                          "FROM daily.mswarnp " &
        "left join daily.mscustp on account=cmacc# " &
         "left join parafiles.appvnd on Vendor=vndnum " &
         mySqlWhere &
         " order by cmname", Parms, -1)
        LoadDatacustomers(Daily_QryData)
        Me.UltraGridCustomers.DataSource = WarrantyTable
        connAS400.Close()
    End Sub

    Public Sub LoadDatacustomers(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF

            Dim WarrantyRow As DataRow = WarrantyTable.NewRow
            WarrantyRow("Account") = rs.Fields("Account").Value
            WarrantyRow("Name") = rs.Fields("cmname").Value
            WarrantyRow("ID") = rs.Fields("ID").Value
            WarrantyRow("Vendor") = rs.Fields("vndmnm").Value
            WarrantyRow("Item") = rs.Fields("Item").Value
            WarrantyRow("Serial") = rs.Fields("Serial").Value

            If Not rs.Fields("StartDate").Value = 0 Then WarrantyRow("Start") = GetDateFromNbr(rs.Fields("StartDate").Value)
            If Not rs.Fields("EndDate").Value = 0 Then WarrantyRow("End") = GetDateFromNbr(rs.Fields("EndDate").Value)
            WarrantyRow("Length") = rs.Fields("LengthOfWarranty").Value
            WarrantyRow("Period") = rs.Fields("WarrantyPeriod").Value

            WarrantyTable.Rows.Add(WarrantyRow)

            rs.MoveNext()
        End While


    End Sub

    Public Sub BuildEmptyDataSet()
        WarrantyTable.Dispose()
        WarrantyTable = Nothing
        WarrantyTable = New DataTable("Warranty")

        GC.Collect()

        WarrantyTable.Columns.Add("ID", GetType(Long))
        WarrantyTable.Columns.Add("Account", GetType(Integer))
        WarrantyTable.Columns.Add("Name", GetType(String))
        WarrantyTable.Columns.Add("Vendor", GetType(String))

        WarrantyTable.Columns.Add("Item", GetType(String))
        WarrantyTable.Columns.Add("Serial", GetType(String))
        WarrantyTable.Columns.Add("Start", GetType(Date))
        WarrantyTable.Columns.Add("End", GetType(Date))
        WarrantyTable.Columns.Add("Length", GetType(Integer))
        WarrantyTable.Columns.Add("Period", GetType(String))

    End Sub

    Private Sub UltraGridcustomers_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridcustomers.ClickCellButton
        Select Case e.Cell.Column.Key
            Case Is = "Account"
                If e.Cell.Row.Cells("Account").Value = 0 Then Exit Sub
                Dim myMscustp As New iSeries.msCustp(e.Cell.Row.Cells("Account").Value, False)
                If myMscustp.found = True Then
                    RaiseEvent ShowCustomer(myMscustp, 0, myReturnToTab)
                Else
                    MsgBox("Sorry the customer is not found", MsgBoxStyle.OkOnly, "Error")
                End If

        End Select

    End Sub

    Private Sub UltraGridcustomers_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridcustomers.InitializeLayout

        With UltraGridcustomers.DisplayLayout.Bands(0)

            .Columns("Account").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("ID").Hidden = True
            .Columns("Name").Width = 250
            .Columns("Item").Width = 250
            .Columns("Vendor").Width = 250
            .Columns("Serial").Width = 100
            .Columns("Period").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList
            .Columns("Period").ValueList = PeriodValueList
            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i


        End With

    End Sub

#End Region

#Region "Build Value Lists"
    Public PeriodValueList As Infragistics.Win.ValueList
    Private Sub BuildValueLists()
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
