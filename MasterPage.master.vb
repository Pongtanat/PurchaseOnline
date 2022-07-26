
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Protected relativePath As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Session("USERNAME") = "" Then
            Response.Redirect("~/login.aspx")
        End If
        Page.MaintainScrollPositionOnPostBack = True
        relativePath = VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/gradian.gif")

        'lblUserName.Text = String.Format("{0}/{1}({2}) [{3}]", Session("USERNAME"), Session("SECTION"), Session("FACTORY"), Session("GROUP"))

        Dim script As String = " if (Sys &&     Sys.WebForms && Sys.WebForms.PageRequestManager &&     Sys.WebForms.PageRequestManager.getInstance)  {     var prm = Sys.WebForms.PageRequestManager.getInstance();     if (prm &&        !prm._postBackSettings)     {         prm._postBackSettings = prm._createPostBackSettings(false, null, null);     }}"

        imgbtnHome.Attributes.Add("onmouseover", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/m_PurchaseOrder_h.png';"))
        imgbtnHome.Attributes.Add("onmouseout", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/m_PurchaseOrder.png';"))

        imgbtnNew.Attributes.Add("onmouseover", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/m_NewRequisition_h.png';"))
        imgbtnNew.Attributes.Add("onmouseout", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/m_NewRequisition.png';"))

        imgbtnHist.Attributes.Add("onmouseover", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/m_History_h.png';"))
        imgbtnHist.Attributes.Add("onmouseout", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/m_History.png';"))

        ddlSection.Attributes.Add("onchange", "return ddlSection_change(this);")
        ddlSection.Attributes.Add("onfocus", "this.oldIndex = this.selectedIndex")


        ScriptManager.RegisterOnSubmitStatement(Page, Page.GetType(), "FixPopupFormSubmit", Script)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblUserName.Text = Session("USERNAME")
            lblSection.Text = "/Section : "
            lblUserName.ToolTip = Session("FULLNAME") & Session("GROUP").ToString.Replace(",", ControlChars.NewLine)
            lblACCPAC.Text = Session("FACTORY") '& " - TEST"
            If ConfigurationManager.AppSettings("SERVER") = "TEST" Then
                lblACCPAC.Text += " - TEST"
            End If
            'If Session("AllowEditPermission") = False Then
            '    Menu1.Items(1).Enabled = False
            '    Menu1.Items.Remove(Menu1.Items(1))
            'End If
            Dim MasterPageBLL As New MasterPageBLL(Session("DATABASE"))
            Dim dtSection As System.Data.DataTable = MasterPageBLL.getAllSubSectionByFactory(Session("SITE"))
            ddlSection.DataTextField = "ECL_SHORTNAME"
            ddlSection.DataValueField = "ECL_SHORTNAME"
            ddlSection.DataSource = dtSection
            ddlSection.DataBind()
            ddlSection.Text = Session("SECTION")

            If ddlSection.Items.IndexOf(ddlSection.Items.FindByText(Session("SECTION"))) < 0 Then
                ddlSection.Items.Insert(0, New ListItem("-Not found-", ""))
                ddlSection.ToolTip = String.Format("Section:{0}?", Session("SECTION"))
            Else
                ddlSection.Text = Session("SECTION")
            End If
        End If

        If Not Session("AllowAnotherSection") Then
            For Each listitem1 As ListItem In ddlSection.Items
                If listitem1.Value <> Session("SECTION") Then
                    listitem1.Attributes.Add("disabled", "disabled")
                End If
            Next
        End If

        Dim arrSection() As String = Session("SECTIONS").ToString.Split(",")
        For Each strSection As String In arrSection
            For Each listitem1 As ListItem In ddlSection.Items
                If listitem1.Value = strSection Then
                    listitem1.Attributes.Remove("disabled")
                End If
            Next
        Next

    End Sub

    Protected Sub ddlSection_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlSection.SelectedIndexChanged
        Session("SECTION") = ddlSection.Text
        Dim lblReqSec As Label = CType(ContentPlaceHolder1.FindControl("lblReqSec"), Label)
        If Not IsNothing(lblReqSec) Then
            lblReqSec.Text = Session("SECTION")
            Session("Action") = ""
            Response.Redirect("Requisition.aspx")
        End If
    End Sub
End Class

