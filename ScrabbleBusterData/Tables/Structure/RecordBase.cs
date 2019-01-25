using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrabbleBusterData.Tables.Structure
{
    public class RecordBase
    {
        public DateTime Created { get; set; }

        public int Id { get; set; }
        public RecordBase()
        {
            Created = DateTime.Now;
        }
    }
}
