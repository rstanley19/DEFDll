Imports System.Data.OleDb
Imports Crownwood.DotNetMagic.Common
Imports Crownwood.DotNetMagic.Docking
Imports System.Drawing

Public Class ucItem
    Public Event CloseMe(ByVal TabKey As String, ReturnToTab As Integer)
    Private myTabkey As String
    Private myReturnToTab As Integer
    Private myMinvtp As SharedDll.Iseries.mInvtp
    Private myMiLocp As SharedDll.Iseries.miLocp
    Private myRow As Infragistics.Win.UltraWinGrid.UltraGridRow

    Public Sub New(ByVal inTabKey As String, inReturnToTab As Integer, inVendorsTable As DataTable, inItem As Integer, inRow As Infragistics.Win.UltraWinGrid.UltraGridRow, Optional inMinvtp As SharedDll.Iseries.mInvtp = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        myTabkey = inTabKey
        myReturnToTab = inReturnToTab
        myMinvtp = inMinvtp
        myMiLocp = New SharedDll.Iseries.miLocp(0, inItem)
        myRow = inRow

        cbImVend.DataSource = inVendorsTable
        cbImVend.ValueMember = inVendorsTable.Columns("Vendor").ColumnName
        cbImVend.DisplayMember = inVendorsTable.Columns("Name").ColumnName
        cbImVend.Text = ""

        TxtFrom.Value = DateAdd(DateInterval.Day, -1, Now)
        TxtTo.Value = Now
        TxtQFrom.Value = DateAdd(DateInterval.Day, -1, Now)
        TxtQTo.Value = Now

        If Not myMinvtp Is Nothing Then
            RefreshMe()
        Else
            TxtImItem.Value = inItem
        End If

        TxtImQoh.ReadOnly = True
        TxtImAvgc.ReadOnly = True
        TxtImAvgf.ReadOnly = True

    End Sub

    Private Sub RefreshMe()

        With myMinvtp
            TxtImItem.Value = .ImItem
            cbImStat.Value = .imStat
            TxtImDes1.Text = .imDes1
            TxtImPuom.Text = .imPuom
            cbImInv1.Checked = GetYesNo(.imInv1)
            cbImPo1.Checked = GetYesNo(.imPo1)
            cbImDsb1.Checked = GetYesNo(.imDsb1)
            TxtIm2uom.Text = .im2uom
            cbImInv2.Checked = GetYesNo(.imInv2)
            cbImPo2.Checked = GetYesNo(.imPo2)
            cbImDsb2.Checked = GetYesNo(.imDsb2)
            cbImVend.Value = .imVend
            TxtImSize.Text = .imSize
            TxtIlSect.Text = myMiLocp.ilSect
            TxtIlSlot.Value = myMiLocp.ilslot
            TxtImStdc.Value = .imStdc
            TxtImAvgc.Value = .imAvgc
            TxtImAvgf.Value = .imAvgf
            TxtImGals.Value = .imGals
            TxtImQoh.Value = .imQtoh

        End With
        'ChangeToReadOnly(UltraTabPageControl1, True)


    End Sub

    Private Sub AllowCostOnHandUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllowCostOnHandUpdateToolStripMenuItem.Click
        Dim myPoSecdp As New SharedDll.Iseries.PoSecdp
        myPoSecdp.GetSecurity(UserId, "CHGITEM")
        If myPoSecdp.found Then
            TxtImQoh.ReadOnly = False
            TxtImAvgc.ReadOnly = False
            TxtImAvgf.ReadOnly = False
        Else
            MsgBox("Sorry you are not authorized to change the quantity on hand or the costs", MsgBoxStyle.OkOnly, "Sorry")
        End If
    End Sub

    Private Sub UltraTabControl2_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl2.ActiveTabChanged
        If e.Tab.Text = "Activity" Then
            If UltraGridHistory.Rows.Count = 0 Then
                RebuildInvoices()
            End If
        ElseIf e.Tab.Text = "QOH / Cost Changes" Then
            If UltraGridQHistory.Rows.Count = 0 Then
                RebuildQInvoices()
            End If
        End If

    End Sub

    Private Sub CancelUpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelUpdateToolStripMenuItem.Click
        RaiseEvent CloseMe(myTabkey, myReturnToTab)
    End Sub

#Region "Update"
    Private Sub UpdateItem()
        If myMinvtp Is Nothing Then
            myMinvtp = New SharedDll.Iseries.mInvtp
            myMinvtp.ImItem = TxtImItem.Value
        End If

        With myMinvtp
            .imStat = cbImStat.Value
            .imDes1 = TxtImDes1.Text
            .imPuom = TxtImPuom.Text
            .imInv1 = GetYesNo(cbImInv1.Checked)
            .imPo1 = GetYesNo(cbImPo1.Checked)
            .imDsb1 = GetYesNo(cbImDsb1.Checked)
            .im2uom = TxtIm2uom.Text
            .imInv2 = GetYesNo(cbImInv2.Checked)
            .imPo2 = GetYesNo(cbImPo2.Checked)
            .imDsb2 = GetYesNo(cbImDsb2.Checked)
            .imVend = cbImVend.Value
            .imSize = TxtImSize.Text

            .imStdc = TxtImStdc.Value
            .imGals = TxtImGals.Value
            If TxtImQoh.ReadOnly = False Then
                Dim mymiHstp As New SharedDll.Iseries.miHstp
                mymiHstp.ihItem = .ImItem
                mymiHstp.ihInv_ = 0
                mymiHstp.ihDate = Convert.ToInt64(Format(Now, "yyyyMMdd"))
                mymiHstp.ihTime = Format(Now, "HHmm")
                mymiHstp.ihUser = UserId
                mymiHstp.ihSyst = "!"

                If Not .imAvgc = TxtImAvgc.Value Then
                    mymiHstp.ihNote = "Manual Average Cost Change - was " & .imAvgc & " now " & TxtImAvgc.Value
                    .imAvgc = TxtImAvgc.Value
                    mymiHstp.Create()
                End If

                If Not .imAvgf = TxtImAvgf.Value Then
                    mymiHstp.ihNote = "Manual Average Frt Cost Change - was " & .imAvgf & " now " & TxtImAvgf.Value
                    .imAvgf = TxtImAvgf.Value
                    mymiHstp.Create()
                End If

                If Not .imQtoh = TxtImQoh.Value Then
                    mymiHstp.ihNote = "Manual Qty On Hand Change - was " & .imQtoh & " now " & TxtImQoh.Value
                    .imQtoh = TxtImQoh.Value
                    mymiHstp.Create()
                End If


            End If

            If .found = False Then
                .Create()
            Else
                If TxtImQoh.ReadOnly = False Then
                    .Update()
                Else
                    .UpdateButNotQuantityOrPrice()
                End If
            End If

            If Not myRow Is Nothing Then
                myRow.Cells("Status").Value = .imStat
                If .imStat = "D" Then myRow.Cells("Status").Value = "Deleted"
                myRow.Cells("Item").Value = .ImItem
                myRow.Cells("Description").Value = .imDes1
                myRow.Cells("Standard Cost").Value = .imStdc
                myRow.Cells("Average Cost").Value = .imAvgc
                myRow.Cells("Average Freight").Value = .imAvgf
                myRow.Cells("On Hand").Value = .imQtoh
                myRow.Cells("Vendor #").Value = .imVend
                myRow.Cells("Vendor Name").Value = cbImVend.Text
            End If
        End With

        With myMiLocp
            .ilSect = TxtIlSect.Text
            .ilSlot = TxtIlSlot.Value
            If .found = False Then
                .Create()
            Else
                .Update()
            End If
        End With

    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click
        UpdateItem()
        RaiseEvent CloseMe(myTabkey, myReturnToTab)
    End Sub
#End Region

#Region "Activity"

#Region "Rebuild Data "
    Private Sub CmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRefresh.Click
        RebuildInvoices()
    End Sub

    Private InvoicesTable As New DataTable("Invoices")

    Private Sub RebuildInvoices()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        CmdPrint.Enabled = False

        Dim Parms As Object
        connAS400.CommandTimeout = 0
        Dim As400Data As ADODB.Recordset

        Dim mySql As String = ""


        mySql = "SELECT * from daily.mihstp " &
                                      " where ihdate between " & Format(TxtFrom.Value, "yyyyMMdd") & " and " & Format(TxtTo.Value, "yyyyMMdd") &
                                      " order by ihitem, ihdate"

        As400Data = connAS400.Execute(mySql, Parms, -1)

        LoadDataSetAR(As400Data)
        UltraGridHistory.DataSource = InvoicesTable
        If UltraGridHistory.Rows.Count > 0 Then Me.CmdPrint.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Public Sub LoadDataSetAR(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSet()

        'Load the data
        While Not rs.EOF
            Dim InvoicesRow As DataRow = InvoicesTable.NewRow
            InvoicesRow("Item") = rs.Fields("ihitem").Value
            InvoicesRow("Invoice") = rs.Fields("ihinv#").Value
            InvoicesRow("Date") = GetDateFromNbr(rs.Fields("ihdate").Value)
            InvoicesRow("Time") = rs.Fields("ihtime").Value
            InvoicesRow("User") = rs.Fields("ihuser").Value
            InvoicesRow("Note") = rs.Fields("ihNote").Value
            InvoicesRow("System") = rs.Fields("ihsyst").Value
            InvoicesTable.Rows.Add(InvoicesRow)
            rs.MoveNext()
        End While
        rs.Close()
        rs = Nothing
    End Sub

    Public Sub BuildEmptyDataSet()
        InvoicesTable.Dispose()
        InvoicesTable = Nothing
        InvoicesTable = New DataTable("Records")

        GC.Collect()

        InvoicesTable.Columns.Add("Item", GetType(Long))
        InvoicesTable.Columns.Add("Invoice", GetType(Long))
        InvoicesTable.Columns.Add("Date", GetType(Date))
        InvoicesTable.Columns.Add("Time", GetType(Integer))
        InvoicesTable.Columns.Add("User", GetType(String))
        InvoicesTable.Columns.Add("Note", GetType(String))
        InvoicesTable.Columns.Add("System", GetType(String))

    End Sub

    Private Sub UltraGridHistory_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridHistory.InitializeLayout
        AddFilterToGrid(e)

        With UltraGridHistory.DisplayLayout.Bands(0)

            .Columns("Note").Width = 250

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

#End Region

#Region "Print"
    Private Sub CmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles CmdPrint.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Dim myRecordsTable As New DataTable("Records")

        myRecordsTable.Columns.Add("Item", GetType(Long))
        myRecordsTable.Columns.Add("Invoice", GetType(Long))
        myRecordsTable.Columns.Add("Date", GetType(Date))
        myRecordsTable.Columns.Add("Time", GetType(Integer))
        myRecordsTable.Columns.Add("User", GetType(String))
        myRecordsTable.Columns.Add("Note", GetType(String))
        myRecordsTable.Columns.Add("System", GetType(String))


        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.UltraGridHistory.Rows.GetFilteredInNonGroupByRows()
            Dim RecordsRow As DataRow = myRecordsTable.NewRow
            RecordsRow("Item") = row.Cells("Item").Value
            RecordsRow("Invoice") = row.Cells("Invoice").Value
            RecordsRow("Date") = row.Cells("Date").Value
            RecordsRow("Time") = row.Cells("Time").Value
            RecordsRow("User") = row.Cells("User").Value
            RecordsRow("Note") = row.Cells("Note").Value
            RecordsRow("System") = row.Cells("System").Value

            myRecordsTable.Rows.Add(RecordsRow)
        Next

        'Get the Report Location
        Dim strReportPath As String = PassApplicationStartupPathToDLL & "\RPTFiles" & "\rpItemHistory.rpt"

        'Check file exists
        If Not IO.File.Exists(strReportPath) Then
            Throw (New Exception("Unable to locate report file:" & vbCrLf & strReportPath))
        End If

        Dim rptDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

        rptDocument.Load(strReportPath)

        rptDocument.SetDataSource(myRecordsTable)
        rptDocument.SetParameterValue("From", Format(TxtFrom.Value, "short date"))
        rptDocument.SetParameterValue("To", Format(TxtTo.Value, "short date"))

        Dim myFrmCrystalViewer As New FrmCrystalViewer("Item History", rptDocument)
        myFrmCrystalViewer.Show()

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

#End Region
#End Region

#Region "Activity QOH / Cost"

#Region "Rebuild Data "
    Private Sub CmdQRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdQRefresh.Click
        RebuildQInvoices()
    End Sub

    Private QInvoicesTable As New DataTable("Records")

    Private Sub RebuildQInvoices()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        CmdPrint.Enabled = False

        Dim Parms As Object
        connAS400.CommandTimeout = 0
        Dim As400Data As ADODB.Recordset

        Dim mySql As String = ""


        mySql = "SELECT * from daily.mihstp 
                                    where ihsyst = '!' and ihdate between " & Format(TxtFrom.Value, "yyyyMMdd") & " and " & Format(TxtTo.Value, "yyyyMMdd") &
                                      " order by ihitem, ihdate"

        As400Data = connAS400.Execute(mySql, Parms, -1)

        LoadDataSetQ(As400Data)
        UltraGridQHistory.DataSource = QInvoicesTable
        'If UltraGridHistory.Rows.Count > 0 Then Me.CmdPrint.Enabled = True

        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Public Sub LoadDataSetQ(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetQ()

        'Load the data
        While Not rs.EOF
            Dim InvoicesRow As DataRow = QInvoicesTable.NewRow
            InvoicesRow("Item") = rs.Fields("ihitem").Value
            InvoicesRow("Date") = GetDateFromNbr(rs.Fields("ihdate").Value)
            InvoicesRow("Time") = rs.Fields("ihtime").Value
            InvoicesRow("User") = rs.Fields("ihuser").Value
            InvoicesRow("Note") = rs.Fields("ihNote").Value
            QInvoicesTable.Rows.Add(InvoicesRow)
            rs.MoveNext()
        End While
        rs.Close()
        rs = Nothing
    End Sub

    Public Sub BuildEmptyDataSetQ()
        QInvoicesTable.Dispose()
        QInvoicesTable = Nothing
        QInvoicesTable = New DataTable("Records")

        GC.Collect()

        QInvoicesTable.Columns.Add("Item", GetType(Long))
        QInvoicesTable.Columns.Add("Date", GetType(Date))
        QInvoicesTable.Columns.Add("Time", GetType(Integer))
        QInvoicesTable.Columns.Add("User", GetType(String))
        QInvoicesTable.Columns.Add("Note", GetType(String))

    End Sub

    Private Sub UltraGridQHistory_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridQHistory.InitializeLayout
        AddFilterToGrid(e)

        With UltraGridQHistory.DisplayLayout.Bands(0)

            .Columns("Note").Width = 400

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

#End Region

    '#Region "Print"
    '    Private Sub CmdPrint_Click(sender As System.Object, e As System.EventArgs) Handles CmdPrint.Click
    '        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

    '        Dim myRecordsTable As New DataTable("Records")

    '        myRecordsTable.Columns.Add("Item", GetType(Long))
    '        myRecordsTable.Columns.Add("Invoice", GetType(Long))
    '        myRecordsTable.Columns.Add("Date", GetType(Date))
    '        myRecordsTable.Columns.Add("Time", GetType(Integer))
    '        myRecordsTable.Columns.Add("User", GetType(String))
    '        myRecordsTable.Columns.Add("Note", GetType(String))
    '        myRecordsTable.Columns.Add("System", GetType(String))


    '        For Each row As Infragistics.Win.UltraWinGrid.UltraGridRow In Me.UltraGridHistory.Rows.GetFilteredInNonGroupByRows()
    '            Dim RecordsRow As DataRow = myRecordsTable.NewRow
    '            RecordsRow("Item") = row.Cells("Item").Value
    '            RecordsRow("Invoice") = row.Cells("Invoice").Value
    '            RecordsRow("Date") = row.Cells("Date").Value
    '            RecordsRow("Time") = row.Cells("Time").Value
    '            RecordsRow("User") = row.Cells("User").Value
    '            RecordsRow("Note") = row.Cells("Note").Value
    '            RecordsRow("System") = row.Cells("System").Value

    '            myRecordsTable.Rows.Add(RecordsRow)
    '        Next

    '        'Get the Report Location
    '        Dim strReportPath As String = PassApplicationStartupPathToDLL & "\RPTFiles" & "\rpItemHistory.rpt"

    '        'Check file exists
    '        If Not IO.File.Exists(strReportPath) Then
    '            Throw (New Exception("Unable to locate report file:" & vbCrLf & strReportPath))
    '        End If

    '        Dim rptDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    '        rptDocument.Load(strReportPath)

    '        rptDocument.SetDataSource(myRecordsTable)
    '        rptDocument.SetParameterValue("From", Format(TxtFrom.Value, "short date"))
    '        rptDocument.SetParameterValue("To", Format(TxtTo.Value, "short date"))

    '        Dim myFrmCrystalViewer As New FrmCrystalViewer("Item History", rptDocument)
    '        myFrmCrystalViewer.Show()

    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '    End Sub

    '#End Region
#End Region

End Class
