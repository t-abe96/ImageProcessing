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
        
        /* 
         * 
         * 画像をウィンドウに表示
         * 
         */
        private void button1_Click(object sender, EventArgs e)
        {
            // 画像の読み込み
            using (Mat mat = new Mat(@"D:\cs_source\img\neko.jpg"))
            {
                // 画像をウィンドウに表示
                Cv2.ImShow("sample_show", mat);

                // 画像を保存
                Cv2.ImWrite(@"D:\cs_source\img\output.jpg", mat);
                // 切り抜いた画像を保存
                var mat2 = mat.Clone(new Rect(100, 100, 200, 150));
                Cv2.ImWrite(@"D:\cs_source\img\output_cut.jpg", mat2);
            }
        }

        /* 
         * 
         * グレースケール処理
         * 
         */
        private void button2_Click(object sender, EventArgs e)
        {
            // 画像の読み込み
            using (Mat mat = new Mat(@"D:\cs_source\img\neko.jpg"))
            using (Mat matGray = mat.CvtColor(ColorConversionCodes.BGR2GRAY))
            {
                // 画像をウィンドウに表示
                Cv2.ImShow("grayscale_show", matGray);

                // 画像を保存
                Cv2.ImWrite(@"D:\cs_source\img\output.jpg", mat);
                // 切り抜いた画像を保存
                var mat2 = mat.Clone(new Rect(100, 100, 200, 150));
                Cv2.ImWrite(@"D:\cs_source\img\output_cut.jpg", mat2);
            }
        }

        /* 
         * 
         * テンプレートマッチング
         * 
         */
        private void button3_Click(object sender, EventArgs e)
        {
            using (Mat mat = new Mat(@"D:\cs_source\img\neko.jpg"))
            using (Mat temp = new Mat(@"D:\cs_source\img\template1.jpg"))
            using (Mat result = new Mat())
            {
                // テンプレートマッチ
                Cv2.MatchTemplate(mat, temp, result, TemplateMatchModes.CCoeffNormed);

                // 類似度が最大/最少となる画素の位置を調べる
                OpenCvSharp.Point minloc, maxloc;
                double minval, maxval;
                Cv2.MinMaxLoc(result, out minval, out maxval, out minloc, out maxloc);

                // 閾値で判断
                var thereshold = 0.9;
                if (maxval >= thereshold)
                {
                    // 最も見るかった場所に赤枠を表示
                    Rect rect = new Rect(maxloc.X, maxloc.Y, temp.Width, temp.Height);
                    Cv2.Rectangle(mat, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);

                    // ウィンドウに画像を表示
                    Cv2.ImShow("template1_show", mat);
                }
                else
                {
                    // 見つからない場合
                    MessageBox.Show("見つかりませんでした");
                }
            }
        }


    }
}
