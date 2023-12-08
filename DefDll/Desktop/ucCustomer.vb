Imports System.Data.OleDb
Imports Crownwood.DotNetMagic.Common
Imports Crownwood.DotNetMagic.Docking
Imports System.Drawing

Public Class ucCustomer
    Public Event CloseMe(ByVal TabKey As String, ReturnToTab As Integer)
    Private myTabkey As String
    Private myReturnToTab As Integer
    Private myMsCustp As iSeries.msCustp
    Dim myUcCreditImages As CustomerMaintenance.ucCreditImages
    Dim myucInvoices As ucInvoices
    Dim myucNotes As ucNotes
    Dim myucWarranty As ucWarranty

    Public Sub New(ByVal inTabKey As String, inMsCustp As iSeries.msCustp, inReturnToTab As Integer)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        myTabkey = inTabKey
        myReturnToTab = inReturnToTab
        myMsCustp = inMsCustp
        cbcmSlm_.DataSource = SalesmanTable
        cbcmSlm_.ValueMember = SalesmanTable.Columns("slslm#").Caption
        cbcmSlm_.DisplayMember = SalesmanTable.Columns("slslnm").Caption
        cbCmSlmd.DataSource = SalesmanTable
        cbCmSlmd.ValueMember = SalesmanTable.Columns("slslm#").Caption
        cbCmSlmd.DisplayMember = SalesmanTable.Columns("slslnm").Caption

        RefreshMe()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        RaiseEvent CloseMe(myTabkey, myReturnToTab)
    End Sub

    Private Sub RefreshMe()
        With myMsCustp
            TxtCmAcc_.Value = .cmAcc_
            TxtCmBlto.Value = .cmBlto
            TxtCmName.Text = .cmName
            TxtCmAdr1.Text = .cmAdr1
            TxtCmAdr2.Text = .cmAdr2
            TxtCmCity.Text = .cmCity
            TxtCmSt.Text = .cmSt
            TxtCmZip.Value = .cmZip
            TxtCmZip4.Value = .cmZip4
            TxtCmCnty.Value = .cmCnty
            TxtCmTel_.Value = .cmTel_
            TxtCmFax_.Value = .cmFax_
            cbcmSlm_.Value = .cmslm_
            cbCmSlmd.Value = .cmslmd

            cbCmDel.Value = .cmDel


        End With
        ChangeToReadOnly(UltraTabPageControl1, True)


    End Sub

    Private Sub UltraTabControl2_ActiveTabChanged(sender As Object, e As Infragistics.Win.UltraWinTabControl.ActiveTabChangedEventArgs) Handles UltraTabControl2.ActiveTabChanged
        If e.Tab.Text = "Documents" Then
            If e.Tab.TabPage.Controls.Count = 0 Then
                Dim mySetHandshake As New CustomerMaintenance.Class1
                mySetHandshake.SetHandShake(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder, iSeriesIP)
                myUcCreditImages = New CustomerMaintenance.ucCreditImages(myMsCustp.cmBlto, myMsCustp.cmAcc_, "", 1, Nothing, False, UserId, UserName, UserPassword, "DEF", False)
                '                myUcCreditImages.SetDefaults(UserId, UserName, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL)
                AddHandler myUcCreditImages.CloseMe, AddressOf CloseMyOrder
                AddHandler myUcCreditImages.ShowImage, AddressOf ShowImage
                myUcCreditImages.Size = UltraTabPageControl2.Size
                myUcCreditImages.Anchor = 15
                e.Tab.TabPage.Controls.Add(myUcCreditImages)
            End If
        ElseIf e.Tab.Text = "Invoices" Then
            If e.Tab.TabPage.Controls.Count = 0 Then
                myucInvoices = New ucInvoices(myMsCustp.cmAcc_, myMsCustp.cmBlto, myMsCustp.cmCtyp)
                myucInvoices.Size = UltraTabPageControl3.Size
                myucInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                 Or System.Windows.Forms.AnchorStyles.Left) _
                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                e.Tab.TabPage.Controls.Add(myucInvoices)
            End If
        ElseIf e.Tab.Text = "Notes" Then
            If e.Tab.TabPage.Controls.Count = 0 Then
                myucNotes = New ucNotes(myMsCustp.cmAcc_, myMsCustp.cmBlto, myMsCustp.cmCtyp)
                myucNotes.Size = UltraTabPageControl3.Size
                myucNotes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                 Or System.Windows.Forms.AnchorStyles.Left) _
                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                e.Tab.TabPage.Controls.Add(myucNotes)
            End If
        ElseIf e.Tab.Text = "Warranty" Then
            If e.Tab.TabPage.Controls.Count = 0 Then
                myucWarranty = New ucWarranty(myMsCustp.cmAcc_, Me)
                myucWarranty.Size = UltraTabPageControl3.Size
                myucWarranty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                 Or System.Windows.Forms.AnchorStyles.Left) _
                 Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                e.Tab.TabPage.Controls.Add(myucWarranty)
            End If
        End If

    End Sub

#Region "Document"

    Private Sub ShowImage(ByVal inImage As String, ReturnToTab As Integer, BackAccount As Long, BackRow As Infragistics.Win.UltraWinGrid.UltraGridRow, inInquiryOnly As Boolean)
        Try

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim mySetHandshake As New CustomerMaintenance.Class1
            mySetHandshake.SetHandShake(UserId, UserName, UserPassword, smtp, PassApplicationProductNameToDLL, PassApplicationStartupPathToDLL, myPrivateUserFolder, iSeriesIP)
            Dim myucimage As CustomerMaintenance.ucShowImage

            Static KeyCounter As Long
            KeyCounter += 1

            UltraTabControl2.Tabs.Add("F" & KeyCounter.ToString, "Image")

            If Not Trim(inImage) = "" Then
                myucimage = New CustomerMaintenance.ucShowImage("F" & KeyCounter.ToString, inImage, BackRow.Cells("img#").Value, BackAccount, BackRow, inInquiryOnly, UserId, UserName, UserPassword, "DEF")
            Else
                myucimage = New CustomerMaintenance.ucShowImage("F" & KeyCounter.ToString, inImage, 0, BackAccount, BackRow, inInquiryOnly, UserId, UserName, UserPassword, "DEF")
            End If

            UltraTabControl2.Tabs("F" & KeyCounter.ToString).TabPage.Controls.Add(myucimage)
            UltraTabControl2.Tabs("F" & KeyCounter.ToString).Selected = True
            AddHandler myucimage.CloseMe, AddressOf TaskWasClosed
            AddHandler myucimage.AddImage, AddressOf AddImage

            myucimage.Size = UltraTabControl2.Size
            myucimage.Anchor = 15

        Catch ex As Exception
            If UserId = "RICKS" Then MsgBox(ex.ToString)
        End Try

        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub TaskWasClosed(ByVal tag As String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        UltraTabControl2.Tabs(tag).Selected = True
        UltraTabControl2.Tabs.Remove(UltraTabControl2.Tabs(tag))
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Public Sub AddImage(Backarscanp As CustomerMaintenance.iSeries.ArScanP, BackLocation As String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        myUcCreditImages.CreateANewImage(Backarscanp, BackLocation)
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CloseMyOrder(ByVal tabkey As String, ReturnToTab As Integer)
        Try
            UltraTabControl2.Tabs.Remove(UltraTabControl2.Tabs(tabkey))
            UltraTabControl2.Tabs(ReturnToTab).Selected = True
        Catch
        End Try
    End Sub

    Private Sub CloseMyOrder(ByVal tabkey As String)
        UltraTabControl2.Tabs.Remove(UltraTabControl2.Tabs(tabkey))
    End Sub
#End Region

    Private Sub llCallPhone_LinkClicked(sender As Object, e As Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llCallPhone.LinkClicked
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        Dim myToken As String = ""

        Dim MyPlaceAVonageCall As New PlaceAVonageCall.Class1
        With MyPlaceAVonageCall
            myToken = .VonageRefresh(myPrivateUserFolder)
            If myToken = "Error" Then
                myToken = .ChilKatOauth2(myPrivateUserFolder)
                If myToken = "Error" Then
                    Me.Cursor = Windows.Forms.Cursors.Default
                    MsgBox("Unable to complete the call at this time", MsgBoxStyle.OkOnly, "Call failed")
                    Exit Sub
                End If
            End If

        End With

        MyPlaceAVonageCall.PlaceVonageCall(myToken, TxtCmTel_.Text)
        Me.Cursor = Windows.Forms.Cursors.Default
    End Sub


End Class
