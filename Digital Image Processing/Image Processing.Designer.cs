namespace Digital_Image_Processing
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnUpload = new Button();
            btnGray = new Button();
            btnToggleCamera = new Button();
            btnNoFilter = new Button();
            btnHistogram = new Button();
            btnInvert = new Button();
            label1 = new Label();
            btnSepia = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(32, 33);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(468, 398);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(549, 33);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(468, 398);
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(138, 446);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(94, 53);
            btnUpload.TabIndex = 2;
            btnUpload.Text = "Upload Image";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // btnGray
            // 
            btnGray.Location = new Point(650, 446);
            btnGray.Name = "btnGray";
            btnGray.Size = new Size(94, 29);
            btnGray.TabIndex = 3;
            btnGray.Text = "Gray";
            btnGray.UseVisualStyleBackColor = true;
            btnGray.Click += btnGray_Click;
            // 
            // btnToggleCamera
            // 
            btnToggleCamera.Location = new Point(284, 446);
            btnToggleCamera.Name = "btnToggleCamera";
            btnToggleCamera.Size = new Size(94, 53);
            btnToggleCamera.TabIndex = 4;
            btnToggleCamera.Text = "Use Camera";
            btnToggleCamera.UseVisualStyleBackColor = true;
            btnToggleCamera.Click += btnToggleCamera_Click;
            // 
            // btnNoFilter
            // 
            btnNoFilter.Location = new Point(550, 446);
            btnNoFilter.Name = "btnNoFilter";
            btnNoFilter.Size = new Size(94, 29);
            btnNoFilter.TabIndex = 5;
            btnNoFilter.Text = "No Filter";
            btnNoFilter.UseVisualStyleBackColor = true;
            btnNoFilter.Click += btnNoFilter_Click;
            // 
            // btnHistogram
            // 
            btnHistogram.Location = new Point(750, 446);
            btnHistogram.Name = "btnHistogram";
            btnHistogram.Size = new Size(94, 29);
            btnHistogram.TabIndex = 6;
            btnHistogram.Text = "Histogram";
            btnHistogram.UseVisualStyleBackColor = true;
            btnHistogram.Click += btnHistogram_Click;
            // 
            // btnInvert
            // 
            btnInvert.Location = new Point(850, 446);
            btnInvert.Name = "btnInvert";
            btnInvert.Size = new Size(94, 29);
            btnInvert.TabIndex = 7;
            btnInvert.Text = "Inverted";
            btnInvert.UseVisualStyleBackColor = true;
            btnInvert.Click += btnInvert_Click;
            // 
            // label1
            // 
            label1.Location = new Point(284, 502);
            label1.Name = "label1";
            label1.Size = new Size(216, 45);
            label1.TabIndex = 8;
            label1.Text = "// Camera display works but Camera output is capture smh";
            // 
            // btnSepia
            // 
            btnSepia.Location = new Point(550, 481);
            btnSepia.Name = "btnSepia";
            btnSepia.Size = new Size(94, 29);
            btnSepia.TabIndex = 9;
            btnSepia.Text = "Sepia";
            btnSepia.UseVisualStyleBackColor = true;
            btnSepia.Click += btnSepia_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1047, 556);
            Controls.Add(btnSepia);
            Controls.Add(label1);
            Controls.Add(btnInvert);
            Controls.Add(btnHistogram);
            Controls.Add(btnNoFilter);
            Controls.Add(btnToggleCamera);
            Controls.Add(btnGray);
            Controls.Add(btnUpload);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button btnUpload;
        private Button btnGray;
        private Button btnToggleCamera;
        private Button btnNoFilter;
        private Button btnHistogram;
        private Button btnInvert;
        private Label label1;
        private Button btnSepia;
    }
}
