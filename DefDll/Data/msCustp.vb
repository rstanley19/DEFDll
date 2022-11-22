Option Strict On
Option Explicit On
Imports System

Namespace iSeries

    Public Class msCustp
        Private Parms As Object
        '    Private connAS400 As New ADODB.Connection

        Property cmAcc_ As Long
        Property cmBlto As Long
        Property cmName As String
        Property cmAdr1 As String
        Property cmAdr2 As String
        Property cmCity As String
        Property cmSt As String
        Property cmZip As Integer
        Property cmZip4 As Integer
        Property cmCnty As Integer
        Property cmTel_ As Long
        Property cmFax_ As Long
        Property cmDel As String
        Property cmAkey As String
        Property cmCtyp As String
        Property cmUST As String
        Property cmCst As Integer ' credit status
        Property cmClim As Long ' credit limit
        Property cmslm_ As Integer ' saleman #
        Property cmslmd As Integer ' saleman #
        Property cmcar_ As Integer ' carrier #
        Property cmReg As String ' region
        Property cmCcrq As String ' Credit Card Required
        Property cmCexp As Long ' WV Cert expired
        Property cmRolu As Long ' Roll Up Aging
        Property cmPorq As String ' PO Required
        ' tanks and sizes added 01/12/2016
        Property cmTnk1 As String
        Property cmSiz1 As Integer
        Property cmTnk2 As String
        Property cmSiz2 As Integer
        Property cmTnk3 As String
        Property cmSiz3 As Integer
        Property cmTnk4 As String
        Property cmSiz4 As Integer
        Property cmTnk5 As String
        Property cmSiz5 As Integer
        Property cmcpo_ As String
        Property cmclas As String
        Property cmScex As String
        Property cmStmt As String
        Property cmDunx As String
        Property cmProm As String
        Property cmRetg As String
        Property cmRetD As String
        Property cmDeal As String
        Property cmPmpc As String
        Property cmFdId As String
        Property cmStId As String
        Property cmDtop As Long
        Property cmDtdl As Long
        Property cmDtRe As Long
        Property cmBilc As String
        Property cmHow As String
        Property cmFidt As Long
        Property cmTerm As Integer
        Property cmDTrm As Integer
        Property cmMtds As Decimal
        Property cmMtdr As Decimal
        Property cmYtds As Decimal
        Property cmYtdr As Decimal
        Property cmLidt As Long
        Property cmLpdt As Long
        Property cmAvdy As Long
        Property cmConm As String
        Property cmCoti As String
        Property cmCoph As Long
        Property cmCoex As Integer
        Property cmClrk As String
        Property cmAvgl As Integer
        Property cmELM As String
        Property cmELMd As String

        Property gallons As Long ' gallons purchased

        Property found As Boolean

        Sub New()
        End Sub

        Sub New(ByVal GetCustomer As Long, GetGallons As Boolean)
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
            End If

            Dim Daily_GenericDATA As ADODB.Recordset

            Dim mySql As String = "select daily.mscustp.* " &
            "from daily.mscustp " &
            "where cmacc# = " & GetCustomer

            If GetGallons Then
                mySql = "select daily.mscustp.*, ifnull(gallons, 0) as gallons " &
            "from daily.mscustp " &
            "left join (select sum(sdqtyd) as gallons, soblto from daily.soprodp " &
            "left join daily.soinvp on soinv# = sdinv# group by soblto) as g on g.soblto = cmacc# " &
            "where cmacc# = " & GetCustomer & " and cmacc# = cmblto"
            End If

            Daily_GenericDATA = connAS400.Execute(mySql, Parms, -1)
            _cmAcc_ = GetCustomer

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _cmBlto = Convert.ToInt32(Daily_GenericDATA.Fields("cmBlto").Value)
                _cmName = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmname").Value))
                _cmAdr1 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmAdr1").Value))
                _cmAdr2 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmAdr2").Value))
                _cmCity = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmCity").Value))
                _cmSt = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmSt").Value))
                _cmZip = Convert.ToInt32(Daily_GenericDATA.Fields("cmZip").Value)
                _cmZip4 = Convert.ToInt32(Daily_GenericDATA.Fields("cmZip4").Value)
                _cmCnty = Convert.ToInt32(Daily_GenericDATA.Fields("cmCnty").Value)
                _cmTel_ = Convert.ToInt64(Daily_GenericDATA.Fields("cmTel#").Value)
                _cmFax_ = Convert.ToInt64(Daily_GenericDATA.Fields("cmfax#").Value)
                _cmDel = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmDel").Value))
                _cmAkey = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmakey").Value))
                _cmCtyp = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmCtyp").Value))
                _cmUST = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmUst").Value))
                _cmCst = Convert.ToInt32(Daily_GenericDATA.Fields("cmCst").Value)
                _cmClim = Convert.ToInt64(Daily_GenericDATA.Fields("cmClim").Value)
                _cmslm_ = Convert.ToInt32(Daily_GenericDATA.Fields("cmSlm#").Value)
                _cmslmd = Convert.ToInt32(Daily_GenericDATA.Fields("cmSlmd").Value)
                _cmcar_ = Convert.ToInt32(Daily_GenericDATA.Fields("cmCar#").Value)
                _cmReg = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmReg").Value))
                _cmCcrq = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmCcrq").Value))
                _cmPorq = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmPorq").Value))
                _cmCexp = Convert.ToInt64(Daily_GenericDATA.Fields("cmCexp").Value)
                _cmRolu = Convert.ToInt32(Daily_GenericDATA.Fields("cmRolu").Value)
                _cmTnk1 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmTnk1").Value))
                _cmSiz1 = Convert.ToInt32(Daily_GenericDATA.Fields("cmSiz1").Value)
                _cmTnk2 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmTnk2").Value))
                _cmSiz2 = Convert.ToInt32(Daily_GenericDATA.Fields("cmSiz2").Value)
                _cmTnk3 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmTnk3").Value))
                _cmSiz3 = Convert.ToInt32(Daily_GenericDATA.Fields("cmSiz3").Value)
                _cmTnk4 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmTnk4").Value))
                _cmSiz4 = Convert.ToInt32(Daily_GenericDATA.Fields("cmSiz4").Value)
                _cmTnk5 = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmTnk5").Value))
                _cmSiz5 = Convert.ToInt32(Daily_GenericDATA.Fields("cmSiz5").Value)
                _cmcpo_ = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmcpo#").Value))
                _cmclas = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmclas").Value))
                _cmStmt = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmstmt").Value))
                _cmScex = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmscex").Value))
                _cmDunx = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmdunx").Value))
                _cmProm = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmprom").Value))
                _cmRetg = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmretg").Value))
                _cmRetD = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmretd").Value))
                _cmDeal = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmdeal").Value))
                _cmPmpc = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmpmpc").Value))
                _cmFdId = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmfdid").Value))
                _cmStId = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmstid").Value))
                _cmDtop = Convert.ToInt64(Daily_GenericDATA.Fields("cmdtop").Value)
                _cmDtdl = Convert.ToInt64(Daily_GenericDATA.Fields("cmdtdl").Value)
                _cmDtRe = Convert.ToInt64(Daily_GenericDATA.Fields("cmdtre").Value)
                _cmBilc = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmbilc").Value))
                _cmUST = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmust").Value))
                _cmHow = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmhow").Value))

                _cmFidt = Convert.ToInt64(Daily_GenericDATA.Fields("cmfidt").Value)
                _cmTerm = Convert.ToInt32(Daily_GenericDATA.Fields("cmTerm").Value)
                _cmDTrm = Convert.ToInt32(Daily_GenericDATA.Fields("cmDTrm").Value)
                _cmMtds = Convert.ToDecimal(Daily_GenericDATA.Fields("cmmtds").Value)
                _cmMtdr = Convert.ToDecimal(Daily_GenericDATA.Fields("cmmtdr").Value)
                _cmYtds = Convert.ToDecimal(Daily_GenericDATA.Fields("cmYtds").Value)
                _cmYtdr = Convert.ToDecimal(Daily_GenericDATA.Fields("cmYtdr").Value)
                _cmLidt = Convert.ToInt64(Daily_GenericDATA.Fields("cmlidt").Value)
                _cmLpdt = Convert.ToInt64(Daily_GenericDATA.Fields("cmLpdt").Value)
                _cmAvdy = Convert.ToInt32(Daily_GenericDATA.Fields("cmAvdy").Value)
                _cmConm = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmconm").Value))
                _cmCoti = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmcoti").Value))
                _cmCoph = Convert.ToInt64(Daily_GenericDATA.Fields("cmcoph").Value)
                _cmCoex = Convert.ToInt32(Daily_GenericDATA.Fields("cmcoex").Value)
                _cmClrk = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmclrk").Value))
                _cmAvgl = Convert.ToInt32(Daily_GenericDATA.Fields("cmAvgl").Value)
                _cmELM = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmelm").Value))
                _cmELMd = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmelmd").Value))

                If GetGallons Then
                    _gallons = Convert.ToInt64(Daily_GenericDATA.Fields("gallons").Value)
                Else
                    _gallons = 0
                End If
                _found = True
            End If

        End Sub

        Function GetBillToName(ByVal GetCustomer As Long) As Boolean
            If connAS400.State = 0 Then
                connAS400.Open("Provider=IBMDA400;Data Source=" & iSeriesIP & ";", UserId, UserPassword)
            End If

            Dim Daily_GenericDATA As ADODB.Recordset

            Dim mySql As String = "select cmname, cmdel " &
            "from daily.mscustp " &
            "where cmacc# = cmblto and cmacc# = " & GetCustomer

            Daily_GenericDATA = connAS400.Execute(mySql, Parms, -1)
            _cmAcc_ = GetCustomer

            If Daily_GenericDATA.EOF Then
                _found = False
            Else
                _cmName = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmname").Value))
                _cmDel = Trim(Convert.ToString(Daily_GenericDATA.Fields("cmDel").Value))
                _found = True
            End If

            Return _found
        End Function

        Function Update() As Boolean

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.mscustp set " &
"( cmfidt, cmclim, cmterm, cmdtrm, cmconm, cmcoti, cmcoph, cmcoex, " &
"cmname, cmadr1, cmadr2, cmcity, cmst, cmzip, cmzip4, " &
"cmcnty, cmtel#, cmfax#, cmslm#, cmslmd, cmclrk, " &
"cmakey, cmctyp, cmclas, cmstmt, cmscex, cmdunx, cmprom, " &
"cmretg, cmretd, cmdeal, cmpmpc, cmhow, cmreg, " &
"cmfdid, cmstid, cmdtop, cmdtdl, cmdtre, cmbilc, cmust, cmdel, cmAvgl, cmelm, cmelmd)" &
" = " &
"(" & _cmFidt & ", " &
_cmClim & ", " & _cmTerm & ", " & _cmDTrm & ", " &
Quoted(_cmConm) & ", " & Quoted(_cmCoti) & "," & _cmCoph & ", " & _cmCoex & ", " &
Quoted(_cmName) & ", " & Quoted(_cmAdr1) & "," & Quoted(_cmAdr2) & ", " & Quoted(_cmCity) & ", " &
Quoted(_cmSt) & ", " & _cmZip & "," & _cmZip4 & ", " &
_cmCnty & ", " & _cmTel_ & "," & _cmFax_ & "," & _cmslm_ & "," & _cmslmd & "," & Quoted(_cmClrk) & "," &
Quoted(_cmAkey) & ", " & Quoted(_cmCtyp) & "," & Quoted(_cmclas) & "," & Quoted(_cmStmt) & "," &
Quoted(_cmScex) & "," & Quoted(_cmDunx) & "," & Quoted(_cmProm) & "," &
Quoted(_cmRetg) & "," & Quoted(_cmRetD) & "," & Quoted(_cmDeal) & "," &
Quoted(_cmPmpc) & "," & Quoted(_cmHow) & "," & Quoted(_cmReg) & "," &
Quoted(_cmFdId) & "," & Quoted(_cmStId) & "," & _cmDtop & "," &
_cmDtdl & "," & _cmDtRe & "," & Quoted(_cmBilc) & "," & Quoted(_cmUST) & "," & Quoted(_cmDel) & ", " & _cmAvgl & "," &
 Quoted(_cmELM) & "," & Quoted(_cmELMd) & ") " &
"where cmacc# = " & _cmAcc_

                .Execute()
            End With

            Return True
        End Function

        Function UpdateFinancials(inBillTo As Long, inCmRout As String, inCmAcct As String) As Boolean

            Dim Daily_GenericUpd As New ADODB.Command
            Daily_GenericUpd.ActiveConnection = connAS400
            Daily_GenericUpd.Prepared = False
            With Daily_GenericUpd

                .CommandText = "update daily.mscustp set " & _
"( cmrout, cmacct )" & _
" = " & _
"(" & Quoted(inCmRout) & ", " & Quoted(inCmAcct) & ") " & _
"where cmacc# = " & inBillTo
                .Execute()
            End With

            Return True
        End Function

    End Class
End Namespace


