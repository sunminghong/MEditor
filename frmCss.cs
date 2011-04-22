using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MEditor.Properties;

namespace MEditor
{
    public partial class frmCss : Form
    {
        private string _css = "";
        private frmMain _fm = null;

        public string Css
        {
            get { return _css; }
            set { _css = value; }
        }
        public frmCss(string css,frmMain fm)
        {
            InitializeComponent();
            _css = css;
            _fm = fm;
            txtCss.AppendText(css);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _fm.SetCss(txtTabWidth.Text);
            _fm.SetCss(txtCss.Text);
            _fm.SetExt(txtExt.Text);
            _fm.SaveSettings();
            
            Close();
         }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //前景色
            _fm.SelectForeColor();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _fm.SelectBackColor();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _fm.SelectFont();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
             _css = Settings.Default.css;
            txtCss.Text  = _css;
            _fm.SetOldStyle();
            //_fm.ReadCss();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
             _css = Settings.Default.css;
            txtCss.Text  = _css;
            _fm.SetBlackWhiteStyle();
            //_fm.ReadCss();
        }

        private void frmCss_Load(object sender, EventArgs e)
        {
            txtExt.Text = Settings.Default.extfile;
        }

    }
}
