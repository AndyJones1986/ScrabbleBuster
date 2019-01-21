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
        public string Value { get; set; }
        public IEnumerable<string> Letters
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Value))
                {
                    return Value.Select(letter => new string(letter, 1)).AsEnumerable();
                }
                else
                {
                    return null;
                }
            }
        }
        public Word(string value)
        {
            this.Value = value;
        }
    }
}
