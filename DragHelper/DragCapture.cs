using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace DragHelper
{
    public static class DragCapture
    {
        #region [ Routed Event ]
        public delegate void DragRoutedEventHandler(object sender, DragEventArgs args);

        public static readonly RoutedEvent DragEvent =
            EventManager.RegisterRoutedEvent("Drag", RoutingStrategy.Direct, typeof(DragRoutedEventHandler), typeof(DragData));

        public static readonly RoutedEvent DragBeginEvent =
            EventManager.RegisterRoutedEvent("DragBegin", RoutingStrategy.Direct, typeof(DragRoutedEventHandler), typeof(DragData));

        public static readonly RoutedEvent DragEndEvent =
            EventManager.RegisterRoutedEvent("DragEnd", RoutingStrategy.Direct, typeof(DragRoutedEventHandler), typeof(DragData));
        #endregion

        #region [ Local Variable ]
        private static Dictionary<IInputElement, DragData> dragDatas;
        #endregion

        #region [ Constructor ]
        static DragCapture()
        {
            dragDatas = new Dictionary<IInputElement, DragData>();
        }
        #endregion

        #region [ Routed Event Extension ]
        public static void AddDragHandler(this IInputElement element, DragRoutedEventHandler handler)
        {
            element.AddHandler(DragEvent, handler);
        }

        public static void AddDragBeginHandler(this IInputElement element, DragRoutedEventHandler handler)
        {
            element.AddHandler(DragBeginEvent, handler);
        }

        public static void AddDragEndHandler(this IInputElement element, DragRoutedEventHandler handler)
        {
            element.AddHandler(DragEndEvent, handler);
        }

        public static void RemoveDragHandler(this IInputElement element, DragRoutedEventHandler handler)
        {
            element.RemoveHandler(DragEvent, handler);
        }

        public static void RemoveDragBeginHandler(this IInputElement element, DragRoutedEventHandler handler)
        {
            element.RemoveHandler(DragBeginEvent, handler);
        }

        public static void RemoveDragEndHandler(this IInputElement element, DragRoutedEventHandler handler)
        {
            element.RemoveHandler(DragEndEvent, handler);
        }

        #endregion

        #region [ Registration ]
        public static bool Register(this IInputElement element, UIElement relativeTarget = null)
        {
            if (dragDatas.ContainsKey(element))
                return false;

            dragDatas[element] = new DragData(element, relativeTarget);

            element.MouseLeftButtonDown += Element_MouseLeftButtonDown;
            element.PreviewMouseLeftButtonUp += Element_PreviewMouseLeftButtonUp;
            element.PreviewMouseMove += Element_PreviewMouseMove;

            return true;
        }

        public static void Unregister(this IInputElement element)
        {
            if (!dragDatas.ContainsKey(element))
                return;

            dragDatas.Remove(element);

            element.MouseLeftButtonDown -= Element_MouseLeftButtonDown;
            element.PreviewMouseLeftButtonUp -= Element_PreviewMouseLeftButtonUp;
            element.PreviewMouseMove -= Element_PreviewMouseMove;

            if (Mouse.Captured.Equals(element))
                element.ReleaseMouseCapture();
        }
        #endregion

        #region [ Event ]
        private static void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = dragDatas[sender as IInputElement];

            if (!item.IsDragging)
            {
                item.Start();

                var args = new RoutedEventArgs(DragBeginEvent, item);
                item.InputTarget.RaiseEvent(args);

                if (args.Handled)
                    return;

                item.IsDragging = true;
                e.Handled = true;
            }
        }

        private static void Element_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = dragDatas[sender as IInputElement];

            if (item.IsDragging)
            {
                item.Stop();
                item.IsDragging = false;
                item.InputTarget.RaiseEvent(new RoutedEventArgs(DragEndEvent, item));

                e.Handled = true;
            }
        }

        private static void Element_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var item = dragDatas[sender as IInputElement];

            if (item.IsDragging)
                item.InputTarget.RaiseEvent(new DragEventArgs(DragEvent, item));
        }
        #endregion
    }
}