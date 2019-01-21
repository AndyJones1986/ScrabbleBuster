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
        [TestMethod]
        public void InsantiateDBTest()
        {
            List<string> testData = new List<string>();

            testData.Add("one");
            testData.Add("two");
            testData.Add("three");
            testData.Add("four");

            using (TestDB db = new TestDB("UnittestDB"))
            {
                testData.ForEach(testString => db.Insert(new TestObject() { Value = testString }));
                testData.ForEach(testString =>
                {
                    Assert.IsFalse(db.Select(item => { return item.Value == testString; }).Count() == 0, string.Format("{0} not found", testString));
                });

                var storedData = db.Select();
                storedData.ToList().ForEach(dbItem => {

                });
            };
        }

    }

    class TestObject
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    class TestDB : DataAccessBase<TestObject>
    {
        public TestDB(string instanceName) : base(instanceName)
        {

        }
    }
}
