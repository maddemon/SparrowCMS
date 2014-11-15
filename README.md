#执行流程
    WebBrowser Visited Url -> //用户访问了一个URL
    CMSHttpModule -> CMSPageHandler -> //自定义HttpHandler
    Context.Init() //初始化上下文，其中包括获取站点信息、页面信息
      site = GetSite(Url.Host)  -> 
      page =  site.GetPages.Where(p=> p.IsMatch(Url.Path)  -> 
      page.Init() //初始化Page实例，
        template.Init()//Page的模板实例初始化
          templateContent = Template.getContent();
          labels = Label.FindLabels(templateContent);//获取所有的标签(以后改成control控件）
            labelDescription.Init();
              label.Parameters = labelDescription.getParamters() //获取所有的参数实例
              label.FieldDecriptions =  labelDescription.getFields()//获取所有的Field描述
                fieldDescription.Attributes //获取字段的属性
              label.InnerLabels // 获取所有的内嵌Label
        RouteData
        ViewData
        
        template.GetRepalcedContent()//获取替换后的html，并展现
          labelDescriptions.ForEach(label.GetReplacedContent())//遍历LabelDescriptions，实例化具体的Label并替换html
            FieldDescription.GetReplacedContent()
