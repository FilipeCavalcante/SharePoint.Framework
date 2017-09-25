using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FC.Framework.SharePoint.CSOM.Base;
using FC.Framework.SharePoint.CSOM.Tests.Business.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FC.Framework.SharePoint.CSOM.Tests.Business.Domain
{
    [TestClass]
    public class BlacklistListTest
    {
        private static readonly string SpoUrl = $"https://stmlcloudservices.sharepoint.com/sites/uol-gestao-parceiros-dev/";
        private IUnitOfWorkSharePointCSOM<BlacklistEntityTest> _subject;

        [TestInitialize]
        public void Setup()
        {
            _subject = new UnitOfWorkSharePointCSOM<BlacklistEntityTest>(SpoUrl, "filipe.cavalcante@stml.com.br", "B3tinhaLinda01");
        }

        /// <summary>
        /// This UnitTest proves that you don't need to define an attribute with field internal name to retrieve data from SharePoint Online specified field;
        /// The "ADUnit" is defined with the same name as 'Internal Name' field on SPO list. Like this, the UnitOfWork can identify the correspondent field, and returns its value to the property
        /// </summary>
        [TestMethod]
        public void It_Should_Be_Possible_To_Get_Data_From_BlackList()
        {
            var collection = _subject.GetAll();
            Debug.Assert(collection != null, "collection != null");
            var blacklistEntityTests = collection as IList<BlacklistEntityTest> ?? collection.ToList();
            blacklistEntityTests.Should().NotBeNull();
            Assert.IsTrue(blacklistEntityTests.Count > 0);
            blacklistEntityTests.First().ADUnit.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// This UnitTest proves that, if you create an property that differs from SPO List fild name or internal name, it should be mapped with an custom attribute.
        /// In this example, the 'BlackListEntity' model has a property called 'Advertise' with 'FieldMapper' attribute that refers to a specific field from SPOList
        /// </summary>
        [TestMethod]
        public void It_Should_Be_Possible_To_Get_Data_From_BlackList_With_Field_And_InternalName_Defined()
        {
            var collection = _subject.GetAll();
            Debug.Assert(collection != null, "collection != null");
            var blacklistEntityTests = collection as IList<BlacklistEntityTest> ?? collection.ToList();
            blacklistEntityTests.Should().NotBeNull();
            Assert.IsTrue(blacklistEntityTests.Count > 0);
            blacklistEntityTests.First().Advertise.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void The_Property_Not_Mapped_And_Not_Identified_On_SPOList_Should_Be_Null()
        {
            var collection = _subject.GetAll();
            Debug.Assert(collection != null);

            var blacklistEntityList = collection as IList<BlacklistEntityTest> ?? collection.ToList();
            blacklistEntityList.Should().NotBeNull();
            Assert.IsTrue(blacklistEntityList.Count > 0);
            blacklistEntityList.First().AnotherField.Should().BeNullOrEmpty();
        }
    }
}
