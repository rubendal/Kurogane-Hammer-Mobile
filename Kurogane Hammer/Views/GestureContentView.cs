using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Kurogane_Hammer.Views
{
	public class GestureContentView : ContentView
	{
		public GestureContentView ()
		{
		    
		}

        public event EventHandler<EventArgs> LongPress;

        public void LongPressEvent()
        {
            if (IsEnabled)
                LongPress?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> SingleTap;

        public void SingleTapEvent()
        {
            if (IsEnabled)
                SingleTap?.Invoke(this, EventArgs.Empty);
        }

    }
}