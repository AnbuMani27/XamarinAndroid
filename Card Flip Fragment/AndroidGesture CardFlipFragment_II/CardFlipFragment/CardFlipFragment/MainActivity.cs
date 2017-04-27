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

        private bool Showingback;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            gesturedetector = new GestureDetector(this, new MyGestureListener(this));
            if (bundle == null)
            {
                FragmentTransaction trans = FragmentManager.BeginTransaction();

                trans.Add(Resource.Id.frameLayout1, new CardFrontFragment());
                trans.Commit();

            }


        }
        public void FlipCard()
        {
            if (Showingback)
            {
                FragmentManager.PopBackStack();

                Showingback = false;
            }
            else
            {
                FragmentTransaction trans = FragmentManager.BeginTransaction();
                trans.SetCustomAnimations(Resource.Animation.card_flip_right_in, Resource.Animation.card_flip_right_out,
                                          Resource.Animation.card_flip_left_in, Resource.Animation.card_flip_left_out);
                trans.Replace(Resource.Id.frameLayout1, new CardBackFragment());
                trans.AddToBackStack(null);
                trans.Commit();
                Showingback = true;
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
        public class CardBackFragment : Fragment
        {
            public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {

                View backcard_view = inflater.Inflate(Resource.Layout.Card_Back, container, false);
                backcard_view.Touch += Backcard_view_Touch;

                return backcard_view;
            }

            private void Backcard_view_Touch(object sender, View.TouchEventArgs e)
            {
                MainActivity myactivity = Activity as MainActivity;

                myactivity.gesturedetector.OnTouchEvent(e.Event);
            }
        }


        public class MyGestureListener : GestureDetector.SimpleOnGestureListener
        {
            private MainActivity mainActivity;

            public MyGestureListener(MainActivity Activity)
            {
                mainActivity = Activity;
            }

            public override bool OnDoubleTap(MotionEvent e)
            {
                //Console.WriteLine("Double Tab");

                mainActivity.FlipCard();
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

