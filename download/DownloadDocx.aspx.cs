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
using System.IO;

using LWAS.Infrastructure.Security;

using LWAS.Developer;

public partial class DownloadDocx : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string file = this.Request["file"];
        if (!String.IsNullOrEmpty(file))
            SendFile(file);
    }

    void SendFile(string file)
    {
        Page.Response.Clear();

        Page.Response.ContentType = "application/ms-word";
        Page.Response.AddHeader("Content-disposition", "attachment; filename=" + file + ".docx");

        file = Path.Combine((new UserData()).RootPath, file + ".docx");
        if (File.Exists(file))
            Page.Response.WriteFile(file);

        Response.End();
    }
}