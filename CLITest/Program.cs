using System;
using System.Drawing;
using System.Threading;
using CC_Functions.Commandline.TUI;

namespace CLITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Screen screen = new Screen(40, 20);
            Button btn1 = new Button("Test")
            {
                Point = new Point(2, 0)
            };
            screen.Controls.Add(btn1);
            btn1.Click += (screen1, eventArgs) =>
            {
                DiffDraw.FullDraw(true);
            };
            Label lab1 = new Label("Meem")
            {
                Point = new Point(2, 1)
            };
            screen.Controls.Add(lab1);
            screen.Controls.Add(new Label("Saas\nSoos")
            {
                Point = new Point(2, 2)
            });
            Button btn2 = new Button("X");
            screen.Controls.Add(btn2);
            
            CheckBox box = new CheckBox("Are u gae?")
            {
                Point = new Point(2, 3)
            };
            screen.Controls.Add(box);
            box.CheckedChanged += (screen1, eventArgs) =>
            {
                lab1.Content = box.Checked ? "Sas" : "Meem";
            };

            TextBox tbox = new TextBox("Hello\nWorld1\n\nHow are u?")
            {
                Size = new Size(20, 10),
                Point = new Point(0, 6)
            };
            screen.Controls.Add(tbox);

            Slider slider = new Slider
            {
                Point = new Point(2, 4),
                Size = new Size(8, 2),
                MaxValue = 75,
                StepSize = 14,
                MinValue = -3,
                Value = 7
            };
            screen.Controls.Add(slider);
            
            bool visible = true;
            btn2.Click += (screen1, eventArgs) => visible = false;
            screen.Close += (screen1, eventArgs) => visible = false;
            screen.Render();
            while (visible)
            {
                Thread.Sleep(50);
                screen.ReadInput();
            }
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Bye");
        }
    }
}