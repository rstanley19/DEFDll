Option Strict On
Option Explicit On
    Imports System

Namespace iSeries

    Public Class soInvp
        Private Parms As Object

        Property soInv_ As Long
        Property soAcc_ As Long
        Property soBlto As Long
        Property soOdat As Long
        Property soSoDt As Long ' date order placed
        Property sosotm As Integer ' time order placed
        Property soIdat As Long
        Property soDdat As Long
        Property soDtis As Integer
        Property soDtie As Integer
        Property soCar_ As Integer ' carrier #
        Property soStat As String
        Property soDel As String
        Property soCpo_ As String
        Property soSins As String
        Property soSows As String ' assigned salesman
        Property soSous As String ' Salesman placing order
        Property soInit As String ' Salesman Initials
        Property soBsta As String ' Billing Status
        Property soLnk_ As Long
        Property soDatf As String ' date changed flag
        Property soMktw As String ' Market Watch
        Property soOkpl As String ' OK To Preload
        Property soCont As String ' Contact Name
        Property soFork As String ' Forklift 
        Property soDock As String ' Dock 
        Property soCarr As String ' Carrier
        Property soReg As String ' Region
        Property soInv As String ' Def Invoice?
        Property soSpfs As String ' Spiff Status
        Property soSpf_ As decimal ' Spiff Paid $
        Property soSpfa As String   'Approved / Denied
        Property soSpus As String ' Spiff Reviewed By
        Property soSp2a As String   'Spiff 2 approve / denied
        Property soSp2U As String 'Spiff 2 reviewed by
        Property soYell As Long ' Yellow Sheet Key
        Property soSp2_ As Integer 'CSR Spiff Salesman
        Property soOfrt As decimal 'outside freight estimate
        Property found As Boolean


        Sub New()
        End Sub

        Sub New(ByVal GetInvoice As Long)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.soinvp where soinv# = " & GetInvoice, Parms, -1)
            soInv_ = GetInvoice

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _soAcc_ = Convert.ToInt32(Daily_GenericDATA.Fields("soAcc#").Value)
                _soBlto = Convert.ToInt32(Daily_GenericDATA.Fields("soBlto").Value)
                _soOdat = Convert.ToInt64(Daily_GenericDATA.Fields("soOdat").Value)
                _soSoDt = Convert.ToInt64(Daily_GenericDATA.Fields("soSodt").Value)
                _sosotm = Convert.ToInt32(Daily_GenericDATA.Fields("sosotm").Value)
                _soIdat = Convert.ToInt64(Daily_GenericDATA.Fields("soidat").Value)
                _soDdat = Convert.ToInt64(Daily_GenericDATA.Fields("soDdat").Value)
                _soDtis = Convert.ToInt32(Daily_GenericDATA.Fields("soDtis").Value)
                _soDtie = Convert.ToInt32(Daily_GenericDATA.Fields("soDtie").Value)
                _soCar_ = Convert.ToInt32(Daily_GenericDATA.Fields("soCar#").Value)
                _soStat = Trim(Convert.ToString(Daily_GenericDATA.Fields("soStat").Value))
                _soDel = Trim(Convert.ToString(Daily_GenericDATA.Fields("sodel").Value))
                _soCpo_ = Trim(Convert.ToString(Daily_GenericDATA.Fields("socpo#").Value))
                _soSins = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSins").Value))
                _soLnk_ = Convert.ToInt64(Daily_GenericDATA.Fields("solnk#").Value)
                _soSows = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSows").Value))
                _soSous = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSous").Value))
                _soInit = Trim(Convert.ToString(Daily_GenericDATA.Fields("soInit").Value))
                _soDatf = Trim(Convert.ToString(Daily_GenericDATA.Fields("soDatf").Value))
                _soBsta = Trim(Convert.ToString(Daily_GenericDATA.Fields("soBsta").Value))
                _soMktw = Trim(Convert.ToString(Daily_GenericDATA.Fields("soMktw").Value))
                _soOkpl = Trim(Convert.ToString(Daily_GenericDATA.Fields("soMktw").Value))
                _soCont = Trim(Convert.ToString(Daily_GenericDATA.Fields("soOkpl").Value))
                _soFork = Trim(Convert.ToString(Daily_GenericDATA.Fields("soFork").Value))
                _soDock = Trim(Convert.ToString(Daily_GenericDATA.Fields("soDock").Value))
                _soCarr = Trim(Convert.ToString(Daily_GenericDATA.Fields("soCarr").Value))
                _soReg = Trim(Convert.ToString(Daily_GenericDATA.Fields("soReg").Value))
                _soInv = Trim(Convert.ToString(Daily_GenericDATA.Fields("soInv").Value))
                _soSpfs = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSpfs").Value))
                _soSpfa = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSpfa").Value))
                _soSpf_ = Convert.Todecimal(Daily_GenericDATA.Fields("soSpf$").Value)
                _soSpus = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSpus").Value))
                _soSp2a = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSp2a").Value))
                _soSp2U = Trim(Convert.ToString(Daily_GenericDATA.Fields("soSp2u").Value))
                _soYell = Convert.ToInt64(Daily_GenericDATA.Fields("soyell").Value)
                _soSp2_ = Convert.ToInt32(Daily_GenericDATA.Fields("soSp2#").Value)
                _soOfrt = Convert.Todecimal(Daily_GenericDATA.Fields("soofrt").Value)

                _found = True
            End If

        End Sub

    End Class

End Namespace


