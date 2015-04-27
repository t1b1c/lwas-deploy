<%@ Page Title="Users" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Users.aspx.cs" Inherits="AccessControl_Users" Theme="Default" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts" TagPrefix="LWASWP" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts.Zones" TagPrefix="LWASWPZ" %>
<%@ Register Assembly="LWAS.Developer" Namespace="LWAS.Developer" TagPrefix="dev" %>
<%@ Register Assembly="LWAS.Developer" Namespace="LWAS.Developer.AccessControl" TagPrefix="devac" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <asp:Label ID="titleLabel" runat="server" class="title" Text="Users" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FreeContent" Runat="Server">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Users.aspx" Text="Users" />&nbsp;|&nbsp;
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="RolesUsers.aspx" Text="Roles and users" />

    <br />
    <p><hr /></p>

    <asp:UpdatePanel ID="hiddenUpdatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <devac:PasswordSet ID="passwordSet" runat="server" RequiresOldPassword="false" />
        </ContentTemplate>
    </asp:UpdatePanel> 
    <LWASWPZ:TableZone ID="ContentZone" runat="server" PersonalizationProviderID="personalizationProvider" WebPartVerbRenderMode="TitleBar">
        <ZoneTemplate>
            <LWASWP:GridViewWebPart ID="usersGrid" runat="server" Title="Users" AllowEdit="false" AllowClose="false" AllowMinimize="false" AllowHide="false" AllowZoneChange="false" ExportMode="None" />
        </ZoneTemplate>
    </LWASWPZ:TableZone>
    
    <LWASWPZ:CustomChromeZone id="LocalProviders" runat="server" Visible="false">
        <ZoneTemplate>
            <LWASWP:DataSourceProviderWebPart runat="server" ID="usersDataSource" Title="Users data source provider" />
        </ZoneTemplate>
    </LWASWPZ:CustomChromeZone>
</asp:Content>

