Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Infragistics.Win.UltraWinToolbars


Public Class FrmInventoryCostAnalysis
    Inherits System.Windows.Forms.Form


    Dim InvoiceTable As New DataTable("Invoice")
    Dim DataSetInvoice As New DataSet()

    Dim InvoiceTable2 As New DataTable("Invoice2")
    Dim DataSetInvoice2 As New DataSet()

    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbFrozenPeriod As System.Windows.Forms.RadioButton
    Friend WithEvents rbUnfrozen As System.Windows.Forms.RadioButton
    Friend WithEvents TxtDateFrozen As System.Windows.Forms.DateTimePicker
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraGridPulledFromInventory As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents CmdExportToExcel As System.Windows.Forms.Button
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        TxtDateFrozen.Value = GetEOMDate(DateAdd(DateInterval.Month, -1, Now))
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents CmdRefresh As System.Windows.Forms.Button
    Friend WithEvents UltraGridDirectShipItems As Infragistics.Win.UltraWinGrid.UltraGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridDirectShipItems = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraGridPulledFromInventory = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.CmdRefresh = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbFrozenPeriod = New System.Windows.Forms.RadioButton()
        Me.rbUnfrozen = New System.Windows.Forms.RadioButton()
        Me.TxtDateFrozen = New System.Windows.Forms.DateTimePicker()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CmdExportToExcel = New System.Windows.Forms.Button()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.UltraGridDirectShipItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.UltraGridPulledFromInventory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.UltraGridDirectShipItems)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 23)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(983, 503)
        '
        'UltraGridDirectShipItems
        '
        Me.UltraGridDirectShipItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraGridDirectShipItems.Location = New System.Drawing.Point(3, 3)
        Me.UltraGridDirectShipItems.Name = "UltraGridDirectShipItems"
        Me.UltraGridDirectShipItems.Size = New System.Drawing.Size(977, 500)
        Me.UltraGridDirectShipItems.TabIndex = 0
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.UltraGridPulledFromInventory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(983, 503)
        '
        'UltraGridPulledFromInventory
        '
        Me.UltraGridPulledFromInventory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraGridPulledFromInventory.Location = New System.Drawing.Point(3, 1)
        Me.UltraGridPulledFromInventory.Name = "UltraGridPulledFromInventory"
        Me.UltraGridPulledFromInventory.Size = New System.Drawing.Size(977, 500)
        Me.UltraGridPulledFromInventory.TabIndex = 0
        '
        'CmdRefresh
        '
        Me.CmdRefresh.Location = New System.Drawing.Point(8, 8)
        Me.CmdRefresh.Name = "CmdRefresh"
        Me.CmdRefresh.Size = New System.Drawing.Size(64, 24)
        Me.CmdRefresh.TabIndex = 0
        Me.CmdRefresh.Text = "Refresh"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbFrozenPeriod)
        Me.Panel1.Controls.Add(Me.rbUnfrozen)
        Me.Panel1.Location = New System.Drawing.Point(89, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(172, 26)
        Me.Panel1.TabIndex = 1
        '
        'rbFrozenPeriod
        '
        Me.rbFrozenPeriod.AutoSize = True
        Me.rbFrozenPeriod.Location = New System.Drawing.Point(78, 3)
        Me.rbFrozenPeriod.Name = "rbFrozenPeriod"
        Me.rbFrozenPeriod.Size = New System.Drawing.Size(90, 17)
        Me.rbFrozenPeriod.TabIndex = 1
        Me.rbFrozenPeriod.Text = "Frozen Period"
        Me.rbFrozenPeriod.UseVisualStyleBackColor = True
        '
        'rbUnfrozen
        '
        Me.rbUnfrozen.AutoSize = True
        Me.rbUnfrozen.Checked = True
        Me.rbUnfrozen.Location = New System.Drawing.Point(3, 3)
        Me.rbUnfrozen.Name = "rbUnfrozen"
        Me.rbUnfrozen.Size = New System.Drawing.Size(68, 17)
        Me.rbUnfrozen.TabIndex = 0
        Me.rbUnfrozen.TabStop = True
        Me.rbUnfrozen.Text = "Unfrozen"
        Me.rbUnfrozen.UseVisualStyleBackColor = True
        '
        'TxtDateFrozen
        '
        Me.TxtDateFrozen.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrozen.CustomFormat = ""
        Me.TxtDateFrozen.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDateFrozen.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.TxtDateFrozen.Location = New System.Drawing.Point(273, 11)
        Me.TxtDateFrozen.Name = "TxtDateFrozen"
        Me.TxtDateFrozen.Size = New System.Drawing.Size(96, 20)
        Me.TxtDateFrozen.TabIndex = 1
        Me.TxtDateFrozen.Value = New Date(2002, 11, 1, 7, 42, 30, 237)
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(8, 58)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(987, 529)
        Me.UltraTabControl1.TabIndex = 7
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Direct Ship Items"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Pulled From Inventory"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(983, 503)
        '
        'CmdExportToExcel
        '
        Me.CmdExportToExcel.Enabled = False
        Me.CmdExportToExcel.Location = New System.Drawing.Point(401, 10)
        Me.CmdExportToExcel.Name = "CmdExportToExcel"
        Me.CmdExportToExcel.Size = New System.Drawing.Size(105, 24)
        Me.CmdExportToExcel.TabIndex = 3
        Me.CmdExportToExcel.Text = "Export to Excel"
        '
        'FrmInventoryCostAnalysis
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(1007, 591)
        Me.Controls.Add(Me.CmdExportToExcel)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtDateFrozen)
        Me.Controls.Add(Me.CmdRefresh)
        Me.Name = "FrmInventoryCostAnalysis"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventory Cost Analysis"
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.UltraGridDirectShipItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.UltraGridPulledFromInventory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        Dim Parms As Object
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        Dim myDateSelect As String = ""

        If rbUnfrozen.Checked Then
            myDateSelect = " sofroz = 0 "
        ElseIf rbFrozenPeriod.Checked Then
            myDateSelect = " sofroz between " & Format(TxtDateFrozen.Value, "yyMM") & " and " & Format(TxtDateFrozen.Value, "yyMM") & " "
        End If

        Dim Daily_GenericDATA As ADODB.Recordset
        Daily_GenericDATA = connAS400.Execute("SELECT odinv#, oditem, odqshp, odavgc, oduuom, diqtpd, dicost, diref#, " & _
"dilin#, diven#, imavgc, imavgf, impuom, im2uom, im3uom, immul2, immul3, iminv1, iminv2, iminv3, sofroz, apfydt, apckno " & _
"FROM daily.soorddp left join daily.soapdip on " & _
"odinv# = diinv# and odlin# = dilin# left join daily.soinvp on odinv# = soinv# left join " & _
"daily.minvtp on oditem = imitem left join parafiles.apppay on diven# = apvnno and diref# = apinvn " & _
"WHERE not oddish = '' and sostat = 'I' and " & myDateSelect & _
" Order BY sofroz, oditem, odinv# ", Parms, -1)
        LoadDataSetDirectShip((Daily_GenericDATA))
        Me.UltraGridDirectShipItems.DataSource = DataSetInvoice

        Daily_GenericDATA = connAS400.Execute("SELECT odinv#, oditem, odqshp, odavgc, oduuom, impuom, im2uom, im3uom, immul2, immul3, iminv1, iminv2, iminv3, " & _
"imavgc, imavgf, sofroz FROM daily.soorddp " & _
"left join daily.soinvp on odinv# = soinv# left join " & _
"daily.minvtp on oditem = imitem WHERE oddish = ' ' and sostat = 'I' and " & myDateSelect & _
" Order BY sofroz, oditem, odinv# ", Parms, -1)
        LoadDataSetPulledFromInventory((Daily_GenericDATA))
        Me.UltraGridPulledFromInventory.DataSource = DataSetInvoice2

        CmdExportToExcel.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub CmdExportToExcel_Click(sender As System.Object, e As System.EventArgs) Handles CmdExportToExcel.Click
        If Me.UltraTabControl1.ActiveTab.Index = 0 Then
            ExportToExcelDirectShipItems()
        Else
            ExportToExcelPulledFromInventory()
        End If



    End Sub

#Region "Direct Ship"

    Public Sub LoadDataSetDirectShip(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetDirectShip()

        'Load the data
        While Not rs.EOF
            Dim InvoiceRow As DataRow = InvoiceTable.NewRow
            InvoiceRow("Invoice") = rs.Fields("odinv#").Value
            InvoiceRow("Item") = rs.Fields("oditem").Value
            InvoiceRow("Shipped") = rs.Fields("odqshp").Value
            InvoiceRow("UOM") = rs.Fields("ODUUOM").Value
            InvoiceRow("Inv Cost") = rs.Fields("odavgc").Value
            InvoiceRow("Paid") = rs.Fields("diqtpd").Value
            InvoiceRow("Pd Cost") = rs.Fields("dicost").Value
            If Not IsDBNull(rs.Fields("diqtpd").Value) And Not IsDBNull(rs.Fields("dicost").Value) Then
                InvoiceRow("Difference") = decimal.round((rs.Fields("odqshp").Value * rs.Fields("odavgc").Value) - (NoNullNumber(rs.Fields("diqtpd").Value) * NoNullNumber(rs.Fields("dicost").Value)), 2)
            Else
                InvoiceRow("Difference") = 0
            End If
            InvoiceRow("Avg Cost") = GetCost(rs.Fields("imavgc").Value + rs.Fields("imavgf").Value, rs.Fields("ODUUOM").Value, rs.Fields("impuom").Value, rs.Fields("im2uom").Value, rs.Fields("IMMUL2").Value, rs.Fields("im3uom").Value, rs.Fields("IMMUL3").Value, rs.Fields("IMinv1").Value, rs.Fields("IMinv2").Value, rs.Fields("IMinv3").Value)
            'InvoiceRow("Avg Cost") = rs.Fields("imavgc").Value
            InvoiceRow("Vend Inv") = rs.Fields("diref#").Value
            InvoiceRow("Line") = rs.Fields("dilin#").Value
            InvoiceRow("Vendor") = rs.Fields("diven#").Value
            InvoiceRow("Period") = rs.Fields("sofroz").Value

            InvoiceRow("Pay Period") = rs.Fields("apfydt").Value
            InvoiceRow("Check") = rs.Fields("apckno").Value

            InvoiceTable.Rows.Add(InvoiceRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSetDirectShip()

        DataSetInvoice.Dispose()
        InvoiceTable.Dispose()

        InvoiceTable = Nothing
        DataSetInvoice = Nothing

        InvoiceTable = New DataTable("Invoices")
        DataSetInvoice = New DataSet

        GC.Collect()

        ' build empty invoice table
        InvoiceTable.Columns.Add("Invoice", GetType(Integer))
        InvoiceTable.Columns.Add("Item", GetType(Integer))
        InvoiceTable.Columns.Add("UOM", GetType(String))
        InvoiceTable.Columns.Add("Shipped", GetType(Integer))
        InvoiceTable.Columns.Add("Inv Cost", GetType(decimal))
        InvoiceTable.Columns.Add("Paid", GetType(Integer))
        InvoiceTable.Columns.Add("Pd Cost", GetType(decimal))
        InvoiceTable.Columns.Add("Difference", GetType(decimal))
        InvoiceTable.Columns.Add("Avg Cost", GetType(decimal))
        InvoiceTable.Columns.Add("Vend Inv", GetType(String))
        InvoiceTable.Columns.Add("Line", GetType(Integer))
        InvoiceTable.Columns.Add("Vendor", GetType(Integer))
        InvoiceTable.Columns.Add("Period", GetType(Integer))
        InvoiceTable.Columns.Add("Pay Period", GetType(Integer))
        InvoiceTable.Columns.Add("Check", GetType(Long))

        ' build dataset
        DataSetInvoice.Tables.Add(InvoiceTable)

    End Sub


    Private Sub UltraGridDirectShipItems_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridDirectShipItems.InitializeLayout
        If UltraGridDirectShipItems.DisplayLayout.Bands(0).Summaries.Count = 0 Then
            Dim band As UltraGridBand = Me.UltraGridDirectShipItems.DisplayLayout.Bands(0)
            Dim Difference As SummarySettings = band.Summaries.Add("Difference", SummaryType.Sum, band.Columns("Difference"))

            ' Set the format of the summary text
            Difference.DisplayFormat = "{0:$##,###.00}"

            ' Change the appearance settings for summaries.
            Difference.Appearance.TextHAlign = HAlign.Right
            Difference.Appearance.BackColor = Drawing.Color.Tan

            ' Set the DisplayInGroupBy property of both summaries to false so they don't
            ' show up in group-by rows.
            Difference.SummaryDisplayArea = SummaryDisplayAreas.TopFixed


            ' Set the caption that shows up on the header of the summary footer.
            band.SummaryFooterCaption = "Total"
            band.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True
            band.Override.SummaryFooterCaptionAppearance.BackColor = Drawing.Color.DarkBlue
            band.Override.SummaryFooterCaptionAppearance.ForeColor = Drawing.Color.LightYellow

        End If

        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

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
        With UltraGridDirectShipItems
            '.DisplayLayout.Bands(0).Columns("Invoice").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .DisplayLayout.Bands(0).Columns("Inv Cost").Format = "$##,##0.0000"
            .DisplayLayout.Bands(0).Columns("Pd Cost").Format = "$###,##0.0000"
            .DisplayLayout.Bands(0).Columns("Difference").Format = "$###,##0.0000"
            .DisplayLayout.Bands(0).Columns("Avg Cost").Format = "$##,##0.0000"
            .DisplayLayout.Bands(0).Columns("UOM").Width = 40
        End With

        With UltraGridDirectShipItems.DisplayLayout.Bands(0)
            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With


    End Sub

    Private Sub ExportToExcelDirectShipItems()

        Dim SaveFileDialog1 As New System.Windows.Forms.SaveFileDialog
        SaveFileDialog1.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"

        SaveFileDialog1.AddExtension = True
        SaveFileDialog1.DefaultExt = "xlsx"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor


            Dim excel As New Microsoft.Office.Interop.Excel.Application
            Dim wBook As Microsoft.Office.Interop.Excel.Workbook
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

            wBook = excel.Workbooks.Add()
            wSheet = wBook.ActiveSheet()

            excel.Cells(1, 1) = "Invoice"
            excel.Cells(1, 2) = "Item"
            excel.Cells(1, 3) = "Shipped"
            excel.Cells(1, 4) = "Inv Cost"
            excel.Cells(1, 5) = "Paid"
            excel.Cells(1, 6) = "Pd Cost"
            excel.Cells(1, 7) = "Difference"
            excel.Cells(1, 8) = "Avg Cost"
            excel.Cells(1, 9) = "Vend Inv"
            excel.Cells(1, 10) = "Line"
            excel.Cells(1, 11) = "Vendor"
            excel.Cells(1, 12) = "Period"

            Dim i As Integer = 2

            For Each row As UltraGridRow In Me.UltraGridDirectShipItems.Rows

                excel.Cells(i, 1) = row.Cells("Invoice")
                excel.Cells(i, 2) = row.Cells("Item")
                excel.Cells(i, 3) = row.Cells("Shipped")
                excel.Cells(i, 4) = row.Cells("Inv Cost")
                excel.Cells(i, 5) = row.Cells("Paid")
                excel.Cells(i, 6) = row.Cells("Pd Cost")
                excel.Cells(i, 7) = row.Cells("Difference")
                excel.Cells(i, 8) = row.Cells("Avg Cost")
                excel.Cells(i, 9) = row.Cells("Vend Inv")
                excel.Cells(i, 10) = row.Cells("Line")
                excel.Cells(i, 11) = row.Cells("Vendor")
                excel.Cells(i, 12) = row.Cells("Period")
                i += 1
            Next

            wSheet.Columns.AutoFit()
            Dim strFileName As String = SaveFileDialog1.FileName
            Dim blnFileOpen As Boolean = False
            Try
                Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                fileTemp.Close()
            Catch ex As Exception
                blnFileOpen = False
            End Try

            If System.IO.File.Exists(strFileName) Then
                System.IO.File.Delete(strFileName)
            End If

            wBook.SaveAs(strFileName)
            If MsgBox("Do you want to attempt to open this file in Excel now?", MsgBoxStyle.YesNo, "Open file?") = MsgBoxResult.Yes Then
                Try
                    excel.Workbooks.Open(strFileName)
                    excel.Visible = True

                Catch ex As Exception
                    MsgBox("Unable to open file.", MsgBoxStyle.OkOnly, "Error")
                End Try
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If

    End Sub

#End Region

#Region "Pulled from Inventory"

    Public Sub LoadDataSetPulledFromInventory(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetPulledFromInventory()

        'Load the data
        While Not rs.EOF
            Dim InvoiceRow As DataRow = InvoiceTable2.NewRow
            InvoiceRow("Invoice") = rs.Fields("odinv#").Value
            InvoiceRow("Item") = rs.Fields("oditem").Value
            InvoiceRow("Shipped") = rs.Fields("odqshp").Value
            InvoiceRow("UOM") = rs.Fields("ODUUOM").Value
            InvoiceRow("Inv Cost") = rs.Fields("odavgc").Value
            ' InvoiceRow("Avg Cost") = rs.Fields("imavgc").Value
            InvoiceRow("Avg Cost") = GetCost(rs.Fields("imavgc").Value + rs.Fields("imavgf").Value, rs.Fields("ODUUOM").Value, rs.Fields("impuom").Value, rs.Fields("im2uom").Value, rs.Fields("IMMUL2").Value, rs.Fields("im3uom").Value, rs.Fields("IMMUL3").Value, rs.Fields("IMinv1").Value, rs.Fields("IMinv2").Value, rs.Fields("IMinv3").Value)
            ' InvoiceRow("Difference") = decimal.round((rs.Fields("odqshp").Value * rs.Fields("odavgc").Value) - (NoNullNumber(rs.Fields("odqshp").Value) * NoNullNumber(rs.Fields("imavgc").Value)), 2)
            InvoiceRow("Difference") = decimal.round((InvoiceRow("Shipped") * InvoiceRow("Inv Cost")) - (NoNullNumber(InvoiceRow("Shipped")) * NoNullNumber(InvoiceRow("Avg Cost"))), 2)
            InvoiceRow("Period") = rs.Fields("sofroz").Value

            InvoiceTable2.Rows.Add(InvoiceRow)

            rs.MoveNext()
        End While
        Dim myComment As String

    End Sub

    Public Sub BuildEmptyDataSetPulledFromInventory()

        DataSetInvoice2.Dispose()
        InvoiceTable2.Dispose()

        InvoiceTable2 = Nothing
        DataSetInvoice2 = Nothing

        InvoiceTable2 = New DataTable("Invoices2")
        DataSetInvoice2 = New DataSet

        GC.Collect()

        ' build empty invoice table
        InvoiceTable2.Columns.Add("Invoice", GetType(Integer))
        InvoiceTable2.Columns.Add("Item", GetType(Integer))
        InvoiceTable2.Columns.Add("UOM", GetType(String))
        InvoiceTable2.Columns.Add("Shipped", GetType(Integer))
        InvoiceTable2.Columns.Add("Inv Cost", GetType(decimal))
        InvoiceTable2.Columns.Add("Avg Cost", GetType(decimal))
        InvoiceTable2.Columns.Add("Difference", GetType(decimal))
        InvoiceTable2.Columns.Add("Period", GetType(Integer))

        ' build dataset
        DataSetInvoice2.Tables.Add(InvoiceTable2)

    End Sub


    Private Sub UltraGridPulledFromInventory_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridPulledFromInventory.InitializeLayout
        If UltraGridPulledFromInventory.DisplayLayout.Bands(0).Summaries.Count = 0 Then
            Dim band As UltraGridBand = Me.UltraGridPulledFromInventory.DisplayLayout.Bands(0)
            Dim Difference As SummarySettings = band.Summaries.Add("Difference", SummaryType.Sum, band.Columns("Difference"))

            ' Set the format of the summary text
            Difference.DisplayFormat = "{0:$##,###.00}"

            ' Change the appearance settings for summaries.
            Difference.Appearance.TextHAlign = HAlign.Right
            Difference.Appearance.BackColor = Drawing.Color.Tan

            ' Set the DisplayInGroupBy property of both summaries to false so they don't
            ' show up in group-by rows.
            Difference.SummaryDisplayArea = SummaryDisplayAreas.TopFixed


            ' Set the caption that shows up on the header of the summary footer.
            band.SummaryFooterCaption = "Total"
            band.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True
            band.Override.SummaryFooterCaptionAppearance.BackColor = Drawing.Color.DarkBlue
            band.Override.SummaryFooterCaptionAppearance.ForeColor = Drawing.Color.LightYellow

        End If
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

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
        With UltraGridPulledFromInventory
            .DisplayLayout.Bands(0).Columns("Inv Cost").Format = "$##,##0.000"
            .DisplayLayout.Bands(0).Columns("Avg Cost").Format = "$##,##0.000"
            .DisplayLayout.Bands(0).Columns("Difference").Format = "$##,##0.000"
            .DisplayLayout.Bands(0).Columns("UOM").Width = 40
        End With

        With UltraGridPulledFromInventory.DisplayLayout.Bands(0)
            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With


    End Sub

    Private Sub ExportToExcelPulledFromInventory()

        Dim SaveFileDialog1 As New System.Windows.Forms.SaveFileDialog
        SaveFileDialog1.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"

        SaveFileDialog1.AddExtension = True
        SaveFileDialog1.DefaultExt = "xlsx"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor


            Dim excel As New Microsoft.Office.Interop.Excel.Application
            Dim wBook As Microsoft.Office.Interop.Excel.Workbook
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

            wBook = excel.Workbooks.Add()
            wSheet = wBook.ActiveSheet()

            excel.Cells(1, 1) = "Invoice"
            excel.Cells(1, 2) = "Item"
            excel.Cells(1, 3) = "Shipped"
            excel.Cells(1, 4) = "Inv Cost"
            excel.Cells(1, 5) = "Avg Cost"
            excel.Cells(1, 6) = "Difference"
            excel.Cells(1, 7) = "Period"

            Dim i As Integer = 2

            For Each row As UltraGridRow In Me.UltraGridPulledFromInventory.Rows
                excel.Cells(i, 1) = row.Cells("Invoice")
                excel.Cells(i, 2) = row.Cells("Item")
                excel.Cells(i, 3) = row.Cells("Shipped")
                excel.Cells(i, 4) = row.Cells("Inv Cost")
                excel.Cells(i, 5) = row.Cells("Avg Cost")
                excel.Cells(i, 6) = row.Cells("Difference")
                excel.Cells(i, 7) = row.Cells("Period")
                i += 1
            Next

            wSheet.Columns.AutoFit()
            Dim strFileName As String = SaveFileDialog1.FileName
            Dim blnFileOpen As Boolean = False
            Try
                Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                fileTemp.Close()
            Catch ex As Exception
                blnFileOpen = False
            End Try

            If System.IO.File.Exists(strFileName) Then
                System.IO.File.Delete(strFileName)
            End If

            wBook.SaveAs(strFileName)
            If MsgBox("Do you want to attempt to open this file in Excel now?", MsgBoxStyle.YesNo, "Open file?") = MsgBoxResult.Yes Then
                Try
                    excel.Workbooks.Open(strFileName)
                    excel.Visible = True

                Catch ex As Exception
                    MsgBox("Unable to open file.", MsgBoxStyle.OkOnly, "Error")
                End Try
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If

    End Sub

#End Region


End Class
