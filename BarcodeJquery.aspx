<%@ Page Title="Generar Código de Barras" Language="C#" MasterPageFile="~/Plantilla.master" AutoEventWireup="true" CodeFile="BarcodeJquery.aspx.cs" Inherits="BarcodeJquery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            var row = $('<tr><td class=barcode_needed></td><td class=barcode_needed></td></tr>');
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

     function PrintElem(elem) {
         Popup($(elem).html());
     }

     function Popup(data) {
         var mywindow = window.open('', 'my div', 'height=400,width=600');
         mywindow.document.write('<html><head><title>my div</title>');
         mywindow.document.write('</head><body >');
         mywindow.document.write(data);
         mywindow.document.write('</body></html>');

         mywindow.document.close(); // necessary for IE >= 10
         mywindow.focus(); // necessary for IE >= 10

         mywindow.print();
         mywindow.close();

         return true;
     }


 </script>

 <style>
.fa-3x
{
    color: #1fa67a;
}

</style>

</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="divBusqueda">

            <table width="100%" cellpadding="3" cellspacing="3">
                <tr>
                    <td style="width:25px; padding-left:20px">
                        <i class="fa fa-barcode fa-3x" aria-hidden="true"></i>
                    </td>
                    <td align="left">
                        <h1 class="label">Generar Código de Barras</h1>
                    </td>
                </tr>
                </table>
            </div>
                <div class="toolbar">
            <table width="100%"><tr>
                <td width="140">
                
    
    <input id="txtCantidad" type="text" value="1" /></td>
                <td width="85">
                
    <input type="button" onclick="generateBarcode();" value="Ver" style="width: 80px"/></td>
                <td width="85">
                  
    <input type="button" value="Imprimir" onclick="PrintElem('#here_table')" style="width: 80px" /></td>
                <td>
                   
                <asp:Button ID="btnSalir" runat="server" onclick="btnSalir_Click" 
                    Text="Salir" Width="80px" />
                </td>
                </tr></table>
            </div>


        <div id="here_table"></div>
</asp:Content>
