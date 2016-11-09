using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragHelper.Example
{
    public partial class MainWindow : Window
    {
        Point objPosition;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ellipse_Drag(object sender, DragEventArgs args)
        {
            // Drag Start Position on RelativeTarget or InputTarget
            this.tbSPos.Text = $"StartPosition: {args.DragData.StartPosition}";

            // Drag Current Position on RelativeTarget or InputTarget
            this.tbPos.Text = $"CurrentPosition: {args.DragData.CurrentPosition}";

            // Drag Delta (CurrentPosition - StartPosition)
            this.tbDelta.Text = $"Delta: {args.DragData.Delta}";

            // Drag Elapsed Time
            this.tbElapsed.Text = $"Elapsed: {args.DragData.ElapsedTimespan}";

            // Canvas Dragging
            Ellipse e = sender as Ellipse;

            Canvas.SetLeft(e, objPosition.X + args.DragData.Delta.X);
            Canvas.SetTop(e, objPosition.Y + args.DragData.Delta.Y);
        }

        private void Ellipse_DragBegin(object sender, DragEventArgs args)
        {
            // if you want to cancel
            if (cbCancel.IsChecked.Value)
            {
                args.Handled = true;
                return;
            }

            Ellipse e = sender as Ellipse;
            objPosition = new Point(Canvas.GetLeft(e), Canvas.GetTop(e));
        }

        private void Ellipse_DragEnd(object sender, DragEventArgs args)
        {
            MessageBox.Show("Drag End");

            Ellipse e = sender as Ellipse;

            Canvas.SetLeft(e, objPosition.X);
            Canvas.SetTop(e, objPosition.Y);
        }
    }
}
