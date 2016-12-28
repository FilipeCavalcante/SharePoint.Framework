using Microsoft.SharePoint;
using System;

namespace PI.Framework.SharePoint.Mapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ListMapperAttribute : Attribute
    {
        public string ListGuid { get; set; }
        public string ListTitle { get; set; }
        public string RelativeUrl { get; set; }
        public SPListTemplateType ListTemplate { get; set; }
    }
}
