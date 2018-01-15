using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Kurogane_Hammer
{
    public class OrientationPage : ContentPage
    {
        private double _width;
        private double _height;

        public event EventHandler<PageOrientationEventArgs> OnOrientationChanged = (e, a) => { };

        public OrientationPage() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            _width = Width;
            _height = Height;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            double pWidth = _width;

            base.OnSizeAllocated(width, height);
            if (Equals(_width, width) && Equals(_height, height)) return;

            _width = width;
            _height = height;

            if (Equals(pWidth, -1)) return;

            if (!Equals(width, pWidth))
                OnOrientationChanged.Invoke(this, new PageOrientationEventArgs((width < height) ? PageOrientation.Portrait : PageOrientation.Landscape));
        }
    }

    public class PageOrientationEventArgs : EventArgs
    {
        public PageOrientation Orientation { get; }

        public PageOrientationEventArgs(PageOrientation orientation)
        {
            Orientation = orientation;
        }
    }

    public enum PageOrientation
    {
        Landscape = 0,
        Portrait = 1,
    }
}
