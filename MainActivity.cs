using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Plugin.Media;

namespace Androbe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        ImageView ImgView;
        ViewStates FabState;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            ImgView = (ImageView)FindViewById(Resource.Id.imageView1);
            FabState = ViewStates.Visible;

            FloatingActionButton fabPlus = FindViewById<FloatingActionButton>(Resource.Id.fabPlus);
            fabPlus.Click += FabPlusOnClick;

            FloatingActionButton fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAdd);
            fabAdd.Click += FabAddOnClick;
        }

        private void FabAddOnClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AddItemActivity));
            StartActivity(intent);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabPlusOnClick(object sender, EventArgs eventArgs)
        {
            FindViewById(Resource.Id.fabFolder).Visibility = FabState;
            FindViewById(Resource.Id.fabAdd).Visibility = FabState;

            if (FabState == ViewStates.Invisible) FabState = ViewStates.Visible;
            else FabState = ViewStates.Invisible;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
