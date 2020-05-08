using System;
using UIKit;
using Foundation;

namespace GravitySlider.Sample
{
    public static class UIColorExtensions
    {

        public static UIColor FromHex (this UIColor color, int hex)
        => UIColor.FromRGB(
                (((float)((hex & 0xFF0000) >> 16)) / 255.0f),
                (((float)((hex & 0xFF00) >> 8)) / 255.0f),
                (((float)(hex & 0xFF)) / 255.0f)
            );       
    }
}
