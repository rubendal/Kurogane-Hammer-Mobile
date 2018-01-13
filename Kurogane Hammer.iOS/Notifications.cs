using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Kurogane_Hammer.iOS
{
    public class Notifications : PlatformDependent.INotification
    {
        private NSTimer alertDelay;
        private UIAlertController alert;

        public void ShowNotification(string content)
        {
            alertDelay = NSTimer.CreateScheduledTimer(3.5, (obj) =>
            {
                if (alert != null)
                {
                    alert.DismissViewController(true, null);
                }
                if (alertDelay != null)
                {
                    alertDelay.Dispose();
                }
            });
            alert = UIAlertController.Create(null, content, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

    }
}