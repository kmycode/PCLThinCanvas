using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using PCLThinCanvas.Core;
using UIKit;

namespace PCLThinCanvas.iOS.Renderers
{
	static class RendererUtil
	{
		public static void SetCap(CGContext context, LineCap cap)
		{
			switch (cap)
			{
				case LineCap.Butt:
					context.SetLineCap(CGLineCap.Butt);
					break;
				case LineCap.Round:
					context.SetLineCap(CGLineCap.Round);
					break;
				case LineCap.Square:
				default:
					context.SetLineCap(CGLineCap.Square);
					break;
			}
		}

		public static void SetStyle(CGContext context, LineStyle style, double lineWidth)
		{
			switch (style)
			{
				case LineStyle.Solid:
				default:
					context.SetLineDash(0, new nfloat[] { }, 0);
					break;
				case LineStyle.Dotted:
					context.SetLineDash(0, new nfloat[] { (nfloat)(lineWidth / 2), (nfloat)(lineWidth * 2) }, 2);
					break;
				case LineStyle.Dashed:
					context.SetLineDash(0, new nfloat[] { (nfloat)(lineWidth), (nfloat)(lineWidth * 4) }, 2);
					break;
			}
		}
	}
}