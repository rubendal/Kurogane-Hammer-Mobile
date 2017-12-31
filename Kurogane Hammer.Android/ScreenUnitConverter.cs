using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Kurogane_Hammer.Droid
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