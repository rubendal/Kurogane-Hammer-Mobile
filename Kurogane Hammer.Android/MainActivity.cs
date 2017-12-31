﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using FFImageLoading.Forms.Droid;
//using Plugin.CurrentActivity;

namespace Kurogane_Hammer.Droid
{
    [Activity(Label = "Kurogane_Hammer", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init(true);

            var metrics = Resources.DisplayMetrics;
            App.ScreenWidth = metrics.WidthPixels;
            App.ScreenHeight = metrics.HeightPixels;
            App.ScreenDensity = metrics.Density;
            App.ScreenUnitConverter = new ScreenUnitConverter(App.ScreenDensity);
            App.Notifications = new Notifications(this);

            //CrossCurrentActivity.Current.Activity = this;

            LoadApplication(new App(new Storage(this), new AndroidAssetManager(this)));
        }
    }
}

