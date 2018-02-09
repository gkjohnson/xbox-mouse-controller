using System;
using System.Windows;
using System.Windows.Threading;
namespace XMouse {
    
    class KeyboardWork {
        // Do Events Faking //
        public static void DoEvents() {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        public static object ExitFrame(object f) {
            ((DispatcherFrame)f).Continue = false;

            return null;
        }

        public const string CHARACTERS =        "abcdefghijklmnopqrstuvwxyz0123456789";
        public const string ALT_CHARACTERS =    "ABCDEFGHIJKLMNOPQRSTUVWXYZ!?|.,-+='\"";

        public const int DIAL_AMT = 6;
        public const int KEY_AMT = 6;

        static KeyPad _dial = null;

        public const float LERP_AMT = 0.6f;

        static float _pos = 0.0f;

        public static void Update() {

            // Logic for creating a new keypad when opening the dial, and closing the keypad window when minimizing it
            // This is an alternative because for some reason, after having the dial closed for a long time, it won't open back up

            // If we're typing
            if (ControlState.isTyping) {

                // Create a new keypad if we don't have one
                if (_dial == null || !_dial.IsVisible) {
                    if (_dial != null) {
                        _dial.Close();
                        _dial = null;
                    }
                    _dial = new KeyPad();
                    UpdateCharacters(false);
                }

                // Position it around the mouse
                double buffer = _dial.Width / 2.5;

                // position the window
                Point mousePos = InputRobot.MouseRobot.GetMousePos();
                mousePos.X = Math.Min(SystemParameters.PrimaryScreenWidth - buffer, Math.Max(mousePos.X, buffer));
                mousePos.Y = Math.Min(SystemParameters.PrimaryScreenHeight - buffer, Math.Max(mousePos.Y, buffer));
                _dial.Left = mousePos.X - _dial.Width / 2.0;
                _dial.Top = mousePos.Y - _dial.Height / 2.0;

                // Set the stick position
                _dial.SetStickPositions(ControllerWork._leftStick, ControllerWork._rightStick);

                // Animate it open
                _pos = _pos + (1.0f - _pos) * LERP_AMT;
                if (_pos > .99f) _pos = 1.0f;

                _dial.SetSize(_pos);
            
            } else {
                // If we're not typing
                if (_dial != null) {
                    // Animate it closed
                    _pos = _pos + (0.0f - _pos) * LERP_AMT;
                    _dial.SetSize(_pos);

                    // If it gets really close to zero
                    if (_pos <= 0.01f) {
                        // close the window
                        _dial.Close();
                        _dial = null;
                    }
                }
            }


            if (_dial != null) {
                DoEvents();
            }
            /*
            // reopen the dial window if it is closed for some reason
            if (_dial == null || !_dial.IsVisible)
            {
                if (_dial != null)
                {
                    _dial.Close();
                    _dial = null;
                }
                _dial = new KeyPad();
                UpdateCharacters(false);
            }

            double buffer = _dial.Width / 2.5;

            // position the window
            Point mousePos = InputRobot.MouseRobot.GetMousePos();
            mousePos.X = Math.Min(SystemParameters.PrimaryScreenWidth - buffer, Math.Max(mousePos.X, buffer));
            mousePos.Y = Math.Min(SystemParameters.PrimaryScreenHeight - buffer, Math.Max(mousePos.Y, buffer));
            _dial.Left = mousePos.X - _dial.Width/2.0;
            _dial.Top = mousePos.Y - _dial.Height / 2.0;


            // Animate opening the window
            float _targetState = 0.0f;
            if (ControlState.isTyping) _targetState = 1.0f;
            _pos = _pos + (_targetState - _pos) * LERP_AMT;

            // Round the value if it's unnoticeably close to 1.0
            if (_pos > .99f) _pos = 1.0f;
            
            // apply the stick positions, then the opening animation
            _dial.SetStickPositions(ControllerWork._leftStick, ControllerWork._rightStick);
            _dial.SetSize(_pos);
            */
            // Fire the standard windo events
               
        }

        // updates the labels of the keys
        public static void UpdateCharacters(bool useAlt) {
            if (_dial != null) {
                _dial.SetCharacters((useAlt) ? ALT_CHARACTERS : CHARACTERS);
            }
        }

        public static void firstButtonAction(int button, bool pressed) {
            if (_dial != null) {
                _dial.SetButtonHighlight(button, pressed);
            }
        }
    }
}
