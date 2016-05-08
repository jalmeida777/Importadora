<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeJquery.aspx.cs" Inherits="BarcodeJquery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<script src="js/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="js/jquery-barcode.js"></script>  
<script type="text/javascript">
   
    function getURLParameter(name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
    }

    function generateBarcode() {

        var cantidad = document.getElementById("txtCantidad").value;

        $('#here_table').html('');

        var table = $('<table></table>');
        for (i = 0; i < cantidad; i++) {
            var row = $('<tr><td class=barcode_needed></td></tr>');
            table.append(row);
        }
        $('#here_table').append(table);

        var v_CodigoInterno = getURLParameter('v_CodigoInterno');
        $('td.barcode_needed').append('<div class="bcTarget">');
        $('.bcTarget').each(function () {
            var $this = $(this);
            $this.barcode(v_CodigoInterno, 'code128');
        });
     }
</script>

 <script type="text/javascript">
     function Popup() {
         var mywindow = window.open('', 'Ticket info', 'height=400,width=600');
         mywindow.document.write('<html><head><title>my div</title>');
         mywindow.document.write('<style type="text/css"> *{margin: 0; padding: 0;} body{padding: 3px; padding-left:20px;font:6px bold Arial;}</style>');
         mywindow.document.write('<script src="js/jquery-1.3.2.min.js"><' + '/script>');
         mywindow.document.write('<script src="js/jquery-barcode.js"><' + '/script>');
         mywindow.document.write('</head><body>');
         mywindow.document.write('<div id="demo"></div>');
         mywindow.document.write('<script type="text/javascript">$("#demo").barcode("1234567890128", "code128");<' + '/script>');
         mywindow.document.write('<script type="text/javascript">window.print();<' + '/script>');
         mywindow.document.write('</body></html>');
         return true;
     }
    </script>
<body>
    
    
    <form id="form1" runat="server">
    <table bgcolor="#99CCFF" class="style1">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="20pt" ForeColor="#333333" Text="Generar Código de Barras"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
    
    
    <input id="txtCantidad" type="text" value="1" />
    <input type="button" onclick="generateBarcode();" value="Ver"/>
    <input type="button" value="Imprimir" onclick="Popup();" /></td>


            <td>
                &nbsp;</td>
        </tr>
    </table>
        <div id="here_table">
    
</div>
    </form>
     
</body>
</html>
