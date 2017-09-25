using FC.Framework.SharePoint.CSOM.Base;
using FC.Framework.SharePoint.CSOM.Mapper;
using Microsoft.SharePoint.Client;

namespace FC.Framework.SharePoint.CSOM.Tests.Business.Entities
{
    [ListMapper(ListTitle = "Blacklist", TemplateType = ListTemplateType.GenericList)]
    public class BlacklistEntityTest : EntityModelBase
    {
        public string ADUnit { get; set; }

        [FieldMapper(InternalName = "Advertiser2")]
        public string Advertise { get; set; }

        public string AnotherField { get; set; }
    }
}
