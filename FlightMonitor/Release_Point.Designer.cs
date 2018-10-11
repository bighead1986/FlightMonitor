namespace FlightMonitor
{
    partial class Release_Point
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
            this.label3 = new System.Windows.Forms.Label();
            this.dtpks = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpjs = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtnr = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnfb = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(24, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "开始时间:";
            // 
            // dtpks
            // 
            this.dtpks.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpks.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpks.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpks.Location = new System.Drawing.Point(105, 28);
            this.dtpks.Name = "dtpks";
            this.dtpks.ShowUpDown = true;
            this.dtpks.Size = new System.Drawing.Size(284, 26);
            this.dtpks.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(443, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "结束时间:";
            // 
            // dtpjs
            // 
            this.dtpjs.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpjs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtpjs.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpjs.Location = new System.Drawing.Point(525, 28);
            this.dtpjs.Name = "dtpjs";
            this.dtpjs.ShowUpDown = true;
            this.dtpjs.Size = new System.Drawing.Size(284, 26);
            this.dtpjs.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(24, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "消息内容:";
            // 
            // txtnr
            // 
            this.txtnr.Location = new System.Drawing.Point(105, 75);
            this.txtnr.Multiline = true;
            this.txtnr.Name = "txtnr";
            this.txtnr.Size = new System.Drawing.Size(704, 104);
            this.txtnr.TabIndex = 11;
            this.txtnr.TextChanged += new System.EventHandler(this.txtnr_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(102, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 14);
            this.label5.TabIndex = 12;
            this.label5.Visible = false;
            // 
            // btnfb
            // 
            this.btnfb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnfb.Location = new System.Drawing.Point(105, 218);
            this.btnfb.Name = "btnfb";
            this.btnfb.Size = new System.Drawing.Size(75, 23);
            this.btnfb.TabIndex = 13;
            this.btnfb.Text = "发布";
            this.btnfb.UseVisualStyleBackColor = true;
            this.btnfb.Click += new System.EventHandler(this.btnfb_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(303, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 14;
            // 
            // 发布点位消息
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 248);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnfb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtnr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpjs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpks);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "发布点位消息";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "发布点位消息";
            this.Load += new System.EventHandler(this.发布点位消息_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpjs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtnr;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnfb;
        private System.Windows.Forms.Label label4;
    }
}