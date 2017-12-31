using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.Classes
{
    public class Formula
    {
        public string formula, equation, notes;

        public Formula()
        {

        }

        public Formula(string formula, string equation, string notes)
        {
            this.formula = formula;
            this.equation = equation;
            this.notes = notes;
        }
    }
}
