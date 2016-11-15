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

namespace WpfTestGDI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _rd;
        private System.Timers.Timer _timeData;
        private System.Timers.Timer _timeDis;
        private int _index;

        public MainWindow()
        {
            InitializeComponent();

            _rd = new Random(1212312);
            _timeData = new System.Timers.Timer(10);
            _timeData.Elapsed += _timeData_Elapsed;
            _index = 0;

            _timeDis = new System.Timers.Timer(100);
            _timeDis.Elapsed += _timeDis_Elapsed;
            _timeDis.Start();
        }

        private void _timeDis_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                CurveECGI.RefreshCurve();
                CurveECGII.RefreshCurve();
                CurveECGIII.RefreshCurve();
                CurveECGaVR.RefreshCurve();
                CurveECGaVL.RefreshCurve();
                CurveECGaVF.RefreshCurve();
                CurveECGV1.RefreshCurve();
                CurveECGV2.RefreshCurve();
                CurveECGV3.RefreshCurve();
                CurveECGV4.RefreshCurve();
                CurveECGV5.RefreshCurve();
                CurveECGV6.RefreshCurve();
            });
        }

        private void _timeData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            double dTemp = 800 * Math.Sin(2 * Math.PI * (_index++ / 100.0));
            this.Dispatcher.Invoke(() =>
            {
                CurveECGI.PushData(dTemp);
                dTemp = 800 * Math.Cos(2 * Math.PI * (_index++ / 100.0));
                CurveECGII.PushData(dTemp);
                CurveECGIII.PushData(dTemp);
                CurveECGaVR.PushData(dTemp);
                CurveECGaVL.PushData(dTemp);
                CurveECGaVF.PushData(dTemp);

                CurveECGV1.PushData(dTemp);
                CurveECGV2.PushData(dTemp);
                CurveECGV3.PushData(dTemp);
                CurveECGV4.PushData(dTemp);
                CurveECGV5.PushData(dTemp);
                CurveECGV6.PushData(dTemp);
            });


            if (_index > 100)
            {
                _index = 0;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _timeData.Start();
        }
    }
}
