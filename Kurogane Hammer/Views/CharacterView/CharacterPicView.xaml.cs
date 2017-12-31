using Kurogane_Hammer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Behaviors;
using XLabs.Forms.Controls;

namespace Kurogane_Hammer.Views.CharacterView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterPicView : ContentView
    {
        private Character _Character;
        private string _CharacterImage, _CharacterName;
        private double _Size = 200;

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

        public Character Character
        {
            get
            {
                return _Character;
            }
            set
            {
                _Character = value;
                _CharacterImage = _Character.Image;
                _CharacterName = _Character.GetCharacterImageName();
                RefreshView();
            }
        }

        public string CharacterImage
        {
            get
            {
                return _CharacterImage;
            }
            set
            {
                _CharacterImage = value;
                RefreshView();
            }
        }
        public string CharacterName
        {
            get
            {
                return _CharacterName;
            }
            set
            {
                _CharacterName = value;
                RefreshView();
            }
        }

        public CharacterPicView()
        {
            InitializeComponent();

            c_Layout.Padding = new Thickness(App.ScreenUnitConverter.PixelsToDIU(10));
        }

        public CharacterPicView(string ImgSource, string Name) : this()
        {
            _CharacterImage = ImgSource;
            _CharacterName = Name;
            RefreshView();
        }

        public CharacterPicView(Character character) : this()
        {
            Character = character;
        }

        public void RefreshView()
        {
            img_Image.Source = CharacterImage;
            img_Image.WidthRequest = Size;
            img_Image.HeightRequest = Size;
            lb_Name.Text = CharacterName.ToUpper();
            lb_Name.HeightRequest = App.ScreenUnitConverter.PixelsToDIU(70);
            lb_Name.TranslationY = Size - lb_Name.HeightRequest;
            lb_Name.WidthRequest = Size;
            lb_Name.FontSize = App.ScreenUnitConverter.PixelsToDIU(32);
        }

        public void GestureRecognized(object sender, GestureResult e)
        {
            switch (e.GestureType)
            {
                case GestureType.SingleTap:
                    Navigation.PushAsync(new CharacterListViewPage(_Character));
                    break;
                case GestureType.LongPress:
                    App.Notifications.ShowNotification("Long press");
                    break;
            }
        }

    }
}