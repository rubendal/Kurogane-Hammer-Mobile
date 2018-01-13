using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Kurogane_Hammer.iOS
{
    public class ScreenUnitConverter : PlatformDependent.ScreenUnitConverter
    {
        private double _Density { get; set; } = 1;

        public ScreenUnitConverter(double Density)
        {
            _Density = Density;
        }

        public double DIUToPixels(double x)
        {
            return x * _Density;
        }

        public double PixelsToDIU(double x)
        {
            return x / _Density;
        }
    }
}