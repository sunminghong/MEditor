using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace MEditor
{
    [System.ComponentModel.DesignerCategory("form")]
    public partial class frmReplace :  Form
    {
        public string result;
        RichTextBox dab;
        private bool _isFind=false;
        public frmReplace(RichTextBox  da, string txt,bool isFind)
        {
            InitializeComponent();
            result = da.Rtf;
            dab = da;
            findbox.Text = txt;

            _isFind = isFind;
        }

        bool CheckEmp()
		{
            //If there's no text
            if (findbox.Text == "")
            {
                //MessageBox.Show("请输入需要查找的字符串.", "Empty TextBox", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //Otherwise, success!
            else return true;
		
		}

        /// <summary>
        /// Replace all cases
        /// </summary>
        private void ReplaceAll(object sender, EventArgs e)
        {
            if (_isFind)
            {
                find();
                return;
            }
            if (CheckEmp())
            {
                Regex replaceRegex = GetRegExpression();
                String replacedString;

                // get the current SelectionStart
                int selectedPos = dab.SelectionStart;

                // get the replaced string
                replacedString = replaceRegex.Replace
                    (dab.Text, repbox.Text);

                // Is the text changed?
                if (dab.Text != replacedString)
                {
                    // then replace it
                    dab.Text = replacedString;
                    MessageBox.Show("替换所有完成. ", Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // restore the SelectionStart
                    dab.SelectionStart = selectedPos;
                }
                //If no matches found...
                else 
                {
                    MessageBox.Show(String.Format("不能找到 '{0}'.   ",
                    findbox.Text),
                    Application.ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }

                dab.Focus();
            }
        }
        
        private void Replace(object sender, EventArgs e)
        {
            //Replace one instance
            if (CheckEmp())
            {
                Regex regexTemp = GetRegExpression();
                Match matchTemp = regexTemp.Match(dab.SelectedText);

                if (matchTemp.Success)
                {
                    // check if it is an exact match
                    if (matchTemp.Value == dab.SelectedText)
                        dab.SelectedText = repbox.Text;
                }

            }
        }
        private void find()
        {
            //Replace one instance
            if (CheckEmp())
            {
                int length;
                try
                {
                    int startPos = dab.SelectionStart + dab.SelectedText.Length;
                    //if (!forward)
                    //    startPos = dab.SelectionStart;

                    if (startPos >= dab.Text.Length)
                        startPos = 0;

                    Regex regexTemp = GetRegExpression();
                    Match mt = regexTemp.Match(dab.Text, startPos);

                    if (mt == null || mt.Length == 0)
                    {
                        length = 0;
                        return;
                    }

                    length = mt.Length;

                    dab.Select(mt.Index, length);
                    dab.ScrollToCaret();

                    return;
                }
                catch
                {

                }
            }
        }

        private Regex GetRegExpression()
        {
            //If it's a regular expression
            Regex result;
            String regExString;

            // Get what the user entered
            regExString = findbox.Text;

            if (useregex.Checked)
            {
                
            }
            // wild cards checkbox checked
            else if (usewild.Checked)
            {
                // multiple characters wildcard (*)
                regExString = regExString.Replace("*", @"\w*");

                // single character wildcard (?)
                regExString = regExString.Replace("?", @"\w");

                // if wild cards selected, find whole words only
                regExString = String.Format("{0}{1}{0}", @"\b", regExString);
            }
            else
                // replace escape characters
                regExString = Regex.Escape(regExString);

            // Is whole word check box checked?
            if (MatchWhole.Checked)
                regExString = String.Format("{0}{1}{0}", @"\b", regExString);

            // Is match case checkbox checked or not?
            if (CaseSen.Checked)
                result = new Regex(regExString);
            else
                result = new Regex(regExString, RegexOptions.IgnoreCase);

            return result;
        }

        private void frmReplace_Load(object sender, EventArgs e)
        {
            if (_isFind)
            {
                repbox.Enabled = false;
                repallbtn.Text = "查找下一个(&F)";
                repbtn.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
