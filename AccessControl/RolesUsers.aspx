<%@ Page Title="Roles and users" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="RolesUsers.aspx.cs" Inherits="RolesUsers" EnableViewState="false" Theme="Default" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts" TagPrefix="LWASWP" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts.Zones" TagPrefix="LWASWPZ" %>
<%@ Register Assembly="LWAS.Developer" Namespace="LWAS.Developer" TagPrefix="dev" %>

<asp:Content ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <asp:Label ID="titleLabel" runat="server" class="title" Text="Roles and users" />
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Users.aspx" Text="Users" />&nbsp;|&nbsp;
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="RolesUsers.aspx" Text="Roles and users" />

    <br />
    <p><hr /></p>
    <LWASWPZ:TableZone ID="ContentZone" runat="server" PersonalizationProviderID="personalizationProvider" WebPartVerbRenderMode="TitleBar">
        <ZoneTemplate>
            <LWASWP:GridViewWebPart ID="rolesGrid" runat="server" Title="Roles" AllowEdit="false" AllowClose="false" AllowMinimize="false" AllowHide="false" AllowZoneChange="false" ExportMode="None" />
            <LWASWP:GridViewWebPart ID="usersGrid" runat="server" Title="Users" AllowEdit="false" AllowClose="false" AllowMinimize="false" AllowHide="false" AllowZoneChange="false" ExportMode="None" />
        </ZoneTemplate>
    </LWASWPZ:TableZone>
    
    <LWASWPZ:CustomChromeZone id="LocalProviders" runat="server" Visible="false">
        <ZoneTemplate>
            <LWASWP:DataSourceProviderWebPart runat="server" ID="rolesDataSource" Title="Roles data source provider" />
        </ZoneTemplate>
    </LWASWPZ:CustomChromeZone>
</asp:Content>

