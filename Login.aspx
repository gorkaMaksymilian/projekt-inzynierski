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
        .auto-style3 {
            width: 364px;
        }
    </style>
</head>
<body>
    
    
    <form id="form1" runat="server" class="auto-style1">

        <div class="auto-style3">
            Uniwersytet Pedagogiczny im. Komisji Edukacji Narodowej
        </div>

        
        <div class="auto-style3">
            Zaloguj się aby oddać głos
        </div>




        <div>
            <asp:TextBox ID="email" runat="server" OnTextChanged="TextBox2_TextChanged" ValidateRequestMode="Enabled" Width="383px">email</asp:TextBox>
            <br />
            <asp:TextBox ID="nralbumu" runat="server" OnTextChanged="TextBox1_TextChanged" CssClass="auto-style2" Height="22px" Width="383
                px">numer albumu</asp:TextBox>
        </div>
        <asp:Button ID="zaloguj" runat="server" BackColor="White" OnClick="Button1_Click" Text="Zaloguj" Width="178px" />
        <asp:Button ID="Sprawdz1" runat="server" OnClick="Sprawdz" Text="Sprawdź Odpowiedź" />
        <br />
        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Maroon"></asp:Label>


    </form>
</body>
</html>
