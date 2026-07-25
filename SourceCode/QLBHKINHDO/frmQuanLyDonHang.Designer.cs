namespace QLBHKINHDO
{
    partial class frmQuanLyDonHang
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
            this.btnDonHangNhap = new System.Windows.Forms.Button();
            this.btnDonHangBan = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDonHangNhap
            // 
            this.btnDonHangNhap.BackColor = System.Drawing.Color.Orange;
            this.btnDonHangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonHangNhap.Location = new System.Drawing.Point(-3, -24);
            this.btnDonHangNhap.Name = "btnDonHangNhap";
            this.btnDonHangNhap.Size = new System.Drawing.Size(127, 74);
            this.btnDonHangNhap.TabIndex = 5;
            this.btnDonHangNhap.Text = "Đơn Hàng Nhập";
            this.btnDonHangNhap.UseVisualStyleBackColor = false;
            this.btnDonHangNhap.Click += new System.EventHandler(this.btnQuanLyDonHang_Click);
            // 
            // btnDonHangBan
            // 
            this.btnDonHangBan.BackColor = System.Drawing.Color.Orange;
            this.btnDonHangBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDonHangBan.Location = new System.Drawing.Point(117, -24);
            this.btnDonHangBan.Name = "btnDonHangBan";
            this.btnDonHangBan.Size = new System.Drawing.Size(118, 74);
            this.btnDonHangBan.TabIndex = 1;
            this.btnDonHangBan.Text = "Đơn Hàng Bán";
            this.btnDonHangBan.UseVisualStyleBackColor = false;
            this.btnDonHangBan.Click += new System.EventHandler(this.btnDonHangBan_Click);
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(-2, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(953, 430);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Location = new System.Drawing.Point(1, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(947, 487);
            this.panel3.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDonHangBan);
            this.panel1.Controls.Add(this.btnDonHangNhap);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 36);
            this.panel1.TabIndex = 6;
            // 
            // frmQuanLyDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 448);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "frmQuanLyDonHang";
            this.Text = "frmQuanLyDonHang";
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDonHangNhap;
        private System.Windows.Forms.Button btnDonHangBan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
    }
}