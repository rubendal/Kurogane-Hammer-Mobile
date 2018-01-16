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

            Nav.BarBackgroundColor = App.HeaderBackgroundColor;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailMainPageMenuItem;
            if (item == null)
                return;

            if (item.TargetType != null)
            {

                var page = (Page)Activator.CreateInstance(item.TargetType);

                if (!item.PushNavigation)
                {
                    if (Detail != null)
                    {
                        if (Detail.Navigation.NavigationStack.Last().GetType() != item.TargetType) //If current page is the same then ignore
                            SetDetail(page);                            
                    }
                    else
                        SetDetail(page);
                }
                else
                {
                    if(Detail != null)
                    {
                        if (Detail.Navigation.NavigationStack.Last().GetType() != item.TargetType)
                            Detail.Navigation.PushAsync(page);
                    }
                    else
                    {
                        //Shouldn't reach this
                        SetDetail(page);
                    }
                }
                IsPresented = false;
            }
            MasterPage.Items.SelectedItem = null;
        }

        private void SetDetail(Page page)
        {
            Detail = new NavigationPage(page)
            {
                BarBackgroundColor = App.HeaderBackgroundColor
            };
        }
    }
}