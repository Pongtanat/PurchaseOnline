'Imports Microsoft.VisualBasic
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Configuration.ConfigurationManager

Public Class ReportBLL

    Private connDB As String = Nothing
    Private _applicantAdapter As ReportDAL = Nothing
    Protected report As ReportDocument

    Sub New(ByVal DB As String)
        connDB = DB
    End Sub

    Protected ReadOnly Property Adapter() As ReportDAL
        Get
            If _applicantAdapter Is Nothing Then
                _applicantAdapter = New ReportDAL(connDB)
            End If

            Return _applicantAdapter
        End Get
    End Property

    Public Function getFactoryFromHist() As DataTable
        Return Adapter.getFactoryFromHist
    End Function

    Public Function getSectionFromHist() As DataTable
        Return Adapter.getSectionFromHist
    End Function

    Public Function getReportHistByCriteria(ByVal Criteria As ReportOBJ) As DataTable
        Return Adapter.getReportHist(Criteria)
    End Function

    Public Function Load(ByVal reportfile As String, _
                         ByVal Sequence As String, _
                         ByVal Database As String, _
                         ByVal DocNo As String, _
                         ByVal Revision As String, _
                         ByVal Effective As String) As ReportDocument
        Dim paramDiscreteValue As ParameterDiscreteValue
        Dim paramFields As New ParameterFields
        Dim paramField As ParameterField

        paramField = New ParameterField
        paramField.Name = "RQNFROM"
        paramDiscreteValue = New ParameterDiscreteValue
        paramDiscreteValue.Value = Sequence
        paramField.CurrentValues.Add(paramDiscreteValue)
        paramFields.Add(paramField)

        paramField = New ParameterField
        paramField.Name = "RQNTO"
        paramDiscreteValue = New ParameterDiscreteValue
        paramDiscreteValue.Value = Sequence
        paramField.CurrentValues.Add(paramDiscreteValue)
        paramFields.Add(paramField)

        paramField = New ParameterField
        paramField.Name = "DOCNO"
        paramDiscreteValue = New ParameterDiscreteValue
        paramDiscreteValue.Value = DocNo
        paramField.CurrentValues.Add(paramDiscreteValue)
        paramFields.Add(paramField)

        paramField = New ParameterField
        paramField.Name = "REV"
        paramDiscreteValue = New ParameterDiscreteValue
        paramDiscreteValue.Value = Revision
        paramField.CurrentValues.Add(paramDiscreteValue)
        paramFields.Add(paramField)

        paramField = New ParameterField
        paramField.Name = "EFF"
        paramDiscreteValue = New ParameterDiscreteValue
        paramDiscreteValue.Value = Effective
        paramField.CurrentValues.Add(paramDiscreteValue)
        paramFields.Add(paramField)

        Dim rpt As New Common.cReport
        'Dim report As ReportDocument = rpt.OpenReport(reportfile, _
        '                        AppSettings("SERVER").ToString(), _
        '                        Database, _
        '                        AppSettings("User").ToString(), _
        '                        AppSettings("Password").ToString(), _
        '                        paramFields)

        'report = rpt.OpenReport(reportfile, _
        '                        AppSettings("SERVER").ToString(), _
        '                        Database, _
        '                        AppSettings("User").ToString(), _
        '                        AppSettings("Password").ToString(), _
        '                        paramFields)
        If ConfigurationManager.AppSettings("SERVER") = "LIVE" Then
            report = rpt.OpenReport(reportfile, _
                                            AppSettings("SERVER_LIVE").ToString(), _
                                            Database, _
                                            ConfigurationManager.AppSettings("DBUser_LIVE"), _
                                            ConfigurationManager.AppSettings("DBPwd_LIVE"), _
                                            paramFields)
        Else
            report = rpt.OpenReport(reportfile, _
                                            AppSettings("SERVER_TEST").ToString(), _
                                            Database, _
                                            ConfigurationManager.AppSettings("DBUser_TEST"), _
                                            ConfigurationManager.AppSettings("DBPwd_TEST"), _
                                            paramFields)
        End If

        'report.RecordSelectionFormula = "({PURCHREQTABLE.PURCHREQID}>={?RQNFROM}) AND ({PURCHREQTABLE.PURCHREQID}<={?RQNTO})"
        Return report
    End Function

    Public Sub CloseReport()
        report.Close()
        report.Dispose()
    End Sub
End Class
