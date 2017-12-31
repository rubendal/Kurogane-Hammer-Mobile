﻿using Kurogane_Hammer.ViewAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttributePage : ContentPage
    {
        public AttributePage()
        {
            InitializeComponent();

            Start();
        }

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

            View v = await AttributeListView.Create(x);
            if (Layout.Children.Count > 0)
                Layout.Children.RemoveAt(0);
            Layout.Children.Add(v);
        }
    }
}