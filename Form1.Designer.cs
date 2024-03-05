namespace TowersofHanoi
{
    partial class Form1
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
            Settings = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            GamePanel = new Panel();
            PoleCount = new NumericUpDown();
            Poles = new Label();
            Disks = new Label();
            DiskCount = new NumericUpDown();
            RedrawButton = new Button();
            ((System.ComponentModel.ISupportInitialize)PoleCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DiskCount).BeginInit();
            SuspendLayout();
            // 
            // Settings
            // 
            Settings.AutoSize = true;
            Settings.FlatStyle = FlatStyle.Flat;
            Settings.Font = new Font("Stencil", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Settings.ImageAlign = ContentAlignment.MiddleLeft;
            Settings.Location = new Point(1220, 30);
            Settings.Name = "Settings";
            Settings.Size = new Size(190, 44);
            Settings.TabIndex = 1;
            Settings.Text = "Settings";
            Settings.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GamePanel
            // 
            GamePanel.BorderStyle = BorderStyle.Fixed3D;
            GamePanel.Location = new Point(30, 30);
            GamePanel.Name = "GamePanel";
            GamePanel.Size = new Size(1180, 700);
            GamePanel.TabIndex = 2;
            // 
            // PoleCount
            // 
            PoleCount.Location = new Point(1352, 129);
            PoleCount.Margin = new Padding(30, 3, 30, 3);
            PoleCount.Maximum = new decimal(new int[] { 1500, 0, 0, 0 });
            PoleCount.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            PoleCount.Name = "PoleCount";
            PoleCount.Size = new Size(46, 27);
            PoleCount.TabIndex = 3;
            PoleCount.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // Poles
            // 
            Poles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            Poles.AutoSize = true;
            Poles.FlatStyle = FlatStyle.Flat;
            Poles.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Poles.ImageAlign = ContentAlignment.MiddleLeft;
            Poles.Location = new Point(1340, 102);
            Poles.Margin = new Padding(3, 0, 30, 0);
            Poles.Name = "Poles";
            Poles.Size = new Size(70, 24);
            Poles.TabIndex = 4;
            Poles.Text = "Poles";
            Poles.TextAlign = ContentAlignment.MiddleCenter;
            Poles.UseMnemonic = false;
            // 
            // Disks
            // 
            Disks.AutoSize = true;
            Disks.FlatStyle = FlatStyle.Flat;
            Disks.Font = new Font("Stencil", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Disks.ImageAlign = ContentAlignment.MiddleLeft;
            Disks.Location = new Point(1220, 102);
            Disks.Margin = new Padding(30, 0, 3, 0);
            Disks.Name = "Disks";
            Disks.Size = new Size(68, 24);
            Disks.TabIndex = 5;
            Disks.Text = "Disks";
            Disks.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DiskCount
            // 
            DiskCount.Location = new Point(1229, 129);
            DiskCount.Margin = new Padding(30, 3, 30, 3);
            DiskCount.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            DiskCount.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            DiskCount.Name = "DiskCount";
            DiskCount.Size = new Size(46, 27);
            DiskCount.TabIndex = 6;
            DiskCount.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // RedrawButton
            // 
            RedrawButton.Font = new Font("Stencil", 22.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RedrawButton.Location = new Point(1227, 525);
            RedrawButton.Name = "RedrawButton";
            RedrawButton.Size = new Size(183, 61);
            RedrawButton.TabIndex = 7;
            RedrawButton.Text = "Redraw";
            RedrawButton.UseVisualStyleBackColor = true;
            RedrawButton.Click += RedrawButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1422, 763);
            Controls.Add(RedrawButton);
            Controls.Add(PoleCount);
            Controls.Add(DiskCount);
            Controls.Add(Disks);
            Controls.Add(Settings);
            Controls.Add(Poles);
            Controls.Add(GamePanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)PoleCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)DiskCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Settings;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Panel GamePanel;
        private NumericUpDown PoleCount;
        private Label Poles;
        private Label Disks;
        private NumericUpDown DiskCount;
        private Button RedrawButton;
    }
}
