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

[assembly: ExportRenderer(typeof(SquareView), typeof(SquareViewRenderer))]
namespace PCLThinCanvas.Droid.Renderers
{
	class SquareViewRenderer : BoxRenderer
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
			var xfview = (SquareView)this.Element;

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

				// ìhÇËÇ¬Ç‘Çµ
				paint.SetStyle(Paint.Style.Fill);
				paint.Color = xfview.FillColor.ToAndroid();
				canvas.DrawRoundRect(rect, (float)(xfview.CornerRadiusSize * expand), (float)(xfview.CornerRadiusSize * expand), paint);

				// ê¸
				paint.SetStyle(Paint.Style.Stroke);
				paint.Color = xfview.LineColor.ToAndroid();
				canvas.DrawRoundRect(rect, (float)(xfview.CornerRadiusSize * expand), (float)(xfview.CornerRadiusSize * expand), paint);
			}
		}
	}
}