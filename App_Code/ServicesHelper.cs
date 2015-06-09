/*
 * Copyrigt 2013 Tiberiu Craciun, tiberiu.craciun@gmail.com
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using LWAS.Extensible.Interfaces.WorkFlow;
using LWAS.Extensible.Interfaces.WebParts;
using LWAS.Extensible.Interfaces.Storage;
using LWAS.Extensible.Interfaces.Expressions;
using LWAS.Extensible.Interfaces.Routing;
using LWAS.Extensible.Interfaces.Monitoring;

using LWAS.Infrastructure;
using LWAS.Infrastructure.Storage;
using LWAS.Drivers;
using LWAS.WebParts;
using LD = LWAS.Database;

public class ServicesHelper
{
    public Page Page { get; private set; }

	public ServicesHelper(Page page)
	{
        this.Page = page;
    }

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

    WorkFlowManagerWebPart _workflowManager;
    public WorkFlowManagerWebPart workflowManager
    {
        get
        {
            if (null == _workflowManager)
                _workflowManager = PagePart<WorkFlowManagerWebPart>("workflowManagerWebPart");
            return _workflowManager;
        }
    }

    SqlDataBridgeWebPart _commander;
    public SqlDataBridgeWebPart commander
    {
        get
        {
            if (null == _commander)
                _commander = PagePart<SqlDataBridgeWebPart>("commander");
            return _commander;
        }
    }

    IRoutingManager _routingManager;
    public IRoutingManager RoutingManager
    {
        get
        {
            if (null == _routingManager)
                _routingManager = this.Manager.RoutingManager;
            return _routingManager;
        }
    }

    IExpressionsManager _expressionsManager;
    public IExpressionsManager ExpressionsManager
    {
        get
        {
            if (null == _expressionsManager)
                _expressionsManager = this.Manager.ExpressionsManager;
            return _expressionsManager;
        }
    }

    LD.ViewsManager _viewsManager;
    public LD.ViewsManager ViewsManager
    {
        get
        {
            if (null == _viewsManager)
            {
                FileAgent agent = new FileAgent();
                string views_config = this.RoutingManager.SettingsRoutes["VIEWS_CONFIG"].Path;

                _viewsManager = new LD.ViewsManager(views_config, agent, this.ExpressionsManager);
            }
            return _viewsManager;
        }
    }

    public T PagePart<T>(string id) where T : WebPart
    {
        return WebPartManager.GetCurrentWebPartManager(this.Page).WebParts[id] as T;
    }

    public void NavigateTo(string relativePath)
    {
        string rootedPath = Path.Combine("~", relativePath);
        this.Page.Response.Redirect(rootedPath, false);
    }

    public void Download()
    {
        NavigateTo("download/download.aspx");
    }
}