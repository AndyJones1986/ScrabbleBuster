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
            using (Data data = new Data("UNITTESTDB"))
            {
                Assert.IsNotNull(data.Database);
            };

        }

        [TestMethod]
        public void GetWordsCollection()
        {
            using (Data data = new Data("UNITTESTDB"))
            {

            }
        }
    }
}
