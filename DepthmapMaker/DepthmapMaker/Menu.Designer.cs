namespace DepthmapMaker
{
    partial class Menu
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
            this.DiffuseBar = new System.Windows.Forms.TrackBar();
            this.diffuseLabel = new System.Windows.Forms.Label();
            this.DiffuseUpdown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.AttenuationBar = new System.Windows.Forms.TrackBar();
            this.AttenuationUpDown = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.DiffuseBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiffuseUpdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttenuationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttenuationUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // DiffuseBar
            // 
            this.DiffuseBar.Location = new System.Drawing.Point(12, 50);
            this.DiffuseBar.Maximum = 100;
            this.DiffuseBar.Name = "DiffuseBar";
            this.DiffuseBar.Size = new System.Drawing.Size(357, 45);
            this.DiffuseBar.TabIndex = 0;
            this.DiffuseBar.Value = 100;
            this.DiffuseBar.ValueChanged += new System.EventHandler(this.DiffuseBar_ValueChanged_1);
            // 
            // diffuseLabel
            // 
            this.diffuseLabel.AutoSize = true;
            this.diffuseLabel.Location = new System.Drawing.Point(12, 32);
            this.diffuseLabel.Name = "diffuseLabel";
            this.diffuseLabel.Size = new System.Drawing.Size(74, 15);
            this.diffuseLabel.TabIndex = 1;
            this.diffuseLabel.Tag = "light diffuse";
            this.diffuseLabel.Text = "Light Diffuse";
            // 
            // DiffuseUpdown
            // 
            this.DiffuseUpdown.DecimalPlaces = 1;
            this.DiffuseUpdown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.DiffuseUpdown.Location = new System.Drawing.Point(375, 50);
            this.DiffuseUpdown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.DiffuseUpdown.Name = "DiffuseUpdown";
            this.DiffuseUpdown.Size = new System.Drawing.Size(120, 23);
            this.DiffuseUpdown.TabIndex = 3;
            this.DiffuseUpdown.Value = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.DiffuseUpdown.ValueChanged += new System.EventHandler(this.DiffuseUpdown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Light Attenuation";
            // 
            // AttenuationBar
            // 
            this.AttenuationBar.Location = new System.Drawing.Point(12, 116);
            this.AttenuationBar.Maximum = 100;
            this.AttenuationBar.Name = "AttenuationBar";
            this.AttenuationBar.Size = new System.Drawing.Size(357, 45);
            this.AttenuationBar.TabIndex = 5;
            this.AttenuationBar.Value = 1;
            this.AttenuationBar.ValueChanged += new System.EventHandler(this.AttenuationBar_ValueChanged);
            // 
            // AttenuationUpDown
            // 
            this.AttenuationUpDown.DecimalPlaces = 2;
            this.AttenuationUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.AttenuationUpDown.Location = new System.Drawing.Point(375, 116);
            this.AttenuationUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AttenuationUpDown.Name = "AttenuationUpDown";
            this.AttenuationUpDown.Size = new System.Drawing.Size(120, 23);
            this.AttenuationUpDown.TabIndex = 6;
            this.AttenuationUpDown.ValueChanged += new System.EventHandler(this.AttenuationUpDown_ValueChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(56, 167);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(376, 91);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save Image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 275);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.AttenuationUpDown);
            this.Controls.Add(this.AttenuationBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DiffuseUpdown);
            this.Controls.Add(this.diffuseLabel);
            this.Controls.Add(this.DiffuseBar);
            this.Name = "Menu";
            this.Text = "ViewerMenu";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DiffuseBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiffuseUpdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttenuationBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttenuationUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBar DiffuseBar;
        private Label diffuseLabel;
        private NumericUpDown DiffuseUpdown;
        private Label label1;
        private TrackBar AttenuationBar;
        private NumericUpDown AttenuationUpDown;
        private Button saveButton;
        private SaveFileDialog saveFileDialog1;
    }
}