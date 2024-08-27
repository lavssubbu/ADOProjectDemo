<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="WebApplication6.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Sign In</h2>
             <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
<asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
<br />
 <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
 <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
 <br />
<asp:Label ID="lblError" runat="server" Text="Error" Visible="false"></asp:Label>
 <asp:Button ID="btnSignin" runat="server" Text="Login" OnClick="btnSignin_Click" />
        </div>
    </form>
</body>
</html>
