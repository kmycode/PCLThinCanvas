# PCLThinCanvas
This is a simple graphics library for Xamarin.Forms (Android/iOS only).
This supports only following:

|Class Name|Description|
|---|---|
|LineView|Draw line|
|SquareView|Draw Square|
|EllipseView|Draw Ellipse|

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

## Properties

|Class Name|LineColor|LineWidth|LineCap|LineStyle|LineDirection|CornerRadiusSize|FillColor|
|---|---|---|---|---|---|---|---|
|LineView|○|○|○|○|○|||
|SquareView|○|○||○||○|○|
|EllipseView|○|○||○|||○|

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
