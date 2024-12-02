using System;
using System.Windows.Forms;

namespace AdventOfCode
{
    public partial class Form1 : Form
    {
        private string _path;
        private IAlgoritm _algoritm;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Application.StartupPath;
                openFileDialog.Filter = "Txt (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _path = openFileDialog.FileName;
                    tbPath.Text = _path;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {

        }
    }
}
