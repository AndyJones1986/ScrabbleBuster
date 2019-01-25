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
    public class WordsTest
    {
        private const string testDbName = "UnitTestDB";

        [TestMethod]
        public void TestWords()
        {
            string[] words = File.ReadAllLines(@"..\..\Resources\wordList.csv");
            using (Words wordTable = new Words(testDbName))
            {
                wordTable.Delete();

                wordTable.Insert(from string word in words.AsEnumerable()
                                 select new Word(word));

                foreach (var grp in wordTable.FirstLetters.AsParallel())
                {
                    var wordBatch = grp.ToList();
                    var originalWords = words.Where(w => w.StartsWith(grp.Key.ToString())).ToList();
                    Assert.IsTrue(wordBatch.Distinct().Count() == originalWords.Distinct().Count());
                }

                wordTable.Delete();
            }
        }
        [TestMethod]
        public void TestRefresh()
        {
            using (Words wordTable = new Words(testDbName))
            {
                wordTable.Delete();
                Random rand = new Random();
                var words = Enumerable.Range(0, 50)
                                .Select(r => wordTable.SourceFileLines[rand.Next(wordTable.SourceFileLines.Count())])
                                .ToList();

                var firstLetters = words.GroupBy(wrd => wrd.Substring(0, 1)).Select(g => g.Key).ToList();

                wordTable.RefreshStorage();
                foreach (var letter in firstLetters.AsParallel())
                {
                    words.Where(w => w.StartsWith(letter.ToString())).AsParallel().ForAll(testW =>
                    {
                        Assert.IsTrue(wordTable.Select(wt => wt.Text.Substring(0, 1).ToString() == letter).Any(w => w.Text.ToUpper() == testW), testW);
                    });
                }
            }
        }

    }
}
