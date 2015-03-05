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

using LWAS.Infrastructure.Security;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Settings settings = new Settings(this);
        this.Title = settings.TitleOnPage;
        Login1.TitleText = settings.TitleOnScreen;
		
        Page.Validate();
        ((TextBox)Login1.FindControl("UserName")).Focus();
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (!Page.IsValid)
            e.Authenticated = false;
        else
            e.Authenticated = Authenticator.Instance.Verify(this.Login1.UserName, this.Login1.Password);
    }
}
