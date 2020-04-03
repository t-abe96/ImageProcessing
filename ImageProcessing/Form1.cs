using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 画像の読み込み
            using (Mat mat = new Mat(@"D:\cs_source\img\neko.jpg"))
            {
                // 画像をウィンドウに表示
                Cv2.ImShow("sample_show", mat);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 画像の読み込み
            using (Mat mat = new Mat(@"D:\cs_source\img\neko.jpg"))
            using (Mat matGray = mat.CvtColor(ColorConversionCodes.BGR2GRAY))
            {
                // 画像をウィンドウに表示
                Cv2.ImShow("grayscale_show", matGray);
            }
        }
    }
}
