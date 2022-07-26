<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Purchase Online - Login</title>
    <link href="App_Themes/Theme1/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="image/favicon.ico" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ajaxToolkit:ScriptManager ID="ScriptManager1" runat="server">
        </ajaxToolkit:ScriptManager>
        <%--<img src="./image/black_ribbon_bottom_left.png" style="position:fixed;z-index: 9999;width:70px;left:0;bottom:0;" alt="black-ribbon"/>--%>
        <table border="0" cellpadding="0" cellspacing="0"
         class="english light" align="center" style="margin-top:50px;">
            <tr class="HeaderStyle">
                <td class="center light">Login : Windows Authentication</td>
            </tr>
            <tr>
                <td class="light"><img src="image/AX.png" />
                    <table align="center">
                        <tr>
                            <td class="right">User name:</td>
                            <td class="left"><asp:TextBox ID="txbUser" runat="server" Width="150"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="right">Password:</td>
                            <td class="left"><asp:TextBox ID="txbPass" runat="server" Width="150" TextMode="password"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="right">Domain:</td>
                            <td class="left">
                                <asp:DropDownList ID="ddlDomain" runat="server" Width="150"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="right">Site:</td>
                            <td class="left">
                                <asp:DropDownList ID="ddlSite" runat="server" Width="150"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="left">
                                <asp:ImageButton ID="ibtnLogin" runat="server"
                                 ImageUrl="~/image/login.png"/>
                            </td>
                        </tr>
                    </table>
                    <asp:RequiredFieldValidator ID="rfvPass" runat="server" SetFocusOnError="true" Display="None" ControlToValidate="txbPass"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <table border="0" align="center">
        <tr><td><asp:Label ID="lblMessage" runat="server" CssClass="english red"></asp:Label></td></tr>
        </table>
        <br /><br /><br /><br /><br /><br /><br /><br />
        <table border="0" align="center">
        <tr><td><div><a href="https://hoyagw.sharepoint.com/sites/optics01/hopt">
            <asp:Image ID="imgSharePoint" runat="server" ImageUrl="~/image/SharePointfavicon.ico" style="vertical-align:middle;"/> go to HOPT Intranet(Share Point)</a></div></td></tr>
        </table>
        <br /><br /><br /><br /><br /><br /><br />
        <table border="0" align="center">
        <tr><td><a href="http://192.1.87.221:81/Purchase_Online/">go to Purchase Online</a></td></tr>
        </table>
    </div>
    </form>
<script type="text/javascript">

/*if ("ActiveXObject" in window) {
    try{
        var WinNetwork = new ActiveXObject("WSCRIPT.Network");
        document.getElementById('').value = WinNetwork.userName);
    }
    catch (e){}
}*/
//if(typeof(window.ActiveXObject)=="undefined"){
//     alert("ActiveX Object not supported"); 
//     } else {
//     alert("ActiveX Object  supported"); 
//}
//var wshshell=new ActiveXObject("wscript.shell");
//var username=wshshell.ExpandEnvironmentStrings("%username%");
//alert(username);
</script>
</body>
</html>
