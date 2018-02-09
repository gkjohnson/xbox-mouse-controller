using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace XMouse
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class KeyPad : Window
    {
        public const double MAX_WIDTH = 550;
        public const double MAX_HEIGHT = 550;

        public const double MAX_LABEL_WIDTH = 30;
        public const double MAX_LABEL_HEIGHT = 30;

        public const double ELLIPSE_SIZE = 110;

        public const double LABEL_DIST = 70;
        public const double ELLIPSE_DIST = 300;

        public const double MAX_FONT_SIZE = 20;

        public const double BUTTON_SIZE = 40;
        public const double BUTTON_DIST = 100;

        double angleOffset = ((2.0*Math.PI)/360.0) * 90.0;

        // Store all the labels, etc for the window
        List<Label> _keyLabels = new List<Label>();
        List<Ellipse> _ellipses = new List<Ellipse>();
        List<Ellipse> _xButtons = new List<Ellipse>();
        List<Image> _imgButtons = new List<Image>();

        // The ellipse used to display the left stick position
        Ellipse _leftStickEllipse;

        // store waht the last size sent was
        double _prevPerc = -1.0;

        // previously selected dial positions
        int _prevDialIndex = -1;
        int _prevLabelIndex = -1;
        double _prevRStickMag = 0.0;

        // Constructor. Create all the parts
        public KeyPad()
        {
            InitializeComponent();

            this.Width = MAX_WIDTH;
            this.Height = MAX_HEIGHT;


            // Add the grid
            grid.Width = this.Width;// this.Width;
            grid.Height = this.Height;// this.Height;



 

            _leftStickEllipse = GetNewEllipse();
            _leftStickEllipse.Width = 10;
            _leftStickEllipse.Height = 10;
            grid.Children.Add(_leftStickEllipse);



            for (int i = 0; i < KeyboardWork.DIAL_AMT; i++)
            {
                Ellipse e = GetNewEllipse();
                grid.Children.Add(e);
                _ellipses.Add(e);

                for (int j = 0; j < KeyboardWork.KEY_AMT; j++)
                {
                    Label l = GetNewLabel();
                    grid.Children.Add(l);
                    _keyLabels.Add(l);
                }
            }





            _imgButtons.Add(bButImage);
            _imgButtons.Add(aButImage);
            _imgButtons.Add(xButImage);
            _imgButtons.Add(yButImage);

            // Add ellipses for xbox bottons
            for (int i = 0; i < 4; i++)
            {
                _xButtons.Add(GetNewEllipse());
                _xButtons[i].Width = BUTTON_SIZE;
                _xButtons[i].Height = BUTTON_SIZE;

                _xButtons[i].StrokeThickness = 0;

                grid.Children.Add(_xButtons[i]);


                _imgButtons[i].Width = 100;
                _imgButtons[i].Height = 100;
                


                grid.Children.Remove(_imgButtons[i]);
                grid.Children.Add(_imgButtons[i]);

        
            }

            //buttonLabelsImage.Source = XMouse.Properties.Resources.typekeys.s

            // TODO : Draw icons for buttons : A - Space, X - Backspace, Y - Alt Toggle, B - Enter

            // use this to convert from XMouse.Properties.Resources.image to an imagesource to be used in an image
            
            // return (ImageSource)c.ConvertFrom(yourBitmap);

            SetSize(0);
            this.Show();
        }

        

        // Sets the animation point for opening the keypad
        public void SetSize(double perc)
        {
            
            // don't do anything if we've already reached the final animation state
            if (perc == _prevPerc) return;

            // loop through each ellipse
            for (int i = 0; i < KeyboardWork.DIAL_AMT; i++)
            {
                SetDialSize(i, perc);
            }


            for (int i = 0; i < _xButtons.Count; i++)
            {
                const double quarter = Math.PI/2.0;

                double x = Math.Cos(quarter * i) * perc * BUTTON_DIST;
                double y = Math.Sin(quarter * i) * perc * BUTTON_DIST;

                _xButtons[i].Margin = new Thickness(x, y, 0, 0);
                _xButtons[i].Width = BUTTON_SIZE * perc;
                _xButtons[i].Height = BUTTON_SIZE * perc;

                _imgButtons[i].Width = BUTTON_SIZE * perc * .75;
                _imgButtons[i].Height = BUTTON_SIZE * perc * .75;

                _imgButtons[i].Margin = new Thickness(x/2.0 + this.Width / 2.0 - _imgButtons[i].Width / 2.0, y/2.0 + this.Height / 2.0 - _imgButtons[i].Height / 2.0, 0, 0);


                SetButtonHighlight(i, false);
            }

            // opacity
            this.Opacity = perc;

            _prevPerc = perc;
        }

        // sets the size of the specific dial
        void SetDialSize(int i, double perc, int keyIndex = -1, double labelPerc = 1.0)
        {
            int index = 0;
            double eX, eY, lX, lY;

            // positioning
            eX = Math.Cos(((float)i / KeyboardWork.KEY_AMT) * 2 * Math.PI - angleOffset) * ELLIPSE_DIST * perc;
            eY = Math.Sin(((float)i / KeyboardWork.KEY_AMT) * 2 * Math.PI - angleOffset) * ELLIPSE_DIST * perc;

            _ellipses[i].Margin = new Thickness(eX, eY, 0, 0);

            // size
            _ellipses[i].Width = ELLIPSE_SIZE * perc;
            _ellipses[i].Height = ELLIPSE_SIZE * perc;

            // loop throug each key
            for (int j = 0; j < KeyboardWork.KEY_AMT; j++)
            {
                index = i * KeyboardWork.DIAL_AMT + j;

                var usePerc = (keyIndex == j) ? labelPerc : perc;

                // positioning
                lX = eX + Math.Cos(((float)j / KeyboardWork.KEY_AMT) * 2 * Math.PI - angleOffset) * LABEL_DIST * usePerc;
                lY = eY + Math.Sin(((float)j / KeyboardWork.KEY_AMT) * 2 * Math.PI - angleOffset) * LABEL_DIST * usePerc;

                _keyLabels[index].Margin = new Thickness(lX, lY, 0, 0);

                // size
                _keyLabels[index].FontSize = Math.Max(0.1, MAX_FONT_SIZE * usePerc);
                _keyLabels[index].Width = MAX_LABEL_WIDTH * usePerc;
                _keyLabels[index].Height = MAX_LABEL_HEIGHT * usePerc;
            }
        }

        // Passes in the controller stick positions
        public void SetStickPositions(Microsoft.Xna.Framework.Vector2 leftStick, Microsoft.Xna.Framework.Vector2 rightStick)
        {
            // move the center dot
            _leftStickEllipse.Margin = new Thickness(leftStick.X * 100, leftStick.Y * -100, 0, 0);

            int dialIndex, labelIndex;
            dialIndex = labelIndex = -1;

            // if the user hasn't pushed the left stick very far, don't do anything
            if (leftStick.Length() > .8)
            {
                // Calculate the angle for the stick, then the index of the corresponding dial
                double lAngle = Math.Atan2(-leftStick.Y, leftStick.X) + angleOffset + Math.PI * 2 + Math.PI / KeyboardWork.DIAL_AMT;
                dialIndex = (int)(lAngle / (2 * Math.PI / KeyboardWork.DIAL_AMT));
                dialIndex %= KeyboardWork.DIAL_AMT;

                // if the user hasn't pushed the right stick very far, don't push a letter
                if (rightStick.Length() > .8)
                {
                    // Calculate the angle for the stick, then the corresponding label
                    double rAngle = Math.Atan2(-rightStick.Y, rightStick.X) + angleOffset + Math.PI * 2 + Math.PI / KeyboardWork.KEY_AMT;
                    labelIndex = (int)(rAngle / (2 * Math.PI / KeyboardWork.KEY_AMT));
                    labelIndex %= KeyboardWork.KEY_AMT;

                    if (_prevRStickMag < .8)
                    {
                        char key = (_keyLabels[labelIndex + KeyboardWork.DIAL_AMT * dialIndex].Content as String)[0];

                        InputRobot.KeyboardRobot.TypeCharacter(key);
                    }
                }
            }

            // Reset the previously selected dial if it has changed
            if (_prevDialIndex != dialIndex || _prevLabelIndex != labelIndex)
            {
                if (_prevDialIndex != -1)
                {
                    SetDialSize(_prevDialIndex, 1.0, _prevLabelIndex, 1.0);
                }

                // Apply the dial size if we're pointing at a dial
                if (dialIndex != -1)
                {
                    SetDialSize(dialIndex, 1.1, labelIndex, 1.3);
                }

                // store the new dial data
                _prevDialIndex = dialIndex;
                _prevLabelIndex = labelIndex;
            }

            // store the previous length of the value
            _prevRStickMag = rightStick.Length();
        }

        // Sets the characters to use for the labels
        public void SetCharacters(string s)
        {
            for (int i = 0; i < _keyLabels.Count && i < s.Length; i++)
            {
                _keyLabels[i].Content = s[i].ToString();
            }
        }

        // Gets a new prepared Ellipse object
        Ellipse GetNewEllipse()
        {
            Ellipse e = new Ellipse();
            e.Width = ELLIPSE_SIZE;
            e.Height = ELLIPSE_SIZE;
            e.Stroke = Brushes.Transparent;

            // This can be moved elsewhere so it's not created over and over
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#222222");
            brush.Freeze();
            e.Fill = brush;


            brush = (Brush)bc.ConvertFrom("#333333");
            brush.Freeze();
            e.Stroke = brush;
            e.StrokeThickness = 4.0;

            return e;
        }

        // Gets a new prepared Label object
        Label GetNewLabel()
        {
            Label l = new Label();
            l.Content = "A";
            l.Foreground = Brushes.White;
            l.FontSize = MAX_FONT_SIZE;

            l.Padding = new Thickness(0, 0, 0, 0);

            l.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            l.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            l.Width = MAX_LABEL_HEIGHT;
            l.Height = MAX_LABEL_WIDTH;

            return l;
        }


        string[] _buttonColor =     { "#884444", "#448844", "#444488", "#888844" };
        string[] _buttonHighlight = { "#dd4444", "#44dd44", "#4444dd", "#dddd44" };


        // Sets a button highlight state
        public void SetButtonHighlight(int num, bool highlight)
        {
            string color = _buttonColor[ num ];
            if( highlight ) color = _buttonHighlight[num];

            BrushConverter bc = new BrushConverter();
            Brush b = bc.ConvertFromString(color) as Brush;

            _xButtons[num].Fill = b;
        }
    }
}
