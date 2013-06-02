/*
 * 由SharpDevelop创建。
 * 用户： dodola
 * 日期: 2012/10/22
 * 时间: 17:20
 * 
 * 要改变这种模板请点击 工具|选项|代码编写|编辑标准头文件
 */

using System;
using System.Collections;
using System.IO;
using System.Text;
using MEditor.Properties;
using MEditor.Sundownlib;

namespace MEditor
{
    /// <summary>
    ///     Description of Utils.
    /// </summary>
    internal static class Utils
    {
        // Fields
        private static readonly Settings _settings = Settings.Default;
        private static readonly Sundownlib.Sundown _sundown = new Sundown();

        // Methods
        public static string AppendValidHTMLTags(string input, string fileName, bool isLivePreview)
        {
            var builder = new StringBuilder();
            if (!isLivePreview)
            {
                builder.AppendLine(
                    "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            }
            builder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"en\" lang=\"en\">");
            builder.AppendLine("<head>");
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "Markdown";
            }
            builder.AppendLine("<title>" + Path.GetFileName(fileName) + "</title>");
            builder.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            builder.AppendLine("<style type=\"text/css\">");
            if (_settings.HTML_UseCustomStylesheet)
            {
                builder.AppendLine(_settings.HTML_CustomStylesheetSource);
            }
            else
            {
                builder.AppendLine(_settings.HTML_StylesheetSource);
            }
            builder.AppendLine("</style>");
            if (isLivePreview)
            {
                if (_settings.HTML_EnableRelativeImagePaths && (fileName != "Markdown"))
                {
                    builder.AppendLine(@"<base href='file:\\\" + Path.GetDirectoryName(fileName) + @"\'/>");
                }
                builder.AppendLine(
                    "<script type=\"text/javascript\">function loadMarkdown(input) { document.body.innerHTML = input; } </script>");
                builder.AppendLine(
                    "<script type=\"text/javascript\">function scroller(input) {window.scrollTo(0, input * (document.body.scrollHeight - document.body.clientHeight)); } </script>");
            }
            builder.AppendLine("</head>");
            builder.AppendLine("<body>");
            builder.AppendLine(input);
            builder.AppendLine("</body>");
            builder.AppendLine("</html>");
            builder.Append(
                "<!-- This document was created with MarkdownPad, the Markdown editor for Windows (http://markdownpad.com) -->");
            return builder.ToString();
        }

        public static bool Contains(this ArrayList input, string stringToCheck, StringComparison comparison)
        {
            foreach (string str in input)
            {
                if (str.IndexOf(stringToCheck, comparison) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Contains(this string input, string stringToCheck, StringComparison comparison)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return (input.IndexOf(stringToCheck, comparison) >= 0);
        }

        public static string ConvertTextToHTML(string plainText)
        {

            return _sundown.Render(plainText);
        }


        //		public static string GenerateBugReportURL(string formUrl)
        //		{
        //			string version = Version;
        //			string versionString = Environment.OSVersion.VersionString;
        //			if (Environment.Is64BitOperatingSystem)
        //			{
        //				versionString = versionString + " x64";
        //			}
        //			else
        //			{
        //				versionString = versionString + " x86";
        //			}
        //			return (formUrl + "&entry_0=" + SecurityElement.Escape(version) + "&entry_4=" + SecurityElement.Escape(versionString));
        //		}


        //		public static void RenderHTMLinBrowser(string markdown, string fileName)
        //		{
        //			string path = string.Empty;
        //			if (string.IsNullOrEmpty(fileName))
        //			{
        //				path = Path.GetTempPath() + "MarkdownPadPreview.html";
        //			}
        //			else
        //			{
        //				string directoryName = Path.GetDirectoryName(fileName);
        //				string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        //				path = directoryName + @"\" + fileNameWithoutExtension + "-MarkdownPadPreview.html";
        //			}
        //			try
        //			{
        //				File.WriteAllText(path, AppendValidHTMLTags(markdown, fileName, false));
        //			}
        //			catch (Exception exception)
        //			{
        //				MessageBox.Show("An error occurred while trying to save the preview document:\n\n" + exception.Message + "\n\nPlease try again. If the problem persists, please report it under Help --> Report a Bug.", "Error Saving Preview Document", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //			}
        //			path.StartProcess();
        //		}


        public static string WriteDocumentToLocalFile(string fileName, string document, string path)
        {
            string str = DateTime.Now.ToString("yyyyMMddHmmss");
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "MarkdownPad.txt";
            }
            string str2 = path;
            path = str2 + @"\" + Path.GetFileNameWithoutExtension(fileName) + "_backup_" + str +
                   Path.GetExtension(fileName);
            var writer = new StreamWriter(path, true);
            writer.WriteLine(
                string.Concat(new object[] { "MarkdownPad Document Backup: ", fileName, " at ", DateTime.Now }));
            writer.WriteLine("-------------------------------" + Environment.NewLine);
            writer.Write(document);
            writer.Close();
            return path;
        }
    }
}