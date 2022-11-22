Option Strict On
Option Explicit On 
Imports System

Namespace iSeries

    Public Class soApDip
        Private Parms As Object
        'Private connAS400 As New ADODB.Connection

        Property diVen_ As Long
        Property diRef_ As String
        Property diInv_ As Long
        Property diLin_ As Integer
        Property diQtpd As Integer
        Property diCost As decimal
        Property diSub_ As decimal
        Property diID As Long

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(ByVal GetID As Long)
            Retrieve(GetID)
        End Sub

        Sub Retrieve(ByVal GetID As Long)
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.soApDip where diId = " & GetID, Parms, -1)
            diID = GetID

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _diVen_ = Convert.ToInt32(Daily_GenericDATA.Fields("diVen#").Value)
                _diRef_ = Convert.ToString(Daily_GenericDATA.Fields("diRef#").Value)
                _diInv_ = Convert.ToInt64(Daily_GenericDATA.Fields("diInv#").Value)
                _diLin_ = Convert.ToInt32(Daily_GenericDATA.Fields("diLin#").Value)
                _diQtpd = Convert.ToInt32(Daily_GenericDATA.Fields("diQtpd").Value)
                _diCost = Convert.Todecimal(Daily_GenericDATA.Fields("diCost").Value)
                _diSub_ = Convert.Todecimal(Daily_GenericDATA.Fields("diSub$").Value)
                _found = True
            End If

        End Sub

        Sub Update()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.soApDip set " & _
        "( diVen#, diRef#, dhtot$, diInv#, " & _
        "diLin#, diQtpd, diCost, " & _
        "diSub$)" & _
        " = " & _
        "(" & _diVen_ & ", " & _
                        Quoted(_diRef_) & ", " & _
                        _diInv_ & ", " & _
                        _diLin_ & ", " & _
                        _diQtpd & ", " & _
                        _diCost & ", " & _
                        _diSub_ & ") " & _
                        "where diId = " & _diID
                .Execute()
            End With

        End Sub

        Sub Create()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.soApDip " & _
                "( diVen#, diRef#, diInv#, " & _
                "diLin#, diQtpd, diCost, " & _
                "diSub$)" & _
                " Values " & _
                "(" & _diVen_ & ", " & _
                                Quoted(_diRef_) & ", " & _
                                _diInv_ & ", " & _
                                _diLin_ & ", " & _
                                _diQtpd & ", " & _
                                _diCost & ", " & _
                                _diSub_ & ") "
                .Execute()
                _found = True
                Dim Daily_QryData As ADODB.Recordset
                Daily_QryData = connAS400.Execute("SELECT IDENTITY_VAL_LOCAL() as ID FROM SYSIBM.SYSDUMMY1", Parms, -1)
                _diID = Convert.ToInt64(Daily_QryData.Fields("ID").Value)

            End With

        End Sub

        Sub Delete()
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "delete from daily.soApDip " & _
                                "where diId = " & _diID
                .Execute()
            End With

        End Sub

        Function GetPreviousPayments(ByVal inVendor As Long, inReference As String, inInvoice As Long, inLine As Integer) As decimal
            Dim connAS4002 As New ADODB.Connection
            connAS4002.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS4002.Execute("select sum(diqtpd) as Previous from daily.soApDip where " & _
            " diinv# = " & inInvoice & _
            " and diLin# = " & inLine & _
            " and (not diven# = " & inVendor & _
            " or not diRef# = " & Quoted(inReference) & ")", Parms, -1)

            If Daily_GenericDATA.EOF Then
                GetPreviousPayments = 0
            Else
                GetPreviousPayments = Convert.ToInt32(NoNullNumber(Daily_GenericDATA.Fields("Previous").Value))
            End If
            connAS4002.Close()
            Return GetPreviousPayments
        End Function

    End Class
End Namespace

