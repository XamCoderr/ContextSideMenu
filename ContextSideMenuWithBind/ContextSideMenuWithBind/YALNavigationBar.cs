using System;
using CoreGraphics;
using UIKit;

namespace ContextSideMenuWithBind
{
    public partial class YALNavigationBar : UINavigationBar
    {
		static float defaultHeight = 65.0f;

		public YALNavigationBar()
		{

		}
		public override CGSize SizeThatFits(CGSize size)
		{
			CGSize amendedSize = base.SizeThatFits(size);
			amendedSize.Height = defaultHeight;
			return amendedSize;
		}
    }
}

