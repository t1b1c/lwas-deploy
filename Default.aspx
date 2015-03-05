<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Theme="Default" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts" TagPrefix="LWASWP" %>
<%@ Register Src="~/menu/DropDownMenu.ascx" TagPrefix="menu" TagName="drop" %>
<%@ Register Src="~/menu/BottomMenu.ascx" TagPrefix="menu" TagName="bottom" %>

<asp:Content ContentPlaceHolderID="TitlePlaceholder" runat="server">
    <asp:Label ID="titleLabel" runat="server" class="title" Text="" />
</asp:Content>
<asp:Content ContentPlaceHolderID="aboveContent" runat="server">
    <menu:drop ID="upperDropMenu" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="bellowContent" runat="server">
    <menu:bottom ID="bottomMenu" runat="server" />
</asp:Content>