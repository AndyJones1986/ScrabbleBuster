using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrabbleBusterData;
using ScrabbleBusterData.Tables.Structure;

namespace ScrabbleBusterData.Tests
{
    [TestClass]
    public class CommonTest
    {
        private const string testDbName = "UnitTestDB";
        private List<TestObject> TestData()
        {
            List<TestObject> testData = new List<TestObject>();

            testData.Add(new TestObject() { Value = "one" });
            testData.Add(new TestObject() { Value = "two" });
            testData.Add(new TestObject() { Value = "three" });
            testData.Add(new TestObject() { Value = "four" });
            return testData;
        }

        [TestMethod]
        public void DBInsertTest()
        {
            using (TestDB db = new TestDB(testDbName + "INSERT"))
            {
                db.Delete();
                TestData().ForEach(testString => db.Insert(testString));
                TestData().ForEach(testString =>
                {
                    Assert.IsTrue(db.Select(item => { return item.Value == testString.Value; }).Count() == 1, string.Format("{0} not found", testString.Value));
                });

                var storedData = db.Select();
                storedData.ToList().ForEach(dbItem =>
                {
                    Assert.IsTrue(TestData().Any(item => item.Value == dbItem.Value), string.Format("Crosscheck fail {0}", dbItem));
                });

                int fullDelete = db.Delete();
                Assert.AreEqual(fullDelete, TestData().Count(), "Deleted Record Mismatch");
            };
        }
        [TestMethod]
        public void DBDeleteTest()
        {
            using (TestDB db = new TestDB(testDbName + "DELETE"))
            {
                db.Delete();
                db.Insert(TestData());
                Assert.AreEqual(db.Select().Count(), TestData().Count(), "Record Mismatch");

                TestData().ForEach(testItem =>
                {
                    int deleted = db.Delete(LiteDB.Query.Where("Value", value => value.AsString == testItem.Value));
                    Assert.AreEqual(deleted, 1, "Deleted Record Mismatch");
                });

                db.Insert(TestData());
                int fullDelete = db.Delete();
                Assert.AreEqual(fullDelete, TestData().Count(), "Deleted Record Mismatch");
            }
        }

        [TestMethod]
        public void DBUpdateTest()
        {
            using (TestDB db = new TestDB(testDbName + "UPDATE"))
            {
                db.Delete();
                db.Insert(TestData());

                db.Select().ToList().ForEach(record =>
                {
                    record.Value = "adjusted";
                    db.Update(record);

                    TestObject adjustedRecord = db.Select(x => x.Id == record.Id).First();

                    Assert.AreEqual(record.Value, adjustedRecord.Value, "UPDATE FAILED");
                });

                int fullDelete = db.Delete();
                Assert.AreEqual(fullDelete, TestData().Count(), "Deleted Record Mismatch");
            }
        }

    }

    public class TestObject : TableBase
    {
        public string Value { get; set; }
    }

    public class TestDB : DataAccessBase<TestObject>
    {
        public override void RefreshStorage()
        {
            throw new NotImplementedException();
        }
        public TestDB(string instanceName) : base(instanceName)
        {

        }
    }
}
