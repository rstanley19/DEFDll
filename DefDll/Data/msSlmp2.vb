Option Strict On
Option Explicit On
Imports System

Namespace iSeries

    Public Class msSlmp2

        Private Parms As Object
        Property sluserIn As String
        Property slSlm_ As Integer
        Sub New(ByVal inUserId As String)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.msslmp where sluser = " & Quoted(inUserId), Parms, -1)

            _sluserIn = "("
            _slSlm_ = 0

            While Not Daily_GenericDATA.EOF
                If Not _sluserIn = "(" Then _sluserIn &= ", "
                _sluserIn &= Convert.ToInt32(Daily_GenericDATA.Fields("slslm#").Value)
                _slSlm_ = Convert.ToInt32(Daily_GenericDATA.Fields("slslm#").Value)

                Daily_GenericDATA.MoveNext()
            End While

            If Not _sluserIn = "(" Then
                _sluserIn &= ")"
            Else
                _sluserIn = "(-1)"
            End If

        End Sub

    End Class

End Namespace




