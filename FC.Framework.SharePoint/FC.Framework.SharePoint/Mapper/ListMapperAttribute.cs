using Microsoft.SharePoint;
using System;

namespace FC.Framework.SharePoint.Mapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ListMapperAttribute : Attribute
    {
        public string ListTitle { get; set; }
        public string RelativeUrl { get; set; }
        public SPListTemplateType ListTemplate { get; set; }
    }
}
