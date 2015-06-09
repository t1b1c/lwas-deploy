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
using System.Text;
using AjaxControlToolkit;
using ClosedXML;
using ClosedXML.Excel;

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
using LD=LWAS.Database;

public partial class import_sablon : Page
{
    ServicesHelper ServicesHelper;
    TemplatesHelper TemplatesHelper;

    FormViewWebPart formaAplicatieCurenta;
    GridViewWebPart gridTipuriSablon;
    GridViewWebPart gridSabloane;

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);

        this.ServicesHelper = new ServicesHelper(this);
        this.TemplatesHelper = new TemplatesHelper(this.ServicesHelper);

        formaAplicatieCurenta = this.ServicesHelper.PagePart<FormViewWebPart>("formaAplicatieCurenta");
        gridTipuriSablon = this.ServicesHelper.PagePart<GridViewWebPart>("gridTipuriSablon");
        gridSabloane = this.ServicesHelper.PagePart<GridViewWebPart>("gridSabloane");

        gridTipuriSablon.MilestoneHandler += new MilestoneEventHandler(gridTipuriSablon_MilestoneHandler);
        gridSabloane.MilestoneHandler += new MilestoneEventHandler(gridSabloane_MilestoneHandler);
    }

    void gridTipuriSablon_MilestoneHandler(IChronicler chronicler, string key)
    {
        if ("view" == key)
        {
            gridTipuriSablon.ReceiveData = WrapTypes();
        }
        else if ("item" == key)
        {
            foreach (Dictionary<string, object> datas in gridTipuriSablon.Items.Select(i => i.Data as Dictionary<string, object>))
                datas["Active"] = null;
            gridTipuriSablon.CurrentField<string>("Active", "yes");

            gridSabloane.Command = "view";
        }
    }

    void gridSabloane_MilestoneHandler(IChronicler chronicler, string key)
    {
        if ("view" == key)
        {
            if (null != gridTipuriSablon.CurrentItem)
            {
                string type = gridTipuriSablon.CurrentField<string>("TemplateType");
                gridSabloane.ReceiveData = WrapList(type);
            }
        }
        else if ("download" == key)
        {
            string type = gridTipuriSablon.CurrentField<string>("TemplateType");
            string file = gridSabloane.CurrentField<string>("FileName");

            this.ServicesHelper.workflowManager.PersistData("DownloadPath", this.TemplatesHelper.TemplateTypeRepo(type).ToString());
            this.ServicesHelper.workflowManager.PersistData("DownloadFile", file);

            this.ServicesHelper.Download();

        }
        else if ("delete" == key)
        {
            string type = gridTipuriSablon.CurrentField<string>("TemplateType");
            string file = gridSabloane.CurrentField<string>("FileName");
            this.TemplatesHelper.Delete(type, file);
        }
    }

    IEnumerable<object> WrapTypes()
    {
        foreach (string t in this.TemplatesHelper.TemplateTypes())
        {
            string key = this.TemplatesHelper.TemplateTypeConfigKey(t);
            string path = this.ServicesHelper.RoutingManager.SettingsRoutes[key].Path;

            yield return new { TemplateType = t, Path = path };
        }
    }

    IEnumerable<object> WrapList(string type)
    {
        foreach (string f in this.TemplatesHelper.ListTemplates(type))
            yield return new { FileName = f };
    }
}