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
        public bool isSimple = true;
        public List<RowValue> valueList;
        public List<string> headers;
        public List<List<string>> rows;

        public SpecificAttribute()
        {

        }

        public SpecificAttribute(string attribute, List<RowValue> valueList)
        {
            this.attribute = attribute;
            this.isSimple = true;
            this.valueList = valueList;
        }

        public SpecificAttribute(string attribute, List<string> headers, List<List<string>> rows)
        {
            this.attribute = attribute;
            this.isSimple = false;
            this.headers = headers;
            this.rows = rows;
        }

        public void Check()
        {
            isSimple = valueList != null;
        }
    }

    public class RowValue
    {
        public string name, value;

        public RowValue(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
