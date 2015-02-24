SPARROW CMS
===================================
该CMS系统是本人业余时间开发，进度无法保证。如果您对此系统感兴趣可以与我联系，一起开发。 ^_^
###处理流程
    
    URL -> Site -> Page -> Template -> Label -> Field -> return GetReplacedContent()
    系统的运行原理是通过URL定位站点（Site）页面（Page）和模板（Template）,
    并对模板的内容进行正则查找，找到所有的标签（Label），
    根据标签的描述进行反射处理，找对标签对应的类，并执行标签的方法替换掉标签在模板的内容。
    

###系统特色
* **插件开发**：可以针对不同的业务开发不同的业务插件模块，并通过标签来使用插件提供的功能。
* **模板标签**：系统所有页面包括前端、后台均是模板生成，所以系统什么样子完全由模板决定。


####插件开发
由于用户不需要编写程序，只需要写标签调用，那么对于开发者来说，工作内容即是插件开发。不同于普通的Web系统开发，本系统没有WebForm的Control也没有MVC的Controller，有的只是Label、Field、Function、API等类的开发。
但最终也离不开数据获取，逻辑处理和最终数据返回输出。所以，插件亦可以分层、分块，只是换了一个样子。


####模板标签

    {Page.List PageSize="5" page="Url(page)" PaginationId="pages"}
        <tr>
            <td>@Name</td>
            <td>@UrlPattern</td>
            <td>@Template.FilePath</td>
            <td>@Template.Layout</td>
            <td><a href="/admin/system/pages/edit?id=@Id" data-toggle="modal" data-target="#myModal" class="btn-xs" title="edit"><span class="glyphicon glyphicon-edit"></span></a>
                <a href="/admin/api?action=page.delete&id=@Id" class="btn-xs" title="delete"><span class="glyphicon glyphicon-remove"></span></a></td>
        </tr>
    {/Page.List}
