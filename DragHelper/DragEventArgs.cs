using System.Windows;

namespace DragHelper
{
    public class DragEventArgs : RoutedEventArgs
    {
        public new DragData OriginalSource
        {
            get { return base.OriginalSource as DragData; }
        }
        
        // for Readability & Usability
        public DragData DragData
        {
            get { return this.OriginalSource; }
        }
        
        public new IInputElement Source
        {
            get { return base.Source as IInputElement; }
        }

        public DragEventArgs(RoutedEvent routedEvent, DragData data) : base(routedEvent, data)
        {
        }
    }
}
