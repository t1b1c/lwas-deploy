/*
 * Copyright 2006-2010 TIBIC SOLUTIONS
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Collections.Generic;

using LWAS.Extensible.Interfaces.WorkFlow;

using Security = LWAS.Infrastructure.Security;
using LWAS.WebParts.Editors;
using LWAS.Developer;

public partial class AccessControl_Users : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        //if ("admin" != Security.User.CurrentUser.Name)
        //    Response.Redirect("../", true);

        base.OnInit(e);

        this.usersGrid.MilestoneHandler += new MilestoneEventHandler(usersGrid_MilestoneHandler);
    }

    protected override void OnPreLoad(EventArgs e)
    {
        InitUsers();

        base.OnPreLoad(e);
    }

    void usersGrid_MilestoneHandler(IChronicler chronicler, string key)
    {
        if ("firstload" == key)
            usersGrid.Command = "view";
        if ("reset" == key)
        {
            string user = null;
            if (null != this.usersGrid.CurrentItem)
                user = (string)((Dictionary<string, object>)usersGrid.CurrentItem.Data)["Name"];
            if (!String.IsNullOrEmpty(user))
            {
                this.passwordSet.App = this.Master.IdentifyApp();
                this.passwordSet.UserName = user;
                this.passwordSet.Show();
            }
        }
    }

    protected void InitUsers()
    {
        string file = ConfigurationManager.AppSettings["USERS"];
        if (String.IsNullOrEmpty(file)) throw new InvalidOperationException("USERS key not set in config file");

        this.usersGrid.DataSource = new UsersDataSource(file, null);
    }
}