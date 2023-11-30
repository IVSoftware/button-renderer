namespace button_renderer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new ButtonEx();
            button2 = new ButtonEx();
            button3 = new ButtonEx();
            comboBoxTimers = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(40, 40, 40);
            button1.FocusRectangleColor = Color.Fuchsia;
            button1.Font = new Font("Segoe UI", 12F);
            button1.ForeColor = Color.LightGray;
            button1.Location = new Point(42, 100);
            button1.Name = "button1";
            button1.Size = new Size(135, 56);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(40, 40, 40);
            button2.FocusRectangleColor = Color.Fuchsia;
            button2.Font = new Font("Segoe UI", 12F);
            button2.ForeColor = Color.LightGray;
            button2.Location = new Point(208, 100);
            button2.Name = "button2";
            button2.Size = new Size(135, 56);
            button2.TabIndex = 1;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(40, 40, 40);
            button3.FocusRectangleColor = Color.Fuchsia;
            button3.Font = new Font("Segoe UI", 12F);
            button3.ForeColor = Color.LightGray;
            button3.Location = new Point(369, 100);
            button3.Name = "button3";
            button3.Size = new Size(135, 56);
            button3.TabIndex = 2;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = false;
            // 
            // comboBoxTimers
            // 
            comboBoxTimers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTimers.Font = new Font("Segoe UI", 12F);
            comboBoxTimers.FormattingEnabled = true;
            comboBoxTimers.Location = new Point(208, 28);
            comboBoxTimers.Name = "comboBoxTimers";
            comboBoxTimers.Size = new Size(296, 40);
            comboBoxTimers.TabIndex = 3;
            comboBoxTimers.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(82, 37);
            label1.Name = "label1";
            label1.Size = new Size(64, 25);
            label1.TabIndex = 4;
            label1.Text = "Timers";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 244);
            Controls.Add(label1);
            Controls.Add(comboBoxTimers);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ButtonEx button1;
        private ButtonEx button2;
        private ButtonEx button3;
        private ComboBox comboBoxTimers;
        private Label label1;
    }
}
