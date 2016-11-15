using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleChart
{
    public partial class SimpleWave : UserControl
    {
        ///// <summary>
        ///// 标题的背景色
        ///// </summary>
        //public Brush TitleBackGround
        //{
        //    get { return (Brush)GetValue(TitleBackGroundProperty); }
        //    set { SetValue(TitleBackGroundProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TitleBackGround.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TitleBackGroundProperty =
        //    DependencyProperty.Register("TitleBackGround", typeof(Brush), typeof(SimpleWave), new PropertyMetadata(Brushes.Black));


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
            DependencyProperty.Register("TitleForceGround", typeof(Brush), typeof(SimpleWave), new PropertyMetadata(Brushes.White));
        
        /// <summary>
        /// 控件标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SimpleWave), new PropertyMetadata(""));


    }
}
