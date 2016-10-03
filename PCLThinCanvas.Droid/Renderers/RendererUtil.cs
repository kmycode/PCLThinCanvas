using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PCLThinCanvas.Core;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Graphics.Paint;

namespace PCLThinCanvas.Droid.Renderers
{
	static class RendererUtil
	{
		public static void SetCap(Paint paint, LineCap cap)
		{
			switch (cap)
			{
				case LineCap.Butt:
					paint.StrokeCap = Cap.Butt;
					break;
				case LineCap.Round:
					paint.StrokeCap = Cap.Round;
					break;
				case LineCap.Square:
				default:
					paint.StrokeCap = Cap.Square;
					break;
			}
		}

		public static LayerType SetStyle(Paint paint, LineStyle style, double lineWidth, double expand)
		{
			LayerType type = LayerType.Hardware;

			switch (style)
			{
				case LineStyle.Solid:
				default:
					paint.SetPathEffect(new PathEffect());
					break;
				case LineStyle.Dotted:
					type = LayerType.Software;
					paint.SetPathEffect(new DashPathEffect(new float[] { (float)(lineWidth * expand / 2), (float)(lineWidth * expand * 2) }, 0));
					break;
				case LineStyle.Dashed:
					type = LayerType.Software;
					paint.SetPathEffect(new DashPathEffect(new float[] { (float)(lineWidth * expand), (float)(lineWidth * expand * 4) }, 0));
					break;
			}

			return type;
		}

		private static Dictionary<ImageSource, Bitmap> _imageSourceCache = new Dictionary<ImageSource, Bitmap>();

		public static Bitmap GetBitmap(ImageSource imageSource, VisualElementRenderer<BoxView> context)
		{
			Bitmap bitmap = null;
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

					bitmap = await handler.LoadImageAsync(imageSource, context.Context);
					_imageSourceCache.Add(imageSource, bitmap);
				}
			}).Wait();
			return bitmap;
		}

		public static void DrawMaskedImage(Canvas canvas, Action<Canvas, Paint> canvasDraw, Paint paint, ImageSource imageSource, VisualElementRenderer<BoxView> context, float expand)
		{
			var maskPaint = new Paint(paint);
			//maskPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.DstIn));
			var imagePaint = new Paint();
			imagePaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.DstOut));

			var bitmap = GetBitmap(imageSource, context);

			canvasDraw(canvas, paint);
			paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.SrcIn));
			canvas.DrawBitmap(bitmap,
				new Rect(0, 0, (int)(bitmap.Width * expand), (int)(bitmap.Height * expand)),
				new Rect(0, 0, (int)(context.Width * expand), (int)(context.Height * expand)),
				paint);
			paint.SetXfermode(null);
			canvas.Restore();
		}
	}
}