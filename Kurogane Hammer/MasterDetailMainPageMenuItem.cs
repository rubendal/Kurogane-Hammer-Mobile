using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kurogane_Hammer
{

    public class MasterDetailMainPageMenuItem
    {
        public MasterDetailMainPageMenuItem()
        {
            TargetType = null;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }

        public double FontSize { get; set; } = 18;
        public Color TextColor { get; set; } = Color.Black;

        
    }
}