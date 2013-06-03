using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Awesomium.Core;
using Awesomium.Core.Data;
using ICSharpCode.AvalonEdit;
using MEditor.Processers;
using MRU;

namespace MEditor
{
    public partial class FrmMain : Form
    {
        //private PrintDocument printDocument = new PrintDocument();

        private MRUManager mruManager;
        private WebSession session;

        public FrmMain()
        {
            InitializeComponent();
            string dataPath = String.Format("{0}{1}Cache", Path.GetDirectoryName(Application.ExecutablePath), Path.DirectorySeparatorChar);

            session = WebCore.Sessions[dataPath] ?? WebCore.CreateWebSession(dataPath, WebPreferences.Default);
            session.AddDataSource("local", new ResourceDataSource(ResourceType.Packed));
            this.webControl1.WebSession = session;

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
            if (HaveSelection())
                meditorManager.GetTextBox().Cut();
        }

        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            if (HaveSelection())
                meditorManager.GetTextBox().Copy();
        }

        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Paste();
        }

        private void 撤销toolStripButton5_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Undo();
        }

        private void 重做toolStripButton6_Click(object sender, EventArgs e)
        {
            meditorManager.GetTextBox().Redo();
        }

        private void 时间日期F5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor rtb = meditorManager.GetTextBox();
            string dat = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            rtb.Document.Insert(rtb.CaretOffset, dat);
        }


        private bool HaveSelection()
        {
            TextEditor editor = meditorManager.GetTextBox();
            return editor != null &&
                   editor.SelectionLength > 0;
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

        private void 打印预览VToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[])e.Data.GetData(DataFormats.FileDrop);
                openfiles(data); //, RichTextBoxStreamType.PlainText);
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
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
            MarkdownEditor markdownEditor = this.meditorManager.GetCurrEditor();
            if (markdownEditor!=null)
            {
                string markdown = markdownEditor.GetMarkdown();
                this.webControl1.Text = Utils.AppendValidHTMLTags(markdown);
            }

            TabSelectRec.Rec(e.TabPageIndex);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            meditorManager.SaveAll();
        }

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

        private void 自动转行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            meditorManager.SetWordWrap(自动转行ToolStripMenuItem.Checked);
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
            Text = Application.ProductName + " V" + Application.ProductVersion;
            //初始化最近打开的文件
            mruManager = new MRUManager();
            mruManager.Initialize(this, 文件ToolStripMenuItem, 最近打开的文件ToolStripMenuItem, // Recent Files menu item
                                  "Software\\YihuiStudio\\MEditor"); // Registry path to keep MRU list
            mruManager.CurrentDir = "....."; // default is current directory
            mruManager.MaxMRULength = 10; // default is 10
            mruManager.MaxDisplayNameLength = 40;

            //定义编辑管理器
            meditorManager = new MarkdownEditorManager(this, tabControl1, mruManager, webControl1);
            ReadCss();
            meditorManager.SetStyle(rtbHtml);

            _filemonitor = new FileMonitor(fsw_Changed);
            string command = Environment.CommandLine; //获取进程命令行参数
            if (!string.IsNullOrEmpty(command))
            {
                string[] para = command.Split('\"');
                if (para.Length > 2)
                {
                    string pathC = para[2]; //获取打开的文件的路径
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

            //			rtbHtml.KeyDown += rtbHtml_KeyDown;
            rtbHtml.DragDrop += frmMain_DragDrop;
            rtbHtml.DragEnter += rtbHtml_DragEnter;

            tabControl1.MouseDown += tabControl1_MouseDown;
            tabControl2.MouseDown += tabControl1_MouseDown;
            //tabControl1.GotFocus += new EventHandler(tabControl1_GotFocus);
        }


        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var tabc = (TabControl)sender;

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

        private void rtbHtml_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
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
            MarkdownEditor me = meditorManager.GetCurrEditor();
            if (me == null) return;

            if (me.GetTextBox().Document.LineCount > 0)
            {
                //Show the dialog
                var gtl = new GoToLineDialog(me.GetTextBox());
                gtl.ShowDialog();
            }
        }

        private void 转为大写ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkdownEditor me = meditorManager.GetCurrEditor();
            if (me == null) return;
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
        }

        private void 查找替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkdownEditor me = meditorManager.GetCurrEditor();
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
        }

        private void 取消md文件关联ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnRegExtfile();
        }

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
            bool isCancel = !meditorManager.CloseAll();
            if (isCancel)
                e.Cancel = true;
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
                SendMessage(Handle.ToInt32(), 0x0112, 0xF012, 0);
            }
        }

        #endregion

        #region 格式设计辅助

        private void 加粗toolStripButton7_Click(object sender, EventArgs e)
        {
            ProcesserFactory.Processe(meditorManager.GetTextBox(), EMark.bold);
        }

        private void 倾斜toolStripButton8_Click(object sender, EventArgs e)
        {
            //			Font newFont = new Font(meditorManager.GetTextBox().SelectionFont, meditorManager.GetTextBox().SelectionFont.Italic ?
            //			                        meditorManager.GetTextBox().SelectionFont.Style & ~FontStyle.Italic : meditorManager.GetTextBox().SelectionFont.Style | FontStyle.Italic);
            //			meditorManager.GetTextBox().SelectionFont = newFont;
        }

        private void 下划线toolStripButton9_Click(object sender, EventArgs e)
        {
            //			Font newFont = new Font(meditorManager.GetTextBox().SelectionFont, meditorManager.GetTextBox().SelectionFont.Underline ?
            //			                        meditorManager.GetTextBox().SelectionFont.Style & ~FontStyle.Underline : meditorManager.GetTextBox().SelectionFont.Style | FontStyle.Underline);
            //			meditorManager.GetTextBox().SelectionFont = newFont;
        }


        #endregion

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextEditor textEditor = meditorManager.GetTextBox();
            string text = Utils.ConvertTextToHTML(textEditor.Text);
            this.rtbHtml.Text = text;

        }
        /// <summary>
        /// 获取源代码
        /// </summary>
        /// <returns></returns>
        private string GrabSources()
        {
            return webControl1.ExecuteJavascriptWithResult("document.getElementsByTagName('html')[0].innerHTML");
        }
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
    }
}