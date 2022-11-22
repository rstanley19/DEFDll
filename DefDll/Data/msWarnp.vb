

Option Strict On
    Option Explicit On
    Imports System

Namespace iSeries

    Public Class msWarnp
        Private Parms As Object

        Property ID As Long
        Property Account As Long
        Property Vendor As Long
        Property Item As String
        Property Serial As String
        Property StartDate As Long
        Property EndDate As Long
        Property LengthOfWarranty As Integer
        Property WarrantyPeriod As String
        Property PO As String

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(inKey As Long)
            Retrieve(inKey)
        End Sub

        Sub Retrieve(inID As Long)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", Serial, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.msWarnp " &
                            "where ID = " & inID, Parms, -1)
            _ID = inID
            _Account = 0
            _Vendor = 0
            _Item = ""
            _Serial = ""
            _StartDate = 0
            _EndDate = 0
            _LengthOfWarranty = 0
            _WarrantyPeriod = ""
            _PO = ""

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _ID = Convert.ToInt64(Daily_GenericDATA.Fields("ID").Value)
                _Account = Convert.ToInt64(Daily_GenericDATA.Fields("Account").Value)
                _Vendor = Convert.ToInt64(Daily_GenericDATA.Fields("Vendor").Value)
                _Item = Convert.ToString(Daily_GenericDATA.Fields("Item").Value)
                _Serial = Convert.ToString(Daily_GenericDATA.Fields("Serial").Value)
                _StartDate = Convert.ToInt64(Daily_GenericDATA.Fields("StartDate").Value)
                _EndDate = Convert.ToInt64(Daily_GenericDATA.Fields("EndDate").Value)
                _LengthOfWarranty = Convert.ToInt32(Daily_GenericDATA.Fields("LengthOfWarranty").Value)
                _WarrantyPeriod = Convert.ToString(Daily_GenericDATA.Fields("WarrantyPeriod").Value)
                _PO = Convert.ToString(Daily_GenericDATA.Fields("PO").Value)

                _found = True
            End If

        End Sub

        Sub Update()

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.msWarnp set " &
        "( Account, Vendor, Item, Serial, StartDate, EndDate, LengthOfWarranty, WarrantyPeriod, PO  )" &
        " = " &
        "(" & _Account & ", " &
                 _Vendor & ", " &
                                Quoted(_Item) & ",  " &
                                Quoted(_Serial) & ",  " &
                                _StartDate & ",  " &
                                _EndDate & ",  " &
                                _LengthOfWarranty & ",  " &
                                Quoted(_WarrantyPeriod) & ", " &
                                Quoted(_PO) & ") " &
                                "where ID = " & _ID
                .Execute()
            End With

        End Sub

        Sub Create()

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.msWarnp " &
                "( Account, Vendor, Item, Serial, StartDate, EndDate, LengthOfWarranty, WarrantyPeriod, PO )" &
                " Values " &
                "(" & _Account & ", " &
                 _Vendor & ", " &
                                Quoted(_Item) & ",  " &
                                Quoted(_Serial) & ",  " &
                                _StartDate & ",  " &
                                _EndDate & ",  " &
                                _LengthOfWarranty & ",  " &
                                Quoted(_WarrantyPeriod) & ", " &
                                Quoted(_PO) & ") "

                _found = True
                .Execute()

                Dim Parms As Object
                Dim Daily_QryData As ADODB.Recordset
                Daily_QryData = connAS400.Execute("SELECT IDENTITY_VAL_LOCAL() as ID FROM SYSIBM.SYSDUMMY1", Parms, -1)
                _ID = Convert.ToInt64(Daily_QryData.Fields("ID").Value)

            End With

        End Sub

        Sub Delete()

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "delete from daily.msWarnp " &
                "where ID = " & _ID
                .Execute()
            End With

        End Sub

    End Class
End Namespace


