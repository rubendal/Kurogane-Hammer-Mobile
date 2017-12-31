using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Kurogane_Hammer.Droid
{
    public class Notifications : PlatformDependent.INotification
    {
        private Context _context;

        public Notifications(Context context)
        {
            _context = context;
        }

        public void ShowNotification(string content)
        {
            Toast.MakeText(_context, content, ToastLength.Long).Show();
        }
    }
}