Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging
Imports System.IO

Public Class ucNotes

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
            "from daily.msnotep "

        mySql &= "where account = " & myAccount

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
        Dim mymsNotep As New iSeries.msNotep
        With mymsNotep
            .Account = myAccount
            .Note = txtnote.Text
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

#End Region
End Class
