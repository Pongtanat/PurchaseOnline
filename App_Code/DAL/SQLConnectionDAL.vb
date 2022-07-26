Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class SQLConnectionDAL

    Protected strConnect As String

    Protected Function GetSqlConnection(ByVal connDB As String) As SqlConnection
        If ConfigurationManager.ConnectionStrings(connDB) Is Nothing OrElse _
            ConfigurationManager.ConnectionStrings(connDB).ConnectionString.Trim() = "" Then

            Throw New Exception("A connection string named '" & connDB & "' with a valid connection string must exist in the <connectionStrings> configuration section for the application.")
        End If

        Dim oConnection As New SqlConnection
        oConnection.ConnectionString = ConfigurationManager.ConnectionStrings(connDB).ConnectionString
        Return oConnection
    End Function

End Class
