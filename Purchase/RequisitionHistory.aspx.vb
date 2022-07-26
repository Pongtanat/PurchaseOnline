'Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.Shared

Partial Class Purchase_RequisitionHistory
    Inherits System.Web.UI.Page

    Dim reportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Dim RequisitionBLL As New RequisitionHistoryBLL("HPTPOL")

            'ddlSection.datavaluefield = "COMMENT"
            'ddlSection.datatextfield = "COMMENT"
            'ddlSection.datasource = RequisitionBLL.getSection
            'ddlSection.databind()

            'Dim lblUserName As Label = CType(Master.FindControl("lblUserName"), Label)
            ddlHistoryBy.Items.Add(New ListItem("All Requisition", ""))
            ddlHistoryBy.Items.Add(New ListItem("My Section", Session("SECTION")))
            ddlHistoryBy.Items.Add(New ListItem("My Requisition", Session("FULLNAME")))
            ddlHistoryBy.SelectedIndex = 2
            'Bind_gvHist()
            Bind_gvHistAX()
        Else
            CrystalReportViewer1.Visible = False
            lblMessage.Text = ""
            If gvHistAX.Rows.Count > 0 AndAlso gvHistAX.SelectedIndex <> -1 AndAlso Session("RQNNUMBER") <> "" Then
                draw_CrystalReportViewer(Session("RQNNUMBER"), _
                                        Session("DOCNO"), _
                                        Session("REVISION"), _
                                        Session("EFFECTIVE")) 'CType(gvHist.Rows(gvHist.SelectedIndex).Cells(0).FindControl("hidRQNNUMBER"), HiddenField).Value
            End If
        End If
    End Sub

    'Protected Sub Bind_gvHist()
    '    If Session("POSTNEW") <> "" Then
    '        gvHist.SelectedIndex = 0
    '        Session("POSTNEW") = ""
    '    End If
    '    Dim ReportOBJ As New ReportOBJ
    '    Select Case ddlHistoryBy.SelectedItem.Text
    '        Case "All Requisition"
    '            ReportOBJ.ReportSection = txbSection.Text 'ddlSection.selecteditem.value
    '        Case "My Section"
    '            ReportOBJ.ReportSection = Session("SECTION") 'ddlHistoryBy.SelectedItem.Value

    '        Case "My Requisition"
    '            ReportOBJ.ReportRequestBy = Session("FULLNAME") 'ddlHistoryBy.SelectedItem.Value
    '            ReportOBJ.ReportSection = Session("SECTION")

    '    End Select
    '    ReportOBJ.ReportVendorName = txbVendor.Text.Trim
    '    If txbDate1.Text <> "" Then ReportOBJ.ReportDateFrom = txbDate1.Text
    '    If txbDate2.Text <> "" Then ReportOBJ.ReportDateTo = txbDate2.Text
    '    ReportOBJ.ReportRequisitionFrom = txbRQ1.Text
    '    ReportOBJ.ReportRequisitionTo = txbRQ2.Text

    '    Dim MyRequisitionBLL As New RequisitionHistoryBLL(Session("DATABASE"))
    '    gvHist.DataSource = MyRequisitionBLL.getRequestHistory(ReportOBJ)
    '    gvHist.DataBind()

    '    If gvHist.Rows.Count = 0 Then
    '        CrystalReportViewer1.Visible = False
    '    Else
    '        CrystalReportViewer1.Visible = True
    '    End If
    '    If gvHist.SelectedIndex <> -1 Then
    '        If gvHist.EditIndex = -1 AndAlso gvHist.Rows(gvHist.SelectedIndex).Cells(6).Text = "" Then
    '            If Session("AllowChangeRequisition") = True Then
    '                gvHist.Rows(gvHist.SelectedIndex).Cells(11).FindControl("imgEdit").Visible = True
    '            End If
    '        End If
    '        draw_CrystalReportViewer(Session("RQNNUMBER"), _
    '                                Session("DOCNO"), _
    '                                Session("REVISION"), _
    '                                Session("EFFECTIVE"))
    '    End If

    'End Sub

    Protected Sub Bind_gvHistAX()
        If Session("POSTNEW") <> "" Then
            gvHistAX.SelectedIndex = 0
            Session("POSTNEW") = ""
        End If
        Dim ReportOBJ As New ReportOBJ
        ReportOBJ.ReportSite = Session("ACCPAC")
        Select Case ddlHistoryBy.SelectedItem.Text
            Case "All Requisition"
                ReportOBJ.ReportSection = txbSection.Text 'ddlSection.selecteditem.value
            Case "My Section"
                ReportOBJ.ReportSection = Session("SECTION") 'ddlHistoryBy.SelectedItem.Value

            Case "My Requisition"
                ReportOBJ.ReportRequestBy = Session("FULLNAME") 'ddlHistoryBy.SelectedItem.Value
                ReportOBJ.ReportSection = Session("SECTION")

        End Select
        ReportOBJ.ReportVendorName = txbVendor.Text.Trim
        If txbDate1.Text <> "" Then ReportOBJ.ReportDateFrom = txbDate1.Text
        If txbDate2.Text <> "" Then ReportOBJ.ReportDateTo = txbDate2.Text
        ReportOBJ.ReportRequisitionFrom = txbRQ1.Text
        ReportOBJ.ReportRequisitionTo = txbRQ2.Text

        Dim MyRequisitionBLL As New RequisitionHistoryBLL(Session("DATABASE"))
        gvHistAX.DataSource = MyRequisitionBLL.getRequestHistoryAX(ReportOBJ)
        gvHistAX.DataBind()

        If gvHistAX.Rows.Count = 0 Then
            CrystalReportViewer1.Visible = False
        Else
            CrystalReportViewer1.Visible = True
        End If
        If gvHistAX.SelectedIndex <> -1 Then
            '--------------------------------------------------------------------------------------------------
            'Enable Edit RQ
            'If gvHistAX.EditIndex = -1 AndAlso gvHistAX.Rows(gvHistAX.SelectedIndex).Cells(6).Text = "" Then
            '    If Session("AllowChangeRequisition") = True Then
            '        gvHistAX.Rows(gvHistAX.SelectedIndex).Cells(10).FindControl("imgEdit").Visible = True
            '    End If
            'End If
            '--------------------------------------------------------------------------------------------------
            draw_CrystalReportViewer(Session("RQNNUMBER"), _
                                    Session("DOCNO"), _
                                    Session("REVISION"), _
                                    Session("EFFECTIVE"))
        End If

    End Sub

    'Protected Sub gvHist_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHist.RowCreated
    '    If (e.Row.RowType = DataControlRowType.DataRow And _
    '        (e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate)) Then

    '        Dim pce As AjaxControlToolkit.PopupControlExtender
    '        pce = CType(e.Row.Cells(0).FindControl("PopupControlExtender1"), AjaxControlToolkit.PopupControlExtender)
    '        Dim behaviorID As String = "pce_" & e.Row.RowIndex
    '        pce.BehaviorID = behaviorID

    '        Dim OnMouseOverScript As String = String.Format("$find('{0}').showPopup();", behaviorID)
    '        Dim OnMouseOutScript As String = String.Format("$find('{0}').hidePopup();", behaviorID)

    '        e.Row.Cells(3).Attributes.Add("onmouseover", OnMouseOverScript)
    '        e.Row.Cells(3).Attributes.Add("onmouseout", OnMouseOutScript)

    '    End If
    'End Sub

    Protected Sub gvHistAX_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHistAX.RowCreated
        If (e.Row.RowType = DataControlRowType.DataRow And _
            (e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate)) Then

            Dim pce As AjaxControlToolkit.PopupControlExtender
            pce = CType(e.Row.Cells(0).FindControl("PopupControlExtender1"), AjaxControlToolkit.PopupControlExtender)
            Dim behaviorID As String = "pce_" & e.Row.RowIndex
            pce.BehaviorID = behaviorID

            Dim OnMouseOverScript As String = String.Format("$find('{0}').showPopup();", behaviorID)
            Dim OnMouseOutScript As String = String.Format("$find('{0}').hidePopup();", behaviorID)

            e.Row.Cells(3).Attributes.Add("onmouseover", OnMouseOverScript)
            e.Row.Cells(3).Attributes.Add("onmouseout", OnMouseOutScript)

        End If
    End Sub

    'Protected Sub gvHist_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHist.RowDataBound
    '    If e.Row.RowType = DataControlRowType.DataRow Then
    '        '####################################
    '        'Post Date
    '        '====================================
    '        e.Row.Cells(5).Text = e.Row.Cells(5).Text.Substring(0, 10)
    '        '####################################
    '        'Date Complete
    '        '====================================
    '        If e.Row.Cells(6).Text = 0 Then
    '            e.Row.Cells(6).Text = ""
    '        Else
    '            e.Row.Cells(6).Text = e.Row.Cells(6).Text.Substring(6, 2) & "/" & e.Row.Cells(6).Text.Substring(4, 2) & "/" & e.Row.Cells(6).Text.Substring(0, 4)
    '        End If
    '        Dim PopupControlExtender1 As New AjaxControlToolkit.PopupControlExtender
    '        PopupControlExtender1 = CType(e.Row.Cells(0).FindControl("PopupControlExtender1"), AjaxControlToolkit.PopupControlExtender)
    '        PopupControlExtender1.DynamicContextKey += "," & Session("DATABASE")
    '        '####################################
    '        'Apv Date
    '        '====================================
    '        If e.Row.RowIndex = gvHist.EditIndex Then
    '            Dim lblPRRCPDate As Label = CType(e.Row.Cells(10).FindControl("lblPRRCPDate"), Label)
    '            If lblPRRCPDate.Text.Length <> 8 Then
    '                lblPRRCPDate.Text = ""
    '            Else
    '                lblPRRCPDate.Text = lblPRRCPDate.Text.Substring(6, 2) & "/" & lblPRRCPDate.Text.Substring(4, 2) & "/" & lblPRRCPDate.Text.Substring(0, 4)
    '            End If

    '            Dim txbAPVDate As TextBox = CType(e.Row.Cells(9).FindControl("txbAPVDate"), TextBox)
    '            If txbAPVDate.Text = "" Then
    '                txbAPVDate.Text = ""
    '            Else
    '                txbAPVDate.Text = txbAPVDate.Text.Substring(4, 2) & "/" & txbAPVDate.Text.Substring(2, 2) & "/" & Year(Now).ToString.Substring(0, 2) & txbAPVDate.Text.Substring(0, 2)
    '            End If

    '            Dim txbPRRCPDate As TextBox = CType(e.Row.Cells(10).FindControl("txbPRRCPDate"), TextBox)
    '            If txbPRRCPDate.Text.Length <> 8 Then
    '                txbPRRCPDate.Text = ""
    '            Else
    '                txbPRRCPDate.Text = txbPRRCPDate.Text.Substring(6, 2) & "/" & txbPRRCPDate.Text.Substring(4, 2) & "/" & txbPRRCPDate.Text.Substring(0, 4)
    '            End If
    '        Else
    '            Dim imgEdit As ImageButton = CType(e.Row.Cells(11).FindControl("imgEdit"), ImageButton)
    '            imgEdit.Visible = True
    '            Dim imgDelete As ImageButton = CType(e.Row.Cells(11).FindControl("imgDelete"), ImageButton)
    '            imgDelete.OnClientClick = "return confirm('Are you sure you want to delete the requisition """ & e.Row.Cells(3).Text.Replace("'", "\'") & """?');"

    '            'If ddlHistoryBy.SelectedIndex = 0 Then
    '            If ddlHistoryBy.SelectedItem.Text <> "My Requisition" Then
    '                imgDelete.Visible = False
    '                imgEdit.Visible = False
    '            End If

    '            If e.Row.Cells(6).Text <> "" Then
    '                imgDelete.Visible = False
    '                imgEdit.Visible = False
    '            End If

    '            Dim lblAPVDate As Label = CType(e.Row.Cells(9).FindControl("lblAPVDate"), Label)
    '            If lblAPVDate.Text = "" Then
    '                lblAPVDate.Text = ""
    '            Else
    '                lblAPVDate.Text = lblAPVDate.Text.Substring(4, 2) & "/" & lblAPVDate.Text.Substring(2, 2) & "/" & Year(Now).ToString.Substring(0, 2) & lblAPVDate.Text.Substring(0, 2)
    '                imgDelete.Visible = False
    '            End If

    '            Dim lblPRRCPDate As Label = CType(e.Row.Cells(10).FindControl("lblPRRCPDate"), Label)
    '            If lblPRRCPDate.Text.Length <> 8 Then
    '                lblPRRCPDate.Text = ""
    '            Else
    '                lblPRRCPDate.Text = lblPRRCPDate.Text.Substring(6, 2) & "/" & lblPRRCPDate.Text.Substring(4, 2) & "/" & lblPRRCPDate.Text.Substring(0, 4)
    '                imgDelete.Visible = False
    '            End If

    '        End If
    '        '####################################
    '        'PR Received Date
    '        '====================================
    '        'Dim lblPRRCPDate As Label = CType(e.Row.Cells(8).FindControl("lblPRRCPDate"), Label)
    '        'If lblPRRCPDate.Text.Length <> 8 Then
    '        '    lblPRRCPDate.Text = "-"
    '        'Else
    '        '    lblPRRCPDate.Text = lblPRRCPDate.Text.Substring(6, 2) & "/" & lblPRRCPDate.Text.Substring(4, 2) & "/" & lblPRRCPDate.Text.Substring(0, 4)
    '        'End If

    '        '####################################
    '        'Entire row
    '        '====================================
    '        'e.Row.Attributes.Add("onclick", "document.getElementById('" & CType(e.Row.Cells(0).FindControl("imgReport"), ImageButton).ClientID & "').click();")
    '        '####################################
    '    End If
    'End Sub

    Protected Sub gvHistAX_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHistAX.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '====================================
            'Created Date
            '------------------------------------
            e.Row.Cells(5).ToolTip = e.Row.Cells(5).Text
            e.Row.Cells(5).Text = e.Row.Cells(5).Text.Substring(0, 10)
            '====================================
            'Approved Date
            '------------------------------------
            'If e.Row.Cells(6).Text <> "" Then
            '    e.Row.Cells(6).ToolTip = e.Row.Cells(6).Text
            '    e.Row.Cells(6).Text = e.Row.Cells(6).Text.Substring(0, 10)
            'End If
            '====================================
            Dim PopupControlExtender1 As New AjaxControlToolkit.PopupControlExtender
            PopupControlExtender1 = CType(e.Row.Cells(0).FindControl("PopupControlExtender1"), AjaxControlToolkit.PopupControlExtender)
            PopupControlExtender1.DynamicContextKey += "," & Session("DATABASE")
            '####################################
            'Apv Date
            '====================================
            Dim imgSubmit As ImageButton = CType(e.Row.Cells(8).FindControl("imgSubmit"), ImageButton)
            imgSubmit.Visible = False

            If e.Row.RowIndex = gvHistAX.EditIndex Then

                Dim txbAPVDate As TextBox = CType(e.Row.Cells(6).FindControl("txbAPVDate"), TextBox)
                If txbAPVDate.Text = "01/01/1900 00:00:00" Then
                    txbAPVDate.Text = ""
                Else
                    'txbAPVDate.Text = txbAPVDate.Text.Substring(4, 2) & "/" & txbAPVDate.Text.Substring(2, 2) & "/" & Year(Now).ToString.Substring(0, 2) & txbAPVDate.Text.Substring(0, 2)
                End If

                Dim lblPRRCPDate As Label = CType(e.Row.Cells(7).FindControl("lblPRRCPDate"), Label)
                Dim txbPRRCPDate As TextBox = CType(e.Row.Cells(7).FindControl("txbPRRCPDate"), TextBox)
                Dim MaskVDate2 As AjaxControlToolkit.MaskedEditValidator = CType(e.Row.Cells(7).FindControl("MaskVDate2"), AjaxControlToolkit.MaskedEditValidator)
                If Session("AllowSubmitPRReceive") = True Then
                    txbPRRCPDate.Visible = True
                    lblPRRCPDate.Visible = False
                    If txbPRRCPDate.Text = "01/01/1900 00:00:00" Then
                        txbPRRCPDate.Text = ""
                    Else
                        'txbPRRCPDate.Text = txbPRRCPDate.Text.Substring(6, 2) & "/" & txbPRRCPDate.Text.Substring(4, 2) & "/" & txbPRRCPDate.Text.Substring(0, 4)
                    End If
                Else
                    txbPRRCPDate.Visible = False
                    MaskVDate2.Enabled = False
                    lblPRRCPDate.Visible = True
                    If lblPRRCPDate.Text = "01/01/1900 00:00:00" Then
                        lblPRRCPDate.Text = ""
                    Else
                        lblPRRCPDate.Text = lblPRRCPDate.Text.Substring(0, 10)
                    End If
                End If

                Dim lblSubmittedDate As Label = CType(e.Row.Cells(8).FindControl("lblSubmittedDatetime"), Label)
                'If e.Row.Cells(8).Text = "01/01/1900 07:00:00" Then
                '    e.Row.Cells(8).Text = ""
                'End If
                If lblSubmittedDate.Text = "01/01/1900 07:00:00" Then
                    lblSubmittedDate.Text = ""
                End If
            Else
                'Dim imgEdit As ImageButton = CType(e.Row.Cells(10).FindControl("imgEdit"), ImageButton)
                'imgEdit.Visible = True
                Dim imgDelete As ImageButton = CType(e.Row.Cells(10).FindControl("imgDelete"), ImageButton)
                imgDelete.OnClientClick = "return confirm('Are you sure you want to delete the requisition """ & e.Row.Cells(3).Text.Replace("'", "\'") & """?');"

                'If ddlHistoryBy.SelectedIndex = 0 Then
                If ddlHistoryBy.SelectedItem.Text <> "My Requisition" Then
                    imgDelete.Visible = False
                    'imgEdit.Visible = False
                End If

                If e.Row.Cells(9).Text <> "Draft" Then
                    imgDelete.Visible = False
                    'imgEdit.Visible = False
                End If

                Dim lblAPVDate As Label = CType(e.Row.Cells(6).FindControl("lblAPVDate"), Label)
                If lblAPVDate.Text <> "01/01/1900 00:00:00" Then
                    lblAPVDate.Text = lblAPVDate.Text.Substring(0, 10)
                    imgDelete.Visible = False
                Else
                    lblAPVDate.Text = ""
                End If

                Dim lblPRRCPDate As Label = CType(e.Row.Cells(7).FindControl("lblPRRCPDate"), Label)
                If lblPRRCPDate.Text <> "01/01/1900 00:00:00" Then
                    lblPRRCPDate.Text = lblPRRCPDate.Text.Substring(0, 10)
                    imgDelete.Visible = False
                Else
                    lblPRRCPDate.Text = ""
                End If

                Dim lblSubmittedDate As Label = CType(e.Row.Cells(8).FindControl("lblSubmittedDatetime"), Label)
                'If e.Row.Cells(8).Text <> "01/01/1900 07:00:00" Then
                '    e.Row.Cells(8).ToolTip = e.Row.Cells(8).Text
                '    e.Row.Cells(8).Text = e.Row.Cells(8).Text.Substring(0, 10)
                'Else
                '    e.Row.Cells(8).Text = ""
                'End If
                If lblSubmittedDate.Text <> "01/01/1900 07:00:00" Then
                    lblSubmittedDate.ToolTip = lblSubmittedDate.Text
                    lblSubmittedDate.Text = lblSubmittedDate.Text.Substring(0, 10)
                    'imgSubmit.Visible = False
                Else
                    lblSubmittedDate.Text = ""
                    'imgSubmit.OnClientClick = "return confirm('Submit PR No. """ & e.Row.Cells(3).Text.Replace("'", "\'") & """?');"

                End If

            End If
        End If
    End Sub

    'Protected Sub gvHist_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvHist.PageIndexChanging
    '    Session("RQNNUMBER") = ""
    '    gvHist.PageIndex = e.NewPageIndex
    '    gvHist.SelectedIndex = -1
    '    gvHist.EditIndex = -1
    '    Bind_gvHist()
    'End Sub

    Protected Sub gvHistAX_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvHistAX.PageIndexChanging
        Session("RQNNUMBER") = ""
        gvHistAX.PageIndex = e.NewPageIndex
        gvHistAX.SelectedIndex = -1
        gvHistAX.EditIndex = -1
        Bind_gvHistAX()
    End Sub

    Protected Sub gvHistAX_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvHistAX.RowCommand
        Dim reportFileName As String = ""
        Select Case e.CommandName
            Case "Select"
                Session("RQNNUMBER") = e.CommandArgument.ToString.Split(",")(0)
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                Dim DocNoObj As New RequisitionHistoryBLL.DocNoObj
                DocNoObj = RequisitionHistoryBLL.setDocRevision(Session("SITE"), Session("ACCPAC"), "PORQN01.RPT", e.CommandArgument.ToString.Split(",")(1))
                Session("DOCNO") = DocNoObj.DOCNO
                Session("REVISION") = DocNoObj.REVISION
                Session("EFFECTIVE") = Format(DocNoObj.EFFECTIVE, "dd/MM/yyyy")
            Case "Preview"
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                Dim DocNoObj As New RequisitionHistoryBLL.DocNoObj
                DocNoObj = RequisitionHistoryBLL.setDocRevision(Session("SITE"), Session("ACCPAC"), "PORQN01.RPT", e.CommandArgument.ToString.Split(",")(1))
                Session("DOCNO") = DocNoObj.DOCNO
                Session("REVISION") = DocNoObj.REVISION
                Session("EFFECTIVE") = Format(DocNoObj.EFFECTIVE, "dd/MM/yyyy")
                Dim sendVariable As New StringBuilder
                sendVariable.Append("RQN=" & e.CommandArgument.ToString.Split(",")(0))
                sendVariable.Append("&doc=" & Session("DOCNO"))
                sendVariable.Append("&rev=" & Session("REVISION"))
                sendVariable.Append("&eff=" & Session("EFFECTIVE"))
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "preview", "window.open('RequisitionDetail.aspx?" & sendVariable.ToString & "','_blank','location=0,menubar=1,toolbar=1,resizable=1,scrollbars=1,width=920');", True)
                'Dim reportfile As String = ""
                'reportfile = Server.MapPath("../Report/PORQN01.RPT")
                'If Session("DATABASE") = "HOPTMO" Then
                '    reportfile = Server.MapPath("../Report/GMOPR02.RPT")
                'End If
                'Session("report") = reportfile
                'Dim ReportBLL As New ReportBLL(Session("DATABASE"))
                'reportDocument.Close()
                'reportDocument = ReportBLL.Load(reportfile, e.CommandArgument, Session("DATABASE"), Session("DOCNO"), Session("REVISION"), Session("EFFECTIVE"))
                'reportDocument.PrintToPrinter(1, True, 0, 0)
            Case "Print1"
                Dim strCondition As String = ""
                If Session("ACCPAC") = "HOPTMO" Then
                    reportFileName = "GMOPR02.RPT"
                Else
                    reportFileName = "PORQN01.RPT"
                    strCondition = e.CommandArgument.ToString.Split(",")(1)
                End If
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                Dim DocNoObj As New RequisitionHistoryBLL.DocNoObj
                DocNoObj = RequisitionHistoryBLL.setDocRevision(Session("SITE"), Session("ACCPAC"), reportFileName, strCondition)
                Session("DOCNO") = DocNoObj.DOCNO
                Session("REVISION") = DocNoObj.REVISION
                Session("EFFECTIVE") = Format(DocNoObj.EFFECTIVE, "dd/MM/yyyy")
                Dim sendVariable As New StringBuilder
                sendVariable.Append("RQN=" & e.CommandArgument.ToString.Split(",")(0))
                sendVariable.Append("&rpt=" & reportFileName)
                sendVariable.Append("&doc=" & Session("DOCNO"))
                sendVariable.Append("&rev=" & Session("REVISION"))
                sendVariable.Append("&eff=" & Session("EFFECTIVE"))
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "preview", "window.open('RequisitionDetail.aspx?" & sendVariable.ToString & "','_blank','location=0,menubar=1,toolbar=1,resizable=1,scrollbars=1,width=920');", True)
            Case "Print2"
                Dim strCondition As String = ""
                If Session("ACCPAC") = "HPTMOL" Then
                    reportFileName = "POMC01.RPT" 'for making production equipment by MC
                Else
                    reportFileName = "PORQN01.RPT"
                    strCondition = e.CommandArgument.ToString.Split(",")(1)
                End If
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                Dim DocNoObj As New RequisitionHistoryBLL.DocNoObj
                DocNoObj = RequisitionHistoryBLL.setDocRevision(Session("SITE"), Session("ACCPAC"), reportFileName, strCondition)
                Session("DOCNO") = DocNoObj.DOCNO
                Session("REVISION") = DocNoObj.REVISION
                Session("EFFECTIVE") = Format(DocNoObj.EFFECTIVE, "dd/MM/yyyy")
                Dim sendVariable As New StringBuilder
                sendVariable.Append("RQN=" & e.CommandArgument.ToString.Split(",")(0))
                sendVariable.Append("&rpt=" & reportFileName)
                sendVariable.Append("&doc=" & Session("DOCNO"))
                sendVariable.Append("&rev=" & Session("REVISION"))
                sendVariable.Append("&eff=" & Session("EFFECTIVE"))
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "preview", "window.open('RequisitionDetail.aspx?" & sendVariable.ToString & "','_blank','location=0,menubar=1,toolbar=1,resizable=1,scrollbars=1,width=920');", True)
            Case "Print3"
                Dim strCondition As String = ""
                If Session("ACCPAC") = "HPTMOL" Then
                    reportFileName = "GMOPR01.RPT"
                Else
                    reportFileName = "PORQN01.RPT"
                    strCondition = e.CommandArgument.ToString.Split(",")(1)
                End If
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                Dim DocNoObj As New RequisitionHistoryBLL.DocNoObj
                DocNoObj = RequisitionHistoryBLL.setDocRevision(Session("SITE"), Session("ACCPAC"), reportFileName, strCondition)
                Session("DOCNO") = DocNoObj.DOCNO
                Session("REVISION") = DocNoObj.REVISION
                Session("EFFECTIVE") = Format(DocNoObj.EFFECTIVE, "dd/MM/yyyy")
                Dim sendVariable As New StringBuilder
                sendVariable.Append("RQN=" & e.CommandArgument.ToString.Split(",")(0))
                sendVariable.Append("&rpt=" & reportFileName)
                sendVariable.Append("&doc=" & Session("DOCNO"))
                sendVariable.Append("&rev=" & Session("REVISION"))
                sendVariable.Append("&eff=" & Session("EFFECTIVE"))
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "preview", "window.open('RequisitionDetail.aspx?" & sendVariable.ToString & "','_blank','location=0,menubar=1,toolbar=1,resizable=1,scrollbars=1,width=920');", True)
            Case "Update"
                '============================
                Dim imgApply As ImageButton = CType(e.CommandSource, ImageButton)
                Dim gvRow As GridViewRow = imgApply.NamingContainer
                Dim rowID As Integer = gvRow.RowIndex
                Dim txbApvDate As TextBox = CType(gvHistAX.Rows(rowID).Cells(6).FindControl("txbAPVDate"), TextBox)
                Dim strApvDate As Date = New Date(txbApvDate.Text.Split("/")(2), txbApvDate.Text.Split("/")(1), txbApvDate.Text.Split("/")(0))
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                RequisitionHistoryBLL.UpdateApproveDateAX(strApvDate, e.CommandArgument) 'Update Approve Date
                '============================
                If Session("AllowSubmitPRReceive") = True Then
                    Dim txbPRRCPDate As TextBox = CType(gvHistAX.Rows(rowID).Cells(7).FindControl("txbPRRCPDate"), TextBox)
                    If txbPRRCPDate.Text <> "" Then
                        Dim strPRRCPDate As Date = New Date(txbPRRCPDate.Text.Split("/")(2), txbPRRCPDate.Text.Split("/")(1), txbPRRCPDate.Text.Split("/")(0))
                        RequisitionHistoryBLL.UpdatePRReceivedDateAX(strPRRCPDate, e.CommandArgument) 'Update PR Received Date
                    End If
                End If
                '============================
                gvHistAX.EditIndex = -1
                Bind_gvHistAX()
            Case "Submit"
                'Dim a As New RequisitionBLL(Session("DATABASE"))
                'a.SubmitPR(e.CommandArgument)
                ''gvHistAX.EditIndex = -1
                'Bind_gvHistAX()
            Case "Delete"
                Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
                lblMessage.Text = RequisitionHistoryBLL.DeletePRAX(e.CommandArgument)
            Case "EditRQ", gvHistAX.EditIndex <> -1
                Session("RQNNUMBER") = e.CommandArgument
                'Session("Action") = e.CommandName
                'Response.Redirect("Requisition.aspx")
                Dim imgApply As ImageButton = CType(e.CommandSource, ImageButton)
                Dim gvRow As GridViewRow = imgApply.NamingContainer
                Dim rowID As Integer = gvRow.RowIndex
                Session("IssueDate") = Common.cConvertString.ToDate(gvHistAX.Rows(rowID).Cells(5).Text)
                HistoryAction(e)
            Case "Copy"
                HistoryAction(e)
        End Select
    End Sub

    'Protected Sub gvHist_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvHist.RowEditing
    '    'If ddlHistoryBy.SelectedIndex = 0 Then 'All Requisition not allow user to go to edit mode
    '    '    lblMessage.Text = "You can edit only your own requisition."
    '    '    e.Cancel = True
    '    'Else
    '    '    gvHist.EditIndex = e.NewEditIndex
    '    '    Bind_gvHist()

    '    '    Dim lblPRRCPDate As Label = CType(gvHist.Rows(e.NewEditIndex).Cells(8).FindControl("lblPRRCPDate"), Label)
    '    '    Dim txbPRRCPDate As TextBox = CType(gvHist.Rows(e.NewEditIndex).Cells(8).FindControl("txbPRRCPDate"), TextBox)
    '    '    If "Trade PR Purchase".IndexOf(Session("SECTION")) > 0 Then
    '    '        txbPRRCPDate.Visible = True
    '    '    Else
    '    '        lblPRRCPDate.Visible = True
    '    '        txbPRRCPDate.Visible = False
    '    '    End If
    '    'End If
    '    '#################

    '    'Dim test As String = Session("SECTION")
    '    'Session("SECTION") = "Trade"
    '    'If "Trade PR Purchase Sale".ToLower.IndexOf(Session("SECTION").ToString.ToLower) >= 0 Then
    '    'If "Srintorn".ToString.ToLower.IndexOf(Session("USERNAME").ToString.ToLower) >= 0 Then
    '    'If Session("SECTION").ToString.ToUpper.IndexOf("/PR") >= 0 Then
    '    If Session("AllowChangeRequisition") = True Then
    '        'txbPRRCPDate.Visible = True
    '        gvHist.EditIndex = e.NewEditIndex
    '        Bind_gvHist()
    '        CType(gvHist.Rows(e.NewEditIndex).Cells(11).FindControl("imgEdit"), ImageButton).Visible = True
    '    Else
    '        If ddlHistoryBy.SelectedItem.Text <> "My Requisition" Then 'All Requisition not allow user to go to edit mode
    '            lblMessage.Text = "You can edit only your own requisition."
    '            e.Cancel = True
    '        Else
    '            gvHist.EditIndex = e.NewEditIndex
    '            Bind_gvHist()

    '            Dim lblPRRCPDate As Label = CType(gvHist.Rows(e.NewEditIndex).Cells(10).FindControl("lblPRRCPDate"), Label)
    '            lblPRRCPDate.Visible = True

    '            Dim txbPRRCPDate As TextBox = CType(gvHist.Rows(e.NewEditIndex).Cells(10).FindControl("txbPRRCPDate"), TextBox)
    '            txbPRRCPDate.Visible = False

    '            Dim txbAPVDate As TextBox = CType(gvHist.Rows(e.NewEditIndex).Cells(9).FindControl("txbAPVDate"), TextBox)

    '            'Dim imgDelete As ImageButton = CType(gvHist.Rows(e.NewEditIndex).Cells(9).FindControl("imgDelete"), ImageButton)
    '            'imgDelete.Visible = False
    '            'If txbAPVDate.Text = "" AndAlso gvHist.Rows(e.NewEditIndex).Cells(8).Text = "" Then
    '            '    imgDelete.Visible = True
    '            'Else
    '            '    imgDelete.Visible = False
    '            'End If
    '        End If
    '    End If
    '    'Session("SECTION") = test
    '    '#################
    'End Sub

    Protected Sub gvHistAX_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvHistAX.RowEditing
        If Session("AllowChangeRequisition") = True Then
            gvHistAX.EditIndex = e.NewEditIndex
            Bind_gvHistAX()
            CType(gvHistAX.Rows(e.NewEditIndex).Cells(10).FindControl("imgEdit"), ImageButton).Visible = True
        Else
            If ddlHistoryBy.SelectedItem.Text <> "My Requisition" Then 'All Requisition not allow user to go to edit mode
                lblMessage.Text = "You can edit only your own requisition."
                e.Cancel = True
            Else
                gvHistAX.EditIndex = e.NewEditIndex
                Bind_gvHistAX()

                Dim lblPRRCPDate As Label = CType(gvHistAX.Rows(e.NewEditIndex).Cells(7).FindControl("lblPRRCPDate"), Label)
                lblPRRCPDate.Visible = True

                Dim txbPRRCPDate As TextBox = CType(gvHistAX.Rows(e.NewEditIndex).Cells(7).FindControl("txbPRRCPDate"), TextBox)
                txbPRRCPDate.Visible = False

                'Dim txbAPVDate As TextBox = CType(gvHistax.Rows(e.NewEditIndex).Cells(6).FindControl("txbAPVDate"), TextBox)

            End If
        End If
    End Sub

    'Protected Sub gvHist_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvHist.RowUpdating
    '    gvHist.EditIndex = -1
    '    Bind_gvHist()
    'End Sub

    Protected Sub gvHistAX_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvHistAX.RowUpdating
        gvHistAX.EditIndex = -1
        Bind_gvHistAX()
    End Sub

    'Protected Sub gvHist_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvHist.RowCancelingEdit
    '    gvHist.EditIndex = -1
    '    Bind_gvHist()
    'End Sub

    Protected Sub gvHistAX_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvHistAX.RowCancelingEdit
        gvHistAX.EditIndex = -1
        Bind_gvHistAX()
    End Sub

    'Protected Sub gvHist_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvHist.SelectedIndexChanging
    '    If gvHist.EditIndex = -1 Then
    '        gvHist.SelectedIndex = e.NewSelectedIndex
    '        Bind_gvHist()
    '    Else
    '        e.Cancel = True 'can not change row if in editing state
    '    End If
    'End Sub

    Protected Sub gvHistAX_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles gvHistAX.SelectedIndexChanging
        If gvHistAX.EditIndex = -1 Then
            gvHistAX.SelectedIndex = e.NewSelectedIndex
            Bind_gvHistAX()
        Else
            e.Cancel = True 'can not change row if in editing state
        End If
    End Sub

    Protected Sub ddlHistoryType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHistoryBy.SelectedIndexChanged
        Session("RQNNUMBER") = ""
        gvHistAX.SelectedIndex = -1
        gvHistAX.EditIndex = -1
        'Bind_gvHist()
        Bind_gvHistAX()
    End Sub

    Protected Sub draw_CrystalReportViewer(ByVal RQNNUMBER As String, _
                                            ByVal DocNo As String, _
                                            ByVal Revision As String, _
                                            ByVal EffectiveDate As String)
        If RQNNUMBER <> "" Then
            Dim reportfile As String = ""
            reportfile = Server.MapPath("../Report/PORQN01.RPT")
            'If Session("DATABASE") = "HOPTMO" Then
            '    reportfile = Server.MapPath("../Report/GMOPR02.RPT")
            'End If
            Session("report") = reportfile
            Dim ReportBLL As New ReportBLL(Session("DATABASE"))
            reportDocument.Close()
            reportDocument = ReportBLL.Load(reportfile, RQNNUMBER, Session("DATABASE"), DocNo, Revision, EffectiveDate)

            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf
            CrystalReportViewer1.ReportSource = reportDocument
            CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None
            CrystalReportViewer1.DisplayToolbar = False
            CrystalReportViewer1.DocumentView = CrystalDecisions.Shared.DocumentViewType.WebLayout
        Else
            CrystalReportViewer1.Visible = False
        End If
    End Sub

    Protected Sub HistoryAction(ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim dt As New System.Data.DataTable
        Dim dr As System.Data.DataRow
        dt.Columns.Add("ITEMID")
        dt.Columns.Add("ItemRecId")
        dt.Columns.Add("ProductDimension")
        dt.Columns.Add("NAME")
        dt.Columns.Add("Config")
        dt.Columns.Add("Size")
        dt.Columns.Add("Color")
        dt.Columns.Add("ITEMCOMMENT")
        dt.Columns.Add("LINENUM")
        dt.Columns.Add("RECID")
        dt.Columns.Add("OQORDERED")
        dt.Columns.Add("ORDERUNIT")
        dt.Columns.Add("VDCODEL")
        dt.Columns.Add("VDNAMEL")
        dt.Columns.Add("EXPARRIVAL")
        dt.Columns.Add("INVENTDIMID")
        dt.Columns.Add("MANITEMNO")
        dt.Columns.Add("DTCOMPLETEL")
        dt.Columns.Add("ITEMCOST")
        dt.Columns.Add("INSTRUCTCOMMENT")
        dt.Columns.Add("CURRENCY")
        dt.Columns.Add("CURRENCYCODEISO")
        Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
        Dim dtl As System.Data.DataTable = RequisitionHistoryBLL.getRequestHistoryListByRequestNumberAX(e.CommandArgument.ToString.Split(",")(0))
        If dtl.Rows.Count > 0 Then
            '    Session("ItemType") = dtl.Rows(0)("OPTFLD4")
            Session("REQUIREDDATEH") = dtl.Rows(0)("REQUIREDDATEH")
            Session("ItemType") = dtl.Rows(0)("ECL_PRAMOUNT")
            Session("INVENTLOCATIONID") = dtl.Rows(0)("INVENTLOCATIONID")
            '    Session("VDCODE") = dtl.Rows(0)("VDCODE")
            '    Session("VDNAME") = dtl.Rows(0)("VDNAME")
            '    Session("DESCRIPTION") = dtl.Rows(0)("DESCRIPTIO")
            '    Session("REFERENCE") = dtl.Rows(0)("REFERENCE")
        End If
        Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        'Dim dtItem As System.Data.DataTable
        For Each drl As System.Data.DataRow In dtl.Rows
            dr = dt.NewRow
            dr("ITEMID") = drl("ITEMID")
            dr("ItemRecId") = drl("PRODUCT")
            dr("ITEMCOMMENT") = drl("ECL_REMARK")
            dr("LINENUM") = drl("LINENUM")
            dr("RECID") = drl("RECID")
            dr("ProductDimension") = drl("PRODUCTDIMENSION")
            dr("Config") = drl("Config")
            dr("Size") = drl("Size")
            dr("Color") = drl("Color")
            'dtItem = RequisitionBLL.getItemByItemCode(drl("ITEMNO"))
            'If dtItem.Rows.Count > 0 Then
            '    dr("ITEMDESC") = dtItem.Rows(0)("DESC") 'drl("ITEMDESC")
            '    dr("ITEMCOMMENT") = drl("ITEMDESC").ToString.Replace(dtItem.Rows(0)("DESC"), "").Replace("(", "").Replace(")", "")
            'End If
            dr("NAME") = drl("NAME")

            'dr("OQORDERED") = Format(CDbl(drl("OQORDERED")), "0.00")
            dr("OQORDERED") = Format(CDbl(drl("PURCHQTY")), "0")
            dr("ORDERUNIT") = drl("UNIT")
            'dr("VDCODEL") = drl("VDCODEL")
            'dr("VDNAMEL") = drl("VDNAMEL")
            dr("EXPARRIVAL") = drl("REQUIREDDATEL")
            dr("INVENTDIMID") = drl("INVENTDIMID")
            dr("MANITEMNO") = drl("ECL_SHORTNAME")
            'dr("DTCOMPLETEL") = drl("DTCOMPLETEL")
            'dr("INSTRUCTCOMMENT") = drl("INSTRUCTCOMMENT")
            'Dim dtItemCost As System.Data.DataTable = RequisitionBLL.getLastPriceByItemCode(drl("ITEMNO"))
            'If dtItemCost.Rows.Count > 0 Then
            '    dr("ITEMCOST") = dtItemCost.Rows(0)("UNITCOST")
            '    dr("CURRENCY") = dtItemCost.Rows(0)("CURRENCY")
            'Else
            '    dr("ITEMCOST") = 0
            'End If

            dr("ITEMCOST") = drl("PURCHPRICE")

            dr("CURRENCY") = drl("CURRENCYCODE")
            dr("CURRENCYCODEISO") = drl("CURRENCYCODEISO")

            dt.Rows.Add(dr)
        Next
        Session("Action") = e.CommandName
        Session("gvItemList") = dt
        Response.Redirect("Requisition.aspx")
    End Sub

    Protected Sub Fillter_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    txbRQ1.TextChanged, _
    txbRQ2.TextChanged, _
    txbDate1.TextChanged, _
    txbDate2.TextChanged, _
    txbVendor.TextChanged, _
    txbSection.textchanged
        'Bind_gvHist()
        Bind_gvHistAX()
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        reportDocument.Close()
        reportDocument.Dispose()
        GC.Collect()
    End Sub

    'Protected Sub gvHist_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvHist.RowDeleting
    '    gvHist.EditIndex = -1
    '    Bind_gvHist()
    'End Sub

    Protected Sub gvHistAX_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvHistAX.RowDeleting
        If gvHistAX.Rows.Count > 0 Then
            gvHistAX.EditIndex = -1
        End If
        Bind_gvHistAX()
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click

    End Sub
End Class
