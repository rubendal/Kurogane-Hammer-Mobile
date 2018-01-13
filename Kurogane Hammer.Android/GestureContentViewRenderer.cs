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

using Kurogane_Hammer.Droid;
using Kurogane_Hammer.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(GestureContentView), typeof(GestureContentViewRenderer))]
namespace Kurogane_Hammer.Droid
{
    public class GestureContentViewRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<GestureContentView, Android.Views.View>
    {
        private GestureListener _listener;
        private GestureDetector _detector;

        public GestureContentViewRenderer(Context context) : base(context)
        {
        }

        public GestureListener Listener
        {
            get
            {
                return _listener;
            }
        }

        public GestureDetector Detector
        {
            get
            {
                return _detector;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GestureContentView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                GenericMotion += HandleGenericMotion;
                Touch += HandleTouch;

                _listener = new GestureListener(Element);
                _detector = new GestureDetector(_listener);
            }
        }

        protected override void Dispose(bool disposing)
        {
            GenericMotion -= HandleGenericMotion;
            Touch -= HandleTouch;

            _listener = null;
            _detector?.Dispose();
            _detector = null;

            base.Dispose(disposing);
        }

        void HandleTouch(object sender, TouchEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }

        void HandleGenericMotion(object sender, GenericMotionEventArgs e)
        {
            _detector.OnTouchEvent(e.Event);
        }
    }

    public class GestureListener : GestureDetector.SimpleOnGestureListener
    {
        readonly GestureContentView _target;

        public GestureListener(GestureContentView s)
        {
            _target = s;
        }

        public override void OnLongPress(MotionEvent e)
        {
            _target.LongPressEvent();

            base.OnLongPress(e);
        }

        public override bool OnSingleTapConfirmed(MotionEvent e)
        {
            _target.SingleTapEvent();

            return base.OnSingleTapConfirmed(e);
        }
    }
}