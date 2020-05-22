using System;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    public class Slider : Control
    {
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
                        break;
                    case ConsoleKey.RightArrow:
                        _value += StepSize;
                        if (_value > MaxValue)
                            _value = MaxValue;
                        Value = _value;
                        break;
                }
            };
        }
        
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                if (value > MinValue && value >= Value)
                    _maxValue = value;
                else
                    throw new ArgumentOutOfRangeException("MaxValue must be larger than MinValue and equal to or larger than Value");
            }
        }

        public int MinValue
        {
            get => _minValue;
            set
            {
                if (value < MaxValue && value <= Value)
                    _minValue = value;
                else
                    throw new ArgumentOutOfRangeException("MaxValue must be larger than MinValue and equal to or smaller than Value");
            }
        }

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

        public int StepSize = 1;
        private int _value = 5;
        private int _maxValue = 10;
        private int _minValue = 0;

        public override Pixel[,] Render()
        {
            int delta = MaxValue - MinValue;
            int litValLen = Math.Max(MaxValue.ToString().Length, MinValue.ToString().Length);
            int prevpts = Math.Max((Value - MinValue) * Size.Width / delta - litValLen - 2, 0);
            int postpts = Math.Max(Size.Width - prevpts - litValLen - 2, 0);
            char[,] rend = $"{new string('=', prevpts)}[{Value.ToString($"D{litValLen}")}]{new string('=', postpts)}".ToNDArray2D();
            int f1 = rend.GetLength(0);
            int f2 = rend.GetLength(1);
            Pixel[,] output = new Pixel[f1, f2];
            output.Populate(new Pixel());
            for (int i = 0; i < f1; i++)
            for (int j = 0; j < f2; j++) output[i, j] = new Pixel(Selected ? ForeColor : BackColor, Selected ? BackColor : ForeColor, rend[i, j]);
            return output;
        }

        protected override void Resize(int width, int height)
        {
            
        }

        public override bool Selectable { get; } = true;
    }
}