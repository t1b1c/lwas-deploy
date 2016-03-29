<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" Theme="Default" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='https://www.google.com/recaptcha/api.js'></script>
</head>
<body>
    <form id="form1" runat="server">
    <LWASI:Manager ID="webPartManager" runat="server" />

    <div id="loginPanel">
        <div id="loginContainer">
            <asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate" 
                Height="200px" Width="400px" DisplayRememberMe="False" EnableTheming="True"  
                TitleText="" LoginButtonText="Login" 
                PasswordLabelText="Parola" PasswordRequiredErrorMessage="Parola este obligatorie" 
                UserNameLabelText="Utilizator" UserNameRequiredErrorMessage="Utilizatorul este obligatoriu"
                FailureText="Ceva e gresit. Incearca din nou!">
            </asp:Login>
        </div>
        <div  id="captchaContainer" style="padding: 40px 20px;">
            <div class="g-recaptcha" data-sitekey="6LcLPOwSAAAAABYdZOuzEsoliE0xJzQuzbHDL4L2"></div>
        </div>
    </div>
    </form>
</body>
</html>
