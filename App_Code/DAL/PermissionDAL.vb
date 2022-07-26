Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class PermissionDAL
    Inherits SQLConnectionDAL
    Dim connPurchase As SqlConnection '= GetSqlConnection(connDB)
    Private Trans As SqlTransaction

    Sub New()
        connPurchase = GetSqlConnection("PR_Online")
    End Sub

    Sub New(ByVal connDB As String)
        connPurchase = GetSqlConnection(connDB)
    End Sub

    Public Function getPermission(ByVal strFactory As String) As DataTable
        Dim sbSql As New StringBuilder
        Dim param As SqlParameterCollection = New SqlCommand().Parameters
        sbSql.AppendLine("SELECT * FROM tbUsers")
        Dim cDBSQL As New Common.cDBSQL
        If strFactory <> "" Then
            sbSql.AppendLine(" WHERE ACCPAC=@ACCPAC")
            param.AddWithValue("@ACCPAC", strFactory)
            Return cDBSQL.GetData(sbSql.ToString, "tbUsers", connPurchase, param).Tables("tbUsers")
        Else
            Return cDBSQL.GetData(sbSql.ToString, "tbUsers", connPurchase).Tables("tbUsers")
        End If
    End Function
End Class
