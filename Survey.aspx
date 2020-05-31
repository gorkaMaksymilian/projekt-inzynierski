<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="PI.Survey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<link href="SurveyCSS.css" rel="stylesheet" />
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
    <form id="form1" runat="server" class="form">
        <div>
            <asp:GridView ID="QuestionsGrid" runat="server" BorderStyle="None" GridLines="None" ShowHeader="False">
            </asp:GridView>
        </div>
        <div>
            <br>
            <br>
        </div>

        <div id="test1" runat="server">
            
        </div>
        <asp:Button CssClass="form button" ID="SaveButton" runat="server" Text="Zapisz" OnClick="SaveButton_Click" />
        <asp:Label ID="dataLabel" runat="server"></asp:Label>
    </form>
</body>
</html>
