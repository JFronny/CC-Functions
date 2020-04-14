namespace CC_Functions.W32.Test
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
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
            this.wnd_select_class_button = new System.Windows.Forms.Button();
            this.wnd_select_title_button = new System.Windows.Forms.Button();
            this.wnd_select_selected = new System.Windows.Forms.Label();
            this.wnd_select_self = new System.Windows.Forms.Button();
            this.wnd = new System.Windows.Forms.GroupBox();
            this.wnd_select_parent = new System.Windows.Forms.Button();
            this.wnd_select_child = new System.Windows.Forms.Button();
            this.wnd_select_list = new System.Windows.Forms.Button();
            this.wnd_action_overlay = new System.Windows.Forms.CheckBox();
            this.wnd_action_style = new System.Windows.Forms.ComboBox();
            this.wnd_action_visible = new System.Windows.Forms.CheckBox();
            this.wnd_action_icon = new System.Windows.Forms.Panel();
            this.wnd_select_class_box = new System.Windows.Forms.TextBox();
            this.screen = new System.Windows.Forms.GroupBox();
            this.screen_draw = new System.Windows.Forms.Button();
            this.screen_get = new System.Windows.Forms.Button();
            this.screen_img = new System.Windows.Forms.Panel();
            this.reader = new System.Windows.Forms.GroupBox();
            this.readerFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.desk = new System.Windows.Forms.GroupBox();
            this.desk_draw = new System.Windows.Forms.Button();
            this.desk_set = new System.Windows.Forms.Button();
            this.desk_get = new System.Windows.Forms.Button();
            this.desk_back = new System.Windows.Forms.Panel();
            this.readerUpdate = new System.Windows.Forms.Timer(this.components);
            this.time = new System.Windows.Forms.GroupBox();
            this.time_set = new System.Windows.Forms.Button();
            this.time_select = new System.Windows.Forms.DateTimePicker();
            this.keyboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_h_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_w_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_y_bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wnd_action_pos_x_bar)).BeginInit();
            this.mouse.SuspendLayout();
            this.power.SuspendLayout();
            this.wnd.SuspendLayout();
            this.screen.SuspendLayout();
            this.reader.SuspendLayout();
            this.desk.SuspendLayout();
            this.time.SuspendLayout();
            this.SuspendLayout();
            // 
            // keyboard_log
            // 
            this.keyboard_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.keyboard_log.Location = new System.Drawing.Point(7, 48);
            this.keyboard_log.Multiline = true;
            this.keyboard_log.Name = "keyboard_log";
            this.keyboard_log.ReadOnly = true;
            this.keyboard_log.Size = new System.Drawing.Size(333, 133);
            this.keyboard_log.TabIndex = 2;
            this.keyboard_log.TextChanged += new System.EventHandler(this.Keyboard_log_TextChanged);
            // 
            // keyboard
            // 
            this.keyboard.Controls.Add(this.keyboard_log);
            this.keyboard.Controls.Add(this.keyboard_enabled);
            this.keyboard.Location = new System.Drawing.Point(308, 224);
            this.keyboard.Name = "keyboard";
            this.keyboard.Size = new System.Drawing.Size(348, 189);
            this.keyboard.TabIndex = 8;
            this.keyboard.TabStop = false;
            this.keyboard.Text = "CC-Functions.W32.KeyboardHook";
            // 
            // keyboard_enabled
            // 
            this.keyboard_enabled.AutoSize = true;
            this.keyboard_enabled.Location = new System.Drawing.Point(7, 22);
            this.keyboard_enabled.Name = "keyboard_enabled";
            this.keyboard_enabled.Size = new System.Drawing.Size(68, 19);
            this.keyboard_enabled.TabIndex = 0;
            this.keyboard_enabled.Text = "Enabled";
            this.keyboard_enabled.UseVisualStyleBackColor = true;
            this.keyboard_enabled.CheckedChanged += new System.EventHandler(this.Keyboard_enabled_CheckedChanged);
            // 
            // mouse_log
            // 
            this.mouse_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mouse_log.Location = new System.Drawing.Point(7, 48);
            this.mouse_log.Multiline = true;
            this.mouse_log.Name = "mouse_log";
            this.mouse_log.ReadOnly = true;
            this.mouse_log.Size = new System.Drawing.Size(333, 148);
            this.mouse_log.TabIndex = 1;
            this.mouse_log.TextChanged += new System.EventHandler(this.Mouse_log_TextChanged);
            // 
            // mouse_enabled
            // 
            this.mouse_enabled.AutoSize = true;
            this.mouse_enabled.Location = new System.Drawing.Point(7, 22);
            this.mouse_enabled.Name = "mouse_enabled";
            this.mouse_enabled.Size = new System.Drawing.Size(68, 19);
            this.mouse_enabled.TabIndex = 0;
            this.mouse_enabled.Text = "Enabled";
            this.mouse_enabled.UseVisualStyleBackColor = true;
            this.mouse_enabled.CheckedChanged += new System.EventHandler(this.Mouse_enabled_CheckedChanged);
            // 
            // wnd_action_pos_h_label
            // 
            this.wnd_action_pos_h_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_pos_h_label.AutoSize = true;
            this.wnd_action_pos_h_label.Location = new System.Drawing.Point(135, 284);
            this.wnd_action_pos_h_label.Name = "wnd_action_pos_h_label";
            this.wnd_action_pos_h_label.Size = new System.Drawing.Size(19, 15);
            this.wnd_action_pos_h_label.TabIndex = 19;
            this.wnd_action_pos_h_label.Text = "H:";
            // 
            // wnd_action_pos_w_label
            // 
            this.wnd_action_pos_w_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_pos_w_label.AutoSize = true;
            this.wnd_action_pos_w_label.Location = new System.Drawing.Point(135, 256);
            this.wnd_action_pos_w_label.Name = "wnd_action_pos_w_label";
            this.wnd_action_pos_w_label.Size = new System.Drawing.Size(21, 15);
            this.wnd_action_pos_w_label.TabIndex = 18;
            this.wnd_action_pos_w_label.Text = "W:";
            // 
            // wnd_action_pos_h_bar
            // 
            this.wnd_action_pos_h_bar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_pos_h_bar.Location = new System.Drawing.Point(159, 284);
            this.wnd_action_pos_h_bar.Name = "wnd_action_pos_h_bar";
            this.wnd_action_pos_h_bar.Size = new System.Drawing.Size(121, 45);
            this.wnd_action_pos_h_bar.TabIndex = 21;
            // 
            // wnd_action_pos_w_bar
            // 
            this.wnd_action_pos_w_bar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_pos_w_bar.Location = new System.Drawing.Point(159, 256);
            this.wnd_action_pos_w_bar.Name = "wnd_action_pos_w_bar";
            this.wnd_action_pos_w_bar.Size = new System.Drawing.Size(121, 45);
            this.wnd_action_pos_w_bar.TabIndex = 20;
            // 
            // wnd_action_pos_y_label
            // 
            this.wnd_action_pos_y_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_pos_y_label.AutoSize = true;
            this.wnd_action_pos_y_label.Location = new System.Drawing.Point(7, 284);
            this.wnd_action_pos_y_label.Name = "wnd_action_pos_y_label";
            this.wnd_action_pos_y_label.Size = new System.Drawing.Size(17, 15);
            this.wnd_action_pos_y_label.TabIndex = 15;
            this.wnd_action_pos_y_label.Text = "Y:";
            // 
            // wnd_action_pos_x_label
            // 
            this.wnd_action_pos_x_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_pos_x_label.AutoSize = true;
            this.wnd_action_pos_x_label.Location = new System.Drawing.Point(7, 256);
            this.wnd_action_pos_x_label.Name = "wnd_action_pos_x_label";
            this.wnd_action_pos_x_label.Size = new System.Drawing.Size(17, 15);
            this.wnd_action_pos_x_label.TabIndex = 13;
            this.wnd_action_pos_x_label.Text = "X:";
            // 
            // wnd_action_pos_y_bar
            // 
            this.wnd_action_pos_y_bar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_pos_y_bar.Location = new System.Drawing.Point(20, 284);
            this.wnd_action_pos_y_bar.Name = "wnd_action_pos_y_bar";
            this.wnd_action_pos_y_bar.Size = new System.Drawing.Size(121, 45);
            this.wnd_action_pos_y_bar.TabIndex = 17;
            // 
            // wnd_action_pos_x_bar
            // 
            this.wnd_action_pos_x_bar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_pos_x_bar.Location = new System.Drawing.Point(20, 256);
            this.wnd_action_pos_x_bar.Name = "wnd_action_pos_x_bar";
            this.wnd_action_pos_x_bar.Size = new System.Drawing.Size(121, 45);
            this.wnd_action_pos_x_bar.TabIndex = 16;
            // 
            // wnd_action_pos
            // 
            this.wnd_action_pos.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.wnd_action_pos.Location = new System.Drawing.Point(101, 226);
            this.wnd_action_pos.Name = "wnd_action_pos";
            this.wnd_action_pos.Size = new System.Drawing.Size(87, 27);
            this.wnd_action_pos.TabIndex = 14;
            this.wnd_action_pos.Text = "Set Position";
            this.wnd_action_pos.UseVisualStyleBackColor = true;
            this.wnd_action_pos.Click += new System.EventHandler(this.Wnd_action_pos_Click);
            // 
            // mouse
            // 
            this.mouse.Controls.Add(this.mouse_log);
            this.mouse.Controls.Add(this.mouse_enabled);
            this.mouse.Location = new System.Drawing.Point(308, 13);
            this.mouse.Name = "mouse";
            this.mouse.Size = new System.Drawing.Size(348, 204);
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
            this.power.Location = new System.Drawing.Point(14, 13);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(287, 115);
            this.power.TabIndex = 5;
            this.power.TabStop = false;
            this.power.Text = "CC-Functions.W32.Power";
            // 
            // power_execute
            // 
            this.power_execute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.power_execute.BackColor = System.Drawing.Color.DarkRed;
            this.power_execute.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.power_execute.ForeColor = System.Drawing.Color.White;
            this.power_execute.Location = new System.Drawing.Point(185, 22);
            this.power_execute.Name = "power_execute";
            this.power_execute.Size = new System.Drawing.Size(94, 87);
            this.power_execute.TabIndex = 6;
            this.power_execute.Text = "DO IT";
            this.power_execute.UseVisualStyleBackColor = false;
            this.power_execute.Click += new System.EventHandler(this.Power_execute_Click);
            // 
            // power_reason_label
            // 
            this.power_reason_label.AutoSize = true;
            this.power_reason_label.Location = new System.Drawing.Point(7, 57);
            this.power_reason_label.Name = "power_reason_label";
            this.power_reason_label.Size = new System.Drawing.Size(45, 15);
            this.power_reason_label.TabIndex = 5;
            this.power_reason_label.Text = "Reason";
            // 
            // power_mod_label
            // 
            this.power_mod_label.AutoSize = true;
            this.power_mod_label.Location = new System.Drawing.Point(7, 88);
            this.power_mod_label.Name = "power_mod_label";
            this.power_mod_label.Size = new System.Drawing.Size(32, 15);
            this.power_mod_label.TabIndex = 4;
            this.power_mod_label.Text = "Mod";
            // 
            // power_mod_box
            // 
            this.power_mod_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.power_mod_box.FormattingEnabled = true;
            this.power_mod_box.Location = new System.Drawing.Point(65, 84);
            this.power_mod_box.Name = "power_mod_box";
            this.power_mod_box.Size = new System.Drawing.Size(112, 23);
            this.power_mod_box.TabIndex = 2;
            // 
            // power_reason_box
            // 
            this.power_reason_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.power_reason_box.FormattingEnabled = true;
            this.power_reason_box.Location = new System.Drawing.Point(65, 53);
            this.power_reason_box.Name = "power_reason_box";
            this.power_reason_box.Size = new System.Drawing.Size(112, 23);
            this.power_reason_box.TabIndex = 3;
            // 
            // power_mode_label
            // 
            this.power_mode_label.AutoSize = true;
            this.power_mode_label.Location = new System.Drawing.Point(7, 25);
            this.power_mode_label.Name = "power_mode_label";
            this.power_mode_label.Size = new System.Drawing.Size(38, 15);
            this.power_mode_label.TabIndex = 1;
            this.power_mode_label.Text = "Mode";
            // 
            // power_mode_box
            // 
            this.power_mode_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.power_mode_box.FormattingEnabled = true;
            this.power_mode_box.Location = new System.Drawing.Point(65, 22);
            this.power_mode_box.Name = "power_mode_box";
            this.power_mode_box.Size = new System.Drawing.Size(112, 23);
            this.power_mode_box.TabIndex = 0;
            // 
            // wnd_select_mouse
            // 
            this.wnd_select_mouse.Location = new System.Drawing.Point(222, 22);
            this.wnd_select_mouse.Name = "wnd_select_mouse";
            this.wnd_select_mouse.Size = new System.Drawing.Size(58, 27);
            this.wnd_select_mouse.TabIndex = 12;
            this.wnd_select_mouse.Text = "Mouse";
            this.wnd_select_mouse.UseVisualStyleBackColor = true;
            this.wnd_select_mouse.Click += new System.EventHandler(this.Wnd_select_mouse_Click);
            // 
            // wnd_action_destroy
            // 
            this.wnd_action_destroy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.wnd_action_destroy.Location = new System.Drawing.Point(7, 226);
            this.wnd_action_destroy.Name = "wnd_action_destroy";
            this.wnd_action_destroy.Size = new System.Drawing.Size(87, 27);
            this.wnd_action_destroy.TabIndex = 2;
            this.wnd_action_destroy.Text = "Destroy";
            this.wnd_action_destroy.UseVisualStyleBackColor = true;
            this.wnd_action_destroy.Click += new System.EventHandler(this.Wnd_action_destroy_Click);
            // 
            // wnd_action_front
            // 
            this.wnd_action_front.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.wnd_action_front.Location = new System.Drawing.Point(196, 226);
            this.wnd_action_front.Name = "wnd_action_front";
            this.wnd_action_front.Size = new System.Drawing.Size(84, 27);
            this.wnd_action_front.TabIndex = 10;
            this.wnd_action_front.Text = "To Front";
            this.wnd_action_front.UseVisualStyleBackColor = true;
            this.wnd_action_front.Click += new System.EventHandler(this.Wnd_action_front_Click);
            // 
            // wnd_action_enabled
            // 
            this.wnd_action_enabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_enabled.AutoSize = true;
            this.wnd_action_enabled.Location = new System.Drawing.Point(7, 167);
            this.wnd_action_enabled.Name = "wnd_action_enabled";
            this.wnd_action_enabled.Size = new System.Drawing.Size(68, 19);
            this.wnd_action_enabled.TabIndex = 9;
            this.wnd_action_enabled.Text = "Enabled";
            this.wnd_action_enabled.UseVisualStyleBackColor = true;
            this.wnd_action_enabled.CheckedChanged += new System.EventHandler(this.Wnd_action_enabled_CheckedChanged);
            // 
            // wnd_action_title_get
            // 
            this.wnd_action_title_get.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_title_get.Location = new System.Drawing.Point(237, 159);
            this.wnd_action_title_get.Name = "wnd_action_title_get";
            this.wnd_action_title_get.Size = new System.Drawing.Size(43, 27);
            this.wnd_action_title_get.TabIndex = 8;
            this.wnd_action_title_get.Text = "Get";
            this.wnd_action_title_get.UseVisualStyleBackColor = true;
            this.wnd_action_title_get.Click += new System.EventHandler(this.Wnd_action_title_get_Click);
            // 
            // wnd_action_title_set
            // 
            this.wnd_action_title_set.Location = new System.Drawing.Point(237, 55);
            this.wnd_action_title_set.Name = "wnd_action_title_set";
            this.wnd_action_title_set.Size = new System.Drawing.Size(43, 27);
            this.wnd_action_title_set.TabIndex = 7;
            this.wnd_action_title_set.Text = "Set";
            this.wnd_action_title_set.UseVisualStyleBackColor = true;
            this.wnd_action_title_set.Click += new System.EventHandler(this.Wnd_action_title_set_Click);
            // 
            // wnd_select_title_box
            // 
            this.wnd_select_title_box.Location = new System.Drawing.Point(108, 58);
            this.wnd_select_title_box.Name = "wnd_select_title_box";
            this.wnd_select_title_box.Size = new System.Drawing.Size(121, 23);
            this.wnd_select_title_box.TabIndex = 6;
            // 
            // wnd_select_class_button
            // 
            this.wnd_select_class_button.Location = new System.Drawing.Point(7, 89);
            this.wnd_select_class_button.Name = "wnd_select_class_button";
            this.wnd_select_class_button.Size = new System.Drawing.Size(94, 27);
            this.wnd_select_class_button.TabIndex = 4;
            this.wnd_select_class_button.Text = "Select (class):";
            this.wnd_select_class_button.UseVisualStyleBackColor = true;
            this.wnd_select_class_button.Click += new System.EventHandler(this.Wnd_select_class_button_Click);
            // 
            // wnd_select_title_button
            // 
            this.wnd_select_title_button.Location = new System.Drawing.Point(7, 55);
            this.wnd_select_title_button.Name = "wnd_select_title_button";
            this.wnd_select_title_button.Size = new System.Drawing.Size(94, 27);
            this.wnd_select_title_button.TabIndex = 2;
            this.wnd_select_title_button.Text = "Select (title):";
            this.wnd_select_title_button.UseVisualStyleBackColor = true;
            this.wnd_select_title_button.Click += new System.EventHandler(this.Wnd_select_title_button_Click);
            // 
            // wnd_select_selected
            // 
            this.wnd_select_selected.AutoSize = true;
            this.wnd_select_selected.Location = new System.Drawing.Point(108, 28);
            this.wnd_select_selected.Name = "wnd_select_selected";
            this.wnd_select_selected.Size = new System.Drawing.Size(93, 15);
            this.wnd_select_selected.TabIndex = 1;
            this.wnd_select_selected.Text = "Selected: 123456";
            // 
            // wnd_select_self
            // 
            this.wnd_select_self.Location = new System.Drawing.Point(7, 22);
            this.wnd_select_self.Name = "wnd_select_self";
            this.wnd_select_self.Size = new System.Drawing.Size(94, 27);
            this.wnd_select_self.TabIndex = 0;
            this.wnd_select_self.Text = "Select self";
            this.wnd_select_self.UseVisualStyleBackColor = true;
            this.wnd_select_self.Click += new System.EventHandler(this.Wnd_select_self_Click);
            // 
            // wnd
            // 
            this.wnd.Controls.Add(this.wnd_select_parent);
            this.wnd.Controls.Add(this.wnd_select_child);
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
            this.wnd.Controls.Add(this.wnd_select_class_button);
            this.wnd.Controls.Add(this.wnd_select_title_button);
            this.wnd.Controls.Add(this.wnd_select_selected);
            this.wnd.Controls.Add(this.wnd_select_self);
            this.wnd.Location = new System.Drawing.Point(14, 135);
            this.wnd.Name = "wnd";
            this.wnd.Size = new System.Drawing.Size(287, 345);
            this.wnd.TabIndex = 6;
            this.wnd.TabStop = false;
            this.wnd.Text = "CC-Functions.W32.Wnd32";
            // 
            // wnd_select_parent
            // 
            this.wnd_select_parent.Location = new System.Drawing.Point(207, 122);
            this.wnd_select_parent.Name = "wnd_select_parent";
            this.wnd_select_parent.Size = new System.Drawing.Size(74, 27);
            this.wnd_select_parent.TabIndex = 27;
            this.wnd_select_parent.Text = "Parent";
            this.wnd_select_parent.UseVisualStyleBackColor = true;
            this.wnd_select_parent.Click += new System.EventHandler(this.wnd_select_parent_Click);
            // 
            // wnd_select_child
            // 
            this.wnd_select_child.Location = new System.Drawing.Point(107, 122);
            this.wnd_select_child.Name = "wnd_select_child";
            this.wnd_select_child.Size = new System.Drawing.Size(94, 27);
            this.wnd_select_child.TabIndex = 27;
            this.wnd_select_child.Text = "Select (childs)";
            this.wnd_select_child.UseVisualStyleBackColor = true;
            this.wnd_select_child.Click += new System.EventHandler(this.wnd_select_child_Click);
            // 
            // wnd_select_list
            // 
            this.wnd_select_list.Location = new System.Drawing.Point(7, 122);
            this.wnd_select_list.Name = "wnd_select_list";
            this.wnd_select_list.Size = new System.Drawing.Size(94, 27);
            this.wnd_select_list.TabIndex = 26;
            this.wnd_select_list.Text = "Select (list)";
            this.wnd_select_list.UseVisualStyleBackColor = true;
            this.wnd_select_list.Click += new System.EventHandler(this.wnd_select_list_Click);
            // 
            // wnd_action_overlay
            // 
            this.wnd_action_overlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_overlay.AutoSize = true;
            this.wnd_action_overlay.Location = new System.Drawing.Point(162, 167);
            this.wnd_action_overlay.Name = "wnd_action_overlay";
            this.wnd_action_overlay.Size = new System.Drawing.Size(66, 19);
            this.wnd_action_overlay.TabIndex = 25;
            this.wnd_action_overlay.Text = "Overlay";
            this.wnd_action_overlay.UseVisualStyleBackColor = true;
            this.wnd_action_overlay.CheckedChanged += new System.EventHandler(this.wnd_action_overlay_CheckedChanged);
            // 
            // wnd_action_style
            // 
            this.wnd_action_style.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wnd_action_style.FormattingEnabled = true;
            this.wnd_action_style.Location = new System.Drawing.Point(7, 195);
            this.wnd_action_style.Name = "wnd_action_style";
            this.wnd_action_style.Size = new System.Drawing.Size(238, 23);
            this.wnd_action_style.TabIndex = 24;
            this.wnd_action_style.SelectedIndexChanged += new System.EventHandler(this.Wnd_action_style_SelectedIndexChanged);
            // 
            // wnd_action_visible
            // 
            this.wnd_action_visible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wnd_action_visible.AutoSize = true;
            this.wnd_action_visible.Location = new System.Drawing.Point(90, 167);
            this.wnd_action_visible.Name = "wnd_action_visible";
            this.wnd_action_visible.Size = new System.Drawing.Size(60, 19);
            this.wnd_action_visible.TabIndex = 23;
            this.wnd_action_visible.Text = "Visible";
            this.wnd_action_visible.UseVisualStyleBackColor = true;
            this.wnd_action_visible.CheckedChanged += new System.EventHandler(this.Wnd_action_visible_CheckedChanged);
            // 
            // wnd_action_icon
            // 
            this.wnd_action_icon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.wnd_action_icon.BackColor = System.Drawing.SystemColors.ControlLight;
            this.wnd_action_icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.wnd_action_icon.Location = new System.Drawing.Point(253, 193);
            this.wnd_action_icon.Name = "wnd_action_icon";
            this.wnd_action_icon.Size = new System.Drawing.Size(27, 27);
            this.wnd_action_icon.TabIndex = 22;
            // 
            // wnd_select_class_box
            // 
            this.wnd_select_class_box.Location = new System.Drawing.Point(108, 91);
            this.wnd_select_class_box.Name = "wnd_select_class_box";
            this.wnd_select_class_box.Size = new System.Drawing.Size(171, 23);
            this.wnd_select_class_box.TabIndex = 5;
            // 
            // screen
            // 
            this.screen.Controls.Add(this.screen_draw);
            this.screen.Controls.Add(this.screen_get);
            this.screen.Controls.Add(this.screen_img);
            this.screen.Location = new System.Drawing.Point(663, 14);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(282, 202);
            this.screen.TabIndex = 10;
            this.screen.TabStop = false;
            this.screen.Text = "CC-Functions.W32.ScreenMan";
            // 
            // screen_draw
            // 
            this.screen_draw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.screen_draw.Location = new System.Drawing.Point(188, 168);
            this.screen_draw.Name = "screen_draw";
            this.screen_draw.Size = new System.Drawing.Size(87, 27);
            this.screen_draw.TabIndex = 3;
            this.screen_draw.Text = "Draw";
            this.screen_draw.UseVisualStyleBackColor = true;
            this.screen_draw.Click += new System.EventHandler(this.screen_draw_Click);
            // 
            // screen_get
            // 
            this.screen_get.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.screen_get.Location = new System.Drawing.Point(7, 168);
            this.screen_get.Name = "screen_get";
            this.screen_get.Size = new System.Drawing.Size(87, 27);
            this.screen_get.TabIndex = 2;
            this.screen_get.Text = "Get";
            this.screen_get.UseVisualStyleBackColor = true;
            this.screen_get.Click += new System.EventHandler(this.screen_get_Click);
            // 
            // screen_img
            // 
            this.screen_img.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.screen_img.BackColor = System.Drawing.Color.White;
            this.screen_img.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.screen_img.Location = new System.Drawing.Point(7, 22);
            this.screen_img.Name = "screen_img";
            this.screen_img.Size = new System.Drawing.Size(268, 140);
            this.screen_img.TabIndex = 1;
            // 
            // reader
            // 
            this.reader.Controls.Add(this.readerFlow);
            this.reader.Location = new System.Drawing.Point(308, 420);
            this.reader.Name = "reader";
            this.reader.Size = new System.Drawing.Size(637, 171);
            this.reader.TabIndex = 11;
            this.reader.TabStop = false;
            this.reader.Text = "CC-Functions.W32.KeyboardReader";
            // 
            // readerFlow
            // 
            this.readerFlow.AutoScroll = true;
            this.readerFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readerFlow.Location = new System.Drawing.Point(3, 19);
            this.readerFlow.Name = "readerFlow";
            this.readerFlow.Size = new System.Drawing.Size(631, 149);
            this.readerFlow.TabIndex = 0;
            // 
            // desk
            // 
            this.desk.Controls.Add(this.desk_draw);
            this.desk.Controls.Add(this.desk_set);
            this.desk.Controls.Add(this.desk_get);
            this.desk.Controls.Add(this.desk_back);
            this.desk.Location = new System.Drawing.Point(663, 224);
            this.desk.Name = "desk";
            this.desk.Size = new System.Drawing.Size(282, 189);
            this.desk.TabIndex = 11;
            this.desk.TabStop = false;
            this.desk.Text = "CC-Functions.W32.DeskMan";
            // 
            // desk_draw
            // 
            this.desk_draw.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.desk_draw.Location = new System.Drawing.Point(188, 156);
            this.desk_draw.Name = "desk_draw";
            this.desk_draw.Size = new System.Drawing.Size(87, 27);
            this.desk_draw.TabIndex = 3;
            this.desk_draw.Text = "Draw";
            this.desk_draw.UseVisualStyleBackColor = true;
            this.desk_draw.Click += new System.EventHandler(this.desk_draw_Click);
            // 
            // desk_set
            // 
            this.desk_set.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.desk_set.Location = new System.Drawing.Point(101, 156);
            this.desk_set.Name = "desk_set";
            this.desk_set.Size = new System.Drawing.Size(79, 27);
            this.desk_set.TabIndex = 2;
            this.desk_set.Text = "Set";
            this.desk_set.UseVisualStyleBackColor = true;
            this.desk_set.Click += new System.EventHandler(this.desk_set_Click);
            // 
            // desk_get
            // 
            this.desk_get.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.desk_get.Location = new System.Drawing.Point(7, 156);
            this.desk_get.Name = "desk_get";
            this.desk_get.Size = new System.Drawing.Size(87, 27);
            this.desk_get.TabIndex = 1;
            this.desk_get.Text = "Get";
            this.desk_get.UseVisualStyleBackColor = true;
            this.desk_get.Click += new System.EventHandler(this.desk_get_Click);
            // 
            // desk_back
            // 
            this.desk_back.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.desk_back.BackColor = System.Drawing.Color.White;
            this.desk_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.desk_back.Location = new System.Drawing.Point(7, 22);
            this.desk_back.Name = "desk_back";
            this.desk_back.Size = new System.Drawing.Size(268, 127);
            this.desk_back.TabIndex = 0;
            // 
            // readerUpdate
            // 
            this.readerUpdate.Enabled = true;
            this.readerUpdate.Interval = 50;
            this.readerUpdate.Tick += new System.EventHandler(this.readerUpdate_Tick);
            // 
            // time
            // 
            this.time.Controls.Add(this.time_set);
            this.time.Controls.Add(this.time_select);
            this.time.Location = new System.Drawing.Point(14, 486);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(287, 102);
            this.time.TabIndex = 12;
            this.time.TabStop = false;
            this.time.Text = "CC-Functions.W32.Time";
            // 
            // time_set
            // 
            this.time_set.Location = new System.Drawing.Point(217, 22);
            this.time_set.Name = "time_set";
            this.time_set.Size = new System.Drawing.Size(64, 27);
            this.time_set.TabIndex = 1;
            this.time_set.Text = "Set";
            this.time_set.UseVisualStyleBackColor = true;
            this.time_set.Click += new System.EventHandler(this.time_set_Click);
            // 
            // time_select
            // 
            this.time_select.Location = new System.Drawing.Point(6, 27);
            this.time_select.Name = "time_select";
            this.time_select.Size = new System.Drawing.Size(205, 23);
            this.time_select.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 595);
            this.Controls.Add(this.time);
            this.Controls.Add(this.desk);
            this.Controls.Add(this.reader);
            this.Controls.Add(this.screen);
            this.Controls.Add(this.keyboard);
            this.Controls.Add(this.mouse);
            this.Controls.Add(this.power);
            this.Controls.Add(this.wnd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
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
            this.screen.ResumeLayout(false);
            this.reader.ResumeLayout(false);
            this.desk.ResumeLayout(false);
            this.time.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.Button wnd_select_class_button;
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
        private System.Windows.Forms.GroupBox screen;
        private System.Windows.Forms.GroupBox reader;
        private System.Windows.Forms.GroupBox desk;
        private System.Windows.Forms.FlowLayoutPanel readerFlow;
        private System.Windows.Forms.Timer readerUpdate;
        private System.Windows.Forms.Panel desk_back;
        private System.Windows.Forms.Button desk_get;
        private System.Windows.Forms.Button desk_set;
        private System.Windows.Forms.Button desk_draw;
        private System.Windows.Forms.Panel screen_img;
        private System.Windows.Forms.Button screen_get;
        private System.Windows.Forms.Button screen_draw;
        private System.Windows.Forms.GroupBox time;
        private System.Windows.Forms.DateTimePicker time_select;
        private System.Windows.Forms.Button time_set;
        private System.Windows.Forms.Button wnd_select_child;
        private System.Windows.Forms.Button wnd_select_parent;
    }
}