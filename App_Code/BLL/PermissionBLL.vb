Imports Microsoft.VisualBasic
Imports System.Data

Public Class PermissionBLL
    Private connDB As String = Nothing
    Private _applicantAdapter As PermissionDAL

    Protected ReadOnly Property Adapter() As PermissionDAL
        Get
            If _applicantAdapter Is Nothing Then
                If connDB = "" Then
                    _applicantAdapter = New PermissionDAL()
                Else
                    _applicantAdapter = New PermissionDAL(connDB)
                End If
            End If

            Return _applicantAdapter
        End Get
    End Property

    Public Function getAllPermission(ByVal strFactory As String) As DataTable
        Return Adapter.getPermission(strFactory)
    End Function
End Class
