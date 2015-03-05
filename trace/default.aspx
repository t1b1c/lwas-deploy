<%@ Page Title="Workflow trace" Language="C#" MasterPageFile="~/MasterPage.master" Theme="Default" AutoEventWireup="false" CodeFile="default.aspx.cs" Inherits="MonitorViewer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceholder" Runat="Server">
    <span class="title">Workflow trace</span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <br />
    <asp:Button ID="clearButton" runat="server" Text="Reset" />
    <asp:Repeater ID="storageRepeater" runat="server">
        <ItemTemplate>
            <asp:LinkButton runat="server" CommandName="view" CommandArgument="<%# Container.DataItem %>" Text="<%# Container.DataItem %>" />
        </ItemTemplate>
        <SeparatorTemplate>
            &nbsp;|&nbsp;
        </SeparatorTemplate>
    </asp:Repeater>

    <br /><hr /><p />

    <div class="monitor_viewer_panel">
        <asp:Literal ID="recordsLiteral" runat="server" />
    </div>
</asp:Content>


