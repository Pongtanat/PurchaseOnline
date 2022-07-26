Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class MasterPageDAL
    Inherits SQLConnectionDAL
    Dim connPurchase As SqlConnection '= GetSqlConnection(connDB)
    Private Trans As SqlTransaction

    Sub New(ByVal connDB As String)
        connPurchase = GetSqlConnection(connDB)
    End Sub

    Public Function getAllSectionByFactory(ByVal strFactory As String) As DataTable
        Dim sbSql As New StringBuilder
        sbSql.AppendLine(" SELECT SSec.ECL_SHORTNAME FROM DIMENSIONATTRIBUTE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEDIRCATEGORY ON DIMENSIONATTRIBUTE.RECID=DIMENSIONATTRIBUTEDIRCATEGORY.DIMENSIONATTRIBUTE")
        sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG Fac ON DIMENSIONATTRIBUTEDIRCATEGORY.DIRCATEGORY=Fac.FINANCIALTAGCATEGORY")
        sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG SSec ON Fac.ECL_COMCODE=SSec.ECL_COMCODE")
        sbSql.AppendLine(" WHERE NAME='D1_Factory' AND NOT(SSec.VALUE LIKE Fac.ECL_COMCODE + '%') ")
        sbSql.AppendLine(" AND SSec.VALUE <> Fac.VALUE")
        'sbSql.AppendLine("AND Fac.VALUE = @FACTORYVALUE")
        sbSql.AppendLine("AND Fac.ECL_SHORTNAME = @FACTORYVALUE")
        sbSql.AppendLine(" GROUP BY SSec.ECL_SHORTNAME")
        sbSql.AppendLine(" ORDER BY SSec.ECL_SHORTNAME")
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        param.AddWithValue("@FACTORYVALUE", strFactory)
        Dim cDBSQL As New Common.cDBSQL
        Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    End Function

    'Public Function getAllSubSectionByFactory(ByVal strFactory As String) As DataTable
    '    Dim sbSql As New StringBuilder
    '    sbSql.AppendLine(" SELECT SSec.VALUE FROM DIMENSIONATTRIBUTE")
    '    sbSql.AppendLine(" INNER JOIN DIMENSIONATTRIBUTEDIRCATEGORY ON DIMENSIONATTRIBUTE.RECID=DIMENSIONATTRIBUTEDIRCATEGORY.DIMENSIONATTRIBUTE")
    '    sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG Fac ON DIMENSIONATTRIBUTEDIRCATEGORY.DIRCATEGORY=Fac.FINANCIALTAGCATEGORY")
    '    sbSql.AppendLine(" INNER JOIN DIMENSIONFINANCIALTAG SSec ON Fac.ECL_COMCODE=SSec.ECL_COMCODE")
    '    sbSql.AppendLine(" WHERE NAME='D1_Factory' AND NOT(SSec.VALUE LIKE Fac.ECL_COMCODE + '%') ")
    '    sbSql.AppendLine(" AND SSec.VALUE <> Fac.VALUE")
    '    'sbSql.AppendLine("AND Fac.VALUE = @FACTORYVALUE")
    '    sbSql.AppendLine("AND Fac.ECL_SHORTNAME = @FACTORYVALUE")
    '    sbSql.AppendLine(" GROUP BY SSec.VALUE")
    '    sbSql.AppendLine(" ORDER BY SSec.VALUE")
    '    Dim param As SqlParameterCollection = New SqlCommand().Parameters
    '    param.AddWithValue("@FACTORYVALUE", strFactory)
    '    Dim cDBSQL As New Common.cDBSQL
    '    Return cDBSQL.GetData(sbSql.ToString, "tbSection", connPurchase, param).Tables("tbSection")
    'End Function
End Class
