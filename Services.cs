using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Media;

namespace Androbe
{
    public class Services
    {
        public static void PopEnum<T>(PopupMenu pm) where T : System.Enum
        {
            int x = 1;
            pm.Menu.Clear();

            foreach (var _enum in Enum.GetValues(typeof(T)))
            {
                pm.Menu.Add(Menu.None, x, x, _enum.ToString());
                x++;
            }
        }

        public static bool SaveImage(Bitmap bitmap, string filename)
        {
            bool success = false;
            FileStream fs = null;
            try
            {
                using (fs = new FileStream(filename, FileMode.Create))
                {
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 8, fs);
                    success = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("SaveImage exception: " + e.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            return success;
        }

        public static async Task<Bitmap> TakePhoto(string photoName)
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
                Name = photoName,
                Directory = "Wardrobe",
                SaveToAlbum = true,
            });

            if (file == null)
            {
                return null;
            }

            byte[] imageArray = System.IO.File.ReadAllBytes(file.Path);
            return BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
        }

        public static async Task<Bitmap> PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CompressionQuality = 40,
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