namespace XMouse
{
    partial class Notification
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
            this.notificationText = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Label();
            this.controllerDot3 = new System.Windows.Forms.Label();
            this.controllerDot2 = new System.Windows.Forms.Label();
            this.controllerDot1 = new System.Windows.Forms.Label();
            this.controllerDot0 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // notificationText
            // 
            this.notificationText.AutoSize = true;
            this.notificationText.Location = new System.Drawing.Point(13, 13);
            this.notificationText.Name = "notificationText";
            this.notificationText.Size = new System.Drawing.Size(116, 13);
            this.notificationText.TabIndex = 0;
            this.notificationText.Text = "TEST NOTIFICATION!";
            // 
            // closeButton
            // 
            this.closeButton.AutoSize = true;
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.Location = new System.Drawing.Point(190, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(10, 17);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "x";
            this.closeButton.UseCompatibleTextRendering = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // controllerDot3
            // 
            this.controllerDot3.AutoSize = true;
            this.controllerDot3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controllerDot3.Location = new System.Drawing.Point(186, 25);
            this.controllerDot3.Name = "controllerDot3";
            this.controllerDot3.Size = new System.Drawing.Size(16, 22);
            this.controllerDot3.TabIndex = 2;
            this.controllerDot3.Text = "•";
            // 
            // controllerDot2
            // 
            this.controllerDot2.AutoSize = true;
            this.controllerDot2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controllerDot2.Location = new System.Drawing.Point(174, 25);
            this.controllerDot2.Name = "controllerDot2";
            this.controllerDot2.Size = new System.Drawing.Size(16, 22);
            this.controllerDot2.TabIndex = 3;
            this.controllerDot2.Text = "•";
            // 
            // controllerDot1
            // 
            this.controllerDot1.AutoSize = true;
            this.controllerDot1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controllerDot1.Location = new System.Drawing.Point(162, 25);
            this.controllerDot1.Name = "controllerDot1";
            this.controllerDot1.Size = new System.Drawing.Size(16, 22);
            this.controllerDot1.TabIndex = 4;
            this.controllerDot1.Text = "•";
            // 
            // controllerDot0
            // 
            this.controllerDot0.AutoSize = true;
            this.controllerDot0.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.controllerDot0.Location = new System.Drawing.Point(150, 25);
            this.controllerDot0.Name = "controllerDot0";
            this.controllerDot0.Size = new System.Drawing.Size(16, 22);
            this.controllerDot0.TabIndex = 5;
            this.controllerDot0.Text = "•";
            // 
            // Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 45);
            this.Controls.Add(this.controllerDot0);
            this.Controls.Add(this.controllerDot1);
            this.Controls.Add(this.controllerDot2);
            this.Controls.Add(this.controllerDot3);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.notificationText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notification";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Notification";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label notificationText;
        private System.Windows.Forms.Label closeButton;
        private System.Windows.Forms.Label controllerDot3;
        private System.Windows.Forms.Label controllerDot2;
        private System.Windows.Forms.Label controllerDot1;
        private System.Windows.Forms.Label controllerDot0;
    }
}