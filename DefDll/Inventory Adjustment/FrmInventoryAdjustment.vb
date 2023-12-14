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

Public Class FrmInventoryAdjustment

    'Implements IComparer

    'Private c_cellBadCell As Infragistics.Win.UltraWinGrid.UltraGridCell
    'Private myCustomer As Long
    Private mySiteRow As Infragistics.Win.UltraWinGrid.UltraGridRow


    Dim KeyCounter As Long = 0

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        LoadValueList()

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
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserID, UserPassword)
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

#End Region

#Region "Show order detail"
    Private Sub CmdNewOrder_Click(sender As System.Object, e As System.EventArgs) Handles CmdNewOrder.Click
        If Edit01 Then ShowOrderDetail(0)
    End Sub

    Private Function Edit01() As Boolean
        If ucAuthorizedBy.Value = 0 Then
            MsgBox("Authorized by must be selected", MsgBoxStyle.OkOnly, "Error")
            Return False
        End If

        If Trim(ucWarehouse.Text) = "" Then
            MsgBox("Warehouse must be selected", MsgBoxStyle.OkOnly, "Error")
            Return False
        End If

        Return True
    End Function

    Private Sub ShowOrderDetail(inReturnToTab As Integer)

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        KeyCounter += 1

        UltraTabControl1.Tabs.Add("A" & KeyCounter.ToString, "Inventory Adjustment Detail")

        Dim myucInventoryAdjustmentDetails As New ucInventoryAdjustmentDetails(ucAuthorizedBy.Value, ucWarehouse.Value, Format(TxtAdjustmentDate.Value, "yyyyMMdd"), "A" & KeyCounter.ToString, inReturnToTab)

        UltraTabControl1.Tabs("A" & KeyCounter.ToString).TabPage.Controls.Add(myucInventoryAdjustmentDetails)
        UltraTabControl1.Tabs("A" & KeyCounter.ToString).Selected = True
        AddHandler myucInventoryAdjustmentDetails.CloseMe, AddressOf CloseMyOrder

        myucInventoryAdjustmentDetails.Size = UltraTabControl1.Size
        myucInventoryAdjustmentDetails.Anchor = 15

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CloseMyOrder(ByVal tabkey As String, ReturnToTab As Integer)
        Try

            For Each childCtl As Windows.Forms.Control In UltraTabControl1.Tabs(tabkey).TabPage.Controls
                DisposeOfAllControls(childCtl)
            Next
            UltraTabControl1.Tabs.Remove(UltraTabControl1.Tabs(tabkey))
            GC.Collect()
            If ReturnToTab > UltraTabControl1.Tabs.Count - 1 Then
                UltraTabControl1.Tabs(0).Selected = True
            Else
                UltraTabControl1.Tabs(ReturnToTab).Selected = True
            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub CloseMyOrder(ByVal tabkey As String)
        For Each childCtl As Windows.Forms.Control In UltraTabControl1.Tabs(tabkey).TabPage.Controls
            DisposeOfAllControls(childCtl)
        Next
        UltraTabControl1.Tabs.Remove(UltraTabControl1.Tabs(tabkey))
        GC.Collect()
    End Sub

#End Region

    Private Sub CmdExistingOrdersGo_Click(sender As System.Object, e As System.EventArgs)
        ExistingOrdersGo()
    End Sub

    Private Sub ExistingOrdersGo()

        'ShowOrderDetail(myMscustp, mySoInvp.soInv_, 0, False)
    End Sub


#Region "Value List"
    Private Sub LoadValueList()
        Dim rs As ADODB.Recordset

        rs = connAS400.Execute("SELECT slslm#, slslnm from daily.msslmp WHERE sldiv# = 6 and not sldel = 'D' order by slslnm", Parms, -1)

        While Not rs.EOF
            ucAuthorizedBy.Items.Add(rs.Fields("slslm#").Value, rs.Fields("slslnm").Value)
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing
    End Sub
#End Region

End Class
