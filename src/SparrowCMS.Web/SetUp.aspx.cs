using SparrowCMS.Core.DataProviders;
using SparrowCMS.Core.Managers;
using SparrowCMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SparrowCMS.Web
{
    public partial class Setup : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            var step = Request.QueryString["step"];
            if (string.IsNullOrEmpty(step))
            {
                if (Request.HttpMethod == "POST")
                {
                    ModifyConfig();
                    Response.Redirect("/setup.aspx?step=initdata", true);
                }
            }
            else if (step == "initdata")
            {
                InitData();
                Response.Redirect("/setup.aspx?step=handlers", true);
            }
            else if (step == "handlers")
            {
                RegisterHandlers();
                Response.Write("配置完成");
                Response.End();
            }
        }

        public void ModifyConfig()
        {
            //修改Config
            var configPath = Request.MapPath("~/web.config");
            var connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=SparrowCMS;Integrated Security=True";

            var doc = XDocument.Load(configPath);
            var connNode = doc.Root.Element("connectionStrings");
            if (connNode == null)
            {
                connNode = new XElement("connectionStrings");
                var item = new XElement("add");
                item.Name = "add";
                item.SetAttributeValue("name", "CMSBaseConnectionString");
                item.SetAttributeValue("connectionString", connectionString);
                item.SetAttributeValue("providerName", "System.Data.SqlClient");
                connNode.Add(item);
                doc.Root.Add(connNode);
            }
            var appNode = doc.Root.Element("appSettings");
            if (appNode == null)
            {
                appNode = new XElement("appSettings");
                var dbTypeNode = new XElement("add");
                dbTypeNode.Name = "add";
                dbTypeNode.SetAttributeValue("key", "DbType");
                dbTypeNode.SetAttributeValue("value", "SqlServer");

                appNode.Add(dbTypeNode);

                doc.Root.Add(appNode);
            }

            doc.Save(configPath);

        }

        public void InitData()
        {
            //add default site
            var request = HttpContext.Current.Request;
            var site = new Site
            {
                Name = request["SiteName"],
            };

            SiteManager.AddSite(site);
        }

        public void RegisterHandlers()
        {
            //修改Config
            var configPath = Request.MapPath("~/web.config");

            var doc = XDocument.Load(configPath);


            var webNode = doc.Root.Element("system.web");
            var handlerNode = webNode.Element("httpHandlers");
            if (handlerNode == null)
            {
                handlerNode = new XElement("httpModules");

                var item = new XElement("add");
                item.Name = "add";
                item.SetAttributeValue("name", "CMSHttpModule");
                item.SetAttributeValue("type", "SparrowCMS.Core.CMSHttpModule");
                handlerNode.Add(item);

                webNode.Add(handlerNode);
            }

            var serverNode = doc.Root.Element("system.webServer");
            if (serverNode == null)
            {
                serverNode = new XElement("system.webServer");
                var validationNode = new XElement("validation");
                validationNode.SetAttributeValue("validateIntegratedModeConfiguration", "false");
                serverNode.Add(validationNode);

                doc.Root.Add(serverNode);
            }
            var handlerNode1 = serverNode.Element("modules");
            if (handlerNode1 == null)
            {
                handlerNode1 = new XElement("handlers");

                var item = new XElement("add");
                item.Name = "add";
                item.SetAttributeValue("name", "CMSHttpModule");
                item.SetAttributeValue("type", "SparrowCMS.Core.CMSHttpModule");

                handlerNode1.Add(item);

                serverNode.Add(handlerNode1);
            }

            doc.Save(configPath);
        }
    }
}