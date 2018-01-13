using Kurogane_Hammer.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Kurogane_Hammer.ViewAdapters;
using System;

namespace Kurogane_Hammer.Views.AttributeView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttributePicView : ContentView
    {
        private AttributeName _Attribute;
        private string _AttributeImage, _AttributeName;
        private double _Size = 200;

        private bool _IsFavorite = false;
        private bool IsFavorite
        {
            get
            {
                return IsFavorite;
            }
            set
            {
                _IsFavorite = value;

                _Attribute.favorite = value;

                Runtime.UpdateFavorite(_Attribute, value);

                UpdateFavorites();
            }
        }

        public double Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
                RefreshView();
            }
        }

        public AttributeName Attribute
        {
            get
            {
                return _Attribute;
            }
            set
            {
                _Attribute = value;
                _AttributeImage = _Attribute.Image;
                _AttributeName = _Attribute.formattedName;
                _IsFavorite = _Attribute.favorite;
                RefreshView();
            }
        }

        public string AttributeImage
        {
            get
            {
                return _AttributeImage;
            }
            set
            {
                _AttributeImage = value;
                RefreshView();
            }
        }
        public string AttributeName
        {
            get
            {
                return _AttributeName;
            }
            set
            {
                _AttributeName = value;
                RefreshView();
            }
        }

        public AttributePicView()
        {
            InitializeComponent();

            c_Layout.Padding = new Thickness(App.ScreenUnitConverter.PixelsToDIU(10));
        }

        public AttributePicView(string ImgSource, string Name) : this()
        {
            _AttributeImage = ImgSource;
            _AttributeName = Name;
            RefreshView();
        }

        public AttributePicView(AttributeName attribute) : this()
        {
            Attribute = attribute;
        }

        public void RefreshView()
        {
            img_Image.Source = AttributeImage;
            img_Image.WidthRequest = Size;
            img_Image.HeightRequest = Size;
            lb_Name.Text = AttributeName.ToUpper();
            lb_Name.HeightRequest = App.ScreenUnitConverter.PixelsToDIU(70);
            lb_Name.TranslationY = Size - lb_Name.HeightRequest;
            lb_Name.WidthRequest = Size;
            lb_Name.FontSize = App.ScreenUnitConverter.PixelsToDIU(32);
            UpdateFavorites();
        }

        private void UpdateFavorites()
        {
            fav_Image.IsVisible = _IsFavorite;
            fav_Image.TranslationX = Size - fav_Image.WidthRequest;
        }

        private async void GestureContentView_SingleTapEvent(object sender, EventArgs e)
        {
            GridTable table = new GridTable(new TextFormat() { Background = Color.FromHex("#D0EAFF") }, new TextFormat());

            List<Classes.Attribute> list = Runtime.GetAttributeToRank(_Attribute.name);

            List<AttributeRank> ranking = AttributeRankMaker.BuildRankList(list);

            List<string> headers = new List<string>() { "Rank", "Character" };

            if (ranking.Count > 0)
            {
                headers.AddRange(ranking[0].types);

                table.AddHeader(headers);

                foreach (AttributeRank ar in ranking)
                {
                    List<string> r = new List<string>();

                    r.Add(ar.rank.ToString());
                    r.Add(ar.GetCharacterName());

                    foreach (string t in ar.types)
                    {
                        r.Add(ar.values[t]);
                    }

                    table.AddRow(r);
                }



                await Navigation.PushAsync(new AttributesRanking(_Attribute, table));
            }
        }

        private void GestureContentView_LongPressEvent(object sender, EventArgs e)
        {
            IsFavorite = !_IsFavorite;
        }

    }
}