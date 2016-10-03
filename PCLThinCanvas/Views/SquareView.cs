using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLThinCanvas.Core;
using Xamarin.Forms;

namespace PCLThinCanvas.Views
{
	public class SquareView : BoxView
	{
		/// <summary>
		/// 線色 プロパティ
		/// </summary>
		public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
			"LineColor",
			typeof(Color),
			typeof(SquareView),
			default(Color),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 線色 プロパティ
		/// </summary>
		public Color LineColor
		{
			get { return (Color)this.GetValue(LineColorProperty); }
			set { this.SetValue(LineColorProperty, value); }
		}

		/// <summary>
		/// 線の太さ プロパティ
		/// </summary>
		public static readonly BindableProperty LineWidthProperty = BindableProperty.Create(
			"LineWidth",
			typeof(double),
			typeof(SquareView),
			1.0,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 線の太さ プロパティ
		/// </summary>
		public double LineWidth
		{
			get { return (double)this.GetValue(LineWidthProperty); }
			set { this.SetValue(LineWidthProperty, value); }
		}

		/// <summary>
		/// 角丸の大きさ プロパティ
		/// </summary>
		public static readonly BindableProperty CornerRadiusSizeProperty = BindableProperty.Create(
			"CornerRadiusSize",
			typeof(double),
			typeof(SquareView),
			0.0,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 角丸の大きさ プロパティ
		/// </summary>
		public double CornerRadiusSize
		{
			get { return (double)this.GetValue(CornerRadiusSizeProperty); }
			set { this.SetValue(CornerRadiusSizeProperty, value); }
		}

		/// <summary>
		/// 線の始端終端の形状 プロパティ
		/// </summary>
		public static readonly BindableProperty LineCapProperty = BindableProperty.Create(
			"LineCap",
			typeof(LineCap),
			typeof(SquareView),
			default(LineCap),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 線の始端終端の形状 プロパティ
		/// </summary>
		public LineCap LineCap
		{
			get { return (LineCap)this.GetValue(LineCapProperty); }
			set { this.SetValue(LineCapProperty, value); }
		}

		/// <summary>
		/// 線のスタイル プロパティ
		/// </summary>
		public static readonly BindableProperty LineStyleProperty = BindableProperty.Create(
			"LineStyle",
			typeof(LineStyle),
			typeof(SquareView),
			default(LineStyle),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 線のスタイル プロパティ
		/// </summary>
		public LineStyle LineStyle
		{
			get { return (LineStyle)this.GetValue(LineStyleProperty); }
			set { this.SetValue(LineStyleProperty, value); }
		}

		/// <summary>
		/// 塗りつぶし色 プロパティ
		/// </summary>
		public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
			"FillColor",
			typeof(Color),
			typeof(SquareView),
			default(Color),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 塗りつぶし色 プロパティ
		/// </summary>
		public Color FillColor
		{
			get { return (Color)this.GetValue(FillColorProperty); }
			set { this.SetValue(FillColorProperty, value); }
		}

		/// <summary>
		/// 塗りつぶし画像 プロパティ
		/// </summary>
		public static readonly BindableProperty FillImageSourceProperty = BindableProperty.Create(
			"FillImageSource",
			typeof(ImageSource),
			typeof(SquareView),
			default(ImageSource),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 塗りつぶし画像 プロパティ
		/// </summary>
		public ImageSource FillImageSource
		{
			get { return (ImageSource)this.GetValue(FillImageSourceProperty); }
			set { this.SetValue(FillImageSourceProperty, value); }
		}

		/// <summary>
		/// アンチエイリアス プロパティ
		/// </summary>
		public static readonly BindableProperty IsAntiAliasProperty = BindableProperty.Create(
			"IsAntiAlias",
			typeof(bool),
			typeof(SquareView),
			false,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as SquareView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// アンチエイリアス プロパティ
		/// </summary>
		public bool IsAntiAlias
		{
			get { return (bool)this.GetValue(IsAntiAliasProperty); }
			set { this.SetValue(IsAntiAliasProperty, value); }
		}
	}
}
