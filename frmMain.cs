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
using MEditor.Processers;
using MRU;
using System.Runtime.InteropServices;
using System.Reflection;
using MarkdownSharp;


namespace MEditor
{
    public partial class frmMain : Form
    {
        //private PrintDocument printDocument = new PrintDocument();       

        private MRUManager mruManager=null;


        public frmMain()
        {
            InitializeComponent();
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            meditorManager.Open("");
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.Open("");
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            openfile();
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openfile();
        }

        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            meditorManager.Save();
        }
        
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.Save();
        }

        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.SaveAs();
        }

        private void 剪切UToolStripButton_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Cut();
        }

        private void 剪切TToolStripMenuItem_Click(object sender, EventArgs e)
        {

            meditorManager.GetTextBox().Cut();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Cut();
        }

        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Copy();
        }

        private void 复制CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Copy();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            meditorManager.GetTextBox().Copy();
        }

        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            //RichTextBox rtb = new RichTextBox();

            //IDataObject idat = Clipboard.GetDataObject();
            //if (idat.GetDataPresent(DataFormats.Text))
            //{DataFormats.GetFormat ("Text
            //    string PasteStr = (string)idat.GetData(DataFormats.Text);
            //    int pasteid = rtb.SelectionStart;
            //    rtb.Text = rtb.Text.Substring(0, pasteid) + PasteStr + rtb.Text.Substring(pasteid);
            //    rtb.SelectionStart = pasteid + PasteStr.Length;
            //}

            meditorManager.GetTextBox().Paste(DataFormats.GetFormat("Text"));
        }

        private void 粘贴PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Paste();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Paste();
        }

        private void 撤销toolStripButton5_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Undo();
        }

        private void 撤消UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Undo();
        }

        private void 撤销ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Undo();
        }

        private void 重做toolStripButton6_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Redo();
        }

        private void 重复RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Redo();
        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().SelectAll();
        }

        private void 全选AToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().SelectAll();
        }

        private void 删除LToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Text = meditorManager.GetTextBox().Text.Remove(meditorManager.GetTextBox().SelectionStart, meditorManager.GetTextBox().SelectionLength);
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Text = meditorManager.GetTextBox().Text.Remove(meditorManager.GetTextBox().SelectionStart, meditorManager.GetTextBox().SelectionLength);
        }

        private void 时间日期F5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox rtb = meditorManager.GetTextBox();
            int st = rtb.SelectionStart;
            string dat=System.DateTime.Now.ToString();
            rtb.Text = rtb.Text.Insert(st, dat);
           rtb.SelectionStart = st + dat.Length;
        }

        private void 字体toolStripButton10_Click(object sender, EventArgs e)
        {
            SelectFont();
        }

        private void 字体颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectForeColor();
        }

        private void 字体颜色toolStripButton11_Click(object sender, EventArgs e)
        {
            SelectForeColor();
        }

        #region 格式设计辅助
        private void 加粗toolStripButton7_Click(object sender, EventArgs e)
        {
            ProcesserFactory.Processe(meditorManager.GetTextBox(), EMark.bold);
            PreviewHtml();
        }

        private void 倾斜toolStripButton8_Click(object sender, EventArgs e)
        {
            Font newFont = new Font(meditorManager.GetTextBox().SelectionFont, meditorManager.GetTextBox().SelectionFont.Italic ?
                                    meditorManager.GetTextBox().SelectionFont.Style & ~FontStyle.Italic : meditorManager.GetTextBox().SelectionFont.Style | FontStyle.Italic);
            meditorManager.GetTextBox().SelectionFont = newFont;

        }

        private void 下划线toolStripButton9_Click(object sender, EventArgs e)
        {
            Font newFont = new Font(meditorManager.GetTextBox().SelectionFont, meditorManager.GetTextBox().SelectionFont.Underline ?
                                    meditorManager.GetTextBox().SelectionFont.Style & ~FontStyle.Underline : meditorManager.GetTextBox().SelectionFont.Style | FontStyle.Underline);
            meditorManager.GetTextBox().SelectionFont = newFont;

        }

        private void 左对齐toolStripButton3_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().SelectionBullet = true;


            meditorManager.GetTextBox().SelectionIndent = 0;
            meditorManager.GetTextBox().SelectionHangingIndent = 0;
            meditorManager.GetTextBox().SelectionRightIndent = 0;
        }

        private void 右对齐toolStripButton1_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().SelectionIndent = 0;
            meditorManager.GetTextBox().SelectionHangingIndent = 0;
            meditorManager.GetTextBox().SelectionRightIndent =0;
        }

        private void 居中toolStripButton2_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().SelectionIndent = 8;
            meditorManager.GetTextBox().SelectionHangingIndent = 3;
            meditorManager.GetTextBox().SelectionRightIndent = 12;
        }

        private void 两端对齐toolStripButton4_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().SelectionIndent = 0;
            meditorManager.GetTextBox().SelectionHangingIndent = 3;
            meditorManager.GetTextBox().SelectionRightIndent = 0;
        }
#endregion

#region 打印控制还没有开放

        ////打印控制开始
        //void printDocument_BeginPrint(object sender, PrintEventArgs e)
        //{
        //    //设置打印文本
        //    text = meditorManager.GetTextBox().Text;
        //}

        //void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    //用DrawString方法输出字符串，在参数中可以设置打印的字体大小和颜色,此处设置为绿色
        //    e.Graphics.DrawString(text, new Font("Arial", 10), Brushes.Green, 50, 50);
        //}
        //private void 打印PToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //关联打印预处理代码
        //    printDocument.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);

        //    //关联打印代码
        //    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

        //    //定义打印对话框
        //    PrintDialog printDialog = new PrintDialog();

        //    //获得用户输入
        //    DialogResult dr = printDialog.ShowDialog();

        //    //若确认则打印
        //    if (dr == DialogResult.OK)
        //    {
        //        printDocument.Print();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}

        //private void 打印PToolStripButton_Click(object sender, EventArgs e)
        //{
        //    //关联打印预处理代码
        //    printDocument.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);

        //    //关联打印代码
        //    printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

        //    //定义打印对话框
        //    PrintDialog printDialog = new PrintDialog();

        //    //获得用户输入
        //    DialogResult dr = printDialog.ShowDialog();

        //    //若确认则打印
        //    if (dr == DialogResult.OK)
        //    {
        //        printDocument.Print();
        //    }
        //    else
        //    {
        //        return;
        //    }
        //}
        ////打印控制结束

        #endregion



        private void 打印预览VToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewHtml();
            ////页面设置对话框
            //PageSetupDialog psd = new PageSetupDialog();

            ////定义所设置的文档
            //psd.Document = printDocument;

            ////页面设置
            //psd.PageSettings = new PageSettings();

            ////打印机设置
            //psd.ShowNetwork = false;

            ////设置对话框
            //DialogResult dr = psd.ShowDialog();

            ////确认时设置打印文档
            //if (dr == DialogResult.OK)
            //{
            //    printDocument.PrinterSettings = psd.PrinterSettings;
            //    printDocument.DefaultPageSettings = psd.PageSettings;
            //}
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
   
        }

        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //先保存
            meditorManager.SaveAll();

            //关闭程序
            Close();
        }

        private void frmMain_DragOver(object sender, DragEventArgs e)
        {            
            e.Effect = DragDropEffects.Move;
        }

        public void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string []data=(string [])e.Data.GetData(DataFormats.FileDrop);
                //MessageBox.Show(data[0]);
                openfiles(data);//, RichTextBoxStreamType.PlainText);

            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (splitContainer1.Panel2Collapsed == false)
                PreviewHtml();
            //else
            //    splitContainer1.Panel2Collapsed = true;
        }

        private void 关闭所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.CloseAll();
        }

        private void 保存所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.SaveAll();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            TabSelectRec.Rec(e.TabPageIndex);
            //if(!tabControl1.ContainsFocus)
            //    SendKeys.Send("{tab}");            
            PreviewHtml();
        }
        
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            meditorManager.SaveAll();
        }

        #region 无标题栏移动窗口代码
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);
        [DllImport("User32.dll", EntryPoint = "ReleaseCapture")]
        private static extern int ReleaseCapture(); 

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle.ToInt32(), 0x0112, 0xF012, 0);
            }

        }

        #endregion

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.Save();
        }

        private void 关闭ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            meditorManager.Close();
        }

        private void 打开所在文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.openDir();
        }

        private void markdown格式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    MarkdownEditor m = meditorManager.GetCurrEditor();
        //    if (m == null) return;

        //    ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
        //    if (tsmi.Tag.ToString() == "markdown")
        //    {
        //        markdown格式ToolStripMenuItem.Checked = true;
        //        html格式ToolStripMenuItem.Checked = false;
        //        m.SwitchToMark();
        //        return;
        //    }

        //    markdown格式ToolStripMenuItem.Checked = false;
        //    html格式ToolStripMenuItem.Checked = true;
        //        m.SwitchToHtml();
        }

        private void 自动转行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (自动转行ToolStripMenuItem.Checked)
                meditorManager.SetWordWrap(true);
            else
                meditorManager.SetWordWrap(false);
        }

        private void 背景颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectBackColor();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SelectBackColor();
        }

        private void 还原系统原设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //meditorManager.SetOldStyle();
            //meditorManager.SetStyle(rtbHtml);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " V" + Application.ProductVersion;
            //初始化最近打开的文件
            mruManager = new MRUManager();
            mruManager.Initialize(this,toolStripDropDownButton1, 最近打开的文件ToolStripMenuItem,                        // Recent Files menu item
                "Software\\YihuiStudio\\MEditor"); // Registry path to keep MRU list
            mruManager.CurrentDir = ".....";           // default is current directory
            mruManager.MaxMRULength = 10;             // default is 10
            mruManager.MaxDisplayNameLength = 40;

            //定义编辑管理器
            meditorManager = new MarkdownEditorManager(this, tabControl1, mruManager);
            ReadCss();
            //webBrowser1.Navigate("about:blank");
            meditorManager.SetStyle(rtbHtml);
            //webBrowser1.DocumentText = meditorManager.GetHTMLStyle("");
            
            _filemonitor = new FileMonitor(fsw_Changed);
            string command = Environment.CommandLine;//获取进程命令行参数
            if (!string.IsNullOrEmpty(command))
            {
                string[] para = command.Split('\"');
                if (para.Length > 2)
                {
                    string pathC = para[2];//获取打开的文件的路径
                    if (pathC.Length > 3)
                    {
                        openfile(pathC);
                    }
                    else
                    {
                        meditorManager.Open("");
                    }
                }
            }
            else
            {
                meditorManager.Open("");
            }

            //this.timer1.Start();
            rtbHtml.EnableAutoDragDrop = false;
            rtbHtml.AllowDrop = true;

            rtbHtml.KeyDown += new KeyEventHandler(rtbHtml_KeyDown);
            rtbHtml.DragDrop += new DragEventHandler(frmMain_DragDrop);
            rtbHtml.DragEnter += new DragEventHandler(rtbHtml_DragEnter);
                        
            tabControl1.MouseDown += new MouseEventHandler(tabControl1_MouseDown);
            tabControl2.MouseDown+=new MouseEventHandler(tabControl1_MouseDown);
            //tabControl1.GotFocus += new EventHandler(tabControl1_GotFocus);
            
            }


        void tabControl1_GotFocus(object sender, EventArgs e)
        {
            SendKeys.Send("{tab}"); 
        }

        void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TabControl tabc = (TabControl)sender;

                for (int i = 0; i < tabc.TabCount; i++)
                {
                    Rectangle recTab = tabc.GetTabRect(i);
                    if (recTab.Contains(e.X, e.Y))
                    {
                        tabc.SelectedIndex = i;
                    }
                }
            }
        }

        void rtbHtml_KeyDown(object sender, KeyEventArgs e)
        {
            RichTextBox rtb = ((RichTextBox)sender);
            if (e.Control && e.KeyCode == Keys.V)
            {
                rtb.Paste(DataFormats.GetFormat("Text"));
                e.SuppressKeyPress = true;
                return;
            }
        }

        void rtbHtml_DragEnter(object sender, DragEventArgs e)
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

        private void 界面布局左右互换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwithFlaout();
        }

        private void 关联md扩展名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            regExtFile();
        }

        private void 转到行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkdownEditor me= meditorManager.GetCurrEditor();
            if(me==null)return ;

            if (me.GetTextBox().Lines.Length > 0)
            {
                //Show the dialog
                GoToLineDialog gtl = new GoToLineDialog(me.GetTextBox());
                gtl.ShowDialog();
            }
        }

        private void 转为大写ToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            MarkdownEditor me= meditorManager.GetCurrEditor();
            if(me==null)return ;

            me.GetTextBox().SelectedText = me.GetTextBox().SelectedText.ToUpper();
        }

        private void 转为小写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkdownEditor me = meditorManager.GetCurrEditor();
            if (me == null) return;

            me.GetTextBox().SelectedText = me.GetTextBox().SelectedText.ToLower();
        }

        private void 替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkdownEditor me = meditorManager.GetCurrEditor();
            if (me == null) return;

            frmReplace r = new frmReplace(me.GetTextBox(), me.GetTextBox().SelectedText,false);
            r.Show((IWin32Window)this);
        }

        private void 查找替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkdownEditor me = meditorManager.GetCurrEditor();
            if (me == null) return;

            frmReplace r = new frmReplace(me.GetTextBox(), me.GetTextBox().SelectedText,true);
            r.Show((IWin32Window)this);
        }

        private void splitContainer1_DoubleClick(object sender, EventArgs e)
        {
            if (isLeft)
            {

                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            }
            else
            {
                splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
        }

        private void 经典黑底白字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //meditorManager.SetBlackWhiteStyle();
        }

        private void 取消md文件关联ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnRegExtfile();
        }

        #region MEditor links
        private void markdown语法介绍ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //showSyntax("https://github.com/5d13cn/MEditor/blob/master/resoucesdocs/syntax.md");
            showSyntax("http://www.cnblogs.com/yihuiso/archive/2011/04/13/markdown.html");
        }

        private void markdown语法介绍精简版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("http://www.cnblogs.com/yihuiso/archive/2011/04/13/minimarkdown.html");
        }

        private void markdown语法介绍二ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("https://github.com/othree/markdown-syntax-zhtw/blob/master/syntax.md");
        }

        private void markdownSytnxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("http://daringfireball.net/projects/markdown/syntax");
        }
        private void 关于MEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("https://github.com/5d13cn/MEditor/blob/master/resoucesdocs/about.md");
        }
        private void mEditor快捷键ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("https://github.com/5d13cn/MEditor/blob/master/resoucesdocs/shortcut.md");
        }
        private void mEditor网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("https://github.com/5d13cn/MEditor");
        }

        private void 检查最新版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showSyntax("https://github.com/5d13cn/MEditor/raw/master/lastver.txt");
        }
        #endregion

        private void 字体OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectFont();
        }

        private void html预览样式设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editCss();
        }

        private void 通用选项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editCss();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isCancel=!meditorManager.CloseAll();
            if (isCancel)
                e.Cancel = true;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string url = e.Url.ToString();
            if (url == "about:blank") return;

            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {                
                return;
            }
            
            //if (url.EndsWith(".md"))
            //{            
                url = url.Remove(0, 6);
                MarkdownEditor me=meditorManager.GetCurrEditor();
                if(me==null) return ;
                url=Path.Combine(Path.GetDirectoryName(me.FileName),url);

                if (_regexExtMarkdowns.IsMatch(url) || _regexExtOthertext.IsMatch(url))
                {
                    e.Cancel = true;
                    openfile(url);
                }
                //webBrowser1.Navigate(url);
            //}
        }

        //void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    string url = e.Url.ToString();

        //    if (url.EndsWith(".md"))
        //    {
        //        string marktext = webBrowser1.DocumentText;

        //        this.toolStripStatusLabel1.Text = " 正在转换" + url + "。。。。 ";

        //        string html = "";

        //        Markdown mark = new Markdown();

        //        if (!string.IsNullOrEmpty(marktext))
        //        {
        //            html = mark.Transform(marktext);
        //            rtbHtml.Text = marktext;
        //        }

        //        webBrowser1.DocumentText = meditorManager.GetHTMLStyle(html);
        //        tabBrowser.Text = url;
        //        tabBrowser.ToolTipText = url;
        //        this.toolStripStatusLabel1.Text = "当前文档：" + url + ".html";

        //    }
        //}
    }
}