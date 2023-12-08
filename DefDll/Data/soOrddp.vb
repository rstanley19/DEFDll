Option Strict On
Option Explicit On 
Imports System

Namespace iSeries

    Public Class soOrddp
        Private Parms As Object
        'Private connAS400 As New ADODB.Connection

        Property odInv_ As Long
        Property odItem As Long
        Property odSlot As String
        Property odLin_ As Long
        Property odUuom As String
        Property odQord As Long
        Property odUcst As Double
        Property odTxst As String
        Property odQshp As Long
        Property odAvgc As Double
        Property odQprt As Long
        Property odUprt As String
        Property odFroz As Integer
        Property odDel As String
        Property odUpda As String
        Property odId As Long
        Property odWhse As Integer
        Property odPlst As Double ' Plastic Cost
        Property odFrtc As Double ' Freight Cost
        Property odToDr As Integer ' 1=Tote 2=Drum 3=refill
        Property odFrtO As String ' Freight Only = Y
        Property odDish As String ' Direct Ship Y/N
        Property odVnds As Integer ' Vendor Source Vendor Direct Ship = Special Handling DS or Not Direct Ship = RegTrmPull
        Property odLocs As Integer ' Location - Terminal # from RegTrmPull
        Property odCar As String ' 0 = Warehouse / 1 = Transport / 2=3rd Party
        Property odCar_ As Integer ' Vendor Special Handling CA
        Property odDstO As String ' Y = Destination only
        Property odDst_ As Integer ' Destination Only #
        Property odBl_ As String ' BOL #

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(ByVal GetID As Long)
            Retrieve(GetID)
        End Sub

        Sub Retrieve(ByVal GetID As Long)
            'connAS400.Open("Provider=IBMDA400;Data Source=PORTS ;", "pc", "pcpc")
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.soorddp where odID = " & GetID, Parms, -1)
            odId = GetID

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _odInv_ = Convert.ToInt32(Daily_GenericDATA.Fields("odInv#").Value)
                _odItem = Convert.ToInt32(Daily_GenericDATA.Fields("odItem").Value)
                _odSlot = Convert.ToString(Daily_GenericDATA.Fields("odSlot").Value)
                _odLin_ = Convert.ToInt64(Daily_GenericDATA.Fields("odLin#").Value)
                _odUuom = Convert.ToString(Daily_GenericDATA.Fields("odUuom").Value)
                _odQord = Convert.ToInt64(Daily_GenericDATA.Fields("odQord").Value)
                _odUcst = Convert.ToDouble(Daily_GenericDATA.Fields("odUcst").Value)
                _odTxst = Convert.ToString(Daily_GenericDATA.Fields("odTxst").Value)
                _odQshp = Convert.ToInt64(Daily_GenericDATA.Fields("odQshp").Value)
                _odAvgc = Convert.ToDouble(Daily_GenericDATA.Fields("odAvgc").Value)
                _odQprt = Convert.ToInt64(Daily_GenericDATA.Fields("odQprt").Value)
                _odUprt = Convert.ToString(Daily_GenericDATA.Fields("odUprt").Value)
                _odFroz = Convert.ToInt32(Daily_GenericDATA.Fields("odFroz").Value)
                _odDel = Trim(Convert.ToString(Daily_GenericDATA.Fields("odDel").Value))
                _odUpda = Trim(Convert.ToString(Daily_GenericDATA.Fields("odUpda").Value))
                _odId = Convert.ToInt64(Daily_GenericDATA.Fields("odId").Value)
                _odWhse = Convert.ToInt32(Daily_GenericDATA.Fields("odWhse").Value)
                _odPlst = Convert.ToDouble(Daily_GenericDATA.Fields("odPlst").Value)
                _odFrtc = Convert.ToDouble(Daily_GenericDATA.Fields("odFrtc").Value)
                _odToDr = Convert.ToInt32(Daily_GenericDATA.Fields("odTodr").Value)

                _odFrtO = Convert.ToString(Daily_GenericDATA.Fields("odFrtO").Value)
                _odDish = Convert.ToString(Daily_GenericDATA.Fields("odDish").Value)
                _odVnds = Convert.ToInt32(Daily_GenericDATA.Fields("odVnds").Value)
                _odLocs = Convert.ToInt32(Daily_GenericDATA.Fields("odLocs").Value)
                _odCar = Convert.ToString(Daily_GenericDATA.Fields("odCar").Value)
                _odCar_ = Convert.ToInt32(Daily_GenericDATA.Fields("odCar#").Value)
                _odDstO = Convert.ToString(Daily_GenericDATA.Fields("odDsto").Value)
                _odDst_ = Convert.ToInt32(Daily_GenericDATA.Fields("odDst#").Value)
                _odBl_ = Convert.ToString(Daily_GenericDATA.Fields("odBl#").Value)

                _found = True
            End If

        End Sub

        Sub Update()

            'connAS400.Open("Provider=IBMDA400;Data Source=PORTS ;", "pc", "pcpc")
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.soOrddp set " &
        "( odInv#, odItem, odSlot, odLin#, odUuom, odQord, odUcst, odTxst, odQshp, odAvgc, " &
        "odQprt, odUprt, odFroz, odDel, odUpda, odWhse, odPlst, odFrtc, odTodr, " &
        "odFrto, odDish, odVnds, odLocs, odCar, odCar#, odDsto, odDst#, odBl#)" &
        " = " &
        "(" & _odInv_ & ", " &
                        _odItem & ", " &
                        Quoted(_odSlot) & ", " &
                        _odLin_ & ", " &
                        Quoted(_odUuom) & ", " &
                        _odQord & ", " &
                        _odUcst & ", " &
                        Quoted(_odTxst) & ", " &
                        _odQshp & ", " &
                        _odAvgc & ", " &
                        _odQprt & ", " &
                        Quoted(_odUprt) & ", " &
                        _odFroz & ", " &
                        Quoted(_odDel) & ", " &
                        Quoted(_odUpda) & ", " &
                        _odWhse & ", " &
                        _odPlst & ", " &
                        _odFrtc & ", " &
                        _odToDr & ", " &
                        Quoted(_odFrtO) & ", " &
                        Quoted(_odDish) & ", " &
                        _odVnds & ", " &
                        _odLocs & ", " &
                        Quoted(_odCar) & ", " &
                        _odCar_ & ", " &
                        Quoted(_odDstO) &
                        ", " & _odDst_ &
                        ", " & Quoted(_odBl_) &
                        ") " &
                        "where odID = " & _odId
                .Execute()
            End With

        End Sub

        Sub Create()

            'connAS400.Open("Provider=IBMDA400;Data Source=PORTS ;", "pc", "pcpc")
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.soOrddp " &
                "( odInv#, odItem, odSlot, odLin#, odUuom, odQord, odUcst, odTxst, odQshp, odAvgc, " &
                "odQprt, odUprt, odFroz, odDel, odUpda, odWhse, odPlst, odFrtc, odTodr, " &
                "odFrto, odDish, odVnds, odLocs, odCar, odCar#, odDsto, odDst#, odBl#)" &
                " Values " &
                "(" & _odInv_ & ", " &
                                _odItem & ", " &
                                Quoted(_odSlot) & ", " &
                                _odLin_ & ", " &
                                Quoted(_odUuom) & ", " &
                                _odQord & ", " &
                                _odUcst & ", " &
                                Quoted(_odTxst) & ", " &
                                _odQshp & ", " &
                                _odAvgc & ", " &
                                _odQprt & ", " &
                                Quoted(_odUprt) & ", " &
                                _odFroz & ", " &
                                Quoted(_odDel) & ", " &
                                Quoted(_odUpda) & ", " &
                                _odWhse & ", " &
                                _odPlst & ", " &
                                _odFrtc & ", " &
                                _odToDr & ", " &
                                Quoted(_odFrtO) & ", " &
                                Quoted(_odDish) & ", " &
                                _odVnds & ", " &
                                _odLocs & ", " &
                                Quoted(_odCar) & ", " &
                                _odCar_ & ", " &
                                Quoted(_odDstO) &
                                ", " & _odDst_ &
                                ", " & Quoted(_odBl_) &
                                ") "
                .Execute()
                _found = True
                Dim Daily_QryData As ADODB.Recordset
                Daily_QryData = connAS400.Execute("SELECT IDENTITY_VAL_LOCAL() as ID FROM SYSIBM.SYSDUMMY1", Parms, -1)
                _odId = Convert.ToInt64(Daily_QryData.Fields("ID").Value)

            End With

        End Sub

        Sub Delete()
            'connAS400.Open("Provider=IBMDA400;Data Source=PORTS ;", "pc", "pcpc")
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "delete from daily.soOrddp " & _
                                "where odID = " & _odId
                .Execute()
            End With

        End Sub

    End Class
End Namespace

