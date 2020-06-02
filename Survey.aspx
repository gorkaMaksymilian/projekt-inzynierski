<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="PI.Survey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server" title="Survey">
<link href="SurveyCSS.css" rel="stylesheet" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>

        
    </title>



</head>
<body>
    <form id="form1" runat="server" class="form">
         
        <div >
            <!--
           <asp:Label ID="Ask1" runat="server" Text="Label"></asp:Label><br />
           <asp:Label ID="Ask2" runat="server" Text="Label"></asp:Label><br />
            <asp:Label ID="Ask3" runat="server" Text="Label"></asp:Label><br />
            -->
            <div >
            <asp:GridView ID="QuestionsGrid" runat="server" BorderStyle="None" GridLines="None" ShowHeader="False">
            </asp:GridView>

            <div id="test1" runat="server">
            </div>
        
            </div>

            <asp:Button CssClass="form button" ID="SaveButton" runat="server" Text="Zapisz" OnClick="SaveButton_Click" style="left: 0px; top: 0px; width: 100%; height: 67px" />
            <asp:Label ID="dataLabel" runat="server"></asp:Label>
            </div>


            

            







        

    </form>
</body>
</html>
