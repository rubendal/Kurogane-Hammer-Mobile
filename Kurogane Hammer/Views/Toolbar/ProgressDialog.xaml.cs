using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Kurogane_Hammer.Views.Toolbar
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressDialog : PopupPage
	{
		public ProgressDialog () : base()
		{
			InitializeComponent ();
		}

        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        protected override Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1);
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            return true;
        }
    }
}