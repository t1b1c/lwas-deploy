<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BottomMenu.ascx.cs" Inherits="menu_BottomMenu" %>

<div class="bottomMenuContainer">
    <asp:PlaceHolder ID="menuPlaceholder" runat="server" />
    <ul class="miscMenu">
    <asp:Repeater ID="miscRepeater" runat="server">
    <ItemTemplate>
        <li><a runat="server" href='<%# "~/ecrane/document.aspx?screen=" + Eval("ScreenName") %>'><%# Eval("ScreenTitle") %></a></li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>
</div>