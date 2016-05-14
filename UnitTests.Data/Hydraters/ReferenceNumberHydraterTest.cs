using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace UnitTests.Data.Hydraters
{
    [TestClass]
    public class ReferenceNumberHydraterTest
    {
        private ReferenceNumberHydrater Target { get; set; }
        private DataTable TestDataTable { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTarget();
            InitializeTestDataTable();
        }

        private void InitializeTarget()
        {
            Target = new ReferenceNumberHydrater();
        }

        private void InitializeTestDataTable()
        {
            TestDataTable = new DataTable();
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.Identity, typeof(int));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.EntityName, typeof(string));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.EntityIdentity, typeof(int));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.ReferenceNumber, typeof(string));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.CreatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.CreatedUserId, typeof(string));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.CreatedProgramCode, typeof(string));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.LastUpdatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.LastUpdatedUserId, typeof(string));
            TestDataTable.Columns.Add(ReferenceNumberColumnNames.LastUpdatedProgramCode, typeof(string));
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
        public void Hydrate_HydratesReferenceNumbersSuccessfully()
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
            Assert.AreEqual("ReferenceNumber", firstElement.ReferenceNumber);
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
            Assert.AreEqual("ReferenceNumber1", secondElement.ReferenceNumber);
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
            testDataRow[ReferenceNumberColumnNames.Identity] = 1 + (increment ?? 0);
            testDataRow[ReferenceNumberColumnNames.EntityName] = "EntityName" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[ReferenceNumberColumnNames.EntityIdentity] = 2 + (increment ?? 0);
            testDataRow[ReferenceNumberColumnNames.ReferenceNumber] = "ReferenceNumber" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[ReferenceNumberColumnNames.CreatedDate] = new DateTime(1 + (increment ?? 0));
            testDataRow[ReferenceNumberColumnNames.CreatedUserId] = "CreatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[ReferenceNumberColumnNames.CreatedProgramCode] = "CreatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[ReferenceNumberColumnNames.LastUpdatedDate] = new DateTime(2 + (increment ?? 0));
            testDataRow[ReferenceNumberColumnNames.LastUpdatedUserId] = "LastUpdatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[ReferenceNumberColumnNames.LastUpdatedProgramCode] = "LastUpdatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);

            return testDataRow;
        }
    }
}

