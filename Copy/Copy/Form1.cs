using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.ShowDialog();
            Copy();
        }
        void Copy()
        {

            var path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                File.Copy(file, Path.Combine(radBrowseEditor1.ToString(), Path.GetFileName(file)), true);
                listBox1.Items.Add(file.ToString());
            }

        }

    }
}
