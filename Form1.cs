using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace cs_form_text_read_write {
    public partial class Form1 : Form {
        public Form1()
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void read_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            //ofd.FileName = "default.html";
            //ofd.InitialDirectory = @"C:\";
            ofd.Filter = "CSVファイル|*.csv|すべてのファイル|*.*";
            // ofd.FilterIndex = 2;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する

                try
                {
                    StreamReader sr = new StreamReader(ofd.FileName, Encoding.GetEncoding("shift_jis"));

                    //内容を一括で読み込む
                    textarea.Text = sr.ReadToEnd();

                    sr.Close();

                }
                catch (Exception ex)
                {

                    string error = ex.ToString();
                    MessageBox.Show(error);
                    return;

                }
            }
        }

        private void write_button_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.FileName = "utf8";
            sfd.Filter = "CSVファイル|*.csv|すべてのファイル|*.*";
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                // false : 上書き, true : 追加
                StreamWriter sw = new StreamWriter(sfd.FileName, false, new UTF8Encoding());
                sw.Write(textarea.Text);
                sw.Close();

                string path = Path.GetDirectoryName(sfd.FileName);

                sw = new StreamWriter($@"{path}\utf8_bom.csv", false, new UTF8Encoding(true));
                sw.Write(textarea.Text);
                sw.Close();

            }

        }
    }
}
