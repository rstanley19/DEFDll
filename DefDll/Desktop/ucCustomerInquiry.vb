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

    End Sub


End Class
