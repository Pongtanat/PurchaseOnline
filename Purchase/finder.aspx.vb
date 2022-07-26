Imports System.data
Partial Class finder
    Inherits System.Web.UI.Page
#Region "Events Handlers"
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'EnableViewState = False
        If Session("USERNAME") = "" Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "close_win", "window.close();", True)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("USERNAME") = "" Then
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "close_win", "window.close();", True)
            Response.Redirect("~/Purchase/finder.aspx")
        End If

        lblName.Text = Request.QueryString("name")
        'litSearch.Text = Request.QueryString("name")

        If Not IsPostBack Then
            'Bind_gvItemList()
            'ScriptManager1.SetFocus(txbFind)
            imgProductDimensionHid.Attributes.Add("onmouseover", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/left_h.png';"))
            imgProductDimensionHid.Attributes.Add("onmouseout", "this.src='" & VirtualPathUtility.MakeRelative(Page.AppRelativeVirtualPath, "~/image/left.png';"))
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Bind_gvItemList()
    End Sub

    Protected Sub gvList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvList.RowCommand
        Select Case e.CommandName
            Case "DoubleClick" : gvList.SelectedIndex = e.CommandArgument
            Case "ProductDimension"
                gvList.Visible = False
                Bind_gvProductDimension(e.CommandArgument)
                Dim imggvProductDimension As ImageButton = CType(e.CommandSource, ImageButton)
                Dim gvRow As GridViewRow = imggvProductDimension.NamingContainer
                Dim rowID As Integer = gvRow.RowIndex
                lblItemNumber.Text = gvList.Rows(rowID).Cells(1).Text
                lblItemDesc.Text = gvList.Rows(rowID).Cells(2).Text
                hidItemRecId.Value = CType(gvList.Rows(rowID).FindControl("hidgvItemRecId"), HiddenField).Value
                hidProductDimension.Value = CType(gvList.Rows(rowID).FindControl("hidgvProductDimension"), HiddenField).Value
                divProductDimension.Visible = True
        End Select
    End Sub

    Protected Sub gvList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim hidgvProductDimension As HiddenField = CType(e.Row.FindControl("hidgvProductDimension"), HiddenField)
            If hidgvProductDimension.Value <> "" Then
                CType(e.Row.FindControl("imggvProductDimension"), ImageButton).Visible = True
            End If

            'Dim gvButton As LinkButton = CType(e.Row.Cells(0).Controls(0), LinkButton)
            Dim postBackReference As String
            If lblName.Text = "ITEMCD" Then
                postBackReference = "SetValue('" & Request.QueryString("cont") & "','" & e.Row.Cells(2).Text.Replace("'", "quot;") & "','" & Request.QueryString("unused") & "')"
            Else
                postBackReference = "SetValue('" & Request.QueryString("cont") & "','" & e.Row.Cells(1).Text & "','" & Request.QueryString("unused") & "')"
            End If

            e.Row.Attributes.Add("ondblclick", postBackReference)
        End If
    End Sub

    Protected Sub gvProductDimension_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProductDimension.RowCreated
        Dim dtProductDimension As DataTable = Session("dtProductDimension")
        If e.Row.RowType = DataControlRowType.DataRow Then
            For iCol As Integer = gvProductDimension.Columns.Count To dtProductDimension.Columns.Count - 1
                Dim txt As New TextBox
                txt.ID = dtProductDimension.Columns(iCol).ColumnName
                txt.Width = 50
                txt.Style("text-align") = "right"
                txt.Attributes.Add("onkeypress", "return validateinput(this.id,event);")
                txt.Attributes.Add("oncontextmenu", "return false")
                txt.Attributes.Add("onpaste", "return false")
                txt.Attributes.Add("oncopy", "return false")
                txt.Attributes.Add("oncut", "return false")
                txt.Attributes.Add("autocomplete", "off")
                Dim tdc1 As New TableCell
                tdc1.Controls.Add(txt)
                e.Row.Cells.Add(tdc1)
            Next
        ElseIf e.Row.RowType = DataControlRowType.Header Then
            For iCol As Integer = gvProductDimension.Columns.Count To dtProductDimension.Columns.Count - 1
                Dim lit As New Literal
                lit.ID = dtProductDimension.Columns(iCol).ColumnName
                lit.Text = dtProductDimension.Columns(iCol).ColumnName
                Dim thc1 As New TableHeaderCell
                thc1.Controls.Add(lit)
                e.Row.Cells.Add(thc1)
            Next
        End If
    End Sub

    Protected Sub gvProductDimension_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvProductDimension.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

        End If
    End Sub

    Protected Sub txbFind_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txbFind.TextChanged
        Bind_gvItemList()
    End Sub

    Protected Sub imgProductDimensionHid_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgProductDimensionHid.Click
        divProductDimension.Visible = False
        gvList.Visible = True
    End Sub

    Protected Sub btnAdd_Click(sender As Object, e As System.EventArgs) Handles btnAdd.Click
        getDataFromGrid()
        '...
        Dim postBackReference As New StringBuilder
        postBackReference.AppendLine("if (window.opener.document.getElementById('ctl00_ContentPlaceHolder1_imgbtnAddMultipleItems')) {")
        postBackReference.AppendLine("window.opener.document.getElementById('ctl00_ContentPlaceHolder1_imgbtnAddMultipleItems').click();")
        postBackReference.AppendLine("} else { window.close(); }")

        ScriptManager.RegisterClientScriptBlock(Page, GetType(Page), "addMultipleItems", postBackReference.ToString, True)
        '...
        divProductDimension.Visible = False
        gvList.Visible = True

    End Sub

#End Region

#Region "Method"

    Protected Sub Bind_gvItemList()
        Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        Dim dt As DataTable
        'Dim bField As New BoundField
        'Dim btnField As New ButtonField
        If ddlFind.SelectedValue = "Item" Then
            'dt = RequisitionBLL.getItemByItemCode(txbFind.Text, 10000)
            'dt = RequisitionBLL.getItemByItemCodeAX(txbFind.Text, Session("FilterItemCode"), Session("DOMAIN"), 10000)
            dt = RequisitionBLL.getItemByItemCodeAX(txbFind.Text, Session("FilterItemCode"), Session("CategorySearchText"), 10000)
        Else
            'dt = RequisitionBLL.getItemByItemDesc(txbFind.Text)
            'dt = RequisitionBLL.getItemByItemDescAX(txbFind.Text, Session("FilterItemCode"), Session("DOMAIN"))
            dt = RequisitionBLL.getItemByItemDescAX(txbFind.Text, Session("FilterItemCode"), Session("CategorySearchText"))
        End If
        lblDesc.Text = dt.Rows.Count & " item(s) found."
        gvList.DataSource = dt
        gvList.DataBind()
    End Sub

    Protected Sub Bind_gvProductDimension(ByVal strItemCode As String)
        'gvProductDimension.Columns.Clear()
        Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        Dim dtConfig As DataTable = RequisitionBLL.getConfigByItemRecIdAX(strItemCode)
        Dim dtSize As DataTable = RequisitionBLL.getSizeByItemRecIdAX(strItemCode)
        Dim dtColor As DataTable = RequisitionBLL.getColorByItemRecIdAX(strItemCode)
        Dim dtProductDimension As New DataTable
        dtProductDimension.Columns.Add("Color")
        dtProductDimension.Columns.Add("Size")

        'Dim bFieldColor As New BoundField
        'bFieldColor.DataField = dtColor.Columns(0).ColumnName
        'bFieldColor.HeaderText = dtColor.Columns(0).ColumnName
        'gvProductDimension.Columns.Add(bFieldColor)

        'Dim bFieldSize As New BoundField
        'bFieldSize.DataField = dtSize.Columns(0).ColumnName
        'bFieldSize.HeaderText = dtSize.Columns(0).ColumnName
        'gvProductDimension.Columns.Add(bFieldSize)
        If dtConfig.Rows.Count = 0 Then
            Dim drNewConfig As DataRow
            drNewConfig = dtConfig.NewRow
            drNewConfig("Config") = "Qty"
            dtConfig.Rows.Add(drNewConfig)
        End If
        For Each drConfig As DataRow In dtConfig.Rows
            'Dim tField As New TemplateField
            'tField.HeaderTemplate = New CreateItemTemplate(ListItemType.Header, drConfig(0), drConfig(0))
            'tField.ItemTemplate = New CreateItemTemplate(ListItemType.Item, drConfig(0), GetType(TextBox).ToString)
            dtProductDimension.Columns.Add(drConfig(0))
            'gvProductDimension.Columns.Add(tField)
        Next

        Dim dr As DataRow
        'Dim drN As Integer = 0
        'Do
        '    For Each drSize As DataRow In dtSize.Rows
        '        dr = dtProductDimension.NewRow()
        '        dr("Color") = IIf(dtColor.Rows.Count > 0, dtColor.Rows(drN)("Color"), "") 'drColor("Color")
        '        dr("Size") = drSize("Size")
        '        For Each drConfig As DataRow In dtConfig.Rows
        '            dr(drConfig(0)) = drConfig(0)
        '        Next
        '        dtProductDimension.Rows.Add(dr)
        '    Next
        '    drN += 1
        'Loop While drN < dtColor.Rows.Count

        Dim drNColor As Integer = 0
        Dim drNSize As Integer = 0
        Do
            Do
                dr = dtProductDimension.NewRow()
                If dtColor.Rows.Count > 0 Then : dr("Color") = dtColor.Rows(drNColor)("Color") 'drColor("Color")
                Else : dr("Color") = ""
                End If
                If dtSize.Rows.Count > 0 Then : dr("Size") = dtSize.Rows(drNSize)("Size")
                Else : dr("Size") = ""
                End If
                For Each drConfig As DataRow In dtConfig.Rows
                    dr(drConfig(0)) = drConfig(0)
                Next
                dtProductDimension.Rows.Add(dr)
                drNSize += 1
            Loop While drNSize < dtSize.Rows.Count
            drNSize = 0
            drNColor += 1
        Loop While drNColor < dtColor.Rows.Count

        Session("dtProductDimension") = dtProductDimension
        gvProductDimension.DataSource = dtProductDimension
        gvProductDimension.DataBind()
    End Sub

    Protected Function getDataFromGrid() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("ItemId")
        dt.Columns.Add("ItemRecId")
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
        Dim strReqDate As String = ""
        Dim RequisitionBLL As New RequisitionBLL(Session("DATABASE"))
        Dim dtItemDesc As DataTable = RequisitionBLL.getItemByItemCodeAX(lblItemNumber.Text, Session("FilterItemCode"), Session("CategorySearchText")) 'RequisitionBLL.getItemByItemCodeAX(lblItemNumber.Text, Session("FilterItemCode"), Session("DOMAIN"))
        Dim dr As DataRow
        Dim iConfigFrom As Integer = gvProductDimension.Columns.Count
        For Each gvrow As GridViewRow In gvProductDimension.Rows
            'If gvProductDimension.Columns.Count >= 2 Then
            'If iConfigFrom = 0 AndAlso gvrow.RowIndex = 0 Then
            '    For iCol As Integer = 1 To gvrow.Cells.Count - 1
            '        If gvrow.Cells(iCol).FindControl(gvProductDimension.HeaderRow.Cells(iCol).Text) IsNot Nothing Then
            '            If gvrow.Cells(iCol).Controls(0).GetType Is GetType(TextBox) Then
            '                iConfigFrom = iCol
            '                Exit For
            '            End If
            '        End If
            '    Next
            'End If
            'End If
            For cConfig As Integer = iConfigFrom To gvrow.Cells.Count - 1
                If gvrow.Cells(cConfig).Controls(0).GetType Is GetType(TextBox) AndAlso _
                   IsNumeric(CType(gvrow.Cells(cConfig).FindControl(CType(gvProductDimension.HeaderRow.Cells(cConfig).Controls(0), Literal).Text), TextBox).Text) Then
                    dr = dt.NewRow
                    dr("ItemId") = lblItemNumber.Text 'gvrow.Cells(0).Text
                    dr("ItemRecId") = hidItemRecId.Value 'CType(gvrow.FindControl("hidItemRecId"), HiddenField).Value
                    dr("ProductDimension") = hidProductDimension.Value
                    dr("Config") = CType(gvProductDimension.HeaderRow.Cells(cConfig).Controls(0), Literal).Text 'CType(gvrow.FindControl("ddlConfig"), DropDownList).Text
                    dr("Size") = gvrow.Cells(1).Text 'CType(gvrow.FindControl("ddlSize"), DropDownList).Text
                    dr("Color") = gvrow.Cells(0).Text 'CType(gvrow.FindControl("ddlColor"), DropDownList).Text
                    dr("Name") = lblItemDesc.Text 'gvrow.Cells(1).ToolTip
                    dr("ITEMCOMMENT") = "" 'CType(gvrow.FindControl("txbDetail"), TextBox).Text
                    dr("LINENUM") = "" 'CType(gvrow.FindControl("hidLineNum"), HiddenField).Value
                    dr("RECID") = "" 'CType(gvrow.FindControl("hidLineRecID"), HiddenField).Value
                    dr("OQORDERED") = CType(gvrow.Cells(cConfig).FindControl(CType(gvProductDimension.HeaderRow.Cells(cConfig).Controls(0), Literal).Text), TextBox).Text 'CType(gvrow.FindControl("txbQty"), TextBox).Text
                    Dim dtItemCost As DataTable = RequisitionBLL.getLastPriceByItemCodeAX(lblItemNumber.Text)
                    If dtItemCost.Rows.Count > 0 Then
                        dr("ORDERUNIT") = dtItemCost.Rows(0)("UNITID") 'gvrow.Cells(4).Text
                        dr("ITEMCOST") = dtItemCost.Rows(0)("PRICE") 'CType(gvrow.FindControl("hidItemCost"), HiddenField).Value
                    End If
                    dr("MANITEMNO") = Session("SECTION") 'CType(gvrow.FindControl("ddlSection"), DropDownList).Text
                    dr("EXPARRIVAL") = 0
                    'strReqDate = CType(gvrow.FindControl("txbReqDate"), TextBox).Text
                    'If strReqDate <> "" Then
                    '    dr("EXPARRIVAL") = strReqDate
                    'End If
                    dr("DTCOMPLETEL") = 0
                    dr("INSTRUCTCOMMENT") = "" 'CType(gvrow.FindControl("lblRemark"), Label).Text
                    dr("CURRENCY") = "THB" 'CType(gvrow.FindControl("hidCurr"), HiddenField).Value
                    dt.Rows.Add(dr)
                End If

            Next
        Next
        Session("gvProductDimension") = dt
        Return dt
    End Function

#End Region

End Class

Class CreateItemTemplate
    Implements ITemplate

#Region "data memebers"

    Private ItemType As ListItemType
    Private FieldName As String
    Private InfoType As String

#End Region

#Region "constructor"

    Public Sub New(ByVal item_type As ListItemType, ByVal field_name As String, ByVal info_type As String)
        ItemType = item_type
        FieldName = field_name
        InfoType = info_type
    End Sub

#End Region

#Region "Methods"

    Public Sub InstantiateIn(container As System.Web.UI.Control) Implements System.Web.UI.ITemplate.InstantiateIn
        Select Case ItemType
            Case ListItemType.Header
                Dim header_ltrl As New Literal()
                header_ltrl.Text = "<b>" & FieldName & "</b>"
                container.Controls.Add(header_ltrl)
                Exit Select
            Case ListItemType.Item
                Select Case InfoType
                    Case "Command"
                        Dim edit_button As New ImageButton()
                        edit_button.ID = "edit_button"
                        edit_button.ImageUrl = "~/images/edit.gif"
                        edit_button.CommandName = "Edit"
                        AddHandler edit_button.Click, AddressOf edit_button_Click
                        edit_button.ToolTip = "Edit"
                        container.Controls.Add(edit_button)

                        Dim delete_button As New ImageButton()
                        delete_button.ID = "delete_button"
                        delete_button.ImageUrl = "~/images/delete.gif"
                        delete_button.CommandName = "Delete"
                        delete_button.ToolTip = "Delete"
                        delete_button.OnClientClick = "return confirm('Are you sure to delete the record?')"
                        container.Controls.Add(delete_button)

                        ' Similarly add button for insert. 
                        ' * It is important to know when 'insert' button is added 
                        ' * its CommandName is set to "Edit" like that of 'edit' button 
                        ' * only because we want the GridView enter into Edit mode, 
                        ' * and this time we also want the text boxes for corresponding fields empty 

                        Dim insert_button As New ImageButton()
                        insert_button.ID = "insert_button"
                        insert_button.ImageUrl = "~/images/insert.bmp"
                        insert_button.CommandName = "Edit"
                        insert_button.ToolTip = "Insert"
                        AddHandler insert_button.Click, AddressOf insert_button_Click
                        container.Controls.Add(insert_button)

                        Exit Select
                    Case GetType(TextBox).ToString
                        Dim field_txtbox As New TextBox()
                        field_txtbox.ID = FieldName
                        field_txtbox.Text = [String].Empty
                        field_txtbox.Width = 50
                        field_txtbox.Style("text-align") = "right"
                        field_txtbox.Attributes.Add("onkeypress", "return validateinput(this.id,event);")
                        If CInt(New Page().Session("InsertFlag")) = 0 Then
                            AddHandler field_txtbox.DataBinding, AddressOf OnDataBinding
                        End If
                        container.Controls.Add(field_txtbox)

                        Dim field_hidfld As New HiddenField()
                        field_hidfld.ID = FieldName
                        field_hidfld.Value = [String].Empty
                        If CInt(New Page().Session("InsertFlag")) = 0 Then
                            AddHandler field_hidfld.DataBinding, AddressOf OnDataBinding
                        End If
                        container.Controls.Add(field_hidfld)
                    Case Else

                        Dim field_lbl As New Label()
                        field_lbl.ID = FieldName
                        field_lbl.Text = [String].Empty
                        'we will bind it later through 'OnDataBinding' event 
                        AddHandler field_lbl.DataBinding, AddressOf OnDataBinding
                        container.Controls.Add(field_lbl)
                        Exit Select

                End Select
                Exit Select
            Case ListItemType.EditItem
                If InfoType = "Command" Then
                    Dim update_button As New ImageButton()
                    update_button.ID = "update_button"
                    update_button.CommandName = "Update"
                    update_button.ImageUrl = "~/images/update.gif"
                    If CInt(New Page().Session("InsertFlag")) = 1 Then
                        update_button.ToolTip = "Add"
                    Else
                        update_button.ToolTip = "Update"
                    End If
                    update_button.OnClientClick = "return confirm('Are you sure to update the record?')"
                    container.Controls.Add(update_button)

                    Dim cancel_button As New ImageButton()
                    cancel_button.ImageUrl = "~/images/cancel.gif"
                    cancel_button.ID = "cancel_button"
                    cancel_button.CommandName = "Cancel"
                    cancel_button.ToolTip = "Cancel"

                    container.Controls.Add(cancel_button)
                Else
                    ' for other 'non-command' i.e. the key and non key fields, bind textboxes with corresponding field values 
                    Dim field_txtbox As New TextBox()
                    field_txtbox.ID = FieldName
                    field_txtbox.Text = [String].Empty
                    ' if Inert is intended no need to bind it with text..keep them empty 
                    If CInt(New Page().Session("InsertFlag")) = 0 Then
                        AddHandler field_txtbox.DataBinding, AddressOf OnDataBinding
                    End If

                    container.Controls.Add(field_txtbox)
                End If
                Exit Select


        End Select
    End Sub
#End Region

#Region "Event Handlers"

    'just sets the insert flag ON so that we ll be able to decide in OnRowUpdating event whether to insert or update 
    Protected Sub insert_button_Click(ByVal sender As [Object], ByVal e As EventArgs)
        HttpContext.Current.Session("InsertFlag") = 1
    End Sub
    'just sets the insert flag OFF so that we ll be able to decide in OnRowUpdating event whether to insert or update 
    Protected Sub edit_button_Click(ByVal sender As [Object], ByVal e As EventArgs)
        HttpContext.Current.Session("InsertFlag") = 0
    End Sub

    Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)

        Dim bound_value_obj As Object = Nothing
        Dim ctrl As Control = DirectCast(sender, Control)
        Dim data_item_container As IDataItemContainer = DirectCast(ctrl.NamingContainer, IDataItemContainer)
        bound_value_obj = DataBinder.Eval(data_item_container.DataItem, FieldName)

        Select Case ItemType
            Case ListItemType.Item
                Select Case sender.GetType
                    Case GetType(Label)
                        Dim field_ltrl As Label = DirectCast(sender, Label)
                        field_ltrl.Text = bound_value_obj.ToString()
                        Exit Select
                    Case GetType(TextBox)
                        Dim field_ltrl As TextBox = DirectCast(sender, TextBox)
                        field_ltrl.Text = "" 'bound_value_obj.ToString()
                        Exit Select
                    Case GetType(HiddenField)
                        Dim field_ltrl As HiddenField = DirectCast(sender, HiddenField)
                        field_ltrl.Value = bound_value_obj.ToString()
                        Exit Select
                End Select

                Exit Select
            Case ListItemType.EditItem
                Dim field_txtbox As TextBox = DirectCast(sender, TextBox)
                field_txtbox.Text = bound_value_obj.ToString()

                Exit Select


        End Select
    End Sub

#End Region
End Class