using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;

namespace PCLThinCanvas.iOS
{
	public abstract class PCLThinCanvasXamarinFormsAppDelegate : FormsApplicationDelegate
	{
		// ios�ŃA�Z���u�������[�h���邽�߂̂��܂��Ȃ�
		// http://dotnetbyexample.blogspot.jp/2015/01/solving-systemiofilenotfoundexception.html
		protected PCLThinCanvas.DummyClassForLoadAssembly _dummyClassForLoadPCLThinCanvas
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected PCLThinCanvas.iOS.DummyClassForLoadAssembly _dummyClassForLoadPCLThinCanvasiOS
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}