<%@ Page Language="C#" Theme="Rapoarte" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Raport.aspx.cs" Inherits="Raport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts" TagPrefix="LWASWP" %>
<%@ Register Src="~/menu/DropDownMenu.ascx" TagPrefix="menu" TagName="drop" %>

<asp:Content ContentPlaceHolderID="TitlePlaceholder" runat="server">
    <asp:Label ID="titleLabel" runat="server" Text="" />
</asp:Content>
<asp:Content ContentPlaceHolderID="aboveContent" runat="server">
    <menu:drop ID="upperDropMenu" runat="server" />
</asp:Content>