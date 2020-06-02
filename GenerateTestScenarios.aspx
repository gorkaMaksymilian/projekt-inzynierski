<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateTestScenarios.aspx.cs" Inherits="PI.GenerateTestScenarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="GenerateButton" runat="server" OnClick="GenerateButton_Click" Text="Generuj" />
        </div>
        <p>
            <asp:Label ID="InfoLabel" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
