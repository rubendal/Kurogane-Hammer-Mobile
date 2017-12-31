using Kurogane_Hammer.Classes;
using Kurogane_Hammer.ViewAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer.Views.CharacterView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterMoves : ContentPage
    {
        private Character _Character;
        private GridTable _Table;

        public CharacterMoves()
        {
            InitializeComponent();
        }

        public CharacterMoves(Character c, GridTable table, string title)
        {
            InitializeComponent();
            _Character = c;

            if (title == null)
                Title = $"{c.getCharacterTitleName()}";
            else
                Title = $"{c.getCharacterTitleName()}/{title}";

            _Table = table;

            Start();
        }

        public CharacterMoves(Character c, View view, string title)
        {
            InitializeComponent();

            _Character = c;

            if (title == null)
                Title = $"{c.getCharacterTitleName()}";
            else
                Title = $"{c.getCharacterTitleName()}/{title}";

            Layout.Content = view;
        }

        private async void Start()
        {
            View v = await _Table.Generate();
            Layout.Content = v;
            //if (Layout.Children.Count > 0)
            //    Layout.Children.RemoveAt(0);
            //Layout.Children.Add(v);
        }
    }
}