using System;
using Microsoft.SharePoint.Client;

namespace FC.Framework.SharePoint.CSOM.Mapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ListMapperAttribute : Attribute
    {
        public string ListTitle { get; set; }
        public ListTemplateType TemplateType { get; set; }
    }
}
