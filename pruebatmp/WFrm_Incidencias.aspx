<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WFrm_Incidencias.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Probar conexión a SQL Server con botón</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnProbarConexion" runat="server" Text="Probar conexión" OnClick="btnProbarConexion_Click" />
            <br /><br />
            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
        </div>
        <div>
            <asp:Button ID="btnMostrarVista" runat="server" Text="Mostrar Vista" OnClick="btnMostrarVista_Click" />
            <br /><br />
            <asp:GridView ID="gvVista" runat="server" AutoGenerateColumns="True"></asp:GridView>
        </div>
    </form>
</body>
</html>
