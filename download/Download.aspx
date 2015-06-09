<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Download" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.Infrastructure" TagPrefix="LWASI" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts" TagPrefix="LWASWP" %>
<%@ Register Assembly="LWAS" Namespace="LWAS.WebParts.Zones" TagPrefix="LWASWPZ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <LWASI:Manager ID="webPartManager" runat="server" EnableAuthorizer="true" />
    <div>
        <LWASWPZ:TableZone ID="Infrastructure" runat="server" PersonalizationProviderID="personalizationProvider" WebPartVerbRenderMode="TitleBar" AllowLayoutChange="false" HeaderText="Infrastructure zone">
        <ZoneTemplate>
            <LWASWP:WorkFlowManagerWebPart ID="workflowManagerWebPart" Initialization="1001" Completion="1001" Title="WorkFlow manager" runat="server" 
                            ChromeType="None" AllowClose="false" AllowZoneChange="false" ExportMode="All" />
        </ZoneTemplate>
        </LWASWPZ:TableZone>
    </div>
    </form>
</body>
</html>
