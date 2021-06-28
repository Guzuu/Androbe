using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
using Androbe.Clothes;

namespace Androbe
{
    [Activity(Label = "AddItemActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class AddItemActivity : AppCompatActivity
    {
        ImageView ImgView;
        TextView textWear, textType, textSize, textColor;
        EditText textBrand;
        PopupMenu popWear, popType, popSize, popColor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_item);

            ImgView = (ImageView)FindViewById(Resource.Id.imageView1);

            FloatingActionButton fabReturn = FindViewById<FloatingActionButton>(Resource.Id.fabReturn);
            fabReturn.Click += FabReturnOnClick;

            FloatingActionButton fabPhoto = FindViewById<FloatingActionButton>(Resource.Id.fabPhoto);
            fabPhoto.Click += FabTakePhotoOnClick;

            FloatingActionButton fabGallery = FindViewById<FloatingActionButton>(Resource.Id.fabGallery);
            fabGallery.Click += FabPickPhotoOnClick;

            //ListView Wear = FindViewById<ListView>(Resource.Id.listView1);
            //ExpandableListView Wear = FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
            //var wear = (Wear[])Enum.GetValues(typeof(Wear));
            //Wear.Adapter = new BaseExpandableListAdapter<Wear>(this, Android.Resource.Layout.SimpleListItem1, wear);
            //Wear.SetAdapter(new ExpandableAdapter(this, wear));

            textWear = FindViewById<TextView>(Resource.Id.textViewWear);
            textWear.Click += TextWearOnClick;

            textType = FindViewById<TextView>(Resource.Id.textViewType);

            textSize = FindViewById<TextView>(Resource.Id.textViewSize);
            textSize.Click += TextSizeOnClick;

            textColor = FindViewById<TextView>(Resource.Id.textViewColor);
            textColor.Click += TextColorOnClick;

            textBrand = FindViewById<EditText>(Resource.Id.editTextBrand);

            popWear = new PopupMenu(this, textWear);
            popType = new PopupMenu(this, textType);
            popSize = new PopupMenu(this, textSize);
            popColor = new PopupMenu(this, textColor);

            Helpers.PopEnum<Wear>(popWear);
            Helpers.PopEnum<Size>(popSize);
            Helpers.PopEnum<Clothes.Color>(popColor);

            popWear.MenuItemClick += popWearOnClick;
            popSize.MenuItemClick += popSizeOnClick;
            popColor.MenuItemClick += popColorOnClick;
        }

        private void popColorOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            textColor.Text = e.Item.TitleFormatted.ToString();
        }

        private void popSizeOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            textSize.Text = e.Item.TitleFormatted.ToString();
        }

        private void TextColorOnClick(object sender, EventArgs e)
        {
            popColor.Show();
        }

        private void TextSizeOnClick(object sender, EventArgs e)
        {
            popSize.Show();
        }

        private void popWearOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            string sWear = e.Item.TitleFormatted.ToString();
            textWear.Text = sWear;
            textType.Text = "Type";
            textType.Click += TextTypeOnClick;

            switch (sWear)
            {
                case "Hat":
                    PopHats();
                    break;

                case "Top":
                    PopShirts();
                    break;

                case "Bottom":
                    PopPants();
                    break;

                case "Shoes":
                    PopShoes();
                    break;
            }
        }

        private void PopTypeOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        { 
            textType.Text = e.Item.TitleFormatted.ToString();
        }

        private void TextTypeOnClick(object sender, EventArgs e)
        {
            popType.Show();
            popType.MenuItemClick += PopTypeOnClick;
        }

        private void TextWearOnClick(object sender, EventArgs e)
        {
            popWear.Show();
        }

        private void PopShoes()
        {
            Helpers.PopEnum<ShoesType>(popType);
        }

        private void PopPants()
        {
            Helpers.PopEnum<PantsType>(popType);
        }

        private void PopShirts()
        {
            Helpers.PopEnum<ShirtType>(popType);
        }

        private void PopHats()
        {
            Helpers.PopEnum<HatType>(popType);
        }

        private void FabReturnOnClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        private async void FabPickPhotoOnClick(object sender, EventArgs e)
        {
            Bitmap btmp = await PickPhoto();

            ImgView = (ImageView)FindViewById(Resource.Id.imageView1);
            ImgView.SetImageBitmap(btmp);
        }

        private async void FabTakePhotoOnClick(object sender, EventArgs e)
        {
            Bitmap btmp = await TakePhoto();

            ImgView = (ImageView)FindViewById(Resource.Id.imageView1);
            ImgView.SetImageBitmap(btmp);
        }

        async Task<Bitmap> TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                Name = "item.jpg",
                Directory = "sample"
            });

            if (file == null)
            {
                return null;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            return BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
        }

        async Task<Bitmap> PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40
            });

            if (file == null)
            {
                return null;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            return BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
        }
    }
}