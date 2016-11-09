# WPF DragHelper
WPF IInputElement Drag Event Helper

A library help to IInputElement dragging

# Basic usage

**XAML namespace mapping**
```xml
xmlns:drag="clr-namespace:DragHelper;assembly=DragHelper"
```

**Using namespace**
```cs
using DragHelper;
```
<br />
**Usage on xaml**
```xml
<Ellipse drag:DragCapture.Draggable="True"  
         drag:DragCapture.DragBegin="Ellipse_DragBegin"
         drag:DragCapture.DragEnd="Ellipse_DragEnd"
         drag:DragCapture.Drag="Ellipse_Drag"/>
```
<br />
**Drag Registration**
```cs
// Register Draggable object
// RelativeTarget(Optional): mouse position based on <RelativeTarget>
DragCapture.Register(<IInputElement>, <RelativeTarget>);

// Unregister Draggable object
DragCapture.Unregister(<IInputElement>);
```

**Drag Event**
```cs
// Add Drag Begin Handler (Raising when IInputElement.MouseLeftButtonDown) 
<UIElement>.AddDragBeginHandler(new DragCapture.DragRoutedEventHandler(<Handler>));

// Add Drag End Handler (Raising when IInputElement.PreviewMouseLeftButtonUp)
<UIElement>.AddDragEndHandler(new DragCapture.DragRoutedEventHandler(<Handler>));

// Add Dragging Handler (Rasing when IInputElement.PreviewMouseMove)
<UIElement>.AddDragHandler(new DragCapture.DragRoutedEventHandler(<Handler>));
```

**Drag Cancel Handling**
```cs
void IInputElement_DragBegin(object sender, DragEventArgs e)
{
    e.Handled = true;
}
```

**Drag Data**
```cs
// DragData is in DragEventArgs.DragData or OriginalSource(overloads)

void IInputElement_Drag(object sender, DragEventArgs e)
{
    // Mouse event target
    e.DragData.InputTarget
    
    // Mouse position calculation target
    e.DragData.RelativeTarget

    // Position based on RelativeTarget or InputTarget
    e.DragData.StartPosition
    e.DragData.CurrentPosition
    
    // Drag Delta (CurrentPosition - StartPosition)
    e.DragData.Delta
    
    // Drag Elapsed Time
    e.DragData.ElapsedTimespan
}
```
