<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Requisition.aspx.vb" Inherits="Purchase_Requisition" title="P/O Requisition Entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<img src="../image/black_ribbon_bottom_left.png" style="position:fixed;z-index: 9999;width:70px;left:0;bottom:0;" alt="black-ribbon"/>--%>
<table border="0" cellpadding="1" cellspacing="0" class="light">
<tr><td class="light">
    <table border="0" cellpadding="1" cellspacing="0">
        <tr>
            <td colspan="4" align="center"><h4>Requisition Order</h4></td>
        </tr>
        <tr>
            <td nowrap style="width:120px" align="right">Requisition No. :</td>
            <td style="width:180px"><asp:TextBox ID="txbReqNo" runat="server" Text="Auto Generate"
             CssClass="watermarked" ReadOnly="true"></asp:TextBox>
            </td>
            <td style="width:120px" align="right">Issue Date :</td>
            <td style="width:260px;"><asp:Label ID="lblIssueDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">Requester Name :</td>
            <td><asp:Label ID="lblReqName" runat="server"></asp:Label></td>
            <td align="right">Section :</td>
            <td><asp:Label ID="lblReqSec" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right">Item Type :</td>
            <td><asp:RadioButtonList ID="rdoItemType" runat="server">
                <asp:ListItem Value="0">Less than 5,000 THB</asp:ListItem>
                <asp:ListItem Value="1">More than 5,000 THB</asp:ListItem>
            </asp:RadioButtonList></td>
            <td align="right" style="vertical-align:top;">Date Require :</td>
            <td style="vertical-align:top;"><asp:TextBox ID="txbRequireDate" runat="server" Width="90" ToolTip="dd/MM/yyyy"></asp:TextBox>
                <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/image/calendar.png"
                 CssClass="pointer"/>
                <asp:ImageButton ID="imgApplyAll" runat="server" ImageUrl="~/image/Option.ico"
                 ToolTip="Apply all" />
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                 PopupButtonID="imgCalendar" TargetControlID="txbRequireDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskDate1" runat="server" ClearTextOnInvalid="true"
                 TargetControlID="txbRequireDate" CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditValidator ID="MaskVDate1" runat="server"
                 ControlExtender="MaskDate1" ControlToValidate="txbRequireDate">
                </ajaxToolkit:MaskedEditValidator>
            </td>
        </tr>
        <%--<tr>
            <td align="right">Expiration Date :</td>
            <td><asp:TextBox ID="txbExpDate" runat="server" Width="90" ToolTip="dd/MM/yyyy" Visible="false"></asp:TextBox>
                <asp:Image ID="imgCalendar2" runat="server" ImageUrl="~/image/calendar.png"
                 CssClass="pointer" Visible="false"/>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                 PopupButtonID="imgCalendar2" TargetControlID="txbExpDate">
                </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:MaskedEditExtender ID="MaskDate2" runat="server" ClearTextOnInvalid="true"
                 TargetControlID="txbExpDate" CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                </ajaxToolkit:MaskedEditExtender>
                <ajaxToolkit:MaskedEditValidator ID="MaskVDate2" runat="server"
                 ControlExtender="MaskDate2" ControlToValidate="txbExpDate">
                </ajaxToolkit:MaskedEditValidator></td>
        </tr>--%>
        <%--<tr>
            <td align="right">Currency :</td>
            <td><asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true"></asp:DropDownList></td>
        </tr>--%>
        <tr>
            <td align="right">Location :</td>
            <td colspan="3">
            <asp:DropDownList ID="ddlWH" runat="server" AutoPostBack="true" Width="126px"></asp:DropDownList>
            <asp:TextBox ID="txbLocName" runat="server" Width="420px" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <%--<tr>
            <td align="right">Remark :</td>
            <td colspan="3"><asp:TextBox ID="txbRemark" runat="server" Width="500px"></asp:TextBox></td>
        </tr>--%>
        <tr>
            <td align="right">Reference :</td>
            <td colspan="3"><asp:TextBox ID="txbReference" runat="server" Width="550px" MaxLength="150"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="4"><hr /></td>
        </tr>
        <%--<tr>
            <td align="right">Vendor :</td>
            <td colspan="3">
                <asp:TextBox ID="txbVendorCode" runat="server" Width="120"></asp:TextBox>
                <asp:TextBox ID="txbVendorName" runat="server" Width="400"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                 TargetControlID="txbVendorCode" OnClientItemSelected="SetVendorName"
                 ServiceMethod="GetVendorByCode" UseContextKey="true" ContextKey='' 
                 ServicePath="../AutoComplete.asmx"
                 MinimumPrefixLength="2"
                 CompletionInterval="1000"
                 EnableCaching="true"
                 CompletionSetCount="30"
                 CompletionListCssClass="completionListElement500"
                 CompletionListItemCssClass="listItem500"
                 CompletionListHighlightedItemCssClass="highlightedListItem500">
                </ajaxToolkit:AutoCompleteExtender>
                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                 TargetControlID="txbVendorName" OnClientItemSelected="SetVendorCode"
                 ServiceMethod="GetVendorByName" UseContextKey="true" ContextKey='' 
                 ServicePath="../AutoComplete.asmx"
                 MinimumPrefixLength="3"
                 CompletionInterval="1000"
                 EnableCaching="true"
                 CompletionSetCount="30"
                 CompletionListCssClass="completionListElement"
                 CompletionListItemCssClass="listItem"
                 CompletionListHighlightedItemCssClass="highlightedListItem">
                </ajaxToolkit:AutoCompleteExtender>
            </td>
        </tr>--%>
        <tr>
            <td align="right">Item :</td>
            <td colspan="3">
<ajaxToolkit:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div style="position: relative;z-index:3;">
        <asp:TextBox ID="txbItemCode" runat="server" Width="120" AutoPostBack="true"></asp:TextBox>
        <asp:TextBox ID="txbItemDesc" runat="server" Width="400" AutoPostBack="true"></asp:TextBox>
        <asp:ImageButton ID="imgbtnFindItem" runat="server" ImageUrl="~/image/find.png" />
        <asp:ImageButton ID="imgbtnAddItem" runat="server" style="display:none;"
         ToolTip="Add Item" ImageUrl="~/image/new.ico" Width="16" />
        <asp:ImageButton ID="imgbtnAddMultipleItems" runat="server" style="display:none;"
         ToolTip="Add Item" ImageUrl="~/image/new.ico" Width="16" />
        <%--<asp:ImageButton ID="imgbtnFindBook" runat="server"
         ToolTip="Item List" ImageUrl="~/image/findbook.png" Width="16" />--%>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
         TargetControlID="txbItemCode" OnClientItemSelected="SetItemDesc"
         ServiceMethod="GetItemByCode" UseContextKey="true" ContextKey='' 
         ServicePath="../AutoComplete.asmx"
         MinimumPrefixLength="3"
         CompletionInterval="1000"
         EnableCaching="true"
         CompletionSetCount="30"
         CompletionListCssClass="completionListElement500"
         CompletionListItemCssClass="listItem500"
         CompletionListHighlightedItemCssClass="highlightedListItem500">
        </ajaxToolkit:AutoCompleteExtender>
        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server"
         TargetControlID="txbItemDesc" OnClientItemSelected="SetItemCode"
         ServiceMethod="GetItemByDesc" UseContextKey="true" ContextKey='' 
         ServicePath="../AutoComplete.asmx"
         MinimumPrefixLength="3"
         CompletionInterval="1000"
         EnableCaching="true"
         CompletionSetCount="30"
         CompletionListCssClass="completionListElement"
         CompletionListItemCssClass="listItem"
         CompletionListHighlightedItemCssClass="highlightedListItem">
        </ajaxToolkit:AutoCompleteExtender>
        
        <%--<ajaxToolkit:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="imgbtnFindBook">
            <Animations>
                <OnClick>
                    <FadeOut Duration=".5" Fps="20" />
                </OnClick>
            </Animations>
        </ajaxToolkit:AnimationExtender>--%></div>
    </ContentTemplate>
</ajaxToolkit:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="right">Import :</td>
            <td colspan="3">
                <div class="fileinputs">
                <asp:FileUpload ID="FileUpload1" class="file" runat="server"/>
                    <div class="fakebutton">
                        <img id="fakeBrowse" src="../image/browse.png" onclick="document.getElementById('<%=FileUpload1.ClientID %>').click();" onmouseover="this.src='../image/browse_hover.png'" onmouseout="this.src='../image/browse.png'" style="visibility:hidden;" />
                        <asp:ImageButton ID="imgbtnUpload" runat="server" ImageUrl="~/image/Load.png" onmouseover="this.src='../image/Load_hover.png'" onmouseout="this.src='../image/Load.png'"/>
                    </div>
                    <div style="display:inline; padding-left:100px;">
                    Excel Format <span class='link' onclick='window.open(&quot;Items.xls&quot;,&quot;_blank&quot;,&quot;location=0,menubar=1,toolbar=1,resizable=1&quot;)'>Download</span></div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right"></td>
            <td colspan="3"><asp:Label ID="lblUpdateMessage" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</td></tr></table>
<ajaxToolkit:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="false" ShowFooter="true"
                 RowStyle-Wrap="false">
                    <Columns>
                        <asp:BoundField DataField="ItemId" HeaderText="Item Number" ItemStyle-Wrap="false"
                         HeaderStyle-Wrap="false" />
                        <asp:BoundField DataField="Name" HeaderText="Item Desc" ItemStyle-Wrap="false"
                         htmlencode="false" />
                        <asp:TemplateField HeaderText="Detail" ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:TextBox ID="txbDetail" runat="server" Text='<%#Eval("ITEMCOMMENT") %>'
                                 Width="90" MaxLength="150"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlProductDimension" runat="server" style="font-size:10px" ></asp:DropDownList>--%>
                                <asp:HiddenField ID="hidLineNum" runat="server" Value='<%#Eval("LINENUM") %>' />
                                <asp:HiddenField ID="hidLineRecID" runat="server" Value='<%#Eval("RECID") %>' />
                                <asp:HiddenField ID="hidItemRecId" runat="server" Value='<%#Eval("ItemRecId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Product Dimension" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlConfig" runat="server" style="font-size:10px" Visible="false" ></asp:DropDownList>
                            <asp:DropDownList ID="ddlSize" runat="server" style="font-size:10px" Visible="false" ></asp:DropDownList>
                            <asp:DropDownList ID="ddlColor" runat="server" style="font-size:10px" Visible="false" ></asp:DropDownList>
                            <asp:HiddenField ID="hidInventDimId" runat="server" Value='<%#Eval("INVENTDIMID") %>' />
                            <asp:HiddenField ID="hidItemMaster" runat="server" Value='<%#Eval("PRODUCTDIMENSION") %>' />
                            <asp:HiddenField ID="hidConfig" runat="server" Value='<%#Eval("Config") %>' />
                            <asp:HiddenField ID="hidSize" runat="server" Value='<%#Eval("Size") %>' />
                            <asp:HiddenField ID="hidColor" runat="server" Value='<%#Eval("Color") %>' />
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Order">
                            <ItemTemplate>
                                <asp:TextBox ID="txbQty" runat="server" Width="70px" Height="13px" CssClass="right" Text='<%#Eval("OQORDERED")%>' AutoPostBack="true"></asp:TextBox>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                 FilterType="Numbers, Custom" TargetControlID="txbQty" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                 TargetControlID="txbQty" MaskType="Number" Mask="9{9}.99" InputDirection="RightToLeft">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                 ControlToValidate="txbQty" ControlExtender="MaskedEditExtender1">
                                </ajaxToolkit:MaskedEditValidator>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ORDERUNIT" HeaderText="Unit" ItemStyle-HorizontalAlign="Center" />
                        <%--<asp:TemplateField HeaderText="Vendor">
                            <ItemTemplate>
                                <asp:TextBox ID="txbVendCode" runat="server" Text='<%#Eval("VDCODEL")%>'
                                 Width="60px"></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                 TargetControlID="txbVendCode" OnClientItemSelected="SetVendName"
                                 ServiceMethod="GetVendorByCode" UseContextKey="true" ContextKey='' 
                                 ServicePath="../AutoComplete.asmx"
                                 MinimumPrefixLength="2"
                                 CompletionInterval="1000"
                                 EnableCaching="true"
                                 CompletionSetCount="30"
                                 CompletionListCssClass="completionListElement300"
                                 CompletionListItemCssClass="listItem300"
                                 CompletionListHighlightedItemCssClass="highlightedListItem300">
                                </ajaxToolkit:AutoCompleteExtender>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField  HeaderText="Vendor Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txbVendName" runat="server" Text='<%#Eval("VDNAMEL")%>' 
                                 Width="100px" ></asp:TextBox>
                                <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                 TargetControlID="txbVendName" OnClientItemSelected="SetVendCode"
                                 ServiceMethod="GetVendorByName" UseContextKey="true" ContextKey='' 
                                 ServicePath="../AutoComplete.asmx"
                                 MinimumPrefixLength="3"
                                 CompletionInterval="1000"
                                 EnableCaching="true"
                                 CompletionSetCount="30"
                                 CompletionListCssClass="completionListElement300"
                                 CompletionListItemCssClass="listItem300"
                                 CompletionListHighlightedItemCssClass="highlightedListItem300">
                                </ajaxToolkit:AutoCompleteExtender>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:HiddenField ID="hidMANITEMNO" runat="server" value='<%#Eval("MANITEMNO")%>'/>
                                <asp:DropDownList ID="ddlSubSection" runat="server" style="font-size:10px"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date Require" FooterStyle-CssClass="right">
                            <ItemTemplate>
                                <asp:TextBox ID="txbReqDate" runat="server" Width="80"
                                 ToolTip="dd/MM/yyyy" Text='<%#Eval("EXPARRIVAL")%>'>
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"
                                 PopupButtonID="txbReqDate" TargetControlID="txbReqDate"
                                 Format="dd/MM/yyyy" Animated="true" PopupPosition="BottomRight">
                                </ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditExtender ID="MaskDate1" runat="server" 
                                 ClearTextOnInvalid="true" TargetControlID="txbReqDate"
                                 CultureName="en-GB" MaskType="Date" Mask="99/99/9999">
                                </ajaxToolkit:MaskedEditExtender>
                                <ajaxToolkit:MaskedEditValidator ID="MaskVDate1" runat="server"
                                 ControlExtender="MaskDate1" ControlToValidate="txbReqDate">
                                </ajaxToolkit:MaskedEditValidator>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:HiddenField ID="hidDTCOMPLETEL" runat="server"
                                 Value='<%#Eval("DTCOMPLETEL")%>'/>
                                <asp:HiddenField ID="hidItemCost" runat="server"
                                 Value='<%#Eval("ITEMCOST")%>'/>
                                <asp:HiddenField ID="hidCurr" runat="server"
                                 Value='<%#Eval("CURRENCY")%>'/>
                                <asp:HiddenField ID="hidCurrISO" runat="server"
                                 Value='<%#Eval("CURRENCYCODEISO")%>'/>
                                <%--<asp:TextBox ID="txbRemark" runat="server" MaxLength="80" Width="100"
                                 Text='<%#Eval("INSTRUCTCOMMENT")%>' CssClass="right" ReadOnly="true"></asp:TextBox>--%>
                                <asp:Label ID='lblUnitPrice' runat="server" Width="80"
                                 Text='<%#Eval("ITEMCOST")%>' CssClass="right"></asp:Label>
                            </ItemTemplate><FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                Total
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost">
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server"  Width="100"
                                 Text='<%#Eval("INSTRUCTCOMMENT")%>' CssClass="right"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDel" runat="server"
                                 ImageUrl="~/image/delete.ico" Width="16px" ToolTip="Cancel this item" 
                                 CommandName="Delete" CommandArgument='<%#Eval("ItemId")%>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
<div id="divgvItemError" runat="server" style="position:absolute;top:50px;left:850px;z-index:3;">
        <table cellpadding="0" cellspacing="0" border="0">
            <tr onmousedown="grab(event,document.getElementById('<%=divgvItemError.ClientID %>'))"><td style="background-color:#8CAEFD;cursor:move;" align="right" id="tdcanmove">
            <div style="display:inline;cursor:pointer" 
             onmouseover="this.style.color='white';" 
             onmouseout="this.style.color='black';" 
             onclick="document.getElementById('<%=divgvItemError.ClientID %>').style.visibility='hidden';">
            <img src="../image/16_del.ico" alt="close" style="filter:gray;border:solid 1px" onmouseover="this.style.filter='none';" onmouseout="this.style.filter='gray';" /></div></td></tr>
            <tr><td>
        <asp:GridView ID="gvItemError" Font-Size="10px" runat="server" AutoGenerateColumns="false" BackColor="White" Visible="false">
            <HeaderStyle CssClass="" />
            <RowStyle CssClass="" />
            <AlternatingRowStyle CssClass="" />
            <Columns>
                <asp:BoundField DataField="Item Number" HeaderText="Item Number" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"/>
                <asp:TemplateField HeaderText="Result"><ItemStyle Wrap="false" />
                    <ItemTemplate>
                        <asp:Image ID="imgSuccess" runat="server" ImageUrl="~/image/apply2.ico" Width="12px" Visible='<%#Eval("success")%>'  />
                        <asp:Image ID="imgError" runat="server" ImageUrl="~/image/delete2.ico" Width="12px" Visible='<%#Eval("error")%>'  />
                        <asp:Label ID="lblResult" runat="server" Text='<%#Eval("result")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>            
            </td></tr>
        </table>
</div>
        <ajaxToolkit:DropShadowExtender ID="DropShadowExtender1" runat="server"
         TargetControlID="divgvItemError" Opacity="0.1" Rounded="false" TrackPosition="true" Radius="1" >
        </ajaxToolkit:DropShadowExtender>
         <asp:Button ID="btnSubmit" runat="server" Text="Post" Visible="false" Width="80px"/>
         <asp:Button ID="btnSave" runat="server" Text="Save" Visible="false" Width="80px"/>
         <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" Width="80px"/>
         <asp:Button ID="btnClear" runat="server" Text="Clear" Visible="false" Width="80px"/>
            <asp:Label ID="lblSubmitResult" runat="server" Text=""></asp:Label>


    </ContentTemplate>
</ajaxToolkit:UpdatePanel>
<script type="text/javascript">
/*function SetVendorName( source, eventArgs ){
var str = document.getElementById('<%--txbVendorCode.ClientID --%>').value;
document.getElementById('<%--txbVendorCode.ClientID --%>').value=str.split("	")[0];// "	" = ControlChars.Tab
document.getElementById('<%--txbVendorName.ClientID --%>').value=eventArgs.get_value();
if(document.getElementById('<%--txbItemCode.ClientID --%>').disabled!=true)
document.getElementById('<%--txbItemCode.ClientID --%>').focus();
}
function SetVendorCode( source, eventArgs ){
document.getElementById('<%--txbVendorCode.ClientID --%>').value=eventArgs.get_value();
if(document.getElementById('<%--txbItemCode.ClientID --%>').disabled!=true)
document.getElementById('<%--txbItemCode.ClientID --%>').focus();
}

function SetVendName( source, eventArgs ){
var str = document.getElementById(source.get_element().id).value;
var txbVendCode = source.get_element().id;
document.getElementById(txbVendCode).value=str.split("	")[0];// "	" = ControlChars.Tab
var txbVendName = txbVendCode.substring(0,txbVendCode.indexOf('txbVendCode')) + 'txbVendName';
document.getElementById(txbVendName).value=str.split("	")[1];
}
function SetVendCode( source, eventArgs ){
var txbVendName = source.get_element().id;
var txbVendCode = txbVendName.substring(0,txbVendName.indexOf('txbVendName')) + 'txbVendCode';
document.getElementById(txbVendCode).value=eventArgs.get_value();
}*/

function SetItemDesc( source, eventArgs ){
var str = document.getElementById('<%=txbItemCode.ClientID %>').value;
document.getElementById('<%=txbItemCode.ClientID %>').value=str.split("	")[0];// "	" = ControlChars.Tab
document.getElementById('<%=txbItemDesc.ClientID %>').value=eventArgs.get_value();
}

function SetItemCode( source, eventArgs ){
document.getElementById('<%=txbItemCode.ClientID %>').value=eventArgs.get_value();
}

function validateinput(source, e) {
    var digits = "0123456789.";
    if (digits.indexOf(String.fromCharCode(e.keyCode)) == -1)
        e.returnValue = false//return false;
    var parts = e.srcElement.value.split('.');
    if (parts.length > 1 && String.fromCharCode(e.keyCode) == '.')
    { e.returnValue = false } //return false; 
    
    return true;
}

var mybrowser=navigator.userAgent;  
    if(mybrowser.indexOf('MSIE')>0){
        //alert("IE");
        //document.getElementById('fakeBrowse').style.visibility = 'hidden'; //hidden
    }  
    if(mybrowser.indexOf('Firefox')>0){
        //alert("Firefox");  
        //document.getElementById('fakeBrowse').style.visibility = 'visible';
    }     
    if(mybrowser.indexOf('Presto')>0){
        //alert("Opera");  
        //document.getElementById('fakeBrowse').style.visibility = 'visible';
    }             
    if(mybrowser.indexOf('Chrome')>0){
        //alert("Chrome");
        //document.getElementById('fakeBrowse').style.visibility = 'visible';
    }

    var grabx = 0;
    var graby = 0;
    var elex = 0;
    var eley = 0;
    var orix = 0;
    var oriy = 0;
    var mousex = 0;
    var mousey = 0;
    var dragobj = null;
    var tmpzIndex;
    function falsefunc() { return false; }
    function grab(e,context) {
        document.onmousedown = falsefunc; // in NS this prevents cascading of events, thus disabling text selection
        dragobj = context;
        tmpzIndex = dragobj.style.zIndex;
        dragobj.style.zIndex = 10; // move it to the top
        dragobj.style.filter = 'alpha(opacity: 70)';
        var b = $find('ctl00_ContentPlaceHolder1_DropShadowExtender1'); b.set_Width(0);
        document.onmousemove = drag;
        document.onmouseup = drop;
        grabx = mouseX(e);
        graby = mouseY(e);
        elex = orix = dragobj.offsetLeft;
        eley = oriy = dragobj.offsetTop;
    }
    function drag(e) // parameter passing is important for NS family 
    {
        //document.getElementById('<%=lblUpdateMessage.ClientID %>').innerHTML=([elex, eley]);
        if (dragobj) {
            elex = orix + (mouseX(e) - grabx);
            eley = oriy + (mouseY(e) - graby);
            dragobj.style.position = "absolute";
            //dragobj.style.left = (elex).toString(10) + 'px';
            //dragobj.style.top = (eley).toString(10) + 'px';
            dragobj.style.left = parseInt(elex) + 'px'; //(parseInt(mouseX(evt)) + offX) + 'px';
            dragobj.style.top = parseInt(eley) + 'px';
        }
        return false; // in IE this prevents cascading of events, thus text selection is disabled
    }
    function drop() {
        if (dragobj) {
            dragobj.style.zIndex = tmpzIndex;
            dragobj.style.filter = 'alpha(opacity: 100)';
            var b = $find('ctl00_ContentPlaceHolder1_DropShadowExtender1'); b.set_Width(5);
            dragobj = null;
        }

        document.onmousemove = null;
        document.onmouseup = null;
        document.onmousedown = null;   // re-enables text selection on NS
    }
    function mouseX(evt) { if (!evt) evt = window.event; if (evt.pageX) return evt.pageX; else if (evt.clientX) return evt.clientX + (document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft); else return 0; }
    function mouseY(evt) { if (!evt) evt = window.event; if (evt.pageY) return evt.pageY; else if (evt.clientY) return evt.clientY + (document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop); else return 0; }
</script>

</asp:Content>

