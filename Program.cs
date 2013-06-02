using System;
using System.Threading;
using System.Windows.Forms;
using MEditor.TestForm;

namespace MEditor
{
    internal static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //   Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(new frmMain());
            //Application.Run(new TestSundown());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Sorry,出现了一个错误！如果严重的影响了您的工作，请将它发给我allen.fantasy@gmail.com" + e.Exception.StackTrace);
        }
    }
}