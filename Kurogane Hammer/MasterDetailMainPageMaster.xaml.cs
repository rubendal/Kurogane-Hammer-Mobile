using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kurogane_Hammer
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailMainPageMaster : ContentPage
    {
        public ListView Items;

        public MasterDetailMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailMainPageMasterViewModel();
            Items = MenuItemsListView;
        }

        class MasterDetailMainPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailMainPageMenuItem> MenuItems { get; set; }
            public ObservableCollection<MasterDetailMainPageMenuItem> MenuItems2 { get; set; }

            public MasterDetailMainPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailMainPageMenuItem>(new[]
                {
                    new MasterDetailMainPageMenuItem { Id = -1, Title = "Smash 4", TextColor = Color.FromHex("#808080") },
                    new MasterDetailMainPageMenuItem { Id = 0, Title = "Characters", TargetType = typeof(MainPage) },
                    new MasterDetailMainPageMenuItem { Id = 1, Title = "Attributes", TargetType = typeof(AttributePage) },
                    new MasterDetailMainPageMenuItem { Id = 2, Title = "Formulas" },
                    new MasterDetailMainPageMenuItem { Id = 3, Title = "Sm4sh Calculator" },
                    new MasterDetailMainPageMenuItem { Id = 4, Title = "About", TargetType = typeof(MainPage) }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}