Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.text
Imports System.data
Imports System.Configuration.ConfigurationManager

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class GetDynamicContent
    Inherits System.Web.Services.WebService


    '<System.Web.Services.WebMethodAttribute()> _
    '<System.Web.Script.Services.ScriptMethodAttribute()> _
    'Public Function GetDynamicContent(ByVal contextKey As String) As String
    '    If contextKey.IndexOf(",") >= 0 Then
    '        Try
    '            Dim DataBase As String = contextKey.Split(",")(1)
    '            Dim RQNNUMBER As String = contextKey.Split(",")(0)
    '            Dim RequisitionBLL As New RequisitionBLL(DataBase)
    '            Dim dtRequisition As DataTable = RequisitionBLL.getRequisitionList(RQNNUMBER)
    '            If dtRequisition.Rows.Count > 0 Then
    '                Dim sb As New StringBuilder
    '                With dtRequisition.Rows(0)
    '                    sb.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid;")
    '                    sb.Append("font-size:10px; font-family:Verdana;' cellspacing='0' cellpadding='0'>")

    '                    sb.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>")
    '                    sb.Append("Requisition Details : <b>" & RQNNUMBER & "</b> by ")
    '                    sb.Append(.Item("REQUESTBY"))
    '                    sb.Append("</td></tr>")

    '                    sb.Append("<tr><td style='text-align:center;width:100px;border-right: #336699 1px solid;border-top: #336699 1px solid;'><b>Item</b></td>")
    '                    sb.Append("<td style='text-align:center;border-right: #336699 1px solid;border-top: #336699 1px solid;'><b>Description</b></td>")
    '                    sb.Append("<td style='text-align:center;border-right: #336699 1px solid;border-top: #336699 1px solid;'><b>QTY</b></td></tr>")

    '                    For iRow As Integer = 0 To dtRequisition.Rows.Count - 1
    '                        sb.Append("<tr style=''>")
    '                        sb.Append("<td style='text-align:left;white-space:nowrap;")
    '                        sb.Append("border-right: #336699 1px solid;")
    '                        sb.Append("border-top: #336699 1px solid;'>")
    '                        sb.AppendLine(dtRequisition.Rows(iRow)("ITEMNO").ToString & "</td>")
    '                        sb.Append("<td style='text-align:left;")
    '                        sb.Append("border-right: #336699 1px solid;")
    '                        sb.Append("border-top: #336699 1px solid;'>")
    '                        sb.AppendLine(dtRequisition.Rows(iRow)("ITEMCOMMENT").ToString & "</td>")
    '                        sb.Append("<td style='text-align:right;")
    '                        sb.Append("border-right: #336699 1px solid;")
    '                        sb.Append("border-top: #336699 1px solid;'>")
    '                        sb.AppendLine(dtRequisition.Rows(iRow)("OQORDERED").ToString & " ")
    '                        sb.AppendLine(dtRequisition.Rows(iRow)("ORDERUNIT").ToString & "</td>")
    '                        sb.Append("</tr>")
    '                    Next
    '                    sb.Append("</table>")
    '                End With

    '                Return sb.ToString
    '            Else
    '                Return ""
    '            End If
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    Else
    '        Return ""
    '    End If

    'End Function

    <System.Web.Services.WebMethodAttribute()> _
    <System.Web.Script.Services.ScriptMethodAttribute()> _
    Public Function GetDynamicContentAX(ByVal contextKey As String) As String
        If contextKey.IndexOf(",") >= 0 Then
            Try
                Dim DataBase As String = contextKey.Split(",")(1)
                Dim RQNNUMBER As String = contextKey.Split(",")(0)
                Dim RequisitionBLL As New RequisitionBLL(DataBase)
                Dim dtRequisition As DataTable = RequisitionBLL.getRequisitionListAX(RQNNUMBER) 'ITEMID, NAME, PURCHQTY
                If dtRequisition.Rows.Count > 0 Then
                    Dim sb As New StringBuilder
                    With dtRequisition.Rows(0)
                        sb.Append("<table style='background-color:#f3f3f3; border: #336699 3px solid;")
                        sb.Append("font-size:10px; font-family:Verdana;' cellspacing='0' cellpadding='0'>")

                        sb.Append("<tr><td colspan='3' style='background-color:#336699; color:white;'>")
                        sb.Append("Requisition Details : <b>" & RQNNUMBER & "</b> by ")
                        sb.Append(.Item("PURCHREQNAME"))
                        sb.Append("</td></tr>")

                        sb.Append("<tr><td style='text-align:center;width:100px;border-right: #336699 1px solid;border-top: #336699 1px solid;'><b>Item</b></td>")
                        sb.Append("<td style='text-align:center;border-right: #336699 1px solid;border-top: #336699 1px solid;'><b>Description</b></td>")
                        sb.Append("<td style='text-align:center;border-right: #336699 1px solid;border-top: #336699 1px solid;'><b>QTY</b></td></tr>")

                        For iRow As Integer = 0 To dtRequisition.Rows.Count - 1
                            sb.Append("<tr style=''>")
                            sb.Append("<td style='text-align:left;white-space:nowrap;")
                            sb.Append("border-right: #336699 1px solid;")
                            sb.Append("border-top: #336699 1px solid;'>")
                            sb.AppendLine(dtRequisition.Rows(iRow)("ITEMID").ToString & "</td>")
                            sb.Append("<td style='text-align:left;white-space:nowrap;")
                            sb.Append("border-right: #336699 1px solid;")
                            sb.Append("border-top: #336699 1px solid;'>")
                            sb.AppendLine(dtRequisition.Rows(iRow)("NAME").ToString & "</td>")
                            sb.Append("<td style='text-align:right;")
                            sb.Append("border-right: #336699 1px solid;")
                            sb.Append("border-top: #336699 1px solid;'>")
                            sb.AppendLine(Format(dtRequisition.Rows(iRow)("PURCHQTY"), "#,##0") & " </td>")
                            'sb.AppendLine(dtRequisition.Rows(iRow)("ORDERUNIT").ToString & "")
                            sb.Append("</tr>")
                        Next
                        sb.Append("</table>")
                    End With

                    Return sb.ToString
                Else
                    Return ""
                End If
            Catch ex As Exception
                Return ""
            End Try
        Else
            Return ""
        End If

    End Function
End Class
