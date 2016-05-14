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
    public class AppointmentMapperTest
    {
        private AppointmentMapper Target { get; set; }
        private Mock<IDatabase> MockDatabase { get; set; }
        private Mock<IQuery> MockQuery { get; set; }
        private Mock<IHydrater<AppointmentVO>> MockHydrater { get; set; }


        [TestInitialize]
        public void TestInitialize()
        {
            MockDatabase = new Mock<IDatabase>();
            MockQuery = new Mock<IQuery>();
            MockHydrater = new Mock<IHydrater<AppointmentVO>>();

            Target = new AppointmentMapper(MockDatabase.Object, MockHydrater.Object);

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
        public void GetAppointmentsByStop_ReturnsNullWhenNoQueryMatches()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<AppointmentVO>());

            var actual = Target.GetAppointmentsByStop(new StopVO());

            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetAppointmentsByStop_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<AppointmentVO>());

            var actual = Target.GetAppointmentsByStop(new StopVO { Identity = 1 });

            MockQuery.Verify(m => m.AddParameter(1, StopRequestQueryParameters.Identity), Times.Once);
        }

        [TestMethod]
        public void GetAppointmentsByStop_ReturnsHydratedEntities()
        {
            List<AppointmentVO> hydratedEntities = new List<AppointmentVO>();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(hydratedEntities);
            var actual = Target.GetAppointmentsByStop(new StopVO());
            Assert.AreEqual(hydratedEntities, actual);
        }

        [TestMethod]
        public void GetAppointmentsByCustomerRequest_ReturnsNullWhenNoQueryMatches()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<AppointmentVO>());

            var actual = Target.GetAppointmentsByCustomerRequest(new CustomerRequestVO());

            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetAppointmentsByCustomerRequest_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<AppointmentVO>());

            var actual = Target.GetAppointmentsByCustomerRequest(new CustomerRequestVO { Identity = 1 });

            MockQuery.Verify(m => m.AddParameter(1, CustomerRequestQueryParameters.Identity), Times.Once);
        }

        [TestMethod]
        public void GetAppointmentsByCustomerRequest_ReturnsHydratedEntities()
        {
            List<AppointmentVO> hydratedEntities = new List<AppointmentVO>();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(hydratedEntities);
            var actual = Target.GetAppointmentsByCustomerRequest(new CustomerRequestVO());
            Assert.AreEqual(hydratedEntities, actual);
        }


    }
}

