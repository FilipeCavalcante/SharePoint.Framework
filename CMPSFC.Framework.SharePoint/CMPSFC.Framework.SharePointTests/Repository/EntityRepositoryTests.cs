using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMPSFC.Framework.SharePoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.SharePoint;
using CMPSFC.Framework.SharePointTests.Entity;

namespace CMPSFC.Framework.SharePoint.Tests
{
    [TestClass()]
    public class EntityRepositoryTests
    {
        private string weburl = "http://win-sp2013/sites/unittest/";
        private Guid listguid = new Guid("520bb23b-fad7-46e9-b8ae-d7aba1940f69");
        private SPWeb spweb = null;


        [TestInitialize]
        public void Setup()
        {
            spweb = new SPSite(weburl).OpenWeb();
        }

        [TestMethod()]
        public void I_Can_Open_Entity_Repository()
        {
            var repository = new SPRepository<EntityTest>(weburl, listguid);
            Assert.IsNotNull(repository);
            Assert.IsNotNull(repository.ParentWeb);
            Assert.IsNotNull(repository.ParentList);
        }
    }
}
