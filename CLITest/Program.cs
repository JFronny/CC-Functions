using System;
using System.Drawing;
using System.Threading;
using CC_Functions.Commandline.TUI;

namespace CLITest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            CenteredScreen cScreen = new CenteredScreen(40, 20, ConsoleColor.Green);
            Panel screen = cScreen.ContentPanel;
            Button btn1 = new Button("Test")
            {
                Point = new Point(2, 0),
                BackColor = ConsoleColor.DarkGreen
            };
            screen.Controls.Add(btn1);
            btn1.Click += (screen1, eventArgs) => { DiffDraw.FullDraw(true); };
            Label lab1 = new Label("Meem")
            {
                Point = new Point(2, 1),
                BackColor = ConsoleColor.Green
            };
            screen.Controls.Add(lab1);
            screen.Controls.Add(new Label("Saas\nSoos")
            {
                Point = new Point(2, 2),
                BackColor = ConsoleColor.Green
            });
            Button btn2 = new Button("X")
            {
                BackColor = ConsoleColor.Red,
                ForeColor = ConsoleColor.White
            };
            screen.Controls.Add(btn2);

            CheckBox box = new CheckBox("Are u gae?")
            {
                Point = new Point(2, 3),
                BackColor = ConsoleColor.DarkGreen
            };
            screen.Controls.Add(box);
            box.CheckedChanged += (screen1, eventArgs) => { lab1.Content = box.Checked ? "Sas" : "Meem"; };

            TextBox tbox = new TextBox("Hello\nWorld1\n\nHow are u?")
            {
                Size = new Size(20, 10),
                Point = new Point(0, 6),
                BackColor = ConsoleColor.DarkYellow
            };
            screen.Controls.Add(tbox);

            Slider slider = new Slider
            {
                Point = new Point(2, 4),
                Size = new Size(16, 2),
                MaxValue = 75,
                StepSize = 14,
                MinValue = -3,
                Value = 7,
                BackColor = ConsoleColor.Magenta
            };
            screen.Controls.Add(slider);

            bool visible = true;
            btn2.Click += (screen1, eventArgs) => visible = false;
            cScreen.Close += (screen1, eventArgs) => visible = false;
            cScreen.TabChanged += (screen1, eventArgs) => btn1.Content = $"Test {cScreen.TabPoint}";
            cScreen.Render();
            while (visible)
            {
                Thread.Sleep(50);
                cScreen.ReadInput();
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Bye");
        }
    }
}