Option Strict On
Option Explicit On
Imports System

Namespace Iseries

    Public Class msCsrHp

        Private Parms As Object
        Property cdcsid As Long
        Property CSRIn As String

        Sub New(ByVal inSalesman As Integer)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select cdslm#, ifnull(cdcsid, 0) as cdcsid from daily.mscsrdp " &
                                                  "where cdcsid in " &
                                                  "(select csid from daily.mscsrhp " &
                                                  "left join daily.mscsrdp on csid = cdcsid " &
                                                  "where csstat = 'A' and cdslm# = " & inSalesman & ") and not cdslm# = " & inSalesman, Parms, -1)

            _CSRIn = "("
            _cdcsid = 0

            While Not Daily_GenericDATA.EOF
                _cdcsid = Convert.ToInt64(Daily_GenericDATA.Fields("cdcsid").Value)
                If Not _CSRIn = "(" Then _CSRIn &= ", "
                _CSRIn &= Convert.ToInt32(Daily_GenericDATA.Fields("cdslm#").Value)
                Daily_GenericDATA.MoveNext()
            End While

            If Not _CSRIn = "(" Then
                _CSRIn &= ")"
            Else
                _CSRIn = ""
            End If

        End Sub

    End Class

End Namespace




