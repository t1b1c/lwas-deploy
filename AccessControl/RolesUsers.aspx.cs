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

public partial class RolesUsers : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        //if ("admin" != Security.User.CurrentUser.Name)
        //    Response.Redirect("../", true);

        base.OnInit(e);

        this.rolesGrid.MilestoneHandler += new MilestoneEventHandler(rolesGrid_Milestone);
        this.usersGrid.MilestoneHandler += new MilestoneEventHandler(usersGrid_MilestoneHandler);
    }

    protected override void OnPreLoad(EventArgs e)
    {
        InitRoles();

        base.OnPreLoad(e);
    }

    void rolesGrid_Milestone(IChronicler chronicler, string key)
    {
        if ("firstload" == key)
            rolesGrid.Command = "view";
        else if ("view" == key)
            rolesGrid.DataSource = rolesDataSource.DataSource as RolesDataSource;
        else if ("item" == key)
        {
            usersGrid.Command = "view";
        }
    }

    void usersGrid_MilestoneHandler(IChronicler chronicler, string key)
    {
        if ("view" == key || "page" == key || "save" == key || "delete" == key)
        {
            usersGrid.DataSource = rolesDataSource.DataSource as RolesDataSource;
            string role = null;
            if (null != rolesGrid.CurrentItem)
                role = (string)((Dictionary<string, object>)rolesGrid.CurrentItem.Data)["Name"];
            if (!String.IsNullOrEmpty(role))
                usersGrid.DataMember = role;
        }
    }

    protected void InitRoles()
    {
        string file = ConfigurationManager.AppSettings["ROLES"];
        if (String.IsNullOrEmpty(file)) throw new InvalidOperationException("ROLES key not set in config file");

        this.rolesDataSource.DataSource = new RolesDataSource(file, null);
    }
}
