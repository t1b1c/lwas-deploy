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
using LD = LWAS.Database;

public static class Extensions
{
    public static T CurrentField<T>(this ContainerWebPart container, string name)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
        if (null == container.CurrentItem) throw new ArgumentException(String.Format("ContainerWebPart '{0}' has no current item", container.ID));

        Dictionary<string, object> data = (Dictionary<string, object>)container.CurrentItem.Data;
        if (!data.ContainsKey(name)) throw new ArgumentException(String.Format("ContainerWebPart '{0}' has no field '{1}'", container.ID, name));

        return (T)data[name];
    }

    public static void CurrentField<T>(this ContainerWebPart container, string name, T value)
    {
        if (String.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
        if (null == container.CurrentItem) throw new ArgumentException(String.Format("ContainerWebPart '{0}' has no current item", container.ID));

        Dictionary<string, object> data = (Dictionary<string, object>)container.CurrentItem.Data;
        
        data[name] = value;
    }

    public static void RegisterMessage(this ContainerWebPart container, string message)
    {
        container.Monitor.Register(container, container.Monitor.NewEventInstance(message, EVENT_TYPE.Info));
    }

    public static void RegisterError(this ContainerWebPart container, string error, Exception exception)
    {
        container.Monitor.Register(container, container.Monitor.NewEventInstance(error, null, exception, EVENT_TYPE.Error));
    }

    public static T GetPersistedData<T>(this WorkFlowManagerWebPart workflowManager, string key)
    {
        if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

        if (!workflowManager.StatePersistence.ContainsKey(key))
            return default(T);
        return (T)workflowManager.StatePersistence.Pull(key);
    }

    public static void PersistData<T>(this WorkFlowManagerWebPart workflowManager, string key, T value)
    {
        if (String.IsNullOrEmpty(key)) throw new ArgumentNullException("key");

        workflowManager.StatePersistence.Push(key, value);
    }
}