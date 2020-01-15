using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CC_Functions.Misc;
using CC_Functions.W32.DCDrawer;
using CC_Functions.W32.Hooks;
using static CC_Functions.W32.Power;

namespace CC_Functions.W32.Test
{
    public partial class MainForm : Form
    {
        private static Wnd32 tmpWnd32_obj;
        private Wnd32 tmpWnd
        {
            get => tmpWnd32_obj;
            set {
                tmpWnd32_obj = value;
                Wnd_action_title_get_Click(null, null);
            }
        }
        private static MainForm mainF;
        private static Form frm;
        private static Label lab;
        private readonly KeyboardHook kHook;
        private readonly MouseHook mHook;
        Label[] readerLabels;

        public MainForm()
        {
            InitializeComponent();
            mainF = this;
            tmpWnd32_obj = Wnd32.fromForm(this);
#if DEBUG
            tmpWnd32_obj.MakeOverlay();
#endif
            set_up_box(power_mode_box, typeof(ShutdownMode));
            set_up_box(power_reason_box, typeof(ShutdownReason));
            set_up_box(power_mod_box, typeof(ShutdownMod));
            mHook = new MouseHook();
            kHook = new KeyboardHook();
            wnd_action_pos_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            wnd_action_pos_y_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            wnd_action_pos_w_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            wnd_action_pos_h_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            wnd_action_style.DataSource = Enum.GetValues(typeof(FormWindowState));
            wnd_action_style.SelectedItem = tmpWnd32_obj.state;
            readerLabels = Enum.GetValues(typeof(Keys)).OfType<Keys>().OrderBy(s => s.ToString()).Select(s =>
            {
                Label lab = new Label { Tag = s };
                readerFlow.Controls.Add(lab);
                return lab;
            }).ToArray();
            Wnd_action_title_get_Click(null, null);
            desk_get_Click(null, null);
            screen_get_Click(null, null);
        }

        public void set_up_box(ComboBox box, Type enumT)
        {
            box.DataSource = Enum.GetNames(enumT);
            Array tmp = Enum.GetValues(enumT);
            box.Tag = new object[tmp.Length];
            int i = 0;
            foreach (object o in tmp)
            {
                ((object[]) box.Tag)[i] = o;
                i++;
            }
        }

        public object get_box_value(ComboBox box) => ((object[]) box.Tag)[box.SelectedIndex];

        private void Power_execute_Click(object sender, EventArgs e) => RaiseEvent((ShutdownMode)get_box_value(power_mode_box), (ShutdownReason)get_box_value(power_reason_box),
                (ShutdownMod)get_box_value(power_mod_box));

        private void Wnd_select_self_Click(object sender, EventArgs e) => tmpWnd = Wnd32.fromForm(this);

        private void wnd_select_list_Click(object sender, EventArgs e) => tmpWnd = SelectBox.Show(Wnd32.Visible, "Please select a window") ?? tmpWnd;

        private void Wnd_select_title_button_Click(object sender, EventArgs e) => tmpWnd = Wnd32.fromMetadata(null, wnd_select_title_box.Text);

        private void Wnd_selet_class_button_Click(object sender, EventArgs e) => tmpWnd = Wnd32.fromMetadata(wnd_select_class_box.Text);

        private void Wnd_action_title_set_Click(object sender, EventArgs e) => tmpWnd.title = wnd_select_title_box.Text;

        private void Wnd_action_title_get_Click(object sender, EventArgs e)
        {
            if (!tmpWnd.stillExists)
                tmpWnd = Wnd32.fromForm(this);
            wnd_select_title_box.Text = tmpWnd.title;
            wnd_action_enabled.Checked = tmpWnd.enabled;
            wnd_select_selected.Text = "Selected: " + tmpWnd.hWnd;
            wnd_action_style.SelectedIndex = (int) tmpWnd.state;
            wnd_select_class_box.Text = tmpWnd.className;
            wnd_action_visible.Checked = tmpWnd.shown;
            try
            {
                wnd_action_icon.BackgroundImage = tmpWnd.icon.ToBitmap();
            }
            catch
            {
                wnd_action_icon.BackgroundImage = null;
            }

            try
            {
                wnd_action_pos_x_bar.Value = tmpWnd.position.X;
            }
            catch
            {
            }

            try
            {
                wnd_action_pos_y_bar.Value = tmpWnd.position.Y;
            }
            catch
            {
            }

            try
            {
                wnd_action_pos_w_bar.Value = tmpWnd.position.Width;
            }
            catch
            {
            }

            try
            {
                wnd_action_pos_h_bar.Value = tmpWnd.position.Height;
            }
            catch
            {
            }
        }

        private void Wnd_action_enabled_CheckedChanged(object sender, EventArgs e) =>
            tmpWnd.enabled = wnd_action_enabled.Checked;

        private void Wnd_action_visible_CheckedChanged(object sender, EventArgs e) =>
            tmpWnd.shown = wnd_action_visible.Checked;

        private void Wnd_action_front_Click(object sender, EventArgs e) => tmpWnd.isForeground = true;

        private void Wnd_action_destroy_Click(object sender, EventArgs e) => tmpWnd.Destroy();

        private void Wnd_select_mouse_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            tmpWnd = Wnd32.fromForm(this);
            tmpWnd.enabled = false;
            frm = new Form();
            frm.BackColor = Color.White;
            frm.Opacity = 0.4f;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.WindowState = FormWindowState.Maximized;
            frm.Click += Frm_Click;
            frm.MouseMove += Frm_MouseMove;
            lab = new Label();
            frm.Controls.Add(lab);
            frm.Show();
            Wnd32.fromForm(frm).overlay = true;
        }

        private void Frm_Click(object sender, EventArgs e)
        {
            frm.Hide();
            frm.WindowState = FormWindowState.Minimized;
            Wnd32 tmp = Wnd32.fromPoint(MousePosition);
            tmpWnd.enabled = true;
            tmpWnd.isForeground = true;
            tmpWnd = tmp;
            mainF.WindowState = FormWindowState.Normal;
            frm.Close();
        }

        private void Frm_MouseMove(object sender, MouseEventArgs e)
        {
            lab.Text = Wnd32.fromPoint(MousePosition).ToString();
            lab.Location = new Point(Cursor.Position.X + 5, Cursor.Position.Y + 5);
        }

        private void Mouse_enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (mouse_enabled.Checked)
                mHook.OnMouse += MHook_OnMouse;
            else
                mHook.OnMouse -= MHook_OnMouse;
        }

        private void MHook_OnMouse(MouseHookEventArgs args) =>
            mouse_log.Text = args.Message + " -|- " + args.Point + "\r\n" + mouse_log.Text;

        private void Mouse_log_TextChanged(object sender, EventArgs e) => mouse_log.Lines = mouse_log.Lines.Take(9).ToArray();

        private void Keyboard_enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboard_enabled.Checked)
                kHook.OnKeyPress += KHook_OnKeyPress;
            else
                kHook.OnKeyPress -= KHook_OnKeyPress;
        }

        private void KHook_OnKeyPress(KeyboardHookEventArgs args) =>
            keyboard_log.Text = args.Key + "\r\n" + keyboard_log.Text;

        private void Keyboard_log_TextChanged(object sender, EventArgs e) => keyboard_log.Lines = keyboard_log.Lines.Take(8).ToArray();

        private void Wnd_action_pos_Click(object sender, EventArgs e) =>
            tmpWnd.position = new Rectangle(wnd_action_pos_x_bar.Value, wnd_action_pos_y_bar.Value,
                wnd_action_pos_w_bar.Value, wnd_action_pos_h_bar.Value);

        private void Wnd_action_style_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(wnd_action_style.SelectedValue.ToString(), out FormWindowState status);
            tmpWnd.state = status;
        }

        private void wnd_action_overlay_CheckedChanged(object sender, EventArgs e) =>
            tmpWnd.overlay = wnd_action_overlay.Checked;

        private void readerUpdate_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < readerLabels.Length; i++)
            {
                Label s = readerLabels[i];
                Keys key = (Keys)s.Tag;
                s.Text = $"{key.ToString()}: {key.IsDown()}";
            }
        }

        private void desk_get_Click(object sender, EventArgs e) => desk_back.BackgroundImage = DeskMan.Wallpaper;

        private void desk_draw_Click(object sender, EventArgs e)
        {
            using IDCDrawer drawer = DeskMan.CreateGraphics();
            Graphics g = drawer.Graphics;
            Pen eye = new Pen(new SolidBrush(Color.Red), 2);
            g.DrawCurve(eye, new PointF[] { makePoint(20, 50), makePoint(50, 65), makePoint(80, 50) });
            g.DrawCurve(eye, new PointF[] { makePoint(20, 50), makePoint(50, 35), makePoint(80, 50) });
            g.DrawEllipse(eye, new RectangleF(PointF.Subtract(makePoint(50, 50), makeSizeY(15, 15)), makeSizeY(30, 30)));
        }

        public static PointF makePoint(float xPercent, float yPercent) => new PointF(Screen.PrimaryScreen.Bounds.Width * xPercent / 100,
            Screen.PrimaryScreen.Bounds.Height * yPercent / 100);

        public static SizeF makeSizeY(float xPercent, float yPercent) => new SizeF(Screen.PrimaryScreen.Bounds.Height * xPercent / 100,
            Screen.PrimaryScreen.Bounds.Height * yPercent / 100);

        private void desk_set_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DeskMan.Wallpaper = Image.FromFile(dlg.FileName);
            }
        }

        private void screen_get_Click(object sender, EventArgs e) => screen_img.BackgroundImage = ScreenMan.CaptureScreen();

        private void screen_draw_Click(object sender, EventArgs e)
        {
            using IDCDrawer drawer = ScreenMan.GetDrawer(false);
            Graphics g = drawer.Graphics;
            Pen eye = new Pen(new SolidBrush(Color.Red), 2);
            g.DrawCurve(eye, new PointF[] { makePoint(20, 50), makePoint(50, 65), makePoint(80, 50) });
            g.DrawCurve(eye, new PointF[] { makePoint(20, 50), makePoint(50, 35), makePoint(80, 50) });
            g.DrawEllipse(eye, new RectangleF(PointF.Subtract(makePoint(50, 50), makeSizeY(15, 15)), makeSizeY(30, 30)));
        }
    }
}