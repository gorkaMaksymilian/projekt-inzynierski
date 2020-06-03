<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PI.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="LoginCSS.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

</head>
<body>
    
    
    <form id="form1" runat="server" class="form">

        <div class="auto-style3">
            Uniwersytet Pedagogiczny im. Komisji Edukacji Narodowej
            <br />
            <br />
        </div>

        
        <div class="auto-style3">
            Zaloguj się aby oddać głos
        </div>

        <div>
            <asp:TextBox CssClass="form input" ID="email" runat="server" ValidateRequestMode="Enabled" Width="383px">email</asp:TextBox>
            <br />
            <asp:TextBox CssClass="form input" ID="nralbumu" runat="server"  Height="22px" Width="383
                px">numer albumu</asp:TextBox>
        <asp:Button CssClass="form button" ID="zaloguj" runat="server" BackColor="White" OnClick="Button1_Click" Text="Zaloguj" />
       
        <asp:Button ID="Sprawdz1" runat="server" OnClick="Sprawdz" Text="Sprawdź Odpowiedź" />
        <br />
        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Maroon"></asp:Label>

        </div>
    </form>
</body>
</html>