<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="sablon.aspx.cs" Inherits="import_sablon" %>
<%@ Register Src="~/menu/DropDownMenu.ascx" TagPrefix="menu" TagName="drop" %>
<%@ Register Src="~/menu/BottomMenu.ascx" TagPrefix="menu" TagName="bottom" %>

<asp:Content ContentPlaceHolderID="TitlePlaceholder" runat="server">
    <asp:Label ID="titleLabel" runat="server" class="title" Text="Import sablon" />
</asp:Content>
<asp:Content ContentPlaceHolderID="aboveContent" runat="server">
    <menu:drop ID="upperDropMenu" runat="server" />
</asp:Content>