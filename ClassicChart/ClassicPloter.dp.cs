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


namespace ClassicChart
{
    public partial class ClassicPloter
    {
        /// <summary>
        /// 控件背景色
        /// </summary>
        public Brush BackGround
        {
            get { return (Brush)GetValue(BackGroundProperty); }
            set { SetValue(BackGroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackGround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackGroundProperty =
            DependencyProperty.Register("BackGround", typeof(Brush), typeof(ClassicPloter), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 控件前景色
        /// </summary>
        public Brush ForceGround
        {
            get { return (Brush)GetValue(ForceGroundProperty); }
            set { SetValue(ForceGroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForceGround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForceGroundProperty =
            DependencyProperty.Register("ForceGround", typeof(Brush), typeof(ClassicPloter), new PropertyMetadata(Brushes.White));


        /// <summary>
        /// 曲线前景色
        /// </summary>
        public Brush CurveForceGround
        {
            get { return (Brush)GetValue(CurveForceGroundProperty); }
            set { SetValue(CurveForceGroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurveLineBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurveForceGroundProperty =
            DependencyProperty.Register("CurveForceGround", typeof(Brush), typeof(ClassicPloter), new PropertyMetadata(Brushes.LightGreen));
        /// <summary>
        /// 标题的前景色
        /// </summary>
        public Brush TitleForceGround
        {
            get { return (Brush)GetValue(TitleForceGroundProperty); }
            set { SetValue(TitleForceGroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TitleForceGround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleForceGroundProperty =
            DependencyProperty.Register("TitleForceGround", typeof(Brush), typeof(ClassicPloter), new PropertyMetadata(Brushes.White));
        
        /// <summary>
        /// 显示的标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ClassicPloter), new PropertyMetadata(""));

        /// <summary>
        /// 曲线刷新点后面的空白宽度，在循环刷新时有效
        /// </summary>
        public int CurveBlackWidth
        {
            get { return (int)GetValue(CurveBlackWidthProperty); }
            set { SetValue(CurveBlackWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurveBlackWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurveBlackWidthProperty =
            DependencyProperty.Register("CurveBlackWidth", typeof(int), typeof(ClassicPloter), new PropertyMetadata(10));
        
        /// <summary>
        /// 控件能显示的最大值
        /// </summary>
        public double CurveMaxVal
        {
            get { return (double)GetValue(CurveMaxValProperty); }
            set { SetValue(CurveMaxValProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurveMaxVal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurveMaxValProperty =
            DependencyProperty.Register("CurveMaxVal", typeof(double), typeof(ClassicPloter), new PropertyMetadata(1000.0));
        
        /// <summary>
        /// 控件所能显示的最小值
        /// </summary>
        public double CurveMinVal
        {
            get { return (double)GetValue(CurveMinValProperty); }
            set { SetValue(CurveMinValProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurveMinVal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurveMinValProperty =
            DependencyProperty.Register("CurveMinVal", typeof(double), typeof(ClassicPloter), new PropertyMetadata(-1000.0));

        /// <summary>
        /// 是否是能1mV标志
        /// </summary>
        public bool ECG1mvTagEnable
        {
            get { return (bool)GetValue(ECG1mvTagEnableProperty); }
            set { SetValue(ECG1mvTagEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ECG1mvTagEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ECG1mvTagEnableProperty =
            DependencyProperty.Register("ECG1mvTagEnable", typeof(bool), typeof(ClassicPloter), new PropertyMetadata(true));

        /// <summary>
        /// 1mV 标志的实际值，用于计算标志在显示时的高度
        /// </summary>
        public double ECG1mvTagVal
        {
            get { return (double)GetValue(ECG1mvTagValProperty); }
            set { SetValue(ECG1mvTagValProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ECG1mvTagVal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ECG1mvTagValProperty =
            DependencyProperty.Register("ECG1mvTagVal", typeof(double), typeof(ClassicPloter), new PropertyMetadata(20971.0));

        /// <summary>
        /// 是否是能背景网格线
        /// </summary>
        public bool GridEnable
        {
            get { return (bool)GetValue(GridEnableProperty); }
            set { SetValue(GridEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridEnableProperty =
            DependencyProperty.Register("GridEnable", typeof(bool), typeof(ClassicPloter), new PropertyMetadata(false));

        /// <summary>
        /// 网格前景色
        /// </summary>
        public Brush GridForceGround
        {
            get { return (Brush)GetValue(GridForceGroundProperty); }
            set { SetValue(GridForceGroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridForceGround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridForceGroundProperty =
            DependencyProperty.Register("GridForceGround", typeof(Brush), typeof(ClassicPloter), new PropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// 曲线滚动显示，否则循环显示
        /// </summary>
        public bool CurveRollEnable
        {
            get { return (bool)GetValue(CurveRollEnableProperty); }
            set { SetValue(CurveRollEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurveRollEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurveRollEnableProperty =
            DependencyProperty.Register("CurveRollEnable", typeof(bool), typeof(ClassicPloter), new PropertyMetadata(false));


    }
}
