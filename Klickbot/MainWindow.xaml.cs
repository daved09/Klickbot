using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Runtime.InteropServices;
using form = System.Windows.Forms;

namespace Klickbot
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool GetAsyncKeyState(form.Keys vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern Key GetAsyncKeyState();
        

        Thread klickThread;
        Thread key;

        public int Pause { get; set; }

        public int Speed { get; set; }

        public form.Keys HotKey { get; set; }

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;


        public MainWindow()
        {
            HotKey = form.Keys.F2;
            keyListener();
            klickThread = null;
            InitializeComponent();
            init();
        }

        private void init()
        {
            txtHotkey.Text = "Hotkey = " + HotKey;
            txtSpeed.Text = "1";
            key.Start();
            Closed += (sder, eargs) =>
            {
                key.Abort();
                if (klickThread != null)
                {
                    klickThread.Abort();
                }
            };
            isActive();
        }

        

        public void klickBot()
        {
            const int grenze = 1000;
            try
            {
                while (true)
                {
                    int X = form.Cursor.Position.X;
                    int Y = form.Cursor.Position.Y;
                    if(Speed > grenze)
                    {
                        for (int i = 0; i < Speed; i++)
                        {
                            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                        }
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                        mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                        Thread.Sleep(Pause);
                    }
                }
            }
            catch(ThreadAbortException E)
            {
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }      

        public void startAutoklicker()
        {
            if (klickThread == null)
                klickThread = new Thread(new ThreadStart(klickBot));
            if ((txtSpeed.Text.Equals("")) || (txtSpeed.Text.Equals("0")))
                MessageBox.Show("Das Textfeld darf nicht leer sein oder eine 0 enthalten.");
            else if ((klickThread.ThreadState == ThreadState.WaitSleepJoin) || (klickThread.ThreadState == ThreadState.Running))
            {
                klickThread.Abort();
                klickThread = null;
            }
            else
            {
                Speed = Int32.Parse(txtSpeed.Text);
                Pause = 1000 / Speed;
                klickThread.Start();
            }
        }

        public void keyListener()
        {
            key = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        bool focus = false;
                        txtHotkey.Dispatcher.Invoke(
                            new Action(() => { focus = txtHotkey.IsFocused; }));
                        int X = form.Cursor.Position.X;
                        int Y = form.Cursor.Position.Y;
                        lblMousePosition.Dispatcher.Invoke(
                        new Action(() => lblMousePosition.Content = "X = " + X + " Y = " + Y));
                        bool actualKey = GetAsyncKeyState(HotKey);
                        if ((actualKey) && (!focus))
                        {
                            txtSpeed.Dispatcher.Invoke(
                                new Action(() => { startAutoklicker(); }));
                            Thread.Sleep(200);
                            lblActive.Dispatcher.Invoke(
                                new Action(() => { isActive(); }));
                        }
                        Thread.Sleep(50);
                    }
                }
                catch (ThreadAbortException E)
                {
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error");
                }
            });           
        }
        

        public void isActive()
        {
            const string title = "Klickbot";
            string text;
            const bool notifiytion = false;
            if (klickThread == null)
            {
                lblActive.Content = "Deaktiviert";
                text = "Autoklicker deaktiviert.";
            }
            else
            {
                lblActive.Content = "Aktiviert (Geschwindigkeit: " + Speed + ")";
                text = "Autoklicker aktiviert mit Geschwindigkeit: " + Speed + ".";
            }
        }

        private void txtHotkey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtHotkey.Text = e.Key.ToString();
            HotKey = (form.Keys)KeyInterop.VirtualKeyFromKey(e.Key);
            txtHotkey.Text = "";
            txtHotkey.Text = "Hotkey = " + HotKey;
        }
    }
}
