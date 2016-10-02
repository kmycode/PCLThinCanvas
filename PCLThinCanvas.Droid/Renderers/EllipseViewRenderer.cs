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
				paint.StrokeWidth = (float)(xfview.LineWidth * expand);
				RendererUtil.SetCap(paint, xfview.LineCap);
				this.SetLayerType(RendererUtil.SetStyle(paint, xfview.LineStyle, xfview.LineWidth, expand), paint);

				// “h‚è‚Â‚Ô‚µ
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = xfview.FillColor.ToAndroid();
				canvas.DrawOval(rect, paint);

				// ‰~
				paint.SetStyle(Paint.Style.Stroke);
				paint.Color = xfview.LineColor.ToAndroid();
				canvas.DrawOval(rect, paint);
			}
		}
	}
}