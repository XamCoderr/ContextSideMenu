using System;

using UIKit;
using ContextSideMenuBinding;
using System.Collections.Generic;
using Foundation;

namespace ContextSideMenuWithBind
{
    public partial class ViewController : UIViewController
    {
		List<string> menuTitles;
		List<UIImage> menuIcons;
        YALContextMenuTableView contextMenuTableView;

        static string menuCellIdentifier = @"rotationCell";

		protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			// Perform any additional setup after loading the view, typically from a nib.
			menuTitles = new List<string>(){
				"",
				"Send message",
				"Like profile",
				"Add to friends",
				"Add to favourites",
				"Block user"
			};

			menuIcons = new List<UIImage>(){
				UIImage.FromFile("icn_close.png"),
				UIImage.FromFile("icn_1.png"),
				UIImage.FromFile("icn_2.png"),
				UIImage.FromFile("icn_3.png"),
				UIImage.FromFile("icn_4.png"),
				UIImage.FromFile("icn_5.png"),
			};

			// set custom navigationBar with a bigger height
			this.NavigationController.SetValueForKeyPath(new YALNavigationBar(), new NSString("navigationBar"));
            //this.NavigationController.NavigationBar.Translucent = false;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void rightClick(NSObject sender)
        {
			if (this.contextMenuTableView == null)
			{
				contextMenuTableView = new YALContextMenuTableView();
				//YALContextTableDelegate delegateInstace = new YALContextTableDelegate(this.contextMenuTableView);
				//YALContextTableDataSource dataInstace = new YALContextTableDataSource(this.contextMenuTableView, menuCellIdentifier, menuTitles, menuIcons);

				this.contextMenuTableView.Source = new YALContextTableDataSource(this.contextMenuTableView, menuCellIdentifier, menuTitles, menuIcons);

				this.contextMenuTableView.AnimationDuration = 0.15f;
				//optional - implement custom YALContextMenuTableView custom protocol
				//  this.contextMenuTableView.YalDelegate = this;
				//optional - implement menu items layout
                this.contextMenuTableView.MenuItemsSide = MenuItemsSide.Right;
                this.contextMenuTableView.MenuItemsAppearanceDirection = MenuItemsAppearanceDirection.TopToBottom;

				//register nib
				//  this.contextMenuTableView.RegisterNibForCellReuse(UINib.FromName("ContextMenuCell", NSBundle.MainBundle), menuCellIdentifier);
				this.contextMenuTableView.RegisterClassForCellReuse(typeof(ContextSideMenuCell), menuCellIdentifier);
			}
			this.contextMenuTableView.ShowInView(this.NavigationController.View, UIEdgeInsets.Zero, true);
        }


		public override bool PrefersStatusBarHidden()
		{
			return true;
		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);
			this.contextMenuTableView.ReloadData();
		}

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);

            this.contextMenuTableView.UpdateAlongsideRotation();
		}

		public override void ViewWillTransitionToSize(CoreGraphics.CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
		{
			base.ViewWillTransitionToSize(toSize, coordinator);


			coordinator.AnimateAlongsideTransition(null, (context) =>
			{
				//should be called after rotation animation completed
				this.contextMenuTableView.ReloadData();
			});
            this.contextMenuTableView.UpdateAlongsideRotation();
		}


    }


	#region UITableView Delegate and DataSource
	//UITableView DataDource and Delegate
	public class YALContextTableDelegate : UITableViewDelegate
	{
		YALContextMenuTableView _tableView;

		public YALContextTableDelegate(YALContextMenuTableView tableView)
		{
			_tableView = tableView;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            _tableView.DismisWithIndexPath(indexPath);
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 65.0f;
		}
	}

    public class YALContextTableDataSource : UITableViewSource
	{
		YALContextMenuTableView _tableView;
        ContextSideMenuCell contextCell;

		string identifier;
		List<string> _menuTitle;
		List<UIImage> _menuIcon;

		public YALContextTableDataSource(YALContextMenuTableView tableView, string _identifier, List<string> menuTitle, List<UIImage> menuIcon)
		{
			_tableView = tableView;
			identifier = _identifier;
			_menuTitle = menuTitle;
			_menuIcon = menuIcon;
		}

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
            contextCell = _tableView.DequeueReusableCell(identifier,indexPath) as ContextSideMenuCell;

			if (contextCell != null)
			{
                contextCell.BackgroundColor = UIColor.Clear;
             
                contextCell.MenuImageView.Image = _menuIcon[indexPath.Row];
                contextCell.MenuTitleLabel.Text = _menuTitle[indexPath.Row];
			}
			return contextCell;
		}

		
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _menuTitle.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
			_tableView.DismisWithIndexPath(indexPath);
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 65.0f;
        }
	}
	#endregion
}
