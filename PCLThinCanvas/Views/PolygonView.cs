using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLThinCanvas.Core;
using Xamarin.Forms;

namespace PCLThinCanvas.Views
{
	public class PolygonView : BoxView
	{
		/// <summary>
		/// 線色 プロパティ
		/// </summary>
		public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
			"LineColor",
			typeof(Color),
			typeof(PolygonView),
			default(Color),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
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
			typeof(PolygonView),
			1.0,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
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
		/// 線の始端終端の形状 プロパティ
		/// </summary>
		public static readonly BindableProperty LineCapProperty = BindableProperty.Create(
			"LineCap",
			typeof(LineCap),
			typeof(PolygonView),
			default(LineCap),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
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
			typeof(PolygonView),
			default(LineStyle),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
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
		/// 線の接続点形状 プロパティ
		/// </summary>
		public static readonly BindableProperty LineJoinProperty = BindableProperty.Create(
			"LineJoin",
			typeof(LineJoin),
			typeof(PolygonView),
			default(LineJoin),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 線の接続点形状 プロパティ
		/// </summary>
		public LineJoin LineJoin
		{
			get { return (LineJoin)this.GetValue(LineJoinProperty); }
			set { this.SetValue(LineJoinProperty, value); }
		}

		/// <summary>
		/// 塗りつぶし色 プロパティ
		/// </summary>
		public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
			"FillColor",
			typeof(Color),
			typeof(PolygonView),
			default(Color),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
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
		/// 座標配列 プロパティ
		/// </summary>
		public static readonly BindableProperty PositionsProperty = BindableProperty.Create(
			"Positions",
			typeof(ICollection<IPoint>),
			typeof(PolygonView),
			default(ICollection<IPoint>),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
				if (view != null)
				{
					var newlist = newValue as INotifyCollectionChanged;
					if (newlist != null)
					{
						newlist.CollectionChanged += view.PositionCollectionChanged;
					}
					var oldlist = oldValue as INotifyCollectionChanged;
					if (oldlist != null)
					{
						oldlist.CollectionChanged -= view.PositionCollectionChanged;
					}
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		private void PositionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.OnPropertyChanged("Positions");
		}

		/// <summary>
		/// 座標配列 プロパティ
		/// </summary>
		public ICollection<IPoint> Positions
		{
			get { return (ICollection<IPoint>)this.GetValue(PositionsProperty); }
			set { this.SetValue(PositionsProperty, value); }
		}

		/// <summary>
		/// 座標が相対的であるか の プロパティ
		/// </summary>
		public static readonly BindableProperty IsRelativePositionsProperty = BindableProperty.Create(
			"IsRelativePositions",
			typeof(bool),
			typeof(PolygonView),
			true,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 座標が相対的であるか の プロパティ
		/// </summary>
		public bool IsRelativePositions
		{
			get { return (bool)this.GetValue(IsRelativePositionsProperty); }
			set { this.SetValue(IsRelativePositionsProperty, value); }
		}

		/// <summary>
		/// 始点と終点を繋げるか の プロパティ
		/// </summary>
		public static readonly BindableProperty IsClosedProperty = BindableProperty.Create(
			"IsClosed",
			typeof(bool),
			typeof(PolygonView),
			false,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// 始点と終点を繋げるか の プロパティ
		/// </summary>
		public bool IsClosed
		{
			get { return (bool)this.GetValue(IsClosedProperty); }
			set { this.SetValue(IsClosedProperty, value); }
		}

		/// <summary>
		/// アンチエイリアス プロパティ
		/// </summary>
		public static readonly BindableProperty IsAntiAliasProperty = BindableProperty.Create(
			"IsAntiAlias",
			typeof(bool),
			typeof(PolygonView),
			false,
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as PolygonView;
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
