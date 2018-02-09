namespace XMouse
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.controller0CheckBox = new System.Windows.Forms.CheckBox();
            this.controller1CheckBox = new System.Windows.Forms.CheckBox();
            this.controller2CheckBox = new System.Windows.Forms.CheckBox();
            this.controller3CheckBox = new System.Windows.Forms.CheckBox();
            this.enableControlCheck = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enable Controllers";
            // 
            // controller0CheckBox
            // 
            this.controller0CheckBox.AutoSize = true;
            this.controller0CheckBox.Location = new System.Drawing.Point(13, 70);
            this.controller0CheckBox.Name = "controller0CheckBox";
            this.controller0CheckBox.Size = new System.Drawing.Size(79, 17);
            this.controller0CheckBox.TabIndex = 1;
            this.controller0CheckBox.Text = "Controller 1";
            this.controller0CheckBox.UseVisualStyleBackColor = true;
            // 
            // controller1CheckBox
            // 
            this.controller1CheckBox.AutoSize = true;
            this.controller1CheckBox.Location = new System.Drawing.Point(98, 70);
            this.controller1CheckBox.Name = "controller1CheckBox";
            this.controller1CheckBox.Size = new System.Drawing.Size(79, 17);
            this.controller1CheckBox.TabIndex = 2;
            this.controller1CheckBox.Text = "Controller 2";
            this.controller1CheckBox.UseVisualStyleBackColor = true;
            // 
            // controller2CheckBox
            // 
            this.controller2CheckBox.AutoSize = true;
            this.controller2CheckBox.Location = new System.Drawing.Point(13, 93);
            this.controller2CheckBox.Name = "controller2CheckBox";
            this.controller2CheckBox.Size = new System.Drawing.Size(79, 17);
            this.controller2CheckBox.TabIndex = 3;
            this.controller2CheckBox.Text = "Controller 3";
            this.controller2CheckBox.UseVisualStyleBackColor = true;
            // 
            // controller3CheckBox
            // 
            this.controller3CheckBox.AutoSize = true;
            this.controller3CheckBox.Location = new System.Drawing.Point(98, 93);
            this.controller3CheckBox.Name = "controller3CheckBox";
            this.controller3CheckBox.Size = new System.Drawing.Size(79, 17);
            this.controller3CheckBox.TabIndex = 4;
            this.controller3CheckBox.Text = "Controller 4";
            this.controller3CheckBox.UseVisualStyleBackColor = true;
            // 
            // enableControlCheck
            // 
            this.enableControlCheck.AutoSize = true;
            this.enableControlCheck.Location = new System.Drawing.Point(13, 12);
            this.enableControlCheck.Name = "enableControlCheck";
            this.enableControlCheck.Size = new System.Drawing.Size(95, 17);
            this.enableControlCheck.TabIndex = 5;
            this.enableControlCheck.Text = "Enable Control";
            this.enableControlCheck.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(11, 116);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(81, 23);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(98, 116);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(79, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 151);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.enableControlCheck);
            this.Controls.Add(this.controller3CheckBox);
            this.Controls.Add(this.controller2CheckBox);
            this.Controls.Add(this.controller1CheckBox);
            this.Controls.Add(this.controller0CheckBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox controller0CheckBox;
        private System.Windows.Forms.CheckBox controller1CheckBox;
        private System.Windows.Forms.CheckBox controller2CheckBox;
        private System.Windows.Forms.CheckBox controller3CheckBox;
        private System.Windows.Forms.CheckBox enableControlCheck;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;
    }
}