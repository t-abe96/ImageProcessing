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
                    // 最も見つかった場所に赤枠を表示
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
        /* 
         * 
         * テンプレートマッチング2
         * 
         */
        private void button4_Click(object sender, EventArgs e)
        {
            // 検索対象の画像とテンプレート画像
            using (Mat mat = new Mat(@"D:\cs_source\img\neko2.jpg"))
            using (Mat temp = new Mat(@"D:\cs_source\img\template1.jpg"))
            using (Mat result = new Mat())
            {
                // テンプレートマッチ
                Cv2.MatchTemplate(mat, temp, result, TemplateMatchModes.CCoeffNormed);

                // 閾値の範囲に絞る
                Cv2.Threshold(result, result, 0.8, 1.0, ThresholdTypes.Tozero);

                while (true)
                {
                    // 類似度が最大/最少となる画素の位置を調べる
                    OpenCvSharp.Point minloc, maxloc;
                    double minval, maxval;
                    Cv2.MinMaxLoc(result, out minval, out maxval, out minloc, out maxloc);

                    var threshold = 0.8;
                    if (maxval >= threshold)
                    {
                        // 見つかった場所に赤枠を表示
                        Rect rect = new Rect(maxloc.X, maxloc.Y, temp.Width, temp.Height);
                        Cv2.Rectangle(mat, rect, new OpenCvSharp.Scalar(0, 0, 255), 2);

                        // 見つかった個所は塗りつぶす
                        Rect outRect;
                        Cv2.FloodFill(result, maxloc, new OpenCvSharp.Scalar(0), out outRect, new OpenCvSharp.Scalar(0.1),
                            new OpenCvSharp.Scalar(1.0), FloodFillFlags.Link4);
                    } else
                    {
                        break;
                    }
                }
                // ウィンドウに画像を表示
                Cv2.ImShow("temlate2_show", mat);
            }
        }
    }
}
