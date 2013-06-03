using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Awesomium.Core;
using Awesomium.Windows.Forms;
using ICSharpCode.AvalonEdit;
using DragDropEffects = System.Windows.DragDropEffects;
using DragEventArgs = System.Windows.DragEventArgs;

namespace MEditor
{
    public class MarkdownEditor
    {
        private readonly TextEditor _markdownRtb;
        private readonly WebControl _previewBrowser;

        private readonly FrmMain _thisForm;
        private string _file = "";

        private JSObject window;
        private bool _isOpen;
        private TabPage _markdownPage;
        private bool alreadyUpdate;

        public MarkdownEditor(FrmMain thisform, TabPage page, WebControl preview)
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

            htmlRtb.AllowDrop = false;

            htmlRtb.TabIndex = 0;

            htmlRtb.AllowDrop = true;
            htmlRtb.TextChanged += _markdownRtb_TextChanged;

            htmlRtb.DragEnter += htmlRtb_DragEnter;
            htmlRtb.Drop += htmlRtb_DragDrop;
        }

        private void htmlRtb_DragDrop(object sender, DragEventArgs e)
        {
            var arrayFileName = (Array)e.Data.GetData(DataFormats.FileDrop);

            string strFileName = arrayFileName.GetValue(0).ToString();


            var data = (string[])e.Data.GetData(DataFormats.FileDrop);
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
                window = this._previewBrowser.ExecuteJavascriptWithResult("window");
                if (window == null)
                {
                    return;
                }
                window.Invoke(scriptName, new JSValue[] { html });
            }
            catch (Exception)
            {

            }
        }


        public bool Openfile(string file)
        {
            string text = Utils.AppendValidHTMLTags(Utils.ConvertTextToHTML(_markdownRtb.Text), file, true);

            _previewBrowser.LoadHTML(text);
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



            if (rtb.WordWrap != wordWrap)
                rtb.WordWrap = wordWrap;

            _isOpen = false;
        }


    }
}