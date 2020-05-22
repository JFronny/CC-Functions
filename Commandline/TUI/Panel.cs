using System.Collections.Generic;
using System.Linq;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    public class Panel : Control
    {
        public List<Control> Controls = new List<Control>();
        
        public override Pixel[,] Render()
        {
            Pixel[,] tmp = new Pixel[Size.Height, Size.Width];
            tmp.Populate(new Pixel(BackColor, ForeColor, SpecialChars.empty));
            foreach (Control control in Controls)
            {
                if (control.Visible)
                {
                    Pixel[,] render = control.Render();
                    render.CopyTo(tmp, control.Point);
                }
            }
            return tmp;
        }

        protected override void Resize(int width, int height)
        {
        }

        public override bool Selectable { get; } = false;

        public Control[] EnumerateRecursive()
        {
            List<Control> output = Controls.ToList();
            int i = 0;
            while (i < output.Count)
            {
                if (output[i] is Panel p) output.AddRange(p.EnumerateRecursive().Where(s => !output.Contains(s)));
                i++;
            }
            return output.ToArray();
        }
    }
}
