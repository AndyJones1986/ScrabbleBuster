using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleBusterData.Tables.Structure
{
    [Serializable()]
    public class Word : TableBase
    {
        public string Text { get; set; }
        public IEnumerable<char> Letters
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Text))
                {
                    return Text.Select(letter => Convert.ToChar(new string(letter, 1))).AsEnumerable();
                }
                else
                {
                    return null;
                }
            }
        }

        public Word()
        {

        }

        public Word(string text)
        {
            this.Text = text;
        }
    }
}
