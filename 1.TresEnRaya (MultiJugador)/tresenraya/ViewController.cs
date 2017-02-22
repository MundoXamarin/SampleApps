using System;

using UIKit;

namespace tresenraya
{
	public partial class ViewController : UIViewController
	{
		UICollectionViewFlowLayout flowLayout;

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			var width = collectionView.Bounds.Size.Width;
			var height = collectionView.Bounds.Size.Height;

			flowLayout = new UICollectionViewFlowLayout()
			{
				SectionInset = new UIEdgeInsets(0, 0, 0, 0),
				ScrollDirection = UICollectionViewScrollDirection.Vertical,
				ItemSize = new CoreGraphics.CGSize((width - 6) / 3, (height - 6) / 3),
				MinimumInteritemSpacing = 3,
				MinimumLineSpacing = 3
			};

			collectionView.CollectionViewLayout = flowLayout;
			collectionView.BackgroundColor = UIColor.Black;
			collectionView.ContentInset = new UIEdgeInsets(0, 0, 0, 0);
			collectionView.RegisterClassForCell(typeof(CustomCollectionViewCell), CustomCollectionViewCell.CellID);
			collectionView.Source = new CustomCollectionSource();
			collectionView.ReloadData();

		}

		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			btnNuevo.TouchUpInside += btnNuevo_Click;
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void btnNuevo_Click(object sender, EventArgs e)
		{
			collectionView.Source = new CustomCollectionSource();
			collectionView.ReloadData();
		}


		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}
