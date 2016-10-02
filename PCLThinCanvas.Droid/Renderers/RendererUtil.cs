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
	}
}