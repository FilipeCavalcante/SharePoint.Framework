using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.SharePoint;
using PI.Framework.SharePointTests.Entity;

namespace PI.Framework.SharePoint.Tests
{
    [TestClass()]
    public class EntityBaseRepositoryTest
    {
        SPRepository<EntityTest> _subject;
        private string weburl = "http://win-sp2013/sites/unittest/";
        private Guid listguid = new Guid("520bb23b-fad7-46e9-b8ae-d7aba1940f69");
        private string listname = "Unit Test";
        private SPWeb spweb = null;


        [TestInitialize]
        public void Setup()
        {
            spweb = new SPSite(weburl).OpenWeb();
        }

        [TestMethod]
        public void I_Can_Open_Repository_Without_Parameter()
        {
            _subject = new SPRepository<EntityTest>();
            Assert.IsNotNull(_subject);
        }

        [TestMethod()]
        public void I_Can_Open_Entity_Repository_With_Guid_Identifier()
        {
            _subject = new SPRepository<EntityTest>(weburl, listguid);
            Assert.IsNotNull(_subject);
            Assert.IsNotNull(_subject.ParentWeb);
            Assert.IsNotNull(_subject.ParentList);
        }

        [TestMethod]
        public void It_Should_Throw_An_Exception_With_List_Not_Found()
        {
            try
            {
                _subject = new SPRepository<EntityTest>(weburl, Guid.NewGuid());
                Assert.IsNotNull(_subject);
                Assert.IsNotNull(_subject.ParentWeb);
                Assert.IsNotNull(_subject.ParentList);
            }
            catch (SPException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The list cannot be found"));
            }

        }

        [TestMethod]
        public void I_Can_Open_Entity_Repository_With_Title_Name()
        {
            _subject = new SPRepository<EntityTest>(weburl, listname);
            Assert.IsNotNull(_subject);
            Assert.IsNotNull(_subject.ParentWeb);
            Assert.IsNotNull(_subject.ParentList);

        }
    }
}
