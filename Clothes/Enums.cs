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
    public enum Wear
    {
        Hat,
        Top,
        Bottom,
        Shoes,
    }

    public enum Color
    {
        Multicolor,
        Black,
        White,
        Yellow,
        Green,
        Red,
        Blue,
        Brown,
        Pink,
        Grey,
        Orange,
        Purple,
        Burgundy,
    }

    public enum Size
    {
        XS,
        S,
        M,
        L,
        XL,
        XXL,
        EU39,
        EU40,
        EU41,
        EU42,
        EU42_5,
        EU43,
    }

    public enum PantsType
    {
        Straight,
        Skinny,
        BootCut,
        Flare,
        WideLeg,
        Pegged,
        Stirrup,
        FivePocketJeans,
        BushPants,
        CargoPants,
        SailorPants,
        Jodhpurs,
        SweatPants,
        Harem,
        Palazzo,
        Carpenter,
        Jumpsuit,
        HotPants,
        Skort,
    }

    public enum HatType
    {
        TopHat,
        ClocheHat,
        BaseballCap,
        BowlerHat,
        TropicalHat,
        Beret,
        SunHat,
        FloppyHat,
        PanamaHat,
        FlatCap,
        NewsboyCap,
        DrivingCap,
    }

    public enum ShoesType
    {
        ConeHeel,
        KittenHeel,
        Stiletto,
        MaryJanePlatform,
        Slingback,
        PeepToe,
        Wedge,
        MaryJane,
        BalletFlat,
        Moccasin,
        Mule,
        Sandal,
        Slipper,
        FlipFlop,
        Ugg,
        AnkleBoot,
        LitaBoot,
        CowboyBoot,
        Oxford,
        BusinessShoe,
        Sneaker,
        HikingBoot,
        RunningShoe,
        SlipOn,
    }

    public enum ShirtType
    {
        SleevelessShirt,
        TankTop,
        TShirt,
        VNeckShirt,
        PoloShirt,
        Jersey,
        LongSleeveJersey,
        SweatShirt,
        Turtleneck,
        Hoodie,
        Crewneck,
        HawaiianShirt,
        DressShirt,
        Tuxedo,
        FlannelShirt,
        Jacket,
    }

    public partial class Helpers
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
    }
}