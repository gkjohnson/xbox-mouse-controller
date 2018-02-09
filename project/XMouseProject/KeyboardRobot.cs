using System;
using System.Runtime.InteropServices;

//namespace the defines a robot class to control user input
namespace InputRobot {
    class KeyboardRobot {
        
        //basic program needed to drive input
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        //returns the state of the keyboard key
        [DllImport("user32.dll")]
        private static extern short GetKeyState(int key);

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char c);


        // Keycodes for important keys
        public const short BACKSPACE_KEY = 0x08;
        public const short TAB_KEY = 0x09;
        public const short ENTER_KEY = 0x0D;
        public const short SHIFT_KEY = 0x10;
        public const short CTRL_KEY = 0x11;
        public const short ALT_KEY = 0x12;
        public const short ESC_KEY = 0x1B;

        public const short UP_KEY = 0x26;
        public const short DOWN_KEY = 0x28;
        public const short LEFT_KEY = 0x25;
        public const short RIGHT_KEY = 0x27;

        public const short WIN_KEY = 0x5B;



        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT {
            
            public ushort keyCode;  
            public ushort scan;     
            public uint flags;    
            public uint time;       
            IntPtr extraInfo;       

            //add padding here because the different inputs need t obe the same size
            //but mouseInput is 24 bytes, yet this one is 16, so 8 more bytes must be
            //added so they match
            int padding;    //extra 4 bytes
            int padding2;   //extra 4 bytes
        }

        //fire a kind of input
        struct INPUT {
            public uint dwType; // the type of input?
            public KEYBDINPUT ki;
        }
        
        //masks for the different kinds of keyboard input
        const uint KEYEVENTF_KEYUP = 0x0002;
        const uint KEYEVENTF_SCANCODE = 0x0008;
        const uint KEYEVENTF_UNICODE = 0x0004;

        //flag for if the key is down
        private const int KEY_DOWN_MASK = 0x8000;

        //returns the down state of the key
        public static bool GetKey(int keycode) {
            return (GetKeyState(keycode) & KEY_DOWN_MASK) != 0;
        }

        // Types the given character, holding the appropriate keys to type it
        public static void TypeCharacter(char c) {
            const short SHIFT_MASK = 0x0100;
            const short CTRL_MASK = 0x0200;
            const short ALT_MASK = 0x0400;

            short key = VkKeyScan(c);

            // These bits are used as flags for whether or not the given keys are being pressed
            bool useShift = (key & SHIFT_MASK) != 0;
            bool useAlt = (key & ALT_MASK) != 0;
            bool useCtrl = (key & CTRL_MASK) != 0;

            if (useShift) PressKey(SHIFT_KEY);
            if (useAlt) PressKey(ALT_KEY);
            if (useCtrl) PressKey(CTRL_KEY);

            PressKey(key);

            if (useShift) ReleaseKey(SHIFT_KEY);
            if (useAlt) ReleaseKey(ALT_KEY);
            if (useCtrl) ReleaseKey(CTRL_KEY);
        }
        // Presses and releases the given keyCode
        public static void TypeKey(short keycode) {
            PressKey(keycode);
            ReleaseKey(keycode);
        }

        //presses the given key
        public static void PressKey(char c){ KeyAction(VkKeyScan(c), true); }
        public static void PressKey(short keycode) { KeyAction(keycode, true); }

        //releases the given key
        public static void ReleaseKey(char c) { KeyAction(VkKeyScan(c), false); }
        public static void ReleaseKey(short keycode) { KeyAction(keycode, false); }

        //fires a mouse click
        public static void KeyAction(short keycode, bool pressed) {
            var input = new INPUT() {
                dwType = 1, // Keyboard input
                ki = new KEYBDINPUT()
                {
                    keyCode = (ushort)keycode,
                    scan = 0,
                    flags = pressed?0:KEYEVENTF_KEYUP,
                    time = 0
                }
            };

            INPUT[] inp = new INPUT[1];
            inp[0] = input;

            uint err = SendInput((uint)inp.Length, inp, Marshal.SizeOf(inp[0].GetType()));
            if (err == 0) {
                Console.WriteLine( Marshal.GetLastWin32Error() );
                //throw new Exception();
            }
        }
    }
}
