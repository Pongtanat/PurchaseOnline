<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RequisitionHistory.aspx.vb" Inherits="Purchase_RequisitionHistory" title="Requisition History" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--<img src="../image/black_ribbon_bottom_left.png" style="position:fixed;z-index: 9999;width:70px;left:0;bottom:0;" alt="black-ribbon"/>--%>

    <asp:DropDownList ID="ddlHistoryBy" runat="server" AutoPostBack="true"></asp:DropDownList>
    <asp:Label ID="lblMessage" CssClass="red" runat="server" ></asp:Label>
    <ajaxToolkit:Accordion ID="Accordion1" runat="server" SelectedIndex="1" 
     AutoSize="None" FadeTransitions="true" TransitionDuration="250" FramesPerSecond="40"
     RequireOpenedPane="false" SuppressHeaderPostbacks="true">
    <Panes>
        <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
            <Header><span style="cursor:pointer">+ Show Advance Filter</span></Header>
            <Content>
                <table>
                    <%--<tr>
                        <td>Section</td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlSection" runat="server"></asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>Section</td>
                        <td colspan="3"><asp:TextBox ID="txbSection" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Requisition NO.</td>
                        <td><asp:TextBox ID="txbRQ1" runat="server" Width="100" AutoPostBack="true"></asp:TextBox></td>
                        <td>To</td>
                        <td><asp:TextBox ID="txbRQ2" runat="server" Width="100" AutoPostBack="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Vendor Contains</td>
                        <td colspan="3"><asp:TextBox ID="txbVendor" runat="server" AutoPostBack="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Post Date</td>
                        <td><asp:TextBox ID="txbDate1" runat="server" Width="100" AutoPostBack="true"></asp:TextBox>
                        <asp:Image ID="imgCalendar1" runat="server" ImageUrl="~/image/calendar.png" CssClass="pointer"/>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                         PopupButtonID="imgCalendar1" TargetControlID="txbDate1">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskDate1" runat="server" ClearTextOnInvalid="true"
                         TargetControlID="txbDate1" CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:MaskedEditValidator ID="MaskVDate1" runat="server"
                         ControlExtender="MaskDate1" ControlToValidate="txbDate1">
                        </ajaxToolkit:MaskedEditValidator>
                        </td>
                        <td>To</td>
                        <td><asp:TextBox ID="txbDate2" runat="server" Width="100" AutoPostBack="true"></asp:TextBox>
                        <asp:Image ID="imgCalendar2" runat="server" ImageUrl="~/image/calendar.png" CssClass="pointer"/>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                         PopupButtonID="imgCalendar2" TargetControlID="txbDate2">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:MaskedEditExtender ID="MaskDate2" runat="server" ClearTextOnInvalid="true"
                         TargetControlID="txbDate2" CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                        </ajaxToolkit:MaskedEditExtender>
                        <ajaxToolkit:MaskedEditValidator ID="MaskVDate2" runat="server"
                         ControlExtender="MaskDate2" ControlToValidate="txbDate2">
                        </ajaxToolkit:MaskedEditValidator>&nbsp&nbsp&nbsp&nbsp
                        <asp:Button ID="btnExport" runat="server" Text="Export" />
                        </td>
                    </tr>
                </table>
            </Content>
        </ajaxToolkit:AccordionPane>
    </Panes>
    </ajaxToolkit:Accordion>
    <asp:Panel ID="Panel1" runat="server" style="z-index:100"></asp:Panel>
    <asp:GridView ID="gvHistAX" runat="server" AutoGenerateColumns="false"
     AllowPaging="true" PageSize="15" pagerSettings-Mode="NumericFirstLast">
     <HeaderStyle HorizontalAlign="Center" />
     <EmptyDataRowStyle CssClass="RowStyle center" />
     <EmptyDataTemplate>
        <table width="600px"><tr><td>There are no any record.</td></tr></table>
     </EmptyDataTemplate>
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:ImageButton ID="imgSelect" CommandName="Select" CommandArgument='<%# String.Format("{0},{1}",Eval("PurchReqId"),Eval("ECL_PRAMOUNT")) %>'
                     ImageUrl="~/image/find.ico" ToolTip="View" Width="16" runat="server" CausesValidation="false" />
                    <asp:HiddenField ID="hidPurchReqId" runat="server" Value='<%# Eval("PurchReqId") %>' />
                    
                    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" 
                       PopupControlID="Panel1" 
                       TargetControlID="gvHistAX" 
                       DynamicContextKey='<%# Eval("PurchReqId") %>' 
                       DynamicControlID="Panel1" Position="Right"
                       DynamicServicePath="../GetDynamicContent.asmx"
                       DynamicServiceMethod="GetDynamicContentAX"> 
                    </ajaxToolkit:PopupControlExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Panel ID="popupMenu" runat="server" CssClass="popupMenuStyle" style="display:none;">
                        <table border="0" cellpadding="1" cellspacing="1">
                            <tr class="RowStyle"><td><img src="../image/pack.ico" alt="" />
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Print1"
                                 CommandArgument='<%# String.Format("{0},{1}",Eval("PurchReqId"),Eval("ECL_PRAMOUNT")) %>'
                                 Text='Purchase Requisition' />
                            </td></tr>
                            <tr class="RowStyle"><td><img src="../image/gear.ico" alt="" />
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Print2"
                                 CommandArgument='<%# String.Format("{0},{1}",Eval("PurchReqId"),Eval("ECL_PRAMOUNT")) %>'
                                 Text='Making Production Equipment' />
                            </td></tr>
                            <tr class="RowStyle"><td><img src="../image/conf.ico" alt="" />
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Print3"
                                 CommandArgument='<%# String.Format("{0},{1}",Eval("PurchReqId"),Eval("ECL_PRAMOUNT")) %>'
                                 Text='Contruction, Making, Repairing' />
                            </td></tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="PanelPrint" runat="server">
                    <asp:ImageButton ID="imgReport" CommandName="Preview" CommandArgument='<%# String.Format("{0},{1}",Eval("PurchReqId"),Eval("ECL_PRAMOUNT")) %>'
                     ImageUrl="~/image/print.ico" ToolTip="Print" Width="16" runat="server" CausesValidation="false"/>
                    </asp:Panel>
                    <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" 
                     PopupControlID="popupMenu"
                     TargetControlID="PanelPrint"
                     HoverCssClass="popupHoverStyle"
                     PopupPosition="Left"
                     OffsetX="0"
                     OffsetY="0"
                     PopDelay="0">
                    </ajaxToolkit:HoverMenuExtender>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ImageButton ID="imgCopy" CommandName="Copy" CommandArgument='<%# Eval("PurchReqId") %>'
                     ImageUrl="~/image/doc_copy.ico" ToolTip="Copy" Width="16" runat="server" CausesValidation="false"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="PurchReqId" HeaderText="PR No." ReadOnly="true" HeaderStyle-Width="100" />
            <asp:BoundField DataField="PurchReqName" HeaderText="Section-Preparer" ReadOnly="true" HeaderStyle-Width="250" />
            <asp:BoundField DataField="createdDateTime" HeaderText="Created Date" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField DataField="ECL_PRDOCUMENTAPPROVEDDATE" HeaderText="Approved Date" />
            <asp:BoundField DataField="ECL_PRDOCUMENTRECEIPTDATE" HeaderText="Receipt Date" />--%>
            <asp:TemplateField HeaderText="Approved Date" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:Label ID="lblAPVDATE" runat="server" CssClass="center"
                     Text='<%# Eval("ECL_PRDOCUMENTAPPROVEDDATE")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txbAPVDate" runat="server" Width="75"
                     Text='<%# Eval("ECL_PRDOCUMENTAPPROVEDDATE")%>'></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                     PopupButtonID="txbAPVDate" PopupPosition="BottomRight" TargetControlID="txbAPVDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskDate1" runat="server" ClearTextOnInvalid="true"
                     TargetControlID="txbAPVDate" CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:MaskedEditValidator ID="MaskVDate1" runat="server"
                     ControlExtender="MaskDate1" ControlToValidate="txbAPVDate">
                    </ajaxToolkit:MaskedEditValidator>
                    <asp:RequiredFieldValidator ID="rqfAPVDate" runat="server"
                     ControlToValidate="txbAPVDate" Display="None" SetFocusOnError="true"
                     ErrorMessage="PR Approved Date could not blank"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Receipt Date" ItemStyle-HorizontalAlign="center">
                <ItemTemplate>
                    <asp:Label ID="lblPRRCPDate" runat="server" CssClass="center"
                     Text='<%# Eval("ECL_PRDOCUMENTRECEIPTDATE") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lblPRRCPDate" runat="server" CssClass="center"
                     Text='<%# Eval("ECL_PRDOCUMENTRECEIPTDATE") %>' Visible="false"></asp:Label>
                    <asp:TextBox ID="txbPRRCPDate" runat="server" Width="75"
                     Text='<%# Eval("ECL_PRDOCUMENTRECEIPTDATE") %>'></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                     PopupButtonID="txbPRRCPDate" PopupPosition="BottomRight" TargetControlID="txbPRRCPDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:MaskedEditExtender ID="MaskDate2" runat="server" ClearTextOnInvalid="true"
                     TargetControlID="txbPRRCPDate" CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                    </ajaxToolkit:MaskedEditExtender>
                    <ajaxToolkit:MaskedEditValidator ID="MaskVDate2" runat="server"
                     ControlExtender="MaskDate2" ControlToValidate="txbPRRCPDate">
                    </ajaxToolkit:MaskedEditValidator>
                    <asp:RequiredFieldValidator ID="rqfPRRCPDate" runat="server"
                     ControlToValidate="txbPRRCPDate" Display="None" SetFocusOnError="true"
                     ErrorMessage="PR Received Date could not blank"></asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <%--<asp:BoundField DataField="SubmittedDateTime" HeaderText="Submitted Date" ReadOnly="true" ItemStyle-HorizontalAlign="center" />--%>
            <asp:TemplateField HeaderText="Submitted Date" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="imgSubmit" CommandName="Submit" ToolTip="Submit" Width="16" runat="server"
                     OnClientClick='return confirm("Submit PR No.?")' CommandArgument='<%# Eval("PurchReqId") %>' ImageUrl="~/image/submit.png"/>
                    <asp:Label ID="lblSubmittedDatetime" runat="server" Text='<%# Eval("SubmittedDateTime") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RequisitionStatus" HeaderText="Status" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="Delete" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="imgEdit" CommandName="Edit" ToolTip="Edit" Width="16" runat="server"
                     CommandArgument='<%# Eval("PurchReqId") %>' Visible="false" ImageUrl="~/image/edit.ico"/>
                    <asp:ImageButton ID="imgDelete" runat="server" CommandArgument='<%# Eval("PurchReqId") %>'
                     CommandName="Delete" ImageUrl="~/image/delete.ico" 
                     OnClientClick="return confirm('sure?')"
                     ToolTip="Delete PR" Width="16" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:ImageButton ID="imgEdit" CommandName="EditRQ" Width="16" runat="server"
                     CausesValidation="false"
                     CommandArgument='<%# Eval("PurchReqId") %>' ImageUrl="~/image/edit.ico"
                     ToolTip="Go to edit this requisition detail" Visible="false"/>
                    <asp:ImageButton ID="imgApply" CommandName="Update" ToolTip="Save" runat="server"
                     CausesValidation="true"
                     CommandArgument='<%# Eval("PurchReqId") %>' ImageUrl="~/image/apply.ico" Width="16"/>
                    <asp:ImageButton ID="imgCancel" CommandName="Cancel" ToolTip="Cancel Changed" runat="server" CausesValidation="false"
                     CommandArgument='<%# Eval("PurchReqId") %>' ImageUrl="~/image/cancel.ico" Width="16"/>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <table width="100%" border="0" cellpadding="0" cellspacing="0"><tr><td>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" Height="50px" Width="350px" BackColor="white"/>
    </td></tr></table>
<script type="text/javascript">
var divName = '<%=Panel1.ClientID %>'; // div that is to follow the mouse
                       // (must be position:absolute)
var offX = 15;          // X offset from mouse position
var offY = 15;          // Y offset from mouse position

function mouseX(evt) {if (!evt) evt = window.event; if (evt.pageX) return evt.pageX; else if (evt.clientX)return evt.clientX + (document.documentElement.scrollLeft ?  document.documentElement.scrollLeft : document.body.scrollLeft); else return 0;}
function mouseY(evt) {if (!evt) evt = window.event; if (evt.pageY) return evt.pageY; else if (evt.clientY)return evt.clientY + (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop); else return 0;}

function follow(evt) {if (document.getElementById) {var obj = document.getElementById(divName).style; obj.visibility = 'visible';
obj.left = (parseInt(mouseX(evt))+offX) + 'px';
obj.top = (parseInt(mouseY(evt))+offY) + 'px';}}
document.onmousemove = follow;
</script>

</asp:Content>

