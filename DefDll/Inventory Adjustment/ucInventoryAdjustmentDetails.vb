Imports System.IO
Imports system.Messaging
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Crownwood.DotNetMagic.Common
Imports Crownwood.DotNetMagic.Docking

Public Class ucInventoryAdjustmentDetails
    Private myTabkey As String
    Private myReturnToTab As Integer

    Private myWarehouse As Long = 0
    Private myDate As Long = 0
    Private myApprovedBy As Integer

    Dim myStopClose As Boolean
    Dim myOkToShowError As Boolean

    Public Event CloseMe(ByVal TabKey As String, ReturnToTab As Integer)

    Public Sub New(inApprovedBy As Integer, inWarehouse As Long, inDate As Long, ByVal inTabKey As String, inReturnToTab As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        myApprovedBy = inApprovedBy
        myWarehouse = inWarehouse
        myDate = inDate

        QueryDataBaseForValueLists()
        BuildEmptyDataSetInventory()
        AddNewLine()
        Me.UltraGridInventory.DataSource = InventoryTable

        myTabkey = inTabKey
        myReturnToTab = inReturnToTab

    End Sub


#Region "Edit and Update"
    Private Sub SaveChangesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SaveChangesToolStripMenuItem.Click
        UltraGridInventory.UpdateData()

        EditInvoice()

        If myStopClose = True Then
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        If UpdateInvoice() = False Then
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Exit Sub
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default

        RaiseEvent CloseMe(myTabkey, myReturnToTab)
    End Sub

    Private Sub CancelChangesToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CancelChangesToolStripMenuItem.Click
        RaiseEvent CloseMe(myTabkey, myReturnToTab)
    End Sub

    Sub EditInvoice()
        LabError.Text = ""
        LabError.Visible = False
        myStopClose = False

        'edit warehouse fields
        If UltraGridInventory.Rows.Count > 0 Then

            EditInventory()

        End If

    End Sub

    Private Sub AddError(InString As String)
        If InStr(LabError.Text, InString) = 0 Then LabError.Text &= InString & vbCrLf
        myStopClose = True
        LabError.Visible = True
    End Sub

    Private Sub AddWarningError(InString As String)
        LabError.Text &= InString & vbCrLf
        LabError.Visible = True
    End Sub

    Private Function UpdateInvoice() As Boolean
        UpdateInventory()
        PrintInventory()
        Return True

    End Function


#End Region

#Region "Inventory"
    ' Inventory section
    Dim InventoryTable As New DataTable("Inventory")

    Private Sub RefreshInventory()

        BuildEmptyDataSetInventory()

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

#Region "Load Inventory"

    Public Sub BuildEmptyDataSetInventory()

        InventoryTable.Dispose()
        InventoryTable = Nothing
        InventoryTable = New dsInventoryAdjustment.InventoryDataTable

        GC.Collect()

    End Sub
#End Region

#Region "Grid Handling"

    Private myRightButtonClicked As Boolean = False
    Private Sub UltraGridInventory_MouseDown(sender As Object, e As MouseEventArgs) Handles UltraGridInventory.MouseDown
        If e.Button = MouseButtons.Right Then
            myRightButtonClicked = True
            AddNewLine()
        Else
            myRightButtonClicked = False
        End If
    End Sub
    Private Sub UltraGridInventory_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridInventory.ClickCellButton

        If myRightButtonClicked Then Exit Sub

        'If txtDeliveryDate.ReadOnly = True Then Exit Sub
        If e.Cell.Column.Key = "Delete" Then
            If e.Cell.Column.Key = "Delete" Then
                If e.Cell.Value = "Pending" Then
                    e.Cell.Value = ""
                Else
                    e.Cell.Value = "Pending"
                    UltraGridInventory.DisplayLayout.Bands(0).Columns("Delete").Width = 75
                End If
            End If

        End If

    End Sub


    Private Sub UltraGridInventory_BeforeRowsDeleted(sender As Object, e As Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs) Handles UltraGridInventory.BeforeRowsDeleted
        e.DisplayPromptMsg = False
    End Sub

    Private Sub UltraGridInventory_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridInventory.AfterCellUpdate

        Dim Grid As UltraGrid = DirectCast(sender, UltraGrid)
        Grid.EventManager.SetEnabled(GridEventIds.AfterCellUpdate, False)
        Try
            If e.Cell.Column.Key = "Item" Then
                e.Cell.Row.Cells("UM").Value = DBNull.Value
                Dim myMinvtp As New SharedDll.Iseries.mInvtp(e.Cell.Value)
                e.Cell.Row.Cells("Cost").Value = myMinvtp.imAvgc
                GetExtended(e.Cell.Row)
            ElseIf e.Cell.Column.Key = "Quantity" Then
                GetExtended(e.Cell.Row)
            ElseIf e.Cell.Column.Key = "UM" Then
                If Not uomDropdown.SelectedRow Is Nothing Then
                    ' e.Cell.Row.Cells("Conversion").Value = uomDropdown.SelectedRow.Cells("Multiplier").Value & "/1"
                    e.Cell.Row.Cells("UM").Value = uomDropdown.SelectedRow.Cells("UOM").Value
                End If
            End If

        Catch ex As Exception
        Finally
            Grid.EventManager.SetEnabled(GridEventIds.AfterCellUpdate, True)
        End Try
    End Sub

    Private Sub GetExtended(inrow As Infragistics.Win.UltraWinGrid.UltraGridRow)
        inrow.Cells("Extended").Value = Math.Round(inrow.Cells("Cost").Value * inrow.Cells("Quantity").Value, 2, MidpointRounding.AwayFromZero)
    End Sub

    Private Sub UltraGridInventory_BeforeCellActivate(sender As Object, e As Infragistics.Win.UltraWinGrid.CancelableCellEventArgs) Handles UltraGridInventory.BeforeCellActivate
        If e.Cell.Column.Key = "UM" Then
            Dim RootBand As UltraGridBand = uomDropdown.DisplayLayout.Bands(0)
            RootBand.ColumnFilters.ClearAllFilters()
            RootBand.ColumnFilters("Item").FilterConditions.Add(FilterComparisionOperator.Match, e.Cell.Row.Cells("Item").Value)
        End If
    End Sub

    Private Sub AddNewLine()

        Dim InventoryRow As DataRow

        InventoryRow = InventoryTable.NewRow

        InventoryRow("Item") = 0
        InventoryRow("Quantity") = 0
        InventoryRow("UM") = ""
        InventoryRow("Cost") = 0
        InventoryRow("Extended") = 0
        InventoryRow("Delete") = ""
        InventoryRow("Warehouse") = myWarehouse
        InventoryRow("Date") = GetDateFromNbr(myDate)

        InventoryTable.Rows.Add(InventoryRow)


    End Sub


    Private Sub UltraGridInventory_InitializeRow(sender As Object, e As Infragistics.Win.UltraWinGrid.InitializeRowEventArgs) Handles UltraGridInventory.InitializeRow

    End Sub

    Dim uomDropdown As UltraDropDown

    Private Sub UltraGridInventory_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridInventory.InitializeLayout
        'Dim myInventoryValueList As New ValueList
        'myInventoryValueList = InventoryValueList.Clone

        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

        e.Layout.Override.CellAppearance.ForeColorDisabled = Color.White
        e.Layout.Override.CellAppearance.BackColorDisabled = Color.FromArgb(0, 128, 128)

        With UltraGridInventory

            uomDropdown = New UltraDropDown
            Me.Controls.Add(uomDropdown)
            uomDropdown.SetDataBinding(InventoryUOMDataSet, "UOM")
            uomDropdown.ValueMember = "Item"
            uomDropdown.DisplayMember = "UOM"

            e.Layout.Bands(0).Columns("UM").ValueList = uomDropdown
            uomDropdown.DisplayLayout.Bands(0).Columns("Item").Hidden = True
            uomDropdown.DisplayLayout.Bands(0).Columns("Multiplier").Hidden = True


            .DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free
            .DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal

            .DisplayLayout.Bands(0).Columns("Item").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList
            .DisplayLayout.Bands(0).Columns("Item").ValueList = InventoryValueList
            .DisplayLayout.Bands(0).Columns("Item").Width = 225

            .DisplayLayout.Bands(0).Columns("Reason").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList
            .DisplayLayout.Bands(0).Columns("Reason").ValueList = ReasonValueList
            .DisplayLayout.Bands(0).Columns("Reason").Width = 125

            .DisplayLayout.Bands(0).Columns("UM").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList
            .DisplayLayout.Bands(0).Columns("UM").Width = 40
            .DisplayLayout.Bands(0).Columns("Cost").Width = 50
            .DisplayLayout.Bands(0).Columns("Quantity").Width = 45

            .DisplayLayout.Bands(0).Columns("Description").Hidden = True
            .DisplayLayout.Bands(0).Columns("Reason Description").Hidden = True
            .DisplayLayout.Bands(0).Columns("Warehouse").Hidden = True
            .DisplayLayout.Bands(0).Columns("Date").Hidden = True

            For i As Int32 = 0 To .DisplayLayout.Bands(0).Columns.Count - 1
                .DisplayLayout.Bands(0).Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

            .DisplayLayout.Bands(0).Columns("Delete").Width = 25
            .DisplayLayout.Bands(0).Columns("Delete").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .DisplayLayout.Bands(0).Columns("Delete").Header.Caption = ""

            .DisplayLayout.Bands(0).Columns("Item").CellActivation = Activation.AllowEdit
            .DisplayLayout.Bands(0).Columns("Quantity").CellActivation = Activation.AllowEdit
            .DisplayLayout.Bands(0).Columns("UM").CellActivation = Activation.AllowEdit
            .DisplayLayout.Bands(0).Columns("Reason").CellActivation = Activation.AllowEdit
        End With
    End Sub

#End Region

#Region "Edit Inventory"
    Private Sub llEditInventory_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        LabError.Text = ""
        LabError.Visible = False
        EditInventory()
    End Sub

    Private Sub EditInventory()
        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.UltraGridInventory.Rows
            If row.Cells("Item").Value = 0 Then
                AddError("Item must be selected")
            End If
            If NoNullString(row.Cells("UM").Value) = "" Then
                AddError("Unit of measure must be selected")
            End If
            If NoNullString(row.Cells("Reason").Value) = "" Then
                AddError("Reason must be selected")
            End If
        Next
    End Sub

#End Region

#Region "Update Inventory"
    Private Sub UpdateInventory()
        Dim myMiAdjp As New SharedDll.Iseries.MiAdjp
        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.UltraGridInventory.Rows
            With myMiAdjp
                .adWhse = myWarehouse
                .adItem = row.Cells("Item").Value
                .adDate = myDate
                .adQty = row.Cells("Quantity").Value
                .adUom = row.Cells("UM").Value
                .adReas = row.Cells("Reason").Value
                .adApby = myApprovedBy
                .adAvgc = row.Cells("Cost").Value
                .Create()

                Dim myMinvtp As New SharedDll.Iseries.mInvtp(.adItem)
                myMinvtp.imQtoh += .adQty
                myMinvtp.Update()

            End With
        Next
    End Sub

#End Region

#Region "Value List"
    Dim InventoryValueList As Infragistics.Win.ValueList
    Dim ReasonValueList As Infragistics.Win.ValueList

    Public InventoryUOMDataSet As DataSet
    Dim UOMTable As DataTable
#End Region
    Public Sub QueryDataBaseForValueLists()

        Dim rs As ADODB.Recordset

        rs = connAS400.Execute("SELECT imitem, imdes1, impuom, im2uom, im3uom, IMMUL2, IMMUL3, impo1, impo2, impo3 FROM daily.minvtp WHERE not imstat = 'D' order by imdes1", Parms, -1)

        InventoryUOMDataSet = New DataSet
        UOMTable = New DataTable("UOM")
        UOMTable.Columns.Add("Item", GetType(Integer))
        UOMTable.Columns.Add("UOM", GetType(String))
        UOMTable.Columns.Add("Multiplier", GetType(String))
        InventoryUOMDataSet.Tables.Add(UOMTable)

        While Not rs.EOF
            If Not rs.Fields("impuom").Value = "" And rs.Fields("impo1").Value = "Y" Then
                Dim UOMRow As DataRow = UOMTable.NewRow
                UOMRow("Item") = rs.Fields("Imitem").Value
                UOMRow("UOM") = rs.Fields("impuom").Value
                UOMRow("Multiplier") = 1
                UOMTable.Rows.Add(UOMRow)
            End If
            If Not rs.Fields("im2uom").Value = "" And rs.Fields("impo2").Value = "Y" Then
                Dim UOMRow As DataRow = UOMTable.NewRow
                UOMRow("Item") = rs.Fields("Imitem").Value
                UOMRow("UOM") = rs.Fields("im2uom").Value
                UOMRow("Multiplier") = rs.Fields("IMMUL2").Value
                UOMTable.Rows.Add(UOMRow)
            End If
            If Not rs.Fields("im3uom").Value = "" And rs.Fields("impo3").Value = "Y" Then
                Dim UOMRow As DataRow = UOMTable.NewRow
                UOMRow("Item") = rs.Fields("Imitem").Value
                UOMRow("UOM") = rs.Fields("im3uom").Value
                UOMRow("Multiplier") = rs.Fields("IMMUL3").Value
                UOMTable.Rows.Add(UOMRow)
            End If
            rs.MoveNext()
        End While

        rs = connAS400.Execute("SELECT imitem, imdes1, impuom, im2uom, im3uom, IMMUL2, IMMUL3, impo1, impo2, impo3 FROM daily.minvtp WHERE not imstat = 'D' order by imdes1", Parms, -1)

        InventoryValueList = New Infragistics.Win.ValueList()
        InventoryValueList.ValueListItems.Clear()

        While Not rs.EOF
            InventoryValueList.ValueListItems.Add(rs.Fields("imitem").Value, rs.Fields("imitem").Value & " - " & rs.Fields("imdes1").Value)
            rs.MoveNext()
        End While

        rs = connAS400.Execute("SELECT tatkey, tadesc from daily.potablp where tatabl = 'IAR' ", Parms, -1)

        ReasonValueList = New Infragistics.Win.ValueList()
        ReasonValueList.ValueListItems.Clear()

        While Not rs.EOF
            ReasonValueList.ValueListItems.Add(rs.Fields("tatkey").Value, rs.Fields("tadesc").Value)
            rs.MoveNext()
        End While

        rs.Close()
        rs = Nothing

    End Sub

#End Region

#Region "Print"

    Private Sub PrintInventory()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim InventoryTable As DataTable
        InventoryTable = New dsInventoryAdjustment.InventoryDataTable
        Dim myTotal As Decimal = 0
        Dim my200 As Decimal = 0
        Dim my206 As Decimal = 0 'Def

        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.UltraGridInventory.Rows
            Dim InventoryRow As DataRow = InventoryTable.NewRow
            For Each cell As Infragistics.Win.UltraWinGrid.UltraGridCell In row.Cells
                InventoryRow(cell.Column.Key) = cell.Value
            Next cell
            InventoryRow("Description") = row.Cells("Item").Text
            InventoryRow("Reason Description") = row.Cells("Reason").Text
            myTotal += InventoryRow("Extended")
            If InStr(UCase(InventoryRow("Description")), "DEF") > 0 Then
                my206 += InventoryRow("Extended")
            Else
                my200 += InventoryRow("Extended")
            End If
            InventoryTable.Rows.Add(InventoryRow)
        Next

        'Get the Report Location
        Dim strReportPath As String = Application.StartupPath & "\RPTFiles" & "\rpInventoryAdjustment.rpt"

        'Check file exists
        If Not IO.File.Exists(strReportPath) Then
            Throw (New Exception("Unable to locate report file:" & vbCrLf & strReportPath))
        End If

        Dim rptDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        rptDocument.Load(strReportPath)

        rptDocument.SetDataSource(InventoryTable)

        rptDocument.SetParameterValue("Total", myTotal)
        rptDocument.SetParameterValue("Total206", my206)
        rptDocument.SetParameterValue("Total200", my200)
        rptDocument.SetParameterValue("User", UserName)

        Dim myFrmCrystalViewer As New FrmCrystalViewer("Inventory Adjustments", rptDocument)
        myFrmCrystalViewer.Show()


        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub



#End Region
End Class
