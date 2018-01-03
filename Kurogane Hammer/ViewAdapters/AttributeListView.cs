
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Kurogane_Hammer.PlatformDependent;
using Kurogane_Hammer.Classes;
using Kurogane_Hammer.Views.CharacterView;
using Newtonsoft.Json;
using System;
using Kurogane_Hammer.Views.AttributeView;

namespace Kurogane_Hammer.ViewAdapters
{
    public static class AttributeListView
    {
        public static async Task<View> Create(double x)
        {
            return await Task.Run<View>(() =>
            {
                Grid grid = new Grid
                {
                    ColumnSpacing = App.ScreenUnitConverter.PixelsToDIU(10),
                    RowSpacing = App.ScreenUnitConverter.PixelsToDIU(10)
                };

                List<AttributeName> attributes = Runtime.GetAttributeNames();

                int columns = (int)(App.ScreenUnitConverter.PixelsToDIU(App.ScreenWidth) / x);
                int rows = (int)Math.Ceiling((double)attributes.Count / columns);

                for (int i = 0; i < rows; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                for (int i = 0; i < columns; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }

                int row = 0;
                int column = 0;
                foreach (AttributeName a in attributes)
                {
                    AttributePicView view = new AttributePicView(a);

                    if (x > App.ScreenUnitConverter.PixelsToDIU(300))
                    {
                        view.Size = x - App.ScreenUnitConverter.PixelsToDIU(20);
                    }
                    else
                    {
                        view.Size = x;
                    }
                    int co = (column % columns);
                    grid.Children.Add(view, co, row);
                    column++;
                    if (column % columns == 0)
                    {
                        row++;
                    }
                }

                return grid;
            });
        }
    }
}
