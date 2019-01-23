using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrabbleBusterData.Tables;
using ScrabbleBusterData.Tables.Structure;
using MoreLinq;

namespace ScrabbleBusterData.Tests.Tables
{
    [TestClass]
    public class LettersTest
    {
        private const string testDbName = "UnitTestDB";


        [TestMethod]
        public void TestRefresh()
        {
            using (Letters letterTable = new Letters(testDbName))
            {
                var letters = letterTable.SourceFileLines.Select(l => new { letter = Convert.ToChar(l.Split(',')[0]), count = Convert.ToInt32(l.Split(',')[2]) }).ToList();

                letterTable.RefreshStorage();
                var data = letterTable.Select();

                Assert.IsTrue(letters.Where(letter => data.Where(n => n.Character == letter.letter).Count() != letter.count).Count() == 0);


            }
        }
    }

}

