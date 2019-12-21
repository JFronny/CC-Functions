using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CC_Functions.Misc;
using static CC_Functions.W32.Power;

namespace CC_Functions.W32.Test
{
    public partial class Form1 : Form
    {
        public static Wnd32 tmpWnd;
        public static Form1 mainF;
        public static Form frm;
        public static Label lab;
        private readonly KeyboardHook kHook;
        private Point locDelB;
        private readonly MouseHook mHook;

        private bool moving;
        private DateTime mST;

        public Form1()
        {
            InitializeComponent();
            mainF = this;
            tmpWnd = Wnd32.fromForm(this);
#if DEBUG
            tmpWnd.MakeOverlay();
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
            wnd_action_style.SelectedItem = tmpWnd.state;
        }

        public void set_up_box(ComboBox box, Type enumT)
        {
            box.DataSource = Enum.GetNames(enumT);
            var tmp = Enum.GetValues(enumT);
            box.Tag = new object[tmp.Length];
            var i = 0;
            foreach (var o in tmp)
            {
                ((object[]) box.Tag)[i] = o;
                i++;
            }
        }

        public object get_box_value(ComboBox box)
        {
            return ((object[]) box.Tag)[box.SelectedIndex];
        }

        private void Power_execute_Click(object sender, EventArgs e)
        {
            RaiseEvent((ShutdownMode) get_box_value(power_mode_box), (ShutdownReason) get_box_value(power_reason_box),
                (ShutdownMod) get_box_value(power_mod_box));
        }

        private void Wnd_select_self_Click(object sender, EventArgs e)
        {
            tmpWnd = Wnd32.fromForm(this);
        }

        private void wnd_select_list_Click(object sender, EventArgs e)
        {
            tmpWnd = SelectBox.Show(Wnd32.getVisible(), "Please select a window") ?? tmpWnd;
        }

        private void Wnd_select_title_button_Click(object sender, EventArgs e)
        {
            tmpWnd = Wnd32.fromMetadata(null, wnd_select_title_box.Text);
        }

        private void Wnd_selet_class_button_Click(object sender, EventArgs e)
        {
            tmpWnd = Wnd32.fromMetadata(wnd_select_class_box.Text);
        }

        private void Wnd_action_title_set_Click(object sender, EventArgs e)
        {
            tmpWnd.title = wnd_select_title_box.Text;
        }

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

        private void Wnd_action_enabled_CheckedChanged(object sender, EventArgs e)
        {
            tmpWnd.enabled = wnd_action_enabled.Checked;
        }

        private void Wnd_action_visible_CheckedChanged(object sender, EventArgs e)
        {
            tmpWnd.shown = wnd_action_visible.Checked;
        }

        private void Wnd_action_front_Click(object sender, EventArgs e)
        {
            tmpWnd.isForeground = true;
        }

        private void Wnd_action_overlay_Click(object sender, EventArgs e)
        {
            tmpWnd.MakeOverlay();
        }

        private void Wnd_action_destroy_Click(object sender, EventArgs e)
        {
            tmpWnd.Destroy();
        }

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
            Wnd32.fromForm(frm).MakeOverlay();
        }

        private void Frm_Click(object sender, EventArgs e)
        {
            frm.Hide();
            frm.WindowState = FormWindowState.Minimized;
            var tmp = Wnd32.fromPoint(MousePosition);
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

        private void MHook_OnMouse(MouseHookEventArgs _args)
        {
            mouse_log.Text = _args.Message + " -|- " + _args.Point + "\r\n" + mouse_log.Text;
        }

        private void Mouse_log_TextChanged(object sender, EventArgs e)
        {
            if (mouse_log.Lines.Length > 10)
            {
                var tmp = mouse_log.Lines.ToList();
                tmp.RemoveRange(9, mouse_log.Lines.Length - 9);
                mouse_log.Lines = tmp.ToArray();
                tmp = null;
            }
        }

        private void Keyboard_enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboard_enabled.Checked)
                kHook.OnKeyPress += KHook_OnKeyPress;
            else
                kHook.OnKeyPress -= KHook_OnKeyPress;
        }

        private void KHook_OnKeyPress(KeyboardHookEventArgs _args)
        {
            keyboard_log.Text = _args.Key + "\r\n" + keyboard_log.Text;
        }

        private void Keyboard_log_TextChanged(object sender, EventArgs e)
        {
            if (keyboard_log.Lines.Length > 10)
            {
                var tmp = keyboard_log.Lines.ToList();
                tmp.RemoveRange(9, keyboard_log.Lines.Length - 9);
                keyboard_log.Lines = tmp.ToArray();
                tmp = null;
            }
        }

        private void Wnd_action_pos_Click(object sender, EventArgs e)
        {
            tmpWnd.position = new Rectangle(wnd_action_pos_x_bar.Value, wnd_action_pos_y_bar.Value,
                wnd_action_pos_w_bar.Value, wnd_action_pos_h_bar.Value);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if ((DateTime.Now - mST).TotalSeconds < 0.15f)
                Application.Exit();
        }

        private void Exit_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving && (DateTime.Now - mST).TotalSeconds >= 0.1f)
                Location = new Point(locDelB.X + Cursor.Position.X, locDelB.Y + Cursor.Position.Y);
        }

        private void Exit_MouseDown(object sender, MouseEventArgs e)
        {
            mST = DateTime.Now;
            locDelB = new Point(Location.X - Cursor.Position.X, Location.Y - Cursor.Position.Y);
            moving = true;
        }

        private void Exit_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void Wnd_action_style_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(wnd_action_style.SelectedValue.ToString(), out FormWindowState status);
            tmpWnd.state = status;
        }

        private void wnd_action_overlay_CheckedChanged(object sender, EventArgs e)
        {
            tmpWnd.overlay = wnd_action_overlay.Checked;
        }
    }
}