using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCLThinCanvas.Sample
{
	class TestModel : INotifyPropertyChanged
	{
		public TestModel()
		{
			this.TestCommand.Model = this;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string name = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private double _lineWidth;
		public double LineWidth
		{
			get
			{
				return this._lineWidth;
			}
			set
			{
				this._lineWidth = value;
				this.OnPropertyChanged();
			}
		}

		public TestCommandImpl TestCommand { get; private set; } = new TestCommandImpl();

		public class TestCommandImpl : ICommand
		{
			public event EventHandler CanExecuteChanged;

			public TestModel Model { private get; set; }

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public void Execute(object parameter)
			{
				this.Model.LineWidth++;
			}
		}
	}
}
