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
    public class StopMapperTest
    {
        private StopMapper Target { get; set; }
        private Mock<IDatabase> MockDatabase { get; set; }
        private Mock<IQuery> MockQuery { get; set; }
        private Mock<IHydrater<StopVO>> MockHydrater { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            MockDatabase = new Mock<IDatabase>();
            MockQuery = new Mock<IQuery>();
            MockHydrater = new Mock<IHydrater<StopVO>>();

            Target = new StopMapper(MockDatabase.Object, MockHydrater.Object);

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
        public void GetStopsByCustomerRequest_ReturnsNullWhenNoQueryMatches()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<StopVO>());

            var actual = Target.GetStopsByCustomerRequest(new CustomerRequestVO());

            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetStopsByCustomerRequest_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<StopVO>());

            var actual = Target.GetStopsByCustomerRequest(new CustomerRequestVO { Identity = 1 });

            MockQuery.Verify(m => m.AddParameter(1, CustomerRequestQueryParameters.Identity), Times.Once);
        }

        [TestMethod]
        public void GetStopsByCustomerRequest_ReturnsHydratedEntities()
        {
            var hydratedEntities = new List<StopVO>();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(hydratedEntities);

            var actual = Target.GetStopsByCustomerRequest(new CustomerRequestVO());

            Assert.AreEqual(hydratedEntities, actual);
        }
    }
}