using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

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
                    base.Icon = new FileImageSource() { File = "sync_icon.png" };
                else
                    base.Icon = new FileImageSource() { File = "sync_icon_disabled.png" };
            }
        }

        public SyncToolbar()
        {
            Icon = new FileImageSource() { File = "sync_icon.png" };
            Clicked += async (o, e) =>
            {
                if (!_Enabled)
                    return;

                Enabled = false;

                bool done = await KHUpdate.Start();

                if (done)
                    App.Notifications.ShowNotification("Sync successful");
                else
                    App.Notifications.ShowNotification("Sync error");

                Enabled = true;
            };
        }
    }
}
