/*
 * Copyright 2006-2012 TIBIC SOLUTIONS
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

using LWAS.Extensible.Interfaces.Monitoring;
using Security = LWAS.Infrastructure.Security;
using LWAS.Infrastructure.Monitoring;
using LWAS.Drivers;
using LWAS.Infrastructure;

public partial class MonitorViewer : System.Web.UI.Page
{
    Monitor _monitor;
    Monitor Monitor
    {
        get
        {
            if (null == _monitor)
				_monitor = ((Manager)WebPartManager.GetCurrentWebPartManager(this.Page)).Monitor as Monitor;
            return _monitor;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.Load += new EventHandler(MonitorViewer_Load);
        this.clearButton.Click += new EventHandler(clearButton_Click);
        this.storageRepeater.ItemCommand += new RepeaterCommandEventHandler(storageRepeater_ItemCommand);

        this.Monitor.IsDisabled = true;
    }

    void MonitorViewer_Load(object sender, EventArgs e)
    {
        BindRepeater();
    }

    void clearButton_Click(object sender, EventArgs e)
    {
        this.Monitor.Entries.Clear();
        BindRepeater();
        recordsLiteral.Text = null;
    }

    void storageRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if ("view" == e.CommandName)
            DisplayRecords((string)e.CommandArgument);
    }

    void BindRepeater()
    {
        storageRepeater.DataSource = this.Monitor.Entries;
        storageRepeater.DataBind();
    }

    void DisplayRecords(string entry)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(this.Monitor.Entries[entry]);
        XmlNode root = doc.SelectSingleNode("Monitor/records");
        List<XmlNode> shownNodes = new List<XmlNode>();
        string str = "";

        XmlNodeList jobNodes = root.SelectNodes("//record[event[contains(@key, 'run job')]]");
        List<XmlNode> jobNodesList = new List<XmlNode>();
        foreach(XmlNode node in jobNodes)
            jobNodesList.Add(node);

        foreach (XmlNode node in root.ChildNodes)
        {
            string stamp = node.Attributes["stamp"].Value;
            XmlNode sourceNode = node.SelectSingleNode("source");
            XmlNode eventNode = node.SelectSingleNode("event");
            if (eventNode.Attributes["key"].Value == "waitfor passed")
                str += "<span class='monitor_viewer_panel_condition'>" + sourceNode.Attributes["title"].Value + "</span>";

            if (jobNodesList.Contains(node))
                str += DisplayJob(root, node);
            else if (eventNode.Attributes["key"].Value == "waitfor passed")
            {
                XmlNode conditionexpressionNode = root.SelectSingleNode("//record[@stamp='" + stamp + "']/following-sibling::record[1]");
                str += DisplayExpression(conditionexpressionNode, false);
            }
        }
        recordsLiteral.Text = str;
    }

    string DisplayExpression(XmlNode recordNode, bool onNewLine)
    {
        string str = "";
        if (null == recordNode) return str;

        XmlNode eventNode = recordNode.SelectSingleNode("event");
        string expression = "";
        bool issuccess = true;
        string val = eventNode.Attributes["key"].Value;
        if (val.StartsWith("expression "))
        {
            expression = val;
            if (val.StartsWith("expression failed"))
                issuccess = false;
        }
        if (!String.IsNullOrEmpty(expression))
        {
            if (onNewLine)
                str += "<br />";
            str += "<span class='" + (issuccess ? "monitor_viewer_panel_expression_success" : "monitor_viewer_panel_expression_fail") + "'>" + expression + "</span>";
            if (issuccess)
                str += "<br />";
        }

        return str;
    }

    string DisplayJob(XmlNode root, XmlNode jobNode)
    {
        string str = "";
        XmlNode sourceNode = jobNode.SelectSingleNode("source");
        string sourceTitle = "";
        if (null != sourceNode)
            sourceTitle = sourceNode.Attributes["title"].Value;
        if (!String.IsNullOrEmpty(sourceTitle))
        {
            str += "<span class='monitor_viewer_panel_job'>" + sourceTitle + "</span>";

            XmlNodeList transitNodes = root.SelectNodes("//record[event[parent[@key='" + sourceTitle + "']]]");
            if (transitNodes.Count > 0)
                str += "<ul>";
            foreach (XmlNode node in transitNodes)
                str += DisplayTransit(root, node);
            if (transitNodes.Count > 0)
                str += "</ul>";
        }
        return str;
    }

    string DisplayTransit(XmlNode root, XmlNode transitNode)
    {
        string str = "";
        XmlNode eventNode = transitNode.SelectSingleNode("event");
        str += "<li>";
        str += eventNode.Attributes["key"].Value.Substring(eventNode.Attributes["key"].Value.LastIndexOf("::") + "::".Length);
        XmlNodeList pointNodes = root.SelectNodes("//record[event[parent[@key='" + eventNode.Attributes["key"].Value + "']]]");
        string source = "";
        string destination = "";
        XmlNode expressionNode = null;
        foreach (XmlNode node in pointNodes)
        {
            XmlNode pointEventNode = node.SelectSingleNode("event");
            XmlNode dataNode = pointEventNode.SelectSingleNode("data");
            string val = pointEventNode.Attributes["key"].Value;
            if (val.StartsWith("expression "))
                expressionNode = node;
            if (val.StartsWith("set "))
            {
                destination = val.Substring("set ".Length);
                if (null != dataNode && null != dataNode.Attributes["trace"])
                    source = dataNode.Attributes["trace"].Value;
            }
            if (String.IsNullOrEmpty(source) && val.StartsWith("get "))
                source = val.Substring("get ".Length);
        }

        if (null != expressionNode)
            str += DisplayExpression(expressionNode, true);

        if (!eventNode.Attributes["key"].Value.Contains("="))
        {
            if (!String.IsNullOrEmpty(destination) || !String.IsNullOrEmpty(source))
                str += "<br /><span class='monitor_viewer_panel_transit'>";
            if (!String.IsNullOrEmpty(destination))
                str += destination + " = ";
            if (!String.IsNullOrEmpty(source))
                str += source;
            if (!String.IsNullOrEmpty(destination) || !String.IsNullOrEmpty(source))
                str += "</span>";
        }
        str += "</li>";
        return str;
    }
}