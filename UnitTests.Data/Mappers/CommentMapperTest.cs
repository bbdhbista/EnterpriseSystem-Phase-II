using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Mappers;
using EnterpriseSystems.Data.Model.Constants;
using EnterpriseSystems.Data.Model.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace UnitTests.Data.Mappers
{
    [TestClass]
    public class CommentMapperTest
    {
        private CommentMapper Target { get; set; }
        private Mock<IDatabase> MockDatabase { get; set; }
        private Mock<IQuery> MockQuery { get; set; }
        private Mock<IHydrater<CommentVO>> MockHydrater { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            MockDatabase = new Mock<IDatabase>();
            MockQuery = new Mock<IQuery>();
            MockHydrater = new Mock<IHydrater<CommentVO>>();

            Target = new CommentMapper(MockDatabase.Object, MockHydrater.Object);

            MockDatabase.Setup(m => m.CreateQuery(It.IsAny<string>())).Returns(MockQuery.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Target = null;
            MockDatabase = null;
            MockQuery = null;
            MockHydrater = null;
        }
        [TestMethod]
        public void GetCommentsByCustomerRequest_ReturnsNullWhenNoQueryMatches()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CommentVO>());

            var actual = Target.GetCommentsByCustomerRequest(new CustomerRequestVO());

            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetCommentsByCustomerRequest_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CommentVO>());

            var actual = Target.GetCommentsByCustomerRequest(new CustomerRequestVO { Identity = 1 });

            MockQuery.Verify(m => m.AddParameter(1, CustomerRequestQueryParameters.Identity), Times.Once);
        }

        [TestMethod]
        public void GetCommentsByCustomeRequest_ReturnsHydratedEntities()
        {
            var hydratedEntity = new CommentVO();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CommentVO> { hydratedEntity });

            var actual = Target.GetCommentsByCustomerRequest(new CustomerRequestVO());

            Assert.AreEqual(hydratedEntity, actual.ElementAt(0));
        }


        [TestMethod]
        public void GetCommentsByStop_ReturnsNullWhenNoQueryMatches()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CommentVO>());

            var actual = Target.GetCommentsByStop(new StopVO());

            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetCommentsByStop_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CommentVO>());

            var actual = Target.GetCommentsByStop(new StopVO { Identity = 1 });

            MockQuery.Verify(m => m.AddParameter(1, StopRequestQueryParameters.Identity), Times.Once);
        }

        [TestMethod]
        public void GetCommentsByStop_ReturnsHydratedEntities()
        {
            List<CommentVO> hydratedEntities = new List<CommentVO>();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(hydratedEntities);
            var actual = Target.GetCommentsByStop(new StopVO());
            Assert.AreEqual(hydratedEntities, actual);
        }
    }
}