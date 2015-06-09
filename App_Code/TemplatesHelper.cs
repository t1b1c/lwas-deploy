/*
 * Copyrigt 2013 Tiberiu Craciun, tiberiu.craciun@gmail.com
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Configuration;

using LWAS.Extensible.Interfaces.Storage;

using LWAS.Infrastructure.Storage;

public class TemplatesHelper
{
    public ServicesHelper ServicesHelper { get; private set; }
    string root;

	public TemplatesHelper(ServicesHelper servicesHelper)
	{
        this.ServicesHelper = servicesHelper;
        root = "~/App_Data/templates/";
	}

    public IEnumerable<string> TemplateTypes()
    {
        return new string[] { "mail", "office/word", "office/excel" };
    }

    public IEnumerable<string> ListTemplates(string type)
    {
        var container = TemplateTypeRepo(type);
        return container.Agent.ListAll("*.*");
    }

    public void Delete(string type, string file)
    {
        var container = TemplateTypeRepo(type);
        container.Agent.Erase(file);
    }

    public IStorageContainer TemplateTypeRepo(string type)
    {
        string key = TemplateTypeConfigKey(type);
        string path = this.ServicesHelper.RoutingManager.SettingsRoutes[key].Path;
        return new DirectoryContainer(root, path);
    }

    public string TemplateTypeConfigKey(string type)
    {
        if ("mail" == type)
            return "MAIL_TEMPLATES";
        else if ("office/word" == type)
            return "DOCX_TEMPLATES";
        else if ("office/excel" == type)
            return "EXCEL_TEMPLATES";
        else
            return null;
    }
}