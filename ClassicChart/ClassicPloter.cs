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
    public partial class ClassicPloter : FrameworkElement
    {
        private VisualCollection _children;
        private bool _bNeedAllRefresh;

        private DrawingVisual _BKVisual;
        private DrawingVisual _CurveVisual;
        private DrawingVisual _TopVisual;
        private DrawingContext _CurveDC;

        private Point[] _CurvePoints;
        private Point[] _DataBuff;
        private Pen _CurvePen;

        private Int32 _CurveDatainIndex;
        private Int32 _CurveDataDisIndex;
        private Int32 _CurveBuffLen;

        private static object _objLock = new object();

        public ClassicPloter()
        {
            _bNeedAllRefresh = true;
            _children = new VisualCollection(this);
            _BKVisual = new DrawingVisual();
            _TopVisual = new DrawingVisual();
            _CurveVisual = new DrawingVisual();

            _CurvePen = new Pen(CurveForceGround, 2);
            _CurvePen.Freeze();

            _CurvePoints = new Point[2000];
            _DataBuff = new Point[2000];
            _CurveBuffLen = 2000;
            _CurveDatainIndex = 0;
            _CurveDataDisIndex = 0;


            this.Loaded += ClassicPloter_Loaded;

        }

        ~ClassicPloter()
        {

        }

        public void PushData(double data)
        {
            double dTemp;

            lock (_objLock)
            {
                if (data > CurveMaxVal)
                {
                    dTemp = 0;
                }
                else if (data < CurveMinVal)
                {
                    dTemp = ActualHeight;
                }
                else
                {
                    dTemp = ActualHeight * (1.0 - ((data - CurveMinVal) / (CurveMaxVal - CurveMinVal)));
                }

                if (CurveRollEnable)
                {
                    for (int i = 0; i < _CurveBuffLen - 1; i++)
                    {
                        _DataBuff[i] = _DataBuff[i + 1];
                        _DataBuff[i].X = i;
                    }
                    _DataBuff[_CurveBuffLen - 1] = new Point(_CurveBuffLen - 1, dTemp);
                }
                else
                {
                    _DataBuff[_CurveDatainIndex++] = new Point(_CurveDatainIndex, dTemp);

                    if (_CurveDatainIndex >= _CurveBuffLen)
                    {
                        _CurveDatainIndex = 0;
                    }
                }

            }
        }

        public void PushDatas(List<double> datas)
        {
            double dTemp;
            double dVal;

            if (datas.Count == 0)
            {
                return;
            }

            lock (_objLock)
            {
                if (CurveRollEnable)
                {
                    if (datas.Count > _CurveBuffLen)
                    {
                        for (int i = 0; i < _CurveBuffLen; i++)
                        {
                            dVal = datas[i + datas.Count - _CurveBuffLen];
                            if (dVal > CurveMaxVal)
                            {
                                dTemp = 0;
                            }
                            else if (dVal < CurveMinVal)
                            {
                                dTemp = ActualHeight;
                            }
                            else
                            {
                                dTemp = ActualHeight * (1.0 - (dVal - CurveMinVal) / (CurveMaxVal - CurveMinVal));
                            }
                            _DataBuff[i] = new Point(i, dTemp);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < (_CurveBuffLen - datas.Count); i++)
                        {
                            _DataBuff[i] = _DataBuff[i + datas.Count];
                            _DataBuff[i].X = i;
                        }
                        for (int i = 0; i < datas.Count; i++)
                        {
                            dVal = datas[i];
                            if (dVal > CurveMaxVal)
                            {
                                dTemp = 0;
                            }
                            else if (dVal < CurveMinVal)
                            {
                                dTemp = ActualHeight;
                            }
                            else
                            {
                                dTemp = ActualHeight * (1.0 - (dVal - CurveMinVal) / (CurveMaxVal - CurveMinVal));
                            }

                            _DataBuff[i + _CurveBuffLen - datas.Count] = new Point(i + _CurveBuffLen - datas.Count, dTemp);
                        }

                    }

                }
                else
                {
                    for (int i = 0; i < datas.Count; i++)
                    {
                        dVal = datas[i];
                        if (dVal > CurveMaxVal)
                        {
                            dTemp = 0;
                        }
                        else if (dVal < CurveMinVal)
                        {
                            dTemp = ActualHeight;
                        }
                        else
                        {
                            dTemp = ActualHeight * (1.0 - (dVal - CurveMinVal) / (CurveMaxVal - CurveMinVal));
                        }

                        _DataBuff[_CurveDatainIndex++] = new Point(_CurveDatainIndex, dTemp);

                        if (_CurveDatainIndex >= _CurveBuffLen)
                        {
                            _CurveDatainIndex = 0;
                        }
                    }

                }

            }
        }

        public void RefreshCurve()
        {
            lock (_objLock)
            {
                _DataBuff.CopyTo(_CurvePoints, 0);
                _CurveDataDisIndex = _CurveDatainIndex;
            }

            _CurveDC = _CurveVisual.RenderOpen();

            if (CurveRollEnable)
            {
                for (int i = 0; i < _CurveBuffLen - 1; i++)
                {
                    _CurveDC.DrawLine(_CurvePen, _CurvePoints[i], _CurvePoints[i + 1]);
                }
            }
            else
            {
                if (_CurveDataDisIndex > 0)
                {
                    for (int i = 0; i < _CurveDataDisIndex - 1; i++)
                    {
                        _CurveDC.DrawLine(_CurvePen, _CurvePoints[i], _CurvePoints[i + 1]);
                    }
                }

                if ((_CurveDataDisIndex + CurveBlackWidth + 1) < _CurveBuffLen)
                {
                    for (int i = _CurveDataDisIndex + CurveBlackWidth; i < _CurveBuffLen - 1; i++)
                    {
                        _CurveDC.DrawLine(_CurvePen, _CurvePoints[i], _CurvePoints[i + 1]);
                    }
                }
            }

            // Persist the drawing content.
            _CurveDC.Close();
        }

        private void ClassicPloter_Loaded(object sender, RoutedEventArgs e)
        {
            _bNeedAllRefresh = true;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            _bNeedAllRefresh = true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (_bNeedAllRefresh)
            {
                // Create a rectangle and draw it in the DrawingContext.
                _CurvePen = new Pen(CurveForceGround, 2);
                _CurvePen.Freeze();

                RefreshVisual(CreateBKVisual());
                RefreshVisual(CreateCurveVisual());
                //RefreshVisual(CreateTopVisual());

                _CurveBuffLen = (int)ActualWidth;
                _CurvePoints = new Point[_CurveBuffLen];
                _DataBuff = new Point[_CurveBuffLen];
                for (int i = 0; i < _CurveBuffLen; i++)
                {
                    _CurvePoints[i] = new Point(i, ActualHeight / 2);
                }
                _CurveDatainIndex = 0;

                _bNeedAllRefresh = false;
            }


        }

        private void RefreshVisual(Visual vis)
        {
            if (_children.Contains(vis))
            {
                _children.Remove(vis);
            }
            _children.Add(vis);
        }

        // Create a DrawingVisual that contains a rectangle.
        private DrawingVisual CreateBKVisual()
        {
            //DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext dc = _BKVisual.RenderOpen();

            // Create a rectangle and draw it in the DrawingContext.

            Rect rect = new Rect(new Point(0, 0), new Size(ActualWidth, ActualHeight));
            dc.DrawRectangle(BackGround, (Pen)null, rect);

            dc.DrawText(new FormattedText(Title, new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 20, TitleForceGround), new Point(20, 5));

            if (ECG1mvTagEnable)
            {
                double dH = (ECG1mvTagVal / (CurveMaxVal - CurveMinVal)) * ActualHeight;
                Pen tagPen = new Pen(Brushes.White, 1);
                int iStartX = 60;
                dc.DrawLine(tagPen, new Point(iStartX, dH + (ActualHeight - dH) / 2), new Point(iStartX + 5, dH + (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 5, dH + (ActualHeight - dH) / 2), new Point(iStartX + 5, (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 5, (ActualHeight - dH) / 2), new Point(iStartX + 10, (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 10, (ActualHeight - dH) / 2), new Point(iStartX + 10, dH + (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 10, dH + (ActualHeight - dH) / 2), new Point(iStartX + 15, dH + (ActualHeight - dH) / 2));

                dc.DrawText(new FormattedText("1mV", new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 15, TitleForceGround), new Point(iStartX + 20, (ActualHeight - dH) / 2));
            }

            if (GridEnable)
            {
                Pen gridPen = new Pen(GridForceGround, 1);
                dc.DrawLine(gridPen, new Point(0, ActualHeight * 0.5), new Point(ActualWidth, ActualHeight * 0.5));
                dc.DrawLine(gridPen, new Point(0, ActualHeight * 0.25), new Point(ActualWidth, ActualHeight * 0.25));
                dc.DrawLine(gridPen, new Point(0, ActualHeight * 0.75), new Point(ActualWidth, ActualHeight * 0.75));

                double dTemp = (CurveMaxVal + CurveMinVal) / 2;
                Decimal decTemp = new Decimal(dTemp);
                Decimal decData = Decimal.Round(decTemp, 2, MidpointRounding.ToEven);
                dTemp = Decimal.ToDouble(decData);
                dc.DrawText(new FormattedText(dTemp.ToString(), new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 10, GridForceGround), new Point(ActualWidth - 50, ActualHeight * 0.5 + 2));

                dTemp = (CurveMaxVal - CurveMinVal) * 0.25 + CurveMinVal;
                decTemp = new Decimal(dTemp);
                decData = Decimal.Round(decTemp, 2, MidpointRounding.ToEven);
                dTemp = Decimal.ToDouble(decData);
                dc.DrawText(new FormattedText(dTemp.ToString(), new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 10, GridForceGround), new Point(ActualWidth - 50, ActualHeight * 0.75 + 2));

                dTemp = (CurveMaxVal - CurveMinVal) * 0.75 + CurveMinVal;
                decTemp = new Decimal(dTemp);
                decData = Decimal.Round(decTemp, 2, MidpointRounding.ToEven);
                dTemp = Decimal.ToDouble(decData);
                dc.DrawText(new FormattedText(dTemp.ToString(), new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 10, GridForceGround), new Point(ActualWidth - 50, ActualHeight * 0.25 + 2));
            }

            // Persist the drawing content.
            dc.Close();

            return _BKVisual;
        }

        private DrawingVisual CreateTopVisual()
        {
            //DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext dc = _TopVisual.RenderOpen();

            dc.DrawText(new FormattedText(Title, new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 20, TitleForceGround), new Point(20, 5));

            if (ECG1mvTagEnable)
            {
                double dH = (ECG1mvTagVal / (CurveMaxVal - CurveMinVal)) * ActualHeight;
                Pen tagPen = new Pen(Brushes.White, 1);
                int iStartX = 60;
                dc.DrawLine(tagPen, new Point(iStartX, dH + (ActualHeight - dH) / 2), new Point(iStartX + 5, dH + (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 5, dH + (ActualHeight - dH) / 2), new Point(iStartX + 5, (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 5, (ActualHeight - dH) / 2), new Point(iStartX + 10, (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 10, (ActualHeight - dH) / 2), new Point(iStartX + 10, dH + (ActualHeight - dH) / 2));
                dc.DrawLine(tagPen, new Point(iStartX + 10, dH + (ActualHeight - dH) / 2), new Point(iStartX + 15, dH + (ActualHeight - dH) / 2));

                dc.DrawText(new FormattedText("1mV", new CultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("宋体"), 15, TitleForceGround), new Point(iStartX + 20, (ActualHeight - dH) / 2));
            }
            // Persist the drawing content.
            dc.Close();

            return _TopVisual;
        }

        private DrawingVisual CreateCurveVisual()
        {
            //DrawingVisual drawingVisual = new DrawingVisual();

            // Retrieve the DrawingContext in order to create new drawing content.
            DrawingContext dc = _CurveVisual.RenderOpen();


            // Persist the drawing content.
            dc.Close();

            return _CurveVisual;
        }

        // Provide a required override for the VisualChildrenCount property.
        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }

        // Provide a required override for the GetVisualChild method.
        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= _children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _children[index];
        }
    }
}
