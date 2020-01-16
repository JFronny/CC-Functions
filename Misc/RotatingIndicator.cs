using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace CC_Functions.Misc
{
    /// <summary>
    ///     Animated control similar to update screens in Windows 8 and 10
    /// </summary>
    public sealed class RotatingIndicator : Control
    {
        private const double IndicatorOffset = Math.PI / 16;
        private const int MaximumIndicators = 6;
        private const int SizeFactor = 20;
        private const double StartAt = (2 * Math.PI) / 3;
        private const double TimerInterval = 100.0;
        private readonly Indicator[] indicators = new Indicator[MaximumIndicators];
        private readonly Timer timer;
        private int indicatorCenterRadius;
        private int indicatorDiameter;

        /// <summary>
        ///     Instantiates the control
        /// </summary>
        public RotatingIndicator()
        {
            for (int i = 0; i < 6; i++)
                indicators[i] = new Indicator(StartAt + (i * IndicatorOffset));
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
            ForeColorChanged += (sender, e) => Invalidate();
            SizeChanged += (sender, e) =>
            {
                int indicatorRadius = (int) Math.Round(Height / (double) SizeFactor);
                indicatorDiameter = 2 * indicatorRadius;
                Height = SizeFactor * indicatorRadius;
                Width = Height;
                int outerRadius = Height / 2;
                int innerRadius = outerRadius - indicatorDiameter;
                indicatorCenterRadius = innerRadius + indicatorRadius;
                Invalidate();
            };
            OnSizeChanged(null);
            timer = new Timer();
            timer.Elapsed += (sender, e) =>
            {
                try
                {
                    if (InvokeRequired)
                        Invoke((Action) Refresh);
                    else Refresh();
                }
                catch
                {
                    // ignored
                }
            };
            timer.Interval = TimerInterval;
            timer.Enabled = true;
        }

        /// <summary>
        ///     Start/stops indicator animation
        /// </summary>
        [Category("Appearance")]
        [Description("Start/stops indicator animation")]
        [DefaultValue(true)]
        [Bindable(true)]
        public bool Animate
        {
            get => timer.Enabled;
            set => timer.Enabled = value;
        }

        /// <summary>
        ///     Specifies indicator rotational refresh
        /// </summary>
        [Category("Appearance")]
        [Description("Specifies indicator rotational refresh")]
        [DefaultValue(200)]
        [Bindable(true)]
        public double RefreshRate
        {
            get => timer.Interval;
            set
            {
                timer.Interval = Math.Max(Math.Min(value, 200), 10);
                Invalidate();
            }
        }

        /// <summary>
        ///     Disposes used objects and the control itself
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) timer.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        ///     Paints the control and cycles the animation
        /// </summary>
        /// <param name="e">Arguments specifying the painting target</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsContainer state = e.Graphics.BeginContainer();
            e.Graphics.TranslateTransform(-Left, -Top);
            Rectangle clip = e.ClipRectangle;
            clip.Offset(Left, Top);
            PaintEventArgs pea = new PaintEventArgs(e.Graphics, clip);
            InvokePaintBackground(Parent, pea);
            InvokePaint(Parent, pea);
            e.Graphics.EndContainer(state);
            e.Graphics.Clear(BackColor);
            Brush brush = new SolidBrush(ForeColor);
            for (int i = MaximumIndicators - 1; i >= 0; i--)
            {
                double degrees = indicators[i].Radians;
                if (degrees < 0.0)
                    degrees += 2 * Math.PI;
                int dx = (int) Math.Round(indicatorCenterRadius * Math.Cos(degrees)) + indicatorCenterRadius;
                int dy = indicatorCenterRadius - (int) Math.Round(indicatorCenterRadius * Math.Sin(degrees));
                e.Graphics.FillEllipse(brush,
                    new Rectangle(new Point(dx, dy), new Size(indicatorDiameter, indicatorDiameter)));
                degrees -= indicators[i].Speed * IndicatorOffset;
                if (indicators[i].Speed > 1.0) indicators[i].Speed += 0.25;
                if (degrees < 0.0) indicators[i].Speed = 1.25;
                else if (degrees < StartAt) indicators[i].Speed = 1.0;
                indicators[i].Radians = degrees;
            }
            brush.Dispose();
        }
    }

    internal struct Indicator
    {
        public Indicator(double radians)
        {
            Radians = radians;
            Speed = 1.0;
        }

        public double Radians { get; set; }
        public double Speed { get; set; }
    }
}