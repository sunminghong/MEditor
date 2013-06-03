﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MEditor.Properties;
using MRU;


namespace MEditor
{
    public partial class FrmMain : Form, IMRUClient
    {
        private static string _myExtName = ".md";
        private readonly SortedList<string, bool> _monitorList = new SortedList<string, bool>();
        private readonly Regex _regexExtHtml = new Regex(@"\.htm|\.xml", RegexOptions.Compiled);
        private readonly Regex _regexExtMarkdowns = new Regex(@"\.md|\.markdown", RegexOptions.Compiled);
        private readonly string myExtName = "Markdown文件|*" + _myExtName + "|所有文件|*.*";


        private Color _bgColor = Color.FromArgb(0x4a, 0x52, 0x5a); //20,0x20,0x20;

        private string _extfileother = "";
        private FileMonitor _filemonitor;
        private Font _font = new Font("微软雅黑", 12);
        private Color _foreColor = Color.FromArgb(0xff, 0xff, 0xff); //0xf2,0xf0,0xdf

        private Regex _regexExtOthertext = new Regex(@"\.txt|\.js|\.htm|\.xml|\.as|\.log|\.php|\.cs",
                                                     RegexOptions.Compiled);

        private bool isLeft = true;
        private MarkdownEditorManager meditorManager;

        #region defaultCss

        private string _defcss = @"
body,td,th {font-family:""微软雅黑"", Verdana, ""Bitstream Vera Sans"", sans-serif; }
body {
	margin-top: 0;
	padding: 0;	
	line-height: 1.5em;
	color: #{1};
	background-color: #{0}; 
	text-align: left;
	}
a em {
	padding-bottom: 2px;
	}


sup {
	line-height: 0px;
	}
sup a:link {
	padding: 3px;
	}
sup a:visited {
	padding: 3px;
	}


p {
 	margin: 0 0 1.6em 0;
	padding: 0;
	}

h1 {
	font-family: ""黑体"",""Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Georgia, serif;
	font-size: 1.55em; 
	line-height: 1.5em;
	text-align: left;
	font-weight: bold;
	margin: 0em 0px 1.25em 0px;
	}

h2 {
	font-family:""黑体"", ""Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Verdana, ""Bitstream Vera Sans"", sans-serif;
	font-size: 1.1em; /* 1 */
	text-align: left;
	font-weight:bold;
	line-height: 1.8em;
	letter-spacing: .2em;
	margin: 1em 0 1em 0;
	text-transform: uppercase;
	}

h1 + h2 {
	margin-top: 2em;
	}
h2 + h3 {
	margin-top: 1.5em;
	}

h3 {
	font-family: ""黑体"",Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Verdana, ""Bitstream Vera Sans"", sans-serif;
	font-size: .91em;
	text-align: left;
	font-weight: normal;
	line-height: 1.8em;
	letter-spacing: .2em;
	margin-bottom: 0.4em;
	margin-top: 0.5em;
	text-transform: uppercase;
	}

p + h3 {
	margin-top: 4em;
	}
pre + h3 {
	margin-top: 4em;
	}

h6 + h2 {
	margin-top: 2em;
	}

h4, h5, h6 {
	font-family: Verdana, ""Bitstream Vera Sans"", sans-serif;
	font-size: 1em;
	text-align: left;
	font-weight: bold;
	line-height: 1.5em;
	margin: 1em 0 0 0;
	}

strong {
	font-weight: bold;
	font-size: .91em;
	letter-spacing: .2em;
	text-transform: uppercase;
	}

em {
	font-style: normal;
	}
blockquote {
	font-size: 1em;
	margin: 2em 2em 2em 1em;
	padding: 0 .75em 0 1.25em;
	border-left: 1px solid #777;
	border-right: 0px solid #777;
	}

blockquote strong {
	font-weight: bold;
	font-size: 1em;
	letter-spacing: normal;
	text-transform: none;
	}


img {
	margin-top: 5px;
	}

thead {
	font-weight: bold;
	}


ul, ol {
	padding-left: 1.25em;
	margin: 0 0 2em 1em;
	}


pre {
	font-family: ""Bitstream Vera Sans Mono"", Courier, Monaco, ProFont, ""American Typewriter"", ""Andale Mono"", monospace;
	line-height: 1.45em;
	color: #{1};
	background-color: inherit;
	margin: 0.5em 0 0.5em 0;
	padding: 5px 0 5px 10px;
	border-width: 1px 0 1px 0;
	border-color: #6b6b6b;
	border-style: dashed;
	}

code {
	font-family: Monaco, ProFont, ""Bitstream Vera Sans Mono"", ""American Typewriter"", ""Andale Mono"", monospace;
	font-size: 0.91em; /* 1.09em for Courier */
	}


ul {
	list-style-type: square;
	}

ul ul {
	list-style-type: disc;
	}
ul ul ul {
	list-style-type: circle;
	}

hr {
	height: 1px;
	margin: 1em 1em 1em 0;
	text-align: center;
	border-color: #777;
	border-width: 0;
	border-style: dotted;
	}

dt {
	font-family: Verdana, ""Bitstream Vera Sans"", sans-serif;
	font-size: 1em;
	text-align: left;
	font-weight: normal;
	margin: 0 0 .4em 0;
	letter-spacing: normal;
	text-transform: none;
	}
dd {
	margin: auto auto 1.5em 1em;
	}

dd p {
	margin: 0 0 1em 0;
	}

table{
    border:1px #CCC solid;    
border-collapse: separate;border-spacing: 0;
}
th,td{padding:5px;border: 1px solid #CCC;}
";

        #endregion

        /// <summary>
        ///     Gets a value indicating if the operating system is a Windows Vista or a newer one.
        /// </summary>
        public static bool IsWindowsVistaOrNewer
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Win32NT) &&
                       (Environment.OSVersion.Version.Major >= 6);
            }
        }

        public void OpenMRUFile(string fileName)
        {
            openfile(fileName);
        }

        public void ReadCss()
        {
            _font = Settings.Default.font;
            _foreColor = Settings.Default.color;
            _bgColor = Settings.Default.bgcolor;
            _defcss = Settings.Default.css;
            _extfileother = Settings.Default.extfile;

            _regexExtOthertext = new Regex(_regexExtOthertext.Replace(".", @"\."), RegexOptions.Compiled);

            meditorManager.SetFont(_font);
            meditorManager.SetForeColor(_foreColor);
            meditorManager.SetBackColor(_bgColor);
            meditorManager.SetCss(_defcss);

            //MessageBox.Show(Settings.Default.appconfig.ToString());
        }

        public void openfiles(string[] fileNames)
        {
            foreach (string s in fileNames)
                openfile(s);
        }

        private void openfile(string fileName)
        {
            if (meditorManager.Open(fileName))
            {
                Add(fileName);
            }
            else
                mruManager.Remove(fileName); // when Open File operation failed
        }

        public void Add(string fullpath)
        {
            string dir = Path.GetDirectoryName(fullpath);
            if (_monitorList.ContainsKey(dir))
            {
                return;
            }
            _monitorList.Add(dir, true);
            _filemonitor.Add(dir, "*.md");
        }

        private void fsw_Changed(object sender, FileSystemEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new dirupdated(meditorManager.RefrushOpen), e.Name);
                return;
            }
            meditorManager.RefrushOpen(e.Name);
        }

        private void openfile()
        {
            var fileone = new OpenFileDialog();
            fileone.Filter = myExtName;
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
                    mruManager.Remove(fileone.FileName); // when Open File operation failed
                }
            }
        }



        private bool filetypeConvert(MarkdownEditor meditor)
        {
            string marktext = meditor.GetMarkdown();
            if (string.IsNullOrEmpty(marktext))
            {
                return false;
            }

            string ext = Path.GetExtension(meditor.FileName);
            if (_regexExtMarkdowns.IsMatch(ext))
            {
                string html = "";
                html = Utils.ConvertTextToHTML(marktext);
                rtbHtml.Text = html;
                webControl1.LoadHTML(meditorManager.GetHTMLStyle(html));
                return true;
            }

            if (_regexExtHtml.IsMatch(ext))
            {
                webControl1.LoadHTML(marktext);
                rtbHtml.Text = marktext;
                return true;
            }

            if (_regexExtOthertext.IsMatch(ext))
            {
                webControl1.LoadHTML(meditorManager.GetHTMLStyle("<pre><code>" + marktext + "</code></pre>"));
                rtbHtml.Text = marktext;
                return true;
            }
            return false;
        }

        public void SelectForeColor()
        {
            var color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                _foreColor = color.Color;
                meditorManager.SetForeColor(_foreColor);
                meditorManager.SetStyle(rtbHtml);

                //Settings.Default.color = _foreColor;
                //Settings.Default.Save();
            }
        }

        public void SetOldStyle()
        {
            _bgColor = Color.FromArgb(0xff, 0xff, 0xff); //20,0x20,0x20;
            _foreColor = Color.FromArgb(0x00, 0x00, 0x00); //0xf2,0xf0,0xdf
            _font = new Font("微软雅黑", 12);

            setstyle();
            //SaveSettings();
        }

        public void SetBlackWhiteStyle()
        {
            _bgColor = Color.FromArgb(0x4a, 0x52, 0x5a); //20,0x20,0x20;
            _foreColor = Color.FromArgb(0xff, 0xff, 0xff); //0xf2,0xf0,0xdf
            _font = new Font("微软雅黑", 12);
            setstyle();
            //SaveSettings();
        }

        private void setstyle()
        {
            meditorManager.SetFont(_font);
            meditorManager.SetForeColor(_foreColor);
            meditorManager.SetBackColor(_bgColor);

            meditorManager.SetStyle(rtbHtml);
        }

        private string convertColor(Color co)
        {
            return co.R.ToString() + "," + co.G.ToString() + "," + co.B.ToString();
        }

        public void SaveSettings()
        {
            string appconfig = Settings.Default.appconfig;

            appconfig = appconfig.Replace("{font}", _font.ToString());
            appconfig = appconfig.Replace("{color}", convertColor(_foreColor));
            appconfig = appconfig.Replace("{bgcolor}", convertColor(_bgColor));
            appconfig = appconfig.Replace("{css}", _defcss);
            appconfig = appconfig.Replace("{extfile}", _extfileother);

            string conffile = Application.UserAppDataPath;
            conffile = Application.ExecutablePath + ".config";
            using (var sw = new StreamWriter(conffile, false, Encoding.UTF8))
            {
                sw.Write(appconfig);
                sw.Close();
            }
        }

        public void SelectBackColor()
        {
            var color = new ColorDialog();
            if (color.ShowDialog() == DialogResult.OK)
            {
                _bgColor = color.Color;
                meditorManager.SetBackColor(_bgColor);
                meditorManager.SetStyle(rtbHtml);

                //Settings.Default.bgcolor = _bgColor;
                //Settings.Default.Save();
            }
        }

        public void SelectFont()
        {
            var font = new FontDialog();
            if (font.ShowDialog() == DialogResult.OK)
            {
                _font = font.Font;
                meditorManager.SetFont(_font);
                meditorManager.SetStyle(rtbHtml);

                //Settings.Default.font =_font;
                //Settings.Default.Save();
            }
        }

        public void SetCss(string css)
        {
            _defcss = css;
            meditorManager.SetCss(_defcss);

        }

        public void SetExt(string ext)
        {
            _extfileother = ext;
            _regexExtOthertext = new Regex(_extfileother.Replace(".", @"\."), RegexOptions.Compiled);
        }

        private void editCss()
        {
            var fcss = new frmCss(_defcss, this);
            fcss.ShowDialog();
        }

        private void showSyntax(string url)
        {
            toolStripStatusLabel1.Text = "正在打开" + url + "...";
            Process.Start(url);
            //webBrowser1.Navigate(url);
            //webBrowser1.DocumentText = html;

            if (splitContainer1.Panel2Collapsed)
                splitContainer1.Panel2Collapsed = false;
        }

        private void SwithFlaout()
        {
            if (isLeft)
            {
                splitContainer1.Panel2.Controls.Add(tabControl1);
                splitContainer1.Panel1.Controls.Add(tabControl2);
                if (splitContainer1.Panel2Collapsed)
                {
                    splitContainer1.Panel2Collapsed = false;
                    splitContainer1.Panel1Collapsed = true;
                }
                isLeft = false;
                return;
            }
            splitContainer1.Panel1.Controls.Add(tabControl1);
            splitContainer1.Panel2.Controls.Add(tabControl2);
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
                splitContainer1.Panel2Collapsed = true;
            }
            isLeft = true;
        }

        private void UnRegExtfile()
        {
            bool rel = FileTypeRegister.UnRegister(_myExtName);

            if (!rel && IsWindowsVistaOrNewer)
            {
                MessageBox.Show("您是在windows7 下运行的此程序，如果执行完扩展名关联但没有成功，请您以“管理员权限方式再打开此程序再关联一次”！");
            }
        }

        private void regExtFile()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string resourceName = "MEditor.logo.ico";
            Stream stream = asm.GetManifestResourceStream(resourceName);
            string icofile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                             "\\markdown.ico";
            var fs = new FileStream(icofile, FileMode.Create, FileAccess.Write);
            var bs = new byte[stream.Length];
            stream.Read(bs, 0, (int)stream.Length);
            fs.Write(bs, 0, bs.Length);
            fs.Flush();
            fs.Close();
            fs.Dispose();

            //注册关联的文件扩展名
            var fr = new FileTypeRegInfo();
            fr.Description = "markdown文档格式";
            fr.ExePath = Application.ExecutablePath;
            fr.ExtendName = _myExtName;
            fr.IcoPath = icofile;

            bool rel = true;
            if (FileTypeRegister.CheckIfRegistered(fr.ExtendName))
            {
                rel = FileTypeRegister.UpdateRegInfo(fr);
            }
            else
            {
                rel = FileTypeRegister.Register(fr);
            }

            if (!rel && IsWindowsVistaOrNewer)
            {
                MessageBox.Show("您是在windows7 下运行的此程序，如果执行完扩展名关联但没有成功，请您以“管理员权限方式再打开此程序再关联一次”！");
            }
        }

        private delegate bool dirupdated(string filename);
    }
}