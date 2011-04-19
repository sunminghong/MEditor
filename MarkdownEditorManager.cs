﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using MRU;

namespace MEditor
{
    public class MarkdownEditorManager
    {
        private static string _myExtName=".mark";
        private string myExtName = "Markdown文件|*" + _myExtName + "|所有文件|*.*";

        private Form _thisForm;
        private TabControl _tabparent;

        private int _fileInd = 0;
        private int noName = 0;

        SortedList<int, MarkdownEditor> _editors = new SortedList<int, MarkdownEditor>();

        private string _workSpace;
        //private int maxDisplayLength = 10;

        private static Color BGColor = System.Drawing.SystemColors.Window;
        private static Color FOREColor = System.Drawing.SystemColors.WindowText;
        private static Font FONT=new Font("",12);

        //private Color  _bgColor = Color.FromArgb(0x20,0x20,0x20);
        //private Color  _foreColor = Color.FromArgb(0xf2,0xf0,0xdf);         

         private Color             _bgColor = Color.FromArgb(0x4a,0x52,0x5a);//20,0x20,0x20;
         private Color     _foreColor = Color.FromArgb(0xff,0xff,0xff); //0xf2,0xf0,0xdf
        private Font _font = FONT;

        private bool _wordWrap = false;

        private MRUManager mruManager;

        public MarkdownEditorManager(Form thisform, TabControl tabparent,MRUManager mru)
        {
            this._tabparent = tabparent;
            this._thisForm = thisform;
            _workSpace = Application.StartupPath;

            mruManager = mru;
        }

        public bool Open(string file)
        {
            if (string.IsNullOrEmpty(file))
            {
                file =Application.StartupPath+"\\未命名" + (noName++).ToString() + ".mark";
            }
            else
            {
                foreach (MarkdownEditor me in _editors.Values)
                {
                    if (me.FileName == file)
                    {
                        _tabparent.SelectedTab = me.MarkdownPage;
                        return true;
                    }
                }
            }

            TabPage markPage = new TabPage(GetDisplayName(file));
            markPage.ToolTipText = file;
            _tabparent.TabPages.Add(markPage);
            markPage.Tag = _fileInd;

            MarkdownEditor meditor = new MarkdownEditor(_thisForm, markPage);
            _editors.Add(_fileInd, meditor);
            _fileInd++;
            meditor.SetStyle(_bgColor, _foreColor, _font, _wordWrap);
            bool rel = meditor.Openfile(file);
            if (rel)
            {
                _tabparent.SelectedTab = markPage;
            }

            return rel;
        }

        public MarkdownEditor GetCurrEditor()
        {
            if (_tabparent.SelectedTab == null)
                return null;

            int ind=int.Parse(_tabparent.SelectedTab.Tag.ToString());
            if (_editors.ContainsKey(ind))
                return _editors[ind];
            else
                return null;
        }

        public RichTextBox GetTextBox()
        {
            MarkdownEditor meditor = GetCurrEditor();
            if (meditor == null)
                return null;

            return meditor.GetTextBox();
        }

        public void SaveAll()
        {
            foreach (int ind in _editors.Keys)
            {
                save(_editors[ind]);
            }
        }

        public void CloseAll()
        {
            foreach (int ind in _editors.Keys)
            {
                close(_editors[ind]);
            }
        }
        
        public void Save()
        {
            MarkdownEditor meditor = GetCurrEditor();
            if (meditor == null)
                return;
             
            save(meditor);
        }

        private void save(MarkdownEditor meditor)
        {
            if (string.IsNullOrEmpty(meditor.FileName) || meditor.FileName.IndexOf("未命名")!=-1)
            {
                saveas(meditor);
                return;
            }
            meditor.Save();
            mruManager.Add(meditor.FileName);          // when file is successfully opened
        }
        public void SaveAs()
        {
            MarkdownEditor meditor = GetCurrEditor();
            if (meditor == null)
                return;
                saveas(meditor);
        }

        private void saveas(MarkdownEditor meditor)
        {
            SaveFileDialog fileone = new SaveFileDialog();
            fileone.Filter = myExtName;
            fileone.FilterIndex = 1;
            if (fileone.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    meditor.Save(fileone.FileName);
                    mruManager.Add(meditor.FileName);          // when file is successfully opened

                }
                catch (ArgumentException)
                {
                    MessageBox.Show("保存不成功");
                }
            }
        }

        public void openDir()
        {
            MarkdownEditor meditor = GetCurrEditor();
            if (meditor == null)
                return;

            System.IO.FileInfo fi = new FileInfo(meditor.FileName);

            System.Diagnostics.Process.Start("explorer.exe", fi.Directory.ToString());

        }
        
        public void Close()
        {
            MarkdownEditor meditor = GetCurrEditor();
            if (meditor == null)
                return;

            close(meditor);
        }

        private void close(MarkdownEditor meditor)
        {
            if (meditor.AlreadyUpdate)
            {
                if (MessageBox.Show("修改还没有保存，需要保存吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    save(meditor);
                }
            }

            meditor.Close();
            _editors.Remove((int)meditor.MarkdownPage.Tag);
            _tabparent.TabPages.Remove(meditor.MarkdownPage);
        }


        [DllImport("shlwapi.dll", CharSet = CharSet.Auto)]
        private static extern bool PathCompactPathEx(
            StringBuilder pszOut,
            string pszPath,
            int cchMax,
            int reserved);


        /// <summary>
        /// Get display file name from full name.
        /// </summary>
        /// <param name="fullName">Full file name</param>
        /// <returns>Short display name</returns>
        private string GetDisplayName(string fullName)
        {
            // if file is in current directory, show only file name
            FileInfo fileInfo = new FileInfo(fullName);
            return fileInfo.Name;

            //if (fileInfo.DirectoryName == _workSpace)
            //    return GetShortDisplayName(fileInfo.Name, maxDisplayLength);

            //return GetShortDisplayName(fullName, maxDisplayLength);
        }

        /// <summary>
        /// Truncate a path to fit within a certain number of characters 
        /// by replacing path components with ellipses.
        /// 
        /// This solution is provided by CodeProject and GotDotNet C# expert
        /// Richard Deeming.
        /// 
        /// </summary>
        /// <param name="longName">Long file name</param>
        /// <param name="maxLen">Maximum length</param>
        /// <returns>Truncated file name</returns>
        private string GetShortDisplayName(string longName, int maxLen)
        {
            StringBuilder pszOut = new StringBuilder(maxLen + maxLen + 2);  // for safety

            if (PathCompactPathEx(pszOut, longName, maxLen, 0))
            {
                return pszOut.ToString();
            }
            else
            {
                return longName;
            }
        }

        private string convertColor(Color co)
        {
            return "#"+co.R.ToString("X2") + co.G.ToString("X2") + co.B.ToString("X2");
        }
        public string GetHTMLStyle(string cont)
        {
            string htmlformat = "<html><head><style>body{background-color:"+convertColor(_bgColor)
                +";color:"+convertColor(_foreColor)+";}"+
@"@charset ""utf-8"";
body {
	margin-top: 0;
	padding: 0;
	font-family: Verdana, ""Bitstream Vera Sans"", sans-serif;
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
	font-family: ""Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Georgia, serif;
	font-size: 1.55em; 
	line-height: 1.5em;
	text-align: left;
	font-weight: bold;
	margin: 0em 0px 1.25em 0px;
	}

h2 {
	font-family: ""Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Verdana, ""Bitstream Vera Sans"", sans-serif;
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
	font-family: ""Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Verdana, ""Bitstream Vera Sans"", sans-serif;
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
	font-weight: normal;
	font-size: .91em;
	letter-spacing: .2em;
	text-transform: uppercase;
	}

em em {
	font-style: normal;
	}

strong strong {
	font-weight: bold;
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
	list-style-type: square;
	}
ul ul ul {
	list-style-type: square;
	}

hr {
	height: 1px;
	margin: 2em 1em 4em 0;
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

h2.dateline {
	margin: 2em 0 0 0;
	font-family: ""Gill Sans"", ""Gill Sans Std"", ""Gill Sans MT"", Georgia, serif;
	}
dl + h2.dateline {
	margin: 5em 0 2em 0;
	}

table{
    border:1px #ececec solid;    
border-collapse: separate;border-spacing: 0;
}
th,td{padding:5px;border: 1px solid #CCC;}
"
                + @"</style></head><body>"+cont+"</body></html>";

            return htmlformat;
        }

        public MarkdownEditor SetStyle()
        {
            MarkdownEditor m = GetCurrEditor();
            if (m == null)
                return null;
            
            m.SetStyle(_bgColor, _foreColor,_font,_wordWrap);

            return m;
        }

        public void SetStyle(RichTextBox rtb)
        {
            if (rtb.BackColor != _bgColor)
                rtb.BackColor = _bgColor;
            if (rtb.ForeColor != _foreColor)
                rtb.ForeColor = _foreColor;
            if (rtb.Font != _font)
                rtb.Font = _font;
            if (rtb.WordWrap != _wordWrap)
                rtb.WordWrap = _wordWrap;
        }

        public void SetOldStyle()
        {
            _bgColor = BGColor;
            _foreColor = FOREColor;
            _font = FONT;
            SetStyle();
        }
        
        public void SetBlackWhiteStyle()
        {
            _bgColor = Color.FromArgb(0x4a,0x52,0x5a);//20,0x20,0x20;
            _foreColor = Color.FromArgb(0xff,0xff,0xff); //0xf2,0xf0,0xdf
            _font = FONT;
            SetStyle();
        }

        public void SetBackColor(Color co)
        {
            if(_bgColor==co)
                return;
            
            _bgColor = co;
            SetStyle();
        }

        public void SetForeColor(Color co)
        {
            if (_foreColor == co)
                return;

            _foreColor = co;
            SetStyle();
        }

        public void SetFont(Font font)
        {
            if (_font == font)
                return;
            _font = font;
            SetStyle();
        }
        public void SetWordWrap(bool wordWarp)
        {
            if (_wordWrap == wordWarp)
                return;
            _wordWrap = wordWarp;
            SetStyle();
        }
    }
}