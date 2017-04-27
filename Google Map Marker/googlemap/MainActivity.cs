using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;

using Android.Gms.Maps.Model;  // Add this Two Name Space
using Android.Graphics;

namespace googlemap
{
    [Activity(Label = "googlemap", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {

        private GoogleMap GMap;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

        
            SetUpMap();
        }

        private void SetUpMap()
        {
            if (GMap == null)
            {
               FragmentManager.FindFragmentById<MapFragment>(Resource.Id.googlemap).GetMapAsync(this);

            }
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            this.GMap = googleMap;
            GMap.UiSettings.ZoomControlsEnabled = true;

            DrawCircle(GMap);  //Calling  Draw Circle

            LatLng latlng = new LatLng(Convert.ToDouble(13.0291), Convert.ToDouble(80.2083));
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 15);
            GMap.MoveCamera(camera);


            MarkerOptions options = new MarkerOptions()
                        .SetPosition(latlng)
                        .SetTitle("Chennai");

            GMap.AddMarker(options);

        }
        public void DrawCircle(GoogleMap gMap)
        {
            Circle circle;

            var CircleMaker = new CircleOptions();          
                var latlong = new LatLng(13.0291, 80.2083); //Location 
                CircleMaker.InvokeCenter(latlong); //
                CircleMaker.InvokeRadius(1000);  //Radius in Circle
                CircleMaker.InvokeStrokeWidth(4);
                CircleMaker.InvokeStrokeColor(Android.Graphics.Color.ParseColor("#e6d9534f"));  //Circle Color
                CircleMaker.InvokeFillColor(Color.Argb(034, 209, 72, 54));
                CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlong, 15); //Map Zoom Level
                circle = GMap.AddCircle(CircleMaker);  //Gmap Add Circle
        
           

        }
    }
}

