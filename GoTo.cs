using System;
using System.ComponentModel;
using System.Windows.Forms;
using ICSharpCode.AvalonEdit;

namespace MEditor
{
    [DesignerCategory("form")]
    public partial class GoToLineDialog : Form
    {
        //Instance of the control passed via 2nd Constructor
        private readonly TextEditor RichTextBoxControl;

        //Secondary Constructor (Used)

        public GoToLineDialog(TextEditor RTB)
        {
            RichTextBoxControl = RTB;
            InitializeComponent();
        }

        public int LineNumber
        {
            get
            {
                int line = 0;
                Int32.TryParse(lnbox.Text, out line);
                return line;
            }
        }


        private void Go(object sender, EventArgs e)
        {
            if (LineNumber > 0 && LineNumber <= RichTextBoxControl.Document.LineCount)
            {
                RichTextBoxControl.ScrollToLine(LineNumber);
                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("MEditor不能转到指定行。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}