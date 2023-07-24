using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepthmapMaker
{
    public partial class SelectModel : Form
    {
        public SelectModel()
        {
            InitializeComponent();
            openFileDialog1.DefaultExt = "obj";
            openFileDialog1.Filter = "objects | *.obj";
            openFileDialog1.ShowDialog(this);
        }

        public string GetModelPath()
        {
            return openFileDialog1.FileName;
        }
    }
}
