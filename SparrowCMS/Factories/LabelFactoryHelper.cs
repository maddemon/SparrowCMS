using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparrowCMS.Factories
{
    public class LabelFactoryHelper
    {
        public static string[] GetFullNames(string pluginName, string labelName, string className, string MemberType)
        {
            return new string[]
            { 
                string.Format("{0}.Labels.{1}.{3}s.{2}",pluginName,labelName,className,MemberType ),
                string.Format("{0}.Shared.Labels.{1}.{3}s.{2}",pluginName,labelName,className,MemberType ),
                string.Format("SparrowCMS.Labels.{0}.{2}s.{1}",labelName,className,MemberType),
                string.Format("SparrowCMS.Shared.Labels.{0}.{2}s.{1}",labelName,className,MemberType),
            };
        }
    }
}
