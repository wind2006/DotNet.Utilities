namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System;
    using System.Drawing;

    using DevExpress.Utils;
    using DevExpress.XtraCharts;

    /// <summary>
    ///Chart帮助类
    /// </summary>
    public static class ChartHelper
    {
        #region Methods

        /// <summary>
        /// 添加基本的Series
        /// </summary>
        /// <param name="chat">ChartControl</param>
        /// <param name="seriesName">Series的名称</param>
        /// <param name="seriesType">Series的类型</param>
        /// <param name="dataSource">Series的绑定数据源</param>
        /// <param name="argumentDataMember">ArgumentDataMember绑定字段名称</param>
        /// <param name="valueDataMembers">ValueDataMembers的绑定字段数组</param>
        /// <param name="visible">Series是否可见</param>
        /// <returns>Series</returns>
        public static Series AddBaseSeries(this ChartControl chat, string seriesName, ViewType seriesType, object dataSource, string argumentDataMember, string[] valueDataMembers, bool visible)
        {
            Series _baseSeries = new Series(seriesName, seriesType);
            _baseSeries.ArgumentDataMember = argumentDataMember;
            _baseSeries.ValueDataMembers.AddRange(valueDataMembers);
            _baseSeries.DataSource = dataSource;
            _baseSeries.Visible = visible;
            chat.Series.Add(_baseSeries);
            return _baseSeries;
        }

        /// <summary>
        /// 增加数据筛选
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="columnName">列名称</param>
        /// <param name="value">列名称对应的筛选数值</param>
        /// <param name="dataFilterCondition">DataFilterCondition枚举</param>
        public static void AddDataFilter(this SeriesBase series, string columnName, object value, DataFilterCondition dataFilterCondition)
        {
            series.DataFilters.Add(new DataFilter(columnName, value.GetType().FullName, dataFilterCondition, value));
        }

        /// <summary>
        /// 创建Drill-Down样式的Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">title文字</param>
        /// <param name="visible">是否可见</param>
        public static void AddDrillDownTitle(this ChartControl chart, string title, bool visible)
        {
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Alignment = StringAlignment.Near;
            _chartTitle.Antialiasing = false;
            _chartTitle.Font = new Font("Tahoma", 10F, FontStyle.Underline);
            _chartTitle.Indent = 20;
            _chartTitle.Text = title;
            _chartTitle.TextColor = Color.RoyalBlue;
            _chartTitle.Visible = false;
            chart.Titles.Add(_chartTitle);
        }

        /// <summary>
        /// 新增ChartControl的Title文字
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">Title文字</param>
        /// <param name="titlePosition">Title位置</param>
        public static void AddTitle(this ChartControl chart, string title, ChartTitleDockStyle titlePosition)
        {
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Text = title;
            _chartTitle.Dock = titlePosition;
            chart.Titles.Add(_chartTitle);
        }

        /// <summary>
        /// 新增ChartControl的Title文字
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">Title文字</param>
        /// <param name="visible">是否显示</param>
        /// <param name="titlePosition">Title位置</param>
        public static void AddTitle(this ChartControl chart, string title, bool visible, ChartTitleDockStyle titlePosition)
        {
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Text = title;
            _chartTitle.Visible = visible;
            _chartTitle.Dock = titlePosition;
            chart.Titles.Add(_chartTitle);
        }

        /// <summary>
        /// 先删除Chart的Title，然后添加新的Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="title">Title文字</param>
        /// <param name="titlePosition">Title位置</param>
        public static void ClearThenAddTitle(this ChartControl chart, string title, ChartTitleDockStyle titlePosition)
        {
            chart.Titles.Clear();
            ChartTitle _chartTitle = new ChartTitle();
            _chartTitle.Text = title;
            _chartTitle.Dock = titlePosition;
            chart.Titles.Add(_chartTitle);
        }

        /// <summary>
        /// 创建基准线ConstantLine
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="ctAxisValue">基准线数值</param>
        /// <param name="ctLegendText">基准线图例文字</param>
        /// <param name="ctTitle">基准线文字</param>
        /// <param name="ctTitleColor">基准线字体颜色</param>
        /// <param name="ctLineColor">基准线颜色</param>
        /// <param name="ctLineStyle">基准线样式</param>
        public static void CreateConstantLine(this ChartControl chart, int ctAxisValue, string ctLegendText, string ctTitle, Color ctTitleColor, Color ctLineColor, DashStyle ctLineStyle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    ConstantLine _ctLine = new ConstantLine();

                    _ctLine.AxisValue = ctAxisValue;
                    _ctLine.Visible = true;
                    _ctLine.ShowInLegend = true;
                    _ctLine.LegendText = ctLegendText;
                    _ctLine.ShowBehind = false;

                    _ctLine.Title.Visible = true;
                    _ctLine.Title.Text = ctTitle;
                    _ctLine.Title.TextColor = ctTitleColor;
                    _ctLine.Title.Antialiasing = false;
                    _ctLine.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                    _ctLine.Title.ShowBelowLine = true;
                    _ctLine.Title.Alignment = ConstantLineTitleAlignment.Far;

                    _ctLine.Color = ctLineColor;
                    _ctLine.LineStyle.DashStyle = ctLineStyle;
                    _ctLine.LineStyle.Thickness = 2;

                    _diagram.AxisY.ConstantLines.Add(_ctLine);
                }
            }
        }

        /// <summary>
        /// 创建Strip
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="strip">Strip</param>
        /// <param name="stripLable">Strip文字</param>
        /// <param name="stripLengend">Strip对应的Lengend文字</param>
        /// <param name="stripColor">Strip颜色</param>
        /// <param name="stripStyle">Strip填充样式</param>
        public static void CreateStrip(this ChartControl chart, Strip strip, string stripLable, string stripLengend, Color stripColor, FillMode stripStyle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null && strip != null)
                {
                    _diagram.AxisY.Strips.Add(strip);

                    _diagram.AxisY.Strips[0].Visible = true;
                    _diagram.AxisY.Strips[0].ShowAxisLabel = true;
                    _diagram.AxisY.Strips[0].AxisLabelText = stripLable;
                    _diagram.AxisY.Strips[0].ShowInLegend = true;
                    _diagram.AxisY.Strips[0].LegendText = stripLengend;

                    _diagram.AxisY.Strips[0].Color = stripColor;
                    _diagram.AxisY.Strips[0].FillStyle.FillMode = stripStyle;
                }
            }
        }

        /// <summary>
        /// 十字标线的Lable格式化设置
        /// <para>【{A} Use it to display a series point arguments 】</para>
        /// <para>【{V} Use it to display a series point values】</para>
        /// <para>【{S} Use it to display the name of the series】</para>
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="formatString">CrosshairLabel格式化设置；【{A}{V}{S}】</param>
        public static void CustomCrosshairLabel(this SeriesBase series, string formatString)
        {
            if (series.CrosshairEnabled != DefaultBoolean.True)
                series.CrosshairEnabled = DefaultBoolean.True;
            series.CrosshairLabelPattern = formatString;
        }

        /// <summary>
        /// Lable格式化设置
        /// <para>【{A} Use it to display a series point arguments 】</para>
        /// <para>【{V} Use it to display a series point values】</para>
        /// <para>【{S} Use it to display the name of the series】</para>
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="formatString">Lable格式化设置；【{A}{V}{S}】</param>
        public static void CustomLable(this SeriesBase series, string formatString)
        {
            if (series.LabelsVisibility != DefaultBoolean.True)
                series.LabelsVisibility = DefaultBoolean.True;
            series.Label.PointOptions.Pattern = formatString;
        }

        /// <summary>
        /// 设置ChartControl X轴滚动条
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backColor">滚动条背景颜色</param>
        /// <param name="barColor">滚动条颜色</param>
        /// <param name="borderColor">滚动条边框颜色</param>
        /// <param name="barThickness">滚动条宽度</param>
        /// <param name="barAlignment">滚动条位置</param>
        public static void SetAxisXScrollBar(this ChartControl chart, Color backColor, Color barColor, Color borderColor, int barThickness, ScrollBarAlignment barAlignment)
        {
            ScrollBarOptions _scrollBarOptions = SetScrollBar(chart, backColor, barColor, borderColor, barThickness);
            if (_scrollBarOptions != null)
            {
                _scrollBarOptions.XAxisScrollBarAlignment = barAlignment;
                _scrollBarOptions.XAxisScrollBarVisible = true;
                _scrollBarOptions.YAxisScrollBarVisible = false;
            }
        }

        /// <summary>
        /// 将X轴格式化成时间轴
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="dateTimeMeasureUnit">X轴刻度单位</param>
        /// <param name="dateTimeGridAlignment">X轴刻度间距的单位</param>
        public static void SetAxisXTime(this ChartControl chart, DateTimeMeasurementUnit dateTimeMeasureUnit, DateTimeMeasurementUnit dateTimeGridAlignment)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisX.DateTimeMeasureUnit = dateTimeMeasureUnit;//X轴刻度单位
                    _diagram.AxisX.DateTimeGridAlignment = dateTimeGridAlignment;//X轴刻度间距
                }
            }
        }

        /// <summary>
        /// 将X轴格式化成时间轴
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="dateTimeMeasureUnit">X轴刻度单位</param>
        /// <param name="dateTimeGridAlignment">X轴刻度间距的单位</param>
        /// <param name="formatString">时间格式；eg:yyyy-MM</param>
        public static void SetAxisXTime(this ChartControl chart, DateTimeMeasurementUnit dateTimeMeasureUnit, DateTimeMeasurementUnit dateTimeGridAlignment, string formatString)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisX.DateTimeMeasureUnit = dateTimeMeasureUnit;//X轴刻度单位
                    _diagram.AxisX.DateTimeGridAlignment = dateTimeGridAlignment;//X轴刻度间距
                    _diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
                    _diagram.AxisX.DateTimeOptions.FormatString = formatString;
                }
            }
        }

        /// <summary>
        /// 设置X轴Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="titleText">Title文字</param>
        /// <param name="titleColor">Title文字颜色</param>
        public static void SetAxisXTitle(this ChartControl chart, string titleText, Color titleColor)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisX.Title.Visible = true;
                    _diagram.AxisX.Title.Alignment = StringAlignment.Center;
                    _diagram.AxisX.Title.Text = titleText;
                    _diagram.AxisX.Title.TextColor = titleColor;
                    _diagram.AxisX.Title.Antialiasing = true;
                    _diagram.AxisX.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                }
            }
        }

        /// <summary>
        /// 设置ChartControl Y轴滚动条
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backColor">滚动条背景颜色</param>
        /// <param name="barColor">滚动条颜色</param>
        /// <param name="borderColor">滚动条边框颜色</param>
        /// <param name="barThickness">滚动条宽度</param>
        /// <param name="barAlignment">滚动条位置</param>
        public static void SetAxisYScrollBar(this ChartControl chart, Color backColor, Color barColor, Color borderColor, int barThickness, ScrollBarAlignment barAlignment)
        {
            ScrollBarOptions _scrollBarOptions = SetScrollBar(chart, backColor, barColor, borderColor, barThickness);
            if (_scrollBarOptions != null)
            {
                _scrollBarOptions.XAxisScrollBarVisible = false;
                _scrollBarOptions.YAxisScrollBarVisible = true;
                _scrollBarOptions.YAxisScrollBarAlignment = barAlignment;
            }
        }

        /// <summary>
        /// 设置Y轴Title
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="titleText">Title文字</param>
        /// <param name="titleColor">Title文字颜色</param>
        public static void SetAxisYTitle(this ChartControl chart, string titleText, Color titleColor)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.AxisY.Title.Visible = true;
                    _diagram.AxisY.Title.Alignment = StringAlignment.Center;
                    _diagram.AxisY.Title.Text = titleText;
                    _diagram.AxisY.Title.TextColor = titleColor;
                    _diagram.AxisY.Title.Antialiasing = true;
                    _diagram.AxisY.Title.Font = new Font("Tahoma", 14, FontStyle.Bold);
                }
            }
        }

        /// <summary>
        /// 设置Legend位于底部并居中
        /// </summary>
        /// <param name="legend">Legend</param>
        public static void SetBottomCenter(this Legend legend)
        {
            legend.Direction = LegendDirection.LeftToRight;
            legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
        }

        /// <summary>
        /// 设置ColorEach
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="colorEach">是否设置成ColorEach</param>
        public static void SetColorEach(this SeriesBase series, bool colorEach)
        {
            SeriesViewColorEachSupportBase colorEachView = (SeriesViewColorEachSupportBase)series.View;
            if (colorEachView != null)
            {
                colorEachView.ColorEach = colorEach;
            }
        }

        /// <summary>
        /// 设置是否显示十字标线
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="crosshair">是否显示十字标线</param>
        public static void SetCrosshair(this ChartControl chart, bool crosshair)
        {
            chart.CrosshairEnabled = crosshair ? DefaultBoolean.True : DefaultBoolean.False;
            chart.CrosshairOptions.ShowArgumentLabels = crosshair;
            chart.CrosshairOptions.ShowArgumentLine = crosshair;
            chart.CrosshairOptions.ShowValueLabels = crosshair;
            chart.CrosshairOptions.ShowValueLine = crosshair;
        }

        /// <summary>
        /// 设置钻取
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backKeyWord">返回主Series的关键字</param>
        /// <param name="gotoHandler">向下钻取委托；参数【SeriesPoint】</param>
        /// <param name="backHandler">返回主Series的委托；参数【SeriesPoint】</param>
        public static void SetDrillDown(this ChartControl chart, string backKeyWord, Action<SeriesPoint> gotoHandler, Action<SeriesPoint> backHandler)
        {
            //eg:
            // chartLh.SetDrillDown(
            // "返回",
            //  point =>
            //{
            //    string _argument = point.Argument.ToString();
            //    if (chartLh.Series["pieSeries"].Visible)
            //    {
            //        chartLh.Series["pieSeries"].Visible = false;
            //        chartLh.SeriesTemplate.Visible = true;
            //        if (chartLh.SeriesTemplate.DataFilters.Count == 0)
            //            chartLh.SeriesTemplate.AddDataFilter("categoryName", _argument, DataFilterCondition.Equal);
            //        else
            //            chartLh.SeriesTemplate.DataFilters[0].Value = _argument;
            //        chartLh.Titles[1].Visible = true;
            //        chartLh.Titles[0].Visible = false;
            //    }
            //},
            //  point =>
            //{
            //    chartLh.Titles[0].Visible = true;
            //    chartLh.Series["pieSeries"].Visible = true;
            //    chartLh.SeriesTemplate.Visible = false;
            //});

            chart.MouseClick += (sender, e) =>
            {
                ChartControl _curChart = sender as ChartControl;
                ChartHitInfo _hitInfo = _curChart.CalcHitInfo(e.X, e.Y);
                SeriesPoint _point = _hitInfo.SeriesPoint;
                if (_point != null)
                {
                    gotoHandler(_point);
                }
                ChartTitle link = _hitInfo.ChartTitle;
                if (link != null && link.Text.StartsWith(backKeyWord))
                {
                    link.Visible = false;
                    backHandler(_point);
                }
            };
        }

        /// <summary>
        /// 设置饼状图的Lable位置
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="lablePosition">PieSeriesLabelPosition枚举</param>
        public static void SetLablePosition(this SeriesBase series, PieSeriesLabelPosition lablePosition)
        {
            if (series.Label is PieSeriesLabel)
            {
                PieSeriesLabel _label = series.Label as PieSeriesLabel;
                _label.Position = lablePosition;
            }
            //if (series.Label is Pie3DSeriesLabel)
            //{
            //    Pie3DSeriesLabel _label = series.Label as Pie3DSeriesLabel;
            //    _label.Position = lablePosition;
            //}
        }

        /// <summary>
        /// 饼状图突出设置
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="pieSeries">Series【仅仅适用于PieSeriesView】</param>
        /// <param name="explodeMode">突出模式【枚举】</param>
        /// <param name="explodedValue">突出间距</param>
        /// <param name="dragPie">是否可以拖动突出饼状</param>
        public static void SetPieExplode(this ChartControl chart, SeriesBase pieSeries, PieExplodeMode explodeMode, int explodedValue, bool dragPie)
        {
            if (pieSeries.View is PieSeriesView)
            {
                if (!chart.RuntimeHitTesting)
                    chart.RuntimeHitTesting = true;

                PieSeriesView _pieView = pieSeries.View as PieSeriesView;
                _pieView.ExplodeMode = explodeMode;
                _pieView.ExplodedDistancePercentage = explodedValue;
                _pieView.RuntimeExploding = dragPie;
            }
        }

        /// <summary>
        /// 饼状Series设置成百分比显示
        /// </summary>
        /// <param name="series">SeriesBase</param>
        /// <param name="valueLegendType">Series对应Lengend显示类型</param>
        /// <param name="lengendPointView">Series对应Lengend PointView类型</param>
        public static void SetPiePercentage(this SeriesBase series, NumericFormat valueLegendType, PointView lengendPointView)
        {
            if (series.View is PieSeriesView || series.View is Pie3DSeriesView)
            {
                PiePointOptions _piePointOptions = (PiePointOptions)series.Label.PointOptions;
                if (_piePointOptions != null)
                {
                    _piePointOptions.PercentOptions.ValueAsPercent = true;
                    _piePointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                    _piePointOptions.ValueNumericOptions.Precision = 0;
                    series.LegendPointOptions.ValueNumericOptions.Format = valueLegendType;
                    series.LegendPointOptions.PointView = lengendPointView;
                }
            }
        }

        /// <summary>
        /// 设置饼状图的Series以及Legend颜色
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="setColorHander">委托；参数顺序【DrawOptions，LegendDrawOptions，SeriesPoint】</param>
        public static void SetPieSeriesColor(this  ChartControl chart, Action<PieDrawOptions, PieDrawOptions, SeriesPoint> setColorHander)
        {
            //eg:
            //chartControl1.SetPieSeriesColor((drawOptions, legendDrawOptions, seriesPoint) =>
            //{
            //    if (seriesPoint.Argument == "A")
            //    {
            //        drawOptions.Color = Color.Red;
            //        legendDrawOptions.Color = Color.Red;
            //    }
            //    if (seriesPoint.Argument == "B")
            //    {
            //        drawOptions.Color = Color.Yellow;
            //        legendDrawOptions.Color = Color.Yellow;
            //    }
            //    if (seriesPoint.Argument == "C")
            //    {
            //        drawOptions.Color = Color.Gray;
            //        legendDrawOptions.Color = Color.Gray;
            //    }
            //    if (seriesPoint.Argument == "D")
            //    {
            //        drawOptions.Color = Color.Green;
            //        legendDrawOptions.Color = Color.Green;
            //    }
            //    if (seriesPoint.Argument == "E")
            //    {
            //        drawOptions.Color = Color.Blue;
            //        legendDrawOptions.Color = Color.Blue;
            //    }
            //});
            chart.CustomDrawSeriesPoint += (sender, e) =>
            {
                if (e.SeriesDrawOptions is PieDrawOptions)
                {
                    PieDrawOptions _drawOptions = e.SeriesDrawOptions as PieDrawOptions;
                    PieDrawOptions _legendDrawOptions = e.LegendDrawOptions as PieDrawOptions;
                    _drawOptions.FillStyle.FillMode = FillMode.Solid;
                    _legendDrawOptions.FillStyle.FillMode = FillMode.Solid;
                    setColorHander(_drawOptions, _legendDrawOptions, e.SeriesPoint);
                }
            };
        }

        /// <summary>
        /// 设置ChartControl滚动条【默认X,Y轴都出现】
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="backColor">滚动条背景颜色</param>
        /// <param name="barColor">滚动条颜色</param>
        /// <param name="borderColor">滚动条边框颜色</param>
        /// <param name="barThickness">滚动条宽度</param>
        public static ScrollBarOptions SetScrollBar(this ChartControl chart, Color backColor, Color barColor, Color borderColor, int barThickness)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _diagram = (XYDiagram)chart.Diagram;
                if (_diagram != null)
                {
                    _diagram.EnableAxisXScrolling = true;
                    _diagram.EnableAxisYScrolling = true;
                    _diagram.EnableAxisXZooming = true;
                    _diagram.EnableAxisYZooming = true;
                    ScrollBarOptions _scrollBarOptions = _diagram.DefaultPane.ScrollBarOptions;
                    _scrollBarOptions.BackColor = backColor;
                    _scrollBarOptions.BarColor = barColor;
                    _scrollBarOptions.BorderColor = borderColor;
                    _scrollBarOptions.BarThickness = barThickness;
                    return _scrollBarOptions;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置Series以及Legend颜色
        /// </summary>
        /// <typeparam name="T">DrawOptions</typeparam>
        /// <param name="chart">ChartControl</param>
        /// <param name="setColorHander">委托；参数顺序【DrawOptions，LegendDrawOptions，LegendText】</param>
        public static void SetSeriesColor<T>(this  ChartControl chart, Action<T, T, string> setColorHander)
            where T : DrawOptions
        {
            //eg:
            //chartControl1.SetSeriesColor<BarDrawOptions>((drawOptions, legendDrawOptions, legendText) =>
            //{
            //    if (legendText == "Month: Jan")
            //    {
            //        drawOptions.Color = Color.Red;
            //        legendDrawOptions.Color = Color.Red;
            //    }
            //    else if (legendText == "Month: Feb")
            //    {
            //        drawOptions.Color = Color.Yellow;
            //        legendDrawOptions.Color = Color.Yellow;
            //    }
            //    else
            //    {
            //        drawOptions.Color = Color.Blue;
            //        legendDrawOptions.Color = Color.Blue;
            //    }
            //    drawOptions.FillStyle.FillMode = FillMode.Solid;
            //    legendDrawOptions.FillStyle.FillMode = FillMode.Solid;
            //});
            chart.CustomDrawSeries += (sender, e) =>
            {
                if (e.SeriesDrawOptions is T)
                {
                    T _drawOptions = e.SeriesDrawOptions as T;
                    T _legendDrawOptions = e.LegendDrawOptions as T;
                    setColorHander(_drawOptions, _legendDrawOptions, e.LegendText);
                }
            };
        }

        /// <summary>
        /// 设置SeriesTemplate参数
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="dataSource">SeriesTemplate的绑定数据源</param>
        /// <param name="argumentDataMember">ArgumentDataMember绑定字段名称</param>
        /// <param name="valueDataMembers">ValueDataMembers的绑定字段数组</param>
        /// <param name="visible">SeriesTemplate是否可见</param>
        /// <returns>SeriesBase</returns>
        public static SeriesBase SetSeriesTemplate(this ChartControl chart, object dataSource, string argumentDataMember, string[] valueDataMembers, bool visible)
        {
            chart.SeriesTemplate.ValueDataMembers.AddRange(valueDataMembers);
            chart.SeriesTemplate.ArgumentDataMember = argumentDataMember;
            chart.SeriesTemplate.Visible = visible;
            chart.DataSource = dataSource;
            return chart.SeriesTemplate;
        }

        /// <summary>
        /// ChartControl的Tooltip设置
        /// <para>举例</para>
        /// <code>
        /// <para> chartControl1.SetToolTip(toolTipController1, "交易详情", (agr, values) =></para>
        /// <para>{</para>
        /// <para> return string.Format("时间：{0}\r\n金额:{1}", agr, values[0]);</para>
        /// <para>});</para>
        /// </code>
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="tooltip">ToolTipController</param>
        /// <param name="tooltipTitle">ToolTip的Title</param>
        /// <param name="paramter">委托，参数『Argument，Values』</param>
        public static void SetToolTip(this ChartControl chart, ToolTipController tooltip, string tooltipTitle, System.Func<string, double[], string> paramter)
        {
            chart.MouseMove += (sender, e) =>
            {
                ChartControl _curChart = sender as ChartControl;
                ChartHitInfo _hitInfo = _curChart.CalcHitInfo(e.X, e.Y);
                SeriesPoint _point = _hitInfo.SeriesPoint;
                if (_point != null)
                {
                    string _msg = paramter(_point.Argument, _point.Values);
                    tooltip.ShowHint(_msg, tooltipTitle);
                }
                else
                {
                    tooltip.HideHint();
                }
            };
        }

        /// <summary>
        /// 设置X轴Lable角度
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="angle">角度</param>
        public static void SetXLableAngle(this ChartControl chart, int angle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _xyDiagram = (XYDiagram)chart.Diagram;
                if (_xyDiagram != null)
                    _xyDiagram.AxisX.Label.Angle = angle;
            }
        }

        /// <summary>
        ///  设置Y轴Lable角度
        /// </summary>
        /// <param name="chart">ChartControl</param>
        /// <param name="angle">角度</param>
        public static void SetYLableAngle(this ChartControl chart, int angle)
        {
            if (chart.Diagram is XYDiagram)
            {
                XYDiagram _xyDiagram = (XYDiagram)chart.Diagram;
                _xyDiagram.AxisY.Label.Angle = angle;
            }
        }

        #endregion Methods
    }
}