using AdventOfCode.Days;
using AdventOfCode.Properties;
using System;
using System.IO;
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
            if (string.IsNullOrWhiteSpace(Settings.Default.Path) == false)
            {
                _path = Settings.Default.Path;
                tbPath.Text = _path;
            } 
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
            try
            {
                IAlgoritm algoritm = new D6();
                if (File.Exists(_path))
                {
                    Settings.Default.Path = _path;
                    Settings.Default.Save();
                    tbResult.Text = algoritm.Run(File.ReadAllText(_path));
                }
                else
                {
                    tbResult.Text = "No file";
                }

            }
            catch (Exception ex)
            {
                tbResult.Text = ex.ToString();
            }
        }
    }
}
