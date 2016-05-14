using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace UnitTests.Data.Hydraters
{
    [TestClass]
    public class CustomerRequestHydraterTest
    {
        private CustomerRequestHydrater Target { get; set; }
        private DataTable TestDataTable { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTarget();
            InitializeTestDataTable();
        }

        private void InitializeTarget()
        {
            Target = new CustomerRequestHydrater();
        }

        private void InitializeTestDataTable()
        {
            TestDataTable = new DataTable();
            TestDataTable.Columns.Add(CustomerRequestColumnNames.Identity, typeof(int));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.Status, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.BusinessEntityName, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.TypeCode, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.ConsumerClassificationType, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.CreatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.CreatedUserId, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.CreatedProgramCode, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.LastUpdatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.LastUpdatedUserId, typeof(string));
            TestDataTable.Columns.Add(CustomerRequestColumnNames.LastUpdatedProgramCode, typeof(string));
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Target = null;
            TestDataTable = null;
        }

        [TestMethod]
        public void Hydrate_ReturnsEmptyWhenDataTableEmpty()
        {
            var actual = Target.Hydrate(TestDataTable);

            Assert.AreEqual(0, actual.Count());
        }

        [TestMethod]
        public void Hydrate_HydratesCustomerRequestsSuccessfully()
        {
            DataRow testDataRow = GetTestDataRow();
            TestDataTable.Rows.Add(testDataRow);

            testDataRow = GetTestDataRow(1);
            TestDataTable.Rows.Add(testDataRow);

            var actual = Target.Hydrate(TestDataTable);

            var firstElement = actual.ElementAt(0);
            Assert.AreEqual(1, firstElement.Identity);
            Assert.AreEqual("Status", firstElement.Status);
            Assert.AreEqual("BusinessEntityName", firstElement.BusinessEntityKey);
            Assert.AreEqual("TypeCode", firstElement.TypeCode);
            Assert.AreEqual("ConsumerClassificationType", firstElement.ConsumerClassificationType);
            Assert.AreEqual(new DateTime(1), firstElement.CreatedDate);
            Assert.AreEqual("CreatedUserId", firstElement.CreatedUserId);
            Assert.AreEqual("CreatedProgramCode", firstElement.CreatedProgramCode);
            Assert.AreEqual(new DateTime(2), firstElement.LastUpdatedDate);
            Assert.AreEqual("LastUpdatedUserId", firstElement.LastUpdatedUserId);
            Assert.AreEqual("LastUpdatedProgramCode", firstElement.LastUpdatedProgramCode);

            var secondElement = actual.ElementAt(1);
            Assert.AreEqual(2, secondElement.Identity);
            Assert.AreEqual("Status1", secondElement.Status);
            Assert.AreEqual("BusinessEntityName1", secondElement.BusinessEntityKey);
            Assert.AreEqual("TypeCode1", secondElement.TypeCode);
            Assert.AreEqual("ConsumerClassificationType1", secondElement.ConsumerClassificationType);
            Assert.AreEqual(new DateTime(2), secondElement.CreatedDate);
            Assert.AreEqual("CreatedUserId1", secondElement.CreatedUserId);
            Assert.AreEqual("CreatedProgramCode1", secondElement.CreatedProgramCode);
            Assert.AreEqual(new DateTime(3), secondElement.LastUpdatedDate);
            Assert.AreEqual("LastUpdatedUserId1", secondElement.LastUpdatedUserId);
            Assert.AreEqual("LastUpdatedProgramCode1", secondElement.LastUpdatedProgramCode);
        }

        private DataRow GetTestDataRow(int? increment = null)
        {
            DataRow testDataRow = TestDataTable.NewRow();

            testDataRow[CustomerRequestColumnNames.Identity] = 1 + (increment ?? 0);
            testDataRow[CustomerRequestColumnNames.Status] = "Status" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.BusinessEntityName] = "BusinessEntityName" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.TypeCode] = "TypeCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.ConsumerClassificationType] = "ConsumerClassificationType" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.CreatedDate] = new DateTime(1 + (increment ?? 0));
            testDataRow[CustomerRequestColumnNames.CreatedUserId] = "CreatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.CreatedProgramCode] = "CreatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.LastUpdatedDate] = new DateTime(2 + (increment ?? 0));
            testDataRow[CustomerRequestColumnNames.LastUpdatedUserId] = "LastUpdatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[CustomerRequestColumnNames.LastUpdatedProgramCode] = "LastUpdatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);

            return testDataRow;
        }
    }
}
