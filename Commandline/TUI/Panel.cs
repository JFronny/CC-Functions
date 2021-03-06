using System.Collections.Generic;
using System.Linq;
using CC_Functions.Misc;

namespace CC_Functions.Commandline.TUI
{
    /// <summary>
    ///     A panel containing other components. MUST be inherited for all other controls that contain others
    /// </summary>
    public class Panel : Control
    {
        /// <summary>
        /// Enable to draw a simple border around this control
        /// </summary>
        public bool Border = true;
        /// <summary>
        ///     The controls inside this panel
        /// </summary>
        public List<Control> Controls = new List<Control>();

        /// <inheritdoc />
        public override bool Selectable { get; } = false;

        /// <summary>
        ///     Renders the control and all contained controls
        /// </summary>
        /// <returns>The rendered pixels</returns>
        public override Pixel[,] Render()
        {
            Pixel[,] tmp = new Pixel[Size.Height, Size.Width];
            tmp.Populate(new Pixel(BackColor, ForeColor, SpecialChars.Empty));
            if (Border)
            {
                for (int i = 0; i < Size.Width; i++) tmp[Size.Height - 1, i] = new Pixel(BackColor, ForeColor, SpecialChars.OneLineSimple.LeftRight);
                for (int i = 0; i < Size.Height; i++) tmp[i, Size.Width - 1] = new Pixel(BackColor, ForeColor, SpecialChars.OneLineSimple.UpDown);
                tmp[Size.Height - 1, Size.Width - 1] = new Pixel(BackColor, ForeColor, '┘');
            }
            foreach (Control control in Controls)
                if (control.Visible)
                {
                    Pixel[,] render = control.Render();
                    render.CopyTo(tmp, control.Point);
                }
            return tmp;
        }

        /// <summary>
        ///     Recursively enumerates all controls
        /// </summary>
        /// <returns>A list of all controls</returns>
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