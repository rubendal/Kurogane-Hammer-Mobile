using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Kurogane_Hammer.Views.Toolbar
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressDialog : PopupPage
	{
		public ProgressDialog () : base()
		{
			InitializeComponent ();
		}

        public ProgressDialog(string message) : base()
        {
            InitializeComponent();

            Message.Text = message;
            Message.IsVisible = true;
        }

        protected override Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.8);
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