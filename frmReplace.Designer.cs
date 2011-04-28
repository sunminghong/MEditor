using System.Windows.Forms;
namespace MEditor
{
    partial class frmReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplace));
            this.usewild = new System.Windows.Forms.CheckBox();
            this.useregex = new System.Windows.Forms.CheckBox();
            this.CaseSen = new System.Windows.Forms.CheckBox();
            this.MatchWhole = new System.Windows.Forms.CheckBox();
            this.repbtn = new System.Windows.Forms.Button();
            this.repallbtn = new System.Windows.Forms.Button();
            this.replbl = new System.Windows.Forms.Label();
            this.findlbl = new System.Windows.Forms.Label();
            this.repbox = new System.Windows.Forms.TextBox();
            this.findbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usewild
            // 
            this.usewild.AutoSize = true;
            this.usewild.Location = new System.Drawing.Point(251, 200);
            this.usewild.Name = "usewild";
            this.usewild.Size = new System.Drawing.Size(91, 20);
            this.usewild.TabIndex = 20;
            this.usewild.Text = "用通配符";
            this.usewild.UseVisualStyleBackColor = true;
            // 
            // useregex
            // 
            this.useregex.AutoSize = true;
            this.useregex.Location = new System.Drawing.Point(89, 200);
            this.useregex.Name = "useregex";
            this.useregex.Size = new System.Drawing.Size(139, 20);
            this.useregex.TabIndex = 19;
            this.useregex.Text = "使用正则表达式";
            this.useregex.UseVisualStyleBackColor = true;
            // 
            // CaseSen
            // 
            this.CaseSen.AutoSize = true;
            this.CaseSen.Location = new System.Drawing.Point(251, 174);
            this.CaseSen.Name = "CaseSen";
            this.CaseSen.Size = new System.Drawing.Size(107, 20);
            this.CaseSen.TabIndex = 18;
            this.CaseSen.Text = "大小写敏感";
            this.CaseSen.UseVisualStyleBackColor = true;
            // 
            // MatchWhole
            // 
            this.MatchWhole.AutoSize = true;
            this.MatchWhole.Location = new System.Drawing.Point(89, 174);
            this.MatchWhole.Name = "MatchWhole";
            this.MatchWhole.Size = new System.Drawing.Size(91, 20);
            this.MatchWhole.TabIndex = 17;
            this.MatchWhole.Text = "整个单词";
            this.MatchWhole.UseVisualStyleBackColor = true;
            // 
            // repbtn
            // 
            this.repbtn.BackColor = System.Drawing.SystemColors.Control;
            this.repbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.repbtn.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.repbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.repbtn.Location = new System.Drawing.Point(230, 244);
            this.repbtn.Name = "repbtn";
            this.repbtn.Size = new System.Drawing.Size(99, 24);
            this.repbtn.TabIndex = 16;
            this.repbtn.Text = "替换(&R)";
            this.repbtn.UseVisualStyleBackColor = false;
            this.repbtn.Click += new System.EventHandler(this.Replace);
            // 
            // repallbtn
            // 
            this.repallbtn.BackColor = System.Drawing.SystemColors.Control;
            this.repallbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.repallbtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repallbtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.repallbtn.Location = new System.Drawing.Point(335, 244);
            this.repallbtn.Name = "repallbtn";
            this.repallbtn.Size = new System.Drawing.Size(120, 24);
            this.repallbtn.TabIndex = 15;
            this.repallbtn.Text = "替换所有(&A)";
            this.repallbtn.UseVisualStyleBackColor = false;
            this.repallbtn.Click += new System.EventHandler(this.ReplaceAll);
            // 
            // replbl
            // 
            this.replbl.AutoSize = true;
            this.replbl.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.replbl.Location = new System.Drawing.Point(28, 93);
            this.replbl.Name = "replbl";
            this.replbl.Size = new System.Drawing.Size(55, 13);
            this.replbl.TabIndex = 14;
            this.replbl.Text = "替换为：";
            // 
            // findlbl
            // 
            this.findlbl.AutoSize = true;
            this.findlbl.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.findlbl.Location = new System.Drawing.Point(16, 12);
            this.findlbl.Name = "findlbl";
            this.findlbl.Size = new System.Drawing.Size(67, 13);
            this.findlbl.TabIndex = 13;
            this.findlbl.Text = "查找内容：";
            // 
            // repbox
            // 
            this.repbox.AcceptsTab = true;
            this.repbox.Location = new System.Drawing.Point(89, 93);
            this.repbox.Multiline = true;
            this.repbox.Name = "repbox";
            this.repbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.repbox.Size = new System.Drawing.Size(366, 75);
            this.repbox.TabIndex = 12;
            this.repbox.WordWrap = false;
            // 
            // findbox
            // 
            this.findbox.AcceptsTab = true;
            this.findbox.Location = new System.Drawing.Point(89, 12);
            this.findbox.Multiline = true;
            this.findbox.Name = "findbox";
            this.findbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.findbox.Size = new System.Drawing.Size(366, 75);
            this.findbox.TabIndex = 11;
            this.findbox.WordWrap = false;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(129, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.TabStop = false;
            this.button1.Text = "取消";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmReplace
            // 
            this.AcceptButton = this.repbtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(469, 278);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.usewild);
            this.Controls.Add(this.useregex);
            this.Controls.Add(this.CaseSen);
            this.Controls.Add(this.MatchWhole);
            this.Controls.Add(this.repbtn);
            this.Controls.Add(this.repallbtn);
            this.Controls.Add(this.replbl);
            this.Controls.Add(this.findlbl);
            this.Controls.Add(this.repbox);
            this.Controls.Add(this.findbox);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReplace";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "查找与替换";
            this.Load += new System.EventHandler(this.frmReplace_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox usewild;
        private CheckBox useregex;
        private CheckBox CaseSen;
        private CheckBox MatchWhole;
        private Button repbtn;
        private Button repallbtn;
        private Label replbl;
        private Label findlbl;
        private TextBox repbox;
        private TextBox findbox;
        private Button button1;

    }
}