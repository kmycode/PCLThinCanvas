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

[assembly: ExportRenderer(typeof(LineView), typeof(LineViewRenderer))]
namespace PCLThinCanvas.iOS.Renderers
{
	public class LineViewRenderer : BoxRenderer
	{
		private bool isDisposed = false;

		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += this.GoInvalidate;
			}
			if (e.OldElement != null)
			{
				e.OldElement.PropertyChanged -= this.GoInvalidate;
			}
		}

		private void GoInvalidate(object sender, EventArgs e)
		{
			if (!this.isDisposed)
				this.SetNeedsDisplay();
		}

		protected override void Dispose(bool disposing)
		{
			this.isDisposed = true;
			if (this.Element != null)
			{
				this.Element.PropertyChanged -= this.GoInvalidate;
			}
			base.Dispose(disposing);
		}

		public override void Draw(CGRect rect)
		{
			var xfview = (LineView)Element;

			nfloat startx = 0;
			nfloat starty = 0;
			nfloat endx = (nfloat)xfview.Width;
			nfloat endy = (nfloat)xfview.Height;

			// 水平線
			if (xfview.Height <= xfview.LineWidth)
			{
				xfview.HeightRequest = xfview.LineWidth;
				startx = (nfloat)(xfview.LineWidth / 2);
				starty = (nfloat)(xfview.Height / 2);
				endx   = (nfloat)(xfview.Width - xfview.LineWidth / 2);
				endy   = (nfloat)(xfview.Height / 2);
			}

			// 垂直線
			else if (xfview.Width <= xfview.LineWidth)
			{
				xfview.WidthRequest = xfview.LineWidth;
				startx = (nfloat)(xfview.Width / 2);
				starty = (nfloat)(xfview.LineWidth / 2);
				endx   = (nfloat)(xfview.Width / 2);
				endy   = (nfloat)(xfview.Height - xfview.LineWidth / 2);
			}

			using (var context = UIGraphics.GetCurrentContext())
			{
				context.SetStrokeColor(xfview.LineColor.ToCGColor());
				context.SetLineWidth((nfloat)xfview.LineWidth);
				RendererUtil.SetCap(context, xfview.LineCap);
				RendererUtil.SetStyle(context, xfview.LineStyle, xfview.LineWidth);

				switch (xfview.LineDirection)
				{
					case LineDirection.LeftToRight:
					default:
						context.MoveTo(startx, starty);
						context.AddLineToPoint(endx, endy);
						break;
					case LineDirection.RightToLeft:
						context.MoveTo(endx, starty);
						context.AddLineToPoint(startx, endy);
						break;
				}

				context.StrokePath();
			}
		}
	}
}