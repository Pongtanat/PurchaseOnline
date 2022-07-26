Imports Microsoft.VisualBasic
Imports System.Data

Public Class MasterPageBLL
    Private connDB As String = Nothing
    Private _applicantAdapter As MasterPageDAL

    Sub New(ByVal DB As String)
        connDB = DB
    End Sub

    Protected ReadOnly Property Adapter() As MasterPageDAL
        Get
            If _applicantAdapter Is Nothing Then
                'If connDB = "" Then
                '    _applicantAdapter = New MasterPageDAL()
                'Else
                _applicantAdapter = New MasterPageDAL(connDB)
                'End If
            End If

            Return _applicantAdapter
        End Get
    End Property

    Public Function getAllSubSectionByFactory(ByVal strFactory As String) As DataTable
        Return Adapter.getAllSectionByFactory(strFactory)
    End Function
End Class
