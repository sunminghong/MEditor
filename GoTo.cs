using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.AvalonEdit;

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
		private TextEditor RichTextBoxControl;

		//Secondary Constructor (Used)

		public GoToLineDialog(TextEditor RTB)
		{
			RichTextBoxControl = RTB;
			InitializeComponent();
		}



		void Go(object sender, EventArgs e)
		{
			if (LineNumber > 0 && LineNumber <= RichTextBoxControl.Document.LineCount)
			{
				RichTextBoxControl.ScrollToLine(LineNumber);
				this.DialogResult = DialogResult.OK;

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
