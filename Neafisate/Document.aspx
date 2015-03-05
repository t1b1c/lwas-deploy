<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Document.aspx.cs" Inherits="Document" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts" TagPrefix="LWASWP" %>
<%@ Register Assembly="LWAS.Developer" Namespace="LWAS.Developer" TagPrefix="dev" %>
<%@ Register Src="~/menu/DropDownMenu.ascx" TagPrefix="menu" TagName="drop" %>
<%@ Register Src="~/menu/BottomMenu.ascx" TagPrefix="menu" TagName="bottom" %>

<asp:Content ContentPlaceHolderID="TitlePlaceholder" runat="server">
    <asp:Label ID="titleLabel" runat="server" Text="" />
</asp:Content>
<asp:Content ContentPlaceHolderID="aboveContent" runat="server">
    <menu:drop ID="upperDropMenu" runat="server" />
</asp:Content>