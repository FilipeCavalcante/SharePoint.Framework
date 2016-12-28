using PI.Framework.SharePoint;
using System;

namespace PI.Framework.SharePointTests.Entity
{
    public class EntityRepository : SPRepository<EntityTest>
    {
        public EntityRepository() : base() { }
        public EntityRepository(string weburl, Guid listguid) : base(weburl, listguid) { }
    }
}
