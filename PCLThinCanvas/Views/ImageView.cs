using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLThinCanvas.Core;
using Xamarin.Forms;

namespace PCLThinCanvas.Views
{
	public class ImageView : BoxView
	{
		/// <summary>
		/// 塗りつぶし画像 プロパティ
		/// </summary>
		public static readonly BindableProperty FillImageSourceProperty = BindableProperty.Create(
			"FillImageSource",
			typeof(ImageSource),
			typeof(ImageView),
			default(ImageSource),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as ImageView;
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
		/// マスク画像 プロパティ
		/// </summary>
		public static readonly BindableProperty MaskImageSourceProperty = BindableProperty.Create(
			"MaskImageSource",
			typeof(ImageSource),
			typeof(ImageView),
			default(ImageSource),
			BindingMode.OneWay,
			null,
			(bindable, oldValue, newValue) =>
			{
				var view = bindable as ImageView;
				if (view != null)
				{
					view.OnPropertyChanged();
				}
			},
			null,
			null);

		/// <summary>
		/// マスク画像 プロパティ
		/// </summary>
		public ImageSource MaskImageSource
		{
			get { return (ImageSource)this.GetValue(MaskImageSourceProperty); }
			set { this.SetValue(MaskImageSourceProperty, value); }
		}
	}
}
