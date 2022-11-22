﻿Option Strict On
Option Explicit On
    Imports System

Namespace iSeries

    Public Class msWrnntp
        Private Parms As Object

        Property ID As Long
        Property WarrantyID As Long
        Property Note As String
        Property Userid As String
        Property NoteDate As Long
        Property NoteTime As Long

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(inKey As Long)
            Retrieve(inKey)
        End Sub

        Sub Retrieve(inID As Long)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", Userid, UserPassword)
            End If
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.msWrnntp " &
                            "where ID = " & inID, Parms, -1)
            _ID = inID
            _Note = ""
            _Userid = ""
            _WarrantyID = 0
            _NoteDate = 0
            _NoteTime = 0

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _ID = Convert.ToInt64(Daily_GenericDATA.Fields("ID").Value)
                _WarrantyID = Convert.ToInt64(Daily_GenericDATA.Fields("WarrantyID").Value)
                _Note = Convert.ToString(Daily_GenericDATA.Fields("Note").Value)
                _Userid = Convert.ToString(Daily_GenericDATA.Fields("Userid").Value)
                _NoteDate = Convert.ToInt64(Daily_GenericDATA.Fields("NoteDate").Value)
                _NoteTime = Convert.ToInt64(Daily_GenericDATA.Fields("NoteTime").Value)

                _found = True
            End If

        End Sub

        'Sub UpNoteDate()
        '    If Len(_Note) > 1000 Then
        '        _Note = Left(_Note, 1000)
        '        MsgBox("Note excceeded 1,000 characters and was truncated.", MsgBoxStyle.OkOnly, "Warning")
        '    End If

        '    '     connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserID, UserPassword)
        '    Dim Daily_GenericUpd As New ADODB.Command
        '    Daily_GenericUpd.ActiveConnection = connAS400
        '    Daily_GenericUpd.Prepared = False
        '    With Daily_GenericUpd

        '        .CommandText = "update daily.msWrnntp set " &
        '"( Note )" &
        '" = " &
        '"(" & Quoted(_Note) & ") " &
        '        "where snType = " & Quoted(_snType) & " and ID = " & _ID
        '        .Execute()
        '    End With

        'End Sub

        Sub Create()
            If Len(_Note) > 1000 Then
                _Note = Left(_Note, 1000)
                MsgBox("Note excceeded 1,000 characters and was truncated.", MsgBoxStyle.OkOnly, "Warning")
            End If

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.msWrnntp " &
                "( WarrantyID, Note, UserId, NoteDate, NoteTime )" &
                " Values " &
                "(" & _WarrantyID & ", " &
                                Quoted(_Note) & ",  " &
                                Quoted(_Userid) & ",  " &
                                _NoteDate & ",  " &
                                _NoteTime & ") "

                _found = True
                .Execute()

                Dim Parms As Object
                Dim Daily_QryData As ADODB.Recordset
                Daily_QryData = connAS400.Execute("SELECT IDENTITY_VAL_LOCAL() as ID FROM SYSIBM.SYSDUMMY1", Parms, -1)
                _ID = Convert.ToInt64(Daily_QryData.Fields("ID").Value)

            End With

        End Sub

        Sub Delete()
            '     connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserID, UserPassword)
            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "delete from daily.msWrnntp " &
                "where ID = " & _ID
                .Execute()
            End With

        End Sub

    End Class
End Namespace


