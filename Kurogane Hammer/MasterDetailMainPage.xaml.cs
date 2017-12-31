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
    public partial class MasterDetailMainPage : MasterDetailPage
    {
        public MasterDetailMainPage()
        {
            InitializeComponent();
            MasterPage.Items.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailMainPageMenuItem;
            if (item == null)
                return;

            if (item.TargetType != null)
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                //page.Title = item.Title;

                Detail = new NavigationPage(page);
                IsPresented = false;
            }
            MasterPage.Items.SelectedItem = null;
        }
    }
}