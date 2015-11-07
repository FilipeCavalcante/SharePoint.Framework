using CMPSFC.Framework.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePointTests.Entity
{
    public class EntityRepository : SPRepository<EntityTest>
    {
        public EntityRepository() : base() { }
        public EntityRepository(string weburl, Guid listguid) : base(weburl, listguid) { }
    }
}
