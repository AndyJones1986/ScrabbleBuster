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
            using (Common dbCommon = new Common("TESTDB"))
            {
                Assert.IsNotNull(Common.Database);
            };

            Assert.IsNull(Common.Database);
        }
    }
}
