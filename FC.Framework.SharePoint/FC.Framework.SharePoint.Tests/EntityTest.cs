using Microsoft.VisualStudio.TestTools.UnitTesting;
using FC.Framework.SharePoint.Tests.Entity;
using System.Linq;

namespace FC.Framework.SharePoint.Tests
{
    [TestClass]
    public class EntityTest
    {
        private const string SPURL = "http://srvmtdev0011:26553";
        private TaskEntityRepository subject;

        [TestInitialize]
        public void Setup()
        {
            subject = new TaskEntityRepository(SPURL);
        }

        [TestMethod]
        public void ItShould_BePossible_ToGet_SharePoint_ListaData()
        {
            // Assert
            Assert.IsNotNull(subject);
            Assert.IsNotNull(subject.ParentWeb);
            Assert.IsNotNull(subject.ParentList);
        }

        [TestMethod]
        public void ITShoud_BePossible_ToGet_Data_Without_InternalName_Defined()
        {
            /// act
            var result = subject.GetAll();

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.First()?.AssignedTo);
        }

        [TestMethod]
        public  void ITShould_BePossible_ToAdd_NewValue()
        {
            var task = new TaskEntity()
            {
                Title = "Task from UnitTest",
                AssignedTo = new Microsoft.SharePoint.SPFieldUserValue(subject.ParentWeb, "1;#")
            };

            //act

            var result = subject.Add(task);

            // assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
