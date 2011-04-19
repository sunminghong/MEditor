using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using MarkdownSharp;
using MRU;
using System.Reflection;


namespace MEditor
{
    public partial class frmMain : Form, IMRUClient
    {
        private static string _myExtName = ".md";
        private string myExtName = "Markdown文件|*" + _myExtName + "|所有文件|*.*";
        private MarkdownEditorManager meditorManager=null;

        public void OpenMRUFile(string fileName)
        {
            openfile(fileName);
        }

        private void openfiles(string[] fileNames)
        {
            foreach (string s in fileNames)
                openfile(s);
        }

        private void openfile(string fileName)
        {
            if (meditorManager.Open(fileName))
            {
                PreivewHtml(true);
            }
            else
                mruManager.Remove(fileName);       // when Open File operation failed
        }
        private void openfile()
        {
            OpenFileDialog fileone = new OpenFileDialog();
            fileone.Filter = myExtName ;
            fileone.FilterIndex = 1;
            if (fileone.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    openfile(fileone.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出现了错误：" + ex.Message);
                    mruManager.Remove(fileone.FileName);       // when Open File operation failed
                }
            }
        }

        private void PreivewHtml(bool refresh)
        {
            this.toolStripStatusLabel1.Text = " 正在转换。。。。 ";

            MarkdownEditor meditor = meditorManager.SetStyle();
            if (meditor == null)
                return;
            meditor.GetTextBox().Focus();
            string html = "";

                Markdown mark = new Markdown();

                string marktext = meditor.GetMarkdown();
                if (!string.IsNullOrEmpty(marktext))
                {
                    html = mark.Transform(marktext);
                    rtbHtml.Text=html;
                }

           webBrowser1.DocumentText =meditorManager.GetHTMLStyle(html);
           this.toolStripStatusLabel1.Text = "当前文档：" + meditor.FileName;

            if (isLeft)
            {
                if (!string.IsNullOrEmpty(html) && splitContainer1.Panel2Collapsed)
                    splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(html) && splitContainer1.Panel1Collapsed)
                    splitContainer1.Panel1Collapsed = false;
            }
        }
        private void SelectForeColor()
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                meditorManager.SetForeColor(color.Color);
                meditorManager.SetStyle(rtbHtml);
            }
        }

        private void SelectBackColor()
        {
            ColorDialog color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                meditorManager.SetBackColor(color.Color);
                meditorManager.SetStyle(rtbHtml);
            }
        }
        private void SelectFont()
        {
            FontDialog font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                meditorManager.SetFont(font.Font);
                meditorManager.SetStyle(rtbHtml);
            }
        }

        private void showSyntax(string url)
        {
            toolStripStatusLabel1.Text ="正在打开" +url+"..." ;
            webBrowser1.Navigate(url);
            //webBrowser1.DocumentText = html;
           
            if (splitContainer1.Panel2Collapsed)
                splitContainer1.Panel2Collapsed = false;
        }

        private bool isLeft = true;
        private void SwithFlaout()
        {
            if (isLeft)
            {
                this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
                this.splitContainer1.Panel1.Controls.Add(this.tabControl2);
                if (splitContainer1.Panel2Collapsed)
                {
                    splitContainer1.Panel2Collapsed = false;
                    splitContainer1.Panel1Collapsed = true;
                }
                isLeft = false;
                return;
            }
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl2);
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel2Collapsed = true;
            }
            isLeft = true;
        }

        private void UnRegExtfile()
        {
           bool rel= FileTypeRegister.UnRegister(_myExtName);

           if (!rel && IsWindowsVistaOrNewer)
           {
               MessageBox.Show("您是在windows7 下运行的此程序，如果执行完扩展名关联但没有成功，请您以“管理员权限方式再打开此程序再关联一次”！");
           }

        }
        /// <summary>
        /// Gets a value indicating if the operating system is a Windows Vista or a newer one.
        /// </summary>
        public static bool IsWindowsVistaOrNewer
        {
            get { return (Environment.OSVersion.Platform == PlatformID.Win32NT) && (Environment.OSVersion.Version.Major >= 6); }
        }
        private void regExtFile()
        {

            Assembly asm = Assembly.GetExecutingAssembly();
            string resourceName = "MEditor.logo.ico";
            Stream stream = asm.GetManifestResourceStream(resourceName);
            string icofile = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\markdown.ico";
            FileStream fs = new FileStream(icofile, FileMode.Create, FileAccess.Write);
            byte[] bs = new byte[stream.Length];
            stream.Read(bs, 0, (int)stream.Length);
            fs.Write(bs, 0, bs.Length);
            fs.Flush();
            fs.Close();
            fs.Dispose();

            //注册关联的文件扩展名
            FileTypeRegInfo fr = new FileTypeRegInfo();
            fr.Description = "markdown文档格式";
            fr.ExePath = Application.ExecutablePath;
            fr.ExtendName = _myExtName;
            fr.IcoPath = icofile;

            bool rel = true;
            if (FileTypeRegister.CheckIfRegistered(fr.ExtendName))
            {
                rel=FileTypeRegister.UpdateRegInfo(fr);
            }
            else{
                rel=FileTypeRegister.Register(fr);
            }

            if (!rel && IsWindowsVistaOrNewer)
            {
                MessageBox.Show("您是在windows7 下运行的此程序，如果执行完扩展名关联但没有成功，请您以“管理员权限方式再打开此程序再关联一次”！");
            }
        }
    }
}

