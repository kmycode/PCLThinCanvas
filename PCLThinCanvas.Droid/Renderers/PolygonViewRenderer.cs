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
			var xfview = (PolygonView)this.Element;

			var expand = this.Width / xfview.Width;

			// ê¸Çï`âÊ
			using (var paint = new Paint())
			{
				paint.AntiAlias = xfview.IsAntiAlias;

				paint.StrokeWidth = (float)(xfview.LineWidth * expand);
				RendererUtil.SetCap(paint, xfview.LineCap);
				this.SetLayerType(RendererUtil.SetStyle(paint, xfview.LineStyle, xfview.LineWidth, expand), paint);

				// ê⁄ë±ïîå`èÛ
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

				// ìhÇËÇ¬Ç‘Çµ
				paint.Color = xfview.FillColor.ToAndroid();
				paint.SetStyle(Paint.Style.Fill);
				this.DrawPolygon(canvas, paint, xfview, expand);

				// ògê¸
				paint.Color = xfview.LineColor.ToAndroid();
				paint.SetStyle(Paint.Style.Stroke);
				this.DrawPolygon(canvas, paint, xfview, expand);
			}
		}

		private void DrawPolygon(Canvas canvas, Paint paint, PolygonView xfview, double expand)
		{
			// ëΩäpå`
			var path = new Path();
			if (xfview.Positions != null && xfview.Positions.Count >= 2)
			{
				// ëäëŒç¿ïWéwíË
				if (xfview.IsRelativePositions)
				{
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

				// ê‚ëŒç¿ïWéwíË
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