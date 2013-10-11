using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SparrowCMS.Base.Parsers
{
    public class LabelParser
    {
        /*
         * regex：
         * {(?<name>[\w.]+)(?<parameters>(\s\w+\s?=\s?("[^"]+"|[^\s\/]+))*)}([\s\S]*?){/(?<name>[\w.]+)}
         * |{(?<name>[\w.]+)(?<parameters>(\s\w+\s?=\s?("[^"]+"|[^\s\/]+))*)/}
         * 【注意】
         * 1、标签支持嵌套，所以需要递归建设标签内的标签
         * 2、并不是所有标签的Name的格式都是XX.YY，也可以只有XX
         * 标签举例1：
         * {Article.List top=10}
         * <a href="@CategoryId">@Category.Name</a>
         * <a href="@id">@(title maxlength=20) @(posttime dateformat="yyyy-MM-dd")</a>
         * {/Article.List}
         * ----------------------------------------------------------------------------
         * 举例2：
         * 这种标签，一般是特殊内嵌标签，不然RecordCount拿不到值，
         * 但也可以不用内嵌，因为毕竟数据量太多，获取RecordCount会很耗时，所以指定最大页数或最大记录数即可。
         * {PageLink pageSize=50 recordCount=@RecordCount}
         * <a href="page/1">首页</a> | <a href="page/@prev">上一页</a>
         * {PageLink.List size=10}
         * <a href="page/@page">@page</a>
         * {/PageLink.List}
         * <a href="page/@next">下一页</a> | <a href="page/@end">尾页</a>
         * {/PageLink}
         * 
         * 创建标签时根据LabelName反射具体实例，如果没有该Name的Type则使用通用标签。
         * ----------------------------------------------------------------------------
         * 
         * 从例子可以看出，Article表并没有Category.Name字段，所以需要通过“关系描述”获取该字段的值
         * 在创建Field的时候要注意这种特殊的“关系字段”。
         * 关系字段一般缓存为主，否则嵌套查询很耗时。后期考虑联合查询，但并不是所有数据库都支持联合查询。
         * 
         * 如果没有创建关系，则需要使用嵌套标签，例如:
         * {Category.Field name=Name id=@article.id/}
         */

        private static Regex _regex = new Regex(@"{(?<name>[\w.]+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s\/]+))*)}([\s\S]*?){/(?<name>[\w.]+)}|{(?<name>[\w.]+)(?<parameters>(\s\w+\s?=\s?(""[^""]+""|[^\s\/]+))*)/}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static LabelBase Parse(string templateContent)
        {
            throw new NotImplementedException();
        }
    }
}
