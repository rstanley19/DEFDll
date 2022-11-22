Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging
Imports System.IO

Public Class ucInvoices

    Dim myAccount As Integer
    Dim myBillTo As Integer
    Dim myType As String
    Dim myInvoice As Long
    Dim myImageFile As String

    Public Sub New(inAccount As Integer, inBillTo As Integer, inType As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Cursor = Windows.Forms.Cursors.WaitCursor
        myAccount = inAccount
        myBillTo = inBillTo
        myType = inType

        TxtFrom.Value = DateAdd(DateInterval.Month, -1, Now)
        TxtTo.Value = Now

        RefreshInvoices()
        Cursor = Windows.Forms.Cursors.Default

    End Sub

    Private Sub UltraTabControl1_ActiveTabChanged(sender As Object, e As UltraWinTabControl.ActiveTabChangedEventArgs)
        Cursor = Windows.Forms.Cursors.WaitCursor
        If e.Tab.Text = "Invoices" Then
            If UltraGridInvoices.Rows.Count = 0 Then
                RefreshInvoices()
            End If
        End If
        Cursor = Windows.Forms.Cursors.Default
    End Sub

    Private Sub CmdRefresh_Click(sender As Object, e As EventArgs) Handles CmdRefresh.Click
        Cursor = Windows.Forms.Cursors.WaitCursor
        RefreshInvoices()
        Cursor = Windows.Forms.Cursors.Default
    End Sub

#Region "Invoice"

    Private InvoicesTable As New DataTable("Invoices")

    Private Sub RefreshInvoices()
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object

        Dim mySql As String = "SELECT soblto, soacc#, soinv#, soodat, soddat, soidat, sodel " &
            "from daily.soinvp "

        If Not myAccount = myBillTo Then
            mySql &= "where soacc# = " & myAccount
        Else
            mySql &= "where soblto = " & myBillTo
        End If

        mySql &= " and soddat between " & Format(TxtFrom.Value, "yyyyMMdd") & " and " & Format(TxtTo.Value, "yyyyMMdd") &
            " order by soddat desc"

        Daily_QryData = connAS400.Execute(mySql, Parms, -1)
        LoadDataInvoices(Daily_QryData)
        UltraGridInvoices.DataSource = InvoicesTable

    End Sub

    Public Sub LoadDataInvoices(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetInvoices()

        'Load the data
        While Not rs.EOF

            Dim InvoiceRow As DataRow = InvoicesTable.NewRow
            InvoiceRow("Bill To") = rs.Fields("soblto").Value
            InvoiceRow("Account") = rs.Fields("soacc#").Value
            InvoiceRow("Invoice") = rs.Fields("soinv#").Value

            If Not rs.Fields("soodat").Value = 0 Then InvoiceRow("Ordered") = GetDateFromNbr(rs.Fields("soodat").Value)
            If Not rs.Fields("soddat").Value = 0 Then InvoiceRow("Delivery") = GetDateFromNbr(rs.Fields("soddat").Value)
            If Not rs.Fields("soidat").Value = 0 Then InvoiceRow("Invoiced") = GetDateFromNbr(rs.Fields("soidat").Value)

            Select Case rs.Fields("sodel").Value
                Case "D"
                    InvoiceRow("Status") = "Deleted"
                Case "F"
                    InvoiceRow("Status") = "Future Credit Check"
                Case "P"
                    InvoiceRow("Status") = "Postponed Credit Check"
                Case "H"
                    InvoiceRow("Status") = "Held for Credit Check"
                Case Else
                    InvoiceRow("Status") = rs.Fields("sodel").Value
            End Select


            InvoicesTable.Rows.Add(InvoiceRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSetInvoices()
        InvoicesTable.Dispose()
        InvoicesTable = Nothing
        InvoicesTable = New DataTable("Invoices")

        GC.Collect()

        InvoicesTable.Columns.Add("Bill To", GetType(Integer))
        InvoicesTable.Columns.Add("Account", GetType(Integer))
        InvoicesTable.Columns.Add("Invoice", GetType(Integer))

        InvoicesTable.Columns.Add("Ordered", GetType(Date))
        InvoicesTable.Columns.Add("Delivery", GetType(Date))
        InvoicesTable.Columns.Add("Invoiced", GetType(Date))
        InvoicesTable.Columns.Add("Status", GetType(String))
    End Sub

    Private Sub UltraGridInvoices_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridInvoices.ClickCellButton

        Select Case e.Cell.Column.Key
            Case Is = "Invoice"

                Dim myFrmInvoice As New Billing2013.FrmInvoice(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder)
                myFrmInvoice.ShowMe(e.Cell.Value)
                myFrmInvoice.Show()

        End Select

    End Sub

    Private Sub UltraGridInvoices_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridInvoices.InitializeLayout

        AddFilterToGrid(e)
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

        e.Layout.Appearance.FontData.SizeInPoints = 8

        With UltraGridInvoices.DisplayLayout.Bands(0)
            .Columns("Invoice").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Bill To").Width = 70
            .Columns("Account").Width = 70
            .Columns("Invoice").Width = 80
            .Columns("Ordered").Width = 100
            .Columns("Delivery").Width = 100
            .Columns("Invoiced").Width = 100
            .Columns("Status").Width = 200

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

#End Region
End Class
