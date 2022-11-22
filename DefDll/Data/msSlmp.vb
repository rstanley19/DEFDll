Option Strict On
Option Explicit On 
Imports System

Namespace iSeries

    Public Class msSlmp
        Private Parms As Object
        '   Private connAS400 As New ADODB.Connection

        Property slSlm_ As Long
        Property slInit As String
        Property slUser As String
        Property slslnm As String
        Property slemai As String
        Property slnema As String

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(ByVal inUser As String)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserID, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select slinit, slslnm, slslm#, slemai, slnema from daily.msslmp where ucase(slUser) = " & Quoted(inUser), Parms, -1)
            _slUser = inUser


            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _slInit = Trim(Convert.ToString(Daily_GenericDATA.Fields("slInit").Value))
                _slSlm_ = Convert.ToInt32(Daily_GenericDATA.Fields("slslm#").Value)
                _slslnm = Trim(Convert.ToString(Daily_GenericDATA.Fields("slslnm").Value))
                _slemai = Trim(Convert.ToString(Daily_GenericDATA.Fields("slemai").Value))
                _slnema = Trim(Convert.ToString(Daily_GenericDATA.Fields("slnema").Value))
                _found = True
            End If

        End Sub


        Sub New(ByVal inSalesman As Integer)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserID, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select slinit, slslnm, slUser, slemai, slnema from daily.msslmp where slslm# = " & inSalesman, Parms, -1)
            _slSlm_ = inSalesman


            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _slInit = Trim(Convert.ToString(Daily_GenericDATA.Fields("slInit").Value))
                _slUser = Trim(Convert.ToString(Daily_GenericDATA.Fields("slUser").Value))
                _slslnm = Trim(Convert.ToString(Daily_GenericDATA.Fields("slslnm").Value))
                _slemai = Trim(Convert.ToString(Daily_GenericDATA.Fields("slemai").Value))
                _slnema = Trim(Convert.ToString(Daily_GenericDATA.Fields("slnema").Value))
                _found = True
            End If

        End Sub

    End Class
End Namespace

