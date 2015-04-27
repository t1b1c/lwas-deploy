using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Configuration;

using LWAS.Infrastructure.Security;
using LWAS.Developer;
using LWAS.WebParts;

public partial class Download : System.Web.UI.Page
{
    WorkFlowManagerWebPart _workflowManager;
    WorkFlowManagerWebPart workflowManager
    {
        get
        {
            if (null == _workflowManager)
                _workflowManager = WebPartManager.GetCurrentWebPartManager(this).WebParts["workflowManagerWebPart"] as WorkFlowManagerWebPart;
            return _workflowManager;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string root = ConfigurationManager.AppSettings["UPLOADS_REPO"];
        string path = workflowManager.StatePersistence.Pull("DownloadPath") as string;
        if (path.StartsWith("/"))
            path = path.Remove(0, 1);
        string file = workflowManager.StatePersistence.Pull("DownloadFile") as string;
        string file_path = Path.Combine(path, Path.GetFileName(file));
        file_path = Path.Combine(root, file_path);
        if (!String.IsNullOrEmpty(file_path))
            SendFile(file_path);
    }

    void SendFile(string file)
    {
        Page.Response.Clear();

        Page.Response.ContentType = "application/octet-stream";
        Page.Response.AddHeader("Content-disposition", "attachment; filename=" + Path.GetFileName(file).Replace(" ", "_"));

        if (!Path.IsPathRooted(file))
            file = Server.MapPath(ResolveUrl(file));
        if (File.Exists(file))
            Page.Response.WriteFile(file);

        Response.End();
    }
}