using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace UnitTests.Data.Hydraters
{
    [TestClass]
    public class StopHydraterTest
    {
        private StopHydrater Target { get; set; }
        private DataTable TestDataTable { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTarget();
            InitializeTestDataTable();
        }

        private void InitializeTarget()
        {
            Target = new StopHydrater();
        }

        private void InitializeTestDataTable()
        {
            TestDataTable = new DataTable();
            TestDataTable.Columns.Add(StopColumnNames.Identity, typeof(int));
            TestDataTable.Columns.Add(StopColumnNames.EntityName, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.EntityIdentity, typeof(int));
            TestDataTable.Columns.Add(StopColumnNames.RoleType, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.StopNumber, typeof(short));
            TestDataTable.Columns.Add(StopColumnNames.CustomerAlias, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.OrganizationName, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.AddressLine1, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.AddressLine2, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.AddressCityName, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.AddressStateCode, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.AddressCountryCode, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.AddressPostalCode, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.CreatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(StopColumnNames.CreatedUserId, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.CreatedProgramCode, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.LastUpdatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(StopColumnNames.LastUpdatedUserId, typeof(string));
            TestDataTable.Columns.Add(StopColumnNames.LastUpdatedProgramCode, typeof(string));
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
        public void Hydrate_HydratesStopsSuccessfully()
        {
            DataRow testDataRow = GetTestDataRow();
            TestDataTable.Rows.Add(testDataRow);

            testDataRow = GetTestDataRow(1);
            TestDataTable.Rows.Add(testDataRow);

            var actual = Target.Hydrate(TestDataTable);

            var firstElement = actual.ElementAt(0); //first row
            Assert.AreEqual(1, firstElement.Identity);
            Assert.AreEqual("EntityName", firstElement.EntityName);
            Assert.AreEqual(2, firstElement.EntityIdentity);
            Assert.AreEqual("RoleType", firstElement.RoleType);
            Assert.AreEqual(((short)3), firstElement.StopNumber);
            Assert.AreEqual("CustomerAlias", firstElement.CustomerAlias);
            Assert.AreEqual("OrganizationName", firstElement.OrganizationName);
            Assert.AreEqual("AddressLine1", firstElement.AddressLine1);
            Assert.AreEqual("AddressLine2", firstElement.AddressLine2);
            Assert.AreEqual("AddressCityName", firstElement.AddressCityName);
            Assert.AreEqual("AddressStateCode", firstElement.AddressStateCode);
            Assert.AreEqual("AddressCountryCode", firstElement.AddressCountryCode);
            Assert.AreEqual("AddressPostalCode", firstElement.AddressPostalCode);
            Assert.AreEqual(new DateTime(1), firstElement.CreatedDate);
            Assert.AreEqual("CreatedUserId", firstElement.CreatedUserId);
            Assert.AreEqual("CreatedProgramCode", firstElement.CreatedProgramCode);
            Assert.AreEqual(new DateTime(2), firstElement.LastUpdatedDate);
            Assert.AreEqual("LastUpdatedUserId", firstElement.LastUpdatedUserId);
            Assert.AreEqual("LastUpdatedProgramCode", firstElement.LastUpdatedProgramCode);

            var secondElement = actual.ElementAt(1); //second row
            Assert.AreEqual(2, secondElement.Identity);
            Assert.AreEqual("EntityName1", secondElement.EntityName);
            Assert.AreEqual(3, secondElement.EntityIdentity);
            Assert.AreEqual("RoleType1", secondElement.RoleType);
            Assert.AreEqual(((short)4), secondElement.StopNumber);
            Assert.AreEqual("CustomerAlias1", secondElement.CustomerAlias);
            Assert.AreEqual("OrganizationName1", secondElement.OrganizationName);
            Assert.AreEqual("AddressLine11", secondElement.AddressLine1);
            Assert.AreEqual("AddressLine21", secondElement.AddressLine2);
            Assert.AreEqual("AddressCityName1", secondElement.AddressCityName);
            Assert.AreEqual("AddressStateCode1", secondElement.AddressStateCode);
            Assert.AreEqual("AddressCountryCode1", secondElement.AddressCountryCode);
            Assert.AreEqual("AddressPostalCode1", secondElement.AddressPostalCode);
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
            testDataRow[StopColumnNames.Identity] = 1 + (increment ?? 0);
            testDataRow[StopColumnNames.EntityName] = "EntityName" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.EntityIdentity] = 2 + (increment ?? 0);
            testDataRow[StopColumnNames.RoleType] = "RoleType" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.StopNumber] = (3 + (short)(increment ?? 0));
            testDataRow[StopColumnNames.CustomerAlias] = "CustomerAlias" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.OrganizationName] = "OrganizationName" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.AddressLine1] = "AddressLine1" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.AddressLine2] = "AddressLine2" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.AddressCityName] = "AddressCityName" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.AddressStateCode] = "AddressStateCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.AddressCountryCode] = "AddressCountryCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.AddressPostalCode] = "AddressPostalCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.CreatedDate] = new DateTime(1 + (increment ?? 0));
            testDataRow[StopColumnNames.CreatedUserId] = "CreatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.CreatedProgramCode] = "CreatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.LastUpdatedDate] = new DateTime(2 + (increment ?? 0));
            testDataRow[StopColumnNames.LastUpdatedUserId] = "LastUpdatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[StopColumnNames.LastUpdatedProgramCode] = "LastUpdatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);

            return testDataRow;
        }
    }
}
