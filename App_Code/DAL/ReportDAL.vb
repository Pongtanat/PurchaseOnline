Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.data

Public Class ReportDAL
    Inherits SQLConnectionDAL

    Dim connPurchase As SqlConnection '= GetSqlConnection("")

    Sub New(ByVal connDB As String)
        connPurchase = GetSqlConnection(connDB)
    End Sub

    Public Function getFactoryFromHist() As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DISTINCT AciEmpFactory ")
        sbSql.AppendLine(" FROM tbAccidentHist ")
        sbSql.AppendLine(" ORDER BY AciEmpFactory ")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbFactory", connPurchase).Tables("tbFactory")
    End Function

    Public Function getSectionFromHist() As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT DISTINCT AciEmpSection ")
        sbSql.AppendLine(" FROM tbAccidentHist")
        sbSql.AppendLine(" ORDER BY AciEmpSection ")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase).Tables("tbSection")
    End Function

    Public Function getReportHist(ByVal Criteria As ReportOBJ) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT * ")
        sbSql.AppendLine(" FROM tbAccidentHist")

        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        'param.AddWithValue("@AciEmpFNameEng", Criteria.EmployeeFirstNameEng)
        'param.AddWithValue("@AciEmpLNameEng", Criteria.EmployeeLastNameEng)
        Dim sbCondition As New StringBuilder

        If Criteria.ReportRequestBy <> "" Then
            sbCondition.AppendLine(" AciEmpCode=@AciEmpCode ")
            param.AddWithValue("@AciEmpCode", Criteria.ReportRequestBy)
        End If
        If Criteria.ReportSection <> "" Then
            If sbCondition.Length > 0 Then sbCondition.Append(" AND ")
            sbCondition.AppendLine(" AciEmpSection=@AciEmpSection ")
            param.AddWithValue("@AciEmpSection", Criteria.ReportSection)
        End If
        If Criteria.ReportFactory <> "" Then
            If sbCondition.Length > 0 Then sbCondition.Append(" AND ")
            sbCondition.AppendLine(" AciEmpFactory=@AciEmpFactory ")
            param.AddWithValue("@AciEmpFactory", Criteria.ReportFactory)
        End If
        If Not IsDBNull(Criteria.ReportDateFrom) And Not IsDBNull(Criteria.ReportDateTo) Then
            If sbCondition.Length > 0 Then sbCondition.Append(" AND ")
            sbCondition.AppendLine(" AciDate1 BETWEEN @AciDate1 AND @AciDate2 ")
            param.AddWithValue("@AciDate1", Criteria.ReportDateFrom)
            param.AddWithValue("@AciDate2", Criteria.ReportDateTo)
        End If

        If sbCondition.Length > 0 Then
            sbSql.AppendLine(" WHERE " & sbCondition.ToString)
        End If

        'sbSql.AppendLine(" AciEmpFNameEng=@AciEmpFNameEng ")
        'sbSql.AppendLine(" AciEmpLNameEng=@AciEmpLNameEng ")
        sbSql.AppendLine(" ORDER BY AciEmpSection,AciEmpFactory,AciSeq ")
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    End Function
End Class
