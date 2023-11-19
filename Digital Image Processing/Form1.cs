using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;


namespace Digital_Image_Processing
{
    public partial class Form1 : Form
    {
        private bool isCameraView = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;


        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (IsImageFile(openFile.FileName))
                    {
                        // load img
                        Image loadedImage = Image.FromFile(openFile.FileName);

                        // set image size to fit the pic box
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                        // load picbox1
                        pictureBox1.Image = loadedImage;
                    }
                    else
                    {
                        MessageBox.Show("Invalid image file selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //CHECKPOINT!!!!!!!!!!!!!!!!!!!!!!!
        //Exception handling codes!! Must put para no error ig run

        //Exception 1: Wrong image format prompt
        private bool IsImageFile(string filePath)
        {
            try
            {
                using (var bitmap = new Bitmap(filePath))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        //STOP camera run after exit spp

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }


        //CAMERA LIVE FEED DISPLAY
        private void btnToggleCamera_Click(object sender, EventArgs e)
        {
            if (isCameraView)
            {
                // CAMERA --> FILE UPLOAD
                isCameraView = false;
                btnUpload.Enabled = true; // upload enable
                btnToggleCamera.Text = "Switch to Camera View";
                pictureBox1.Image = null; // Clear 
                StopCameraView();

            }
            else
            {
                // FILE UPLOAD --> CAMERA
                isCameraView = true;
                btnUpload.Enabled = false; // Disable upload
                btnToggleCamera.Text = "Switch to File Upload View";
                StartCameraView(); // display cam
            }
        }


        private void StartCameraView()
        {
            // scan device camera
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count > 0)
            {
                //general camera
                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += VideoSource_NewFrame;
                videoSource.Start();
            }
            else
            {
                MessageBox.Show("No video devices found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopCameraView()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        //CAMERA VIDEO CODES ARI


        //private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //    // Handle each new frame from the camera here
        //    // The event is triggered for each new frame received from the camera
        //    Image cameraFrame = (Image)eventArgs.Frame.Clone();

        //    // Update the PictureBox with the new frame
        //    pictureBox1.Invoke((MethodInvoker)delegate
        //    {
        //        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Set SizeMode to Zoom
        //        pictureBox1.Image = cameraFrame;

        //        if (btnGray.Enabled == false)
        //        {
        //            Image grayscaleFrame = ConvertToGrayscale((Bitmap)cameraFrame.Clone());
        //            Image copyFrame = CopyImage((Bitmap)cameraFrame.Clone());

        //            // Decide which image to display in pictureBox2
        //            if (btnNoFilter.Enabled == false)
        //            {
        //                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        //                pictureBox2.Image = copyFrame;
        //            }
        //            else
        //            {
        //                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
        //                pictureBox2.Image = grayscaleFrame;
        //            }
        //        }
        //    });
        //}

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //CLONE CAM
            Image cameraFrame = (Image)eventArgs.Frame.Clone();

            // update picbox1
            pictureBox1.Invoke((MethodInvoker)delegate
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Set SizeMode to Zoom
                pictureBox1.Image = cameraFrame;

                if (btnNoFilter.Enabled == false)
                {
                    // If "Copy" button is enabled, display the live camera feed in pictureBox2
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = cameraFrame;
                }
                else if (btnGray.Enabled == false)
                {
                    // If "Gray" button is enabled, display the live grayscale feed in pictureBox2
                    Image grayscaleFrame = ConvertToGrayscale((Bitmap)cameraFrame);
                    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox2.Image = grayscaleFrame;
                }
            });
        }






        //IMAGE PROCESSING CODES ARI


        //1. GRAYSCALE (credits to CITU DIP METHODS REPOSITORY)
        private void btnGray_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // grayscale display to picbox2
                Image grayscaleImage = ConvertToGrayscale((Bitmap)pictureBox1.Image.Clone());
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = grayscaleImage;
            }
            else
            {
                MessageBox.Show("No image to process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image ConvertToGrayscale(Bitmap originalImage)
        {
            // Copy image
            Bitmap grayscaleImage = new Bitmap(originalImage.Width, originalImage.Height);

            //  LockBits
            BitmapData originalData = originalImage.LockBits(new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                                             ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData grayscaleData = grayscaleImage.LockBits(new Rectangle(0, 0, grayscaleImage.Width, grayscaleImage.Height),
                                                               ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0.ToPointer();
                byte* grayscalePtr = (byte*)grayscaleData.Scan0.ToPointer();

                int pixelSize = 3; // 24bits/pixel RGB

                for (int y = 0; y < originalImage.Height; y++)
                {
                    for (int x = 0; x < originalImage.Width; x++)
                    {
                        byte blue = originalPtr[0];
                        byte green = originalPtr[1];
                        byte red = originalPtr[2];

                        // GRAYSCALE FORMULA
                        byte grayscaleValue = (byte)(red * 0.3 + green * 0.59 + blue * 0.11);

                        // SET PIXEL
                        grayscalePtr[0] = grayscalePtr[1] = grayscalePtr[2] = grayscaleValue;

                        originalPtr += pixelSize;
                        grayscalePtr += pixelSize;
                    }

                    originalPtr += originalData.Stride - originalImage.Width * pixelSize;
                    grayscalePtr += grayscaleData.Stride - grayscaleImage.Width * pixelSize;
                }
            }

            // Unlock bits
            originalImage.UnlockBits(originalData);
            grayscaleImage.UnlockBits(grayscaleData);

            return grayscaleImage;
        }


        //2. COPY OR NO FILTER
        private void btnNoFilter_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                Image copyImage = CopyImage((Bitmap)pictureBox1.Image.Clone());
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = copyImage;
            }
            else
            {
                MessageBox.Show("No image to process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image CopyImage(Bitmap originalImage)
        {
            // Copy
            Bitmap copyImage = new Bitmap(originalImage.Width, originalImage.Height);

            // Iterate through each pixel in the original image
            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    // GET PIXEL
                    Color originalColor = originalImage.GetPixel(x, y);

                    // SET PIXEL
                    copyImage.SetPixel(x, y, originalColor);
                }
            }

            return copyImage;
        }



        //3. COLOR INVERSION
        //4. HISTOGRAM
        private void btnHistogram_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {

                Image histogramImage = HistogramImage((Bitmap)pictureBox1.Image.Clone());
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = histogramImage;
            }
            else
            {
                MessageBox.Show("No image to process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Image HistogramImage(Bitmap originalImage)
        {
            // Convert the original image to grayscale
            Bitmap grayscaleImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    Color originalColor = originalImage.GetPixel(x, y);

                    //            // GRAYSCALE FORMULA
                                byte grayscaleValue = (byte)(originalColor.R * 0.3 + originalColor.G * 0.59 + originalColor.B * 0.11);

                    //            // SET PIXEL
                                grayscaleImage.SetPixel(x, y, Color.FromArgb(grayscaleValue, grayscaleValue, grayscaleValue));
                }
            }

            // 1D histogram data
            int[] histData = new int[256];
            for (int x = 0; x < grayscaleImage.Width; x++)
            {
                for (int y = 0; y < grayscaleImage.Height; y++)
                {
                    Color sample = grayscaleImage.GetPixel(x, y);
                    histData[sample.R]++;
                }
            }

            // Display histogram image
            Bitmap histogramImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    histogramImage.SetPixel(x, y, Color.White);
                }
            }

            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < Math.Min(histData[x] / 5, histogramImage.Height - 1); y++)
                {
                    histogramImage.SetPixel(x, (histogramImage.Height - 1) - y, Color.Black);
                }
            }

            return histogramImage;
        }


        //5. SEPIA
    }
}
