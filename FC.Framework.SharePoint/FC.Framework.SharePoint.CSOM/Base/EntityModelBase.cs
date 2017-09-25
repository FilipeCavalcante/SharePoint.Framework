using System;

namespace FC.Framework.SharePoint.CSOM.Base
{
    public class EntityModelBase : IEntityModelBase
    {
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
