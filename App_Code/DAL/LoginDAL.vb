Imports System.Data.SqlClient
Imports System.Data

Public Class LoginDAL
    Inherits SQLConnectionDAL

    Private connPR_Online As SqlConnection = GetSqlConnection("PR_Online")

    Public Function Login(ByVal LoginOBJ As LoginOBJ) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT tbUsers.AllowSelectVendor")
        sbSql.AppendLine(" , tbUsers.AllowSubmitPRApprove")
        sbSql.AppendLine(" , tbUsers.AllowSubmitPRReceive")
        sbSql.AppendLine(" , tbUsers.AllowChangeRequisition")
        sbSql.AppendLine(" , tbUsers.AllowEditPermission")
        sbSql.AppendLine(" , tbUsers.AllowAnotherSection")
        sbSql.AppendLine(" , tbUsers.Section, ISNULL(FilterItemCode,'') FilterItemCode")
        sbSql.AppendLine(" , ISNULL(Preparer.EmpCode,'') PreparerEmpCode, ISNULL(Preparer.UserName,'') PreparerName")
        sbSql.AppendLine(" FROM tbUsers")
        sbSql.AppendLine(" LEFT OUTER JOIN tbFilterItem ON tbUsers.ACCPAC=tbFilterItem.ACCPAC")
        sbSql.AppendLine(" LEFT OUTER JOIN tbUsers Preparer ON tbUsers.ACCPAC=Preparer.ACCPAC")
        sbSql.AppendLine(" WHERE tbUsers.DomainName = @DomainName")
        sbSql.AppendLine(" AND tbUsers.UserName = @UserName")
        sbSql.AppendLine(" AND tbUsers.ACCPAC = @ACCPAC")
        sbSql.AppendLine(" AND Preparer.PRPreparer=1")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@DomainName", LoginOBJ.DomainName)
        param.AddWithValue("@UserName", LoginOBJ.UserName)
        param.AddWithValue("@ACCPAC", LoginOBJ.ACCPAC)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbLogin", connPR_Online, param).Tables("tbLogin")
    End Function

    Public Function LoginAXByAD(ByVal LoginOBJ As LoginOBJ) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT HCMWORKER.PERSON,HCMWORKER.RECID,DIRPARTYTABLE.KNOWNAS ADUSER")
        sbSql.AppendLine(" ,DIRPARTYTABLE.NAME")
        sbSql.AppendLine(" ,HcmWorkerTitle.OFFICELOCATION,HCMWORKER.PERSONNELNUMBER")
        sbSql.AppendLine(" ,DimFinTag_head.ECL_SHORTNAME")
        sbSql.AppendLine(" ,HcmWorkerTask.WORKERTASKID,HcmWorkerTask.DESCRIPTION")
        sbSql.AppendLine(" ,HcmWorkerTask.NOTE")
        sbSql.AppendLine(" FROM HCMWORKER")
        sbSql.AppendLine(" INNER JOIN HcmWorkerTitle ON HcmWorkerTitle.WORKER=HCMWORKER.RECID")
        sbSql.AppendLine(" INNER JOIN DirPersonName ON DirPersonName.PERSON=HCMWORKER.PERSON")
        sbSql.AppendLine(" INNER JOIN DIRPARTYTABLE ON DIRPARTYTABLE.RECID=HCMWORKER.PERSON")
        sbSql.AppendLine(" INNER JOIN HCMemployment ON HCMemployment.WORKER=hcmworker.RECID")

        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEVALUESETITEM DimValSet_head ON DimValSet_head.DIMENSIONATTRIBUTEVALUESET=HCMemployment.DefaultDimension")
        sbSql.AppendLine(" INNER JOIN DimensionAttributeValue DimVal_head ON DimVal_head.RECID=DimValSet_head.DIMENSIONATTRIBUTEVALUE")
        sbSql.AppendLine(" INNER JOIN DimensionFinancialTag DimFinTag_head ON DimFinTag_head.recid=DimVal_head.ENTITYINSTANCE")
        sbSql.AppendLine(" INNER JOIN DimensionAttribute Dim_head ON Dim_head.recid=DimVal_head.DIMENSIONATTRIBUTE")

        sbSql.AppendLine(" INNER JOIN HCMWORKERTASKASSIGNMENT ON HCMWORKERTASKASSIGNMENT.WORKER=HCMWORKER.RECID")
        sbSql.AppendLine(" INNER JOIN HcmWorkerTask ON HcmWorkerTask.RECID=HCMWORKERTASKASSIGNMENT.WORKERTASK")
        sbSql.AppendLine(" WHERE DirPersonName.VALIDTO>GETDATE() AND Dim_head.NAME='D2_Section'")
        sbSql.AppendLine(" AND HcmWorkerTitle.VALIDTO>GETDATE()")
        sbSql.AppendLine(" AND HcmWorkerTask.WORKERTASKID LIKE 'PurchaseOnline%'")
        sbSql.AppendLine(" AND DIRPARTYTABLE.KNOWNAS=@User")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@User", String.Format("{0}\{1}", LoginOBJ.DomainName, LoginOBJ.UserName))
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbWorker", connPR_Online, param).Tables("tbWorker")
    End Function

    Public Sub AccessLog(ByVal LoginOBJ As LoginOBJ)
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" UPDATE tbUsers SET LastAccess=getdate()")
        sbSql.AppendLine(" WHERE tbUsers.DomainName = @DomainName")
        sbSql.AppendLine(" AND tbUsers.UserName = @UserName")
        sbSql.AppendLine(" AND tbUsers.ACCPAC = @ACCPAC")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@DomainName", LoginOBJ.DomainName)
        param.AddWithValue("@UserName", LoginOBJ.UserName)
        param.AddWithValue("@ACCPAC", LoginOBJ.ACCPAC)
        Dim cDBSQL As New Common.cDBSQL
        cDBSQL.ExecuteData(sbSql.ToString, connPR_Online, param)
    End Sub

    Public Function CheckRules(ByVal LoginOBJ As LoginOBJ, ByVal OUGroup As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT *")
        sbSql.AppendLine(" FROM tbRules ")
        sbSql.AppendLine(" INNER JOIN tbConnection ON tbRules.FACTORY = tbConnection.FACTORY")
        sbSql.AppendLine(" WHERE tbRules.DomainName = @DomainName AND tbConnection.ACCPAC=@ACCPAC")
        sbSql.AppendLine(" AND tbRules.OUGROUP IN ('" & OUGroup & "')")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@DomainName", LoginOBJ.DomainName)
        param.AddWithValue("@ACCPAC", LoginOBJ.ACCPAC)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbLogin", connPR_Online, param).Tables("tbLogin")
    End Function

    Public Function GetConnection(ByVal LoginOBJ As LoginOBJ) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT ISNULL(DocNo,'') DOCNO")
        sbSql.AppendLine(" ,ISNULL(Revision,'') REVISION")
        sbSql.AppendLine(" ,CASE WHEN EffectDate IS NULL THEN '' ELSE CONVERT(CHAR(10),EffectDate,103) END EffectDate")
        sbSql.AppendLine(" FROM tbConnection ")
        sbSql.AppendLine(" WHERE ACCPAC = @ACCPAC ")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@ACCPAC", LoginOBJ.ACCPAC)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbConnection", connPR_Online, param).Tables("tbConnection")
    End Function
End Class
