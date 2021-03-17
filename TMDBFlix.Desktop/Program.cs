using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.Storage;

namespace TMDBFlix.Desktop
{
    class Program
    {
        static Process cmd;
        static DirectoryInfo folder;

        static bool showfiles = true;
        static bool keepfiles = true;
        static string folderpath;
        static string autoplay;

        static string link;

        static bool downloadStarted = false;
        static bool exitSystem = false;

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

        #region Trap application termination
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            keepfiles = (bool)ApplicationData.Current.LocalSettings.Values["keepfiles"];

            if (!keepfiles)
            {
                if (downloadStarted)
                {
                    Console.Clear();
                    Console.WriteLine("Deleting temporary files...");

                    //do your cleanup here
                    var subfolders = new List<DirectoryInfo>();
                    var subfolderpaths = Directory.GetDirectories(folder.FullName);

                    if(subfolderpaths.Length == 0)
                    {
                        var files = new List<FileInfo>();
                        var filepaths = Directory.GetFiles(folder.FullName);

                        foreach (var f in filepaths)
                        {
                            files.Add(new FileInfo(f));
                        }
                        files = files.OrderByDescending(x => x.CreationTimeUtc).ToList();
                        files[0].Delete();
                    }
                    foreach (var f in subfolderpaths)
                    {
                        subfolders.Add(new DirectoryInfo(f));
                    }
                    subfolders = subfolders.OrderByDescending(x => x.CreationTimeUtc).ToList();
                    subfolders[0].Delete(true);

                    //Console.WriteLine("Cleanup complete");

                    Thread.Sleep(1000);
                }
            }

            //allow main to run off
            exitSystem = true;

            //shutdown right away so there are no lingering threads
            Environment.Exit(-1);

            return true;
        }
        #endregion

        static void Main(string[] args)
        {
            // Some biolerplate to react to close window event, CTRL-C, kill, etc
            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

            //start your multi threaded program here
            Program p = new Program();
            p.Start();

            //hold the console so it doesn’t run off the end
            while (!exitSystem)
            {
                Thread.Sleep(500);
            }
        }

        public void Start()
        {
            showfiles = (bool) ApplicationData.Current.LocalSettings.Values["showfiles"];
            keepfiles = (bool) ApplicationData.Current.LocalSettings.Values["keepfiles"];
            folderpath = ApplicationData.Current.LocalSettings.Values["folderpath"] as string;
            autoplay = ApplicationData.Current.LocalSettings.Values["autoplay"] as string;
            link = ApplicationData.Current.LocalSettings.Values["link"] as string;

            if (folderpath != null && !folderpath.Equals("")) folder = Directory.CreateDirectory(folderpath);
            else folder = Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "\\TMDBFlix");
            
            var p = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                UseShellExecute = false
            };
            cmd = Process.Start(p);

            cmd.StandardInput.WriteLine($"prompt $g & cls");

            var filenumber = "";
            if (showfiles)
            {
                cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" -l");
                filenumber = "-i " + Console.ReadLine();
            }

            downloadStarted = true;
            
            switch (autoplay)
            {
                case "-":
                    cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" {filenumber} -f \"{folder.FullName}\"");
                    break;
                case "vlc":
                    cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" {filenumber} -f \"{folder.FullName}\" --vlc");
                    break;
                case "mpc-hc":
                    cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" {filenumber} -f \"{folder.FullName}\" --mpchc");
                    break;
                case "potplayer":
                    cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" {filenumber} -f \"{folder.FullName}\" --potplayer");
                    break;
                default:
                    cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" {filenumber} -f \"{folder.FullName}\"");
                    break;
            }

            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            if (!autoplay.Equals("-"))
            {
                ShowWindow(handle, 7);

                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(
                delegate (object o, DoWorkEventArgs args)
                {
                    BackgroundWorker b = o as BackgroundWorker;
                    Thread.Sleep(5000);

                    switch (autoplay)
                    {
                        case "vlc":
                            while (Process.GetProcesses().Any(x => x.ProcessName.Contains("vlc")))
                            {
                                Thread.Sleep(1000);
                            }
                            break;
                        case "mpc-hc":
                            while (Process.GetProcesses().Any(x => x.ProcessName.Contains("mpc-hc")))
                            {
                                Thread.Sleep(1000);
                            }
                            break;
                        case "potplayer":
                            while (Process.GetProcesses().Any(x => x.ProcessName.Contains("PotPlayer")))
                            {
                                Thread.Sleep(1000);
                            }
                            break;
                    }

                });

                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs args)
                {
                    ShowWindow(handle, 9);
                });

                bw.RunWorkerAsync();
            }
            
        }
    }

}
