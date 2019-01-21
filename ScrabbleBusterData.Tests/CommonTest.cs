using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrabbleBusterData;

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
        public void DBBasics()
        {
            using (TestDB db = new TestDB(testDbName))
            {
                db.Delete();
            }
            DBInsertTest();
            DBDeleteTest();
        }

        private void DBInsertTest()
        {
            using (TestDB db = new TestDB(testDbName))
            {
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

            };
        }

        private void DBDeleteTest()
        {
            using (TestDB db = new TestDB(testDbName))
            {
                Assert.AreEqual(db.Select().Count(), TestData().Count(), "Record Mismatch");

                TestData().ForEach(testItem =>
                {
                    int deleted = db.Delete(LiteDB.Query.Where("Value", value => value.AsString == testItem.Value));
                    Assert.AreEqual(deleted, 1, "Deleted Record Mismatch");
                });

                DBInsertTest();

                int fullDelete = db.Delete();
                Assert.AreEqual(fullDelete, TestData().Count(), "Deleted Record Mismatch");
            }
        }

    }

    public class TestObject : Tables.Structure.TableBase
    {
        public string Value { get; set; }
    }

    public class TestDB : DataAccessBase<TestObject>
    {
        public TestDB(string instanceName) : base(instanceName)
        {

        }
    }
}
