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

[assembly: ExportRenderer(typeof(EllipseView), typeof(EllipseViewRenderer))]
namespace PCLThinCanvas.Droid.Renderers
{
	class EllipseViewRenderer : BoxRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += (sender, ev) => this.Invalidate();
			}
		}

		public override void Draw(Canvas canvas)
		{
			var xfview = (EllipseView)this.Element;

			var expand = this.Width / xfview.Width;
			var rect = new RectF((float)(xfview.LineWidth * expand / 2),
								(float)(xfview.LineWidth * expand / 2),
								(float)(xfview.Width * expand - xfview.LineWidth * expand / 2),
								(float)(xfview.Height * expand - xfview.LineWidth * expand / 2));

			using (var paint = new Paint())
			{
				paint.AntiAlias = xfview.IsAntiAlias;

				paint.StrokeWidth = (float)(xfview.LineWidth * expand);
				RendererUtil.SetCap(paint, xfview.LineCap);

				var layerType = RendererUtil.SetStyle(paint, xfview.LineStyle, xfview.LineWidth, expand);
				if (xfview.FillImageSource != null)
				{
					layerType = LayerType.Software;
				}
				this.SetLayerType(layerType, paint);

				// “h‚è‚Â‚Ô‚µ
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = xfview.FillColor.ToAndroid();
				if (xfview.FillImageSource == null)
				{
					canvas.DrawOval(rect, paint);
				}
				else
				{
					RendererUtil.DrawMaskedImage(canvas, (cv, pt) =>
					{
						cv.DrawOval(rect, pt);
					}, paint, xfview.FillImageSource, this, (float)expand);
				}

				// ‰~
				paint.SetStyle(Paint.Style.Stroke);
				paint.Color = xfview.LineColor.ToAndroid();
				canvas.DrawOval(rect, paint);
			}
		}
	}
}