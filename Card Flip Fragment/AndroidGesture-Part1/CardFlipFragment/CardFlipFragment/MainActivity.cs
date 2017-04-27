using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;

namespace CardFlipFragment
{
    [Activity(Label = "CardFlipFragment", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        GestureDetector gesturedetector;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            gesturedetector = new GestureDetector(this, new MyGestureListener());

            if (bundle == null)
            {
                FragmentTransaction trans = FragmentManager.BeginTransaction();

                trans.Add(Resource.Id.frameLayout1, new CardFrontFragment());
                trans.Commit();

            }

        }
        public class CardFrontFragment : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {

                View frontcard_view = inflater.Inflate(Resource.Layout.card_front, container, false);

                frontcard_view.Touch += Frontcard_view_Touch;
                return frontcard_view;
            }

            private void Frontcard_view_Touch(object sender, View.TouchEventArgs e)
            {
                MainActivity myactivity = Activity as MainActivity;

                myactivity.gesturedetector.OnTouchEvent(e.Event);
            }
        }


        public class MyGestureListener : GestureDetector.SimpleOnGestureListener
        {
            public override bool OnDoubleTap(MotionEvent e)
            {
                Console.WriteLine("Double Tab");
                return true;
            }

            public override void OnLongPress(MotionEvent e)
            {
                Console.WriteLine("Long Press");
            }
            public override bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
            {

                Console.WriteLine("Fling");
                return base.OnFling(e1, e2, velocityX, velocityY);
            }

            public override bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
            {
                Console.WriteLine("Scroll");
                return base.OnScroll(e1, e2, distanceX, distanceY);
            }
        }
    }
}

