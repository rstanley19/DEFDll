Public Class FrmDirectShipDocumentType
    Inherits System.Windows.Forms.Form
    Dim GotLogNbr As Int32 = 0
    Dim myImageFolderLocation As Long = 0
    Friend WithEvents LabVendor As System.Windows.Forms.Label
    Friend WithEvents LabVendorName As System.Windows.Forms.Label
    Dim wrkBillingStatus As String = ""
    Dim myYesDeleteThisPage As Boolean

    Public Event CopyThisPage(ByVal PassDocumentNbr As Int32, ByVal PassImageLocation As Int32)
    Public Event ShowInvFreight(ByVal PassImgLo As Integer, ByVal PassDocNbr As Integer, ByVal PassDocType As Integer, PassDeleteThisPage As Boolean)

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonCredit As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LabShipToName As System.Windows.Forms.Label
    Friend WithEvents LabInvoice As System.Windows.Forms.Label
    Friend WithEvents LabAccountNumber As System.Windows.Forms.Label
    Friend WithEvents RadioButtonBackup As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonVerifyNow As System.Windows.Forms.RadioButton
    Friend WithEvents CmdKeep As System.Windows.Forms.Button
    Friend WithEvents CmdRemove As System.Windows.Forms.Button
    Friend WithEvents RadioButtonVerifyLater As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonVendorsInvoice As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonBackup = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCredit = New System.Windows.Forms.RadioButton()
        Me.RadioButtonVendorsInvoice = New System.Windows.Forms.RadioButton()
        Me.CmdKeep = New System.Windows.Forms.Button()
        Me.CmdRemove = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonVerifyLater = New System.Windows.Forms.RadioButton()
        Me.RadioButtonVerifyNow = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.LabShipToName = New System.Windows.Forms.Label()
        Me.LabInvoice = New System.Windows.Forms.Label()
        Me.LabAccountNumber = New System.Windows.Forms.Label()
        Me.LabVendor = New System.Windows.Forms.Label()
        Me.LabVendorName = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonBackup)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCredit)
        Me.GroupBox1.Controls.Add(Me.RadioButtonVendorsInvoice)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 69)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(184, 120)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Document Type"
        '
        'RadioButtonBackup
        '
        Me.RadioButtonBackup.Location = New System.Drawing.Point(5, 64)
        Me.RadioButtonBackup.Name = "RadioButtonBackup"
        Me.RadioButtonBackup.Size = New System.Drawing.Size(171, 18)
        Me.RadioButtonBackup.TabIndex = 8
        Me.RadioButtonBackup.Text = "Backup"
        '
        'RadioButtonCredit
        '
        Me.RadioButtonCredit.Location = New System.Drawing.Point(5, 40)
        Me.RadioButtonCredit.Name = "RadioButtonCredit"
        Me.RadioButtonCredit.Size = New System.Drawing.Size(171, 18)
        Me.RadioButtonCredit.TabIndex = 7
        Me.RadioButtonCredit.Text = "Credit"
        '
        'RadioButtonVendorsInvoice
        '
        Me.RadioButtonVendorsInvoice.Location = New System.Drawing.Point(5, 16)
        Me.RadioButtonVendorsInvoice.Name = "RadioButtonVendorsInvoice"
        Me.RadioButtonVendorsInvoice.Size = New System.Drawing.Size(171, 18)
        Me.RadioButtonVendorsInvoice.TabIndex = 6
        Me.RadioButtonVendorsInvoice.Text = "Direct Ship Invoice"
        '
        'CmdKeep
        '
        Me.CmdKeep.Location = New System.Drawing.Point(200, 165)
        Me.CmdKeep.Name = "CmdKeep"
        Me.CmdKeep.Size = New System.Drawing.Size(136, 24)
        Me.CmdKeep.TabIndex = 8
        Me.CmdKeep.Text = "Keep on Queue"
        Me.ToolTip1.SetToolTip(Me.CmdKeep, "Click here to keep this page on the Billing Work Queue.  You can then assign this" & _
        " page to additional B/L's")
        '
        'CmdRemove
        '
        Me.CmdRemove.Location = New System.Drawing.Point(200, 133)
        Me.CmdRemove.Name = "CmdRemove"
        Me.CmdRemove.Size = New System.Drawing.Size(136, 24)
        Me.CmdRemove.TabIndex = 9
        Me.CmdRemove.Text = "Remove from Queue"
        Me.ToolTip1.SetToolTip(Me.CmdRemove, "Click here to remove this page of the document from the Billing Work Queue")
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButtonVerifyLater)
        Me.GroupBox3.Controls.Add(Me.RadioButtonVerifyNow)
        Me.GroupBox3.Location = New System.Drawing.Point(200, 69)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(136, 56)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Verify Status"
        '
        'RadioButtonVerifyLater
        '
        Me.RadioButtonVerifyLater.Location = New System.Drawing.Point(5, 37)
        Me.RadioButtonVerifyLater.Name = "RadioButtonVerifyLater"
        Me.RadioButtonVerifyLater.Size = New System.Drawing.Size(128, 16)
        Me.RadioButtonVerifyLater.TabIndex = 7
        Me.RadioButtonVerifyLater.Text = "Verify Later"
        Me.ToolTip1.SetToolTip(Me.RadioButtonVerifyLater, "Select this option to continue assigning documents to invoices and then verify at" & _
        " a later time")
        '
        'RadioButtonVerifyNow
        '
        Me.RadioButtonVerifyNow.Location = New System.Drawing.Point(5, 16)
        Me.RadioButtonVerifyNow.Name = "RadioButtonVerifyNow"
        Me.RadioButtonVerifyNow.Size = New System.Drawing.Size(128, 16)
        Me.RadioButtonVerifyNow.TabIndex = 6
        Me.RadioButtonVerifyNow.Text = "Verify Now"
        Me.ToolTip1.SetToolTip(Me.RadioButtonVerifyNow, "Select this option to continue on to the data entry screen ")
        '
        'LabShipToName
        '
        Me.LabShipToName.AutoSize = True
        Me.LabShipToName.BackColor = System.Drawing.SystemColors.Control
        Me.LabShipToName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabShipToName.ForeColor = System.Drawing.Color.Black
        Me.LabShipToName.Location = New System.Drawing.Point(56, 24)
        Me.LabShipToName.Name = "LabShipToName"
        Me.LabShipToName.Size = New System.Drawing.Size(100, 13)
        Me.LabShipToName.TabIndex = 94
        Me.LabShipToName.Text = "LabShipToName"
        '
        'LabInvoice
        '
        Me.LabInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.LabInvoice.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabInvoice.ForeColor = System.Drawing.Color.Black
        Me.LabInvoice.Location = New System.Drawing.Point(8, 8)
        Me.LabInvoice.Name = "LabInvoice"
        Me.LabInvoice.Size = New System.Drawing.Size(88, 16)
        Me.LabInvoice.TabIndex = 93
        Me.LabInvoice.Text = "LabInvoice"
        '
        'LabAccountNumber
        '
        Me.LabAccountNumber.BackColor = System.Drawing.SystemColors.Control
        Me.LabAccountNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabAccountNumber.ForeColor = System.Drawing.Color.Black
        Me.LabAccountNumber.Location = New System.Drawing.Point(8, 24)
        Me.LabAccountNumber.Name = "LabAccountNumber"
        Me.LabAccountNumber.Size = New System.Drawing.Size(40, 13)
        Me.LabAccountNumber.TabIndex = 95
        Me.LabAccountNumber.Text = "00000"
        '
        'LabVendor
        '
        Me.LabVendor.BackColor = System.Drawing.SystemColors.Control
        Me.LabVendor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVendor.ForeColor = System.Drawing.Color.Black
        Me.LabVendor.Location = New System.Drawing.Point(8, 40)
        Me.LabVendor.Name = "LabVendor"
        Me.LabVendor.Size = New System.Drawing.Size(40, 13)
        Me.LabVendor.TabIndex = 97
        Me.LabVendor.Text = "00000"
        '
        'LabVendorName
        '
        Me.LabVendorName.AutoSize = True
        Me.LabVendorName.BackColor = System.Drawing.SystemColors.Control
        Me.LabVendorName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabVendorName.ForeColor = System.Drawing.Color.Black
        Me.LabVendorName.Location = New System.Drawing.Point(56, 40)
        Me.LabVendorName.Name = "LabVendorName"
        Me.LabVendorName.Size = New System.Drawing.Size(100, 13)
        Me.LabVendorName.TabIndex = 96
        Me.LabVendorName.Text = "LabVendorName"
        '
        'FrmDirectShipDocumentType
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(344, 198)
        Me.Controls.Add(Me.LabVendor)
        Me.Controls.Add(Me.LabVendorName)
        Me.Controls.Add(Me.LabAccountNumber)
        Me.Controls.Add(Me.LabShipToName)
        Me.Controls.Add(Me.LabInvoice)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CmdRemove)
        Me.Controls.Add(Me.CmdKeep)
        Me.Name = "FrmDirectShipDocumentType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Document"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Sub ShowMe(ByVal PassInvoice As Integer, ByVal PassAccount As Integer, ByVal PassLocation As String)
        LabInvoice.Text = PassInvoice
        LabAccountNumber.Text = PassAccount
        LabShipToName.Text = PassLocation
        BillDocumentNow = 0
        BillImageLocationNow = 0
        BillDocType = 0
        myYesDeleteThisPage = False
        ' me.showdialog 
        Me.Show()
    End Sub


    Private Function GetDocType() As Int32
        If RadioButtonVendorsInvoice.Checked Then Return 201
        If RadioButtonCredit.Checked Then Return 202
        If RadioButtonBackup.Checked Then Return 203


    End Function

    Private Sub CmdRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdRemove.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        UpdateFile()

        If RadioButtonVerifyNow.Checked Then
            BillDocumentNow = GotLogNbr
            BillImageLocationNow = myImageFolderLocation
        End If

        myYesDeleteThisPage = True

        Me.Cursor = System.Windows.Forms.Cursors.Default

        Me.Close()

    End Sub

    Private Sub CmdKeep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdKeep.Click
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        UpdateFile()

        If RadioButtonVerifyNow.Checked Then
            BillDocumentNow = GotLogNbr
            BillImageLocationNow = myImageFolderLocation
        End If

        myYesDeleteThisPage = False

        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Close()

    End Sub


    Private Sub UpdateFile()
        myImageFolderLocation = CreateImageFolderLocation()
        GotLogNbr = GetNxtLogNbrFlextStorImages()

        saveDocType = GetDocType()
        BillDocType = saveDocType

        ' output faxlog record
        Dim CallGeneric As New ADODB.Command()
        With CallGeneric
            .ActiveConnection = connAS400
            .CommandText = "insert into daily.soblfxp " & _
            "( bxinv#," & _
            " bxacc#," & _
            " bxkey," & _
            " bxtype," & _
            " bxstat," & _
            " bxdate," & _
            " bxuser, " & _
            " bximlo)" & _
            " values " & _
            "(" & LabInvoice.Text & ", " & _
            LabAccountNumber.Text & ", " & _
            GotLogNbr & ", " & _
            saveDocType & ", " & _
            "' ', " & _
            Format(Now(), "yyyyMMdd") & ", " & _
                        Quoted(UserId) & ", " & _
            myImageFolderLocation & ") "

            .Prepared = False
            .Execute()
            RaiseEvent CopyThisPage(GotLogNbr, myImageFolderLocation)

        End With

    End Sub

    Private Sub FrmDocumentType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case saveDocType
            Case Is = 201
                RadioButtonVendorsInvoice.Checked = True
            Case Is = 202
                RadioButtonCredit.Checked = True
            Case Is = 203
                RadioButtonBackup.Checked = True
            Case Else
                RadioButtonVendorsInvoice.Checked = True
        End Select

        If saveVerStat = 1 Then
            RadioButtonVerifyNow.Checked = True
        Else
            RadioButtonVerifyLater.Checked = True
        End If

    End Sub

    Private Sub FrmDocumentType_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed

        If RadioButtonVerifyLater.Checked = True Then
            saveVerStat = 2
        Else
            saveVerStat = 1
        End If

        If Not BillDocumentNow = 0 Then
            RaiseEvent ShowInvFreight(BillImageLocationNow, BillDocumentNow, BillDocType, myYesDeleteThisPage)
        Else
            If myYesDeleteThisPage Then
                RaiseEvent ShowInvFreight(0, 0, 0, myYesDeleteThisPage)
            End If
        End If
    End Sub
End Class
