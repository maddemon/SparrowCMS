using SparrowCMS.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ExampleSite
{
    public partial class Setup : System.Web.UI.Page
    {
        public void Page_Load(object sender, EventArgs e)
        {
            var step = string.Empty;
            if (string.IsNullOrEmpty(step))
            {
                if (Request.HttpMethod == "POST")
                {
                    ModifyConfig();
                    Response.Redirect("/setup.aspx?step=initdata", true);
                }
            }
            else if(step == "initdata")
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

                var siteNode = new XElement("add");
                siteNode.SetAttributeValue("key", "SiteName");
                siteNode.SetAttributeValue("value", Request["SiteName"]);

                appNode.Add(dbTypeNode);
                appNode.Add(siteNode);

                doc.Root.Add(appNode);
            }

            doc.Save(configPath);

        }

        public void InitData()
        {
            //初始化数据
            var pageDao = DataProviderFactory.GetProvider<SparrowCMS.Base.IDataProvider.IPageDataProvider>();
            pageDao.AddPage(new SparrowCMS.Base.Page { });
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
                handlerNode = new XElement("httpHandlers");
                var item = new XElement("add");
                item.Name = "add";
                item.SetAttributeValue("verb", "*");
                item.SetAttributeValue("path", "*");
                item.SetAttributeValue("type", "SparrowCMS.Base.PageHandlerFactory");
                handlerNode.Add(item);

                webNode.Add(handlerNode);
            }

            var serverNode = doc.Root.Element("system.webServer");
            var handlerNode1 = serverNode.Element("handlers");
            if (handlerNode1 == null)
            {
                handlerNode1 = new XElement("handlers");

                var item = new XElement("add");
                item.Name = "add";
                item.SetAttributeValue("name", "pageHandlerFactory");
                item.SetAttributeValue("verb", "*");
                item.SetAttributeValue("path", "*");
                item.SetAttributeValue("type", "SparrowCMS.Base.PageHandlerFactory");

                handlerNode1.Add(item);

                serverNode.Add(handlerNode1);
            }

            doc.Save(configPath);
        }
    }
}