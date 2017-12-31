using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Kurogane_Hammer.ViewAdapters
{
    public class TextFormat
    {
        public double FontSize { get; set; } = 14;
        public Color TextColor { get; set; } = Color.Black;
        public Color Background { get; set; } = Color.Transparent;
        public TextAlignment HorizontalTextAlignment { get; set; } = TextAlignment.Start;
        public TextAlignment VerticalTextAlignment { get; set; } = TextAlignment.Start;
        public FontAttributes FontAttributes { get; set; } = FontAttributes.None;
        public string FontFamily { get; set; } = null;
        public double WidthRequest { get; set; } = 0;
        public double HeightRequest { get; set; } = 0;
        public double XTranslation { get; set; } = 0;
        public double YTranslation { get; set; } = 0;

        public TextFormat()
        {
            FontFamily = Font.Default.FontFamily;

        }
    }
}
