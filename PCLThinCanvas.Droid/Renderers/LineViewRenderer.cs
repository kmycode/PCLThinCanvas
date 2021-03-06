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

[assembly: ExportRenderer(typeof(LineView), typeof(LineViewRenderer))]
namespace PCLThinCanvas.Droid.Renderers
{
	class LineViewRenderer : BoxRenderer
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
			var xfview = (LineView)this.Element;

			var expand = this.Width / xfview.Width;
			var rect = new RectF(0,
								0,
								(float)(xfview.Width * expand),
								(float)(xfview.Height * expand));

			// 水平線
			if (xfview.Height <= xfview.LineWidth)
			{
				xfview.HeightRequest = xfview.LineWidth;
				rect = new RectF((float)(xfview.LineWidth * expand / 2),
								this.Height / 2,
								this.Width - (float)(xfview.LineWidth * expand / 2),
								this.Height / 2);
			}

			// 垂直線
			else if (xfview.Width <= xfview.LineWidth)
			{
				xfview.WidthRequest = xfview.LineWidth;
				rect = new RectF(this.Width / 2,
								(float)(xfview.LineWidth * expand / 2),
								this.Width / 2,
								this.Height - (float)(xfview.LineWidth * expand / 2));
			}

			// 線を描画
			using (var paint = new Paint())
			{
				paint.AntiAlias = xfview.IsAntiAlias;

				paint.Color = xfview.LineColor.ToAndroid();
				paint.StrokeWidth = (float)(xfview.LineWidth * expand);
				RendererUtil.SetCap(paint, xfview.LineCap);
				this.SetLayerType(RendererUtil.SetStyle(paint, xfview.LineStyle, xfview.LineWidth, expand), paint);

				paint.SetStyle(Paint.Style.Stroke);

				switch (xfview.LineDirection)
				{
					case LineDirection.LeftToRight:
					default:
						canvas.DrawLine(rect.Left, rect.Top, rect.Right, rect.Bottom, paint);
						break;
					case LineDirection.RightToLeft:
						canvas.DrawLine(rect.Right, rect.Top, rect.Left, rect.Bottom, paint);
						break;
				}
			}
		}
	}
}