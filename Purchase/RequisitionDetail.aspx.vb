Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared
Imports Neodynamic.SDK.Web

Partial Class Purchase_RequisitionDetail
    Inherits System.Web.UI.Page
    Dim reportDocument As New ReportDocument

    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        If WebClientPrint.ProcessPrintJob(Request) Then
            Dim RQNNUMBER As String = Request.QueryString("RQN")
            Dim DocNo As String = Request.QueryString("DOC")
            Dim Revision As String = Request.QueryString("REV")
            Dim EffectiveDate As String = Request.QueryString("EFF")
            Dim Report As String = Request.QueryString("RPT")
            Dim reportfile As String = ""

            If Report <> "" Then
                reportfile = Server.MapPath("../Report/" & Report)
            Else
                reportfile = Server.MapPath("../Report/PORQN01.RPT")
            End If
            Session("report") = reportfile

            'Dim ReportBLL As New ReportBLL("HOAX61TEST")
            Dim ReportBLL As New ReportBLL(Session("DATABASE"))
            'reportDocument.Close()
            'reportDocument = ReportBLL.Load(reportfile, RQNNUMBER, "HOAX61TEST", DocNo, Revision, EffectiveDate)
            reportDocument = ReportBLL.Load(reportfile, RQNNUMBER, Session("DATABASE"), DocNo, Revision, EffectiveDate)

            Dim pdfContent As Byte() = Nothing
            Using ms As System.IO.MemoryStream = DirectCast(reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat), System.IO.MemoryStream)
                pdfContent = ms.ToArray()
            End Using

            Dim printerName As String = Server.UrlDecode(Request("printerName"))

            Dim fileName As String = Guid.NewGuid().ToString("N") + ".pdf"

            Dim file As New PrintFile(pdfContent, fileName)
            Dim cpj As New ClientPrintJob()
            cpj.PrintFile = file
            If printerName = "Default Printer" Then
                cpj.ClientPrinter = New DefaultPrinter()
            Else
                cpj.ClientPrinter = New InstalledPrinter(printerName)
            End If
            cpj.SendToClient(Response)

        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim RQNNUMBER As String = Request.QueryString("RQN")
        Dim DocNo As String = Request.QueryString("DOC")
        Dim Revision As String = Request.QueryString("REV")
        Dim EffectiveDate As String = Request.QueryString("EFF")
        Dim Report As String = Request.QueryString("RPT")
        If RQNNUMBER <> "" Then
            Dim reportfile As String = ""
            If Report <> "" Then
                reportfile = Server.MapPath("../Report/" & Report)
            Else
                reportfile = Server.MapPath("../Report/PORQN01.RPT")
            End If
            Session("report") = reportfile

            'Dim ReportBLL As New ReportBLL("HOAX61TEST")
            Dim ReportBLL As New ReportBLL(Session("DATABASE"))
            'reportDocument.Close()
            'reportDocument = ReportBLL.Load(reportfile, RQNNUMBER, "HOAX61TEST", DocNo, Revision, EffectiveDate)
            reportDocument = ReportBLL.Load(reportfile, RQNNUMBER, Session("DATABASE"), DocNo, Revision, EffectiveDate)

            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf
            CrystalReportViewer1.ReportSource = reportDocument
            'CrystalReportViewer1.DisplayGroupTree = False
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None

            Response.Buffer = False
            'Response.Buffer = True
            Response.ClearContent()
            Response.ClearHeaders()

            'Response.ContentType = "application/pdf"

            'Dim oStream As New System.IO.MemoryStream()
            'oStream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)

            'Try
            '    Response.BinaryWrite(oStream.ToArray())
            '    Response.End()
            'Catch err As Exception
            '    Response.Write("< BR >")
            '    Response.Write(err.Message.ToString)
            'End Try
            'reportDocument.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, True, RQNNUMBER)
        Else
            'CrystalReportViewer1.Visible = False
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        reportDocument.Close()
        reportDocument.Dispose()
        GC.Collect()
    End Sub

End Class
