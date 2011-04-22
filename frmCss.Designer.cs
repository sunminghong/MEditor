namespace MEditor
{
    partial class frmCss
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtCss = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTabWidth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(259, 338);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "取消(&C)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCss
            // 
            this.txtCss.Location = new System.Drawing.Point(8, 95);
            this.txtCss.Multiline = true;
            this.txtCss.Name = "txtCss";
            this.txtCss.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCss.Size = new System.Drawing.Size(570, 226);
            this.txtCss.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 337);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "保存(&S)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(101, 27);
            this.button3.TabIndex = 3;
            this.button3.Text = "前景色";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(133, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(101, 27);
            this.button4.TabIndex = 4;
            this.button4.Text = "背景色";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(253, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(101, 27);
            this.button5.TabIndex = 5;
            this.button5.Text = "字  体";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(366, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tab宽度(0即为\\t)：";
            // 
            // txtTabWidth
            // 
            this.txtTabWidth.Location = new System.Drawing.Point(470, 16);
            this.txtTabWidth.MaxLength = 8;
            this.txtTabWidth.Name = "txtTabWidth";
            this.txtTabWidth.ReadOnly = true;
            this.txtTabWidth.Size = new System.Drawing.Size(38, 21);
            this.txtTabWidth.TabIndex = 7;
            this.txtTabWidth.Text = "4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Html预览样式：";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(340, 337);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(99, 24);
            this.button6.TabIndex = 9;
            this.button6.Text = "还原白底黑字";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(445, 336);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(99, 24);
            this.button7.TabIndex = 10;
            this.button7.Text = "还原黑底白字";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "可编辑的文档格式：";
            // 
            // txtExt
            // 
            this.txtExt.Location = new System.Drawing.Point(116, 52);
            this.txtExt.Name = "txtExt";
            this.txtExt.Size = new System.Drawing.Size(459, 21);
            this.txtExt.TabIndex = 12;
            // 
            // frmCss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(587, 370);
            this.Controls.Add(this.txtExt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTabWidth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtCss);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCss";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "选项";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCss_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCss;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTabWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExt;
    }
}