using Microsoft.SharePoint;
using PI.Framework.SharePoint.Base;
using PI.Framework.SharePoint.Mapper;

namespace PI.Framework.SharePoint.Tests.Entity
{
    [ListMapper(ListTemplate = SPListTemplateType.Tasks, ListTitle = "Tasks", RelativeUrl = "/")]
    public class TaskEntity : SPBaseEntity
    {
        [FieldMapper(InternalName = "Status")]
        public string StatusTarefa { get; set; }
        public SPFieldUserValue AssignedTo { get; set; }
    }
}
