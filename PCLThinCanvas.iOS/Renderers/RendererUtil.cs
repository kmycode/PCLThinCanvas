using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Foundation;
using PCLThinCanvas.Core;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

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

		private static Dictionary<ImageSource, UIImage> _imageSourceCache = new Dictionary<ImageSource, UIImage>();

		public static UIImage GetUIImage(ImageSource imageSource)
		{
			UIImage bitmap = null;
			Task.Run(async () =>
			{
				if (!_imageSourceCache.TryGetValue(imageSource, out bitmap))
				{
					IImageSourceHandler handler;

					if (imageSource is FileImageSource)
					{
						handler = new FileImageSourceHandler();
					}
					else if (imageSource is StreamImageSource)
					{
						handler = new StreamImagesourceHandler(); // sic
					}
					else if (imageSource is UriImageSource)
					{
						handler = new ImageLoaderSourceHandler(); // sic
					}
					else
					{
						throw new NotImplementedException();
					}

					bitmap = await handler.LoadImageAsync(imageSource);
					_imageSourceCache.Add(imageSource, bitmap);
				}
			}).Wait();
			return bitmap;
		}

		public static void DrawMaskedImage(CGContext context, CGRect rect, Action<CGContext> canvasDraw, ImageSource imageSource, float width, float height)
		{
			var uiimage = GetUIImage(imageSource);

			// ƒ}ƒXƒN‚ð•`‰æ
			UIGraphics.BeginImageContext(new CGSize(width, height));
			using (var ct = UIGraphics.GetCurrentContext())
				canvasDraw(ct);

			var maskImage = UIGraphics.GetImageFromCurrentImageContext();
			var maskcgImage = maskImage.CGImage;
			var mask = CGImage.CreateMask((int)maskcgImage.Width,
										(int)maskcgImage.Height,
										(int)maskcgImage.BitsPerComponent,
										(int)maskcgImage.BitsPerPixel,
										(int)maskcgImage.BytesPerRow,
										maskcgImage.DataProvider,
										null,
										false);

			var masked = uiimage.CGImage.WithMask(mask);

			context.ScaleCTM(1, -1);
			context.TranslateCTM(0, -height);
			context.DrawImage(rect, masked);
			UIGraphics.EndImageContext();
		}
	}
}
 