using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Androbe.Clothes
{
    public class Pants : IClothes
    {
        public Color color { get; set; }
        public string brand { get; set; }
        public Size size { get; set; }
        public Guid guid { get; set; }
        public PantsType type { get; set; }
    }
}