// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ContextSideMenuWithBind
{
	[Register ("ContextMenuCell")]
	partial class ContextMenuCell
	{
		[Outlet]
		UIKit.UIImageView menuImageView { get; set; }

		[Outlet]
		UIKit.UILabel menuTitleLable { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (menuImageView != null) {
				menuImageView.Dispose ();
				menuImageView = null;
			}

			if (menuTitleLable != null) {
				menuTitleLable.Dispose ();
				menuTitleLable = null;
			}
		}
	}
}
