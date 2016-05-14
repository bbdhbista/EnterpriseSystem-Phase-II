using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseSystems.Data.Mappers;
using EnterpriseSystems.Data.DAO;
using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Constants;
using EnterpriseSystems.Data.Model.Entities;
using Moq;

namespace UnitTests.Data.Mappers
{
    [TestClass]
    public class CustomerRequestMapperTest
    {
        private CustomerRequestMapper Target { get; set; }
        private Mock<IDatabase> MockDatabase { get; set; }
        private Mock<IQuery> MockQuery { get; set; }
        private Mock<IHydrater<CustomerRequestVO>> MockHydrater { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            MockDatabase = new Mock<IDatabase>();
            MockQuery = new Mock<IQuery>();
            MockHydrater = new Mock<IHydrater<CustomerRequestVO>>();

            Target = new CustomerRequestMapper(MockDatabase.Object, MockHydrater.Object);

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
        public void GetCustomerRequestByIdentity_ReturnsNullWhenNoQueryMatches()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO>());

            var actual = Target.GetCustomerRequestByIdentity(new CustomerRequestVO());

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetCustomerRequestByIdentity_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO>());

            var actual = Target.GetCustomerRequestByIdentity(new CustomerRequestVO { Identity = 1 });

            MockQuery.Verify(m => m.AddParameter(1, CustomerRequestQueryParameters.Identity), Times.Once);
        }

        [TestMethod]
        public void GetCustomerRequestByIdentity_ReturnsHydratedEntity()
        {
            var hydratedEntity = new CustomerRequestVO();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO> { hydratedEntity });

            var actual = Target.GetCustomerRequestByIdentity(new CustomerRequestVO());

            Assert.AreEqual(hydratedEntity, actual);
        }

        [TestMethod]
        public void GetCustomerRequestsByReferenceNumber_SetsQueryParameter()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO>());

            var actual = Target.GetCustomerRequestsByReferenceNumber(new CustomerRequestVO { ReferenceNumbers = new List<ReferenceNumberVO> { new ReferenceNumberVO { ReferenceNumber = "1" } } });

            MockQuery.Verify(m => m.AddParameter("1", CustomerRequestQueryParameters.ReferenceNumber), Times.Once);
        }

        [TestMethod]
        public void GetCustomerRequestsByReferenceNumber_ReturnsHydratedEntities()
        {
            var hydratedEntities = new CustomerRequestVO();
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO> { hydratedEntities });

            var actual = Target.GetCustomerRequestsByReferenceNumber(new CustomerRequestVO { ReferenceNumbers = new List<ReferenceNumberVO> { new ReferenceNumberVO { ReferenceNumber = "1" } } });

            Assert.AreEqual(hydratedEntities, actual.ElementAt(0));
        }


        [TestMethod]
        public void GetCustomerRequestsByReferenceNumberAndBusinessName_SetsQueryParameters()
        {
            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO>());

            var actual = Target.GetCustomerRequestsByReferenceNumberAndBusinessName(new CustomerRequestVO
            {
                ReferenceNumbers = new List<ReferenceNumberVO> { new ReferenceNumberVO { ReferenceNumber = "1" } },
                BusinessEntityKey = "1"
            });

            MockQuery.Verify(m => m.AddParameter("1", CustomerRequestQueryParameters.ReferenceNumber), Times.Once);
            MockQuery.Verify(m => m.AddParameter("1", CustomerRequestQueryParameters.BusinessName), Times.Once);
        }

        [TestMethod]
        public void GetCustomerRequestsByReferenceNumberAndBusinessName_ReturnsHydratedEntities()
        {
            var hydratedEntity = new CustomerRequestVO();

            MockHydrater.Setup(m => m.Hydrate(It.IsAny<DataTable>())).Returns(new List<CustomerRequestVO> { hydratedEntity });

            var actual = Target.GetCustomerRequestsByReferenceNumberAndBusinessName(new CustomerRequestVO
            {
                ReferenceNumbers = new List<ReferenceNumberVO> { new ReferenceNumberVO { ReferenceNumber = "1" } },
                BusinessEntityKey = "1"
            });

            Assert.AreEqual(hydratedEntity, actual.ElementAt(0));
        }
    }
}
