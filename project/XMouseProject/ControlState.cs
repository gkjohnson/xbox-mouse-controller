namespace XMouse {
    class ControlState {
        static bool _enableControl;
        static bool[] _controllerEnabled = new bool[4];

        public static bool isTyping {
            get { return _isTyping; }
            set  {
                _isTyping = value;
                if (_isTyping && _useAltCharacters) {
                    ToggleAltCharacters();
                } 
            }
        }
        static bool _isTyping = false;
        static bool _useAltCharacters = false;

        public const int X_BUTTON = 2;
        public const int A_BUTTON = 1;
        public const int B_BUTTON = 0;
        public const int Y_BUTTON = 3;

        //returns whether the system is enabled or not
        public static bool GetSystemEnabled() {
            return _enableControl;
        }

        //sets the system enabled state
        public static void SetSystemEnabled( bool enabled ) {

            string s = "Enabled controller to mouse input";
            if (!enabled) s = "Disabled controller to mouse input";

            isTyping = false;

            Notification.ShowNotification(s);
            _enableControl = enabled;
        }

        //sets the enabled state
        public static bool ToggleSystemEnabled() {
            SetSystemEnabled(!_enableControl);
            return _enableControl;
        }

        //sets the enabled state of the 
        public static void SetControllerEnabled( int controller, bool enabled ) {
            if (controller >= _controllerEnabled.Length) return;
            _controllerEnabled[controller] = enabled;
        }

        //returns whether or not the given controller is enabled
        public static bool GetControllerEnabled(int controller) {
            if (controller >= _controllerEnabled.Length) return false;
            return _controllerEnabled[controller];
        }

        // Toggles the ability to use alternate characters only when typing
        public static void ToggleAltCharacters() {
            if (_isTyping) {
                _useAltCharacters = !_useAltCharacters;
                KeyboardWork.UpdateCharacters(_useAltCharacters);
            }
        }

    }
}
