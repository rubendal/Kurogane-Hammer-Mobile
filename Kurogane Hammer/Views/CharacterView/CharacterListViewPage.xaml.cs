using Kurogane_Hammer.Classes;
using Kurogane_Hammer.ViewAdapters;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer.Views.CharacterView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterListViewPage : ContentPage
    {
        private Character _Character { get; set; }

        public ObservableCollection<string> Items { get; set; }

        public CharacterListViewPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {

            };

            MyListView.ItemsSource = Items;
        }

        public CharacterListViewPage(Character c)
        {
            InitializeComponent();

            _Character = c;

            Items = new ObservableCollection<string>
            {
                "All data",
                "Attributes",
                "Detailed attributes",
                "All Moves",
                "Ground Moves",
                "Throws",
                "Aerial Moves",
                "Special Moves"
            };

            if (_Character.DisplayName == "Little Mac")
                Items[6] = "Aerial Moves (Please don't actually use these)";

            if (_Character.hasSpecificAttributes)
                Items.Add(_Character.specificAttribute.attribute);

            MyListView.ItemsSource = Items;

            Title = c.getCharacterTitleName();
        }

        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            ((ListView)sender).SelectedItem = null;

            int index = Items.IndexOf((string)e.Item);

            await Navigation.PushAsync(new CharacterMoves(_Character, index));
        }
    }
}
