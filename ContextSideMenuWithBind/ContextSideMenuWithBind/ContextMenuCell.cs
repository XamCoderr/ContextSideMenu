using System;
using ContextSideMenuBinding;
using CoreGraphics;
using Foundation;
using UIKit;

namespace ContextSideMenuWithBind
{
    public partial class ContextMenuCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ContextMenuCell");
        public static readonly UINib Nib;

        public UIImageView imagview;
        public UILabel lblTitle;

       
        static ContextMenuCell()
        {
            Nib = UINib.FromName("ContextMenuCell", NSBundle.MainBundle);
        }

        protected ContextMenuCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.SelectionStyle = UITableViewCellSelectionStyle.None;
            this.Layer.MasksToBounds = true;
            this.Layer.ShadowOffset = new CGSize(0, 2);
            this.Layer.ShadowColor = UIColor.FromRGBA(181.0f, 181.0f, 181.0f, 1.0f).CGColor;
            this.Layer.ShadowRadius = 5;
            this.Layer.ShadowOpacity = 0.5f;

            lblTitle = this.menuTitleLable;
            imagview = this.menuImageView;
        }
        public override void SetSelected(bool selected, bool animated)
        {
            base.SetSelected(selected, animated);
        }

        //#region YALContextMenuCell
        public UIView animatedIcon()
        {
            return this.menuImageView;
        }
        public UIView animatedContent()
        {
            return this.menuTitleLable;
        }

    }
   
}
