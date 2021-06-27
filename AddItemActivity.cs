﻿using System;
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
        ViewStates FabState;
        TextView textWear;
        PopupMenu popWear;

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

            FloatingActionButton fabPlus = FindViewById<FloatingActionButton>(Resource.Id.fabPlus);
            fabPlus.Click += FabPlusOnClick;

            //ListView Wear = FindViewById<ListView>(Resource.Id.listView1);
            //ExpandableListView Wear = FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
            //var wear = (Wear[])Enum.GetValues(typeof(Wear));
            //Wear.Adapter = new BaseExpandableListAdapter<Wear>(this, Android.Resource.Layout.SimpleListItem1, wear);
            //Wear.SetAdapter(new ExpandableAdapter(this, wear));

            textWear = FindViewById<TextView>(Resource.Id.textView1);
            textWear.Click += TextWearOnClick;

            popWear = new PopupMenu(this, textWear);
            int x = 1;

            foreach (var wear in (Wear[])Enum.GetValues(typeof(Wear)))
            {
                popWear.Menu.Add(Menu.None, x, x, wear.ToString());
                x++;
            }

            popWear.MenuItemClick += popWearOnClick;
        }

        private void popWearOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            textWear.Text = e.Item.TitleFormatted.ToString();
        }

        private void TextWearOnClick(object sender, EventArgs e)
        {
            popWear.Show();
        }



        private void FabPlusOnClick(object sender, EventArgs eventArgs)
        {
            FindViewById(Resource.Id.fabFolder).Visibility = FabState;
            FindViewById(Resource.Id.fabGallery).Visibility = FabState;
            FindViewById(Resource.Id.fabPhoto).Visibility = FabState;

            if (FabState == ViewStates.Invisible) FabState = ViewStates.Visible;
            else FabState = ViewStates.Invisible;
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