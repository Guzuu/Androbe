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
using Newtonsoft.Json;
using Plugin.Media;

namespace Androbe
{
    public class Services
    {

        /// <summary>
        /// Czysci i wypelnia popup menu wartosciami enum
        /// </summary>
        /// <param name="pm">Obiekt popup menu</param>
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


        /// <summary>
        /// Zapisuje zdjecie
        /// </summary>
        /// <param name="bitmap">obraz</param>
        /// <param name="filename">nazwa pliku</param>
        /// <returns>true lub false</returns>
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

        /// <summary>
        /// Uruchamia aparat i odsyla stworzone zdjecie
        /// </summary>
        /// <param name="photoName">nazwa dla zdjecia</param>
        /// <returns>wykonane zdjecie w formacie bitmapy</returns>
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

        /// <summary>
        /// Szuka zdjecia w galerii i zwraca
        /// </summary>
        /// <param name="filePath">sciezka zdjecia</param>
        /// <returns>zdjecie w formacie bitmapy</returns>
        public static Bitmap GetPhoto(string filePath)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(filePath) ?? null;
            return BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
        }

        /// <summary>
        /// Uruchamia galerie i odsyla wybrane zdjecie
        /// </summary>
        /// <returns>wybrane zdjecie w formacie bitmapy</returns>
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


        /// <summary>
        /// Zapisuje obiekt do istniejacego pliku json
        /// </summary>
        /// <param name="wear">typ ubrania</param>
        /// <param name="filePath">sciezka do pliku json</param>
        public static void SaveToJson<T>(T wear, string filePath)
        {
            string jsonData = "";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            else
            {
                jsonData = File.ReadAllText(filePath);
            }
            List<T> list = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            list.Add(wear);

            jsonData = JsonConvert.SerializeObject(list);
            File.WriteAllText(filePath, jsonData);
        }

        /// <summary>
        /// Zczytuje dane z pliku json i konwertuje na liste obiektow
        /// </summary>
        /// <param name="filePath">sciezka do pliku json</param>
        public static List<T> ReadFromJson<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            var jsonData = File.ReadAllText(filePath);
            List<T> list = JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();

            return list;
        }
    }
}