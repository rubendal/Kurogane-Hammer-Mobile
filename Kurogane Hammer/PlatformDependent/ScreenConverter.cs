using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kurogane_Hammer.PlatformDependent
{
    public interface ScreenUnitConverter
    {
        double PixelsToDIU(double x);
        double DIUToPixels(double x);

        //double DPToDIU(double x);
        //double DIUToDP(double x);
    }
}
