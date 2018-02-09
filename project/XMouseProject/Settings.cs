using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMouse
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            enableControlCheck.Checked = ControlState.GetSystemEnabled();

            controller0CheckBox.Checked = ControlState.GetControllerEnabled(0);
            controller1CheckBox.Checked = ControlState.GetControllerEnabled(1);
            controller2CheckBox.Checked = ControlState.GetControllerEnabled(2);
            controller3CheckBox.Checked = ControlState.GetControllerEnabled(3);
        }

        //when the save button is clicked
        private void saveButton_Click(object sender, EventArgs e)
        {
            ControlState.SetSystemEnabled(enableControlCheck.Checked);

            ControlState.SetControllerEnabled(0, controller0CheckBox.Checked);
            ControlState.SetControllerEnabled(1, controller1CheckBox.Checked);
            ControlState.SetControllerEnabled(2, controller2CheckBox.Checked);
            ControlState.SetControllerEnabled(3, controller3CheckBox.Checked);

            Close();
        }

        //when the cancel button is clicked
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
