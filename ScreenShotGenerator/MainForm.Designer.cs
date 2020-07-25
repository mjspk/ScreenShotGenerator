namespace ScreenShotGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MockUpPic = new System.Windows.Forms.PictureBox();
            this.SelectMockupBtn = new System.Windows.Forms.Button();
            this.SelectScreenShotsBtn = new System.Windows.Forms.Button();
            this.GenerateBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MockUpPic)).BeginInit();
            this.SuspendLayout();
            // 
            // MockUpPic
            // 
            this.MockUpPic.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MockUpPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MockUpPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MockUpPic.Location = new System.Drawing.Point(12, 12);
            this.MockUpPic.Name = "MockUpPic";
            this.MockUpPic.Size = new System.Drawing.Size(262, 475);
            this.MockUpPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MockUpPic.TabIndex = 0;
            this.MockUpPic.TabStop = false;
            this.MockUpPic.Paint += new System.Windows.Forms.PaintEventHandler(this.MockUpPic_Paint);
            this.MockUpPic.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MockUpPic_MouseDown);
            this.MockUpPic.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MockUpPic_MouseMove);
            // 
            // SelectMockupBtn
            // 
            this.SelectMockupBtn.Location = new System.Drawing.Point(12, 497);
            this.SelectMockupBtn.Name = "SelectMockupBtn";
            this.SelectMockupBtn.Size = new System.Drawing.Size(118, 38);
            this.SelectMockupBtn.TabIndex = 1;
            this.SelectMockupBtn.Text = "Select Mockup";
            this.SelectMockupBtn.UseVisualStyleBackColor = true;
            this.SelectMockupBtn.Click += new System.EventHandler(this.SelectMockupBtn_Click);
            // 
            // SelectScreenShotsBtn
            // 
            this.SelectScreenShotsBtn.Location = new System.Drawing.Point(156, 497);
            this.SelectScreenShotsBtn.Name = "SelectScreenShotsBtn";
            this.SelectScreenShotsBtn.Size = new System.Drawing.Size(118, 38);
            this.SelectScreenShotsBtn.TabIndex = 2;
            this.SelectScreenShotsBtn.Text = "Select ScreenShots";
            this.SelectScreenShotsBtn.UseVisualStyleBackColor = true;
            this.SelectScreenShotsBtn.Click += new System.EventHandler(this.SelectScreenShotsBtn_Click);
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(12, 541);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(262, 38);
            this.GenerateBtn.TabIndex = 3;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 592);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.SelectScreenShotsBtn);
            this.Controls.Add(this.SelectMockupBtn);
            this.Controls.Add(this.MockUpPic);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "ScreenShotGenerator";
            ((System.ComponentModel.ISupportInitialize)(this.MockUpPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox MockUpPic;
        private System.Windows.Forms.Button SelectMockupBtn;
        private System.Windows.Forms.Button SelectScreenShotsBtn;
        private System.Windows.Forms.Button GenerateBtn;
    }
}