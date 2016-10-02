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
}
