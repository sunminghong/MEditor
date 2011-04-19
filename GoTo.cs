using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MEditor
{
    [System.ComponentModel.DesignerCategory("form")]
    public partial class GoToLineDialog : Form
    {
        public int LineNumber
        {
            get
            {
                int line = 0;
                Int32.TryParse(lnbox.Text, out line);
                return line;
            }
        }

        //Instance of the control passed via 2nd Constructor
        private RichTextBox RichTextBoxControl;

        //Secondary Constructor (Used)

        public GoToLineDialog(RichTextBox RTB)
        {
            RichTextBoxControl = RTB;
            InitializeComponent();
        }



        void Go(object sender, EventArgs e)
        {
            if (LineNumber > 0 && LineNumber <= RichTextBoxControl.Lines.Length)
            {
                int start = RichTextBoxControl.GetFirstCharIndexFromLine(LineNumber - 1);
                RichTextBoxControl.SelectionStart = start;
                RichTextBoxControl.SelectionLength = 0;
                RichTextBoxControl.ScrollToCaret();
                this.DialogResult = DialogResult.OK;
                RichTextBoxControl.Select();               
            }
            else
                MessageBox.Show("MEditor不能转到指定行。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
