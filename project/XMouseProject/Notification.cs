using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;

using System.Threading;

namespace XMouse
{
    public partial class Notification : Form
    {
        static Notification _lastNote = null;

        Stopwatch _stopwatch;
        float _liveTime = 1000;


        public Notification()
        {
            //throws error when disposing last window in threading
            CheckForIllegalCrossThreadCalls = false;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor,true);

            InitializeComponent();
            this.BackColor = Color.LightGray;
            this.TopMost = true;
            //this.Enabled = false;

            this.Opacity = 0;
        }

        //show a given notification
        public static void ShowNotification(string msg)
        {
            //close the last notificatio nif it's still open
            if ( _lastNote != null && !_lastNote.IsDisposed )
            {
                //_lastNote.Dispose();
                _lastNote.SetValue(msg,true);
                return;
            }
            _lastNote = new Notification();

            //start a new thread to open the new note in
            Thread t = 
                new Thread(
                    delegate()
                    {
                        Notification nf = _lastNote;

                        nf.Location = new Point(Screen.PrimaryScreen.WorkingArea.Right - nf.Width, Screen.PrimaryScreen.WorkingArea.Bottom - nf.Height);
                        nf.notificationText.Text = msg;

                        try
                        {
                            nf.Show();
                        }
                        catch { }
                    }
                );

            t.Start();

        }

        // resets the notification
        public void SetValue(string msg, bool reset)
        {
            if (reset) _liveTime = 1000;
            this.notificationText.Text = msg;
            this.Opacity = 1.0;
        }

        //close the notification when the x is clicked
        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //when the application loads
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);

            Show();
            Visible = true;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();


            this.TopMost = true;

            //run the loop here
            while (!this.IsDisposed)
            {
                //Console.WriteLine(this.Opacity);

                if (_stopwatch.ElapsedMilliseconds > 16)
                {
                    _liveTime -= _stopwatch.ElapsedMilliseconds;

                    _stopwatch.Restart();

                    SetControllerDotColor(controllerDot0, 0);
                    SetControllerDotColor(controllerDot1, 1);
                    SetControllerDotColor(controllerDot2, 2);
                    SetControllerDotColor(controllerDot3, 3);

                    if (_liveTime < 0)
                    {
                        //fade the opacity
                        this.Opacity -= .05f;
                        if (this.Opacity <= .01f)
                        {
                            Dispose();
                        }
                    }
                    else
                    {
                        this.Opacity += .2f;
                        if (this.Opacity > 1) this.Opacity = 1;
                    }
                }
                //allow the form app to call it events
                Application.DoEvents();
            }
        }

        //update the controller color for each of the items
        void SetControllerDotColor(Label dot, int controller)
        {
            if (!ControllerWork.IsControllerConnected(controller))
            {
                dot.ForeColor = Color.Gray;
            }
            else if (ControlState.GetControllerEnabled(controller))
            {
                dot.ForeColor = Color.Green;
            }
            else
            {
                dot.ForeColor = Color.Gray;
            }
            


        }
    }
}
