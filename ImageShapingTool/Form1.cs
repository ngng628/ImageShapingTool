using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageShapingTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "画像ファイル (*.jpg;*png;*.gif;*.bmp;*.exig;*.tif;*.wmf;*.emf)|*.jpg;*png;*.gif;*.bmp;*.exig;*.tif;*.wmf;*.emf|All Files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in ofd.FileNames)
                {
                    textBox1.Text += fileName + Environment.NewLine;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string extension = comboBox1.Text;
            if (extension == "")
            {
                MessageBox.Show("拡張子を設定してください。", 
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            var paths = textBox1.Text.Replace("\r\n", "\n").Split(new[] { '\n', '\r' });

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "変換先フォルダの指定";
            string saveFolder = Environment.SpecialFolder.Desktop.ToString();
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                saveFolder = fbd.SelectedPath + "/";
            }

            int height = int.Parse(heightSize.Text);
            int width = int.Parse(widthSize.Text);

            int index = 1;
            foreach (string path in paths)
            {
                if (path == "") continue;
                Bitmap image = new Bitmap(Image.FromFile(path), width, height);
                String filename = checkBox2.Checked
                    ? String.Format("{0:D5}", index) + extension
                    : Path.GetFileNameWithoutExtension(path) + extension;
                image.Save(saveFolder + filename, StringToImageFormat(extension));
                image.Dispose();
                index++;
            }
        }

        private ImageFormat StringToImageFormat(String format)
        {
            switch (format)
            {
                case ".png":
                    return ImageFormat.Png;
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".gif":
                    return ImageFormat.Gif;
                case ".tif":
                    return ImageFormat.Tiff;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".emf":
                    return ImageFormat.Emf;
                case ".wmf":
                    return ImageFormat.Wmf;
                default:
                    return null;
            }
        }

        private void widthSize_Leave(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                heightSize.Text = widthSize.Text;
            }
        }

        private void heightSize_Leave(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                widthSize.Text = heightSize.Text;
            }
        }

        private void 閉じるCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
