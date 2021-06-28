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
    public interface IClothes
    {
        Color color { get; set; }
        string brand { get; set; }
        Size size { get; set; }
        Guid guid { get; set; }
    }
}