<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" Theme="Default" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <LWASI:Manager ID="webPartManager" runat="server" />

    <div id="loginPanel">
        <div id="loginContainer">
        <asp:Login ID="Login1" runat="server" onauthenticate="Login1_Authenticate" 
            Height="200px" Width="400px" DisplayRememberMe="False" EnableTheming="True"  
            TitleText="" LoginButtonText="Login" 
            PasswordLabelText="Password" PasswordRequiredErrorMessage="Password is mandatory" 
            UserNameLabelText="User" UserNameRequiredErrorMessage="User is mandatory"
            FailureText="Your credentials are wrong. Try again!">
        </asp:Login>
        </div>
        <div id="captchaContainer">
        <recaptcha:RecaptchaControl
            ID="recaptcha"
            runat="server"
            PublicKey=""
            PrivateKey=""
            Theme="white"
            />
        </div>
    </div>
    </form>
</body>
</html>
