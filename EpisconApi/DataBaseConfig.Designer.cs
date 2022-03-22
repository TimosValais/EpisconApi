namespace EpisconApi
{
    partial class DataBaseConfig
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbDatabaseConnString = new System.Windows.Forms.TextBox();
            this.tbDatabaseUsername = new System.Windows.Forms.TextBox();
            this.tbDatabasePass = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.lblDatabaseUserName = new System.Windows.Forms.Label();
            this.lblDatabasePassword = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbDatabaseConnString
            // 
            this.tbDatabaseConnString.Location = new System.Drawing.Point(274, 149);
            this.tbDatabaseConnString.Name = "tbDatabaseConnString";
            this.tbDatabaseConnString.Size = new System.Drawing.Size(238, 23);
            this.tbDatabaseConnString.TabIndex = 0;
            // 
            // tbDatabaseUsername
            // 
            this.tbDatabaseUsername.Location = new System.Drawing.Point(274, 198);
            this.tbDatabaseUsername.Name = "tbDatabaseUsername";
            this.tbDatabaseUsername.Size = new System.Drawing.Size(238, 23);
            this.tbDatabaseUsername.TabIndex = 1;
            // 
            // tbDatabasePass
            // 
            this.tbDatabasePass.Location = new System.Drawing.Point(274, 253);
            this.tbDatabasePass.Name = "tbDatabasePass";
            this.tbDatabasePass.Size = new System.Drawing.Size(238, 23);
            this.tbDatabasePass.TabIndex = 2;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(316, 130);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(154, 15);
            this.lblConnectionString.TabIndex = 3;
            this.lblConnectionString.Text = "Database Connection String";
            // 
            // lblDatabaseUserName
            // 
            this.lblDatabaseUserName.AutoSize = true;
            this.lblDatabaseUserName.Location = new System.Drawing.Point(351, 180);
            this.lblDatabaseUserName.Name = "lblDatabaseUserName";
            this.lblDatabaseUserName.Size = new System.Drawing.Size(65, 15);
            this.lblDatabaseUserName.TabIndex = 4;
            this.lblDatabaseUserName.Text = "User Name";
            // 
            // lblDatabasePassword
            // 
            this.lblDatabasePassword.AutoSize = true;
            this.lblDatabasePassword.Location = new System.Drawing.Point(355, 237);
            this.lblDatabasePassword.Name = "lblDatabasePassword";
            this.lblDatabasePassword.Size = new System.Drawing.Size(57, 15);
            this.lblDatabasePassword.TabIndex = 5;
            this.lblDatabasePassword.Text = "Password";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(341, 323);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // DataBaseConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblDatabasePassword);
            this.Controls.Add(this.lblDatabaseUserName);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.tbDatabasePass);
            this.Controls.Add(this.tbDatabaseUsername);
            this.Controls.Add(this.tbDatabaseConnString);
            this.Name = "DataBaseConfig";
            this.RightToLeftLayout = true;
            this.Text = "DataBase Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbDatabaseConnString;
        private TextBox tbDatabaseUsername;
        private TextBox tbDatabasePass;
        private Label lblConnectionString;
        private Label lblDatabaseUserName;
        private Label lblDatabasePassword;
        private Button btnConnect;
    }
}