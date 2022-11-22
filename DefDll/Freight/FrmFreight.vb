Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.IO
Imports System.Text.Encoding
Imports System.Text
Imports System.Windows.Forms

Public Class FrmFreight


#Region "Screen Handling"

    Private Sub CmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        RebuildInvoices()
    End Sub

    Private Sub FrmSalesContract_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub FrmSalesContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If Not connAS400.State = 1 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
        End If

        TxtFrom.Value = Format(GetEOMDate(DateAdd(DateInterval.Month, -1, Now)), "MM/01/yyyy")
        TxtTo.Text = GetEOMDate(DateAdd(DateInterval.Month, -1, Now))

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

#End Region

#Region "Rebuild Data "
    Private InvoicesTable As New DataTable("Invoices")
    Private DataSetInvoices As New DataSet

    Private Sub RebuildInvoices()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim Parms As Object
        connAS400.CommandTimeout = 0
        Dim As400Data As ADODB.Recordset
        As400Data = connAS400.Execute("SELECT cmname, soinv#, sostat, soblto, soidat, sofroz, sofrt$, soofrp, soest$, soprof, soefrc, soofrt, odcar#, carrier, ifnull(slslnm, 'Unknown') as sales
 FROM daily.soinvp                                         
        left join daily.mscustp on soacc# = cmacc#
        left join lateral (select case when odcar# < 100 then tadesc else caname end as Carrier, odcar#, odinv# from daily.soorddp 
        left join daily.potablp on tatabl = 'INC' and tatkey = odcar#
        left join daily.mscarrp on cacar# = odcar#
        where soinv# = odinv# fetch first 1 row only) on soinv# = odinv#
        left join daily.msslmp on slslm# = cmslmd
WHERE not sodel = 'D' and soinv = 'Y' 
and (not sofrt$ = 0 or not soofrp= 0 or not soest$ = 0 or not soprof = 0 or not soefrc = 0 or not soofrt = 0)  
and soidat between " & Format(TxtFrom.Value, "yyyyMMdd") & " and " & Format(TxtTo.Value, "yyyyMMdd") &
        " order by cmname, soinv# ", Parms, -1)

        LoadDataSet(As400Data)

        UltraGridInvoices.DataSource = DataSetInvoices

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Public Sub LoadDataSet(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF
            Dim InvoicesRow As DataRow = InvoicesTable.NewRow
            InvoicesRow("Invoice") = rs.Fields("soinv#").Value
            InvoicesRow("Account") = rs.Fields("soblto").Value
            InvoicesRow("Customer") = rs.Fields("cmname").Value
            InvoicesRow("Sales") = rs.Fields("sales").Value
            If rs.Fields("sostat").Value = "T" Then
                InvoicesRow("Type") = "Trans"
            Else
                InvoicesRow("Type") = ""
            End If
            InvoicesRow("Carrier") = rs.Fields("Carrier").Value

            InvoicesRow("Invoice Date") = GetDateFromNbr(rs.Fields("soidat").Value)

            InvoicesRow("sofrt$") = rs.Fields("sofrt$").Value
            InvoicesRow("soest$") = rs.Fields("soest$").Value
            InvoicesRow("Frt Difference") = rs.Fields("sofrt$").Value - rs.Fields("soest$").Value

            InvoicesRow("soprof") = rs.Fields("soprof").Value
            InvoicesRow("soefrc") = rs.Fields("soefrc").Value

            InvoicesRow("soofrp") = rs.Fields("soofrp").Value
            InvoicesRow("soofrt") = rs.Fields("soofrt").Value
            If rs.Fields("soofrp").Value = -1 Then
                InvoicesRow("Outside Difference") = 0
            Else
                InvoicesRow("Outside Difference") = rs.Fields("soofrp").Value - rs.Fields("soofrt").Value
            End If

            InvoicesTable.Rows.Add(InvoicesRow)
            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSet()
        InvoicesTable.Dispose()

        InvoicesTable = Nothing
        DataSetInvoices = Nothing

        InvoicesTable = New DataTable("Invoices")
        DataSetInvoices = New DataSet

        GC.Collect()

        InvoicesTable.Columns.Add("Invoice", GetType(Long))
        InvoicesTable.Columns.Add("Account", GetType(Long))
        InvoicesTable.Columns.Add("Customer", GetType(String))
        InvoicesTable.Columns.Add("Sales", GetType(String))
        InvoicesTable.Columns.Add("Type", GetType(String))

        InvoicesTable.Columns.Add("Carrier", GetType(String))
        InvoicesTable.Columns.Add("Invoice Date", GetType(Date))

        InvoicesTable.Columns.Add("sofrt$", GetType(decimal))
        InvoicesTable.Columns.Add("soest$", GetType(decimal))
        InvoicesTable.Columns.Add("Frt Difference", GetType(decimal))

        InvoicesTable.Columns.Add("soprof", GetType(decimal))
        InvoicesTable.Columns.Add("soefrc", GetType(decimal))

        InvoicesTable.Columns.Add("soofrp", GetType(decimal))
        InvoicesTable.Columns.Add("soofrt", GetType(decimal))
        InvoicesTable.Columns.Add("Outside Difference", GetType(decimal))

        ' build dataset
        DataSetInvoices.Tables.Add(InvoicesTable)

    End Sub

    Private Sub UltraGridInvoices_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridInvoices.ClickCellButton
        'Select Case e.Cell.Column.Key
        '    Case Is = "Invoice"
        '        Dim mySetHandshake As New Billing2013.Class1
        '        mySetHandshake.SetHandShake(UserId, UserName, UserPassword, smtp, Application.ProductName, Application.StartupPath, myPrivateUserFolder, iSeriesIP)
        '        Dim myFrmInvoice As New Billing2013.FrmInvoice(UserId, UserName, UserPassword, smtp, Application.ProductName, Application.StartupPath, myPrivateUserFolder)
        '        AddHandler myFrmInvoice.ShowTransportationInvoice, AddressOf myShowTransportationInvoice
        '        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        '        myFrmInvoice.ShowMe(System.Convert.ToInt32(e.Cell.Row.Cells("Invoice").Value))
        '        Me.Cursor = System.Windows.Forms.Cursors.Default
        'End Select
    End Sub

    Private Sub UltraGridInvoices_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridInvoices.InitializeLayout
        AddFilterToGrid(e)

        Dim band As UltraGridBand = Me.UltraGridInvoices.DisplayLayout.Bands(0)

        If UltraGridInvoices.DisplayLayout.Bands(0).Summaries.Count = 0 Then
            Dim FrtPaid As SummarySettings = band.Summaries.Add("sofrt$", SummaryType.Sum, band.Columns("sofrt$"))
            ' Set the format of the summary text
            FrtPaid.DisplayFormat = "{0:$###,###,###.00}"
            ' Change the appearance settings for summaries.
            FrtPaid.Appearance.TextHAlign = HAlign.Right
            FrtPaid.Appearance.BackColor = Drawing.Color.Tan
            ' Set the DisplayInGroupBy property of both summaries to false so they don't
            ' show up in group-by rows.
            FrtPaid.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            Dim FrtEstimated As SummarySettings = band.Summaries.Add("soest$", SummaryType.Sum, band.Columns("soest$"))
            FrtEstimated.DisplayFormat = "{0:$###,###,###.00}"
            FrtEstimated.Appearance.TextHAlign = HAlign.Right
            FrtEstimated.Appearance.BackColor = Drawing.Color.Tan
            FrtEstimated.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            Dim FrtDifference As SummarySettings = band.Summaries.Add("FrtDifference", SummaryType.Sum, band.Columns("Frt Difference"))
            FrtDifference.DisplayFormat = "{0:$###,###,###.00}"
            FrtDifference.Appearance.TextHAlign = HAlign.Right
            FrtDifference.Appearance.BackColor = Drawing.Color.Tan
            FrtDifference.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            Dim soofrp As SummarySettings = band.Summaries.Add("soofrp", SummaryType.Sum, band.Columns("soofrp"))
            soofrp.DisplayFormat = "{0:$###,###,###.00}"
            soofrp.Appearance.TextHAlign = HAlign.Right
            soofrp.Appearance.BackColor = Drawing.Color.Tan
            soofrp.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            Dim soofrt As SummarySettings = band.Summaries.Add("soofrt", SummaryType.Sum, band.Columns("soofrt"))
            soofrt.DisplayFormat = "{0:$###,###,###.00}"
            soofrt.Appearance.TextHAlign = HAlign.Right
            soofrt.Appearance.BackColor = Drawing.Color.Tan
            soofrt.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            Dim OutsideDifference As SummarySettings = band.Summaries.Add("OutsideDifference", SummaryType.Sum, band.Columns("Outside Difference"))
            OutsideDifference.DisplayFormat = "{0:$###,###,###.00}"
            OutsideDifference.Appearance.TextHAlign = HAlign.Right
            OutsideDifference.Appearance.BackColor = Drawing.Color.Tan
            OutsideDifference.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            ' Set the caption that shows up on the header of the summary footer.
            band.SummaryFooterCaption = "Total"
            band.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True
            band.Override.SummaryFooterCaptionAppearance.BackColor = Drawing.Color.DarkBlue
            band.Override.SummaryFooterCaptionAppearance.ForeColor = Drawing.Color.LightYellow

        End If

        With UltraGridInvoices.DisplayLayout.Bands(0)

            .Columns("Customer").Width = 200
            .Columns("Type").Width = 60
            .Columns("sofrt$").Format = "$###,###.00"
            .Columns("sofrt$").CellAppearance.TextHAlign = HAlign.Right
            .Columns("sofrt$").Header.Caption = "Frt Paid"

            .Columns("soest$").Format = "$###,###.00"
            .Columns("soest$").CellAppearance.TextHAlign = HAlign.Right
            .Columns("soest$").Header.Caption = "Frt Estimated"

            .Columns("Frt Difference").Format = "$###,###.00"
            .Columns("Frt Difference").CellAppearance.TextHAlign = HAlign.Right

            .Columns("soprof").Format = "$###,###.00"
            .Columns("soprof").CellAppearance.TextHAlign = HAlign.Right
            .Columns("soprof").Header.Caption = "Profit Frt"
            .Columns("soprof").Hidden = True

            .Columns("soefrc").Format = "$###,###.00"
            .Columns("soefrc").CellAppearance.TextHAlign = HAlign.Right
            .Columns("soefrc").Header.Caption = "Frt Cost"
            .Columns("soefrc").Hidden = True

            .Columns("soofrp").Format = "$###,###.00"
            .Columns("soofrp").CellAppearance.TextHAlign = HAlign.Right
            .Columns("soofrp").Header.Caption = "Outside Paid"

            .Columns("soofrt").Format = "$###,###.00"
            .Columns("soofrt").CellAppearance.TextHAlign = HAlign.Right
            .Columns("soofrt").Header.Caption = "Outside Estimated"

            .Columns("Outside Difference").Format = "$###,###.00"
            .Columns("Outside Difference").CellAppearance.TextHAlign = HAlign.Right


            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

            .Columns("Invoice").CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly
            .Columns("Invoice").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button

        End With

    End Sub

#End Region

End Class
