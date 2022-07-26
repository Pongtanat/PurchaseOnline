Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class AutoComplete
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetItemByCode(ByVal prefixText As String, ByVal count As Integer, _
                                    ByVal contextKey As String) As String()
        Dim RequisitionBLL As New RequisitionBLL(contextKey.Split(",")(0))
        Dim dtItem As DataTable = RequisitionBLL.getItemByItemCodeAX(prefixText, contextKey.Split(",")(1), contextKey.Split(",")(2), count)
        Dim s1 As String
        If (count = 0) Then
            count = 10
        End If

        Dim items As New List(Of String)
        For i As Integer = 1 To dtItem.Rows.Count
            s1 = dtItem.Rows(i - 1)("ItemId") + ControlChars.Tab + dtItem.Rows(i - 1)("Name")
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(s1, dtItem.Rows(i - 1)("Name")))
        Next i

        Return items.ToArray()
    End Function

    <WebMethod()> _
    Public Function GetItemByDesc(ByVal prefixText As String, ByVal count As Integer, _
                                    ByVal contextKey As String) As String()
        Dim RequisitionBLL As New RequisitionBLL(contextKey.Split(",")(0))
        Dim dtItem As DataTable = RequisitionBLL.getItemByItemDescAX(prefixText, contextKey.Split(",")(1), contextKey.Split(",")(2), count)
        Dim s1 As String
        If (count = 0) Then
            count = 10
        End If

        Dim items As New List(Of String)
        For i As Integer = 1 To dtItem.Rows.Count
            s1 = dtItem.Rows(i - 1)("Name") '+ " " + dtEmp.Rows(i - 1)("PersonLNameEng")
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(s1, dtItem.Rows(i - 1)("ItemId")))
        Next i

        Return items.ToArray()
    End Function

    <WebMethod()> _
    Public Function GetVendorByCode(ByVal prefixText As String, ByVal count As Integer, _
                                    ByVal contextKey As String) As String()
        Dim RequisitionBLL As New RequisitionBLL(contextKey.Split(",")(0))
        Dim dtItem As DataTable = RequisitionBLL.getVendorByVendorCodeAX(prefixText, count)
        Dim s1 As String
        If (count = 0) Then
            count = 10
        End If

        Dim items As New List(Of String)
        For i As Integer = 1 To dtItem.Rows.Count
            s1 = dtItem.Rows(i - 1)("VENDORID") + ControlChars.Tab + dtItem.Rows(i - 1)("VENDNAME")
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(s1, dtItem.Rows(i - 1)("VENDNAME")))
        Next i

        Return items.ToArray()
    End Function

    <WebMethod()> _
    Public Function GetVendorByName(ByVal prefixText As String, ByVal count As Integer, _
                                    ByVal contextKey As String) As String()
        Dim RequisitionBLL As New RequisitionBLL(contextKey.Split(",")(0))
        Dim dtItem As DataTable = RequisitionBLL.getVendorByVendorNameAX(prefixText, count)
        Dim s1 As String
        If (count = 0) Then
            count = 10
        End If

        Dim items As New List(Of String)
        For i As Integer = 1 To dtItem.Rows.Count
            s1 = dtItem.Rows(i - 1)("VENDNAME") '+ " " + dtEmp.Rows(i - 1)("PersonLNameEng")
            items.Add(AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(s1, dtItem.Rows(i - 1)("VENDORID")))
        Next i

        Return items.ToArray()
    End Function
End Class
