<%@ Control Language="C#" AutoEventWireup="false" CodeFile="DropDownMenu.ascx.cs" Inherits="menu_DropDownMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<span id="menuHead1" runat="server" class="mainMenuHead">Ecrane</span>
<ul id="menu1" runat="server" class="mainMenu" style="display:none">
    <asp:Repeater ID="repeater1" runat="server">
    <ItemTemplate>
        <li><a runat="server" href='<%# "~/ecrane/document.aspx?screen=" + Eval("ScreenName") %>'><%# Eval("ScreenTitle") %></a></li>
    </ItemTemplate>
    </asp:Repeater>
</ul>
<span id="menuHead2" runat="server" class="mainMenuHead">Rapoarte</span>
<ul id="menu2" runat="server" class="mainMenu" style="display:none">
    <asp:Repeater ID="repeater2" runat="server">
    <ItemTemplate>
        <li><a  runat="server" href='<%# "~/rapoarte/raport.aspx?screen=" + Eval("ScreenName") %>'><%# Eval("ScreenTitle") %></a></li>
    </ItemTemplate>
    </asp:Repeater>
</ul>

<ajax:DropDownExtender runat="server" ID="menu1Ex" BehaviorID="menu1Behaviour" TargetControlID="menuHead1" DropDownControlID="menu1" />
<ajax:DropDownExtender runat="server" ID="menu2Ex" BehaviorID="menu2Behaviour" TargetControlID="menuHead2" DropDownControlID="menu2" />
