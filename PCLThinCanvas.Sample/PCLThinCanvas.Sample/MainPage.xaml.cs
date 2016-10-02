using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLThinCanvas.Core;
using Xamarin.Forms;

namespace PCLThinCanvas.Sample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			ObservableCollection<IPoint> positions = new ObservableCollection<IPoint>();
			positions.Add(new Core.Point { Top = 0.2, Left = 0.5 });
			positions.Add(new Core.Point { Top = 0.8, Left = 0.8 });
			positions.Add(new Core.Point { Top = 0.8, Left = 0.2 });
			this.Polygon.Positions = positions;
		}
	}
}
