<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="PI.Check" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="CheckCSS.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="form">
        <p>
            Wpisz hasło:<br />
            <asp:TextBox ID="TypePass" runat="server" OnTextChanged="TypePass_TextChanged"></asp:TextBox>
            <asp:Button ID="ButtonPass" runat="server" OnClick="ButtonPass_Click" Text="Autoryzuj" />
        </p>
        <p>
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
        </p>
        <p>
            <asp:Label ID="Label2" runat="server"></asp:Label>
            </p>
        <p>
            &nbsp;</p>
        <p>
            <asp:Button ID="returnButton" runat="server" OnClick="returnButton_Click" Text="Powrot" />
        </p>
    </form>
</body>
</html>