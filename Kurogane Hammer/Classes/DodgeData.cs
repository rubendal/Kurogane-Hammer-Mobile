using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.Classes
{
    public class DodgeData
    {
        public string attribute, intangibility, faf;

        public DodgeData()
        {

        }

        public DodgeData(string attribute, string intangibility, string faf)
        {
            this.attribute = attribute;
            this.intangibility = intangibility;
            this.faf = faf;
        }

        public DodgeData(Move move)
        {
            this.attribute = move.name;
            this.intangibility = move.hitboxActive;
            this.faf = move.FAF;
        }
    }
}
