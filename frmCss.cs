using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MEditor
{
    public partial class frmCss : Form
    {
        private string _css = "";

        public string Css
        {
            get { return _css; }
            set { _css = value; }
        }
        public frmCss(string css)
        {
            InitializeComponent();
            _css = css;
            txtCss.AppendText(css);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _css = txtCss.Text;
            Close();
         }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
