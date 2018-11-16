using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KamiModpackBuilder.Forms
{
    public partial class ImagePreview : Form
    {

        private float WidthRatio;

        public Image image { set {
                pictureBoxImage.BackgroundImage = value;
                WidthRatio = (float)value.Width / (float)value.Height;
                Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
                int titleHeight = screenRectangle.Top - this.Top;
                Size = new Size(value.Width + 6,value.Height + titleHeight + 6); } }

        public ImagePreview()
        {
            InitializeComponent();
        }

        private void ImagePreview_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxImage.Height = panel1.Height;
            pictureBoxImage.Width = (int)(pictureBoxImage.Height * WidthRatio);
            if (pictureBoxImage.Width > Width)
            {
                pictureBoxImage.Width = panel1.Width;
                pictureBoxImage.Height = (int)(pictureBoxImage.Width / WidthRatio);
            }
        }
    }
}
