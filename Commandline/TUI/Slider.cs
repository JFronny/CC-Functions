using System;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     Provides a control to select a number from a range of numbers
    /// </summary>
    public class Slider : Control
    {
        private int _maxValue = 10;
        private int _minValue;
        private int _value = 5;

        /// <summary>
        ///     The size of steps in this slider
        /// </summary>
        public int StepSize = 1;

        /// <summary>
        ///     Generates a new slider
        /// </summary>
        public Slider()
        {
            Input += (screen, args) =>
            {
                switch (args.Info.Key)
                {
                    case ConsoleKey.LeftArrow:
                        _value -= StepSize;
                        if (_value < MinValue)
                            _value = MinValue;
                        Value = _value;
                        ValueChanged?.Invoke(screen, new EventArgs());
                        break;
                    case ConsoleKey.RightArrow:
                        _value += StepSize;
                        if (_value > MaxValue)
                            _value = MaxValue;
                        Value = _value;
                        ValueChanged?.Invoke(screen, new EventArgs());
                        break;
                }
            };
        }

        /// <summary>
        ///     The maximum value for this slider
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if too low/high</exception>
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                if (value > MinValue && value >= Value)
                    _maxValue = value;
                else
                    throw new ArgumentOutOfRangeException(
                        "MaxValue must be larger than MinValue and equal to or larger than Value");
            }
        }

        /// <summary>
        ///     The minimal value for this slider
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if too low/high</exception>
        public int MinValue
        {
            get => _minValue;
            set
            {
                if (value < MaxValue && value <= Value)
                    _minValue = value;
                else
                    throw new ArgumentOutOfRangeException(
                        "MaxValue must be larger than MinValue and equal to or smaller than Value");
            }
        }

        /// <summary>
        ///     The current value of this slider
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if too low/high</exception>
        public int Value
        {
            get => _value;
            set
            {
                if (value <= MaxValue && value >= MinValue)
                    _value = value;
                else
                    throw new ArgumentOutOfRangeException("Value must be between MinValue and MaxValue");
            }
        }

        /// <inheritdoc />
        public override bool Selectable { get; } = true;

        /// <inheritdoc />
        public override Pixel[,] Render()
        {
            int delta = MaxValue - MinValue;
            int litValLen = Math.Max(MaxValue.ToString().Length, MinValue.ToString().Length);
            int prevpts = Math.Max((Value - MinValue) * Size.Width / delta - litValLen - 2, 0);
            int postpts = Math.Max(Size.Width - prevpts - litValLen - 2, 0);
            char[,] rend = $"{new string('=', prevpts)}[{Value.ToString($"D{(Value < 0 ? litValLen - 1 : litValLen)}")}]{new string('=', postpts)}"
                .ToNdArray2D();
            int f1 = rend.GetLength(0);
            int f2 = rend.GetLength(1);
            Pixel[,] output = new Pixel[f1, f2];
            output.Populate(new Pixel());
            for (int i = 0; i < f1; i++)
            for (int j = 0; j < f2; j++)
                output[i, j] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor,
                    rend[i, j]);
            return output;
        }
        
        /// <summary>
        /// Called if the selected value of the slider changes
        /// </summary>
        public event OnClick ValueChanged;
    }
}