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

[assembly: ExportRenderer(typeof(ImageView), typeof(ImageViewRenderer))]
namespace PCLThinCanvas.iOS.Renderers
{
	public class ImageViewRenderer : BoxRenderer
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
			var xfview = (ImageView)Element;

			nfloat startx = (nfloat)(0);
			nfloat starty = (nfloat)(0);
			nfloat endx = (nfloat)(xfview.Width);
			nfloat endy = (nfloat)(xfview.Height);

			using (var context = UIGraphics.GetCurrentContext())
			{
				var path = CGPath.FromRect(new CGRect(startx, starty, endx - startx, endy - starty));
				context.AddPath(path);

				if (xfview.FillImageSource == null)
				{
				}
				else
				{
					var fullRect = new CGRect(0, 0, xfview.Width, xfview.Height);
					RendererUtil.DrawMaskedImage(context, fullRect, (ct) =>
					{
						UIColor.White.SetFill();
						ct.FillRect(fullRect);
						ct.AddPath(path);

						var uiimage = RendererUtil.GetUIImage(xfview.MaskImageSource);
						ct.ScaleCTM(1, -1);
						ct.TranslateCTM(0, (float)-xfview.Height);
						ct.DrawImage(rect, uiimage.CGImage);
					}, xfview.FillImageSource, (float)xfview.Width, (float)xfview.Height);
				}
			}
		}
	}
}