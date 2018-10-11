namespace FlightMonitor
{
    partial class Manager_Msg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_msg = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_wz = new System.Windows.Forms.ComboBox();
            this.cmb_hzl = new System.Windows.Forms.ComboBox();
            this.cmb_zl = new System.Windows.Forms.ComboBox();
            this.dtp_ks = new System.Windows.Forms.DateTimePicker();
            this.dtp_js = new System.Windows.Forms.DateTimePicker();
            this.txb_msg = new System.Windows.Forms.TextBox();
            this.btn_gx = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_sb = new System.Windows.Forms.ComboBox();
            this.btn_fb = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_msg)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1068, 658);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_msg);
            this.panel1.Location = new System.Drawing.Point(1, 225);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1068, 435);
            this.panel1.TabIndex = 0;
            // 
            // dgv_msg
            // 
            this.dgv_msg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_msg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_msg.Location = new System.Drawing.Point(0, 0);
            this.dgv_msg.Name = "dgv_msg";
            this.dgv_msg.RowTemplate.Height = 23;
            this.dgv_msg.Size = new System.Drawing.Size(1068, 435);
            this.dgv_msg.TabIndex = 0;
            this.dgv_msg.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_msg_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(-2, 663);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_fb);
            this.panel2.Controls.Add(this.cmb_sb);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.btn_del);
            this.panel2.Controls.Add(this.btn_gx);
            this.panel2.Controls.Add(this.txb_msg);
            this.panel2.Controls.Add(this.dtp_js);
            this.panel2.Controls.Add(this.dtp_ks);
            this.panel2.Controls.Add(this.cmb_zl);
            this.panel2.Controls.Add(this.cmb_hzl);
            this.panel2.Controls.Add(this.cmb_wz);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txt_id);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel2.ForeColor = System.Drawing.Color.Blue;
            this.panel2.Location = new System.Drawing.Point(0, 10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1068, 207);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(11, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(34, 10);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(196, 23);
            this.txt_id.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(246, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "位置:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "名称:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 4;
            this.label5.Text = "指廊:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(748, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "航站楼:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(246, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 14);
            this.label7.TabIndex = 6;
            this.label7.Text = "开始:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(496, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "结束:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(-3, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 8;
            this.label9.Text = "消息:";
            // 
            // cmb_wz
            // 
            this.cmb_wz.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_wz.FormattingEnabled = true;
            this.cmb_wz.Location = new System.Drawing.Point(285, 11);
            this.cmb_wz.Name = "cmb_wz";
            this.cmb_wz.Size = new System.Drawing.Size(196, 21);
            this.cmb_wz.TabIndex = 10;
            // 
            // cmb_hzl
            // 
            this.cmb_hzl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_hzl.FormattingEnabled = true;
            this.cmb_hzl.Location = new System.Drawing.Point(799, 11);
            this.cmb_hzl.Name = "cmb_hzl";
            this.cmb_hzl.Size = new System.Drawing.Size(196, 21);
            this.cmb_hzl.TabIndex = 11;
            // 
            // cmb_zl
            // 
            this.cmb_zl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_zl.FormattingEnabled = true;
            this.cmb_zl.Location = new System.Drawing.Point(34, 70);
            this.cmb_zl.Name = "cmb_zl";
            this.cmb_zl.Size = new System.Drawing.Size(196, 21);
            this.cmb_zl.TabIndex = 12;
            // 
            // dtp_ks
            // 
            this.dtp_ks.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtp_ks.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_ks.Location = new System.Drawing.Point(285, 66);
            this.dtp_ks.Name = "dtp_ks";
            this.dtp_ks.Size = new System.Drawing.Size(196, 23);
            this.dtp_ks.TabIndex = 13;
            // 
            // dtp_js
            // 
            this.dtp_js.CustomFormat = "yyyy/MM/dd HH:mm:ss";
            this.dtp_js.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_js.Location = new System.Drawing.Point(535, 66);
            this.dtp_js.Name = "dtp_js";
            this.dtp_js.Size = new System.Drawing.Size(196, 23);
            this.dtp_js.TabIndex = 14;
            // 
            // txb_msg
            // 
            this.txb_msg.Location = new System.Drawing.Point(34, 97);
            this.txb_msg.Multiline = true;
            this.txb_msg.Name = "txb_msg";
            this.txb_msg.Size = new System.Drawing.Size(697, 83);
            this.txb_msg.TabIndex = 15;
            this.txb_msg.TextChanged += new System.EventHandler(this.txb_msg_TextChanged);
            // 
            // btn_gx
            // 
            this.btn_gx.Location = new System.Drawing.Point(737, 157);
            this.btn_gx.Name = "btn_gx";
            this.btn_gx.Size = new System.Drawing.Size(60, 23);
            this.btn_gx.TabIndex = 16;
            this.btn_gx.Text = "更新";
            this.btn_gx.UseVisualStyleBackColor = true;
            this.btn_gx.Click += new System.EventHandler(this.btn_gx_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(803, 157);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(59, 23);
            this.btn_del.TabIndex = 17;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(34, 181);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 12);
            this.label10.TabIndex = 18;
            this.label10.Visible = false;
            // 
            // cmb_sb
            // 
            this.cmb_sb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_sb.FormattingEnabled = true;
            this.cmb_sb.Location = new System.Drawing.Point(535, 11);
            this.cmb_sb.Name = "cmb_sb";
            this.cmb_sb.Size = new System.Drawing.Size(196, 21);
            this.cmb_sb.TabIndex = 19;
            // 
            // btn_fb
            // 
            this.btn_fb.ForeColor = System.Drawing.Color.Green;
            this.btn_fb.Location = new System.Drawing.Point(868, 157);
            this.btn_fb.Name = "btn_fb";
            this.btn_fb.Size = new System.Drawing.Size(75, 23);
            this.btn_fb.TabIndex = 20;
            this.btn_fb.Text = "发布消息";
            this.btn_fb.UseVisualStyleBackColor = true;
            this.btn_fb.Click += new System.EventHandler(this.btn_fb_Click);
            // 
            // 管理消息
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 681);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "管理消息";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "管理消息";
            this.Load += new System.EventHandler(this.管理消息_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_msg)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_msg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txb_msg;
        private System.Windows.Forms.DateTimePicker dtp_js;
        private System.Windows.Forms.DateTimePicker dtp_ks;
        private System.Windows.Forms.ComboBox cmb_zl;
        private System.Windows.Forms.ComboBox cmb_hzl;
        private System.Windows.Forms.ComboBox cmb_wz;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_gx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_sb;
        private System.Windows.Forms.Button btn_fb;
    }
}