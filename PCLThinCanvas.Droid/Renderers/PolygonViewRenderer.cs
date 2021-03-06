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

[assembly: ExportRenderer(typeof(PolygonView), typeof(PolygonViewRenderer))]
namespace PCLThinCanvas.Droid.Renderers
{
	class PolygonViewRenderer : BoxRenderer
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
			var xfview = (PolygonView)this.Element;

			var expand = this.Width / xfview.Width;

			// 線を描画
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

				// 接続部形状
				switch (xfview.LineJoin)
				{
					case LineJoin.Bevel:
					default:
						paint.StrokeJoin = Paint.Join.Bevel;
						break;
					case LineJoin.Miter:
						paint.StrokeJoin = Paint.Join.Miter;
						break;
					case LineJoin.Round:
						paint.StrokeJoin = Paint.Join.Round;
						break;
				}

				// 塗りつぶし
				paint.Color = xfview.FillColor.ToAndroid();
				paint.SetStyle(Paint.Style.Fill);
				if (xfview.FillImageSource == null)
				{
					this.DrawPolygon(canvas, paint, xfview, expand);
				}
				else
				{
					RendererUtil.DrawMaskedImage(canvas, (cv, pt) =>
					{
						this.DrawPolygon(cv, pt, xfview, expand);
					}, paint, xfview.FillImageSource, this, (float)expand);
				}

				// 枠線
				paint.Color = xfview.LineColor.ToAndroid();
				paint.SetStyle(Paint.Style.Stroke);
				this.DrawPolygon(canvas, paint, xfview, expand);
			}
		}

		private void DrawPolygon(Canvas canvas, Paint paint, PolygonView xfview, double expand)
		{
			// 多角形
			var path = new Path();
			if (xfview.Positions != null && xfview.Positions.Count >= 2)
			{
				// 相対座標指定
				if (xfview.IsRelativePositions)
				{
					var rect = new Rect(0, 0, this.Width, this.Height);

					bool isFirst = true;
					foreach (var pos in xfview.Positions)
					{
						if (isFirst)
						{
							path.MoveTo((float)(pos.Left * this.Width), (float)(pos.Top * this.Height));
							isFirst = false;
						}
						else
						{
							path.LineTo((float)(pos.Left * this.Width), (float)(pos.Top * this.Height));
						}
					}
				}

				// 絶対座標指定
				else
				{
					bool isFirst = true;
					foreach (var pos in xfview.Positions)
					{
						if (isFirst)
						{
							path.MoveTo((float)(pos.Left * expand), (float)(pos.Top * expand));
							isFirst = false;
						}
						else
						{
							path.LineTo((float)(pos.Left * expand), (float)(pos.Top * expand));
						}
					}
				}
			}
			if (xfview.IsClosed)
			{
				path.Close();
			}
			canvas.DrawPath(path, paint);
		}
	}
}