Option Strict On
Option Explicit On 
Imports System

Namespace iSeries

    Public Class soApDhp
        Private Parms As Object
        'Private connAS400 As New ADODB.Connection

        Property dhVen_ As Long
        Property dhRef_ As String
        Property dhtot_ As decimal
        Property dhIndt As Long
        Property dhRcvd As Long
        Property dhImag As Long
        Property dhImlo As Integer
        Property dhStat As String
        Property dhDtpd As Long
        Property dhChk_ As Integer
        Property dhID As Long

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(ByVal GetID As Long)
            Retrieve(GetID)
        End Sub

        Sub Retrieve(ByVal GetID As Long)
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.soapdhp where dhID = " & GetID, Parms, -1)
            dhID = GetID

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _dhVen_ = Convert.ToInt32(Daily_GenericDATA.Fields("dhVen#").Value)
                _dhRef_ = Convert.ToString(Daily_GenericDATA.Fields("dhRef#").Value)
                _dhtot_ = Convert.Todecimal(Daily_GenericDATA.Fields("dhtot$").Value)
                _dhIndt = Convert.ToInt64(Daily_GenericDATA.Fields("dhIndt").Value)
                _dhRcvd = Convert.ToInt64(Daily_GenericDATA.Fields("dhRcvd").Value)
                _dhImag = Convert.ToInt64(Daily_GenericDATA.Fields("dhImag").Value)
                _dhImlo = Convert.ToInt32(Daily_GenericDATA.Fields("dhImlo").Value)
                _dhStat = Convert.ToString(Daily_GenericDATA.Fields("dhStat").Value)
                _dhDtpd = Convert.ToInt32(Daily_GenericDATA.Fields("dhDtpd").Value)
                _dhChk_ = Convert.ToInt32(Daily_GenericDATA.Fields("dhChk#").Value)
                _dhID = Convert.ToInt64(Daily_GenericDATA.Fields("dhID").Value)

                _found = True
            End If

        End Sub

        Sub Update()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.soapdhp set " & _
        "( dhVen#, dhRef#, dhtot$, dhIndt, " & _
        "dhRcvd, dhimag, dhImlo, " & _
        "dhStat, dhDtpd, dhChk#)" & _
        " = " & _
        "(" & _dhVen_ & ", " & _
                        Quoted(_dhRef_) & ", " & _
                        _dhtot_ & ", " & _
                        _dhIndt & ", " & _
                        _dhRcvd & ", " & _
                        _dhImag & ", " & _
                        _dhImlo & ", " & _
                        Quoted(_dhStat) & ", " & _
                        _dhDtpd & ", " & _
                        _dhChk_ & ") " & _
                        "where dhId = " & _dhID
                .Execute()
            End With

        End Sub

        Sub Create()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.soapdhp " & _
                "( dhVen#, dhRef#, dhtot$, dhIndt, " & _
                "dhRcvd, dhImag, dhImlo, " & _
                "dhStat, dhDtpd, dhChk#)" & _
                " Values " & _
                "(" & _dhVen_ & ", " & _
                                Quoted(_dhRef_) & ", " & _
                                _dhtot_ & ", " & _
                                _dhIndt & ", " & _
                                _dhRcvd & ", " & _
                                _dhImag & ", " & _
                                _dhImlo & ", " & _
                                Quoted(_dhStat) & ", " & _
                                _dhDtpd & ", " & _
                                _dhChk_ & ") "
                .Execute()
                _found = True
                Dim Daily_QryData As ADODB.Recordset
                Daily_QryData = connAS400.Execute("SELECT IDENTITY_VAL_LOCAL() as ID FROM SYSIBM.SYSDUMMY1", Parms, -1)
                _dhID = Convert.ToInt64(Daily_QryData.Fields("ID").Value)

            End With

        End Sub

        Sub Delete()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "delete from daily.soapdhp " & _
                                "where dhId = " & _dhID
                .Execute()
            End With

        End Sub

    End Class
End Namespace

