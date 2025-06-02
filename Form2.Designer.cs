
namespace webcamproject
{
    partial class password1
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
            this.getpass = new System.Windows.Forms.Label();
            this.HEADER = new System.Windows.Forms.Label();
            this.OKBTN = new System.Windows.Forms.Button();
            this.userinputtb = new System.Windows.Forms.TextBox();
            this.CANCELBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // getpass
            // 
            this.getpass.AutoSize = true;
            this.getpass.Font = new System.Drawing.Font("Modern No. 20", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getpass.Location = new System.Drawing.Point(21, 79);
            this.getpass.Name = "getpass";
            this.getpass.Size = new System.Drawing.Size(148, 15);
            this.getpass.TabIndex = 0;
            this.getpass.Text = "ENTER PASSWORD :";
            // 
            // HEADER
            // 
            this.HEADER.AutoSize = true;
            this.HEADER.BackColor = System.Drawing.Color.LightSeaGreen;
            this.HEADER.Font = new System.Drawing.Font("Modern No. 20", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HEADER.ForeColor = System.Drawing.Color.White;
            this.HEADER.Location = new System.Drawing.Point(45, 24);
            this.HEADER.Name = "HEADER";
            this.HEADER.Size = new System.Drawing.Size(255, 21);
            this.HEADER.TabIndex = 2;
            this.HEADER.Text = "PASSWORD VALIDATION";
            // 
            // OKBTN
            // 
            this.OKBTN.BackColor = System.Drawing.Color.LightSeaGreen;
            this.OKBTN.Location = new System.Drawing.Point(57, 126);
            this.OKBTN.Name = "OKBTN";
            this.OKBTN.Size = new System.Drawing.Size(94, 39);
            this.OKBTN.TabIndex = 3;
            this.OKBTN.Text = "OK";
            this.OKBTN.UseVisualStyleBackColor = false;
            this.OKBTN.Click += new System.EventHandler(this.OKBTN_Click);
            // 
            // userinputtb
            // 
            this.userinputtb.ForeColor = System.Drawing.Color.Black;
            this.userinputtb.Location = new System.Drawing.Point(178, 77);
            this.userinputtb.Name = "userinputtb";
            this.userinputtb.PasswordChar = '*';
            this.userinputtb.Size = new System.Drawing.Size(126, 20);
            this.userinputtb.TabIndex = 4;
            this.userinputtb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CANCELBTN
            // 
            this.CANCELBTN.BackColor = System.Drawing.Color.LightSeaGreen;
            this.CANCELBTN.Location = new System.Drawing.Point(178, 126);
            this.CANCELBTN.Name = "CANCELBTN";
            this.CANCELBTN.Size = new System.Drawing.Size(94, 39);
            this.CANCELBTN.TabIndex = 5;
            this.CANCELBTN.Text = "CENCEL";
            this.CANCELBTN.UseVisualStyleBackColor = false;
            this.CANCELBTN.Click += new System.EventHandler(this.CANCELBTN_Click);
            // 
            // password1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(337, 192);
            this.Controls.Add(this.CANCELBTN);
            this.Controls.Add(this.userinputtb);
            this.Controls.Add(this.OKBTN);
            this.Controls.Add(this.HEADER);
            this.Controls.Add(this.getpass);
            this.Name = "password1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label getpass;
        private System.Windows.Forms.Label HEADER;
        private System.Windows.Forms.Button OKBTN;
        private System.Windows.Forms.TextBox userinputtb;
        private System.Windows.Forms.Button CANCELBTN;
    }
}