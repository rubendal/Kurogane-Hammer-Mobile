using Kurogane_Hammer.iOS;
using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Kurogane_Hammer.Views;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(GestureContentView), typeof(GestureContentViewRenderer))]
namespace Kurogane_Hammer.iOS
{
    
    public class GestureContentViewRenderer : ViewRenderer<GestureContentView, UIView>
    {
        UILongPressGestureRecognizer longPressGestureRecognizer;
        UITapGestureRecognizer singleTapGestureRecognizer;

        protected override void OnElementChanged(ElementChangedEventArgs<GestureContentView> e)
        {
            longPressGestureRecognizer = longPressGestureRecognizer ??
                new UILongPressGestureRecognizer(() =>
                {
                    Element.LongPressEvent();
                });

            if (longPressGestureRecognizer != null)
            {
                if (e.NewElement == null)
                {
                    RemoveGestureRecognizer(longPressGestureRecognizer);
                }
                else if (e.OldElement == null)
                {
                    AddGestureRecognizer(longPressGestureRecognizer);
                }
            }

            singleTapGestureRecognizer = singleTapGestureRecognizer ??
                new UITapGestureRecognizer(() =>
                {
                    Element.SingleTapEvent();
                });

            if (singleTapGestureRecognizer != null)
            {
                if (e.NewElement == null)
                {
                    RemoveGestureRecognizer(singleTapGestureRecognizer);
                }
                else if (e.OldElement == null)
                {
                    AddGestureRecognizer(singleTapGestureRecognizer);
                }
            }
        }


    }
}
