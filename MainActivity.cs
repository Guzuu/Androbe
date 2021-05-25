using System;
using Android.App;
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

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fabPlus);
            fab.Click += FabOnClick;

            FloatingActionButton fabPhoto = FindViewById<FloatingActionButton>(Resource.Id.fabPhoto);
            fabPhoto.Click += FabPhotoOnClick;
        }

        private void FabPhotoOnClick(object sender, EventArgs e)
        {
            TakePhoto();
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

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            FindViewById(Resource.Id.fabFolder).Visibility = FabState;
            FindViewById(Resource.Id.fabGallery).Visibility = FabState;
            FindViewById(Resource.Id.fabPhoto).Visibility = FabState;

            if (FabState == ViewStates.Invisible) FabState = ViewStates.Visible;
            else FabState = ViewStates.Invisible;
        }

        async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize=Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                Name = "item.jpg",
                Directory = "sample"
            });

            if (file == null)
            {
                return;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            Bitmap bitmapImage = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
            ImgView.SetImageBitmap(bitmapImage);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
