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
using Androbe;

namespace Androbe
{
    [Activity(Label = "AddItemActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class AddItemActivity : AppCompatActivity
    {
        ImageView ImgView;
        TextView textWear, textType, textSize, textColor;
        EditText textBrand;
        PopupMenu popWear, popType, popSize, popColor;
        Guid guid;
        Bitmap btmp;

        /// <summary>
        /// Konfiguruje widoki 
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_item);

            guid = Guid.NewGuid();

            ImgView = (ImageView)FindViewById(Resource.Id.imageView1);

            FloatingActionButton fabReturn = FindViewById<FloatingActionButton>(Resource.Id.fabReturn);
            fabReturn.Click += FabReturnOnClick;

            FloatingActionButton fabPhoto = FindViewById<FloatingActionButton>(Resource.Id.fabPhoto);
            fabPhoto.Click += FabTakePhotoOnClick;

            FloatingActionButton fabSave = FindViewById<FloatingActionButton>(Resource.Id.fabSave);
            fabSave.Click += FabSaveOnClick;

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

            Services.PopEnum<Wear>(popWear);
            Services.PopEnum<Size>(popSize);
            Services.PopEnum<Clothes.Color>(popColor);

            popWear.MenuItemClick += popWearOnClick;
            popSize.MenuItemClick += popSizeOnClick;
            popColor.MenuItemClick += popColorOnClick;
        }

        /// <summary>
        /// Zapisuje obiekt oraz zdjecie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FabSaveOnClick(object sender, EventArgs e)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filePath;

            switch (textWear.Text)
            {
                case "Hat":
                    Hat hat = new Hat()
                    {
                        type = (HatType)Enum.Parse(typeof(HatType), textType.Text),
                        size = (Size)Enum.Parse(typeof(Size), textSize.Text),
                        color = (Clothes.Color)Enum.Parse(typeof(Clothes.Color), textColor.Text),
                        brand = textBrand.Text,
                        guid = guid,
                    };
                    filePath = System.IO.Path.Combine(path, "hats.json");
                    Services.SaveToJson(hat, filePath);
                    break;

                case "Top":
                    Shirt shirt = new Shirt()
                    {
                        type = (ShirtType)Enum.Parse(typeof(ShirtType), textType.Text),
                        size = (Size)Enum.Parse(typeof(Size), textSize.Text),
                        color = (Clothes.Color)Enum.Parse(typeof(Clothes.Color), textColor.Text),
                        brand = textBrand.Text,
                        guid = guid,
                    };
                    filePath = System.IO.Path.Combine(path, "shirts.json");
                    Services.SaveToJson(shirt, filePath);
                    break;

                case "Bottom":
                    Pants pants = new Pants()
                    {
                        type = (PantsType)Enum.Parse(typeof(PantsType), textType.Text),
                        size = (Size)Enum.Parse(typeof(Size), textSize.Text),
                        color = (Clothes.Color)Enum.Parse(typeof(Clothes.Color), textColor.Text),
                        brand = textBrand.Text,
                        guid = guid,
                    };
                    filePath = System.IO.Path.Combine(path, "pants.json");
                    Services.SaveToJson(pants, filePath);
                    break;

                case "Shoes":
                    Shoes shoes = new Shoes()
                    {
                        type = (ShoesType)Enum.Parse(typeof(ShoesType), textType.Text),
                        size = (Size)Enum.Parse(typeof(Size), textSize.Text),
                        color = (Clothes.Color)Enum.Parse(typeof(Clothes.Color), textColor.Text),
                        brand = textBrand.Text,
                        guid = guid,
                    };
                    filePath = System.IO.Path.Combine(path, "shoes.json");
                    Services.SaveToJson(shoes, filePath);
                    break;
            }
            FabReturnOnClick(sender, e);
        }

        /// <summary>
        /// Ustawia tekst koloru na podstawie wybranego z popup menu obiektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popColorOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            textColor.Text = e.Item.TitleFormatted.ToString();
        }

        /// <summary>
        /// Ustawia tekst rozmiaru na podstawie wybranego z popup menu obiektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popSizeOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            textSize.Text = e.Item.TitleFormatted.ToString();
        }

        /// <summary>
        /// Pokazuje popup menu kolorow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextColorOnClick(object sender, EventArgs e)
        {
            popColor.Show();
        }

        /// <summary>
        /// Pokazuje popup menu rozmiarow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextSizeOnClick(object sender, EventArgs e)
        {
            popSize.Show();
        }

        /// <summary>
        /// Ustawia odpowiedni typ ubioru na podstawie wybranego ubioru
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Ustawia tekst typu na podstawie wybranego z popup menu obiektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopTypeOnClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        { 
            textType.Text = e.Item.TitleFormatted.ToString();
        }

        /// <summary>
        /// Pokazuje popup menu typow oraz tworzy onclick event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextTypeOnClick(object sender, EventArgs e)
        {
            popType.Show();
            popType.MenuItemClick += PopTypeOnClick;
        }

        /// <summary>
        /// Pokazuje popup menu typow ubran
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextWearOnClick(object sender, EventArgs e)
        {
            popWear.Show();
        }

        /// <summary>
        /// Wypelnia popup menu butow wartosciami z enum butow
        /// </summary>
        private void PopShoes()
        {
            Services.PopEnum<ShoesType>(popType);
        }

        /// <summary>
        /// Wypelnia popup menu spodni wartosciami z enum spodni
        /// </summary>
        private void PopPants()
        {
            Services.PopEnum<PantsType>(popType);
        }

        /// <summary>
        /// Wypelnia popup menu koszulek wartosciami z enum koszulek
        /// </summary>
        private void PopShirts()
        {
            Services.PopEnum<ShirtType>(popType);
        }

        /// <summary>
        /// Wypelnia popup menu czapek wartosciami z enum czapek
        /// </summary>
        private void PopHats()
        {
            Services.PopEnum<HatType>(popType);
        }

        /// <summary>
        /// Wraca do glownego panelu
        /// </summary>
        private void FabReturnOnClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }

        /// <summary>
        /// Uruchamia akcje zdjecia oraz ustawia zdjecie na widoku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FabTakePhotoOnClick(object sender, EventArgs e)
        {
            btmp = await Services.TakePhoto(guid.ToString()+".jpg");

            ImgView = (ImageView)FindViewById(Resource.Id.imageView1);
            ImgView.SetImageBitmap(btmp);
        }
    }
}