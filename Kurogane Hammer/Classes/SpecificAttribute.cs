using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.Classes
{
    public class SpecificAttribute
    {
        public string attribute;
        public bool isSimple
        {
            get
            {
                return NameValueTable != null;
            }
        }
        public List<RowValue> NameValueTable;
        public List<string> headers;
        public List<List<string>> data;

        public SpecificAttribute()
        {

        }
    }

    public class RowValue
    {
        public string name, value;

        public RowValue()
        {

        }

        public RowValue(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
