using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleBusterData.Tables.Structure
{
    public class Game : RecordBase
    {
        public bool? Won { get; set; }
        public List<int> Hands { get; set; }
        public List<int> MyWords { get; set; }
        public List<int> TheirWords { get; set; }
        public int MyScore { get; set; }
        public int TheirScore { get; set; }
        public Game()
        {
            Hands = new List<int>();
            TheirWords = new List<int>();
            MyWords = new List<int>();
        }
    }
}
