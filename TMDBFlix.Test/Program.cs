using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Windows.Storage;

namespace TMDBFlix.Test
{
    class Program
    {
        static Process cmd;
        static DirectoryInfo downloads;
        static string link;
        static string mode;
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
            if (downloadStarted)
            {
                Console.Clear();
                Console.WriteLine("Deleting temporary files...");

                //do your cleanup here
                downloads.Delete(true);

                //Console.WriteLine("Cleanup complete");

                Thread.Sleep(1000);
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
            downloads = Directory.CreateDirectory(Path.GetTempPath() + "\\flix");
            link = ApplicationData.Current.LocalSettings.Values["link"] as string;
            mode = ApplicationData.Current.LocalSettings.Values["mode"] as string;

            var p = new ProcessStartInfo
            {
                FileName = "cmd",
                RedirectStandardInput = true,
                UseShellExecute = false
            };
            cmd = Process.Start(p);

            cmd.StandardInput.WriteLine($"prompt $g & cls");

            var filenumber = "";
            if (mode.Equals("showFileList"))
            {
                cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" -l");
                Console.WriteLine(link);
                filenumber = "-i " + Console.ReadLine();
            }

            downloadStarted = true;

            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);

            cmd.StandardInput.WriteLine($"cls & peerflix \"{link}\" {filenumber} -f {downloads.FullName} --mpchc");
            Console.WriteLine(link);
        }
    }

}
