Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.IO
Imports System.Text.Encoding
Imports System.Text
Imports System.Windows.Forms
Imports Crownwood.DotNetMagic.Common
Imports Crownwood.DotNetMagic.Docking
Imports Crownwood.DotNetMagic.Controls
Imports Crownwood.DotNetMagic.Menus

Public Class FrmUpdateBOL

    Protected WithEvents dockingManager As Crownwood.DotNetMagic.Docking.DockingManager
    Private FloatingMenu As Content
    Private WithEvents myInvoicesGrid As Infragistics.Win.UltraWinGrid.UltraGrid

#Region "Screen Handling"

    Private Sub CmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        RebuildBOLS()
        'If UltraGridBOLS.Rows.Count > 0 Then Me.CmdPrint.Enabled = True
    End Sub



    Private Sub FrmSalesContract_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        If Not connAS400.State = 1 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

#End Region

#Region "Rebuild Data "
    Private BOLSTable As New DataTable("BOLS")

    Private Sub RebuildBOLS()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim Parms As Object
        connAS400.CommandTimeout = 0
        Dim As400Data As ADODB.Recordset

        Dim mySql As String = ""

        mySql = "select soinv#, oditem, odid, odbl#, imdes1
from daily.soinvp
left join daily.soorddp on soinv# = odinv#
left join daily.minvtp on oditem = imitem
where soinv# = " & TxtInvoice.Value

        As400Data = connAS400.Execute(mySql, Parms, -1)

        LoadDataSetBOLS(As400Data)

        UltraGridBOLS.DataSource = BOLSTable

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Public Sub LoadDataSetBOLS(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF

            Dim BOLSRow As DataRow = BOLSTable.NewRow
            BOLSRow("Invoice") = rs.Fields("soinv#").Value
            BOLSRow("Item") = rs.Fields("oditem").Value
            BOLSRow("odid") = rs.Fields("odid").Value
            BOLSRow("Description") = rs.Fields("imdes1").Value
            BOLSRow("BOL") = rs.Fields("odbl#").Value
            BOLSRow("Update") = "Update"

            BOLSTable.Rows.Add(BOLSRow)
            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSet()
        BOLSTable.Dispose()

        BOLSTable = Nothing
        BOLSTable = New DataTable("BOLS")

        GC.Collect()

        BOLSTable.Columns.Add("Invoice", GetType(Long))
        BOLSTable.Columns.Add("Item", GetType(Long))
        BOLSTable.Columns.Add("odid", GetType(Long))
        BOLSTable.Columns.Add("Description", GetType(String))
        BOLSTable.Columns.Add("BOL", GetType(String))
        BOLSTable.Columns.Add("Update", GetType(String))
    End Sub

    Private Sub UltraGridBOLS_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridBOLS.InitializeLayout

        AddFilterToGrid(e)

        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti
        e.Layout.Override.WrapHeaderText = DefaultableBoolean.True

        With UltraGridBOLS.DisplayLayout.Bands(0)
            .Columns("Description").Width = 200
            .Columns("BOL").MaxLength = 20
            .Columns("Update").Width = 75
            .Columns("Update").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("odid").Hidden = True

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i
            .Columns("BOL").CellActivation = Activation.AllowEdit
        End With

    End Sub

    Private Sub UltraGridBOLS_ClickCellButton(sender As Object, e As CellEventArgs) Handles UltraGridBOLS.ClickCellButton
        Static myCount As Integer
        Select Case e.Cell.Column.Key
            Case Is = "Update"
                Dim mySoorddp As New iSeries.soOrddp(e.Cell.Row.Cells("odid").Value)
                mySoorddp.odBl_ = e.Cell.Row.Cells("BOL").Value
                mySoorddp.Update()
        End Select

    End Sub

#End Region

End Class
