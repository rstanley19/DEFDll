Public Class FrmCrystalViewer
    Public Sub New(ByVal inDescription As String, ByVal inDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Text = inDescription
        CrystalReportViewer1.ReportSource = inDocument

    End Sub
End Class