using System.Windows.Forms;
namespace MEditor
{
    partial class GoToLineDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoToLineDialog));
            this.desclbl = new System.Windows.Forms.Label();
            this.okbtn = new System.Windows.Forms.Button();
            this.lnbox = new System.Windows.Forms.TextBox();
            this.MainPan = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.MainPan.SuspendLayout();
            this.SuspendLayout();
            // 
            // desclbl
            // 
            this.desclbl.AutoSize = true;
            this.desclbl.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desclbl.Location = new System.Drawing.Point(21, 24);
            this.desclbl.Name = "desclbl";
            this.desclbl.Size = new System.Drawing.Size(46, 16);
            this.desclbl.TabIndex = 1;
            this.desclbl.Text = "行号:";
            // 
            // okbtn
            // 
            this.okbtn.BackColor = System.Drawing.Color.AliceBlue;
            this.okbtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.okbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okbtn.ForeColor = System.Drawing.Color.RoyalBlue;
            this.okbtn.Location = new System.Drawing.Point(44, 58);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(70, 20);
            this.okbtn.TabIndex = 2;
            this.okbtn.Text = "转到";
            this.okbtn.UseVisualStyleBackColor = false;
            this.okbtn.Click += new System.EventHandler(this.Go);
            // 
            // lnbox
            // 
            this.lnbox.Location = new System.Drawing.Point(75, 23);
            this.lnbox.Name = "lnbox";
            this.lnbox.Size = new System.Drawing.Size(157, 21);
            this.lnbox.TabIndex = 0;
            // 
            // MainPan
            // 
            this.MainPan.Controls.Add(this.button1);
            this.MainPan.Controls.Add(this.lnbox);
            this.MainPan.Controls.Add(this.desclbl);
            this.MainPan.Controls.Add(this.okbtn);
            this.MainPan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPan.Location = new System.Drawing.Point(0, 0);
            this.MainPan.Name = "MainPan";
            this.MainPan.Size = new System.Drawing.Size(244, 90);
            this.MainPan.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GoToLineDialog
            // 
            this.AcceptButton = this.okbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(244, 90);
            this.Controls.Add(this.MainPan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoToLineDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "转到指定行";
            this.MainPan.ResumeLayout(false);
            this.MainPan.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label desclbl;
        private Button okbtn;
        private TextBox lnbox;
        private Panel MainPan;
        private Button button1;
    }
}