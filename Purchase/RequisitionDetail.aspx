<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RequisitionDetail.aspx.vb" Inherits="Purchase_RequisitionDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" Height="50px" Width="350px" BackColor="white"/>
    </div>
    </form>

    <%-- Add Reference to jQuery at Google CDN --%>
    <%--<script type="text/javascript" src="../jquery.min.js"></script>--%>

    <%-- Register the WebClientPrint script code --%>
    <%--<%=Neodynamic.SDK.Web.WebClientPrint.CreateScript()%>--%>
    <%--<script type="text/javascript" src="../JScriptTest.js"></script>--%>
    <%--<script type="text/javascript">
        alert('hi2');
        var jsWebClientPrint = (function () {
            var b = '94f2f49c9621d50e1d5d22094d22079448d2120859784d2120349c5d2693929c949a89445eb0f3949fea123fc54520fe33237523c95e4029e4a8440affcf31c8f34a49554f05238135ffec9e5f05238135f251593949fe45419ce1308f21ed021300013764f3d6256d6566d63c95e4029e46d754029e4523a8440affcf31c8f34a49554f05238135ffec9e5f730e184f394d33945e4569e548440affcf31c8f34a49554f05238135ffec9e5f730e184f754029e45236394dd754730065239fea8440affcf31c8f34a49554f05238135ffec9e5f730e184f394d8440affcf31c8f34a49554f05238135ffec9e5f730e184f754730065239fe6394d665666736676662666632226632227667632322666667323227776632767666667736666663767676663667667762223277676666666777667367773226666666773333332577666765466666257766676256776767666467666267773554355333333332666327673266632666667576672266757667677367773226666666773333332577666765466666276726763766327667666666666777322666666677333333257766676546666627672676366757667677276632667567756776663677732266666667733333325776667654666662767267637663677732266666667733333325776667654666662767267636675677567766627663';
            var l = b.length / 2;
            var c = function () {
                if (arguments.length == 1) {
                    return String.fromCharCode(parseInt(arguments[0], 16));
                }
                else {
                    var t = '';
                    var i = arguments[0];
                    var j = arguments[1] + i;
                    for (i; i < j; i++) { t += c(b.charAt(i + l) + b.charAt(i)); }
                    return t;
                }
            };
            var setIF = function () {
                var if_id = c(0, 3) + new Date().getTime();
                $(c(3, 4)).append(c(7, 14) + if_id + c(21, 6) + if_id + c(27, 69));
                $(c(96, 1) + if_id).attr(c(97, 3), c(100, 15) + arguments[0]); setTimeout(function () {
                    $(c(96, 1) + if_id).remove()
                }, 5000)
            };
            return { print: function () { setIF(c(115, 112) + (arguments.length == 1 ? c(227, 1) + arguments[0] : '')) }, getPrinters: function () {
                setIF(c(228, 64) + $(c(292, 4)).val());
                var delay_ms = (typeof wcppGetPrintersDelay_ms === c(296, 9)) ? 10000 : wcppGetPrintersDelay_ms;
                setTimeout(function () { $.get(c(305, 63) + $(c(292, 4)).val(), function (data) { if (data.length > 0) { wcpGetPrintersOnSuccess(data) } else { wcpGetPrintersOnFailure() } }) }, delay_ms)
            }, getWcppVer: function () {
                setIF(c(368, 67) + $(c(292, 4)).val());
                var delay_ms = (typeof wcppGetVerDelay_ms === c(296, 9)) ? 10000 : wcppGetVerDelay_ms;
                setTimeout(function () { $.get(c(435, 66) + $(c(292, 4)).val(), function (data) { if (data.length > 0) { wcpGetWcppVerOnSuccess(data) } else { wcpGetWcppVerOnFailure() } }) }, delay_ms)
            }, send: function () { setIF.apply(this, arguments) } 
            }
        })();    
    </script>--%>

    <script type="text/javascript">
//        $(document).ready(function () {
//            
//            <%-- remove built-in print button behavior --%>
//            $('#<%=CrystalReportViewer1.ClientID%>_toptoolbar_print').prop("onclick", null).attr("onclick", null);
//            <%-- add our own print button behavior --%>
//            $('#<%=CrystalReportViewer1.ClientID%>_toptoolbar_print').click(function() {
//                jsWebClientPrint.print('printerName=' + $('#ddlClientPrinters').val()); 
//            });

//            $('#<%=CrystalReportViewer1.ClientID%>_toptoolbar_print').parent().parent().prepend('<select name="ddlClientPrinters" id="ddlClientPrinters" class="comboEditable" title="Select Printer"><option>Loading printers...</option></select>');
//            
//            <%-- load client printers through WebClientPrint --%>
//            jsWebClientPrint.getPrinters();

//            
//        });

//        <%-- Time delay we'll wait for getting client printer names --%>
//        var wcppGetPrintersDelay_ms = 5000; //5 sec

//        function wcpGetPrintersOnSuccess(){
//            <%-- Display client installed printers --%>
//            if(arguments[0].length > 0){
//                var p=arguments[0].split("|");
//                var options = '<option>Default Printer</option>';
//                for (var i = 0; i < p.length; i++) {
//                    options += '<option>' + p[i] + '</option>';
//                }
//                $('#ddlClientPrinters').html(options);                                            
//            }else{
//                alert("No printers are installed in your system.");
//            }
//        }

//        function wcpGetPrintersOnFailure() {
//            <%-- Do something if printers cannot be got from the client --%>
//            alert("No printers are installed in your system.");
//        }

    </script>

</body>
</html>
