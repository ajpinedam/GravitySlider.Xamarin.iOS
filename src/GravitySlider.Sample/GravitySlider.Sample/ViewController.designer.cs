// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace GravitySlider.Sample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UICollectionView collectionView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIPageControl pageControl { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton priceButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel productSubtitleLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel productTitleLabel { get; set; }

        [Action ("didPressPriceButton:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void didPressPriceButton (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (collectionView != null) {
                collectionView.Dispose ();
                collectionView = null;
            }

            if (pageControl != null) {
                pageControl.Dispose ();
                pageControl = null;
            }

            if (priceButton != null) {
                priceButton.Dispose ();
                priceButton = null;
            }

            if (productSubtitleLabel != null) {
                productSubtitleLabel.Dispose ();
                productSubtitleLabel = null;
            }

            if (productTitleLabel != null) {
                productTitleLabel.Dispose ();
                productTitleLabel = null;
            }
        }
    }
}