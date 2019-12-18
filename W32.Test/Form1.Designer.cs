namespace CC_Functions.W32.Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.exit = new System.Windows.Forms.Button();
            this.keyboard_log = new System.Windows.Forms.TextBox();
            this.keyboard = new System.Windows.Forms.GroupBox();
            this.keyboard_enabled = new System.Windows.Forms.CheckBox();
            this.mouse_log = new System.Windows.Forms.TextBox();
            this.mouse_enabled = new System.Windows.Forms.CheckBox();
            this.wnd_action_pos_h_label = new System.Windows.Forms.Label();
            this.wnd_action_pos_w_label = new System.Windows.Forms.Label();
            this.wnd_action_pos_h_bar = new System.Windows.Forms.TrackBar();
            this.wnd_action_pos_w_bar = new System.Windows.Forms.TrackBar();
            this.wnd_action_pos_y_label = new System.Windows.Forms.Label();
            this.wnd_action_pos_x_label = new System.Windows.Forms.Label();
            this.wnd_action_pos_y_bar = new System.Windows.Forms.TrackBar();
            this.wnd_action_pos_x_bar = new System.Windows.Forms.TrackBar();
            this.wnd_action_pos = new System.Windows.Forms.Button();
            this.mouse = new System.Windows.Forms.GroupBox();
            this.power = new System.Windows.Forms.GroupBox();
            this.power_execute = new System.Windows.Forms.Button();
            this.power_reason_label = new System.Windows.Forms.Label();
            this.power_mod_label = new System.Windows.Forms.Label();
            this.power_mod_box = new System.Windows.Forms.ComboBox();
            this.power_reason_box = new System.Windows.Forms.ComboBox();
            this.power_mode_label = new System.Windows.Forms.Label();
            this.power_mode_box = new System.Windows.Forms.ComboBox();
            this.wnd_select_mouse = new System.Windows.Forms.Button();
            this.wnd_action_destroy = new System.Windows.Forms.Button();
            this.wnd_action_front = new System.Windows.Forms.Button();
            this.wnd_action_enabled = new System.Windows.Forms.CheckBox();
            this.wnd_action_title_get = new System.Windows.Forms.Button();
            this.wnd_action_title_set = new System.Windows.Forms.Button();
            this.wnd_select_title_box = new System.Windows.Forms.TextBox();
            this.wnd_selet_class_button = new System.Windows.Forms.Button();
            this.wnd_select_title_button = new System.Windows.Forms.Button();
            this.wnd_select_selected = new System.Windows.Forms.Label();
            this.wnd_select_self = new System.Windows.Forms.Button();
            this.wnd = new System.Windows.Forms.GroupBox();
            this.wnd_action_overlay = new System.Windows.Forms.CheckBox();
            this.wnd_action_style = new System.Windows.Forms.ComboBox();
            this.wnd_action_visible = new System.Windows.Forms.CheckBox();
            this.wnd_action_icon = new System.Windows.Forms.Panel();
            this.wnd_select_class_box = new System.Windows.Forms.TextBox();
            this.wnd_select_list = new System.Windows.Forms.Button();
            this.keyboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_h_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_w_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_y_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_x_bar)).BeginInit();
            this.mouse.SuspendLayout();
            this.power.SuspendLayout();
            this.wnd.SuspendLayout();
            this.SuspendLayout();
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Black;
            this.exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.ForeColor = System.Drawing.Color.White;
            this.exit.Location = new System.Drawing.Point(264, 117);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(35, 37);
            this.exit.TabIndex = 9;
            this.exit.Text = "°";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.Exit_Click);
            this.exit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Exit_MouseDown);
            this.exit.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Exit_MouseMove);
            this.exit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Exit_MouseUp);
            // 
            // keyboard_log
            // 
            this.keyboard_log.Location = new System.Drawing.Point(6, 42);
            this.keyboard_log.Multiline = true;
            this.keyboard_log.Name = "keyboard_log";
            this.keyboard_log.ReadOnly = true;
            this.keyboard_log.Size = new System.Drawing.Size(286, 150);
            this.keyboard_log.TabIndex = 2;
            this.keyboard_log.TextChanged += new System.EventHandler(this.Keyboard_log_TextChanged);
            // 
            // keyboard
            // 
            this.keyboard.Controls.Add(this.keyboard_log);
            this.keyboard.Controls.Add(this.keyboard_enabled);
            this.keyboard.Location = new System.Drawing.Point(264, 160);
            this.keyboard.Name = "keyboard";
            this.keyboard.Size = new System.Drawing.Size(298, 198);
            this.keyboard.TabIndex = 8;
            this.keyboard.TabStop = false;
            this.keyboard.Text = "CC-Functions.W32.KeyboardHook";
            // 
            // keyboard_enabled
            // 
            this.keyboard_enabled.AutoSize = true;
            this.keyboard_enabled.Location = new System.Drawing.Point(6, 19);
            this.keyboard_enabled.Name = "keyboard_enabled";
            this.keyboard_enabled.Size = new System.Drawing.Size(65, 17);
            this.keyboard_enabled.TabIndex = 0;
            this.keyboard_enabled.Text = "Enabled";
            this.keyboard_enabled.UseVisualStyleBackColor = true;
            this.keyboard_enabled.CheckedChanged += new System.EventHandler(this.Keyboard_enabled_CheckedChanged);
            // 
            // mouse_log
            // 
            this.mouse_log.Location = new System.Drawing.Point(6, 42);
            this.mouse_log.Multiline = true;
            this.mouse_log.Name = "mouse_log";
            this.mouse_log.ReadOnly = true;
            this.mouse_log.Size = new System.Drawing.Size(245, 95);
            this.mouse_log.TabIndex = 1;
            this.mouse_log.TextChanged += new System.EventHandler(this.Mouse_log_TextChanged);
            // 
            // mouse_enabled
            // 
            this.mouse_enabled.AutoSize = true;
            this.mouse_enabled.Location = new System.Drawing.Point(6, 19);
            this.mouse_enabled.Name = "mouse_enabled";
            this.mouse_enabled.Size = new System.Drawing.Size(65, 17);
            this.mouse_enabled.TabIndex = 0;
            this.mouse_enabled.Text = "Enabled";
            this.mouse_enabled.UseVisualStyleBackColor = true;
            this.mouse_enabled.CheckedChanged += new System.EventHandler(this.Mouse_enabled_CheckedChanged);
            // 
            // wnd_action_pos_h_label
            // 
            this.wnd_action_pos_h_label.AutoSize = true;
            this.wnd_action_pos_h_label.Location = new System.Drawing.Point(116, 210);
            this.wnd_action_pos_h_label.Name = "wnd_action_pos_h_label";
            this.wnd_action_pos_h_label.Size = new System.Drawing.Size(18, 13);
            this.wnd_action_pos_h_label.TabIndex = 19;
            this.wnd_action_pos_h_label.Text = "H:";
            // 
            // wnd_action_pos_w_label
            // 
            this.wnd_action_pos_w_label.AutoSize = true;
            this.wnd_action_pos_w_label.Location = new System.Drawing.Point(116, 186);
            this.wnd_action_pos_w_label.Name = "wnd_action_pos_w_label";
            this.wnd_action_pos_w_label.Size = new System.Drawing.Size(21, 13);
            this.wnd_action_pos_w_label.TabIndex = 18;
            this.wnd_action_pos_w_label.Text = "W:";
            // 
            // wnd_action_pos_h_bar
            // 
            this.wnd_action_pos_h_bar.Location = new System.Drawing.Point(136, 210);
            this.wnd_action_pos_h_bar.Name = "wnd_action_pos_h_bar";
            this.wnd_action_pos_h_bar.Size = new System.Drawing.Size(104, 45);
            this.wnd_action_pos_h_bar.TabIndex = 21;
            // 
            // wnd_action_pos_w_bar
            // 
            this.wnd_action_pos_w_bar.Location = new System.Drawing.Point(136, 186);
            this.wnd_action_pos_w_bar.Name = "wnd_action_pos_w_bar";
            this.wnd_action_pos_w_bar.Size = new System.Drawing.Size(104, 45);
            this.wnd_action_pos_w_bar.TabIndex = 20;
            // 
            // wnd_action_pos_y_label
            // 
            this.wnd_action_pos_y_label.AutoSize = true;
            this.wnd_action_pos_y_label.Location = new System.Drawing.Point(6, 210);
            this.wnd_action_pos_y_label.Name = "wnd_action_pos_y_label";
            this.wnd_action_pos_y_label.Size = new System.Drawing.Size(17, 13);
            this.wnd_action_pos_y_label.TabIndex = 15;
            this.wnd_action_pos_y_label.Text = "Y:";
            // 
            // wnd_action_pos_x_label
            // 
            this.wnd_action_pos_x_label.AutoSize = true;
            this.wnd_action_pos_x_label.Location = new System.Drawing.Point(6, 186);
            this.wnd_action_pos_x_label.Name = "wnd_action_pos_x_label";
            this.wnd_action_pos_x_label.Size = new System.Drawing.Size(17, 13);
            this.wnd_action_pos_x_label.TabIndex = 13;
            this.wnd_action_pos_x_label.Text = "X:";
            // 
            // wnd_action_pos_y_bar
            // 
            this.wnd_action_pos_y_bar.Location = new System.Drawing.Point(17, 210);
            this.wnd_action_pos_y_bar.Name = "wnd_action_pos_y_bar";
            this.wnd_action_pos_y_bar.Size = new System.Drawing.Size(104, 45);
            this.wnd_action_pos_y_bar.TabIndex = 17;
            // 
            // wnd_action_pos_x_bar
            // 
            this.wnd_action_pos_x_bar.Location = new System.Drawing.Point(17, 186);
            this.wnd_action_pos_x_bar.Name = "wnd_action_pos_x_bar";
            this.wnd_action_pos_x_bar.Size = new System.Drawing.Size(104, 45);
            this.wnd_action_pos_x_bar.TabIndex = 16;
            // 
            // wnd_action_pos
            // 
            this.wnd_action_pos.Location = new System.Drawing.Point(87, 160);
            this.wnd_action_pos.Name = "wnd_action_pos";
            this.wnd_action_pos.Size = new System.Drawing.Size(75, 23);
            this.wnd_action_pos.TabIndex = 14;
            this.wnd_action_pos.Text = "Set Position";
            this.wnd_action_pos.UseVisualStyleBackColor = true;
            this.wnd_action_pos.Click += new System.EventHandler(this.Wnd_action_pos_Click);
            // 
            // mouse
            // 
            this.mouse.Controls.Add(this.mouse_log);
            this.mouse.Controls.Add(this.mouse_enabled);
            this.mouse.Location = new System.Drawing.Point(305, 11);
            this.mouse.Name = "mouse";
            this.mouse.Size = new System.Drawing.Size(257, 143);
            this.mouse.TabIndex = 7;
            this.mouse.TabStop = false;
            this.mouse.Text = "CC-Functions.W32.MouseHook";
            // 
            // power
            // 
            this.power.Controls.Add(this.power_execute);
            this.power.Controls.Add(this.power_reason_label);
            this.power.Controls.Add(this.power_mod_label);
            this.power.Controls.Add(this.power_mod_box);
            this.power.Controls.Add(this.power_reason_box);
            this.power.Controls.Add(this.power_mode_label);
            this.power.Controls.Add(this.power_mode_box);
            this.power.Location = new System.Drawing.Point(12, 11);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(287, 100);
            this.power.TabIndex = 5;
            this.power.TabStop = false;
            this.power.Text = "CC-Functions.W32.Power";
            // 
            // power_execute
            // 
            this.power_execute.BackColor = System.Drawing.Color.DarkRed;
            this.power_execute.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.power_execute.ForeColor = System.Drawing.Color.White;
            this.power_execute.Location = new System.Drawing.Point(200, 19);
            this.power_execute.Name = "power_execute";
            this.power_execute.Size = new System.Drawing.Size(81, 75);
            this.power_execute.TabIndex = 6;
            this.power_execute.Text = "DO IT";
            this.power_execute.UseVisualStyleBackColor = false;
            this.power_execute.Click += new System.EventHandler(this.Power_execute_Click);
            // 
            // power_reason_label
            // 
            this.power_reason_label.AutoSize = true;
            this.power_reason_label.Location = new System.Drawing.Point(6, 49);
            this.power_reason_label.Name = "power_reason_label";
            this.power_reason_label.Size = new System.Drawing.Size(44, 13);
            this.power_reason_label.TabIndex = 5;
            this.power_reason_label.Text = "Reason";
            // 
            // power_mod_label
            // 
            this.power_mod_label.AutoSize = true;
            this.power_mod_label.Location = new System.Drawing.Point(6, 76);
            this.power_mod_label.Name = "power_mod_label";
            this.power_mod_label.Size = new System.Drawing.Size(28, 13);
            this.power_mod_label.TabIndex = 4;
            this.power_mod_label.Text = "Mod";
            // 
            // power_mod_box
            // 
            this.power_mod_box.FormattingEnabled = true;
            this.power_mod_box.Location = new System.Drawing.Point(56, 73);
            this.power_mod_box.Name = "power_mod_box";
            this.power_mod_box.Size = new System.Drawing.Size(138, 21);
            this.power_mod_box.TabIndex = 2;
            // 
            // power_reason_box
            // 
            this.power_reason_box.FormattingEnabled = true;
            this.power_reason_box.Location = new System.Drawing.Point(56, 46);
            this.power_reason_box.Name = "power_reason_box";
            this.power_reason_box.Size = new System.Drawing.Size(138, 21);
            this.power_reason_box.TabIndex = 3;
            // 
            // power_mode_label
            // 
            this.power_mode_label.AutoSize = true;
            this.power_mode_label.Location = new System.Drawing.Point(6, 22);
            this.power_mode_label.Name = "power_mode_label";
            this.power_mode_label.Size = new System.Drawing.Size(34, 13);
            this.power_mode_label.TabIndex = 1;
            this.power_mode_label.Text = "Mode";
            // 
            // power_mode_box
            // 
            this.power_mode_box.FormattingEnabled = true;
            this.power_mode_box.Location = new System.Drawing.Point(56, 19);
            this.power_mode_box.Name = "power_mode_box";
            this.power_mode_box.Size = new System.Drawing.Size(138, 21);
            this.power_mode_box.TabIndex = 0;
            // 
            // wnd_select_mouse
            // 
            this.wnd_select_mouse.Location = new System.Drawing.Point(190, 19);
            this.wnd_select_mouse.Name = "wnd_select_mouse";
            this.wnd_select_mouse.Size = new System.Drawing.Size(50, 23);
            this.wnd_select_mouse.TabIndex = 12;
            this.wnd_select_mouse.Text = "Mouse";
            this.wnd_select_mouse.UseVisualStyleBackColor = true;
            this.wnd_select_mouse.Click += new System.EventHandler(this.Wnd_select_mouse_Click);
            // 
            // wnd_action_destroy
            // 
            this.wnd_action_destroy.Location = new System.Drawing.Point(6, 160);
            this.wnd_action_destroy.Name = "wnd_action_destroy";
            this.wnd_action_destroy.Size = new System.Drawing.Size(75, 23);
            this.wnd_action_destroy.TabIndex = 2;
            this.wnd_action_destroy.Text = "Destroy";
            this.wnd_action_destroy.UseVisualStyleBackColor = true;
            this.wnd_action_destroy.Click += new System.EventHandler(this.Wnd_action_destroy_Click);
            // 
            // wnd_action_front
            // 
            this.wnd_action_front.Location = new System.Drawing.Point(168, 160);
            this.wnd_action_front.Name = "wnd_action_front";
            this.wnd_action_front.Size = new System.Drawing.Size(72, 23);
            this.wnd_action_front.TabIndex = 10;
            this.wnd_action_front.Text = "To Front";
            this.wnd_action_front.UseVisualStyleBackColor = true;
            this.wnd_action_front.Click += new System.EventHandler(this.Wnd_action_front_Click);
            // 
            // wnd_action_enabled
            // 
            this.wnd_action_enabled.AutoSize = true;
            this.wnd_action_enabled.Location = new System.Drawing.Point(113, 110);
            this.wnd_action_enabled.Name = "wnd_action_enabled";
            this.wnd_action_enabled.Size = new System.Drawing.Size(65, 17);
            this.wnd_action_enabled.TabIndex = 9;
            this.wnd_action_enabled.Text = "Enabled";
            this.wnd_action_enabled.UseVisualStyleBackColor = true;
            this.wnd_action_enabled.CheckedChanged += new System.EventHandler(this.Wnd_action_enabled_CheckedChanged);
            // 
            // wnd_action_title_get
            // 
            this.wnd_action_title_get.Location = new System.Drawing.Point(203, 77);
            this.wnd_action_title_get.Name = "wnd_action_title_get";
            this.wnd_action_title_get.Size = new System.Drawing.Size(37, 23);
            this.wnd_action_title_get.TabIndex = 8;
            this.wnd_action_title_get.Text = "Get";
            this.wnd_action_title_get.UseVisualStyleBackColor = true;
            this.wnd_action_title_get.Click += new System.EventHandler(this.Wnd_action_title_get_Click);
            // 
            // wnd_action_title_set
            // 
            this.wnd_action_title_set.Location = new System.Drawing.Point(203, 48);
            this.wnd_action_title_set.Name = "wnd_action_title_set";
            this.wnd_action_title_set.Size = new System.Drawing.Size(37, 23);
            this.wnd_action_title_set.TabIndex = 7;
            this.wnd_action_title_set.Text = "Set";
            this.wnd_action_title_set.UseVisualStyleBackColor = true;
            this.wnd_action_title_set.Click += new System.EventHandler(this.Wnd_action_title_set_Click);
            // 
            // wnd_select_title_box
            // 
            this.wnd_select_title_box.Location = new System.Drawing.Point(93, 50);
            this.wnd_select_title_box.Name = "wnd_select_title_box";
            this.wnd_select_title_box.Size = new System.Drawing.Size(104, 20);
            this.wnd_select_title_box.TabIndex = 6;
            // 
            // wnd_selet_class_button
            // 
            this.wnd_selet_class_button.Location = new System.Drawing.Point(6, 77);
            this.wnd_selet_class_button.Name = "wnd_selet_class_button";
            this.wnd_selet_class_button.Size = new System.Drawing.Size(81, 23);
            this.wnd_selet_class_button.TabIndex = 4;
            this.wnd_selet_class_button.Text = "Select (class):";
            this.wnd_selet_class_button.UseVisualStyleBackColor = true;
            this.wnd_selet_class_button.Click += new System.EventHandler(this.Wnd_selet_class_button_Click);
            // 
            // wnd_select_title_button
            // 
            this.wnd_select_title_button.Location = new System.Drawing.Point(6, 48);
            this.wnd_select_title_button.Name = "wnd_select_title_button";
            this.wnd_select_title_button.Size = new System.Drawing.Size(81, 23);
            this.wnd_select_title_button.TabIndex = 2;
            this.wnd_select_title_button.Text = "Select (title):";
            this.wnd_select_title_button.UseVisualStyleBackColor = true;
            this.wnd_select_title_button.Click += new System.EventHandler(this.Wnd_select_title_button_Click);
            // 
            // wnd_select_selected
            // 
            this.wnd_select_selected.AutoSize = true;
            this.wnd_select_selected.Location = new System.Drawing.Point(93, 24);
            this.wnd_select_selected.Name = "wnd_select_selected";
            this.wnd_select_selected.Size = new System.Drawing.Size(91, 13);
            this.wnd_select_selected.TabIndex = 1;
            this.wnd_select_selected.Text = "Selected: 123456";
            // 
            // wnd_select_self
            // 
            this.wnd_select_self.Location = new System.Drawing.Point(6, 19);
            this.wnd_select_self.Name = "wnd_select_self";
            this.wnd_select_self.Size = new System.Drawing.Size(81, 23);
            this.wnd_select_self.TabIndex = 0;
            this.wnd_select_self.Text = "Select self";
            this.wnd_select_self.UseVisualStyleBackColor = true;
            this.wnd_select_self.Click += new System.EventHandler(this.Wnd_select_self_Click);
            // 
            // wnd
            // 
            this.wnd.Controls.Add(this.wnd_select_list);
            this.wnd.Controls.Add(this.wnd_action_overlay);
            this.wnd.Controls.Add(this.wnd_action_style);
            this.wnd.Controls.Add(this.wnd_action_visible);
            this.wnd.Controls.Add(this.wnd_action_icon);
            this.wnd.Controls.Add(this.wnd_action_pos_h_label);
            this.wnd.Controls.Add(this.wnd_action_pos_w_label);
            this.wnd.Controls.Add(this.wnd_action_pos_h_bar);
            this.wnd.Controls.Add(this.wnd_action_pos_w_bar);
            this.wnd.Controls.Add(this.wnd_action_pos_y_label);
            this.wnd.Controls.Add(this.wnd_action_pos_x_label);
            this.wnd.Controls.Add(this.wnd_action_pos_y_bar);
            this.wnd.Controls.Add(this.wnd_action_pos_x_bar);
            this.wnd.Controls.Add(this.wnd_action_pos);
            this.wnd.Controls.Add(this.wnd_select_mouse);
            this.wnd.Controls.Add(this.wnd_action_destroy);
            this.wnd.Controls.Add(this.wnd_action_front);
            this.wnd.Controls.Add(this.wnd_action_enabled);
            this.wnd.Controls.Add(this.wnd_action_title_get);
            this.wnd.Controls.Add(this.wnd_action_title_set);
            this.wnd.Controls.Add(this.wnd_select_title_box);
            this.wnd.Controls.Add(this.wnd_select_class_box);
            this.wnd.Controls.Add(this.wnd_selet_class_button);
            this.wnd.Controls.Add(this.wnd_select_title_button);
            this.wnd.Controls.Add(this.wnd_select_selected);
            this.wnd.Controls.Add(this.wnd_select_self);
            this.wnd.Location = new System.Drawing.Point(12, 117);
            this.wnd.Name = "wnd";
            this.wnd.Size = new System.Drawing.Size(246, 241);
            this.wnd.TabIndex = 6;
            this.wnd.TabStop = false;
            this.wnd.Text = "CC-Functions.W32.Wnd32";
            // 
            // wnd_action_overlay
            // 
            this.wnd_action_overlay.AutoSize = true;
            this.wnd_action_overlay.Location = new System.Drawing.Point(136, 135);
            this.wnd_action_overlay.Name = "wnd_action_overlay";
            this.wnd_action_overlay.Size = new System.Drawing.Size(62, 17);
            this.wnd_action_overlay.TabIndex = 25;
            this.wnd_action_overlay.Text = "Overlay";
            this.wnd_action_overlay.UseVisualStyleBackColor = true;
            this.wnd_action_overlay.CheckedChanged += new System.EventHandler(this.wnd_action_overlay_CheckedChanged);
            // 
            // wnd_action_style
            // 
            this.wnd_action_style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wnd_action_style.FormattingEnabled = true;
            this.wnd_action_style.Location = new System.Drawing.Point(6, 133);
            this.wnd_action_style.Name = "wnd_action_style";
            this.wnd_action_style.Size = new System.Drawing.Size(121, 21);
            this.wnd_action_style.TabIndex = 24;
            this.wnd_action_style.SelectedIndexChanged += new System.EventHandler(this.Wnd_action_style_SelectedIndexChanged);
            // 
            // wnd_action_visible
            // 
            this.wnd_action_visible.AutoSize = true;
            this.wnd_action_visible.Location = new System.Drawing.Point(184, 110);
            this.wnd_action_visible.Name = "wnd_action_visible";
            this.wnd_action_visible.Size = new System.Drawing.Size(56, 17);
            this.wnd_action_visible.TabIndex = 23;
            this.wnd_action_visible.Text = "Visible";
            this.wnd_action_visible.UseVisualStyleBackColor = true;
            this.wnd_action_visible.CheckedChanged += new System.EventHandler(this.Wnd_action_visible_CheckedChanged);
            // 
            // wnd_action_icon
            // 
            this.wnd_action_icon.BackColor = System.Drawing.SystemColors.ControlLight;
            this.wnd_action_icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wnd_action_icon.Location = new System.Drawing.Point(217, 131);
            this.wnd_action_icon.Name = "wnd_action_icon";
            this.wnd_action_icon.Size = new System.Drawing.Size(23, 23);
            this.wnd_action_icon.TabIndex = 22;
            // 
            // wnd_select_class_box
            // 
            this.wnd_select_class_box.Location = new System.Drawing.Point(93, 79);
            this.wnd_select_class_box.Name = "wnd_select_class_box";
            this.wnd_select_class_box.Size = new System.Drawing.Size(104, 20);
            this.wnd_select_class_box.TabIndex = 5;
            // 
            // wnd_select_list
            // 
            this.wnd_select_list.Location = new System.Drawing.Point(6, 106);
            this.wnd_select_list.Name = "wnd_select_list";
            this.wnd_select_list.Size = new System.Drawing.Size(81, 23);
            this.wnd_select_list.TabIndex = 26;
            this.wnd_select_list.Text = "Select (list)";
            this.wnd_select_list.UseVisualStyleBackColor = true;
            this.wnd_select_list.Click += new System.EventHandler(this.wnd_select_list_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 370);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.mouse);
            this.Controls.Add(this.power);
            this.Controls.Add(this.wnd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "CC-Functions.W32.Test";
            this.keyboard.ResumeLayout(false);
            this.keyboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_h_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_w_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_y_bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_x_bar)).EndInit();
            this.mouse.ResumeLayout(false);
            this.mouse.PerformLayout();
            this.power.ResumeLayout(false);
            this.power.PerformLayout();
            this.wnd.ResumeLayout(false);
            this.wnd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.TextBox keyboard_log;
        private System.Windows.Forms.GroupBox keyboard;
        private System.Windows.Forms.CheckBox keyboard_enabled;
        private System.Windows.Forms.TextBox mouse_log;
        private System.Windows.Forms.CheckBox mouse_enabled;
        private System.Windows.Forms.Label wnd_action_pos_h_label;
        private System.Windows.Forms.Label wnd_action_pos_w_label;
        private System.Windows.Forms.TrackBar wnd_action_pos_h_bar;
        private System.Windows.Forms.TrackBar wnd_action_pos_w_bar;
        private System.Windows.Forms.Label wnd_action_pos_y_label;
        private System.Windows.Forms.Label wnd_action_pos_x_label;
        private System.Windows.Forms.TrackBar wnd_action_pos_y_bar;
        private System.Windows.Forms.TrackBar wnd_action_pos_x_bar;
        private System.Windows.Forms.Button wnd_action_pos;
        private System.Windows.Forms.GroupBox mouse;
        private System.Windows.Forms.GroupBox power;
        private System.Windows.Forms.Button power_execute;
        private System.Windows.Forms.Label power_reason_label;
        private System.Windows.Forms.Label power_mod_label;
        private System.Windows.Forms.ComboBox power_mod_box;
        private System.Windows.Forms.ComboBox power_reason_box;
        private System.Windows.Forms.Label power_mode_label;
        private System.Windows.Forms.ComboBox power_mode_box;
        private System.Windows.Forms.Button wnd_select_mouse;
        private System.Windows.Forms.Button wnd_action_destroy;
        private System.Windows.Forms.Button wnd_action_front;
        private System.Windows.Forms.CheckBox wnd_action_enabled;
        private System.Windows.Forms.Button wnd_action_title_get;
        private System.Windows.Forms.Button wnd_action_title_set;
        private System.Windows.Forms.TextBox wnd_select_title_box;
        private System.Windows.Forms.Button wnd_selet_class_button;
        private System.Windows.Forms.Button wnd_select_title_button;
        private System.Windows.Forms.Label wnd_select_selected;
        private System.Windows.Forms.Button wnd_select_self;
        private System.Windows.Forms.GroupBox wnd;
        private System.Windows.Forms.TextBox wnd_select_class_box;
        private System.Windows.Forms.Panel wnd_action_icon;
        private System.Windows.Forms.CheckBox wnd_action_visible;
        private System.Windows.Forms.ComboBox wnd_action_style;
        private System.Windows.Forms.CheckBox wnd_action_overlay;
        private System.Windows.Forms.Button wnd_select_list;
    }
}