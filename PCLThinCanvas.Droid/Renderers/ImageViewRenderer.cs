using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PCLThinCanvas.Core;
using PCLThinCanvas.Droid.Renderers;
using PCLThinCanvas.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Graphics.Paint;

[assembly: ExportRenderer(typeof(PCLThinCanvas.Views.ImageView), typeof(ImageViewRenderer))]
namespace PCLThinCanvas.Droid.Renderers
{
	class ImageViewRenderer : BoxRenderer
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
				this.Invalidate();
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

		public override void Draw(Canvas canvas)
		{
			var xfview = (Views.ImageView)this.Element;

			var expand = this.Width / xfview.Width;

			using (var paint = new Paint())
			{
				this.SetLayerType(LayerType.Software, paint);

				// ‰æ‘œ•`‰æ
				RendererUtil.DrawMaskedImage(canvas, (cv, pt) =>
				{
					var bitmap = RendererUtil.GetBitmap(xfview.MaskImageSource, this);
					cv.DrawBitmap(bitmap,
						new Rect(0, 0, (int)(bitmap.Width * expand), (int)(bitmap.Height * expand)),
						new Rect(0, 0, (int)(this.Width * expand), (int)(this.Height * expand)),
						pt);
				}, paint, xfview.FillImageSource, this, (float)expand);
			}
		}
	}
}