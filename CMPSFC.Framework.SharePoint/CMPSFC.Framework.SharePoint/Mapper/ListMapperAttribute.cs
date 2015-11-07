using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePoint.Mapper
{
    public class ListMapperAttribute: Attribute
    {
        public string ListGuid { get; set; }
        public string ListTitle { get; set; }
        public string RelativeUrl { get; set; }
        public SPListTemplateType ListTemplate { get; set; }
    }
}
