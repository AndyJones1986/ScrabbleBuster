using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleBusterData.Tables.Structure
{
    public class Letter : TableBase
    {      
        
        public char Character { get; set; }
        public int Score { get; set; }
    }
}
