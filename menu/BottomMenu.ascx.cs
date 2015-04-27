/*
 * Copyright 2015 Tiberiu Craciun, tiberiu.craciun@gmail.com
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
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

using LWAS.Infrastructure.Storage;

public partial class menu_BottomMenu : System.Web.UI.UserControl
{
    struct ScreenRoute
    {
        public string ScreenName;
        public string ScreenTitle;
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        string file_path = Server.MapPath("~/App_Data/config/routes.xml");
        FileAgent agent = new FileAgent();
        XDocument xdoc = XDocument.Parse(agent.Read(file_path));

        int count = 0;
        menuPlaceholder.Controls.Add(new LiteralControl("<ul class='miscMenu'>"));
        foreach (var o in WrapScreens(xdoc, "/routes/route[@name='Nomenclatoare']"))
        {
            if (count % 6 == 0)
            {
                if (count > 0)
                {
                    menuPlaceholder.Controls.Add(new LiteralControl("</ul>"));
                    menuPlaceholder.Controls.Add(new LiteralControl("<ul class='miscMenu'>"));
                }
            }

            menuPlaceholder.Controls.Add(new LiteralControl("<li><a href='" + this.ResolveUrl("~") + "Nomenclatoare/document.aspx?screen=" + o.ScreenName + "'>" + o.ScreenTitle + "</a></li>"));
            
            count++;
        }
        menuPlaceholder.Controls.Add(new LiteralControl("</ul>"));
    }

    IEnumerable<ScreenRoute> WrapScreens(XDocument xdoc, string path)
    {
        XElement root = xdoc.XPathSelectElement(path);
        if (null == root)
            yield break;
        foreach (XElement screen in root.Elements("screen"))
            yield return new ScreenRoute() { ScreenName = screen.Attribute("name").Value, ScreenTitle = screen.Attribute("name").Value };
    }
}