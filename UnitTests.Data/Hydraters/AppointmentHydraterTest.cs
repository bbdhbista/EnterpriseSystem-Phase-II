using EnterpriseSystems.Data.Hydraters;
using EnterpriseSystems.Data.Model.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Linq;

namespace UnitTests.Data.Hydraters
{
    [TestClass]
    public class AppointmentHydraterTest
    {
        private AppointmentHydrater Target { get; set; }
        private DataTable TestDataTable { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            InitializeTarget();
            InitializeTestDataTable();
        }

        private void InitializeTarget()
        {
            Target = new AppointmentHydrater();
        }

        private void InitializeTestDataTable()
        {
            TestDataTable = new DataTable();
            TestDataTable.Columns.Add(AppointmentColumnNames.Identity, typeof(int));
            TestDataTable.Columns.Add(AppointmentColumnNames.EntityName, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.EntityIdentity, typeof(int));
            TestDataTable.Columns.Add(AppointmentColumnNames.SequenceNumber, typeof(short));
            TestDataTable.Columns.Add(AppointmentColumnNames.FunctionType, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.AppointmentBegin, typeof(DateTime));
            TestDataTable.Columns.Add(AppointmentColumnNames.AppointmentEnd, typeof(DateTime));
            TestDataTable.Columns.Add(AppointmentColumnNames.TimezoneDescription, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.Status, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.CreatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(AppointmentColumnNames.CreatedUserId, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.CreatedProgramCode, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.LastUpdatedDate, typeof(DateTime));
            TestDataTable.Columns.Add(AppointmentColumnNames.LastUpdatedUserId, typeof(string));
            TestDataTable.Columns.Add(AppointmentColumnNames.LastUpdatedProgramCode, typeof(string));
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
        public void Hydrate_HydratesAppointmentsSuccessfully()
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
            Assert.AreEqual(((short)3), firstElement.SequenceNumber);
            Assert.AreEqual("FunctionType", firstElement.FunctionType);
            Assert.AreEqual(new DateTime(1), firstElement.AppointmentBegin);
            Assert.AreEqual(new DateTime(2), firstElement.AppointmentEnd);
            Assert.AreEqual("TimezoneDescription", firstElement.TimezoneDescription);
            Assert.AreEqual("Status", firstElement.Status);
            Assert.AreEqual(new DateTime(3), firstElement.CreatedDate);
            Assert.AreEqual("CreatedUserId", firstElement.CreatedUserId);
            Assert.AreEqual("CreatedProgramCode", firstElement.CreatedProgramCode);
            Assert.AreEqual(new DateTime(4), firstElement.LastUpdatedDate);
            Assert.AreEqual("LastUpdatedUserId", firstElement.LastUpdatedUserId);
            Assert.AreEqual("LastUpdatedProgramCode", firstElement.LastUpdatedProgramCode);

            var secondElement = actual.ElementAt(1); //second row
            Assert.AreEqual(2, secondElement.Identity);
            Assert.AreEqual("EntityName1", secondElement.EntityName);
            Assert.AreEqual(3, secondElement.EntityIdentity);
            Assert.AreEqual(((short)4), secondElement.SequenceNumber);
            Assert.AreEqual("FunctionType1", secondElement.FunctionType);
            Assert.AreEqual(new DateTime(2), secondElement.AppointmentBegin);
            Assert.AreEqual(new DateTime(3), secondElement.AppointmentEnd);
            Assert.AreEqual("TimezoneDescription1", secondElement.TimezoneDescription);
            Assert.AreEqual("Status1", secondElement.Status);
            Assert.AreEqual(new DateTime(4), secondElement.CreatedDate);
            Assert.AreEqual("CreatedUserId1", secondElement.CreatedUserId);
            Assert.AreEqual("CreatedProgramCode1", secondElement.CreatedProgramCode);
            Assert.AreEqual(new DateTime(5), secondElement.LastUpdatedDate);
            Assert.AreEqual("LastUpdatedUserId1", secondElement.LastUpdatedUserId);
            Assert.AreEqual("LastUpdatedProgramCode1", secondElement.LastUpdatedProgramCode);
        }

        private DataRow GetTestDataRow(int? increment = null)
        {
            DataRow testDataRow = TestDataTable.NewRow();
            testDataRow[AppointmentColumnNames.Identity] = 1 + (increment ?? 0);
            testDataRow[AppointmentColumnNames.EntityName] = "EntityName" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.EntityIdentity] = 2 + (increment ?? 0);
            testDataRow[AppointmentColumnNames.SequenceNumber] = (short)(3 + (increment ?? 0));
            testDataRow[AppointmentColumnNames.FunctionType] = "FunctionType" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.AppointmentBegin] = new DateTime(1 + (increment ?? 0));
            testDataRow[AppointmentColumnNames.AppointmentEnd] = new DateTime(2 + (increment ?? 0));
            testDataRow[AppointmentColumnNames.TimezoneDescription] = "TimezoneDescription" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.Status] = "Status" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.CreatedDate] = new DateTime(3 + (increment ?? 0));
            testDataRow[AppointmentColumnNames.CreatedUserId] = "CreatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.CreatedProgramCode] = "CreatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.LastUpdatedDate] = new DateTime(4 + (increment ?? 0));
            testDataRow[AppointmentColumnNames.LastUpdatedUserId] = "LastUpdatedUserId" + (increment.HasValue ? increment.Value.ToString() : String.Empty);
            testDataRow[AppointmentColumnNames.LastUpdatedProgramCode] = "LastUpdatedProgramCode" + (increment.HasValue ? increment.Value.ToString() : String.Empty);

            return testDataRow;
        }
    }
}
