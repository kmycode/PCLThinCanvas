using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLThinCanvas.Core
{
	/// <summary>
	/// 座標
	/// </summary>
	public struct TCPosition
	{
		public double X;
		public double Y;
	}

	/// <summary>
	/// 色
	/// </summary>
	public struct TCColor
	{
		public double R;
		public double G;
		public double B;
		public double A;

		public TCColor(double r, double g, double b, double a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}
	}
}
