Imports System.Data

Partial Class Purchase_Requisition
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AutoCompleteExtender1.ContextKey = Session("DATABASE")
        'AutoCompleteExtender2.ContextKey = Session("DATABASE")

        'AutoCompleteExtender3.ContextKey = Session("DATABASE")
        'AutoCompleteExtender4.ContextKey = Session("DATABASE")
        'AutoCompleteExtender3.ContextKey = Session("DATABASE") & "," & Session("FilterItemCode") & "," & Session("DOMAIN") 'for search item with filter by factory and Domain
        AutoCompleteExtender3.ContextKey = Session("DATABASE") & "," & Session("FilterItemCode") & "," & Session("CategorySearchText") 'for search item with filter by factory and Domain
        'AutoCompleteExtender4.ContextKey = Session("DATABASE") & "," & Session("FilterItemCode") & "," & Session("DOMAIN") 'for search item with filter by factory and Domain
        AutoCompleteExtender4.ContextKey = Session("DATABASE") & "," & Session("FilterItemCode") & "," & Session("CategorySearchText") 'for search item with filter by factory and Domain

        divgvItemError.Visible = False

        If Not IsPostBack Then
            txbReqNo.Attributes.Add("onkeydown", "if(event.keyCode==8)return false;")
            txbLocName.Attributes.Add("onkeydown", "if(event.keyCode==8)return false;")
            lblIssueDate.Text = Format(Now, "dd-MMM-yyyy")

            Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
            'ddlSite.DataTextField = "INVENTSITEID"
            'ddlSite.DataValueField = "INVENTSITEID"
            'ddlSite.DataSource = RequisitionBLL.getSiteAX
            'ddlSite.DataBind()

            'For Each litem As ListItem In ddlSite.Items
            '    litem.Attributes.Add("title", litem.Value)
            'Next

            Bind_ddlWH()
            'Bind_ddlCurrency()
            'Session("dtAllSec") = RequisitionBLL.getAllSectionByFactory(Session("DIMENSIONFINANCIALTAG_FACTORY"))
            Session("dtAllSec") = RequisitionBLL.getAllSubSectionBySection(Session("SECTION"))
            'Session("dtAllSec") = RequisitionBLL.getAllSubSectionBySection(CType(Master.FindControl("ddlSection"), DropDownList).SelectedItem.Text)
            'Session("dtAllSec")

            If ddlWH.Items.Count = 1 Then
                txbLocName.Text = ddlWH.SelectedItem.Text
                'Session("dtSize") = RequisitionBLL.getSizeBySiteLocationAX(Session("SITE"), ddlWH.SelectedItem.Text)
            ElseIf ddlWH.Items.Count > 1 Then
                Dim listBlankLoc As New ListItem("-Select Location-", "")
                listBlankLoc.Attributes.Add("disabled", "disabled")
                ddlWH.Items.Insert(0, listBlankLoc)

                txbItemCode.BackColor = Drawing.Color.LightGray : txbItemCode.Enabled = False
                txbItemDesc.BackColor = Drawing.Color.LightGray : txbItemDesc.Enabled = False
                imgbtnFindItem.Enabled = False
            Else
                txbItemCode.BackColor = Drawing.Color.LightGray : txbItemCode.Enabled = False
                txbItemDesc.BackColor = Drawing.Color.LightGray : txbItemDesc.Enabled = False
                imgbtnFindItem.Enabled = False
            End If

            lblReqName.Text = Session("FULLNAME")
            lblReqSec.Text = Session("SECTION")
            'Dim ddlSection As DropDownList = CType(Master.FindControl("ddlSection"), DropDownList)
            'If Not (IsNothing(ddlSection)) Then
            '    If ddlSection.SelectedItem.Value = "" Then
            '        lblReqSec.Text = "??"
            '        lblReqSec.CssClass = "error"
            '    End If
            'End If

            'Session("Action") = "EditRQ"
            'Session("RQNNUMBER") = "RQH1204005"
            If Session("Action") = "EditRQ" Then
                Dim dtList As DataTable = RequisitionBLL.getRequisitionAX(Session("RQNNUMBER"))
                txbReqNo.Text = Session("RQNNUMBER")
                'lblIssueDate.Text = Format(New Date(dtList.Rows(0)("POSTDATE").ToString.Substring(0, 4), dtList.Rows(0)("POSTDATE").ToString.Substring(4, 2), dtList.Rows(0)("POSTDATE").ToString.Substring(6, 2)), "dd-MMM-yyyy")
                lblIssueDate.Text = dtList.Rows(0)("POSTDATE").ToString
                'Session("RQNHSEQ") = dtList.Rows(0)("RQNHSEQ")
                lblReqName.Text = dtList.Rows(0)("REQUESTBY").ToString.Split("-")(2)
                lblReqSec.Text = dtList.Rows(0)("REQUESTBY").Split("-")(1)
                rdoItemType.SelectedValue = dtList.Rows(0)("PRAMOUNT") : rdoItemType.Enabled = True
                'If dtList.Rows(0)("EXPARRIVALH").ToString <> "0" Then
                '    txbRequireDate.Text = String.Format("{0}/{1}/{2}", dtList.Rows(0)("EXPARRIVALH").ToString.Substring(6, 2), dtList.Rows(0)("EXPARRIVALH").ToString.Substring(4, 2), dtList.Rows(0)("EXPARRIVALH").ToString.Substring(0, 4))
                'End If
                txbRequireDate.Text = IIf(dtList.Rows(0)("EXPARRIVALH").ToString = "01/01/1900", "", dtList.Rows(0)("EXPARRIVALH").ToString)
                'If dtList.Rows(0)("EXPIRATION").ToString <> "0" Then
                '    txbExpDate.Text = String.Format("{0}/{1}/{2}", dtList.Rows(0)("EXPIRATION").ToString.Substring(6, 2), dtList.Rows(0)("EXPIRATION").ToString.Substring(4, 2), dtList.Rows(0)("EXPIRATION").ToString.Substring(0, 4))
                'End If
                'txbVendorCode.Text = dtList.Rows(0)("VDCODE")
                'txbVendorName.Text = dtList.Rows(0)("VDNAME")
                'txbRemark.Text = dtList.Rows(0)("DESCRIPTION")
                txbReference.Text = dtList.Rows(0)("REFERENCE")
                'Session("gvItemList") = dtList
                txbItemCode.BackColor = Drawing.Color.LightGray : txbItemCode.Enabled = False
                txbItemDesc.BackColor = Drawing.Color.LightGray : txbItemDesc.Enabled = False
                imgbtnAddItem.Enabled = False
                Bind_gvItemList()
            ElseIf Session("Action") = "Copy" AndAlso Not (Session("gvItemList") Is Nothing) Then
                rdoItemType.SelectedIndex = Session("ItemType")
                'rdoItemType.Items.IndexOf(rdoItemType.Items.FindByValue(Session("ItemType")))
                txbRequireDate.Text = IIf(Session("REQUIREDDATEH") <> "01/01/1900", Session("REQUIREDDATEH"), "")
                'txbVendorCode.Text = Session("VDCODE")
                'txbVendorName.Text = Session("VDNAME")
                'txbRemark.Text = Session("DESCRIPTION")
                txbReference.Text = Session("REFERENCE")
                'txbVendorCode.BackColor = Drawing.Color.LightGray : txbVendorCode.Enabled = False
                'txbVendorName.BackColor = Drawing.Color.LightGray : txbVendorName.Enabled = False

                ddlWH.SelectedIndex = ddlWH.Items.IndexOf(ddlWH.Items.FindByText(Session("INVENTLOCATIONID")))
                txbLocName.Text = ddlWH.SelectedItem.Text
                txbItemCode.BackColor = Nothing : txbItemCode.Enabled = True
                txbItemDesc.BackColor = Nothing : txbItemDesc.Enabled = True
                imgbtnFindItem.Enabled = True

                Bind_gvItemList()
                Session("Action") = "NewRQ"
            Else
                Dim dt As New DataTable
                dt.Columns.Add("ITEMNO")
                dt.Columns.Add("ITEMDESC")
                dt.Columns.Add("ItemRecId")
                dt.Columns.Add("ProductDimension")
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
                'dt.PrimaryKey = New DataColumn() {dt.Columns("ITEMNO")}
                'dt.Columns("ITEMNO").Unique = True
                'txbVendorCode.BackColor = Drawing.Color.LightGray : txbVendorCode.Enabled = False
                'txbVendorName.BackColor = Drawing.Color.LightGray : txbVendorName.Enabled = False
                Session("gvItemList") = dt
                Session("Action") = "NewRQ"
            End If
            'Dim test As String = Session("SECTION")
            'Session("SECTION") = "Trade"
            'If "Trade PR Purchase Sale".ToLower.IndexOf(Session("SECTION").ToString.ToLower) = -1 Then
            'If "Srintorn".ToString.ToLower.IndexOf(Session("USERNAME").ToString.ToLower) = -1 Then
            'If Session("SECTION").ToString.ToUpper.IndexOf("/PR") = -1 Then
            'If Session("AllowSelectVendor") = False Then
            '    txbVendorCode.BackColor = Drawing.Color.LightGray : txbVendorCode.Enabled = False
            '    txbVendorName.BackColor = Drawing.Color.LightGray : txbVendorName.Enabled = False
            'End If
            'Session("SECTION") = test

            Dim stbScript As New StringBuilder
            stbScript.AppendLine("if(event.keyCode==13)")
            stbScript.AppendLine("document.getElementById('" & imgbtnAddItem.ClientID & "').click();")
            'txbItemCode.Attributes.Add("onkeypress", stbScript.ToString) 'event fire with onkeydown but not work with drill down items
            'txbItemDesc.Attributes.Add("onkeypress", stbScript.ToString)
            'txbItemCode.Attributes.Add("onfocus", "this.select()")
            'txbItemDesc.Attributes.Add("onfocus", "this.select()")
            'If txbReqDate.Text <> "" Then imgCalendar.Visible = False 'disabled calendar
            'imgCalendar.Attributes.Add("onclick", "document.getElementById('" & txbRequireDate.ClientID & "').focus();")

            imgbtnFindItem.Attributes.Add("onclick", "window.open('finder.aspx?cont=" & txbItemCode.ClientID & "','_blank','status=1,location=0,scrollbars=1,menubar=0,toolbar=0,resizable=1,height=300,width=590');")
        Else
            For Each listitem1 As ListItem In ddlWH.Items
                If listitem1.Value = "" Then
                    listitem1.Attributes.Add("disabled", "disabled")
                End If
            Next
            lblSubmitResult.Text = ""
        End If
    End Sub

    Protected Sub ddlWH_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlWH.SelectedIndexChanged
        txbLocName.Text = ddlWH.SelectedItem.Text
        If txbLocName.Text = "" Then
            txbItemCode.BackColor = Drawing.Color.LightGray : txbItemCode.Enabled = False
            txbItemDesc.BackColor = Drawing.Color.LightGray : txbItemDesc.Enabled = False
            imgbtnFindItem.Enabled = False
        Else
            txbItemCode.BackColor = Nothing : txbItemCode.Enabled = True
            txbItemDesc.BackColor = Nothing : txbItemDesc.Enabled = True
            imgbtnFindItem.Enabled = True
        End If
        'Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        'Session("dtSize") = RequisitionBLL.getSizeBySiteLocationAX(Session("SITE"), ddlWH.SelectedItem.Text)
    End Sub

    Protected Sub txbDetail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        dt = Session("gvItemList")
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim gvRow As GridViewRow = currentTextBox.NamingContainer
        Dim rowID As Integer = gvRow.RowIndex
        dt.Rows(rowID)("ITEMCOMMENT") = currentTextBox.Text
        Session("gvItemList") = dt
        'Dim ScriptManager1 As ScriptManager = CType(Master.FindControl("ScriptManager1"), ScriptManager)
        'ScriptManager1.SetFocus(currentTextBox)
    End Sub

    Protected Sub txbQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        dt = getDataFromGrid()
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim gvRow As GridViewRow = currentTextBox.NamingContainer
        Dim rowID As Integer = gvRow.RowIndex
        dt.Rows(rowID)("OQORDERED") = currentTextBox.Text
        Session("gvItemList") = dt
        Bind_gvItemList()

        Dim str As New StringBuilder
        Dim ddlSubSection As DropDownList = CType(gvItemList.Rows(rowID).FindControl("ddlSubSection"), DropDownList)
        str.AppendLine("document.getElementById('" & ddlSubSection.ClientID & "').focus();")
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "gotoddlSubSection", str.ToString, True)
        'Dim ScriptManager1 As ScriptManager = CType(Master.FindControl("ScriptManager1"), ScriptManager)
        'ScriptManager1.SetFocus(currentTextBox)
    End Sub

    'Protected Sub ddlProductDimension_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim dt As DataTable
    '    dt = getDataFromGrid()
    '    Dim currentDropdownlist As DropDownList = CType(sender, DropDownList)
    '    Dim gvRow As GridViewRow = currentDropdownlist.NamingContainer
    '    Dim rowID As Integer = gvRow.RowIndex
    '    dt.Rows(rowID)("LOCATION") = currentDropdownlist.SelectedItem.Value
    '    Session("gvItemList") = dt
    '    Bind_gvItemList()
    'End Sub

    Protected Sub txbVendCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        dt = Session("gvItemList")
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim gvRow As GridViewRow = currentTextBox.NamingContainer
        Dim rowID As Integer = gvRow.RowIndex
        dt.Rows(rowID)("VDCODE") = currentTextBox.Text
        Session("gvItemList") = dt
        'Dim ScriptManager1 As ScriptManager = CType(Master.FindControl("ScriptManager1"), ScriptManager)
        'ScriptManager1.SetFocus(currentTextBox)
    End Sub

    Protected Sub txbVendName_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        dt = Session("gvItemList")
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim gvRow As GridViewRow = currentTextBox.NamingContainer
        Dim rowID As Integer = gvRow.RowIndex
        dt.Rows(rowID)("VDNAME") = currentTextBox.Text
        Session("gvItemList") = dt
        'Dim ScriptManager1 As ScriptManager = CType(Master.FindControl("ScriptManager1"), ScriptManager)
        'ScriptManager1.SetFocus(currentTextBox)
    End Sub

    Protected Sub txbManufac_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        dt = Session("gvItemList")
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim gvRow As GridViewRow = currentTextBox.NamingContainer
        Dim rowID As Integer = gvRow.RowIndex
        dt.Rows(rowID)("MANITEMNO") = currentTextBox.Text
        Session("gvItemList") = dt
        'Dim ScriptManager1 As ScriptManager = CType(Master.FindControl("ScriptManager1"), ScriptManager)
        'ScriptManager1.SetFocus(currentTextBox)
    End Sub

    Protected Sub txbReqDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dt As DataTable
        dt = Session("gvItemList")
        Dim currentTextBox As TextBox = CType(sender, TextBox)
        Dim gvRow As GridViewRow = currentTextBox.NamingContainer
        Dim rowID As Integer = gvRow.RowIndex
        dt.Rows(rowID)("EXPARRIVAL") = 0
        If currentTextBox.Text <> "" Then
            dt.Rows(rowID)("EXPARRIVAL") = currentTextBox.Text.Split("/")(2) & currentTextBox.Text.Split("/")(1) & currentTextBox.Text.Split("/")(0)
        End If
        Session("gvItemList") = dt
        'Dim ScriptManager1 As ScriptManager = CType(Master.FindControl("ScriptManager1"), ScriptManager)
        'ScriptManager1.SetFocus(currentTextBox)
    End Sub

    Protected Sub gvItemList_DataBinding(sender As Object, e As System.EventArgs) Handles gvItemList.DataBinding
        Dim dt As DataTable = gvItemList.DataSource
        Dim boolFoundItemMaster As Boolean = False
        For Each dr As DataRow In dt.Rows
            If dr("ProductDimension").ToString <> "" Then
                boolFoundItemMaster = True
                Exit For
            End If
        Next
        gvItemList.Columns(3).Visible = boolFoundItemMaster
    End Sub

    Protected Sub gvItemList_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvItemList.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim txbDetail As TextBox = CType(e.Row.Cells(2).FindControl("txbDetail"), TextBox)
            'AddHandler txbDetail.TextChanged, AddressOf txbDetail_TextChanged
            Dim txbQty As TextBox = CType(e.Row.FindControl("txbQty"), TextBox)
            AddHandler txbQty.TextChanged, AddressOf txbQty_TextChanged
            'Dim ddlProductDimension As DropDownList = CType(e.Row.Cells(2).FindControl("ddlProductDimension"), DropDownList)
            'AddHandler ddlProductDimension.SelectedIndexChanged, AddressOf ddlProductDimension_SelectedIndexChanged
            'Dim txbVendCode As TextBox = CType(e.Row.Cells(5).FindControl("txbVendCode"), TextBox)
            'AddHandler txbVendCode.TextChanged, AddressOf txbVendCode_TextChanged
            'Dim txbVendName As TextBox = CType(e.Row.Cells(6).FindControl("txbVendName"), TextBox)
            'AddHandler txbVendName.TextChanged, AddressOf txbVendName_TextChanged
            'Dim txbManufac As TextBox = CType(e.Row.Cells(7).FindControl("txbMANITEMNO"), TextBox)
            'AddHandler txbManufac.TextChanged, AddressOf txbManufac_TextChanged
            'Dim txbReqDate As TextBox = CType(e.Row.Cells(8).FindControl("txbReqDate"), TextBox)
            'AddHandler txbReqDate.TextChanged, AddressOf txbReqDate_TextChanged
        End If
    End Sub

    Protected Sub gvItemList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvItemList.RowDataBound
        'If Session("Action") = "EditRQ" Then
        '    e.Row.Cells(10).Visible = False
        'End If
        If e.Row.RowType = DataControlRowType.DataRow Then
            '-----------------------------------------------------------------------------
            e.Row.Cells(1).ToolTip = e.Row.Cells(1).Text
            If e.Row.Cells(1).Text.Length > 30 Then
                e.Row.Cells(1).Text = e.Row.Cells(1).Text.Substring(0, 30) & "..."
            End If
            '-----------------------------------------------------------------------------
            'Dim txbDetail As TextBox = CType(e.Row.Cells(2).FindControl("txbDetail"), TextBox)
            'If e.Row.Cells(1).ToolTip.Length < 58 Then
            '    txbDetail.MaxLength = 60 - (e.Row.Cells(1).ToolTip.Length + 2)
            'Else
            '    txbDetail.ReadOnly = True
            '    txbDetail.MaxLength = 0 'e.Row.Cells(1).ToolTip.Length
            'End If
            'txbDetail.Text = txbDetail.Text.ToString.Replace(e.Row.Cells(1).ToolTip, "").Replace("(", "").Replace(")", "")
            '-----------------------------------------------------------------------------
            'Dim test As String = Session("SECTION")
            'Session("SECTION") = "Trade"
            'Dim txbVDCode As TextBox = CType(e.Row.Cells(5).FindControl("txbVendCode"), TextBox)
            'Dim txbVDName As TextBox = CType(e.Row.Cells(6).FindControl("txbVendName"), TextBox)
            'If "Trade PR Purchase Sale".ToLower.IndexOf(Session("SECTION").ToString.ToLower) = -1 Then
            'If "Srintorn".ToString.ToLower.IndexOf(Session("USERNAME").ToString.ToLower) = -1 Then
            'If Session("SECTION").ToString.ToUpper.IndexOf("/PR") = -1 Then
            'If Session("AllowSelectVendor") = False Then
            '    txbVDCode.Enabled = False
            '    txbVDName.Enabled = False
            '    txbVDCode.BackColor = Drawing.Color.LightGray
            '    txbVDName.BackColor = Drawing.Color.LightGray
            '    'txbVDCode.BackColor = 
            '    'txbVDName.Enabled = True
            'Else
            '    Dim AutoCompleteExtender1 As New AjaxControlToolkit.AutoCompleteExtender
            '    AutoCompleteExtender1 = CType(e.Row.Cells(5).FindControl("AutoCompleteExtender1"), AjaxControlToolkit.AutoCompleteExtender)
            '    Dim AutoCompleteExtender2 As New AjaxControlToolkit.AutoCompleteExtender
            '    AutoCompleteExtender2 = CType(e.Row.Cells(6).FindControl("AutoCompleteExtender2"), AjaxControlToolkit.AutoCompleteExtender)
            '    AutoCompleteExtender1.ContextKey = Session("DATABASE")
            '    AutoCompleteExtender2.ContextKey = Session("DATABASE")
            'End If
            'Session("SECTION") = test
            '-----------------------------------------------------------------------------
            Dim hidMANITEMNO As HiddenField = CType(e.Row.FindControl("hidMANITEMNO"), HiddenField)
            Dim ddlSubSection As DropDownList = CType(e.Row.FindControl("ddlSubSection"), DropDownList)
            'ddlSection.DataTextField = "ECL_SHORTNAME"
            'ddlSection.DataValueField = "ECL_SHORTNAME"
            'ddlSection.DataTextField = "SubSection"
            'ddlSection.DataValueField = "SubSection"
            ddlSubSection.DataTextField = "VALUE"
            ddlSubSection.DataValueField = "VALUE"
            ddlSubSection.DataSource = Session("dtAllSec")
            ddlSubSection.DataBind()
            ddlSubSection.Text = hidMANITEMNO.Value
            ddlSubSection.Items.Insert(0, New ListItem("-", ""))
            'For Each listitem1 As ListItem In ddlSection.Items
            '    If listitem1.Text <> hidMANITEMNO.Value Then
            '        listitem1.Attributes.Add("disabled", "disabled")
            '    End If
            'Next
            '-----------------------------------------------------------------------------
            'CType(e.Row.Cells(2).FindControl("txbDetail"), TextBox).Text = CType(e.Row.Cells(2).FindControl("hidItemMaster"), HiddenField).Value
            'Dim hidLocation As HiddenField = CType(e.Row.Cells(2).FindControl("hidLocation"), HiddenField)
            'Dim ddlProductDimension As DropDownList = CType(e.Row.Cells(2).FindControl("ddlProductDimension"), DropDownList)
            'ddlProductDimension.DataTextField = "PRODUCTDIMENSION"
            'ddlProductDimension.DataValueField = "INVENTDIMID"
            'ddlProductDimension.DataSource = Session("dtSize")
            'ddlProductDimension.DataBind()
            'ddlProductDimension.Items.Insert(0, New ListItem("-Not spicific-", ""))
            'ddlProductDimension.Text = hidLocation.Value
            If CType(e.Row.FindControl("hidItemMaster"), HiddenField).Value <> "" Then
                Dim hidItemRecId As HiddenField = CType(e.Row.FindControl("hidItemRecId"), HiddenField)
                Dim hidConfig As HiddenField = CType(e.Row.FindControl("hidConfig"), HiddenField)
                Dim hidSize As HiddenField = CType(e.Row.FindControl("hidSize"), HiddenField)
                Dim hidColor As HiddenField = CType(e.Row.FindControl("hidColor"), HiddenField)
                Dim ddlConfig As DropDownList = CType(e.Row.FindControl("ddlConfig"), DropDownList)
                Dim ddlSize As DropDownList = CType(e.Row.FindControl("ddlSize"), DropDownList)
                Dim ddlColor As DropDownList = CType(e.Row.FindControl("ddlColor"), DropDownList)

                Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))

                ddlConfig.DataTextField = "Config"
                ddlConfig.DataValueField = "Config"
                ddlConfig.DataSource = RequisitionBLL.getConfigByItemRecIdAX(hidItemRecId.Value)
                ddlConfig.DataBind()
                ddlConfig.Text = hidConfig.Value

                ddlSize.DataTextField = "Size"
                ddlSize.DataValueField = "Size"
                ddlSize.DataSource = RequisitionBLL.getSizeByItemRecIdAX(hidItemRecId.Value)
                ddlSize.DataBind()
                ddlSize.Text = hidSize.Value

                ddlColor.DataTextField = "Color"
                ddlColor.DataValueField = "Color"
                ddlColor.DataSource = RequisitionBLL.getColorByItemRecIdAX(hidItemRecId.Value)
                ddlColor.DataBind()
                ddlColor.Text = hidColor.Value

                ddlConfig.Items.Insert(0, New ListItem("#Config", ""))
                ddlSize.Items.Insert(0, New ListItem("#Size", ""))
                ddlColor.Items.Insert(0, New ListItem("#Color", ""))

                ddlConfig.Visible = True
                ddlSize.Visible = True
                ddlColor.Visible = True
            End If
            '-----------------------------------------------------------------------------
            Dim txbReqDate As TextBox = CType(e.Row.FindControl("txbReqDate"), TextBox)
            If txbReqDate.Text <> "0" And txbReqDate.Text <> "" Then
                'txbReqDate.Text = String.Format("{0}/{1}/{2}", txbReqDate.Text.Substring(6, 2), txbReqDate.Text.Substring(4, 2), txbReqDate.Text.Substring(0, 4))
                txbReqDate.Text = txbReqDate.Text
            Else
                txbReqDate.Text = ""
            End If
            '-----------------------------------------------------------------------------
            'Dim txbRemark As TextBox = CType(e.Row.Cells(9).FindControl("txbRemark"), TextBox)
            Dim txbRemark As Label = CType(e.Row.FindControl("lblUnitPrice"), Label)
            txbRemark.Text = String.Format("{0}", Format(CDbl(txbRemark.Text), "#,##0.00"))
            '-----------------------------------------------------------------------------
            Dim imgbtnDel As ImageButton = CType(e.Row.FindControl("imgbtnDel"), ImageButton)
            imgbtnDel.OnClientClick = "return confirm('Are you sure you want to remove?\n\n""" & e.Row.Cells(1).Text.Replace("'", "\'") & """\n\nfrom this requisition.');"
            '-----------------------------------------------------------------------------
            Dim txblistQty As TextBox = CType(e.Row.FindControl("txbQty"), TextBox)
            txblistQty.Attributes.Add("onkeypress", "return validateinput(this.id,event);")
            txblistQty.Attributes.Add("oncontextmenu", "return false")
            txblistQty.Attributes.Add("onpaste", "return false")
            txblistQty.Attributes.Add("oncopy", "return false")
            txblistQty.Attributes.Add("oncut", "return false")
            txblistQty.Attributes.Add("autocomplete", "off")

            If Session("Action") = "EditRQ" Then
                Dim txbQty As TextBox = CType(e.Row.FindControl("txbQty"), TextBox)
                'txbQty.Enabled = False
                'txbQty.BackColor = Drawing.Color.LightGray

                Dim hidDTCOMPLETEL As HiddenField = CType(e.Row.FindControl("hidDTCOMPLETEL"), HiddenField)
                'If hidDTCOMPLETEL.Value <> 0 Then
                '    txbDetail.Enabled = False
                '    txbVDCode.Enabled = False
                '    txbVDName.Enabled = False
                '    txbMANITEMNO.Enabled = False
                '    txbReqDate.Enabled = False
                '    'txbRemark.Enabled = False
                '    imgbtnDel.Enabled = False
                'End If
                Dim hidITEMCOST As HiddenField = CType(e.Row.FindControl("hidITEMCOST"), HiddenField)
            End If
            '-----------------------------------------------------------------------------
        End If
    End Sub

    Protected Sub gvItemList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvItemList.RowDeleting
        Dim dt As DataTable
        dt = getDataFromGrid()
        dt.Rows(e.RowIndex).Delete()
        Session("gvItemList") = dt
        Bind_gvItemList()
    End Sub

    Protected Sub txbItemCode_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txbItemCode.TextChanged
        AddItem(txbItemCode.Text, "", 0, "", "01/01/1900")
    End Sub

    Protected Sub txbItemDesc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txbItemDesc.TextChanged
        'AddItem()
    End Sub

    Protected Sub imgbtnAddItem_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAddItem.Click
        'AddItem()
    End Sub

    Protected Sub imgbtnAddMultipleItems_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnAddMultipleItems.Click
        Dim dtItemList As DataTable = getDataFromGrid()
        Dim dtProductDimension As DataTable = Session("gvProductDimension")

        Dim dr As DataRow
        For Each drProductDimension As DataRow In dtProductDimension.Rows
            dr = dtItemList.NewRow
            For iCol As Integer = 0 To dtItemList.Columns.Count - 1
                dr(iCol) = drProductDimension(iCol)
                If dtItemList.Columns(iCol).ColumnName = "EXPARRIVAL" AndAlso txbRequireDate.Text <> "" Then
                    dr(iCol) = txbRequireDate.Text
                End If
            Next
            dtItemList.Rows.Add(dr)
        Next

        Session("gvItemList") = dtItemList
        Bind_gvItemList()
    End Sub

    Protected Function getDataFromGrid() As DataTable
        Dim dt As New DataTable
        'dt.Columns.Add("ITEMNO")
        dt.Columns.Add("ItemId")
        dt.Columns.Add("ItemRecId")
        'dt.Columns.Add("ITEMDESC")
        dt.Columns.Add("ProductDimension")
        dt.Columns.Add("Config")
        dt.Columns.Add("Size")
        dt.Columns.Add("Color")
        dt.Columns.Add("Name")
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
        Dim strReqDate As String = ""
        Dim dr As DataRow
        For Each gvrow2 As GridViewRow In gvItemList.Rows
            dr = dt.NewRow
            'dr("ITEMNO") = gvrow2.Cells(0).Text
            dr("ItemId") = gvrow2.Cells(0).Text
            dr("ItemRecId") = CType(gvrow2.FindControl("hidItemRecId"), HiddenField).Value
            'dr("ITEMDESC") = gvrow2.Cells(1).ToolTip
            dr("ProductDimension") = CType(gvrow2.FindControl("hidItemMaster"), HiddenField).Value
            dr("Config") = CType(gvrow2.FindControl("ddlConfig"), DropDownList).Text
            dr("Size") = CType(gvrow2.FindControl("ddlSize"), DropDownList).Text
            dr("Color") = CType(gvrow2.FindControl("ddlColor"), DropDownList).Text
            dr("Name") = gvrow2.Cells(1).ToolTip
            dr("ITEMCOMMENT") = CType(gvrow2.FindControl("txbDetail"), TextBox).Text
            dr("LINENUM") = CType(gvrow2.FindControl("hidLineNum"), HiddenField).Value
            dr("RECID") = CType(gvrow2.FindControl("hidLineRecID"), HiddenField).Value
            dr("OQORDERED") = CType(gvrow2.FindControl("txbQty"), TextBox).Text
            dr("ORDERUNIT") = gvrow2.Cells(5).Text
            'dr("VDCODEL") = CType(gvrow2.Cells(5).FindControl("txbVendCode"), TextBox).Text
            'dr("VDNAMEL") = CType(gvrow2.Cells(6).FindControl("txbVendName"), TextBox).Text
            'dr("MANITEMNO") = CType(gvrow2.Cells(7).FindControl("txbMANITEMNO"), TextBox).Text
            dr("MANITEMNO") = CType(gvrow2.FindControl("ddlSubSection"), DropDownList).Text
            dr("EXPARRIVAL") = IIf(CType(gvrow2.FindControl("txbReqDate"), TextBox).Text = "", "01/01/1900", CType(gvrow2.FindControl("txbReqDate"), TextBox).Text)
            strReqDate = CType(gvrow2.FindControl("txbReqDate"), TextBox).Text
            If strReqDate <> "" Then
                dr("EXPARRIVAL") = strReqDate 'strReqDate.Split("/")(2) & strReqDate.Split("/")(1) & strReqDate.Split("/")(0)
            End If
            '---------------------------------------------------------------------------------------
            'dr("LOCATION") = ddlWH.SelectedItem.Value
            'dr("LOCATION") = CType(gvrow2.Cells(2).FindControl("hidLocation"), HiddenField).Value
            'If CType(gvrow2.Cells(2).FindControl("ddlProductDimension"), DropDownList).SelectedValue <> "" Then
            '    dr("LOCATION") = CType(gvrow2.Cells(2).FindControl("ddlProductDimension"), DropDownList).SelectedValue
            'Else
            '    dr("LOCATION") = CType(gvrow2.Cells(2).FindControl("hidLocation"), HiddenField).Value
            'End If
            '---------------------------------------------------------------------------------------
            dr("DTCOMPLETEL") = 0
            dr("ITEMCOST") = CType(gvrow2.FindControl("hidItemCost"), HiddenField).Value
            'dr("INSTRUCTCOMMENT") = CType(gvrow2.Cells(7).FindControl("txbRemark"), TextBox).Text
            dr("INSTRUCTCOMMENT") = CType(gvrow2.FindControl("lblRemark"), Label).Text
            dr("CURRENCY") = CType(gvrow2.FindControl("hidCurr"), HiddenField).Value 'ddlCurrency.SelectedItem.Text
            dr("CURRENCYCODEISO") = CType(gvrow2.FindControl("hidCurrISO"), HiddenField).Value
            dt.Rows.Add(dr)
        Next
        Session("gvItemList") = dt
        Return dt
    End Function

    Protected Function AddItem(ByVal strItemCode As String, ByVal strDetail As String, ByVal Qty As Integer, ByVal strSection As String, ByVal strRQDate As String) As String
        Dim dt As New DataTable
        Dim dr As DataRow
        dt = getDataFromGrid()
        Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        Dim dtItemDesc As DataTable = RequisitionBLL.getItemByItemCodeAX(strItemCode, Session("FilterItemCode"), Session("CategorySearchText"), False) 'RequisitionBLL.getItemByItemCodeAX(strItemCode, Session("FilterItemCode"), Session("DOMAIN"), False)
        If dtItemDesc.Rows.Count > 0 Then
            dr = dt.NewRow
            'dr("ITEMNO") = txbItemCode.Text
            dr("ItemId") = strItemCode
            dr("ItemRecId") = dtItemDesc.Rows(0)("PRODUCT")
            'dr("ITEMDESC") = dtItemDesc.Rows(0)("ITEMDESC")
            dr("ProductDimension") = dtItemDesc.Rows(0)("ProductDimension")
            dr("Config") = ""
            dr("Size") = ""
            dr("Color") = ""
            dr("Name") = dtItemDesc.Rows(0)("Name")
            dr("ITEMCOMMENT") = strDetail
            dr("OQORDERED") = Qty
            'dr("ORDERUNIT") = dtItemDesc.Rows(0)("STOCKUNIT")
            'dr("VDCODEL") = txbVendorCode.Text
            'dr("VDNAMEL") = txbVendorName.Text
            dr("EXPARRIVAL") = strRQDate
            If txbRequireDate.Text <> "" Then
                'Dim tmpDate As New Date(txbRequireDate.Text.Split("/")(2), txbRequireDate.Text.Split("/")(1), txbRequireDate.Text.Split("/")(0))
                'If Not (IsDBNull(dtItemDesc.Rows(0)("LEADTIME"))) AndAlso dtItemDesc.Rows(0)("LEADTIME") <> 0 Then
                '    tmpDate = skipWeekEnd(tmpDate, dtItemDesc.Rows(0)("LEADTIME"))
                '    dr("EXPARRIVAL") = Format(tmpDate, "yyyy") & Format(tmpDate, "MM") & Format(tmpDate, "dd")
                'Else
                '    dr("EXPARRIVAL") = txbRequireDate.Text.Split("/")(2) & txbRequireDate.Text.Split("/")(1) & txbRequireDate.Text.Split("/")(0)
                'End If
                dr("EXPARRIVAL") = txbRequireDate.Text
            End If
            dr("INVENTDIMID") = ddlWH.SelectedItem.Value
            dr("MANITEMNO") = strSection 'lblReqSec.Text
            dr("DTCOMPLETEL") = 0
            dr("INSTRUCTCOMMENT") = ""
            dr("ITEMCOST") = 0
            'dr("CURRENCY") = "THB" 'ddlCurrency.SelectedItem.Text
            'dr("CURRENCYCODEISO") = "THB"
            Dim dtItemCost As DataTable = RequisitionBLL.getLastPriceByItemCodeAX(strItemCode)
            If dtItemCost.Rows.Count > 0 Then
                dr("ITEMCOST") = dtItemCost.Rows(0)("PRICE")
                dr("ORDERUNIT") = dtItemCost.Rows(0)("UNITID")
                dr("CURRENCY") = dtItemCost.Rows(0)("HOYA_CURRENCYCODE")
                dr("CURRENCYCODEISO") = dtItemCost.Rows(0)("CURRENCYCODEISO")
            End If
            dt.Rows.Add(dr)
            Session("gvItemList") = dt
            txbItemCode.Text = ""
            txbItemDesc.Text = ""
        Else
            Return "not found"
        End If
        Bind_gvItemList()

        Dim str As New StringBuilder
        Dim txbDetail As TextBox = CType(gvItemList.Rows(gvItemList.Rows.Count - 1).FindControl("txbDetail"), TextBox)
        str.AppendLine("document.getElementById('" & txbDetail.ClientID & "').focus();")
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "gototxbDetail", str.ToString, True)

        Return ""
        'Dim dt As DataTable
        'Dim dr As DataRow
        'dt = Session("gvItemList")
        'Dim RequisitionBLL As New RequisitionBLL
        'Dim dtRequisitionBLL As DataTable = RequisitionBLL.getItemByItemCode(txbItemCode.Text)
        'If dtRequisitionBLL.Rows.Count > 0 Then
        '    dr = dt.NewRow
        '    dr("ITEMNO") = txbItemCode.Text
        '    dr("ITEMDESC") = dtRequisitionBLL.Rows(0)("DESC")
        '    dr("ITEMCOMMENT") = ""
        '    dr("OQORDERED") = "0.00"
        '    dr("ORDERUNIT") = dtRequisitionBLL.Rows(0)("STOCKUNIT")
        '    dr("VDCODE") = txbVendorCode.Text
        '    dr("VDNAME") = txbVendorName.Text
        '    dr("EXPARRIVAL") = 0
        '    If txbRequireDate.Text <> "" Then
        '        dr("EXPARRIVAL") = txbRequireDate.Text.Split("/")(2) & txbRequireDate.Text.Split("/")(1) & txbRequireDate.Text.Split("/")(0)
        '    End If
        '    dr("LOCATION") = ddlLocation.SelectedItem.Text
        '    dr("MANITEMNO") = ""
        '    dt.Rows.Add(dr)
        '    Session("gvItemList") = dt
        '    txbItemCode.Text = ""
        '    txbItemDesc.Text = ""
        'End If
        'Bind_gvItemList()

    End Function

    Protected Sub Bind_ddlWH()
        Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        ddlWH.DataTextField = "INVENTLOCATIONID"
        ddlWH.DataValueField = "INVENTDIMID"
        ddlWH.DataSource = RequisitionBLL.getWHAX(Session("SITE"))
        ddlWH.DataBind()
    End Sub

    'Protected Sub Bind_ddlCurrency()
    '    'Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
    '    'ddlCurrency.DataTextField = "INVENTLOCATIONID"
    '    'ddlCurrency.DataValueField = "INVENTDIMID"
    '    'ddlCurrency.DataSource = RequisitionBLL.getWHAX(Session("SITE"))
    '    'ddlCurrency.DataBind()

    '    'ddlCurrency.Items.Add(New ListItem("THB", "THB"))
    '    'ddlCurrency.Items.Add(New ListItem("JPY", "JPS"))
    '    'ddlCurrency.Items.Add(New ListItem("USD", "USS"))
    '    'ddlCurrency.Items.Add(New ListItem("CNY", "CNS"))
    '    If Not (Session("SITEVALUE").ToString.IndexOf("HOPT") >= 0 And Session("SITEVALUE") <> "HOPTHO") Then
    '        ddlCurrency.Items.Add(New ListItem("THB", "THB"))
    '        'For Each listitem1 As ListItem In ddlCurrency.Items
    '        '    If listitem1.Value <> "THB" Then
    '        '        listitem1.Attributes.Add("disabled", "disabled")
    '        '    End If
    '        'Next
    '    Else
    '        ddlCurrency.Items.Add(New ListItem("JPY", "JPS"))
    '        ddlCurrency.Items.Add(New ListItem("USD", "USS"))
    '        ddlCurrency.Items.Add(New ListItem("CNY", "CNS"))
    '        'For Each listitem1 As ListItem In ddlCurrency.Items
    '        '    If listitem1.Value = "THB" Then
    '        '        ddlCurrency.SelectedIndex = 1
    '        '        listitem1.Attributes.Add("disabled", "disabled")
    '        '    End If
    '        'Next
    '    End If
    'End Sub

    Protected Sub Bind_gvItemList()
        Dim dt As DataTable
        dt = Session("gvItemList")

        'CType(gvItemList.Rows(rowID).Cells(9).FindControl("txbRemark"), TextBox).Text = strPrice

        Dim tmpTotal As Double = 0
        Dim tmpCurr As String = ""
        Dim SameCurrency As Boolean = True
        Dim haveAllPrice As Boolean = True
        Dim strCurr As String = ""
        Dim strPrice As String = ""
        For rowID As Integer = 0 To dt.Rows.Count - 1
            'strPrice = CDbl(dt.Rows(rowID)("OQORDERED") * dt.Rows(rowID)("ITEMCOST"))
            dt.Rows(rowID)("INSTRUCTCOMMENT") = ""
            If IsNumeric(dt.Rows(rowID)("OQORDERED")) AndAlso dt.Rows(rowID)("OQORDERED") > 0 Then
                strPrice = Format(CDbl(dt.Rows(rowID)("OQORDERED") * dt.Rows(rowID)("ITEMCOST")), "#,###.####")
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("th-TH")) 'THB
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("ja-JP")) 'JPY
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("en-US")) 'USD
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("zh-HK")) 'HKD Chinese - Hong Kong SAR
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("zh-TW")) 'TWD
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("zh-SG")) 'SGD
                'strPrice = CDbl(strPrice).ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("zh-CHS")) 'CNY zh-CHS zh-CHT zh-CN
                'dt.Rows(rowID)("INSTRUCTCOMMENT") = "(" & dt.Rows(rowID)("CURRENCY") & ")" & strPrice
                If tmpCurr = "" Then tmpCurr = dt.Rows(rowID)("CURRENCYCODEISO").ToString
                If CDbl(dt.Rows(rowID)("OQORDERED") * dt.Rows(rowID)("ITEMCOST")) <> 0 Then
                    strPrice = "(" & dt.Rows(rowID)("CURRENCYCODEISO").ToString & ")" & strPrice
                    strCurr = dt.Rows(rowID)("CURRENCYCODEISO").ToString
                Else
                    haveAllPrice = False
                End If
                dt.Rows(rowID)("INSTRUCTCOMMENT") = strPrice
                tmpTotal += dt.Rows(rowID)("ITEMCOST") * dt.Rows(rowID)("OQORDERED")
                If dt.Rows(rowID)("CURRENCYCODEISO").ToString <> "" AndAlso strCurr <> tmpCurr Then
                    SameCurrency = False
                End If
                'If dt.Rows(rowID)("ITEMCOST")
            End If
            'If Session("Action") = "EditRQ" Then
            '    dt.Rows(rowID)("EXPARRIVAL") = IIf(dt.Rows(rowID)("EXPARRIVAL") = "01/01/1900", "", dt.Rows(rowID)("EXPARRIVAL"))
            'Else
            '    dt.Rows(rowID)("EXPARRIVAL") = ""
            'End If
            'dt.Rows(rowID)("EXPARRIVAL") = IIf(dt.Rows(rowID)("EXPARRIVAL") = "01/01/1900", "", dt.Rows(rowID)("EXPARRIVAL"))
            dt.Rows(rowID)("EXPARRIVAL") = IIf(dt.Rows(rowID)("EXPARRIVAL").ToString.Substring(0, 10) = "01/01/1900", "", dt.Rows(rowID)("EXPARRIVAL"))
            'dt.Rows(rowID)("EXPARRIVAL") = dt.Rows(rowID)("EXPARRIVAL")

            dt.Rows(rowID)("DTCOMPLETEL") = 0
        Next

        gvItemList.DataSource = dt
        gvItemList.DataBind()

        If gvItemList.Rows.Count > 0 Then
            If Session("Action") = "EditRQ" Then
                btnSave.Visible = True
                btnCancel.Visible = True
            Else
                btnSubmit.Visible = True
            End If
            btnClear.Visible = True
            gvItemList.FooterRow.Cells(9).CssClass = "right"
            'gvItemList.FooterRow.Cells(7).Text = IIf(SameCurrency, String.Format("({0}){1}", tmpCurr, Format(CDbl(tmpTotal), "#,###.####")), "")
            If SameCurrency Then
                gvItemList.FooterRow.Cells(9).Text = IIf(SameCurrency, String.Format("({0})", tmpCurr), "")
                gvItemList.FooterRow.Cells(9).Text += Format(CDbl(tmpTotal), "#,###.####")
                gvItemList.FooterRow.Cells(9).Text += IIf(haveAllPrice, "", "+")
            End If
        Else
            btnSubmit.Visible = False
            btnClear.Visible = False
        End If
    End Sub

    Protected Function skipWeekEnd(ByVal tmpDate As Date, ByVal addDay As Integer) As Date
        Dim tmpAddDay As Integer = 0
        Do While addDay > 0
            If Weekday(tmpDate) + addDay > 6 Then
                tmpAddDay = 7 - Weekday(tmpDate)
                tmpDate = tmpDate.AddDays(tmpAddDay)
                tmpDate = tmpDate.AddDays(2)
            Else
                tmpDate = tmpDate.AddDays(addDay)
            End If
            addDay = addDay - tmpAddDay
        Loop

        Return tmpDate
    End Function

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click, btnSave.Click
        If ddlWH.Items.Count > 1 And ddlWH.SelectedIndex = 0 Then
            lblSubmitResult.Text = "<br>Please choose location."
            lblSubmitResult.CssClass = "error"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "Description invalid", "document.getElementById('" & ddlWH.ClientID & "').focus();", True)
            Exit Sub
        End If
        If rdoItemType.Items(0).Selected = False And rdoItemType.Items(1).Selected = False Then
            lblSubmitResult.Text = "<br>Please choose item type"
            lblSubmitResult.CssClass = "error"
            Exit Sub
        End If
        Dim dtItemList As DataTable = getDataFromGrid()
        Dim rqOQORDERED As Double = 0.0
        Dim irow As Integer = 0
        For Each dr As DataRow In dtItemList.Rows
            
            If dr("ProductDimension") <> "" Then
                Dim ddlConfig As DropDownList = CType(gvItemList.Rows(irow).FindControl("ddlConfig"), DropDownList)
                If ddlConfig.Items.Count > 1 AndAlso ddlConfig.SelectedIndex = 0 Then
                    lblSubmitResult.Text = "<br>Please select product dimension : Config"
                    lblSubmitResult.CssClass = "error"
                    Dim str As New StringBuilder
                    str.AppendLine("document.getElementById('" & ddlConfig.ClientID & "').focus();")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "product_dimensioninvalid", str.ToString, True)
                    Exit Sub
                End If
                Dim ddlSize As DropDownList = CType(gvItemList.Rows(irow).FindControl("ddlSize"), DropDownList)
                If ddlSize.Items.Count > 1 AndAlso ddlSize.SelectedIndex = 0 Then
                    lblSubmitResult.Text = "<br>Please select product dimension : Size"
                    lblSubmitResult.CssClass = "error"
                    Dim str As New StringBuilder
                    str.AppendLine("document.getElementById('" & ddlSize.ClientID & "').focus();")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "product_dimensioninvalid", str.ToString, True)
                    Exit Sub
                End If
                Dim ddlColor As DropDownList = CType(gvItemList.Rows(irow).FindControl("ddlColor"), DropDownList)
                If ddlColor.Items.Count > 1 AndAlso ddlColor.SelectedIndex = 0 Then
                    lblSubmitResult.Text = "<br>Please select product dimension : Color"
                    lblSubmitResult.CssClass = "error"
                    Dim str As New StringBuilder
                    str.AppendLine("document.getElementById('" & ddlColor.ClientID & "').focus();")
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "product_dimensioninvalid", str.ToString, True)
                    Exit Sub
                End If
            End If
            If IsNumeric(dr("OQORDERED")) AndAlso dr("OQORDERED") <> 0 Then
                rqOQORDERED += dr("OQORDERED")
            Else
                lblSubmitResult.Text = "<br>Quantity of selected item can not be zero."
                lblSubmitResult.CssClass = "error"
                Dim txbQty As TextBox = CType(gvItemList.Rows(irow).FindControl("txbQty"), TextBox)
                Dim str As New StringBuilder
                str.AppendLine("document.getElementById('" & txbQty.ClientID & "').focus();")
                str.AppendLine("document.getElementById('" & txbQty.ClientID & "').select();")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "qtyinvalid", str.ToString, True)
                Exit Sub
            End If
            If dr("MANITEMNO") = "" Then
                lblSubmitResult.Text = "<br>Please select CATEGORY"
                lblSubmitResult.CssClass = "error"
                Dim ddlSubSec As DropDownList = CType(gvItemList.Rows(irow).FindControl("ddlSubSection"), DropDownList)
                Dim str As New StringBuilder
                str.AppendLine("document.getElementById('" & ddlSubSec.ClientID & "').focus();")
                'str.AppendLine("document.getElementById('" & ddlSubSec.ClientID & "').select();")
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "SubSecInvalid", str.ToString, True)
                Exit Sub
            End If
            irow += 1
        Next


        'Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        'Dim dtVendor As DataTable
        'irow = 0
        'For Each dr As DataRow In dtItemList.Rows
        '    If dr("VDCODEL") <> "" Then
        '        dtVendor = RequisitionBLL.getVendorByVendorCode(dr("VDCODEL"))
        '        If dtVendor.Rows.Count = 0 Then
        '            lblSubmitResult.Text += "<br>VDCode: " & dr("VDCODEL").ToString.ToUpper & " not found or inactive."
        '            lblSubmitResult.CssClass = "error"
        '            Dim txbQty As TextBox = CType(gvItemList.Rows(irow).Cells(4).FindControl("txbVendCode"), TextBox)
        '            Dim str As New StringBuilder
        '            str.AppendLine("document.getElementById('" & txbQty.ClientID & "').focus();")
        '            str.AppendLine("document.getElementById('" & txbQty.ClientID & "').select();")
        '            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "qtyinvalid", str.ToString, True)
        '            Exit Sub
        '        End If
        '    End If
        '    irow += 1
        'Next

        Dim RequestHOBJ As New RequestHOBJ()
        RequestHOBJ.rqAUDTUSER = Left(Session("USERNAME").ToString, 8)
        RequestHOBJ.rqAUDTORG = Session("DATABASE")
        'If txbExpDate.Text <> "" Then
        '    RequestHOBJ.rqEXPIRATION = txbExpDate.Text.Split("/")(2) & txbExpDate.Text.Split("/")(1) & txbExpDate.Text.Split("/")(0)
        'End If
        If txbRequireDate.Text <> "" Then
            RequestHOBJ.rqEXPARRIVAL = New Date(txbRequireDate.Text.Split("/")(2), txbRequireDate.Text.Split("/")(1), txbRequireDate.Text.Split("/")(0))
        End If
        'RequestHOBJ.rqSTCODE = ddlLocation.SelectedItem.Text
        RequestHOBJ.rqSTDESC = txbLocName.Text
        'RequestHOBJ.rqVDCODE = txbVendorCode.Text
        'RequestHOBJ.rqVDNAME = txbVendorName.Text
        'RequestHOBJ.rqDESCRIPTION = txbRemark.Text.Trim
        RequestHOBJ.rqREFERENCE = txbReference.Text.Trim
        'RequestHOBJ.rqOPTFLD4 = rdoItemType.SelectedItem.Value
        RequestHOBJ.axPRAmount = rdoItemType.SelectedItem.Value
        RequestHOBJ.rqACCPAC = Session("ACCPAC")
        RequestHOBJ.axHOYA_SITE = Session("SITE")
        RequestHOBJ.axHOYA_LOCATION = ddlWH.SelectedItem.Text
        If Session("Action") = "NewRQ" Then
            RequestHOBJ.rqREQUESTBY = Session("FULLNAME")
            RequestHOBJ.rqCOMMENT = Session("SECTION")
        End If
        RequestHOBJ.rqOQORDERED = rqOQORDERED
        RequestHOBJ.PRPreparerEmpCode = Session("PRPreparerEmpCode")
        '================================
        '2012-08-30 PO Domain use default onhold = 1
        '2012-10-12 All factory(except HO) use default onhold = 1
        '--------------------------------
        'If Session("DOMAIN") = "PO" Then
        '    RequestHOBJ.rqONHOLD = 1
        'End If
        'If Session("DATABASE") = "HOPTHO" Then
        '    RequestHOBJ.rqONHOLD = 0
        'Else
        '    RequestHOBJ.rqONHOLD = 1
        'End If
        '================================

        Dim strMessage As String = ""
        Dim RequisitionLBLL As New RequisitionBLL(Session("DATABASE"))

        If Session("Action") = "EditRQ" Then
            RequestHOBJ.rqNHSEQ = Session("RQNHSEQ")
            'strMessage = RequisitionLBLL.UpdateRequisition(RequestHOBJ, dtItemList, txbReqNo.Text)
            strMessage = RequisitionLBLL.UpdateRequisitionAX(RequestHOBJ, dtItemList, txbReqNo.Text)
        ElseIf Session("Action") = "NewRQ" Then
            'strMessage = RequisitionLBLL.NewRequisition(RequestHOBJ, dtItemList)
            strMessage = RequisitionLBLL.NewRequisitionAX(RequestHOBJ, dtItemList, Session("DIMENSIONFINANCIALTAG_FACTORY"))
            Session("POSTNEW") = "go to history page with print prompt."
        End If

        If strMessage.IndexOf("Error") >= 0 Then
            lblSubmitResult.CssClass = "error"
            lblSubmitResult.Text = "<br>" & strMessage
        Else
            Session("RQNNUMBER") = strMessage
            Session("Action") = ""

            Dim RequisitionHistoryBLL As New RequisitionHistoryBLL(Session("DATABASE"))
            Dim DocNoObj As New RequisitionHistoryBLL.DocNoObj
            DocNoObj = RequisitionHistoryBLL.setDocRevision(Session("SITE"), Session("ACCPAC"), "PORQN01.RPT", Choose(RequestHOBJ.axPRAmount + 1, "L5000", "M5000"))
            Session("DOCNO") = DocNoObj.DOCNO
            Session("REVISION") = DocNoObj.REVISION
            Session("EFFECTIVE") = Format(DocNoObj.EFFECTIVE, "dd/MM/yyyy")

            Response.Redirect("RequisitionHistory.aspx")
        End If
    End Sub

    Protected Sub imgApplyAll_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgApplyAll.Click
        If txbRequireDate.Text <> "" Then
            Dim dtItemList As DataTable = getDataFromGrid()
            For Each dr As DataRow In dtItemList.Rows
                dr("EXPARRIVAL") = txbRequireDate.Text 'txbRequireDate.Text.Split("/")(2) & txbRequireDate.Text.Split("/")(1) & txbRequireDate.Text.Split("/")(0)
            Next
            Bind_gvItemList()
        End If
    End Sub

    'Protected Sub ddlCurrency_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCurrency.SelectedIndexChanged
    '    Dim dtItemList As DataTable = getDataFromGrid()
    '    Bind_gvItemList()
    'End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'Session("RQNNUMBER") = ""
        Session("Action") = ""
        Response.Redirect("RequisitionHistory.aspx")
    End Sub

    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Session("Action") = ""
        Response.Redirect("Requisition.aspx")
    End Sub

    Protected Sub imgbtnUpload_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtnUpload.Click
        If FileUpload1.HasFile Then
            If FileUpload1.PostedFile.FileName.IndexOf(".xls") = -1 Then
                lblUpdateMessage.Text = "Importing allow for Excel format only."
                Exit Sub
            End If
            If FileUpload1.PostedFile.ContentLength / 1048576 > 3 Then
                lblUpdateMessage.Text = "File can not larger than 3MB (current " & Format(FileUpload1.PostedFile.ContentLength / 1048576, "#,###.0#") & "MB)"
                Exit Sub
            End If
            'Dim strSharedPath As String = ConfigurationManager.AppSettings("SharedPath")
            Dim TempInput As String = ConfigurationManager.AppSettings("ExcelNetwork_LIVE")
            Dim strFullPath As String = ConfigurationManager.AppSettings("ExcelFullPath_LIVE")
            Dim xlsFile As String = String.Format("{0}({1}-{2}-{3} {4}.{5}).xls", _
                                                        Year(Now), Right("0" & Month(Now), 2), Right("0" & Day(Now), 2), _
                                                        Format(Hour(Now), "00"), Format(Minute(Now), "00"), _
                                                        Session("USERNAME"))
            lblUpdateMessage.Text = ""
            Try
                'TempInput = Server.MapPath("..\" & TempInput) '& xlsFile
                FileUpload1.PostedFile.SaveAs(TempInput & xlsFile)
                Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
                Dim dt As DataTable = RequisitionBLL.getExcel(strFullPath & xlsFile)
                If IO.File.Exists(TempInput & xlsFile) = True Then
                    IO.File.Delete(TempInput & xlsFile)
                End If
                'Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
                If dt.Rows.Count > 0 Then
                    Dim blError As Boolean = False
                    Dim dtResult As New DataTable("dtResult")
                    Dim drResult As DataRow
                    'dtResult.Columns.Add("Item Number")
                    dtResult = dt.Clone
                    'For Each dc As DataColumn In dt.Columns
                    '    dtResult.Columns.Add(dc)
                    'Next
                    dtResult.Columns.Add("success")
                    dtResult.Columns.Add("error")
                    dtResult.Columns.Add("result")
                    For Each dr As DataRow In dt.Rows
                        drResult = dtResult.NewRow
                        drResult("Item Number") = dr("Item Number")
                        drResult("Detail") = dr("Detail")
                        drResult("Qty Order") = dr("Qty Order")
                        drResult("Section") = dr("Section")
                        drResult("Date Require") = dr("Date Require")
                        If dr("Item Number") <> "" AndAlso AddItem(dr("Item Number"), dr("Detail"), dr("Qty Order"), dr("Section"), dr("Date Require")) = "" Then
                            drResult("success") = "true"
                            drResult("error") = "false"
                        Else
                            drResult("result") = "Item not found"
                            drResult("success") = "false"
                            drResult("error") = "true"
                            blError = True
                        End If
                        dtResult.Rows.Add(drResult)
                    Next

                    gvItemError.DataSource = dtResult
                    gvItemError.DataBind()
                    If blError Then
                        divgvItemError.Visible = True
                        gvItemError.Visible = True
                    End If
                End If
            Catch ex As Exception
                lblUpdateMessage.Text = "Error: Unable to get data from your excel. Please download our recommended excel format.<br>" & ex.Message
            Finally

            End Try
        End If
    End Sub
End Class

'Public Class GridViewTemplate
'    Implements ITemplate

'    Public Sub InstantiateIn(container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn

'    End Sub
'End Class
