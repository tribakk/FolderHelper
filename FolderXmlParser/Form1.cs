using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FolderXmlParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void parseFolder(string path)
        {
            List<string> files = TestExport.kernal.utils.GetSubFiles(path, false);
            int count = files.Count;
            for (int i = 0; i < count; i++)
            {
                string fileName = files[i];
                string ext = Path.GetExtension(fileName);
                if (ext == ".rels" || ext == ".xml")
                {
                    string text = TestExport.kernal.utils.ReadFile(fileName);
                    text = TestExport.kernal.utils.PrintXML(text);
                    if (text != "")
                        TestExport.kernal.utils.WriteFile(fileName, text);
                }
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            string path1 = textBox1.Text;
            string path2 = textBox2.Text;
            if (path1 != "")
            {
                parseFolder(path1);
            }
            if (path2 != "")
            {
                parseFolder(path2);
            }
        }
    }
}
