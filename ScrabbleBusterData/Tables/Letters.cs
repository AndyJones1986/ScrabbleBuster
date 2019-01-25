using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrabbleBusterData.Tables.Structure;
using System.IO;
using System.Collections.Concurrent;

namespace ScrabbleBusterData.Tables
{
    public class Letters : TableBase<Letter>
    {
        public string[] SourceFileLines { get { return File.ReadAllLines(@"..\..\Resources\ScrabbleLetters.csv"); } }
        public Letters(string instance = "") : base(instance)
        {

        }

        public Letters() : base("")
        {
        }

        public override void RefreshStorage()
        {
            base.Delete();
            ConcurrentBag<Letter> letters = new ConcurrentBag<Letter>();
            SourceFileLines.AsParallel().ForAll(src =>
            {
                string[] columns = src.Split(',');
                int count = Convert.ToInt32(columns[2]);
                int i = 0;
                while (i < count)
                {
                    letters.Add(new Letter() { Character = Convert.ToChar(columns[0]), Score = Convert.ToInt32(columns[1]) });
                    i++;
                }
            });
            base.Insert(letters);
        }
    }
}
