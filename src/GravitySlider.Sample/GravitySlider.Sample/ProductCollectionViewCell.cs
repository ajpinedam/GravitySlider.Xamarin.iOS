using System;

using Foundation;
using UIKit;

namespace GravitySlider.Sample
{
    public partial class ProductCollectionViewCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString("ProductCollectionViewCell");
        public static readonly UINib Nib;

        static ProductCollectionViewCell()
        {
        }

        protected ProductCollectionViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public UIImageView ProductImage => productImage;
        public UILabel NewLabel => newLabel;
    }
}
