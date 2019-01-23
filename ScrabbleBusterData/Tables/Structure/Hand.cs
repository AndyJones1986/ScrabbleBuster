using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleBusterData.Tables.Structure
{
    public class Hand : TableBase
    {
        IEnumerable<Letter> CharactersInHand { get; set; }
        private bool CanAdd
        {
            get
            {
                return CharactersInHand.Count() <= 7;
            }
        }

        public Hand()
        {
            CharactersInHand = new IEnumerable<Letter>();
        }

        public int AddLetter(char character)
        {
            if (CanAdd)
            {
                CharactersInHand.add
            }
        }
    }
}
