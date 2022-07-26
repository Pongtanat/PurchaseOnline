Imports System.Data
Imports System.DirectoryServices

Partial Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            ScriptManager1.SetFocus(txbUser)
            Session.Abandon()

            'ddlDomain.Items.Add(New ListItem("HO", "HO"))
            'ddlDomain.Items.Add(New ListItem("PO", "PO"))
            'ddlDomain.Items.Add(New ListItem("RP", "RP"))
            'ddlDomain.Items.Add(New ListItem("GMO", "MO"))
            ddlDomain.Items.Add(New ListItem("HOYA", "hoya"))

            ddlSite.Items.Add(New ListItem("Head Office", "HOPTHO"))
            ddlSite.Items.Add(New ListItem("---------------------", ""))
            ddlSite.Items.Add(New ListItem("PO Domestic", "HPTPOL"))
            ddlSite.Items.Add(New ListItem("PO Import", "HOPT"))
            ddlSite.Items.Add(New ListItem("PO Material", "HPTMAT"))
            ddlSite.Items.Add(New ListItem("---------------------", ""))
            ddlSite.Items.Add(New ListItem("PO3 Domestic", "HPTNPL"))
            ddlSite.Items.Add(New ListItem("PO3 Import", "HOPTNP"))
            ddlSite.Items.Add(New ListItem("PO3 Material", "HMATNP"))
            ddlSite.Items.Add(New ListItem("---------------------", ""))
            ddlSite.Items.Add(New ListItem("RP Domestic", "HPTRPL"))
            ddlSite.Items.Add(New ListItem("RP Import", "HOPTRP"))
            ddlSite.Items.Add(New ListItem("---------------------", ""))
            ddlSite.Items.Add(New ListItem("GMO Domestic", "HPTMOL"))
            ddlSite.Items.Add(New ListItem("GMO Import", "HOPTMO"))

            'ddlSite.Items.Add(New ListItem("Head Office", "HO"))
            'ddlSite.Items.Add(New ListItem("PO Factory", "PO"))
            'ddlSite.Items.Add(New ListItem("PO3 Factory", "PO3"))
            'ddlSite.Items.Add(New ListItem("RP Factory", "RP"))
            'ddlSite.Items.Add(New ListItem("GMO Factory", "GMO"))

            'Dim RequisitionBLL As New RequisitionBLL("HOAX61TEST")
            'ddlSite.DataTextField = "INVENTSITEID"
            'ddlSite.DataValueField = "INVENTSITEID"
            'ddlSite.DataSource = RequisitionBLL.getSiteAX
            'ddlSite.DataBind()

            For Each listitem1 As ListItem In ddlSite.Items
                If listitem1.Value = "" Then
                    listitem1.Attributes.Add("disabled", "disabled")
                End If
            Next

            ibtnLogin.Attributes.Add("onmouseover", "this.src='image/login_over.png'")
            ibtnLogin.Attributes.Add("onmouseout", "this.src='image/login.png'")
            ddlDomain.Attributes.Add("onkeypress", "if(event.keyCode==13)document.getElementById('" & ibtnLogin.ClientID & "').click();")
            ddlSite.Attributes.Add("onkeypress", "if(event.keyCode==13)document.getElementById('" & ibtnLogin.ClientID & "').click();")

            If Not (Request.Cookies("PROnline") Is Nothing) Then
                txbUser.Text = Request.Cookies("PROnline")("USR")
                ddlDomain.SelectedValue = Request.Cookies("PROnline")("DOMAIN")
                ddlSite.SelectedValue = Request.Cookies("PROnline")("SITE")
                ScriptManager1.SetFocus(txbPass)
            End If
        End If

    End Sub

    Protected Sub ibtnLogin_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibtnLogin.Click
        Dim strDomain As String
        Dim strUser As String
        If txbUser.Text.IndexOf("\") >= 0 Then
            strDomain = txbUser.Text.Split("\")(0)
            strUser = txbUser.Text.Split("\")(1)
        ElseIf txbUser.Text.IndexOf("@") > 0 Then
            strDomain = txbUser.Text.Split("@")(1)
            strUser = txbUser.Text.Split("@")(0)
        Else
            strDomain = ddlDomain.SelectedItem.Value.ToUpper
            strUser = txbUser.Text
        End If

        If ConfigurationManager.AppSettings("SERVER") = "LIVE" Then
            Session("DATABASE") = ConfigurationManager.AppSettings("DATABASE_LIVE")
        Else
            Session("DATABASE") = ConfigurationManager.AppSettings("DATABASE_TEST")
        End If

        Dim LoginBLL As New LoginBLL
        Dim loginOBJ As LoginOBJ = LoginBLL.Login(strDomain, strUser, txbPass.Text, ddlSite.SelectedItem.Value)

        If loginOBJ.AUTHENTICATED = True Then
            Session("USERNAME") = loginOBJ.UserName
            Session("AUTHENTICATED") = loginOBJ.AUTHENTICATED
            Session("FULLNAME") = loginOBJ.FullName
            'Session("USER") = result.Properties("SAMAccountName")(0).ToString
            Session("SECTION") = loginOBJ.Section
            Session("SECTIONS") = loginOBJ.Sections
            Session("DOMAIN") = loginOBJ.DomainName
            Session("FACTORY") = ddlSite.SelectedItem.Text 'loginOBJ.Factory
            Session("WORKPLACE") = loginOBJ.WorkPlace
            Session("GROUP") = loginOBJ.OUGroup 'loginOBJ.UserGroup
            Session("FirstName") = loginOBJ.FirstName
            Session("LastName") = loginOBJ.LastName
            'Session("PRGROUP") = loginOBJ.PRGroup
            Session("AllowSelectVendor") = loginOBJ.AllowSelectVendor
            Session("AllowSubmitPRApprove") = loginOBJ.AllowSubmitPRApprove
            Session("AllowSubmitPRReceive") = loginOBJ.AllowSubmitPRReceive
            Session("AllowChangeRequisition") = loginOBJ.AllowChangeRequisition
            Session("AllowEditPermission") = loginOBJ.AllowEditPermission
            Session("AllowAnotherSection") = loginOBJ.AllowAnotherSection
            Session("ACCPAC") = loginOBJ.ACCPAC '"HOPTHO"
            'Session("DATABASE") = "HOAX61LIVE" ' "" 'HOAX61TEST
            Session("SITE") = loginOBJ.axHOYA_Site '"HO,PO,RP,GMO,PO3"
            Session("SITEVALUE") = ddlSite.SelectedItem.Value 'HOPTHO,HOPTMO,HPTMOL,HOPTPO,HOPT,HPTPOL,HOPTRP,HPTRPL
            Session("DIMENSIONFINANCIALTAG_FACTORY") = loginOBJ.axHOYA_FactoryDIMENSIONFINANCIALTAG
            Session("FilterItemCode") = loginOBJ.FilterItemCode
            Session("CategorySearchText") = loginOBJ.CategorySearchText
            Session("PRPreparerEmpCode") = loginOBJ.PRPreparerEmpCode
            'Dim dtConnection As DataTable = LoginBLL.GetConnection(loginOBJ)
            'If dtConnection.Rows.Count > 0 Then
            '    Session("DOCNO") = dtConnection.Rows(0)("DocNo")
            '    Session("REVISION") = dtConnection.Rows(0)("Revision")
            '    Session("EFFECTIVE") = dtConnection.Rows(0)("EffectDate")
            'End If

            Response.Cookies("PROnline")("USR") = System.Globalization.CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(txbUser.Text)
            Response.Cookies("PROnline")("DOMAIN") = ddlDomain.SelectedItem.Value
            Response.Cookies("PROnline")("SITE") = ddlSite.SelectedItem.Value
            Response.Cookies("PROnline").Expires = Now.AddMonths(1)

            Response.Redirect("Default.aspx")
        Else
            ScriptManager1.SetFocus(txbPass)
            lblMessage.Text = "Error : <br>" & loginOBJ.Message
        End If

    End Sub
End Class
