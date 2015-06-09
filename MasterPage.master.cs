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
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Net;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Globalization;
using System.Xml;
using System.IO;

using LWAS.Extensible.Interfaces.Monitoring;
using LWAS.Extensible.Interfaces.WorkFlow;

using LWAS.Infrastructure.Storage;
using LWAS.Infrastructure.Security;
using LWAS.Drivers;
using LWAS.Developer;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public MasterPage()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("ro-RO");
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ro-RO");
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //if (!Request.IsSecureConnection)
        //Response.Redirect(Request.Url.ToString().Replace( "http://" , "https://"));

        Response.Cache.SetNoServerCaching();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1.1));
		
		Response.Cache.SetValidUntilExpires(false);
		Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
		Response.Cache.SetNoStore();

        if (!Page.ClientScript.IsStartupScriptRegistered(this.GetType(), "MaskedEditFix"))
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MaskedEditFix", String.Format("<script type='text/javascript' src='{0}'></script>", Page.ResolveUrl("~/scripts/MaskedEditFix.js")));

        commander.MilestoneHandler += new MilestoneEventHandler(commander_MilestoneHandler);
        this.Page.InitComplete += new EventHandler(Page_InitComplete);

        passwordChange.Visible = passwordChange.IsShown;
    }

    void Page_InitComplete(object sender, EventArgs e)
    {
        Settings settings = new Settings(this.Page);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(settings.Culture);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.Culture);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string screen = null;
        if (null != this.Page && null != this.Page.Request.QueryString["screen"])
            screen = this.Page.Request.QueryString["screen"];

        if (!String.IsNullOrEmpty(screen) && !AccessVerifier.Instance.HasAccess(screen))
            Response.Redirect("~/");

        menuLabel.Text = User.CurrentUser.Name;
    }

    void commander_MilestoneHandler(IChronicler chronicler, string key)
    {
        if ("create" == key)
        {
            string app = IdentifyApp();
            if (!String.IsNullOrEmpty(app))
                commander.Connection = app;
        }
    }

    protected void logoutButton_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void changePassword_Click(object sender, EventArgs e)
    {
        this.passwordChange.Visible = true;
        this.passwordChange.Show();
    }

    public string IdentifyApp()
    {
        return "atest";
    }
}