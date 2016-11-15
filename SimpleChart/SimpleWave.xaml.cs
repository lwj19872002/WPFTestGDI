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
using System.Globalization;

namespace SimpleChart
{
    /// <summary>
    /// Interaction logic for SimpleWave.xaml
    /// </summary>
    public partial class SimpleWave : UserControl
    {
        private List<Point> _curveDatas;

        public SimpleWave()
        {
            InitializeComponent();

            DrawingVisual dv = new DrawingVisual();
            DrawingContext dc = dv.RenderOpen();

            dc.DrawLine(new Pen(Brushes.Red, 2), new Point(100, 40), new Point(200, 55));

            dc.Close();

            
        }

        

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawRectangle(Brushes.Black, new Pen(Brushes.White, 2), new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            
            // Create the initial formatted text string.
            FormattedText formattedText = new FormattedText(Title, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight,
                                                new Typeface("Verdana"), this.ActualHeight/15, TitleForceGround);
            drawingContext.DrawText(formattedText, new Point(20, 5));


            ////
            //// Create three drawings.
            ////
            //GeometryDrawing ellipseDrawing =
            //    new GeometryDrawing(
            //        new SolidColorBrush(Color.FromArgb(102, 181, 243, 20)),
            //        new Pen(Brushes.Black, 4),
            //        new EllipseGeometry(new Point(50, 50), 50, 50)
            //    );

            //GeometryDrawing ellipseDrawing2 =
            //    new GeometryDrawing(
            //        new SolidColorBrush(Color.FromArgb(102, 181, 243, 20)),
            //        new Pen(Brushes.Black, 4),
            //        new EllipseGeometry(new Point(150, 150), 50, 50)
            //    );

            //// Create a DrawingGroup to contain the drawings.
            //DrawingGroup aDrawingGroup = new DrawingGroup();
            //aDrawingGroup.Children.Add(ellipseDrawing);
            //aDrawingGroup.Children.Add(ellipseDrawing2);

            //drawingContext.DrawDrawing(aDrawingGroup);

            drawingContext.DrawLine(new Pen(Brushes.LightGreen, 2), new Point(100, 10), new Point(200, 15));
        }
    }
}
