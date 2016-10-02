using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLThinCanvas.Core
{
	/// <summary>
	/// 直線
	/// </summary>
	public class LineShape : IShape
	{
		/// <summary>
		/// 座標1
		/// </summary>
		public TCPosition Position1 { get; set; }

		/// <summary>
		/// 座標2
		/// </summary>
		public TCPosition Position2 { get; set; }

		/// <summary>
		/// 線の色
		/// </summary>
		public TCColor LineColor { get; set; }

		/// <summary>
		/// 線の太さ
		/// </summary>
		public double LineWidth { get; set; }
	}
}
