# PCLThinCanvas
This is a simple graphics library for Xamarin.Forms (Android/iOS only).
This supports only following:

|Class Name|Description|
|---|---|
|LineView|Draw diagonal line. If rect width or height is less than line-width, it became vertical or horizontal line.|
|SquareView|Draw Square|
|EllipseView|Draw Ellipse|
|PolygonView|Draw Polygon|

One instance draw one graphic. To combine some graphics, use overlayable class such as AbstractLayout, RelativeLayout or Grid.

![Droid Sample](https://github.com/kmycode/PCLThinCanvas/blob/master/images/droid.png "Droid Sample")
![iOS Sample](https://github.com/kmycode/PCLThinCanvas/blob/master/images/ios.png "iOS Sample")

## Example

XAML:
```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:tc="clr-namespace:PCLThinCanvas.Views;assembly=PCLThinCanvas"
             x:Class="XamarinFormsTest2.MainPage">
  <Grid>
    <Grid.RowDefinitions>
      <RowDefinition/>
      <RowDefinition Height="auto"/>
      <RowDefinition/>
      <RowDefinition/>
      <RowDefinition/>
    </Grid.RowDefinitions>
    <tc:LineView LineColor="Blue" LineDirection="LeftToRight"/>
    <tc:LineView LineColor="Purple" LineDirection="RightToLeft"/>
    <tc:LineView LineColor="Yellow" LineCap="Square" LineWidth="3" LineStyle="Dashed" HeightRequest="1" Grid.Row="1"/>
    <tc:SquareView LineColor="Green" FillColor="Green" Grid.Row="2"/>
    <tc:SquareView LineColor="Red" FillColor="Pink" LineCap="Round" LineWidth="6" CornerRadiusSize="24" LineStyle="Dotted" Grid.Row="3"/>
    <tc:EllipseView LineColor="Blue" FillColor="White" LineWidth="10" Grid.Row="4"/>
  </Grid>
</ContentPage>
```

## Get Started

Download nuget package.

```
Install-Package KMY.PCLThinCanvas
```

## Properties

|-|LineView|SquareView|EllipseView|PolygonView|
|---|---|---|---|---|
|IsAntiAlias ※1|○|○|○|○|
|LineColor|○|○|○|○|
|LineWidth|○|○|○|○|
|LineCap|○|||○|
|LineStyle|○|○|○|○|
|LineDirection|○||||
|LineJoin||||○|
|IsClosed||||○|
|Positions||||○|
|IsRelativePositions||||○|
|CornerRadiusSize||○|||
|FillColor||○|○|○|

※1: Android only

### How to draw polygon

Create a collection to input polygon path points (relative: 0 <= point <= 1).
Next, set the collection to PolygonView.Positions.

```csharp
ObservableCollection<IPoint> positions = new ObservableCollection<IPoint>();
positions.Add(new PCLThinCanvas.Core.Point { Top = 0.2, Left = 0.5 });
positions.Add(new PCLThinCanvas.Core.Point { Top = 0.8, Left = 0.8 });
positions.Add(new PCLThinCanvas.Core.Point { Top = 0.8, Left = 0.2 });
this.Polygon.Positions = positions;
```

If PolygonView.IsRelativePosition == false, you can set abstract points.

```csharp
ObservableCollection<IPoint> positions = new ObservableCollection<IPoint>();
positions.Add(new PCLThinCanvas.Core.Point { Top = 40, Left = 100 });
positions.Add(new PCLThinCanvas.Core.Point { Top = 160, Left = 160 });
positions.Add(new PCLThinCanvas.Core.Point { Top = 160, Left = 40 });
this.Polygon.IsRelativePosition = false;
this.Polygon.Positions = positions;
```

## Enums

### LineCap

default: Square

```csharp
	public enum LineCap
	{
		Square,
		Butt,
		Round,
	}
```

### LineStyle

default: Solid

```csharp
	public enum LineStyle
	{
		Solid,
		Dotted,
		Dashed,
	}
```

### LineJoin

default: Bevel

```csharp
	public enum LineJoin
	{
		Bevel,
		Miter,
		Round,
	}
```

### LineDirection

default: LeftToRight

```csharp
	public enum LineDirection
	{
		LeftToRight,
		RightToLeft,
	}
```

### iOS important point

for iOS, insert following code in AppDelegate.cs.

```csharp
		protected PCLThinCanvas.DummyClassForLoadAssembly _dummy1
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		protected PCLThinCanvas.iOS.DummyClassForLoadAssembly _dummy2
		{
			get
			{
				throw new NotImplementedException();
			}
		}
```
