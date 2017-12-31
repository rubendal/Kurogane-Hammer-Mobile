using Kurogane_Hammer.Classes;
using Kurogane_Hammer.ViewAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer.Views.AttributeView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttributesRanking : ContentPage
    {
        private AttributeName _Attribute;
        private GridTable _Table;

        public AttributesRanking()
        {
            InitializeComponent();
        }

        public AttributesRanking(AttributeName a, GridTable table, string title = null)
        {
            InitializeComponent();
            _Attribute = a;

            if (title == null)
                Title = $"{a.formattedName}";
            else
                Title = $"{a.formattedName}/{title}";

            _Table = table;

            Start();
        }

        private async void Start()
        {
            View v = await _Table.Generate();
            Layout.Content = v;
        }
    }
}