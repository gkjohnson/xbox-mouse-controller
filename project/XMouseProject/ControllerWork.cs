using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using InputRobot;

namespace XMouse {
    class ControllerWork {
        static GamePadState[] _prevGamePadStates = new GamePadState[4];
        public static Microsoft.Xna.Framework.Vector2 _leftStick = new Microsoft.Xna.Framework.Vector2();
        public static Microsoft.Xna.Framework.Vector2 _rightStick = new Microsoft.Xna.Framework.Vector2();


        //do the work needed to run the controller and mouse
        public static void Update() {
            //if (KeyboardRobot.GetKey(27)) Application.Exit(); // quit the application if escape is pressed

            // Reset stick positions
            _rightStick.X = _leftStick.X = _rightStick.Y = _leftStick.Y = 0;

            // move mouse
            Vector2 mouseMovement = Vector2.Zero;
            Vector2 scrollAmount = Vector2.Zero;

            // store the button states -1 is pressed, 1 is up, 0 is nochange
            float rTrigger = 0.0f;
            float lTrigger = 0.0f;

            // go through every controller to check inputs
            for (int i = 0; i < 4; i++) {
                GamePadState gps = GamePad.GetState((PlayerIndex)i, GamePadDeadZone.Circular);
                GamePadState pgps = _prevGamePadStates[i];


                // check if enable key combo is enabled
                if (IsEnableToggleComboPressed(gps) && !IsEnableToggleComboPressed(pgps)) {
                    ControlState.ToggleSystemEnabled();

                    // store they previous state
                    _prevGamePadStates[i] = gps;
                    break;
                }

                //check if the quit key combo is enabled
                if (IsCtrlQComboPressed(gps) && !IsCtrlQComboPressed(pgps)) {
                    KeyboardRobot.PressKey(0xA2);
                    KeyboardRobot.PressKey('q');
                    KeyboardRobot.ReleaseKey('q');
                    KeyboardRobot.ReleaseKey(0xA2);
                }

                // don't do anything if the player isn't enabled or if the entire system isn't enabled
                if (!ControlState.GetControllerEnabled(i) || !ControlState.GetSystemEnabled()) {
                    _prevGamePadStates[i] = gps;
                    continue;
                }

                // save stick states
                _rightStick += gps.ThumbSticks.Right;
                _leftStick += gps.ThumbSticks.Left; 

                // store stick amount for mouse movement
                mouseMovement += gps.ThumbSticks.Left;
                scrollAmount += gps.ThumbSticks.Right;

                // store the largest trigger value
                if (rTrigger < gps.Triggers.Right) rTrigger = gps.Triggers.Right;
                if (lTrigger < gps.Triggers.Left) lTrigger = gps.Triggers.Left;


                // If we're in mouse control
                if (!ControlState.isTyping) {
                    // MOUSE CLICKS //
                    // left click when a pressed
                    if (KeyPressed(gps, pgps, Buttons.A)) MouseRobot.PressButton(0);
                    else if (KeyReleased(gps, pgps, Buttons.A)) MouseRobot.ReleaseButton(0);

                    // right click when b is pressed
                    if (KeyPressed(gps, pgps, Buttons.B)) MouseRobot.PressButton(1);
                    else if (KeyReleased(gps, pgps, Buttons.B)) MouseRobot.ReleaseButton(1);

                    // middle click when y is pressed
                    if (KeyPressed(gps, pgps, Buttons.Y)) MouseRobot.PressButton(2);
                    else if (KeyReleased(gps, pgps, Buttons.Y)) MouseRobot.ReleaseButton(2);

                    // ENTER KEY //
                    // press enter key when start is pressed
                    if (KeyPressed(gps, pgps, Buttons.Start)) KeyboardRobot.PressKey(13);
                    else if (KeyReleased(gps, pgps, Buttons.Start)) KeyboardRobot.ReleaseKey(13);
                } else {
                    // Toggle they alt character set
                    if (KeyPressed(gps, pgps, Buttons.LeftShoulder)) ControlState.ToggleAltCharacters();

                    if (KeyPressed(gps, pgps, Buttons.Y)) {
                        ControlState.ToggleAltCharacters();
                        KeyboardWork.firstButtonAction(ControlState.Y_BUTTON, true);
                    }
                    else if (KeyReleased(gps, pgps, Buttons.Y)) {
                        KeyboardWork.firstButtonAction(ControlState.Y_BUTTON, false);
                    }
                    
                    // backspace when X is pressed
                    if (KeyPressed(gps, pgps, Buttons.X)) {
                        KeyboardRobot.PressKey(KeyboardRobot.BACKSPACE_KEY);
                        KeyboardWork.firstButtonAction(ControlState.X_BUTTON, true);
                    }
                    else if (KeyReleased(gps, pgps, Buttons.X)) {
                        KeyboardRobot.ReleaseKey(KeyboardRobot.BACKSPACE_KEY);
                        KeyboardWork.firstButtonAction(ControlState.X_BUTTON, false);
                    }

                    // space when A is pressed
                    if (KeyPressed(gps, pgps, Buttons.A)) {
                        KeyboardRobot.PressKey(' ');
                        KeyboardWork.firstButtonAction(ControlState.A_BUTTON, true);
                    } else if (KeyReleased(gps, pgps, Buttons.A)) {
                        KeyboardRobot.ReleaseKey(' ');
                        KeyboardWork.firstButtonAction(ControlState.A_BUTTON, false);
                    }

                    // enter when B is pressed
                    if (KeyPressed(gps, pgps, Buttons.B)) {
                        KeyboardRobot.PressKey(KeyboardRobot.ENTER_KEY);
                        KeyboardWork.firstButtonAction(ControlState.B_BUTTON, true);
                    } else if (KeyReleased(gps, pgps, Buttons.B)) {
                        KeyboardRobot.ReleaseKey(KeyboardRobot.ENTER_KEY);
                        KeyboardWork.firstButtonAction(ControlState.B_BUTTON, false);
                    }
                }

                // For both key states
                // TOGGLE TYPING STATE //
                if (KeyPressed(gps, pgps, Buttons.LeftStick)) {
                    ControlState.isTyping = !ControlState.isTyping;
                }
                // ARROW KEYS //
                // Left Arrow when left DPad
                if (KeyPressed(gps, pgps, Buttons.DPadLeft)) KeyboardRobot.PressKey(37);
                else if (KeyReleased(gps, pgps, Buttons.DPadLeft)) KeyboardRobot.ReleaseKey(37);
                // Right Arrow when right dpad
                if (KeyPressed(gps, pgps, Buttons.DPadRight)) KeyboardRobot.PressKey(39);
                else if (KeyReleased(gps, pgps, Buttons.DPadRight)) KeyboardRobot.ReleaseKey(39);
                // Up Arrow
                if (KeyPressed(gps, pgps, Buttons.DPadUp)) KeyboardRobot.PressKey(38);
                else if (KeyReleased(gps, pgps, Buttons.DPadUp)) KeyboardRobot.ReleaseKey(38);
                // Down Arrow
                if (KeyPressed(gps, pgps, Buttons.DPadDown)) KeyboardRobot.PressKey(40);
                else if (KeyReleased(gps, pgps, Buttons.DPadDown)) KeyboardRobot.ReleaseKey(40);
                
                // store they previous state
                _prevGamePadStates[i] = gps;
            }

            // don't do anything if the system is not enabled
            if (!ControlState.GetSystemEnabled()) return;
            if (ControlState.isTyping) return;

            // super speed if right trigger is pressed
            mouseMovement *= 30;
            mouseMovement -= rTrigger * mouseMovement * .7f;
            mouseMovement -= lTrigger * mouseMovement * .7f;

            scrollAmount *= 15;
            MouseRobot.Scroll((int)scrollAmount.Y);

            // moves the mouse
            MouseRobot.MoveMouse((int)mouseMovement.X, -(int)mouseMovement.Y);

        }

        // returns whether the key was just released or not
        static bool KeyReleased(GamePadState gps, GamePadState prevgps, Buttons button) {
            if (!gps.IsButtonDown(button) && prevgps.IsButtonDown(button)) return true;

            return false;
        }

        // returns whether the key was just pressed or not
        static bool KeyPressed(GamePadState gps, GamePadState prevgps, Buttons button) {
            if (gps.IsButtonDown(button) && !prevgps.IsButtonDown(button)) return true;

            return false;
        }

        // checks to see if the special key combo to enable and disable the input manipulation is enabled or not
        // both triggers, both shoulders, and both sticks
        static bool IsEnableToggleComboPressed(GamePadState gps) {
            return
                gps.IsButtonDown(Buttons.LeftTrigger) &&
                gps.IsButtonDown(Buttons.RightTrigger) &&
                gps.IsButtonDown(Buttons.LeftShoulder) &&
                gps.IsButtonDown(Buttons.RightShoulder) &&
                gps.IsButtonDown(Buttons.LeftStick) &&
                gps.IsButtonDown(Buttons.RightStick);
        }

        // checks if the special key combo to fire "CTRL + Q" is pressed
        static bool IsCtrlQComboPressed(GamePadState gps) {
            return gps.IsButtonDown(Buttons.LeftTrigger) &&
                gps.IsButtonDown(Buttons.RightTrigger) &&
                gps.IsButtonDown(Buttons.LeftShoulder) &&
                gps.IsButtonDown(Buttons.RightShoulder) &&
                gps.IsButtonDown(Buttons.Start) &&
                gps.IsButtonDown(Buttons.Back);
        }

        // returns whether or not a controllers is connected
        public static bool IsControllerConnected(int controller) {
            if (controller < 0 || controller > 3) return false;

            return GamePad.GetState((PlayerIndex)controller).IsConnected;
        }
    }
}
