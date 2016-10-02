using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLThinCanvas.Core
{
	public delegate void TappedEventHandler(object sender, TappedEventArgs e);
	public class TappedEventArgs : EventArgs
	{
		public double TapX { get; set; }
		public double TapY { get; set; }
	}

	public delegate void LongTappedEventHandler(object sender, LongTappedEventArgs e);
	public class LongTappedEventArgs : EventArgs
	{
		public double TapX { get; set; }
		public double TapY { get; set; }
	}

	public enum LineCap
	{
		Square,
		Butt,
		Round,
	}

	public enum LineStyle
	{
		Solid,
		Dotted,
		Dashed,
	}

	public enum LineDirection
	{
		LeftToRight,
		RightToLeft,
	}

	public enum LineJoin
	{
		Bevel,
		Miter,
		Round,
	}

	public interface IPoint
	{
		double Top { get; }
		double Left { get; }
	}

	public struct Point : IPoint
	{
		public double Top { get; set; }
		public double Left { get; set; }
	}

	public struct RelativePoint : IPoint
	{
		public double Top { get; set; }
		public double Left { get; set; }
		public double Right
		{
			get
			{
				return 1 - this.Left;
			}
			set
			{
				this.Left = 1 - value;
			}
		}
		public double Bottom
		{
			get
			{
				return 1 - this.Top;
			}
			set
			{
				this.Top = 1 - value;
			}
		}
	}
}
