<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <h1>Generate Access Token</h1>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Usuario"></asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server"  value="hundred"></asp:TextBox>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server" Text="Aplicacion"></asp:Label>
            <asp:TextBox ID="txtaplicacion" runat="server" value="F749-52E8-D9C9-43DE-B4E4-7B23-D61F"></asp:TextBox>
        </p>
           <p>
               <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click"  />
           </p>
    </div>
    </form>
</body>
</html>
