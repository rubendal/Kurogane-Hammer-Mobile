using Kurogane_Hammer.ViewAdapters;
//using Plugin.DeviceOrientation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using Plugin.DeviceOrientation.Abstractions;

namespace Kurogane_Hammer
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //CrossDeviceOrientation.Current.OrientationChanged += OrientationChanged;

            Start();
        }

        //private void OrientationChanged(object sender, OrientationChangedEventArgs e)
        //{
        //    if(App.CurrentOrientation == DeviceOrientations.Undefined)
        //    {
        //        App.CurrentOrientation = e.Orientation;
        //        return;
        //    }
        //    if (App.CurrentOrientation != e.Orientation)
        //    {
        //        double min = Math.Min(App.ScreenWidth, App.ScreenHeight), max = Math.Max(App.ScreenWidth, App.ScreenHeight);
        //        if (e.Orientation == DeviceOrientations.Landscape || e.Orientation == DeviceOrientations.LandscapeFlipped)
        //        {
        //            App.ScreenWidth = max;
        //            App.ScreenHeight = min;
        //        }
        //        else if (e.Orientation == DeviceOrientations.Portrait || e.Orientation == DeviceOrientations.PortraitFlipped)
        //        {
        //            App.ScreenWidth = min;
        //            App.ScreenHeight = max;
        //        }
        //        App.CurrentOrientation = e.Orientation;
        //        Start();
        //    }
        //}

        public async void Start()
        {
            double x = App.ScreenUnitConverter.PixelsToDIU(300);
            if (App.ScreenWidth <= 800)
            {
                x = App.ScreenUnitConverter.PixelsToDIU((App.ScreenWidth / 2) - 5);
            }
            else if (App.ScreenWidth > 800 && App.ScreenWidth <= 1800)
            {
                x = App.ScreenUnitConverter.PixelsToDIU(350);
            }
            else
            {
                x = App.ScreenUnitConverter.PixelsToDIU(400);
            }

            View v = await CharacterListView.Create(x);
            if (Layout.Children.Count > 0)
                Layout.Children.RemoveAt(0);
            Layout.Children.Add(v);

            try
            {
                
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.InnerException);
            }
        }
    }
}
