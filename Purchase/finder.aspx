<%@ Page Language="VB" AutoEventWireup="false" CodeFile="finder.aspx.vb" Inherits="finder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search Page</title>
    <link href="../App_Themes/Theme1/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="../image/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
<%--<ajax:ScriptManager ID="ScriptManager1" runat="server">
</ajax:ScriptManager>--%>
    <div>
<%--<ajax:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table align="center" border='0'>
            <tr><td colspan="2" align="center"><asp:Literal ID="lblName" runat="server"></asp:Literal></td></tr>
            <tr>
                <td align="right">Search By :</td>
                <td style="width:395px;">
                    <asp:DropDownList ID="ddlFind" runat="server">
                        <asp:ListItem Text="Item" Value="Item"></asp:ListItem>
                        <asp:ListItem Text="Description" Value="Desc"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">Search :</td>
                <td>
                    <asp:TextBox ID="txbFind" runat="server" AutoPostBack="true"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" UseSubmitBehavior="true" Text="Search"/>
                    <asp:Label ID="lblDesc" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="false">
                        <Columns>
                            <asp:ButtonField Text="Button" CommandName="DoubleClick" Visible="False" />
                            <asp:BoundField DataField="ITEMID" HeaderText="ITEM NUMBER" />
                            <asp:BoundField DataField="Name" HeaderText="ITEM DESCRIPTION" />
                            <asp:TemplateField HeaderText="Dim" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:HiddenField ID="hidgvItemRecId" runat="server" Value='<%# Eval("PRODUCT") %>' />
                                    <asp:HiddenField ID="hidgvProductDimension" runat="server" Value='<%# Eval("ProductDimension") %>' />
                                    <asp:ImageButton ID="imggvProductDimension" runat="server" Visible="false" CommandName='ProductDimension' CommandArgument='<%# Eval("PRODUCT") %>'
                                     ImageUrl="~/image/object.png" ToolTip="select by product dimension"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:ButtonField DataTextField="LEADTIME" HeaderText="LEAD TIME" />--%>
                        </Columns>
                    </asp:GridView>
                    <div id="divProductDimension" runat="server" visible="false">
                        <table>
                        <tr><td></td><td>Item Number: <asp:Label ID="lblItemNumber" runat="server" CssClass="link"></asp:Label></td></tr>
                        <tr><td></td><td>Item Description: <asp:Label ID="lblItemDesc" runat="server" CssClass="link"></asp:Label>
                        <asp:HiddenField ID="hidItemRecId" runat="server" />
                        <asp:HiddenField ID="hidProductDimension" runat="server" />
                        </td></tr>
                        <tr style=" vertical-align:top;">
                        <td><asp:ImageButton ID="imgProductDimensionHid" runat="server" ImageUrl="~/image/left.png" /></td>
                        <td><table><tr><td>
                        <asp:GridView ID="gvProductDimension" runat="server" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Color" HeaderText="Color" />
                                <asp:BoundField DataField="Size" HeaderText="Size" />
                            </Columns>
                        </asp:GridView>
                        </td></tr>
                        <tr><td align="right"><asp:Button ID="btnAdd" runat="server" Text=" Add "/></td></tr></table>
                        </td></tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    <%--</ContentTemplate>
</ajax:UpdatePanel>--%>
<%--<input type="button" value="in" onclick="fadeEffect.init('divProductDimension', 1, 50)" />
<input type="button" value="out" onclick="fadeEffect.init('divProductDimension', 1)" />--%>
    </div>
    </form>
</body>
<script type="text/javascript">
function SetValue(control,textvalue){
    //window.opener.document.getElementById(unusedcontrol).value='';
    if (window.opener.document.getElementById(control)) {
        //if (window.opener.document.getElementById(control).enabled == true) {
            window.opener.document.getElementById(control).value = textvalue;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_imgbtnAddItem').click();
        //}
    }else{window.close();}
    //window.close();
    }
/*function SetMultipleValue() {
    if (window.opener.document.getElementById('ctl00_ContentPlaceHolder1_imgbtnAddMultipleItems')) {
        window.opener.document.getElementById('ctl00_ContentPlaceHolder1_imgbtnAddMultipleItems').click();
    } else { window.close(); }
}*/
function validateinput(source, e) {
    var digits = "0123456789.";
    if (digits.indexOf(String.fromCharCode(e.keyCode)) == -1)
        e.returnValue = false//return false;
    var parts = e.srcElement.value.split('.');
    if (parts.length > 1 && String.fromCharCode(e.keyCode) == '.')
    { e.returnValue = false } //return false; 

    return true;
}
var fadeEffect = function() {
    return {
        init: function(id, flag, target) {
            this.elem = document.getElementById(id);
            clearInterval(this.elem.si);
            this.target = target ? target : flag ? 100 : 0;
            this.flag = flag || -1;
            this.alpha = this.elem.style.opacity ? parseFloat(this.elem.style.opacity) * 100 : 0;
            this.elem.si = setInterval(function () { fadeEffect.tween() }, 20);
        },
        tween: function () {
            if (this.alpha == this.target) {
                clearInterval(this.elem.si);
            } else {
                var value = Math.round(this.alpha + ((this.target - this.alpha) * .05)) + (1 * this.flag);
                this.elem.style.opacity = value / 100; this.elem.style.filter = 'alpha(opacity=' + value + ')';
                this.alpha = value
            }
        }
    }
} ();
</script>
</html>
