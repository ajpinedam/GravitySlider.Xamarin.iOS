using CoreAnimation;
using CoreGraphics;
using Foundation;
using GravitySlider.Xamarin;
using System;
using UIKit;


namespace GravitySlider.Sample
{
    public partial class ViewController : UIViewController, IUICollectionViewDataSource, IUICollectionViewDelegate
    {

        UIImage[] images = { UIImage.FromBundle("beats_headphone"), UIImage.FromBundle("beats_headphones_white"), UIImage.FromBundle("beats_image"), };
        string[] titles = { "Beats Solo", "Beats X - White", "Beats Studio" };
        string[] subtitles = { "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "For most people, buying a new computer\ndoes not have to be as stressful as\n        buying a new car.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." };
        string[] prices = { "$30.00", "$19.00", "$60.00" };

        readonly nfloat collectionViewCellHeightCoefficient = 0.85f;
        readonly nfloat collectionViewCellWidthCoefficient = 0.55f;
        readonly nfloat priceButtonCornerRadius = 10f;
        readonly CGColor gradientFirstColor = UIColor.Clear.FromHex(0xFF8181).CGColor;
        readonly CGColor gradientSecondColor = UIColor.Clear.FromHex(0xA81382).CGColor;
        readonly CGColor cellsShadowColor = UIColor.Clear.FromHex(0x2a002a).CGColor;

        readonly NSString productCellIdentifier = new NSString("ProductCollectionViewCell");

        private int itemsNumber = 1000;

        public ViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            configureCollectionView();
            configurePriceButton();
        }

        [Export("scrollViewDidScroll:")]
        public void Scrolled(UIScrollView scrollView)
        {
            var locationFirst = new CGPoint(x: collectionView.Center.X + scrollView.ContentOffset.X, y: collectionView.Center.Y + scrollView.ContentOffset.Y);
            var locationSecond = new CGPoint(x: collectionView.Center.X + scrollView.ContentOffset.X + 20, y: collectionView.Center.Y + scrollView.ContentOffset.Y);
            var locationThird = new CGPoint(x: collectionView.Center.X + scrollView.ContentOffset.X - 20, y: collectionView.Center.Y + scrollView.ContentOffset.Y);

            var indexPathFirst = collectionView.IndexPathForItemAtPoint(locationFirst);
            var indexPathSecond = collectionView.IndexPathForItemAtPoint(locationSecond);
            var indexPathThird = collectionView.IndexPathForItemAtPoint(locationThird);


            if (indexPathFirst.Row == indexPathSecond.Row && indexPathSecond.Row == indexPathThird.Row && indexPathFirst.Row != pageControl.CurrentPage)
            {
                pageControl.CurrentPage = indexPathFirst.Row % images.Length;
                animateChangingTitle(indexPathFirst);
            }
        }


        private void configureCollectionView()
        {
            var gravitySliderLayout = new GravitySliderFlowLayout(new CGSize(width: collectionView.Frame.Size.Height * collectionViewCellWidthCoefficient, height: collectionView.Frame.Size.Height * collectionViewCellHeightCoefficient));

            collectionView.CollectionViewLayout = gravitySliderLayout;
            collectionView.DataSource = this;

            collectionView.Delegate = this;
        }

        private void configurePriceButton()
        {
            priceButton.Layer.CornerRadius = priceButtonCornerRadius;
        }

        private void configureProductCell(ProductCollectionViewCell cell, NSIndexPath indexPath)
        {
            cell.ClipsToBounds = false;
            var gradientLayer = new CAGradientLayer();
            gradientLayer.Frame = cell.Bounds;
            gradientLayer.Colors = new[] { gradientFirstColor, gradientSecondColor };
            gradientLayer.CornerRadius = 21;
            gradientLayer.MasksToBounds = true;
            cell.Layer.InsertSublayer(gradientLayer, 0);

            cell.Layer.ShadowColor = cellsShadowColor;
            cell.Layer.ShadowOpacity = 0.2f;
            cell.Layer.ShadowRadius = 20;
            cell.Layer.ShadowOffset = new CGSize(width: 0.0, height: 30);

            cell.ProductImage.Image = images[indexPath.Row % images.Length];

            cell.NewLabel.Layer.CornerRadius = 8;
            cell.NewLabel.ClipsToBounds = true;
            cell.NewLabel.Layer.BorderColor = UIColor.White.CGColor;
            cell.NewLabel.Layer.BorderWidth = 1.0f;
        }

        private void animateChangingTitle(NSIndexPath indexPath)
        {
            UIView.Transition(productTitleLabel, 0.3, UIViewAnimationOptions.TransitionCrossDissolve, () =>
            {
                productTitleLabel.Text = titles[indexPath.Row % titles.Length];
            }, completion: null);


            UIView.Transition(productSubtitleLabel, 0.3, UIViewAnimationOptions.TransitionCrossDissolve, () =>
            {
                productSubtitleLabel.Text = subtitles[indexPath.Row % subtitles.Length];
            }, null);


            UIView.Transition(withView: priceButton, duration: 0.3, options: UIViewAnimationOptions.TransitionCrossDissolve, animation: () =>
            {
                priceButton.SetTitle(prices[indexPath.Row % prices.Length], UIControlState.Normal);
            }, completion: null);
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section) => itemsNumber;

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = collectionView.DequeueReusableCell(productCellIdentifier, indexPath) as ProductCollectionViewCell;
            configureProductCell(cell, indexPath);
            return cell;
        }
    }
}