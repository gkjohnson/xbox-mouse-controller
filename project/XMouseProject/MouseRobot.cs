using System;
using System.Runtime.InteropServices;
using System.Windows.Forms; //used to get mouse pos
using System.Windows;

//namespace the defines a robot class to control user input
namespace InputRobot {

    //this class is used to simulate mouse movements and clicks
    class MouseRobot {

        //basic program needed to drive input
        [DllImport("user32.dll", SetLastError = true)]
        static extern int SendInput(int nInputs, ref INPUT pInputs, int cbSize);

        //sets the cursor's position
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        static bool[] _buttonStates = new bool[3];

        //a structure that holds any kind of information about clicking the mouse
        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT {
            //mouse position
            int dx; 
            int dy;
 
            public int mouseData;
            public int dwFlags;
            int time;
            IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point {
            public Int32 X;
            public Int32 Y;
        };

        //fire a kind of input
        struct INPUT {
            public uint dwType; // the type of input?
            public MOUSEINPUT mi;
        }

        //presses the given button
        public static void PressButton(int button) {
            ButtonAction(button, true);
        }

        //releases the given button
        public static void ReleaseButton(int button) {
            ButtonAction(button, false);
        }

        //scrolls the given amount
        public static void Scroll(int amount) {
            
            //create a new input event
            var input = new INPUT()
            {
                dwType = 0,
                //with scroll data
                mi = new MOUSEINPUT()
                {
                    dwFlags = 0x0800, //MOUSEEVENTF_WHEEL mask
                    mouseData = amount
                }
            };
        
            //fire the event
            if(SendInput( 1, ref input, Marshal.SizeOf( input ) ) == 0 )
            {
                throw new Exception();
            }
        }

        static void ButtonAction(int button, bool press) {
   
            //if the button id is too high or the button is already in the passed state
            if (_buttonStates.Length <= button || _buttonStates[button] == press) return;

            //calculate the offset for the button's action state
            int shift = 2;
            if (press) shift--;

            //calculate the id
            int val = 1 << ((2 * button) + shift);

            //click the mouse
            DoClickMouse(val);

            //store what the state of the button is.
            _buttonStates[button] = press;
        }

        //fires a mouse click
        public static void DoClickMouse(int mouseButton) {
            var input = new INPUT()
            {
                dwType = 0, // Mouse input
                mi = new MOUSEINPUT() { dwFlags = mouseButton }
            };

            if (SendInput(1, ref input, Marshal.SizeOf(input)) == 0)
            {
                throw new Exception();
            }
        }

        //moves the mouse absolutely to a screen position
        public static void SetMousePos(int x, int y) {
            SetCursorPos(x, y);
        }

        public static Point GetMousePos() {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }


        //moves the mouse relative to its current position
        public static void MoveMouse(int xDelta, int yDelta) {
            SetCursorPos(Cursor.Position.X + xDelta, Cursor.Position.Y + yDelta);
        }
    }
}
