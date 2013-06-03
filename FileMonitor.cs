using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MEditor
{
    public class FileMonitor
    {
        private readonly int TimeoutMillis = 2000;

        private readonly List<String> files = new List<string>();
        private readonly FileSystemEventHandler fswHandler;
        private readonly Timer m_timer;
        private FileSystemWatcher fsw = new FileSystemWatcher();

        public FileMonitor(FileSystemEventHandler watchHandler)
        {
            m_timer = new Timer(OnTimer, null, Timeout.Infinite, Timeout.Infinite);
            fswHandler = watchHandler;
        }


        public FileMonitor(FileSystemEventHandler watchHandler, int timerInterval)
        {
            m_timer = new Timer(OnTimer, null, Timeout.Infinite, Timeout.Infinite);
            TimeoutMillis = timerInterval;
            fswHandler = watchHandler;
        }

        public void Add(string dir, string filter)
        {
            var fsw = new FileSystemWatcher(dir, filter);
            fsw.NotifyFilter = NotifyFilters.LastWrite;
            fsw.Changed += OnFileChanged;
            fsw.EnableRaisingEvents = true;
        }

        public void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            //MessageBox.Show("Created", "Create triggered");
            var mutex = new Mutex(false, "FSW");
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
            var backup = new List<string>();

            var mutex = new Mutex(false, "FSW");
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