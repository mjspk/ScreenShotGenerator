using ScreenShotGenerator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenShotGenerator
{
    public partial class MainForm : Form
    {
        private int Hr;
        private int Wr;
        private Point RectStartPoint;
        private Rectangle Rect = new Rectangle();
        private Rectangle RealRect;
        private string path { get { return Path.Combine(Settings.Default.FolderName, "ScreenShot Generator Result"); } }

        public MainForm()
        {
            InitializeComponent();
        }

        private void SelectMockupBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    MockUpPic.Image = Image.FromFile(dlg.FileName);
                    bitmap = new Bitmap(MockUpPic.Image);
                    gr = Graphics.FromImage(bitmap);
                    Hr = Math.Abs(MockUpPic.Image.Height / MockUpPic.Height);
                    Wr = Math.Abs(MockUpPic.Image.Width / MockUpPic.Width);
                }

                dlg.Dispose();
            }
            catch
            {

            }
        }
        List<Image> ScreenShotsList = new List<Image>();
        private Bitmap bitmap;
        private Graphics gr;

        private void SelectScreenShotsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Title = "Open Image";
                dlg.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
                dlg.Multiselect = true;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    foreach (var item in dlg.FileNames)
                    {
                        ScreenShotsList.Add(Image.FromFile(item));
                    }
                }

                dlg.Dispose();
            }
            catch
            {

            }
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            if (MockUpPic.Image!=null)
            {
                if (ScreenShotsList.Count > 0)
                {
                    if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                    {
                        Generate();
                    }
                    else
                    {
                        MessageBox.Show("please select your screen shot location on the mockup");
                    }
                }
                else
                {
                    MessageBox.Show("please select some screen shots");
                }
            }
            else
            {
                MessageBox.Show("please select your mockup first");
            }
           
        }
        private void MockUpPic_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                RectStartPoint = e.Location;
                Invalidate();
            }
            catch
            {

            }

        }
        private void MockUpPic_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Left)
                    return;
                Point tempEndPoint = e.Location;
                Rect.Location = new Point(
                    Math.Min(RectStartPoint.X, tempEndPoint.X),
                    Math.Min(RectStartPoint.Y, tempEndPoint.Y));
                Rect.Size = new Size(
                    Math.Abs(RectStartPoint.X - tempEndPoint.X),
                    Math.Abs(RectStartPoint.Y - tempEndPoint.Y));

                RealRect.Location = new Point(
                     Math.Min(TranslateZoomMousePosition(RectStartPoint).X, TranslateZoomMousePosition(tempEndPoint).X),
                     Math.Min(TranslateZoomMousePosition(RectStartPoint).Y, TranslateZoomMousePosition(tempEndPoint).Y));
                RealRect.Size = new Size(
                    Math.Abs(TranslateZoomMousePosition(RectStartPoint).X - TranslateZoomMousePosition(tempEndPoint).X),
                    Math.Abs(TranslateZoomMousePosition(RectStartPoint).Y - TranslateZoomMousePosition(tempEndPoint).Y));

                MockUpPic.Invalidate();
            }
            catch
            {

            }


        }

        private Image RoundCorners(Image image, int cornerRadius)
        {
            cornerRadius *= 2;
            Bitmap roundedImage = new Bitmap(image.Width, image.Height);
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(0, 0, cornerRadius, cornerRadius, 180, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0, cornerRadius, cornerRadius, 270, 90);
            gp.AddArc(0 + roundedImage.Width - cornerRadius, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gp.AddArc(0, 0 + roundedImage.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            using (Graphics g = Graphics.FromImage(roundedImage))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.SetClip(gp);
                g.DrawImage(image, Point.Empty);
            }
            return roundedImage;
        }
        private void MockUpPic_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            try
            {
                if (MockUpPic.Image != null)
                {
                    if (Rect != null && Rect.Width > 0 && Rect.Height > 0)
                    {
                        if (ScreenShotsList.Count > 0)
                        { 
                            var roundedImage = RoundCorners(ScreenShotsList.FirstOrDefault(), 60);
                            e.Graphics.DrawImage(roundedImage, Rect);
                        }
                        else
                        {
                            MessageBox.Show("please select some screen shots");
                        }
                    }
                }
            }
            catch
            {

            }

        }
        protected Point TranslateZoomMousePosition(Point coordinates)
        {
            if (MockUpPic.Image == null) return coordinates;

            if (MockUpPic.Width == 0 || MockUpPic.Height == 0 || MockUpPic.Image.Width == 0 || MockUpPic.Image.Height == 0)
                return coordinates;

            float imageAspect = (float)MockUpPic.Image.Width / MockUpPic.Image.Height;
            float controlAspect = (float)MockUpPic.Width / MockUpPic.Height;
            float newX = coordinates.X;
            float newY = coordinates.Y;
            try
            {
                if (imageAspect > controlAspect)
                {
                    // This means that we are limited by width, 
                    // meaning the image fills up the entire control from left to right
                    float ratioWidth = (float)MockUpPic.Image.Width / MockUpPic.Width;
                    newX *= ratioWidth;
                    float scale = (float)MockUpPic.Width / MockUpPic.Image.Width;
                    float displayHeight = scale * MockUpPic.Image.Height;
                    float diffHeight = MockUpPic.Height - displayHeight;
                    diffHeight /= 2;
                    newY -= diffHeight;
                    newY /= scale;
                }
                else
                {
                    // This means that we are limited by height, 
                    // meaning the image fills up the entire control from top to bottom
                    float ratioHeight = (float)MockUpPic.Image.Height / Height;
                    newY *= ratioHeight;
                    float scale = (float)MockUpPic.Height / MockUpPic.Image.Height;
                    float displayWidth = scale * MockUpPic.Image.Width;
                    float diffWidth = MockUpPic.Width - displayWidth;
                    diffWidth /= 2;
                    newX -= diffWidth;
                    newX /= scale;
                }
            }
            catch
            {

            }


            return new Point((int)newX, (int)newY);
        }
        public void AddStampAndSave(Image image)
        {
            try
            {
                var roundedImage = RoundCorners(image, 60);
                gr.DrawImage(roundedImage, RealRect);
                bitmap.Save(Path.Combine(path, DateTime.Now.ToString("yyyyMMddHHmmss") + ".png"), ImageFormat.Png);
            }
            catch
            {

            }

        }
        public async void Generate()
        {
            this.Enabled = false;
            try
            {

                if (string.IsNullOrEmpty(Settings.Default.FolderName) || !Directory.Exists(path))
                {
                    using (var fbd = new FolderBrowserDialog())
                    {
                        DialogResult result = fbd.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        {
                            Settings.Default.FolderName = fbd.SelectedPath;
                            Settings.Default.Save();
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);
                            Generate();
                        }
                        this.Enabled = true;

                    }
                }
                else
                {
                    await Task.Run(async () =>
                    {
                        foreach (var item in ScreenShotsList)
                        {
                            await Task.Delay(900);
                            AddStampAndSave(item);
                        }
                    });
                    this.Enabled = true;
                    Process.Start("explorer.exe", path);

                }

            }
            catch
            {

            }

        }

    }
}
