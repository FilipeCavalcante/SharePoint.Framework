using Microsoft.SharePoint;
using FC.Framework.SharePoint.Base;
using FC.Framework.SharePoint.Mapper;

namespace FC.Framework.SharePoint.Tests.Entity
{
    [ListMapper(ListTemplate = SPListTemplateType.Tasks, ListTitle = "Tasks", RelativeUrl = "/")]
    public class TaskEntity : SPBaseEntity
    {
        [FieldMapper(InternalName = "Status")]
        public string StatusTarefa { get; set; }
        public SPFieldUserValue AssignedTo { get; set; }
    }
}
