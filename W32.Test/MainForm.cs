using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CC_Functions.W32.DCDrawer;
using CC_Functions.W32.Forms;
using CC_Functions.W32.Hooks;
using static CC_Functions.W32.Power;

namespace CC_Functions.W32.Test
{
    public partial class MainForm : Form
    {
        private static Wnd32 _tmpWnd32Obj;
        private static MainForm _mainF;

        private static Wnd32 _wndSelectMouseCurrent;
        private readonly KeyboardHook _kHook;
        private readonly MouseHook _mHook;
        private readonly Label[] _readerLabels;

        public MainForm()
        {
            _wndSelectMouseCurrent = this.GetWnd32();
            InitializeComponent();
            _mainF = this;
            _tmpWnd32Obj = Wnd32.FromForm(this);
#if DEBUG
            _tmpWnd32Obj.Overlay = true;
#endif
            set_up_box(power_mode_box, typeof(ShutdownMode));
            set_up_box(power_reason_box, typeof(ShutdownReason));
            set_up_box(power_mod_box, typeof(ShutdownMod));
            _mHook = new MouseHook();
            _kHook = new KeyboardHook();
            wnd_action_pos_x_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            wnd_action_pos_y_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            wnd_action_pos_w_bar.Maximum = Screen.PrimaryScreen.Bounds.Width;
            wnd_action_pos_h_bar.Maximum = Screen.PrimaryScreen.Bounds.Height;
            wnd_action_style.DataSource = Enum.GetValues(typeof(FormWindowState));
            wnd_action_style.SelectedItem = _tmpWnd32Obj.State;
            _readerLabels = Enum.GetValues(typeof(Keys)).OfType<Keys>().OrderBy(s => s.ToString()).Select(s =>
            {
                Label lab = new Label {Tag = s};
                readerFlow.Controls.Add(lab);
                return lab;
            }).ToArray();
            Wnd_action_title_get_Click(null, null);
            desk_get_Click(null, null);
            screen_get_Click(null, null);
            time_select.Value = DateTime.Now;
        }

        private Wnd32 TmpWnd
        {
            get => _tmpWnd32Obj;
            set
            {
                _tmpWnd32Obj = value;
                Wnd_action_title_get_Click(null, null);
            }
        }

        private static void set_up_box(ComboBox box, Type enumT)
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

        private static object get_box_value(ListControl box) => ((object[]) box.Tag)[box.SelectedIndex];

        private void Power_execute_Click(object sender, EventArgs e) => RaiseEvent(
            (ShutdownMode) get_box_value(power_mode_box), (ShutdownReason) get_box_value(power_reason_box),
            (ShutdownMod) get_box_value(power_mod_box));

        private void Wnd_select_self_Click(object sender, EventArgs e) => TmpWnd = Wnd32.FromForm(this);

        private void wnd_select_list_Click(object sender, EventArgs e) =>
            TmpWnd = SelectBox.Show(Wnd32.Visible, "Please select a window") ?? TmpWnd;

        private void wnd_select_parent_Click(object sender, EventArgs e) => TmpWnd = TmpWnd.Parent;

        private void wnd_select_child_Click(object sender, EventArgs e)
        {
            if (TmpWnd.HWnd != Handle && !this.GetWnd32().Children.Contains(TmpWnd))
            {
                WindowState = FormWindowState.Minimized;
                TmpWnd.Enabled = false;
            }
            Wnd32.FromForm(this).IsForeground = true;
            Form frm = new Form
            {
                BackColor = Color.White,
                Opacity = 0.4f,
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized
            };
            Label lab = new Label {AutoSize = true};
            frm.Controls.Add(lab);
            Panel pan = new Panel {BackColor = Color.Red};
            frm.Controls.Add(pan);
            Wnd32[] children = TmpWnd.Children;

            void UpdateGui(Point labelPosition)
            {
                lab.Text = $"{_wndSelectMouseCurrent.Title} ({_wndSelectMouseCurrent.HWnd})";
                lab.Location = new Point(labelPosition.X + 5, labelPosition.Y + 5);
                pan.Bounds = _wndSelectMouseCurrent.Position;
            }

            void MouseEventHandler(object sender1, MouseEventArgs e1)
            {
                Func<Wnd32, bool> checkWnd = s => s.Position.Contains(e1.Location);
                if (children.Any(checkWnd))
                {
                    _wndSelectMouseCurrent = children.First(checkWnd);
                    UpdateGui(Cursor.Position);
                }
            }

            void EventHandler(object sender1, EventArgs e1)
            {
                frm.Hide();
                frm.WindowState = FormWindowState.Minimized;
                Wnd32 tmp = _wndSelectMouseCurrent;
                TmpWnd.Enabled = true;
                TmpWnd.IsForeground = true;
                TmpWnd = tmp;
                _mainF.WindowState = FormWindowState.Normal;
                frm.Close();
            }

            void KeyEventHandler(object sender1, KeyEventArgs e1)
            {
                int tmp;
                switch (e1.KeyCode)
                {
                    case Keys.Escape:
                        frm.Close();
                        break;
                    case Keys.Up:
                    case Keys.Left:
                        tmp = Array.IndexOf(children, _wndSelectMouseCurrent);
                        if (tmp == 0)
                            tmp = children.Length;
                        tmp--;
                        _wndSelectMouseCurrent = children[tmp];
                        UpdateGui(_wndSelectMouseCurrent.Position.Location);
                        break;
                    case Keys.Down:
                    case Keys.Right:
                        tmp = Array.IndexOf(children, _wndSelectMouseCurrent);
                        tmp++;
                        if (tmp == children.Length)
                            tmp = 0;
                        _wndSelectMouseCurrent = children[tmp];
                        UpdateGui(_wndSelectMouseCurrent.Position.Location);
                        break;
                }
            }

            frm.Click += EventHandler;
            pan.Click += EventHandler;
            lab.Click += EventHandler;
            frm.MouseMove += MouseEventHandler;
            pan.MouseMove += MouseEventHandler;
            lab.MouseMove += MouseEventHandler;
            frm.KeyDown += KeyEventHandler;
            UpdateGui(Cursor.Position);
            frm.Show();
            _wndSelectMouseCurrent = frm.GetWnd32();
            Cursor.Position = Cursor.Position;
            frm.GetWnd32().Overlay = true;
        }

        private void Wnd_select_title_button_Click(object sender, EventArgs e) =>
            TmpWnd = Wnd32.FromMetadata(null, wnd_select_title_box.Text);

        private void Wnd_select_class_button_Click(object sender, EventArgs e) =>
            TmpWnd = Wnd32.FromMetadata(wnd_select_class_box.Text);

        private void Wnd_action_title_set_Click(object sender, EventArgs e) => TmpWnd.Title = wnd_select_title_box.Text;

        private void Wnd_action_title_get_Click(object sender, EventArgs e)
        {
            if (!TmpWnd.StillExists)
                TmpWnd = Wnd32.FromForm(this);
            wnd_select_title_box.Text = TmpWnd.Title;
            wnd_action_enabled.Checked = TmpWnd.Enabled;
            wnd_select_selected.Text = $"Selected: {TmpWnd.HWnd}";
            wnd_action_style.SelectedIndex = (int) TmpWnd.State;
            wnd_select_class_box.Text = TmpWnd.ClassName;
            wnd_action_visible.Checked = TmpWnd.Shown;
            try
            {
                wnd_action_icon.BackgroundImage = TmpWnd.Icon.ToBitmap();
            }
            catch
            {
                wnd_action_icon.BackgroundImage = null;
            }

            try
            {
                wnd_action_pos_x_bar.Value = TmpWnd.Position.X;
            }
            catch
            {
                // ignored
            }

            try
            {
                wnd_action_pos_y_bar.Value = TmpWnd.Position.Y;
            }
            catch
            {
                // ignored
            }

            try
            {
                wnd_action_pos_w_bar.Value = TmpWnd.Position.Width;
            }
            catch
            {
                // ignored
            }

            try
            {
                wnd_action_pos_h_bar.Value = TmpWnd.Position.Height;
            }
            catch
            {
                // ignored
            }

            try
            {
                wnd_select_parent.Tag = TmpWnd.Parent;
                wnd_select_parent.Enabled = true;
            }
            catch
            {
                wnd_select_parent.Enabled = false;
            }
        }

        private void Wnd_action_enabled_CheckedChanged(object sender, EventArgs e) =>
            TmpWnd.Enabled = wnd_action_enabled.Checked;

        private void Wnd_action_visible_CheckedChanged(object sender, EventArgs e) =>
            TmpWnd.Shown = wnd_action_visible.Checked;

        private void Wnd_action_front_Click(object sender, EventArgs e) => TmpWnd.IsForeground = true;

        private void Wnd_action_destroy_Click(object sender, EventArgs e) => TmpWnd.Destroy();

        private void Wnd_select_mouse_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            TmpWnd = Wnd32.FromForm(this);
            TmpWnd.Enabled = false;
            Form frm = new Form
            {
                BackColor = Color.White,
                Opacity = 0.4f,
                FormBorderStyle = FormBorderStyle.None,
                WindowState = FormWindowState.Maximized
            };
            Label lab = new Label {AutoSize = true};
            frm.Controls.Add(lab);
            Panel pan = new Panel {BackColor = Color.Red};
            frm.Controls.Add(pan);

            void UpdateGui(Point labelPosition)
            {
                lab.Text = $"{_wndSelectMouseCurrent.Title} ({_wndSelectMouseCurrent.HWnd})";
                lab.Location = new Point(labelPosition.X + 5, labelPosition.Y + 5);
                pan.Bounds = _wndSelectMouseCurrent.Position;
            }

            void MouseEventHandler(object sender1, MouseEventArgs e1)
            {
                _wndSelectMouseCurrent = Wnd32.AllFromPoint(MousePosition, true).First(s => s != frm.GetWnd32());
                UpdateGui(Cursor.Position);
            }

            void EventHandler(object sender1, EventArgs e1)
            {
                frm.Hide();
                frm.WindowState = FormWindowState.Minimized;
                Wnd32 tmp = _wndSelectMouseCurrent;
                TmpWnd.Enabled = true;
                TmpWnd.IsForeground = true;
                TmpWnd = tmp;
                _mainF.WindowState = FormWindowState.Normal;
                frm.Close();
            }

            void KeyEventHandler(object sender1, KeyEventArgs e1)
            {
                if (e1.KeyCode == Keys.Escape)
                    frm.Close();
            }

            frm.Click += EventHandler;
            pan.Click += EventHandler;
            lab.Click += EventHandler;
            frm.MouseMove += MouseEventHandler;
            pan.MouseMove += MouseEventHandler;
            lab.MouseMove += MouseEventHandler;
            frm.KeyDown += KeyEventHandler;

            frm.Show();
            _wndSelectMouseCurrent = frm.GetWnd32();
            Cursor.Position = Cursor.Position;
            frm.GetWnd32().Overlay = true;
        }

        private void Mouse_enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (mouse_enabled.Checked)
                _mHook.OnMouse += MHook_OnMouse;
            else
                _mHook.OnMouse -= MHook_OnMouse;
        }

        private void MHook_OnMouse(MouseHookEventArgs args) =>
            mouse_log.Text = $"{args.Message} -|- {args.Point}\r\n{mouse_log.Text}";

        private void Mouse_log_TextChanged(object sender, EventArgs e) =>
            mouse_log.Lines = mouse_log.Lines.Take(9).ToArray();

        private void Keyboard_enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (keyboard_enabled.Checked)
                _kHook.OnKeyPress += KHook_OnKeyPress;
            else
                _kHook.OnKeyPress -= KHook_OnKeyPress;
        }

        private void KHook_OnKeyPress(KeyboardHookEventArgs args) =>
            keyboard_log.Text = $"{args.Key}\r\n{keyboard_log.Text}";

        private void Keyboard_log_TextChanged(object sender, EventArgs e) =>
            keyboard_log.Lines = keyboard_log.Lines.Take(8).ToArray();

        private void Wnd_action_pos_Click(object sender, EventArgs e) =>
            TmpWnd.Position = new Rectangle(wnd_action_pos_x_bar.Value, wnd_action_pos_y_bar.Value,
                wnd_action_pos_w_bar.Value, wnd_action_pos_h_bar.Value);

        private void Wnd_action_style_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(wnd_action_style.SelectedValue.ToString(), out FormWindowState status);
            TmpWnd.State = status;
        }

        private void wnd_action_overlay_CheckedChanged(object sender, EventArgs e) =>
            TmpWnd.Overlay = wnd_action_overlay.Checked;

        private void readerUpdate_Tick(object sender, EventArgs e)
        {
            foreach (Label s in _readerLabels)
            {
                Keys key = (Keys) s.Tag;
                s.Text = $"{key.ToString()}: {key.IsDown()}";
            }
        }

        private void desk_get_Click(object sender, EventArgs e) => desk_back.BackgroundImage = DeskMan.Wallpaper;

        private void desk_draw_Click(object sender, EventArgs e)
        {
            using IDCDrawer drawer = DeskMan.CreateGraphics();
            Graphics g = drawer.Graphics;
            Pen eye = new Pen(new SolidBrush(Color.Red), 2);
            g.DrawCurve(eye, new[] {MakePoint(20, 50), MakePoint(50, 65), MakePoint(80, 50)});
            g.DrawCurve(eye, new[] {MakePoint(20, 50), MakePoint(50, 35), MakePoint(80, 50)});
            g.DrawEllipse(eye,
                new RectangleF(PointF.Subtract(MakePoint(50, 50), MakeSizeY(15, 15)), MakeSizeY(30, 30)));
        }

        private static PointF MakePoint(float xPercent, float yPercent) => new PointF(
            Screen.PrimaryScreen.Bounds.Width * xPercent / 100,
            Screen.PrimaryScreen.Bounds.Height * yPercent / 100);

        private static SizeF MakeSizeY(float xPercent, float yPercent) => new SizeF(
            Screen.PrimaryScreen.Bounds.Height * xPercent / 100,
            Screen.PrimaryScreen.Bounds.Height * yPercent / 100);

        private void desk_set_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";
            if (dlg.ShowDialog() == DialogResult.OK) DeskMan.Wallpaper = Image.FromFile(dlg.FileName);
        }

        private void screen_get_Click(object sender, EventArgs e) =>
            screen_img.BackgroundImage = ScreenMan.CaptureScreen();

        private void screen_draw_Click(object sender, EventArgs e)
        {
            using IDCDrawer drawer = ScreenMan.GetDrawer(false);
            Graphics g = drawer.Graphics;
            Pen eye = new Pen(new SolidBrush(Color.Red), 2);
            g.DrawCurve(eye, new[] {MakePoint(20, 50), MakePoint(50, 65), MakePoint(80, 50)});
            g.DrawCurve(eye, new[] {MakePoint(20, 50), MakePoint(50, 35), MakePoint(80, 50)});
            g.DrawEllipse(eye,
                new RectangleF(PointF.Subtract(MakePoint(50, 50), MakeSizeY(15, 15)), MakeSizeY(30, 30)));
        }

        private void time_set_Click(object sender, EventArgs e) => Time.Set(time_select.Value);
    }
}