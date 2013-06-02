using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using DragDropEffects = System.Windows.DragDropEffects;
using DragEventArgs = System.Windows.DragEventArgs;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace MEditor
{
    public class MarkdownEditor
    {
        private readonly TextEditor _markdownRtb;
        private readonly WebBrowser _previewBrowser;
        private readonly frmMain _thisForm;
        private string _file = "";


        private bool _isOpen;
        private TabPage _markdownPage;
        private bool alreadyUpdate;

        public MarkdownEditor(frmMain thisform, TabPage page, WebBrowser preview)
        {
            _markdownPage = page;
            _thisForm = thisform;
            _previewBrowser = preview;

            createRTB(ref _markdownRtb);
            _markdownRtb.TextChanged += _markdownRtb_TextChanged;
            var elementHost = new ElementHost();
            elementHost.Child = _markdownRtb;
            elementHost.Dock = DockStyle.Fill;
            page.Controls.Add(elementHost);
        }

        public bool AlreadyUpdate
        {
            get { return alreadyUpdate; }
            set { alreadyUpdate = value; }
        }

        public string FileName
        {
            get { return _file; }
            set { _file = value; }
        }

        public TabPage MarkdownPage
        {
            get { return _markdownPage; }
            set { _markdownPage = value; }
        }

        private void createRTB(ref TextEditor htmlRtb)
        {
            htmlRtb = new TextEditor();
            //_htmlRtb.BackColor = bg ;
            //_htmlRtb.ForeColor =fore;
//			htmlRtb.WordWrap = false;
            htmlRtb.AllowDrop = false;
//			htmlRtb.ScrollBars = RichTextBoxScrollBars.Both;
            //htmlRtb.Dock = DockStyle.Fill;

            htmlRtb.TabIndex = 0;

//			htmlRtb.AcceptsTab = true;
//			htmlRtb.BulletIndent = 4;
//			htmlRtb.DetectUrls = false;

//			htmlRtb.ZoomFactor = 1.0f;
//
//			htmlRtb.EnableAutoDragDrop = false;
//			htmlRtb.HideSelection = true;

            int lef = 60; // (int)getfontWeight(4, font);
//			htmlRtb.SelectionTabs = new int[] { lef, lef * 2, lef * 3, lef * 3, lef * 4 };
            //htmlRtb.TabIndent=4;


            htmlRtb.AllowDrop = true;
            //htmlRtb.KeyDown+=new System.Windows.Input.KeyEventHandler(htmlRtb_KeyDown);
            htmlRtb.TextChanged += _markdownRtb_TextChanged;

            htmlRtb.DragEnter += htmlRtb_DragEnter;
            htmlRtb.Drop += htmlRtb_DragDrop;
        }

        private void htmlRtb_DragDrop(object sender, DragEventArgs e)
        {
            var arrayFileName = (Array) e.Data.GetData(DataFormats.FileDrop);

            string strFileName = arrayFileName.GetValue(0).ToString();


            var data = (string[]) e.Data.GetData(DataFormats.FileDrop);
            _thisForm.openfiles(data);
        }

        private void htmlRtb_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void htmlRtb_KeyDown(object sender, KeyEventArgs e)
        {
            var rtb = ((TextEditor) sender);
            if (e.KeyStates == Keyboard.GetKeyStates(Key.V) && Keyboard.Modifiers == ModifierKeys.Control)
            {
                rtb.Paste();
                e.Handled = true;
                return;
            }

            if (e.KeyStates == Keyboard.GetKeyStates(Key.Tab))
                return;

            if (e.KeyStates == Keyboard.GetKeyStates(Key.LeftShift))
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
                len = olen + text.Length - len - 1;
                if (len > 0)
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

            e.Handled = true;
        }

        public TextEditor GetTextBox()
        {
            return _markdownRtb;
        }

        private void _markdownRtb_TextChanged(object sender, EventArgs e)
        {
            invokeScript("loadMarkdown", Utils.ConvertTextToHTML(_markdownRtb.Text));
        }

        public string GetMarkdown()
        {
            return _markdownRtb.Text;
        }


        public void invokeScript(string scriptName, string html)
        {
            try
            {
                _previewBrowser.Document.InvokeScript(scriptName, new[] {html});
            }
            catch (COMException)
            {
                string text = Utils.AppendValidHTMLTags(html, "", true);
                _previewBrowser.DocumentText = text;
            }
        }


        public bool Openfile(string file)
        {
            string text = Utils.AppendValidHTMLTags(Utils.ConvertTextToHTML(_markdownRtb.Text), file, true);

            _previewBrowser.DocumentText = text;
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
                var sr = new StreamReader(file, Encoding.UTF8);
                _markdownRtb.Text = sr.ReadToEnd();
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
            _file = file;
            Save();
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(_file))
                return;

            var sw = new StreamWriter(_file, false, Encoding.UTF8);
            sw.Write(_markdownRtb.Text);
            //this._markdownRtb.SaveFile(sw, RichTextBoxStreamType.PlainText);
            sw.Close();
            sw.Dispose();
            alreadyUpdate = false;

            var fileInfo = new FileInfo(FileName);

            _markdownPage.Text = fileInfo.Name;
            _markdownPage.ToolTipText = FileName;
        }

        public void SetStyle(Color bg, Color fore, Font font, bool wordWrap, int tabWidth)
        {
            _isOpen = true;
            TextEditor rtb = GetTextBox();

//			if (rtb.BackColor != bg)
//				rtb.BackColor = bg;
//			if (rtb.ForeColor != fore)
//				rtb.ForeColor = fore;
//			if (rtb.Font != font)
//				rtb.Font = font;

            if (rtb.WordWrap != wordWrap)
                rtb.WordWrap = wordWrap;

            _isOpen = false;
        }

        private float getfontWeight(int width, Font font)
        {
            Graphics g = _thisForm.CreateGraphics();
            SizeF sizeF = g.MeasureString("A", font);
            return sizeF.Width*width;
        }
    }
}