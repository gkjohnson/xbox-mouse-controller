using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//added libraries
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

//my libraries
using InputRobot;

using System.Threading;

namespace XMouse
{
    //the program that will move the mouse
    class XMouseService : Form
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run( new XMouseService() );
        }

        private Settings _lastSettingsWindow;
        private NotifyIcon _icon;
        private ContextMenu _menu;
        //private MenuItem _enableMenuItem;
        //private MenuItem[] _enablePlayers = new MenuItem[4];

        GamePadState[] _prevGamePadStates = new GamePadState[4];

       
        //constructor for the main application
        public XMouseService()
        {
            // Create a simple tray menu with only one item.
            _menu = new ContextMenu();

            //create the settings item
            MenuItem settingsMenu = new MenuItem("Settings", _icon_Click);
            _menu.MenuItems.Add(settingsMenu);

            //add the break
            _menu.MenuItems.Add(new MenuItem("-"));

            //exit option is last
            MenuItem exitMenu = new MenuItem("Exit", Quit);
            _menu.MenuItems.Add(exitMenu);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            _icon = new NotifyIcon();
            _icon.Text = "XMouse";
            
            // Add menu to tray icon and show it.
            _icon.ContextMenu = _menu;
            _icon.Visible = true;



            ControlState.SetSystemEnabled(true);
            ControlState.SetControllerEnabled(0, true);


            _icon.Click += new EventHandler(_icon_Click);
        }

        void _icon_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me == null || me.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (_lastSettingsWindow == null || _lastSettingsWindow.IsDisposed)
                {
                    _lastSettingsWindow = new Settings();
                    _lastSettingsWindow.TopMost = true;
                    _lastSettingsWindow.Show();
                    _lastSettingsWindow.TopMost = false;
                }
                else
                {
                    _lastSettingsWindow.Activate();
                }
            }
        }
        
        //Quit the application
        void Quit(object sender, EventArgs e) { Application.Exit(); }


        //when the application loads
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //hide the window and the taskbar icon
            Visible = false;
            ShowInTaskbar = false;

            //run the controller code
            bool run = true;
            Thread t = 
                new Thread(
                    delegate() { 
                        while (run)
                        { 
                            ControllerWork.Update();
                            KeyboardWork.Update();
                            Thread.Sleep(1000 / 60);
                        } 
                    }
                );
            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            //run the loop here
            while ( !this.IsDisposed )
            {

                if (ControlState.GetSystemEnabled()) _icon.Icon = XMouse.Properties.Resources.trayIcon_sm_enabled;
                else _icon.Icon = XMouse.Properties.Resources.trayIcon_sm_disabled;

                //allow the form app to call it events
                Application.DoEvents();

                Thread.Sleep(200);
            }
            
            //stop the thread
            run = false;
        }

        //when the application is being destroyed
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _icon.Visible = false;
                _icon.Dispose();
            }
        }
    }
}
