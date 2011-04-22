using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;


namespace MEditor
{

    public class FileMonitor
    {
        private int TimeoutMillis = 2000;

        System.IO.FileSystemWatcher fsw = new System.IO.FileSystemWatcher();
        System.Threading.Timer m_timer = null;
        List<String> files = new List<string>();
        FileSystemEventHandler fswHandler = null;

        public FileMonitor(FileSystemEventHandler watchHandler)
        {
            m_timer = new System.Threading.Timer(new TimerCallback(OnTimer), null, Timeout.Infinite, Timeout.Infinite);
            fswHandler = watchHandler;

        }


        public FileMonitor(FileSystemEventHandler watchHandler, int timerInterval)
        {
            m_timer = new System.Threading.Timer(new TimerCallback(OnTimer), null, Timeout.Infinite, Timeout.Infinite);
            TimeoutMillis = timerInterval;
            fswHandler = watchHandler;

        }

        public void Add(string dir,string filter)
        {
            System.IO.FileSystemWatcher fsw = new System.IO.FileSystemWatcher(dir, filter);
            fsw.NotifyFilter = System.IO.NotifyFilters.LastWrite;
            fsw.Changed += new FileSystemEventHandler(OnFileChanged);
            fsw.EnableRaisingEvents = true;
        }

        public void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("Created", "Create triggered");
            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            if (!files.Contains(e.FullPath))
            {
                files.Add(e.FullPath);
            }
            mutex.ReleaseMutex();

            m_timer.Change(TimeoutMillis, Timeout.Infinite);
        }

        private void OnTimer(object state)
        {
            List<String> backup = new List<string>();

            Mutex mutex = new Mutex(false, "FSW");
            mutex.WaitOne();
            backup.AddRange(files);
            files.Clear();
            mutex.ReleaseMutex();


            foreach (string file in backup)
            {
                fswHandler(this, new FileSystemEventArgs(WatcherChangeTypes.Changed, string.Empty, file));
            }

        }
    }
}
