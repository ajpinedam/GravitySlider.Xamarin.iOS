// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace GravitySlider.Sample
{
    [Register ("ProductCollectionViewCell")]
    partial class ProductCollectionViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel newLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView productImage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (newLabel != null) {
                newLabel.Dispose ();
                newLabel = null;
            }

            if (productImage != null) {
                productImage.Dispose ();
                productImage = null;
            }
        }
    }
}