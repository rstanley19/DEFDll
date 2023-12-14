Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports System.Text

Module Module1
    Public mySetHandshakeShared As SharedDll.Class1
    Public Parms As Object
    Public UserName As String
    Public UserId As String
    Public UserPassword As String
    Public iSeriesIP As String
    Public myPrivateUserFolder As String

    Public SalesmanNumber As Integer
    Public SalesmanInitials As String
    Public smtp As String
    Public PassApplicationProductNameToDLL As String
    Public PassApplicationStartupPathToDLL As String
    Public myBinAppSettings As BinAppSettings
    Public myAppSettings As New AppSettings(AppSettings.Config.PrivateFile)
    Public connAS400 As New ADODB.Connection()

    ' save flags for document type assignment -frmdocumenttype
    Public BillDocumentNow As Int32 = 0
    Public BillImageLocationNow As Int32 = 0
    Public BillDocType As Int32 = 0
    Public saveDocType As Int32 = 1
    Public saveVerStat As Int32 = 1

    Public Function GetConnectionString() As String
        Return "Password=pDipOrts!;User Id=PortsPdi;Persist Security Info=True;Data Source=IMADEXWEB\sqlexpress;Initial Catalog=PortsPetroleum"
    End Function

    Public Function Quoted(ByVal s As Object) As String
        If s Is Nothing Then Return "''"
        If IsDBNull(s) Then Return "''"
        Return "'" & s.Replace("'", "''") & "'"
    End Function

    Public Function Quoted(ByVal s As Date) As String
        If IsDBNull(s) Then Return "''"
        Return "'" & s & "'"
    End Function

    Public Function Quoted(ByVal s As Boolean) As String
        Try
            Return "'" & s & "'"
        Catch
            Return "''"
        End Try
    End Function

    Public Function NoNullNumber(ByVal inValue As Object) As Decimal
        Try
            If IsNumeric(inValue) Then Return Convert.ToDecimal(inValue)
            Return 0
        Catch
            Return 0
        End Try
    End Function

    Public Function ConvertToSingleNoNull(ByVal inValue As Object) As Single
        If IsNumeric(inValue) Then Return Convert.ToSingle(inValue)

        Return 0

    End Function

    Public Function NoNullString(ByVal inString As Object) As String
        If IsDBNull(inString) Then Return ""
        Return inString
    End Function

    Public Function GetDateFromNbr(ByVal InNumber As String) As String
        Dim wrkNumberAsString As String

        If Len(Trim(InNumber)) = 0 Then
            GetDateFromNbr = ""
            Exit Function
        End If

        If InNumber = 0 Then
            GetDateFromNbr = ""
            Exit Function
        End If

        wrkNumberAsString = InNumber.ToString
        GetDateFromNbr = Mid(wrkNumberAsString, 5, 2) & "/" & Mid(wrkNumberAsString, 7, 2) & "/" & Mid(wrkNumberAsString, 3, 2)

    End Function

    Public Function GetFiscalYear(ByVal inDate As Date) As Integer
        If Format(inDate, "MM") > 7 Then
            Return Format(inDate, "yyyy") + 1
        Else
            Return Format(inDate, "yyyy")
        End If
    End Function

    Public Function GetFiscalPeriod(ByVal inDate As Date) As Integer
        If Format(inDate, "MM") > 7 Then
            Return Format(inDate, "MM") - 7
        Else
            Return Format(inDate, "MM") + 5
        End If
    End Function

    Public Function GetEOMDate(ByVal InDate As Date) As Date
        InDate = DateAdd(DateInterval.Month, 1, InDate)
        InDate = Format(InDate, "MM") & "/01/" & Format(InDate, "yy")
        GetEOMDate = DateAdd(DateInterval.Day, -1, InDate)
    End Function

    Public Function GetBOMDate(ByVal InDate As Date) As Date
        GetBOMDate = Format(InDate, "MM") & "/01/" & Format(InDate, "yy")
    End Function

    Public Function GetYesNo(ByVal inBoolean As Boolean) As String
        If inBoolean Then
            Return "Y"
        Else
            Return "N"
        End If
    End Function

    Public Function GetYesNo(ByVal inYorN As String) As Boolean
        If UCase(inYorN) = "Y" Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GetDataFromExcel(ByVal FileName As String, ByVal HasHeader As Boolean, inSheetName As String) As System.Data.DataSet
        ' Returns a DataSet containing information from a named range
        ' in the passed Excel worksheet
        Try
            Dim strConn As String =
                "Provider=Microsoft.Ace.OLEDB.12.0;" &
                "Data Source=" & FileName & "; Extended Properties=Excel 12.0;"

            If HasHeader Then
                strConn = "Provider=Microsoft.Ace.OLEDB.12.0;" &
                "Data Source=" & FileName & "; Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'"
            End If

            Dim objConn _
                As New System.Data.OleDb.OleDbConnection(strConn)
            objConn.Open()

            Dim dtExcelSchema As DataTable = objConn.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, Nothing)
            Dim sheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME")
            If Not Trim(sheetName) = "" Then
                sheetName = inSheetName
            End If

            ' Create objects ready to grab data
            Dim objCmd As New System.Data.OleDb.OleDbCommand(
                "SELECT * FROM [" & sheetName & "]", objConn)
            Dim objDA As New System.Data.OleDb.OleDbDataAdapter()
            objDA.SelectCommand = objCmd
            ' Fill DataSet
            Dim objDS As New System.Data.DataSet()
            objDA.Fill(objDS)
            ' Clean up and return DataSet
            objConn.Close()
            Return objDS
        Catch ex As Exception
            ' Possible errors include Excel file already open and
            ' locked, et al.
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

#Region "Value List"
    Public SalesmanValueList As Infragistics.Win.ValueList
    Public SalesmanTable As DataTable

    Public Sub QueryDataBaseForValueLists()

        Dim Daily_QryData As ADODB.Recordset

        If Not connAS400.State = 1 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
        End If

        Daily_QryData = connAS400.Execute("SELECT slslm#, slslnm from daily.msslmp WHERE not sldel = 'D' order by slslnm", Parms, -1)

        SalesmanValueList = New Infragistics.Win.ValueList()
        SalesmanValueList.ValueListItems.Clear()

        SalesmanTable = New DataTable("Salesman")
        SalesmanTable.Columns.Add("slslm#", GetType(Integer))
        SalesmanTable.Columns.Add("slslnm", GetType(String))

        While Not Daily_QryData.EOF
            SalesmanValueList.ValueListItems.Add(Daily_QryData.Fields("slslm#").Value, Daily_QryData.Fields("slslnm").Value)
            Dim SalesmanRow As DataRow = SalesmanTable.NewRow
            SalesmanRow("slslm#") = Daily_QryData.Fields("slslm#").Value
            SalesmanRow("slslnm") = Daily_QryData.Fields("slslnm").Value
            SalesmanTable.Rows.Add(SalesmanRow)
            Daily_QryData.MoveNext()
        End While

        GetClerk()
    End Sub

#Region "Clerk"
    Public ClerkValueList As Infragistics.Win.ValueList
    Public ClerkTable As DataTable

    Public Sub GetClerk()

        ClerkValueList = New Infragistics.Win.ValueList()
        ClerkValueList.ValueListItems.Clear()

        ClerkTable = New DataTable("Salesman")
        ClerkTable.Columns.Add("cmclrk", GetType(String))
        ClerkTable.Columns.Add("Name", GetType(String))

        Dim ClerkRow As DataRow = ClerkTable.NewRow
        ClerkRow("cmclrk") = "MARK"
        ClerkRow("Name") = "Mark Reynolds"
        ClerkTable.Rows.Add(ClerkRow)

        ClerkRow = ClerkTable.NewRow
        ClerkRow("cmclrk") = "SHERRYW"
        ClerkRow("Name") = "Sherry Wood"
        ClerkTable.Rows.Add(ClerkRow)

        ClerkRow = ClerkTable.NewRow
        ClerkRow("cmclrk") = "DENISE"
        ClerkRow("Name") = "Denise Hershberger"
        ClerkTable.Rows.Add(ClerkRow)

        ClerkRow = ClerkTable.NewRow
        ClerkRow("cmclrk") = "ACCOUNT"
        ClerkRow("Name") = "Accounting"
        ClerkTable.Rows.Add(ClerkRow)

        ClerkRow = ClerkTable.NewRow
        ClerkRow("cmclrk") = "CREDIT"
        ClerkRow("Name") = "Credit Unassigned"
        ClerkTable.Rows.Add(ClerkRow)




    End Sub
#End Region

#End Region

#Region "Add Filter to a grid"
    Public Sub AddFilterToGrid(e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs)

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
        e.Layout.Override.WrapHeaderText = DefaultableBoolean.True

    End Sub
#End Region

#Region "Image creation"

    Public Function CreateImageFolderLocation() As Long
        Dim myYyyyMM As Long = Format(Now, "yyyyMM")
        Dim myImfoldp As New iSeries.ImFoldp

        With myImfoldp
            .Retrieve(myYyyyMM)
            If Not .found Then
                .imKey = myYyyyMM
                .Create()
            End If

            Return .imKey
        End With
    End Function

    Public Function GetNxtLogNbrFlextStorImages() As Int32

        Dim CallQry2 As New ADODB.Command

        CallQry2.ActiveConnection = connAS400
        CallQry2.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc

        CallQry2.Parameters.Append(CallQry2.CreateParameter("", ADODB.DataTypeEnum.adChar, ADODB.ParameterDirectionEnum.adParamOutput, 9, "000000000"))
        CallQry2.CommandText = ("object.sor285asp")
        CallQry2.Execute()
        Return Val(CallQry2.Parameters(0).Value)

    End Function
#End Region

#Region "Make all controls read only or not enabled"
    Public Sub ChangeToReadOnly(ByVal inCtl As Windows.Forms.Control, inProtect As Boolean)
        Select Case TypeName(inCtl)
            Case Is = "TextBox"
                CType(inCtl, Windows.Forms.TextBox).ReadOnly = inProtect
            Case Is = "UltraMaskedEdit"
                CType(inCtl, Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit).ReadOnly = inProtect
            Case Is = "Label"
            Case Is = "UltraCombo"
                CType(inCtl, Infragistics.Win.UltraWinGrid.UltraCombo).ReadOnly = inProtect
            Case Is = "UltraCurrencyEditor"
                CType(inCtl, Infragistics.Win.UltraWinEditors.UltraCurrencyEditor).ReadOnly = inProtect
            Case Is = "UltraDateTimeEditor"
                CType(inCtl, Infragistics.Win.UltraWinEditors.UltraDateTimeEditor).ReadOnly = inProtect
            Case Is = "UltraComboEditor"
                CType(inCtl, Infragistics.Win.UltraWinEditors.UltraComboEditor).ReadOnly = inProtect
            Case Is = "UltraNumericEditor"
                CType(inCtl, Infragistics.Win.UltraWinEditors.UltraNumericEditor).ReadOnly = inProtect
            Case Is = "Button"
                CType(inCtl, Windows.Forms.Button).Enabled = Not (inProtect)
            Case Is = "RadioButton"
                CType(inCtl, Windows.Forms.RadioButton).Enabled = Not (inProtect)
            Case Is = "UltraGrid"
                'handling grid in the form (protect each gridbox)
                'CType(inCtl, Infragistics.Win.UltraWinGrid.UltraGrid).Enabled = False
            Case Is = "CheckBox"
                CType(inCtl, Windows.Forms.CheckBox).Enabled = Not (inProtect)
            Case Is = "RichTextBox"
                CType(inCtl, Windows.Forms.RichTextBox).ReadOnly = inProtect
            Case Is = "Panel"
            Case Is = "Splitter"
            Case Is = "PictureBox"
            Case Is = "UltraPictureBox"
            Case Is = "UltraTabControl"
            Case Is = "UltraTabSharedControlsPage"
            Case Is = "UltraTabPageControl"
            Case Is = "AutoHideControl"
            Case Is = "TabPage"
            Case Is = "TabControl"
            Case Is = "AutoHidePanel"
            Case Is = "AutoHideControl"
            Case Is = "LinkLabel"
            Case Is = "GroupBox"
            Case Is = "UnpinnedTabArea"
            Case Is = "RasterImageViewer"
            Case Is = "ComboBox"
            Case Is = "FpDateTime"
            Case Is = "Fpdecimal"
            Case Is = "ucContractsetup"
            Case Is = "ucContractSetup2015"
            Case Is = "ucContractSetupProduct"
            Case Is = "ucSupplierInvoice"
            Case Is = "EmbeddableTextBoxWithUIPermissions"
            Case Is = "ucPipelineDelivery"
            Case Is = "ucPipelineReceipt"
            Case Is = "ucBOLReceipt"
            Case Is = "ucMonthlyTerminal"
            Case Is = "ucTransfer"
            Case Is = "ucShowProductDetail"
            Case Is = "ucShowTerminalDetail"
            Case Is = "ucShowTransfers"
            Case Is = "ucShowBillingDetail"
            Case Is = "ucAdjustBillingCost"
            Case Else
                MsgBox("Control needs added to ChangeToReadOnly in Module1.  Control is " & TypeName(inCtl))
                '("Ricks@Fuelmart.com", "Control needs added to ChangeToReadOnly in DA400Links.  Control is " & TypeName(inCtl))
        End Select


        For Each childCtl As Windows.Forms.Control In inCtl.Controls
            ChangeToReadOnly(childCtl, inProtect)
            If inCtl.Name = "ucContractSetup1" And childCtl.Name = "cbSupplier" Then
                CType(childCtl, Infragistics.Win.UltraWinEditors.UltraComboEditor).ReadOnly = Not (inProtect)
            End If
            If inCtl.Name = "ucContractSetupProduct" And childCtl.Name = "cbPullPoint" Then
                CType(childCtl, Infragistics.Win.UltraWinEditors.UltraComboEditor).ReadOnly = Not (inProtect)
            End If
        Next

    End Sub
#End Region

#Region "Dispose of all controls within a control"
    Public Sub DisposeOfAllControls(ByVal inCtl As Windows.Forms.Control)
        Try


            For Each childCtl As Windows.Forms.Control In inCtl.Controls
                DisposeOfAllControls(childCtl)
            Next

            'If TypeOf (inCtl) Is Infragistics.Win.Misc.UltraPanel Then
            If TypeOf (inCtl) Is Infragistics.Win.Misc.UltraPanelClientArea Then
                Exit Sub
            End If

            inCtl.Dispose()
        Catch
            'MsgBox("Here")
        End Try
    End Sub

#End Region

#Region "Customer Search "

    Public SearchTable As New DataTable("Search")
    Public StillBuildingSearch As Boolean = True

    Public Sub QueryDataBase()
        StillBuildingSearch = True
        Dim connAS400 As New ADODB.Connection
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object
        connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)

        Daily_QryData = connAS400.Execute("SELECT cmblto, cmname FROM daily.mscustp " &
        "where cmacc# = cmblto and cmdel = '' order by cmname", Parms, -1)
        LoadDataBillTo(Daily_QryData)
        StillBuildingSearch = False
        connAS400.Close()
        'Me.BeginInvoke(CallDataBindToDataGrid)

    End Sub

    'Public Sub DataBindToDataGrid()
    '    UltraGridSearch.DataSource = DataSetSearch
    '    Me.LabBuilding.Visible = False
    '    Me.UltraGridSearch.Visible = True
    '    Me.UltraTabControl1.Tabs(1).Appearance.ForeColor = Drawing.Color.Black

    'End Sub

    Private Sub LoadDataBillTo(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF

            Dim SearchRow As DataRow = SearchTable.NewRow
            SearchRow("Account") = rs.Fields("cmblto").Value
            SearchRow("Name") = rs.Fields("cmname").Value

            SearchTable.Rows.Add(SearchRow)

            rs.MoveNext()
        End While

    End Sub

    Private Sub BuildEmptyDataSet()

        SearchTable.Dispose()
        SearchTable = Nothing
        SearchTable = New DataTable("Search")

        GC.Collect()

        SearchTable.Columns.Add("Account", GetType(Integer))
        SearchTable.Columns.Add("Name", GetType(String))

    End Sub

#End Region

#Region "Load Notes"
    Public Sub LoadListBoxNotes(ByRef lb As System.Windows.Forms.ListBox, ByRef rs As ADODB.Recordset)
        Dim wrkLine As String()

        lb.Items.Clear()

        'Load the data
        While Not rs.EOF
            lb.Items.Add(rs.Fields("ihinv#").Value & " " & rs.Fields("ihuser").Value _
            & " " & GetDateFromNbr(Convert.ToString(rs.Fields("ihdate").Value)) _
            & " " & Format(rs.Fields("ihtime").Value, "00:00") _
            & " " & rs.Fields("ihnote").Value)
            rs.MoveNext()
        End While

    End Sub
#End Region

#Region "Get Document Type"
    Public Function GetDocType(ByVal InType As Int32) As String
        'also change doc table in 400
        'also change getdoctype in billing.frmDocumentType
        'also change getdoctype in billing.da400links.vb

        'billing document
        If InType = 1 Then Return "Sales Order from carrier"
        If InType = 2 Then Return "Bill of Lading"
        If InType = 3 Then Return "Carrier Delivery Receipt"
        If InType = 4 Then Return "Customer Delivery Receipt"
        If InType = 5 Then Return "Supplier Invoice"
        If InType = 6 Then Return "Freight Invoice"
        If InType = 7 Then Return "Contract"
        If InType = 8 Then Return "Automatic Tank Gauging"
        If InType = 9 Then Return "Carrier Invoice"
        If InType = 10 Then Return "Carrier Bill of Lading"
        If InType = 11 Then Return "Carrier Delivery Receipt"
        If InType = 12 Then Return "Carrier Tax Diversion"
        If InType = 13 Then Return "Location Compliance"
        If InType = 14 Then Return "Summary Invoice"
        If InType = 15 Then Return "Meter Ticket"
        If InType = 16 Then Return "Scale Ticket"
        If InType = 17 Then Return "Def Document"
        If InType = 18 Then Return "Invoice"
        If InType = 19 Then Return "DLA Orders"
        If InType = 20 Then Return "PO's"
        If InType = 21 Then Return "Credit Card Receipt"
        If InType = 22 Then Return "TABS"
        If InType = 23 Then Return "Freight Only Invoice"
        If InType = 24 Then Return "Transportation Invoice"
        If InType = 25 Then Return "NY Manifest"
        If InType = 26 Then Return "Outside Freight"

        ' contract document
        If InType = 101 Then Return "Supplier Invoice"
        If InType = 102 Then Return "Pipeline Delivery"
        If InType = 103 Then Return "Pipeline Receipt"
        If InType = 104 Then Return "Bill of Lading"
        If InType = 105 Then Return "Pipeline Invoice"
        If InType = 106 Then Return "Carrier Freight"
        If InType = 107 Then Return "Monthly Terminal Settlement"
        If InType = 108 Then Return "BOL Transfer"

        ' Direct Ship PO document
        If InType = 201 Then Return "Direct Ship Invoice"
        If InType = 202 Then Return "Direct Ship Credit"
        If InType = 203 Then Return "Direct Ship Backup"

        Return "Unknown"
    End Function

#End Region

#Region "Get Cost"
    Public Function GetCost(inAverageCost As Decimal, inUom As String, inPuom As String, inUom2 As String, inMultiplier2 As Integer, inUom3 As String, inMultiplier3 As Integer, inInv1 As String, inInv2 As String, inInv3 As String) As Decimal


        Select Case inUom
            Case Is = inPuom
                Return inAverageCost
            Case Is = inUom2
                If inInv1 = "Y" Then
                    Return inAverageCost * inMultiplier2
                Else
                    Return inAverageCost
                End If
            Case Is = inUom3
                If inInv1 = "Y" Or inInv2 = "Y" Then
                    Return inAverageCost * inMultiplier3
                Else
                    Return inAverageCost
                End If
        End Select

        Return inAverageCost

    End Function

#End Region
End Module
