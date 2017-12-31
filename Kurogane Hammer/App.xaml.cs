using Kurogane_Hammer.ViewAdapters;
//using Plugin.DeviceOrientation.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kurogane_Hammer
{
    public partial class App : Application
    {
        public static PlatformDependent.Storage storage { get; set; }
        public static PlatformDependent.AssetResourceManager assetManager { get; set; }

        public static double ScreenWidth { get; set; }
        public static double ScreenHeight { get; set; }
        public static double ScreenDensity { get; set; }

        public static PlatformDependent.ScreenUnitConverter ScreenUnitConverter { get; set; }
        public static PlatformDependent.INotification Notifications { get; set; }

        //public static DeviceOrientations CurrentOrientation { get; set; } = DeviceOrientations.Undefined;

        public static TextFormat HeaderTableFormat = new TextFormat()
        {

        };

        public static TextFormat ContentTableFormat = new TextFormat()
        {

        };

        public static int DpWidth
        {
            get
            {
                return (int)(ScreenWidth / ScreenDensity);
            }
        }

        public static int DpHeight
        {
            get
            {
                return (int)(ScreenHeight / ScreenDensity);
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new Kurogane_Hammer.MasterDetailMainPage();
        }

        public App(PlatformDependent.Storage storage, PlatformDependent.AssetResourceManager assetManager)
        {
            InitializeComponent();

            App.storage = storage;
            App.assetManager = assetManager;

            Initialization.Initialize(storage, assetManager);

            Runtime.InitializeRuntime();

            MainPage = new Kurogane_Hammer.MasterDetailMainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
