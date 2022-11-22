Public Class Class1

    Public Sub SetHandShake(inUserId As String, inUserName As String, InUserPassword As String, inSmtp As String, inPassApplicationProductNameToDLL As String, inPassApplicationStartupPathToDLL As String, inPrivateUserFolder As String, iniSeriesIP As String)
        iSeriesIP = iniSeriesIP
        PassApplicationProductNameToDLL = inPassApplicationProductNameToDLL
        PassApplicationStartupPathToDLL = inPassApplicationStartupPathToDLL

        myBinAppSettings = Nothing
        myBinAppSettings = New BinAppSettings

        myPrivateUserFolder = inPrivateUserFolder

        UserId = inUserId
        UserPassword = InUserPassword
        UserName = inUserName

        smtp = inSmtp
        If connAS400.State = 0 Then
            connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
        End If

        Dim mymsSlmp As New iSeries.msSlmp(inUserId)

    End Sub

    Public Sub BuildSalesmanList()
        QueryDataBaseForValueLists()
    End Sub
End Class
