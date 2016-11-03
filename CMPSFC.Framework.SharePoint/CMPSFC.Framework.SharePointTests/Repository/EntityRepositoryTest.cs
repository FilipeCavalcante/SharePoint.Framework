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
        private SPWeb spweb = null;


        [TestInitialize]
        public void Setup()
        {
            spweb = new SPSite(weburl).OpenWeb();
            _subject = new EntityRepository(weburl, listguid);
            AddTestItem();
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

        private void AddTestItem()
        {
            _subject.Add(new EntityTest()
            {
                Title = string.Format("New item created on {0}", DateTime.Now)
            });
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

        /// <summary>
        /// It should be possible to get an specific item using LAMBDA Expression
        /// </summary>
        [TestMethod]
        public void I_Can_Get_Items_Filtered_By_Predicate_Expression()
        {
            var result = _subject.FindBy(f => f.Title != string.Empty);
            Assert.IsNotNull(result);
            CollectionAssert.AllItemsAreNotNull(result.ToList());
        }


        /// <summary>
        /// It should be possible to add new item
        /// </summary>
        [TestMethod]
        public void I_Can_Add_New_Item_To_List()
        {
            var newitem = new EntityTest()
            {
                Title = "New item created on " + DateTime.Now,
            };

            var result = _subject.Add(newitem);
            Assert.IsNotNull(result);
        }


        [TestCleanup]
        public void TearDown()
        {
            var collection = _subject.GetAll(false);
            _subject.Delete(collection.ToList());
        }
    }
}
