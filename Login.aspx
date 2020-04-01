<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Głosowanievol2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 72px;
            width: 131px;
            margin: auto;

        }
        .auto-style2 {
            margin-right: 0px;
        }
    </style>
</head>
<body style ="background-color:black">

    <form id="form1" runat="server" class="auto-style1">
        <div>
            <asp:TextBox ID="email" runat="server" OnTextChanged="TextBox2_TextChanged" ValidateRequestMode="Enabled" Width="383px">email</asp:TextBox>
            <br />
            <asp:TextBox ID="nralbumu" runat="server" OnTextChanged="TextBox1_TextChanged" CssClass="auto-style2" Height="22px" Width="383
                px">numer albumu</asp:TextBox>
        </div>
        <asp:Button ID="zaloguj" runat="server" BackColor="White" OnClick="Button1_Click" Text="Zaloguj" Width="118px" />
        <br />
    </form>
</body>
</html>
