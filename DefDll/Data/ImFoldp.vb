Option Strict On
Option Explicit On

Imports System.IO
Imports System

Namespace iSeries

    Public Class ImFoldp
        Private m_imKey As Long
        Private m_imDir As String
        Private m_found As Boolean

        Sub New()
        End Sub


        Sub Retrieve(ByVal inyyyyMM As Long)
            Dim Parms As Object
            Dim Daily_GenericDATA As ADODB.Recordset
            Daily_GenericDATA = connAS400.Execute("select * from daily.imfoldp where imkey = " & inyyyyMM, Parms, -1)

            InitializeFields()

            If Daily_GenericDATA.EOF Then
                m_found = False
            Else
                MoveFields(Daily_GenericDATA)

                m_found = True
            End If

        End Sub

        Sub MoveFields(ByVal inDaily_GenericDATA As ADODB.Recordset)

            m_imKey = Convert.ToInt64(inDaily_GenericDATA.Fields("imkey").Value)
            m_imDir = Trim(Convert.ToString(inDaily_GenericDATA.Fields("imdir").Value))

        End Sub

        Sub InitializeFields()
            m_imKey = 0
            m_imDir = ""
        End Sub

        Function Create() As Long
            Dim SaveImageFileLocation As String = myBinAppSettings.GetSetting("SaveImageFileLocation") & m_imKey & "\"

            If Not Directory.Exists(SaveImageFileLocation) Then
                Directory.CreateDirectory(SaveImageFileLocation)
            End If

            m_imDir = SaveImageFileLocation

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd
                .CommandText = "insert into daily.imfoldp " & _
                "( imkey, imdir)" & _
                " Values " & _
                "(" & m_imKey & ", " & _
                                Quoted(m_imDir) & ") "

                .Execute()
            End With

        End Function


        Property imKey() As Long
            Get
                Return m_imKey
            End Get
            Set(ByVal Value As Long)
                m_imKey = Value
            End Set
        End Property

        Property imDir() As String
            Get
                Return m_imDir
            End Get
            Set(ByVal Value As String)
                m_imDir = Value
            End Set
        End Property

        Property found() As Boolean
            Get
                Return m_found
            End Get
            Set(ByVal Value As Boolean)
                m_found = Value
            End Set
        End Property

    End Class
End Namespace






