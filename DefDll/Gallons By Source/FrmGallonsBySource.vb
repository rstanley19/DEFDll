Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.IO
Imports System.Text.Encoding
Imports System.Text
Imports System.Windows.Forms


Public Class FrmGallonsBySource


#Region "Screen Handling"

    Private Sub CmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        RebuildDEF()
    End Sub

    Private Sub FrmSalesContract_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub FrmSalesContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If Not connAS400.State = 1 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
        End If

        TxtFrom.Value = "01/01/" & Format(Now, "yyyy")
        TxtTo.Value = Now

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

#End Region

#Region "Rebuild DEF Data "
    Private DEFTable As New DataTable("DEF")

    Private Sub RebuildDEF()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim myCurrentYear As Integer = Format(TxtFrom.Value, "yyyy")
        Dim myPreviousYear As Integer = myCurrentYear - 1

        Dim Parms As Object
        connAS400.CommandTimeout = 0
        Dim As400Data As ADODB.Recordset

        Dim mySql As String = ""

        mySql = "select sum(odqshp) as shipped, s.vndmnm as Source, l.vndmnm as Location
from daily.soorddp
left join daily.soinvp on odinv# = soinv#
left join parafiles.appvnd as s on s.vndnum = odvnds
left join parafiles.appvnd as l on l.vndnum = odlocs
WHERE sostat = 'I' and soidat between  " & Format(TxtFrom.Value, "yyyyMMdd") & " and " & Format(TxtTo.Value, "yyyyMMdd") &
         " and oditem = 1580 and not odlocs = 0
group by s.vndmnm, l.vndmnm
order by s.vndmnm, l.vndmnm"

        As400Data = connAS400.Execute(mySql, Parms, -1)

        LoadDataSetDEF(As400Data)

        UltraGridDEF.DataSource = DEFTable

        PrintToolStripMenuItem.Enabled = True


        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Public Sub LoadDataSetDEF(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetDEF()

        'Load the data
        While Not rs.EOF

            Dim DEFRow As DataRow = DEFTable.NewRow
            DEFRow("Shipped") = rs.Fields("Shipped").Value
            DEFRow("Source") = rs.Fields("Source").Value
            DEFRow("Location") = rs.Fields("Location").Value

            DEFTable.Rows.Add(DEFRow)
            rs.MoveNext()
        End While


    End Sub

    Public Sub BuildEmptyDataSetDEF()
        DEFTable.Dispose()

        DEFTable = Nothing


        DEFTable = New DataTable("DEF")

        GC.Collect()

        DEFTable.Columns.Add("Shipped", GetType(Long))
        DEFTable.Columns.Add("Source", GetType(String))
        DEFTable.Columns.Add("Location", GetType(String))

    End Sub

    Private Sub UltraGridDEF_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridDEF.InitializeLayout
        If UltraGridDEF.DisplayLayout.Bands(0).Summaries.Count = 0 Then
            Dim band As UltraGridBand = Me.UltraGridDEF.DisplayLayout.Bands(0)


            Dim Shipped As SummarySettings = band.Summaries.Add("Shipped", SummaryType.Sum, band.Columns("Shipped"))
            ' Set the format of the summary text
            Shipped.DisplayFormat = "{0:###,###,###}"
            ' Change the appearance settings for summaries.
            Shipped.Appearance.TextHAlign = HAlign.Right
            Shipped.Appearance.BackColor = Drawing.Color.Tan
            ' Set the DisplayInGroupBy property of both summaries to false so they don't
            ' show up in group-by rows.
            Shipped.SummaryDisplayArea = SummaryDisplayAreas.TopFixed

            ' Set the caption that shows up on the header of the summary footer.
            band.SummaryFooterCaption = "Total"
            band.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True
            band.Override.SummaryFooterCaptionAppearance.BackColor = Drawing.Color.DarkBlue
            band.Override.SummaryFooterCaptionAppearance.ForeColor = Drawing.Color.LightYellow

        End If

        AddFilterToGrid(e)

        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti
        e.Layout.Override.WrapHeaderText = DefaultableBoolean.True

        e.Layout.UseFixedHeaders = True


        With UltraGridDEF.DisplayLayout.Bands(0)
            .Columns("Source").Width = 350
            .Columns("Location").Width = 350
            .Columns("Shipped").Format = "###,###,##0"
            .Columns("Shipped").CellAppearance.TextHAlign = HAlign.Right

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

#Region "Print"

    Private Sub PrintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintToolStripMenuItem.Click


        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim mydsGallons As DataTable
        mydsGallons = New dsGallonsBySource.GallonsDataTable


        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.UltraGridDEF.Rows.GetFilteredInNonGroupByRows()
            Dim GallonsRow As DataRow = mydsGallons.NewRow
            For Each cell As Infragistics.Win.UltraWinGrid.UltraGridCell In row.Cells
                GallonsRow(cell.Column.Key) = cell.Value
            Next cell
            mydsGallons.Rows.Add(GallonsRow)
        Next

        'Get the Report Location
        Dim strReportPath As String = Application.StartupPath & "\RPTFiles" & "\rpDefGallonsBySource.rpt"

        'Check file exists
        If Not IO.File.Exists(strReportPath) Then
            Throw (New Exception("Unable to locate report file:" & vbCrLf & strReportPath))
        End If

        Dim rptDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        rptDocument.Load(strReportPath)

        rptDocument.SetDataSource(mydsGallons)

        rptDocument.SetParameterValue("Heading", "DEF Gallons By Source " & TxtFrom.Text & " to " & TxtTo.Text)


        Dim myFrmCrystalViewer As New FrmCrystalViewer("DEF Gallons By Source", rptDocument)
        myFrmCrystalViewer.Show()


        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub



#End Region

#End Region


End Class
