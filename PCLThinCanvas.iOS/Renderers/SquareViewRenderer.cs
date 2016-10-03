using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using PCLThinCanvas.Core;
using PCLThinCanvas.iOS.Renderers;
using PCLThinCanvas.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SquareView), typeof(SquareViewRenderer))]
namespace PCLThinCanvas.iOS.Renderers
{
	public class SquareViewRenderer : BoxRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += (sender, ev) => this.SetNeedsDisplay();
			}
		}

		public override void Draw(CGRect rect)
		{
			var xfview = (SquareView)Element;

			nfloat startx = (nfloat)(xfview.LineWidth / 2);
			nfloat starty = (nfloat)(xfview.LineWidth / 2);
			nfloat endx = (nfloat)(xfview.Width - xfview.LineWidth / 2);
			nfloat endy = (nfloat)(xfview.Height - xfview.LineWidth / 2);

			using (var context = UIGraphics.GetCurrentContext())
			{
				context.SetStrokeColor(xfview.LineColor.ToCGColor());
				context.SetFillColor(xfview.FillColor.ToCGColor());
				context.SetLineWidth((nfloat)xfview.LineWidth);
				RendererUtil.SetCap(context, xfview.LineCap);
				RendererUtil.SetStyle(context, xfview.LineStyle, xfview.LineWidth);

				var path = CGPath.FromRoundedRect(new CGRect(startx, starty, endx - startx, endy - starty), (nfloat)xfview.CornerRadiusSize, (nfloat)xfview.CornerRadiusSize);
				context.AddPath(path);

				if (xfview.FillImageSource == null)
				{
					context.DrawPath(CGPathDrawingMode.FillStroke);
				}
				else
				{
					var fullRect = new CGRect(0, 0, xfview.Width, xfview.Height);
					RendererUtil.DrawMaskedImage(context, fullRect, (ct) =>
					{
						UIColor.White.SetFill();
						ct.FillRect(fullRect);
						ct.SetLineWidth(0);
						ct.SetFillColor(UIColor.Black.CGColor);
						ct.AddPath(path);
						ct.DrawPath(CGPathDrawingMode.FillStroke);
					}, xfview.FillImageSource, (float)xfview.Width, (float)xfview.Height);

					// ògê¸ïîï™
					context.DrawPath(CGPathDrawingMode.Stroke);
				}
			}
		}
	}
}