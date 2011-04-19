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
        private Form _thisForm;

        private RichTextBox _markdownRtb;
//        private RichTextBox _htmlRtb;

        private TabPage _markdownPage;

        public TabPage MarkdownPage
        {
            get { return _markdownPage; }
            set { _markdownPage = value; }
        }
        public MarkdownEditor(Form thisform ,TabPage page)
        {
            this._markdownPage = page;
            this._thisForm=thisform;

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

            htmlRtb.AcceptsTab = true;
            htmlRtb.BulletIndent = 4;
            htmlRtb.DetectUrls = false;

            htmlRtb.ZoomFactor = 1.0f;
            htmlRtb.KeyDown += new KeyEventHandler(htmlRtb_KeyDown);
        }

        void htmlRtb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                ((RichTextBox)sender).SelectedText = "    ";
                e.SuppressKeyPress = true;
            }
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

            _markdownPage.Text = "*"+_markdownPage.Text;
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
               this._markdownRtb.LoadFile(file, RichTextBoxStreamType.PlainText);
                _isOpen = false;

               return true;
            }
            catch
            {
                return false;
            }
        }

        public  void Save(string file)
        {
            this._file = file;
            Save();
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(_file))
                return;

            this._markdownRtb.SaveFile(_file, RichTextBoxStreamType.PlainText);
            alreadyUpdate = false;

            FileInfo fileInfo = new FileInfo(FileName);

            _markdownPage.Text = fileInfo.Name;
            _markdownPage.ToolTipText = FileName;
        }

        /// <summary>
        /// 清场
        /// </summary>
        public void Close()
        {
            _markdownPage.Dispose();
           //_htmlRtb.Dispose();
        }

        public void SetStyle(Color bg, Color fore,Font font,bool wordWrap){
            _isOpen = true;
            RichTextBox rtb=GetTextBox();

            if (rtb.BackColor!= bg)
                rtb.BackColor = bg;
            if (rtb.ForeColor != fore)
                rtb.ForeColor = fore;
            if (rtb.Font != font)
                rtb.Font = font;
            if (rtb.WordWrap != wordWrap)
                rtb.WordWrap = wordWrap;
            _isOpen = false;
        }

    }
}
