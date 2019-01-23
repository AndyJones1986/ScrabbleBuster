using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrabbleBusterData.Tables.Structure;
using System.Collections.Concurrent;

namespace ScrabbleBusterData.Tables
{
    public class Words : DataAccessBase<Word>
    {
        public string[] SourceFileLines { get { return File.ReadAllLines(@"..\..\Resources\wordList.csv"); } }
        public IEnumerable<IGrouping<char, Word>> FirstLetters
        {
            get
            {
                return base.Select()
                    .GroupBy(wrd => wrd.Letters.First());
            }
        }

        public Words(string instance = "") : base(instance)
        {

        }

        public void RefreshStorage()
        {
            ConcurrentBag<Word> newWords = new ConcurrentBag<Word>();
            List<IGrouping<char, Word>> existingData = FirstLetters.ToList();

            SourceFileLines.AsParallel().ForAll(newWord =>
            {
                char firstLetter = Convert.ToChar(newWord.Substring(0, 1));
                var wordGroup = existingData.Where(e => e.Key == firstLetter).ToList();
                if (wordGroup.Count > 0)
                {
                    wordGroup.ForEach(wg =>
                    {
                        if (!wg.Any(e => e.Text.ToUpper() == newWord.ToUpper()))
                        {
                            newWords.Add(new Word(newWord));
                        }
                    });
                }
                else
                {
                    newWords.Add(new Word(newWord));
                }

            });

            base.Insert(newWords.Distinct().ToList());

            //SourceFileLines.GroupBy(grp => new { letter = Convert.ToChar(grp.Substring(0, 1)) }).Select(g => g.Key.letter)
            //    .AsParallel().ForAll(c =>
            //    {
            //        var thisGroup = FirstLetters.Where(g => g.Key == c).ToList();
            //        var newWordsGroup = SourceFileLines.Where(s => Convert.ToChar(s.Substring(0, 1)) == c);
            //        foreach (var newWord in newWordsGroup.AsParallel())
            //        {
            //            if (base.Select(db => db.Text == newWord).Count() == 0)
            //            {
            //                base.Insert(new Word(newWord));
            //            }
            //        }
            //    });
        }
    }
}
