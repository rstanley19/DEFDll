Option Strict On
Option Explicit On 
Imports System

Namespace iSeries

    Public Class soApDmp
        Private Parms As Object
        'Private connAS400 As New ADODB.Connection

        Property dmVen_ As Integer
        Property dmRef_ As String
        Property dmLine As Integer
        Property dmDesc As String
        Property dmAmt As decimal
        Property dmGL_ As Long
        Property dmID As Long

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(ByVal GetID As Long)
            Retrieve(GetID)
        End Sub

        Sub Retrieve(ByVal GetID As Long)
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.soapdmp where dmID = " & GetID, Parms, -1)
            dmID = GetID

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _dmVen_ = Convert.ToInt32(Daily_GenericDATA.Fields("dmVen#").Value)
                _dmRef_ = Convert.ToString(Daily_GenericDATA.Fields("dmRef#").Value)
                _dmLine = Convert.ToInt32(Daily_GenericDATA.Fields("dmLine").Value)
                _dmDesc = Convert.ToString(Daily_GenericDATA.Fields("dmDesc").Value)
                _dmAmt = Convert.Todecimal(Daily_GenericDATA.Fields("dmAmt").Value)
                _dmGL_ = Convert.ToInt64(Daily_GenericDATA.Fields("dmGL#").Value)
                _dmID = Convert.ToInt64(Daily_GenericDATA.Fields("dmID").Value)

                _found = True
            End If

        End Sub

        Sub Update()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.soapdmp set " & _
        "( dmVen#, dmRef#, dmLine, " & _
        "dmDesc, dmAmt, dmGL#)" & _
        " = " & _
        "(" & _dmVen_ & ", " & _
                        Quoted(_dmRef_) & ", " & _
                        _dmLine & ", " & _
                        Quoted(_dmDesc) & ", " & _
                        _dmAmt & ", " & _
                        _dmGL_ & ") " & _
                        "where dmID = " & _dmID
                .Execute()
            End With

        End Sub

        Sub Create()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.soapdmp " & _
                "( dmVen#, dmRef#, dmLine, " & _
                "dmDesc, dmAmt, dmGL#)" & _
                " Values " & _
                "(" & _dmVen_ & ", " & _
                                Quoted(_dmRef_) & ", " & _
                                _dmLine & ", " & _
                                Quoted(_dmDesc) & ", " & _
                                _dmAmt & ", " & _
                                _dmGL_ & ") "
                .Execute()
                _found = True
                Dim Daily_QryData As ADODB.Recordset
                Daily_QryData = connAS400.Execute("SELECT IDENTITY_VAL_LOCAL() as ID FROM SYSIBM.SYSDUMMY1", Parms, -1)
                _dmID = Convert.ToInt64(Daily_QryData.Fields("ID").Value)

            End With

        End Sub

        Sub Delete()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "delete from daily.soapdmp " & _
                                "where dmID = " & _dmID
                .Execute()
            End With

        End Sub

    End Class
End Namespace

