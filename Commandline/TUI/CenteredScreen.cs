using System;
using System.Drawing;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     Provides a screen that stays in the middle of the console. Use ContentPanel to attach controls
    /// </summary>
    public class CenteredScreen : Screen
    {
        private bool _resizing;

        /// <summary>
        ///     The actual size of this control. The "Size" property is assigned automatically
        /// </summary>
        public Size ActualSize;

        /// <summary>
        ///     The panel used for storing and rendering the actual controls
        /// </summary>
        public Panel ContentPanel;

        private string _title = "CC-Functions.CommandLine app";
        private readonly Label _titleLabel;

        /// <summary>
        ///     Creates a screen that stays in the middle of the console. Use ContentPanel to attach controls
        /// </summary>
        /// <param name="width">The width of the content panel</param>
        /// <param name="height">The height of the content panel</param>
        /// <param name="contentBack">The content panels background (Should be different from Console.BackgroundColor)</param>
        /// <param name="color">Whether to use color when drawing</param>
        public CenteredScreen(int width, int height, ConsoleColor contentBack, bool color = true) : base(width, height, color)
        {
            ContentPanel = new Panel {BackColor = contentBack};
            ActualSize = new Size(width, height);
            _titleLabel = new Label(Title);
            Controls.Add(ContentPanel);
            Controls.Add(_titleLabel);
            WindowResize += (screen, args) => CalculatePosition();
            ((Control) this).Resize += (caller, args) => CalculatePosition();
            CalculatePosition(true);
        }

        /// <summary>
        ///     The title to display at the top of the console
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value && !string.IsNullOrWhiteSpace(value))
                {
                    _title = value;
                    CalculatePosition(true);
                }
            }
        }

        /// <summary>
        ///     Calculates the Size variable, Title and ContentPanel position/size
        /// </summary>
        /// <param name="initial">Whether this is the initial calculation</param>
        public void CalculatePosition(bool initial = false)
        {
            if (!_resizing)
            {
                _resizing = true;
                Size = new Size(Console.WindowWidth, Console.WindowHeight - 1);
                _titleLabel.Content = Title + Environment.NewLine + new string(SpecialChars.OneLineSimple.LeftRight, Console.WindowWidth);
                ContentPanel.Size = ActualSize;
                ContentPanel.Point = new Point((Console.WindowWidth - ActualSize.Width) / 2,
                    (Console.WindowHeight - ActualSize.Height) / 2);
                if (!initial)
                {
                    Console.Clear();
                    DiffDraw.FullDraw(Color);
                }
                _resizing = false;
            }
        }
    }
}