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

[assembly: ExportRenderer(typeof(PolygonView), typeof(PolygonViewRenderer))]
namespace PCLThinCanvas.iOS.Renderers
{
	public class PolygonViewRenderer : BoxRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
			{
				e.NewElement.PropertyChanged += (sender, ev) => this.SetNeedsDisplay();
			}
		}

		public override void Draw(CGRect rect)
		{
			var xfview = (PolygonView)Element;
			
			using (var context = UIGraphics.GetCurrentContext())
			{
				context.SetStrokeColor(xfview.LineColor.ToCGColor());
				context.SetFillColor(xfview.FillColor.ToCGColor());
				xfview.FillColor.ToUIColor().SetFill();
				context.SetLineWidth((nfloat)xfview.LineWidth);
				RendererUtil.SetCap(context, xfview.LineCap);
				RendererUtil.SetStyle(context, xfview.LineStyle, xfview.LineWidth);

				// 接続部形状
				switch (xfview.LineJoin)
				{
					case LineJoin.Bevel:
					default:
						context.SetLineJoin(CGLineJoin.Bevel);
						break;
					case LineJoin.Miter:
						context.SetLineJoin(CGLineJoin.Miter);
						break;
					case LineJoin.Round:
						context.SetLineJoin(CGLineJoin.Round);
						break;
				}

				this.DrawPolygon(context, xfview);
			}
		}

		private void DrawPolygon(CGContext context, PolygonView xfview)
		{
			// 多角形
			var path = new CGPath();
			if (xfview.Positions != null && xfview.Positions.Count >= 2)
			{
				// 相対座標指定
				if (xfview.IsRelativePositions)
				{
					bool isFirst = true;
					foreach (var pos in xfview.Positions)
					{
						if (isFirst)
						{
							path.MoveToPoint((float)(pos.Left * this.Element.Width), (float)(pos.Top * this.Element.Height));
							isFirst = false;
						}
						else
						{
							path.AddLineToPoint((float)(pos.Left * this.Element.Width), (float)(pos.Top * this.Element.Height));
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
							path.MoveToPoint((float)(pos.Left), (float)(pos.Top));
							isFirst = false;
						}
						else
						{
							path.AddLineToPoint((float)(pos.Left), (float)(pos.Top));
						}
					}
				}
			}
			if (xfview.IsClosed)
			{
				path.CloseSubpath();
			}

			context.AddPath(path);

			if (xfview.FillImageSource == null)
			{
				context.DrawPath(CGPathDrawingMode.FillStroke);
			}
			else
			{
				var fullRect = new CGRect(0, 0, xfview.Width, xfview.Height);
				RendererUtil.DrawMaskedImage(context, fullRect, (ct) =>
				{
					UIColor.White.SetFill();
					ct.FillRect(fullRect);
					ct.SetLineWidth(0);
					ct.SetFillColor(UIColor.Black.CGColor);
					ct.AddPath(path);
					ct.DrawPath(CGPathDrawingMode.FillStroke);
				}, xfview.FillImageSource, (float)xfview.Width, (float)xfview.Height);

				// 枠線部分
				context.DrawPath(CGPathDrawingMode.Stroke);
			}
		}
	}
}