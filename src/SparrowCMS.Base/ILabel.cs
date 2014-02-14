using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Core
{
    public interface ILabel
    {
        string GetReplacedContent(string innerHtml);
    }

    //public abstract class Label
    //{
    //    public IEnumerable<Field> Fields { get; set; }

    //    public abstract string GetReplacedContent(string innerHtml);
    //}
}
