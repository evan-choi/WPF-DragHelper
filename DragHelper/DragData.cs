using System;
using System.Windows;
using System.Windows.Input;

namespace DragHelper
{
    public class DragData
    {
        #region [ Properties ]
        public IInputElement InputTarget { get; }

        public UIElement RelativeTarget { get; }

        public DateTime StartTime { get; set; }

        public TimeSpan ElapsedTimespan
        {
            get { return (IsDragging ? DateTime.Now : stopTime) - StartTime; }
        }

        public Point StartPosition { get; set; }
        
        public Point CurrentPosition
        {
            get { return Mouse.GetPosition(RelativeTarget ?? InputTarget); }
        }
        
        public Point Delta
        {
            get { return (Point)(CurrentPosition - StartPosition); }
        }
        #endregion

        #region [ Local Variable ]
        private DateTime stopTime;
        internal bool IsDragging { get; set; }
        #endregion

        #region [ Constructor ]
        public DragData(IInputElement target, UIElement relativeTarget)
        {
            this.InputTarget = target;
            this.RelativeTarget = relativeTarget;
        }
        #endregion

        #region [ Functions ]
        internal void Start()
        {
            this.StartTime = DateTime.Now;
            this.StartPosition = this.CurrentPosition;

            Mouse.Capture(InputTarget);
        }

        internal void Stop()
        {
            stopTime = DateTime.Now;

            InputTarget.ReleaseMouseCapture();
        }
        #endregion
    }
}
