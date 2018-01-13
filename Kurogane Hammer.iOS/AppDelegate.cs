using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using FFImageLoading.Forms.Touch;

namespace Kurogane_Hammer.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            CachedImageRenderer.Init();

            App.ScreenWidth = UIScreen.MainScreen.NativeBounds.Width;
            App.ScreenHeight = UIScreen.MainScreen.NativeBounds.Height;
            App.ScreenDensity = UIScreen.MainScreen.Scale;
            App.ScreenUnitConverter = new ScreenUnitConverter(UIScreen.MainScreen.Scale);

            App.Notifications = new Notifications();

            LoadApplication(new App(new Storage(), new iOSAssetManager()));

            return base.FinishedLaunching(app, options);
        }
    }
}
