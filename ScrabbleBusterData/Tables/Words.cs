﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrabbleBusterData.Tables.Structure;
using System.Collections.Concurrent;

namespace ScrabbleBusterData.Tables
{
    public class Words : TableBase<Word>
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

        public override void RefreshStorage()
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
        }
    }
}
