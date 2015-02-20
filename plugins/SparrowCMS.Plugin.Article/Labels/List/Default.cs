using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SparrowCMS.Labels.Shared;

namespace SparrowCMS.Plugin.Article.Labels.List
{
    [LabelName("Article.List")]
    public class Default : ListLabelBase, ILabel
    {
        public int CategoryId { get; set; }

        public int Top { get; set; }

        public override IEnumerable<Document> GetDatas()
        {
            throw new NotImplementedException();
        }
    }
}
