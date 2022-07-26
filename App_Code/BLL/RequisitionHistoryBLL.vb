'Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text

Public Class RequisitionHistoryBLL

    Private connDB As String = Nothing
    Private _applicantAdapter As RequisitionHistoryDAL

    Sub New(ByVal DB As String)
        connDB = DB
    End Sub

    Protected ReadOnly Property Adapter() As RequisitionHistoryDAL
        Get
            If _applicantAdapter Is Nothing Then
                _applicantAdapter = New RequisitionHistoryDAL(connDB)
            End If

            Return _applicantAdapter
        End Get
    End Property

    Public Function getRequestHistory(ByVal ReportOBJ As ReportOBJ) As DataTable
        Return Adapter.getRequestHistory(ReportOBJ)
    End Function

    Public Function getRequestHistoryAX(ByVal ReportOBJ As ReportOBJ) As DataTable
        Return Adapter.getRequestHistoryAX(ReportOBJ)
    End Function

    Public Function getSection() As DataTable
        Return Adapter.getSection
    End Function

    Public Function setDocRevision(ByVal Factory As String, ByVal ACCPAC As String, ByVal ReportName As String, ByVal Condition As String) As DocNoObj
        Dim dt As DataTable = Adapter.getDocRevision(Factory, ACCPAC, ReportName, Condition)
        Dim myDocNoObj As New DocNoObj
        If dt.Rows.Count > 0 Then
            myDocNoObj.DOCNO = dt.Rows(0)("DocNo")
            myDocNoObj.REVISION = dt.Rows(0)("Revision")
            myDocNoObj.EFFECTIVE = dt.Rows(0)("EffectDate")
        End If
        Return myDocNoObj
    End Function

    Public Function getRequestHistoryListByRequestNumber(ByVal RQNNUMBER As String) As DataTable
        Return Adapter.getRequestHistoryListByRequestNumber(RQNNUMBER)
    End Function

    Public Function getRequestHistoryListByRequestNumberAX(ByVal RQNNUMBER As String) As DataTable
        Return Adapter.getRequestHistoryListByRequestNumberAX(RQNNUMBER)
    End Function

    Public Sub UpdateApproveDate(ByVal ApvYearMonth As String, ByVal ApvDATE As String, ByVal RQNNUMBER As String)
        Adapter.UpdateApproveDate(ApvYearMonth, ApvDATE, RQNNUMBER)
    End Sub

    Public Sub UpdateApproveDateAX(ByVal ApvDATE As Date, ByVal purchReqId As String)
        Adapter.UpdateApproveDateAX(ApvDATE, purchReqId)
    End Sub

    Public Sub UpdatePRReceivedDate(ByVal NewPRReceivedDate As Integer, ByVal RQNNUMBER As String)
        Adapter.UpdatePRReceivedDate(NewPRReceivedDate, RQNNUMBER)
    End Sub

    Public Sub UpdatePRReceivedDateAX(ByVal NewPRReceivedDate As Date, ByVal purchReqId As String)
        Adapter.UpdatePRReceivedDateAX(NewPRReceivedDate, purchReqId)
    End Sub

    Public Function DeletePR(ByVal RQNNUMBER As String) As String
        Dim RequisitionHistoryDAL As New RequisitionHistoryDAL(connDB)
        Try
            Dim dt As DataTable = Adapter.getRequestSequenceByRequestNumber(RQNNUMBER)
            If dt.Rows.Count > 0 Then
                Dim strSEQ As String = dt.Rows(0)("RQNHSEQ")
                RequisitionHistoryDAL.BeginTrans()
                Dim strError As String = ""
                Dim ExecuteReturn As Common.cDBSQL.ExecuteReturn

                ExecuteReturn = RequisitionHistoryDAL.DeleteRQH(strSEQ)
                If ExecuteReturn.intRowEffected = -1 Then
                    strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
                End If

                ExecuteReturn = RequisitionHistoryDAL.DeleteRQH2(strSEQ)
                If ExecuteReturn.intRowEffected = -1 Then
                    strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
                End If

                ExecuteReturn = RequisitionHistoryDAL.DeleteRQL(strSEQ)
                If ExecuteReturn.intRowEffected = -1 Then
                    strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
                End If

                ExecuteReturn = RequisitionHistoryDAL.DeleteRQC(strSEQ)
                If ExecuteReturn.intRowEffected = -1 Then
                    strError += ExecuteReturn.strErrorMessage & ControlChars.NewLine
                End If

                If strError.Length > 0 Then
                    RequisitionHistoryDAL.RollBackTrans()
                    Return "Error : " & strError
                Else
                    RequisitionHistoryDAL.CommitTrans()
                    Return RQNNUMBER & " has been deleted."
                End If

            Else
                Return "Error " & RQNNUMBER & " not found."
            End If

        Catch ex As Exception
            RequisitionHistoryDAL.RollBackTrans()
            Return ex.Message
        End Try

    End Function

    Public Function DeletePRAX(ByVal strPurchReqId As String) As String
        Dim AX As New Microsoft.Dynamics.BusinessConnectorNet.Axapta
        Dim axRecord As Microsoft.Dynamics.BusinessConnectorNet.AxaptaRecord
        Dim strMessage As String = ""
        AX.Logon("", "", "", "")
        AX.TTSBegin()
        axRecord = AX.CreateAxaptaRecord("PurchReqTable")
        axRecord.ExecuteStmt("SELECT FORUPDATE * FROM %1 WHERE %1.PURCHREQID=='" & strPurchReqId & "'")
        Try
            If axRecord.Found Then
                axRecord.Delete()
                AX.TTSCommit()
            End If
            strMessage = strPurchReqId & " has been deleted."
        Catch ex As Exception
            AX.TTSAbort()
            strMessage = "Error : " & ex.Message
        End Try
        AX.Logoff()
        Return strMessage
    End Function

    Public Class DocNoObj
        Protected strDOCNO As String = ""
        Protected strREVISION As String = ""
        Protected strEFFECTIVE As Date = Date.MinValue

        Public Property DOCNO() As String
            Get
                Return strDOCNO
            End Get
            Set(ByVal value As String)
                strDOCNO = value
            End Set
        End Property
        Public Property REVISION() As String
            Get
                Return strREVISION
            End Get
            Set(ByVal value As String)
                strREVISION = value
            End Set
        End Property
        Public Property EFFECTIVE() As Date
            Get
                Return strEFFECTIVE
            End Get
            Set(ByVal value As Date)
                strEFFECTIVE = value
            End Set
        End Property
    End Class
End Class
