using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MEditor
{
    public class MarkdownEditor
    {
        private bool alreadyUpdate = false;
        private bool _isOpen = false;

        public bool AlreadyUpdate
        {
            get { return alreadyUpdate; }
            set { alreadyUpdate = value; }
        }

        private string _file = "";

        public string FileName
        {
            get { return _file; }
            set { _file = value; }
        }
        private frmMain _thisForm;

        private RichTextBox _markdownRtb;
        //        private RichTextBox _htmlRtb;

        private TabPage _markdownPage;

        public TabPage MarkdownPage
        {
            get { return _markdownPage; }
            set { _markdownPage = value; }
        }
        public MarkdownEditor(frmMain thisform, TabPage page)
        {
            this._markdownPage = page;
            this._thisForm = thisform;

            createRTB(ref _markdownRtb);
            _markdownRtb.TextChanged += new EventHandler(_markdownRtb_TextChanged);
            page.Controls.Add(_markdownRtb);
        }

        private void createRTB(ref RichTextBox htmlRtb)
        {
            htmlRtb = new RichTextBox();
            //_htmlRtb.BackColor = bg ;
            //_htmlRtb.ForeColor =fore;
            htmlRtb.WordWrap = false;
            htmlRtb.AllowDrop = false;
            htmlRtb.ScrollBars = RichTextBoxScrollBars.Both;
            htmlRtb.Dock = DockStyle.Fill;
            htmlRtb.TabIndex = 0;

            htmlRtb.AcceptsTab = true;
            htmlRtb.BulletIndent = 4;
            htmlRtb.DetectUrls = false;

            htmlRtb.ZoomFactor = 1.0f;

            htmlRtb.EnableAutoDragDrop = false;
            htmlRtb.HideSelection = true;

            int lef = 60;// (int)getfontWeight(4, font);
            htmlRtb.SelectionTabs = new int[] { lef, lef * 2, lef * 3, lef * 3, lef * 4 };

            htmlRtb.AllowDrop = true;
            htmlRtb.KeyDown += new KeyEventHandler(htmlRtb_KeyDown);
            htmlRtb.DragEnter += new DragEventHandler(htmlRtb_DragEnter);
            htmlRtb.DragDrop += new DragEventHandler(htmlRtb_DragDrop);
            

        }

        void htmlRtb_DragDrop(object sender, DragEventArgs e)
        {
            Array arrayFileName = (Array)e.Data.GetData(DataFormats.FileDrop);

            string strFileName = arrayFileName.GetValue(0).ToString();


            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
            //MessageBox.Show(data[0]);
            _thisForm.openfiles(data);//, RichTextBoxStreamType.PlainText);

        }

        void htmlRtb_DragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        void htmlRtb_KeyDown(object sender, KeyEventArgs e)
        {
            RichTextBox rtb = ((RichTextBox)sender);
            if (e.Control && e.KeyCode == Keys.V)
            {
                rtb.Paste(DataFormats.GetFormat("Text"));
                e.SuppressKeyPress = true;
                return;
            }

            if (e.KeyCode != Keys.Tab)
                return;

            if (e.Shift)
            {
                //如果特择了多行，则每行前加入一个\t
                string text = rtb.SelectedText;

                int st = rtb.SelectionStart;
                int st1 = rtb.Text.LastIndexOf("\n", st);
                st1 = st1 > -1 ? st1 : 0;
                int olen = rtb.SelectionLength;
                int len = st - st1 + rtb.SelectionLength;
                rtb.SelectionStart = st1;
                rtb.SelectionLength = len;
                text = rtb.SelectedText;

                text = text.Replace("\n\t", "\n");

                rtb.SelectedText = text;
                rtb.SelectionStart = st + 1;
                len=olen + text.Length - len - 1;
                if(len>0)
                    rtb.SelectionLength = len;
            }
            else
            {
                //如果特择了多行，则每行前加入一个\t
                string text = rtb.SelectedText;
                if (text.IndexOf("\n") != -1)
                {
                    int st = rtb.SelectionStart;
                    if (st > 0)
                    {
                        int st1 = rtb.Text.LastIndexOf("\n", st);
                        st1 = st1 > -1 ? st1 : 0;
                        int olen = rtb.SelectionLength;
                        int len = st - st1 + rtb.SelectionLength;
                        rtb.SelectionStart = st1;
                        rtb.SelectionLength = len;
                        text = rtb.SelectedText;
                        //text = rtb.Text.Substring(st1, len );

                        text = text.Replace("\n", "\n\t");
                        rtb.SelectedText = text;
                        rtb.SelectionStart = st + 1;
                        rtb.SelectionLength = olen + text.Length - len - 1;
                    }
                
                }
                else
                {
                    //rtb.SelectedText = "    ";
                    return;
                }
            }

            e.SuppressKeyPress = true;
        }

        public RichTextBox GetTextBox()
        {
            //if (_htmlRtb.Visible)
            //    return _htmlRtb;
            //else
            return _markdownRtb;
        }

        void _markdownRtb_TextChanged(object sender, EventArgs e)
        {
            if (_isOpen)
            {
                alreadyUpdate = false;
                return;
            }

            if (alreadyUpdate) return;
            alreadyUpdate = true;

            _markdownPage.Text = "*" + _markdownPage.Text;
        }
        //public void SwitchToHtml()
        //{
        //    _markdownRtb.Visible = false;
        //    _htmlRtb.Visible = true;
        //}
        //public void SwitchToMark()
        //{         
        //   _htmlRtb.Visible = false;
        //   _markdownRtb.Visible = true;
        //}

        public string GetMarkdown()
        {
            return _markdownRtb.Text;
        }
        //public string GetHtml()
        //{
        //    return _htmlRtb.Text;
        //}
        //public void SetHtml(string s)
        //{
        //    _htmlRtb.Text = s;
        //}

        public bool Openfile(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                return true;
            }

            _file = file;
            if (!File.Exists(file))
                return true;
            try
            {
                _isOpen = true;
                StreamReader sr = new StreamReader(file, Encoding.UTF8);
                this._markdownRtb.Text = sr.ReadToEnd();
                //this._markdownRtb.LoadFile(sr, RichTextBoxStreamType.PlainText);
                sr.Close();
                sr.Dispose();
                _isOpen = false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save(string file)
        {
            this._file = file;
            Save();
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(_file))
                return;

            StreamWriter sw = new StreamWriter(_file,false, Encoding.UTF8);
            sw.Write(this._markdownRtb.Text);
            //this._markdownRtb.SaveFile(sw, RichTextBoxStreamType.PlainText);
            sw.Close();
            sw.Dispose();
            alreadyUpdate = false;

            FileInfo fileInfo = new FileInfo(FileName);

            _markdownPage.Text = fileInfo.Name;
            _markdownPage.ToolTipText = FileName;
        }

        public void SetStyle(Color bg, Color fore, Font font, bool wordWrap,int tabWidth)
        {
            _isOpen = true;
            RichTextBox rtb = GetTextBox();

            if (rtb.BackColor != bg)
                rtb.BackColor = bg;
            if (rtb.ForeColor != fore)
                rtb.ForeColor = fore;
            if (rtb.Font != font)
                rtb.Font = font;
            if (rtb.WordWrap != wordWrap)
                rtb.WordWrap = wordWrap;
                        
            _isOpen = false;
        }

        private float getfontWeight(int width,Font font)
        {
            Graphics g = _thisForm.CreateGraphics();
            SizeF sizeF = g.MeasureString("A",font );
            return sizeF.Width * width;
        }
    }
}