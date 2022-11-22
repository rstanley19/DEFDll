Imports System.IO
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.SqlClient
Imports Infragistics.Win
Imports Infragistics.Win.UltraWinGrid
Imports Crownwood.DotNetMagic.Common
Imports Crownwood.DotNetMagic.Docking

Public Class ucCustomerInquiry

    Public Event CloseMe()

    Dim myCustomerProfileSoNotep As SalesOrder2013.iSeries.soNotep
    Dim myCustomerNoteSoNotep As SalesOrder2013.iSeries.soNotep

    Private mymInvtp As SalesOrder2013.iSeries.mInvtp

    Private myCustomer As Long

    Public Sub New(ByVal inCustomer As Long)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        SelectCustomer(inCustomer)

        CheckSecurity()

    End Sub


    Private Sub CheckSecurity()
        Dim Daily_PoSecdlQry As New ADODB.Command()
        Dim Daily_PoSecdlDATA As ADODB.Recordset

        With Daily_PoSecdlQry
            .ActiveConnection = connAS400
            .CommandText = "select * from daily.posecdl where sduser = '" & UserId & "' and SdProc = " & Quoted("CUSTPROF") & " for read only optimize for 1 rows"
            .Prepared = False
            Daily_PoSecdlDATA = Daily_PoSecdlQry.Execute
        End With
        If Daily_PoSecdlDATA.EOF Then
            TxtCustomerProfileNote.ReadOnly = True
            TxtCustomerNote.ReadOnly = True
        Else
            TxtCustomerProfileNote.ReadOnly = False
            TxtCustomerNote.ReadOnly = False
        End If
    End Sub


    Private Sub SelectCustomer(inCustomer As Integer)

        LabCustomer.Text = inCustomer
        myCustomer = inCustomer

        ' removed 01/18/16
        'Dim myAppVnd As New iSeries.apPvnd(inCustomer)
        'If Not myAppVnd Is Nothing Then
        '    With myAppVnd
        '        LabCustomerDescription.Text = .vndmnm & vbCrLf & .vnadd1 & vbCrLf & _
        '           .vnadd5 & " " & Trim(.vnstte) & "  " & Format(.vnzpcd, "00000") & _
        '           vbCrLf & .vntele
        '    End With
        'End If

        ' added 01/18/16
        Dim myMsCustp As New iSeries.msCustp(inCustomer, False)
        If Not myMsCustp Is Nothing Then
            With myMsCustp
                LabCustomerDescription.Text = .cmName & vbCrLf & .cmAdr1 & vbCrLf & _
                   .cmCity & " " & Trim(.cmSt) & "  " & Format(.cmZip, "00000") & _
                   vbCrLf & .cmTel_
            End With
        End If

        myCustomerProfileSoNotep = New SalesOrder2013.iSeries.soNotep("CustPro", inCustomer)
        If myCustomerProfileSoNotep.found Then
            TxtCustomerProfileNote.Text = myCustomerProfileSoNotep.snNote
        Else
            TxtCustomerProfileNote.Text = ""
        End If

        myCustomerNoteSoNotep = New SalesOrder2013.iSeries.soNotep("CustNote", inCustomer)
        If myCustomerNoteSoNotep.found Then
            TxtCustomerNote.Text = myCustomerNoteSoNotep.snNote
        Else
            TxtCustomerNote.Text = ""
        End If

        'RefreshEmail()

    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles CloseToolStripMenuItem.Click
        If myCustomer = 0 Then Exit Sub
        'If EditEmail() = False Then Exit Sub

        UpdateCustomer()

        RaiseEvent CloseMe()


    End Sub

    Private Sub UpdateCustomer()
        If TxtCustomerProfileNote.ReadOnly Then Exit Sub
        If myCustomer = 0 Then Exit Sub

        If Not Trim(myCustomerProfileSoNotep.snNote) = Trim(TxtCustomerProfileNote.Text) Then
            With myCustomerProfileSoNotep
                If Trim(TxtCustomerProfileNote.Text) = "" Then
                    If .found Then
                        .Delete()
                    End If
                Else
                    .snNote = TxtCustomerProfileNote.Text
                    If .found Then
                        .Update()
                    Else
                        .Create()
                    End If
                End If
            End With
        End If


        If Not Trim(myCustomerNoteSoNotep.snNote) = Trim(TxtCustomerNote.Text) Then
            With myCustomerNoteSoNotep
                If Trim(TxtCustomerNote.Text) = "" Then
                    If .found Then
                        .Delete()
                    End If
                Else
                    .snNote = TxtCustomerNote.Text
                    If .found Then
                        .Update()
                    Else
                        .Create()
                    End If
                End If
            End With
        End If

        'UpdateEmail()
    End Sub

    '#Region "Email"
    '    ' Email section
    '    Dim EmailTable As New DataTable("Email")
    '    Dim EmailDataSet As New DataSet
    '    Dim eMailUpdated As Boolean

    '    Private Sub RefreshEmail()

    '        Dim connAS400 As New ADODB.Connection
    '        Dim Daily_QryData As ADODB.Recordset
    '        Dim Parms As Object
    '        connAS400.Open("Provider=IBMDA400;Data Source=" & iseriesip & ";", Userid, UserPassword)

    '        Daily_QryData = connAS400.Execute("SELECT * from daily.apemaip " & _
    '        "WHERE amvnd# = " & myCustomer, Parms, -1)
    '        LoadEmail(Daily_QryData)
    '        connAS400.Close()

    '        UltraGridEmail.DataSource = EmailDataSet

    '        Me.Cursor = System.Windows.Forms.Cursors.Default
    '        eMailUpdated = False
    '    End Sub

    '#Region "Get Data Email"

    '    Public Sub LoadEmail(ByRef rs As ADODB.Recordset)
    '        'Public Sub LoadProducts(ByRef rs As ADODB.Recordset)
    '        BuildEmptyDataSetEmail()

    '        Dim EmailRow As DataRow
    '        While Not rs.EOF
    '            EmailRow = EmailTable.NewRow
    '            EmailRow("Name") = rs.Fields("amname").Value
    '            EmailRow("eMail") = rs.Fields("amaddr").Value
    '            EmailRow("PO") = GetYesNo(rs.Fields("ampo").Value)
    '            EmailRow("Delete") = ""
    '            EmailTable.Rows.Add(EmailRow)
    '            rs.MoveNext()

    '        End While
    '    End Sub

    '    Public Sub BuildEmptyDataSetEmail()

    '        EmailTable.Dispose()
    '        EmailDataSet.Dispose()

    '        EmailDataSet = Nothing
    '        EmailTable = Nothing

    '        EmailTable = New DataTable("Email")
    '        EmailDataSet = New DataSet

    '        GC.Collect()

    '        ' Vacation table
    '        EmailTable.Columns.Add("Name", GetType(String))
    '        EmailTable.Columns.Add("eMail", GetType(String))
    '        EmailTable.Columns.Add("PO", GetType(Boolean))
    '        EmailTable.Columns.Add("Delete", GetType(String))
    '        ' build dataset
    '        EmailDataSet.Tables.Add(EmailTable)

    '    End Sub
    '#End Region

    '#Region "Grid Handling"

    '    Private Sub UltraGridEmail_AfterCellUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridEmail.AfterCellUpdate
    '        If TxtCustomerPricingNote.ReadOnly = True Then Exit Sub
    '        If myCustomer = 0 Then Exit Sub
    '        eMailUpdated = True

    '    End Sub

    '    Private Sub UltraGridEmail_CellChange(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridEmail.CellChange
    '        If TxtCustomerPricingNote.ReadOnly = True Then Exit Sub
    '        If myCustomer = 0 Then Exit Sub
    '        eMailUpdated = True
    '    End Sub


    '    Private Sub UltraGridEmail_AfterRowUpdate(sender As Object, e As Infragistics.Win.UltraWinGrid.RowEventArgs) Handles UltraGridEmail.AfterRowUpdate
    '        If TxtCustomerPricingNote.ReadOnly = True Then Exit Sub
    '        If myCustomer = 0 Then Exit Sub
    '        eMailUpdated = True
    '    End Sub

    '    Private Sub UltraGridEmail_ClickCellButton(sender As Object, e As Infragistics.Win.UltraWinGrid.CellEventArgs) Handles UltraGridEmail.ClickCellButton
    '        If TxtCustomerPricingNote.ReadOnly = True Then Exit Sub
    '        If myCustomer = 0 Then Exit Sub
    '        If e.Cell.Column.Key = "Delete" Then
    '            e.Cell.Row.Delete()
    '            eMailUpdated = True
    '        End If

    '    End Sub

    '    Private Sub UltraGridEmail_BeforeRowsDeleted(sender As Object, e As Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs) Handles UltraGridEmail.BeforeRowsDeleted
    '        e.DisplayPromptMsg = False
    '    End Sub

    '    Private Sub UltraGridEmail_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles UltraGridEmail.MouseClick
    '        If TxtCustomerPricingNote.ReadOnly = True Then Exit Sub
    '        If myCustomer = 0 Then Exit Sub
    '        If e.Button = Windows.Forms.MouseButtons.Right Then
    '            Dim EmailRow As DataRow
    '            EmailRow = EmailTable.NewRow
    '            EmailRow("Name") = ""
    '            EmailRow("eMail") = ""
    '            EmailRow("PO") = False
    '            EmailRow("Delete") = ""

    '            EmailTable.Rows.Add(EmailRow)
    '        End If
    '    End Sub

    '    Private Sub UltraGridEmail_MouseEnterElement(ByVal sender As Object, ByVal e As Infragistics.Win.UIElementEventArgs) Handles UltraGridEmail.MouseEnterElement
    '        ToolTip1.Active = True
    '        ToolTip1.SetToolTip(sender, "Right click to create a new line.")
    '    End Sub

    '    Private Sub UltraGridEmail_InitializeRow(sender As Object, e As Infragistics.Win.UltraWinGrid.InitializeRowEventArgs) Handles UltraGridEmail.InitializeRow
    '        e.Row.Appearance.ForeColor = Color.Black
    '        e.Row.Cells("Delete").Appearance.Image = My.Resources.myDelete.ToBitmap
    '    End Sub

    '    Private Sub UltraGridEmail_InitializeLayout(ByVal sender As System.Object, ByVal e As Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs) Handles UltraGridEmail.InitializeLayout

    '        e.Layout.Override.HeaderClickAction = HeaderClickAction.SortMulti

    '        e.Layout.Override.CellAppearance.ForeColorDisabled = Color.White
    '        e.Layout.Override.CellAppearance.BackColorDisabled = Color.FromArgb(0, 128, 128)

    '        With UltraGridEmail

    '            .DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.Free
    '            .DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal

    '            .DisplayLayout.Override.ActiveAppearancesEnabled = DefaultableBoolean.False
    '            .DisplayLayout.Override.SelectedAppearancesEnabled = DefaultableBoolean.False

    '            .DisplayLayout.Override.RowAppearance.ForeColor = Color.Black
    '            .DisplayLayout.Override.RowAppearance.BackColor = Color.White

    '            .DisplayLayout.Override.GroupByRowAppearance.ForeColor = Color.White
    '            .DisplayLayout.Override.GroupByRowAppearance.BackColor = Color.FromArgb(0, 128, 128)

    '            .DisplayLayout.Override.RowSelectors = False
    '            .DisplayLayout.RowConnectorColor = Color.White
    '            .DisplayLayout.InterBandSpacing = 0


    '            .DisplayLayout.Bands(0).Columns("Name").Width = 170
    '            .DisplayLayout.Bands(0).Columns("eMail").Width = 170

    '            .DisplayLayout.Bands(0).Columns("Name").MaxLength = 25
    '            .DisplayLayout.Bands(0).Columns("eMail").MaxLength = 60

    '            .DisplayLayout.Bands(0).Columns("Delete").Width = 25
    '            .DisplayLayout.Bands(0).Columns("Delete").Header.Appearance.Image = My.Resources.myDelete.ToBitmap
    '            .DisplayLayout.Bands(0).Columns("Delete").Header.Caption = ""
    '            '.DisplayLayout.Bands(0).Columns("Delete").TabStop = False

    '            With UltraGridEmail.DisplayLayout.Bands(0)
    '                For i As Int32 = 0 To .Columns.Count - 1
    '                    .Columns(i).CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit
    '                Next i
    '            End With

    '            If Not TxtCustomerPricingNote.ReadOnly = True Then
    '                .DisplayLayout.Bands(0).Columns("Name").CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit
    '                .DisplayLayout.Bands(0).Columns("eMail").CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit
    '                .DisplayLayout.Bands(0).Columns("PO").CellActivation = Infragistics.Win.UltraWinGrid.Activation.AllowEdit
    '                .DisplayLayout.Bands(0).Columns("Delete").Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button

    '            End If
    '        End With

    '    End Sub

    '#End Region

    '#Region "Edit Email"
    '    Private Function EditEmail() As Boolean
    '        EditEmail = True
    '        For Each row As UltraGridRow In Me.UltraGridEmail.Rows
    '            If Not Trim(row.Cells("email").Text) = "" Then
    '                If IsValidEmailFormat(row.Cells("email").Text) = False Then
    '                    If EditEmail = True Then
    '                        EditEmail = False
    '                        eMailUpdated = True
    '                        MsgBox("Invalid email address", MsgBoxStyle.OkOnly, "Error")
    '                    End If
    '                    row.Cells("email").Appearance.BackColor = Color.Red
    '                Else
    '                    row.Cells("email").Appearance.BackColor = Color.White
    '                End If
    '            End If
    '        Next
    '        Return EditEmail
    '    End Function
    '#End Region

    '    Function IsValidEmailFormat(ByVal s As String) As Boolean
    '        Try
    '            Dim a As New System.Net.Mail.MailAddress(s)
    '        Catch
    '            Return False
    '        End Try
    '        Return True
    '    End Function

    '#Region "Update Email"
    '    Private Sub UpdateEmail()
    '        If Not eMailUpdated Then Exit Sub

    '        Dim myApEmaip As New iSeries.apEmaip
    '        With myApEmaip
    '            .DeleteAll(myCustomer)

    '            For Each row As UltraGridRow In Me.UltraGridEmail.Rows
    '                If Not Trim(row.Cells("email").Text) = "" Then
    '                    .amVnd_ = myCustomer
    '                    .amName = Trim(row.Cells("Name").Text)
    '                    .amAddr = row.Cells("email").Text
    '                    .amPO = row.Cells("po").Text
    '                    .Create()
    '                End If
    '            Next

    '        End With


    '    End Sub
    '#End Region

    '#End Region


End Class
