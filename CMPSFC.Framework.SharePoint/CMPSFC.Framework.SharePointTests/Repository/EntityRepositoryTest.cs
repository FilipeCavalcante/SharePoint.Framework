using CMPSFC.Framework.SharePointTests.Entity;
using Microsoft.SharePoint;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPSFC.Framework.SharePointTests.Repository
{
    [TestClass()]
    public class EntityRepositoryTest
    {
        EntityRepository _subject;
        private string weburl = "http://win-sp2013/sites/unittest/";
        private Guid listguid = new Guid("520bb23b-fad7-46e9-b8ae-d7aba1940f69");
        private string listname = "Unit Test";
        private SPWeb spweb = null;


        [TestInitialize]
        public void Setup()
        {
            spweb = new SPSite(weburl).OpenWeb();
            _subject = new EntityRepository(weburl, listguid);
        }

        /// <summary>
        /// It should be possible to retrieve all data from SPList with all item properties, even those that IS NOT mapped on TEntity class
        /// </summary>
        [TestMethod]
        public void I_Can_Get_All_Items_And_All_Metadata()
        {
            var result = _subject.GetAll(true);
            CollectionAssert.AllItemsAreNotNull(result.ToList());
            Assert.IsNotNull(result.First().Title);
        }

        /// <summary>
        /// It should be possible to retrieve all data but only with item properties that IS mapped on TEntity class
        /// </summary>
        [TestMethod]
        public void I_Can_Get_All_Items_With_Mapped_Metadata()
        {
            var result = _subject.GetAll(false);
            CollectionAssert.AllItemsAreNotNull(result.ToList());
            Assert.IsNotNull(result.First().Title);
            Assert.IsNotNull(result.First().Author);
            Assert.IsNotNull(result.First().Created);
        }
    }
}
