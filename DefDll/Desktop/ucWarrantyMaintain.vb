Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging
Imports System.IO

Public Class ucWarrantyMaintain
    Private myTabkey As String
    Public Event CloseMe(ByVal TabKey As String)
    Public Event AddWarranty(BackmsWarnp As iSeries.msWarnp)

    Private myWarrantyID As Integer
    Private myAccount As Integer
    Private myRow As Infragistics.Win.UltraWinGrid.UltraGridRow
    Private mymsWarnp As iSeries.msWarnp

    Public Sub New(ByVal inTabKey As String, inWarrantyID As Long, inAccount As Long, inRow As Infragistics.Win.UltraWinGrid.UltraGridRow, inVendorValueList As ValueList)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Cursor = Windows.Forms.Cursors.WaitCursor
        myTabkey = inTabKey
        myWarrantyID = inWarrantyID
        myAccount = inAccount
        myRow = inRow
        cbVendor.ValueList = inVendorValueList

        If Not inWarrantyID = 0 Then
            mymsWarnp = New iSeries.msWarnp
            With mymsWarnp
                .Retrieve(myWarrantyID)

                cbVendor.Value = .Vendor
                TxtItem.Text = .Item
                TxtSerial.Text = .Serial
                If Not .StartDate = 0 Then TxtStartDate.Value = GetDateFromNbr(.StartDate)
                If Not .EndDate = 0 Then TxtEndDate.Value = GetDateFromNbr(.EndDate)
                TxtLengthOfWarranty.Value = .LengthOfWarranty
                cbWarrantyPeriod.Value = .WarrantyPeriod
                TxtPO.Text = .PO
            End With

        End If

        RefreshNotes()
        Cursor = Windows.Forms.Cursors.Default

    End Sub

    Private Sub CmdRefresh_Click(sender As Object, e As EventArgs)
        Cursor = Windows.Forms.Cursors.WaitCursor
        RefreshNotes()
        Cursor = Windows.Forms.Cursors.Default
    End Sub

#Region "Notes"

    Private NotesTable As New DataTable("Notes")

    Private Sub RefreshNotes()
        Dim Daily_QryData As ADODB.Recordset
        Dim Parms As Object

        Dim mySql As String = "SELECT * " &
            "from daily.msWrnntp "

        mySql &= "where WarrantyID = " & myWarrantyID

        mySql &= " order by NoteDate desc"

        Daily_QryData = connAS400.Execute(mySql, Parms, -1)
        LoadDataNotes(Daily_QryData)
        UltraGridNotes.DataSource = NotesTable

    End Sub

    Public Sub LoadDataNotes(ByRef rs As ADODB.Recordset)

        BuildEmptyDataSetNotes()

        'Load the data
        While Not rs.EOF

            Dim NoteRow As DataRow = NotesTable.NewRow
            NoteRow("ID") = rs.Fields("ID").Value
            NoteRow("Note") = rs.Fields("Note").Value
            NoteRow("User") = rs.Fields("UserId").Value

            If Not rs.Fields("NoteDate").Value = 0 Then NoteRow("Date") = GetDateFromNbr(rs.Fields("NoteDate").Value)
            NoteRow("Time") = rs.Fields("NoteTime").Value

            NotesTable.Rows.Add(NoteRow)

            rs.MoveNext()
        End While

    End Sub

    Public Sub BuildEmptyDataSetNotes()
        NotesTable.Dispose()
        NotesTable = Nothing
        NotesTable = New DataTable("Notes")

        GC.Collect()

        NotesTable.Columns.Add("ID", GetType(Long))
        NotesTable.Columns.Add("Note", GetType(String))
        NotesTable.Columns.Add("User", GetType(String))

        NotesTable.Columns.Add("Date", GetType(Date))
        NotesTable.Columns.Add("Time", GetType(Integer))
    End Sub

    Private Sub UltraGridNotes_ClickCellButton(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridNotes.ClickCellButton

        Select Case e.Cell.Column.Key
            Case Is = "Note"

                'Dim myFrmInvoice As New Billing2013.FrmInvoice(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder)
                'myFrmInvoice.ShowMe(e.Cell.Value)
                'myFrmInvoice.Show()

        End Select

    End Sub

    Private Sub UltraGridNotes_InitializeLayout(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridNotes.InitializeLayout

        AddFilterToGrid(e)
        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

        e.Layout.Appearance.FontData.SizeInPoints = 8

        With UltraGridNotes.DisplayLayout.Bands(0)
            .Columns("Note").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button
            .Columns("Note").Width = 550
            .Columns("ID").Hidden = True
            .Columns("Time").Format = "00:00"

            For i As Int32 = 0 To .Columns.Count - 1
                .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
            Next i

        End With

    End Sub

    Private Sub CmdSave_Click(sender As Object, e As EventArgs) Handles CmdSave.Click
        If myWarrantyID = 0 Then
            If MsgBox("You have to create the warranty before you can add the note.  Do you want the warranty to be created now?", MsgBoxStyle.YesNo, "Warning") = MsgBoxResult.No Then Exit Sub
            UpdateWarranty()
        End If

        Dim mymsWrnntp As New iSeries.msWrnntp
        With mymsWrnntp
            .WarrantyID = myWarrantyID
            .Note = TxtNote.Text
            .NoteDate = Format(Now, "yyyyMMdd")
            .NoteTime = Format(Now, "HHmm")
            .Userid = UserId
            .Create()

            Dim NoteRow As DataRow = NotesTable.NewRow
            NoteRow("ID") = .ID
            NoteRow("Note") = .Note
            NoteRow("User") = .Userid

            If Not .NoteDate = 0 Then NoteRow("Date") = GetDateFromNbr(.NoteDate)
            NoteRow("Time") = .NoteTime

            NotesTable.Rows.Add(NoteRow)
        End With

        TxtNote.Text = ""
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAndCloseToolStripMenuItem.Click

        UpdateWarranty

        RaiseEvent CloseMe(myTabkey)

    End Sub

    Private Sub UpdateWarranty()
        If mymsWarnp Is Nothing Then
            mymsWarnp = New iSeries.msWarnp
        End If

        With mymsWarnp
            .Vendor = cbVendor.Value
            .Item = TxtItem.Text
            .Serial = TxtSerial.Text
            If Not TxtStartDate.Value Is Nothing Then .StartDate = Format(TxtStartDate.Value, "yyyyMMdd")
            If Not TxtEndDate.Value Is Nothing Then .EndDate = Format(TxtEndDate.Value, "yyyyMMdd")
            .LengthOfWarranty = TxtLengthOfWarranty.Value
            .WarrantyPeriod = cbWarrantyPeriod.Value
            .Account = myAccount
            .PO = TxtPO.Text

            If .ID = 0 Then
                .Create()
                myWarrantyID = mymsWarnp.ID
                RaiseEvent AddWarranty(mymsWarnp)
            Else
                .Update()
                myRow.Cells("ID").Value = .ID
                myRow.Cells("Vendor").Value = .Vendor
                myRow.Cells("Item").Value = .Item
                myRow.Cells("Serial").Value = .Serial
                If Not .StartDate = 0 Then myRow.Cells("Start").Value = GetDateFromNbr(.StartDate)
                If Not .EndDate = 0 Then myRow.Cells("End").Value = GetDateFromNbr(.EndDate)
                myRow.Cells("Length").Value = .LengthOfWarranty
                myRow.Cells("PO").Value = .PO
                myRow.Cells("Period").Value = .WarrantyPeriod

            End If
        End With
    End Sub


    Private Sub CloseWithoutSavingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseWithoutSavingToolStripMenuItem.Click
        RaiseEvent CloseMe(myTabkey)
    End Sub

    Private Sub CbWarrantyPeriod_ValueChanged(sender As Object, e As EventArgs) Handles cbWarrantyPeriod.ValueChanged
        CalculateEndDate()
    End Sub

    Private Sub TxtLengthOfWarranty_ValueChanged(sender As Object, e As EventArgs) Handles TxtLengthOfWarranty.ValueChanged
        CalculateEndDate()
    End Sub

    Private Sub TxtStartDate_ValueChanged(sender As Object, e As EventArgs) Handles TxtStartDate.ValueChanged
        CalculateEndDate()
    End Sub

    Private Sub CalculateEndDate()
        Select Case cbWarrantyPeriod.Value
            Case "D"
                TxtEndDate.Value = DateAdd(DateInterval.Day, TxtLengthOfWarranty.Value, TxtStartDate.Value)
            Case "W"
                TxtEndDate.Value = DateAdd(DateInterval.Day, TxtLengthOfWarranty.Value * 7, TxtStartDate.Value)
            Case "M"
                TxtEndDate.Value = DateAdd(DateInterval.Month, TxtLengthOfWarranty.Value, TxtStartDate.Value)
            Case "Y"
                TxtEndDate.Value = DateAdd(DateInterval.Year, TxtLengthOfWarranty.Value, TxtStartDate.Value)
            Case Else
                TxtEndDate.Value = TxtStartDate
        End Select
    End Sub

#End Region

End Class
