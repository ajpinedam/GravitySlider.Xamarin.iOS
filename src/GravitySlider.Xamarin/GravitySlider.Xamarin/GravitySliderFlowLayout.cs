using System;
using CoreGraphics;
using Foundation;
using UIKit;
using CoreAnimation;

namespace GravitySlider.Xamarin
{
    public class GravitySliderFlowLayout : UICollectionViewFlowLayout
    {
        nfloat lineSpacingArgument = -2.5f;
        CGSize lastCollectionViewSize = CGSize.Empty;

        public nfloat Period => (CollectionView?.Bounds.Width ?? 0) * 0.86f;

        public GravitySliderFlowLayout(CGSize itemSize) : base()
        {
            ScrollDirection = UICollectionViewScrollDirection.Horizontal;
            ItemSize = itemSize;
            MinimumLineSpacing = itemSize.Width / lineSpacingArgument;
        }

        public GravitySliderFlowLayout(NSCoder coder) : base(coder) { }


        public void Setup(UICollectionViewScrollDirection scrollDirection, CGSize collectionViewSize)
        {
            if (collectionViewSize == lastCollectionViewSize) { return; }

            lastCollectionViewSize = collectionViewSize;
            ScrollDirection = scrollDirection;

            switch (ScrollDirection)
            {
                case UICollectionViewScrollDirection.Horizontal:
                    SectionInset = new UIEdgeInsets(top: 0.0f, left: collectionViewSize.Width / 2f - ItemSize.Width / 2f, bottom: 0.0f, right: collectionViewSize.Width / 2f - ItemSize.Width / 2f);
                    break;
                case UICollectionViewScrollDirection.Vertical:
                    break;
            }
        }

        public override void PrepareLayout()
        {
            base.PrepareLayout();

            if (CollectionView == null) { return; }

            Setup(ScrollDirection, CollectionView.Bounds.Size);
        }


        public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect(CGRect rect)
        {
            var targetRect = CGRect.Empty;
            switch (ScrollDirection)
            {
                case UICollectionViewScrollDirection.Horizontal:

                    targetRect.Location = new CGPoint(x: rect.Location.X - rect.Width / 2, y: rect.Location.Y);
                    targetRect.Size = new CGSize(width: rect.Width * 2, height: rect.Height);

                    var attributes = base.LayoutAttributesForElementsInRect(targetRect);

                    if (attributes == null || CollectionView == null) { return null; }

                    foreach (var attribute in attributes)
                    {
                        var cellX = CollectionView.ConvertPointToView(attribute.Center, null).X;
                        var difference = CollectionView.Center.X - cellX;
                        var zIndexValue = new nfloat(-Math.Pow(Math.Abs(difference) / CollectionView.Frame.Size.Width, 2.0) + 1.0);
                        var scaleFactor = new nfloat(-Math.Pow(difference / 1.8 / CollectionView.Frame.Size.Width * 2.0, 2.0) + 1.0);

                        var numPeriods = Math.Floor(cellX / Period);
                        var adjustment = (nfloat)(numPeriods * Period);
                        var relativeDistanceFromCenter = CollectionView.Center.X - (cellX - adjustment);
                        var centerProximityMagnitude = SinDistributor(x: cellX, period: Period, xOffset: CollectionView.Center.X);

                        var transform = CATransform3D.Identity;

                        var tx = new nfloat(relativeDistanceFromCenter - adjustment + centerProximityMagnitude * ItemSize.Width * 0.6);
                        transform = transform.Translate(tx, 0.0f, 0.0f);
                        transform = transform.Scale(scaleFactor, scaleFactor, 1.0f);

                        var distanceFromCenter = new nfloat(Math.Abs(CollectionView.Center.X - cellX));
                        attribute.Alpha = SqrtDistributor(x: distanceFromCenter, threshold: new nfloat(Period * 0.5), xOrigin: new nfloat(Period * 0.6));
                        attribute.ZIndex = (nint)(zIndexValue * 1000);
                        attribute.Transform3D = transform;
                    }
                    return attributes;
                case UICollectionViewScrollDirection.Vertical:
                    return null;
                default: return null;
            }
        }

        public override bool ShouldInvalidateLayoutForBoundsChange(CGRect newBounds) => true;

        public override CGPoint TargetContentOffset(CGPoint proposedContentOffset, CGPoint scrollingVelocity)
        {
            if (CollectionView == null) { return CGPoint.Empty; }

            var latestOffset = base.TargetContentOffset(proposedContentOffset, scrollingVelocity);

            switch (ScrollDirection)
            {
                case UICollectionViewScrollDirection.Horizontal:
                    var difference = CollectionView.Frame.Size.Width / 2f - SectionInset.Left - ItemSize.Width / 2f;
                    var inset = CollectionView.ContentInset.Left;
                    var row = new nfloat(Math.Round((latestOffset.X - inset + difference) / (ItemSize.Width + MinimumLineSpacing)));
                    var calculatedOffset = row * ItemSize.Width + row * MinimumLineSpacing + inset;
                    var targetOffset = new CGPoint(x: ((row == 0 && proposedContentOffset.X < (calculatedOffset - difference)) || (((nint)row ==
                CollectionView.NumberOfItemsInSection(0) - 1) && proposedContentOffset.X > (calculatedOffset - difference))) ?
                    proposedContentOffset.X : calculatedOffset - difference, y: latestOffset.Y);

                    return targetOffset;

                case UICollectionViewScrollDirection.Vertical:
                    return CGPoint.Empty;

                default:
                    return CGPoint.Empty;
            }
        }


        private nfloat SqrtDistributor(nfloat x, nfloat threshold, nfloat xOrigin)
        {
            var arg = (x - xOrigin) / (threshold - xOrigin);
            arg = arg <= 0 ? 0 : arg;
            var y = Math.Sqrt(arg);
            return new nfloat(y > 1 ? 1 : y);
        }

        private nfloat SinDistributor(nfloat x, nfloat period, nfloat xOffset)
        {
            var halfPeriod = period / 2;
            return (nfloat)Math.Sin(x / (halfPeriod / Math.PI) - xOffset / (halfPeriod / Math.PI));
        }
    }
}
