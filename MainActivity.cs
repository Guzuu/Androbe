using System;
using System.Threading.Tasks;
using Androbe.Clothes;
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
        LinearLayout linearLayoutImages;
        ViewStates FabState;
        ImageView imgViewHat, imgViewShirt, imgViewPants, imgViewShoes;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            FabState = ViewStates.Visible;

            imgViewHat = (ImageView)FindViewById(Resource.Id.imageViewHat);
            imgViewHat.Click += ImgViewHat_Click;

            imgViewShirt = (ImageView)FindViewById(Resource.Id.imageViewShirt);
            imgViewShirt.Click += ImgViewShirt_Click;

            imgViewPants = (ImageView)FindViewById(Resource.Id.imageViewPants);
            imgViewPants.Click += ImgViewPants_Click;

            imgViewShoes = (ImageView)FindViewById(Resource.Id.imageViewShoes);
            imgViewShoes.Click += ImgViewShoes_Click;

            FloatingActionButton fabPlus = FindViewById<FloatingActionButton>(Resource.Id.fabPlus);
            fabPlus.Click += FabPlusOnClick;

            FloatingActionButton fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAdd);
            fabAdd.Click += FabAddOnClick;

            linearLayoutImages = FindViewById<LinearLayout>(Resource.Id.linearLayoutImages);
        }

        private void ImgViewShoes_Click(object _sender, EventArgs _e)
        {
            linearLayoutImages.RemoveAllViews();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath;
            filePath = System.IO.Path.Combine(path, "shoes.json");
            var list = Services.ReadFromJson<Shoes>(filePath);
            linearLayoutImages.Visibility = ViewStates.Visible;

            foreach (var item in list)
            {
                ImageView image = new ImageView(this);
                TextView text = new TextView(this);
                path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                filePath = System.IO.Path.Combine(path, "Wardrobe", item.guid.ToString() + ".jpg");
                image.SetImageBitmap(Services.GetPhoto(filePath));
                text.Text = $"{item.brand} {item.type} {item.color} {item.size}";
                text.TextSize = 30;

                linearLayoutImages.AddView(image);
                linearLayoutImages.AddView(text);

                image.Click += delegate (object sender, EventArgs e) { Image_Click(sender, e, Wear.Shoes); };
            }
        }

        private void ImgViewPants_Click(object _sender, EventArgs _e)
        {
            linearLayoutImages.RemoveAllViews();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath;
            filePath = System.IO.Path.Combine(path, "pants.json");
            var list = Services.ReadFromJson<Pants>(filePath);
            linearLayoutImages.Visibility = ViewStates.Visible;

            foreach (var item in list)
            {
                ImageView image = new ImageView(this);
                TextView text = new TextView(this);
                path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                filePath = System.IO.Path.Combine(path, "Wardrobe", item.guid.ToString() + ".jpg");
                image.SetImageBitmap(Services.GetPhoto(filePath));
                text.Text = $"{item.brand} {item.type} {item.color} {item.size}";
                text.TextSize = 30;

                linearLayoutImages.AddView(image);
                linearLayoutImages.AddView(text);

                image.Click += delegate (object sender, EventArgs e) { Image_Click(sender, e, Wear.Bottom); };
            }
        }

        private void ImgViewShirt_Click(object _sender, EventArgs _e)
        {
            linearLayoutImages.RemoveAllViews();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath;
            filePath = System.IO.Path.Combine(path, "shirts.json");
            var list = Services.ReadFromJson<Shirt>(filePath);
            linearLayoutImages.Visibility = ViewStates.Visible;

            foreach (var item in list)
            {
                ImageView image = new ImageView(this);
                TextView text = new TextView(this);
                path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                filePath = System.IO.Path.Combine(path, "Wardrobe", item.guid.ToString() + ".jpg");
                image.SetImageBitmap(Services.GetPhoto(filePath));
                text.Text = $"{item.brand} {item.type} {item.color} {item.size}";
                text.TextSize = 30;

                linearLayoutImages.AddView(image);
                linearLayoutImages.AddView(text);

                image.Click += delegate (object sender, EventArgs e) { Image_Click(sender, e, Wear.Top); };
            }
        }

        private void ImgViewHat_Click(object _sender, EventArgs _e)
        {
            linearLayoutImages.RemoveAllViews();
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath;
            filePath = System.IO.Path.Combine(path, "hats.json");
            var list = Services.ReadFromJson<Hat>(filePath);
            linearLayoutImages.Visibility = ViewStates.Visible;

            foreach (var item in list)
            {
                ImageView image = new ImageView(this);
                TextView text = new TextView(this);
                path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures).AbsolutePath;
                filePath = System.IO.Path.Combine(path, "Wardrobe", item.guid.ToString() + ".jpg");
                image.SetImageBitmap(Services.GetPhoto(filePath));
                text.Text = $"{item.brand} {item.type} {item.color} {item.size}";
                text.TextSize = 30;

                linearLayoutImages.AddView(image);
                linearLayoutImages.AddView(text);

                image.Click += delegate (object sender, EventArgs e) { Image_Click(sender, e, Wear.Hat); };
            }
        }

        private void Image_Click(object sender, EventArgs e, Wear wear)
        {
            linearLayoutImages.Visibility = ViewStates.Gone;

            switch (wear)
            {
                case Wear.Hat:
                    imgViewHat.SetImageDrawable(((ImageView)sender).Drawable);
                    break;
                case Wear.Top:
                    imgViewShirt.SetImageDrawable(((ImageView)sender).Drawable);
                    break;
                case Wear.Bottom:
                    imgViewPants.SetImageDrawable(((ImageView)sender).Drawable);
                    break;
                case Wear.Shoes:
                    imgViewShoes.SetImageDrawable(((ImageView)sender).Drawable);
                    break;
            }
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
