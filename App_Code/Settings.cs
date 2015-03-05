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
using System.Xml.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using LWAS.Infrastructure;
using LWAS.Infrastructure.Routing;

public class Settings
{
    XDocument settings;
    public Page Page { get; private set; }
    
    Manager _manager;
    public Manager Manager
    {
        get
        {
            if (null == _manager)
                _manager = ((Manager)WebPartManager.GetCurrentWebPartManager(this.Page));
            return _manager;
        }
    }

    public string TitleOnPage
    {
        get
        {
            return settings.Element("settings")
                           .Element("title")
                           .Attribute("page")
                           .Value;
        }
    }

    public string TitleOnScreen
    {
        get
        {
            return settings.Element("settings")
                           .Element("title")
                           .Attribute("screen")
                           .Value;
        }
    }

    public string Culture
    {
        get
        {
            string culture = "ro-RO";
            XElement localeElement = settings.Element("settings")
                                             .Element("locale");

            if (null != localeElement)
                return localeElement.Attribute("culture").Value;
            else
                return culture;
        }
    }

    public Settings(Page page)
    {
        this.Page = page;
        string settings_file = this.Manager.RoutingManager.SettingsRoutes["SETTINGS_FILE"].Path;
        settings = XDocument.Load(settings_file);
    }
}