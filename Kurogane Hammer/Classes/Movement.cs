using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.Classes
{
    public class Movement
    {
        public string name, value;

        public Movement()
        {

        }

        public Movement(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public static Movement Convert(MovementData data)
        {
            return new Movement(data.Name, data.Value);
        }
    }

    public class MovementData
    {
        public string Name, Owner, Value;
        public int OwnerId;

        public MovementData()
        {

        }
    }
}
