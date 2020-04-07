<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="PI.Survey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>
        <style type="text/css">
        div {
            float: left;
        }
       
        
    </style>
    </title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="float:left">
            <asp:GridView ID="QuestionsGrid" runat="server" BorderStyle="None" GridLines="None" ShowHeader="False">
            </asp:GridView>
        </div>
        <div id="test1" runat="server">
            
        </div>
        <asp:Button ID="SaveButton" runat="server" Text="Zapisz" />
        <asp:Label ID="dataLabel" runat="server"></asp:Label>
    </form>
</body>
</html>
