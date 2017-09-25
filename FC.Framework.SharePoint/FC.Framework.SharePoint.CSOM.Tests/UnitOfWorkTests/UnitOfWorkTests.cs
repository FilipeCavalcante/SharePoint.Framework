using System.Net;
using FC.Framework.SharePoint.CSOM.Base;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FC.Framework.SharePoint.CSOM.Tests.UnitOfWorkTests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        private static readonly string SpoUrl = $"https://stmlcloudservices.sharepoint.com/sites/uol-gestao-parceiros-dev/";
        private IUnitOfWorkSharePointCSOM<object> _subject;

        [TestInitialize]
        public void Setup() { }


        [TestMethod]
        public void It_Should_Be_Possible_To_Open_SPOnline_Context_With_UserName_And_Password()
        {
            _subject = new UnitOfWorkSharePointCSOM<object>(SpoUrl, "filipe.cavalcante@stml.com.br", "B3tinhaLinda01");
            _subject.ContextWeb.Should().NotBeNull();
            _subject.ContextWeb.Title.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void It_Should_Not_Possible_To_Open_SPOnline_Context_Without_UserName_And_Password()
        {
            _subject = new UnitOfWorkSharePointCSOM<object>(SpoUrl);
            Assert.IsNotNull(_subject.ContextWeb.Title);
        }

    }
}
