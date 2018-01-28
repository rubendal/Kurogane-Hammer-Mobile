using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

namespace Kurogane_Hammer.Views.Toolbar
{
    public class SyncToolbar : ToolbarItem
    {
        private bool _Enabled = true;
        public bool Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                if(_Enabled)
                    Icon = new FileImageSource() { File = "sync_icon.png" };
                else
                    Icon = new FileImageSource() { File = "sync_icon_disabled.png" };
            }
        }

        public SyncToolbar()
        {
            Icon = new FileImageSource() { File = "sync_icon.png" };
            Clicked += async (o, e) =>
            {
                if (!_Enabled)
                    return;

                await PopupNavigation.PushAsync(new ProgressDialog("Syncing with KH API"));

                Enabled = false;

                bool done = await KHUpdate.Start();

                await PopupNavigation.PopAsync();

                if (done)
                    App.Notifications.ShowNotification("Sync successful");
                else
                    App.Notifications.ShowNotification("Sync error");

                Enabled = true;
            };
        }
    }
}
