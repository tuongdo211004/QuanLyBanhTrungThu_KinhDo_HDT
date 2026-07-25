namespace QLBHKINHDO
{
    partial class frmQuanLyTonKho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQuanLyTonKho));
            this.label2 = new System.Windows.Forms.Label();
            this.btnDanhsach = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoSapHet = new System.Windows.Forms.RadioButton();
            this.rdoTatCa = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.txtMaLoaiSP = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtThongTin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(437, 240);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 86;
            this.label2.Text = "Kết quả";
            // 
            // btnDanhsach
            // 
            this.btnDanhsach.Location = new System.Drawing.Point(574, 199);
            this.btnDanhsach.Margin = new System.Windows.Forms.Padding(2);
            this.btnDanhsach.Name = "btnDanhsach";
            this.btnDanhsach.Size = new System.Drawing.Size(96, 37);
            this.btnDanhsach.TabIndex = 85;
            this.btnDanhsach.Text = "Danh sách";
            this.btnDanhsach.UseVisualStyleBackColor = true;
           
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoSapHet);
            this.groupBox1.Controls.Add(this.rdoTatCa);
            this.groupBox1.Location = new System.Drawing.Point(24, 124);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(398, 50);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm theo";
            // 
            // rdoSapHet
            // 
            this.rdoSapHet.AutoSize = true;
            this.rdoSapHet.Location = new System.Drawing.Point(125, 20);
            this.rdoSapHet.Margin = new System.Windows.Forms.Padding(2);
            this.rdoSapHet.Name = "rdoSapHet";
            this.rdoSapHet.Size = new System.Drawing.Size(265, 17);
            this.rdoSapHet.TabIndex = 26;
            this.rdoSapHet.TabStop = true;
            this.rdoSapHet.Text = "Chỉ hiện sản phẩm có số lượng < 50 (để báo động)";
            this.rdoSapHet.UseVisualStyleBackColor = true;
          
            // rdoTatCa
            // 
            this.rdoTatCa.AutoSize = true;
            this.rdoTatCa.Location = new System.Drawing.Point(16, 20);
            this.rdoTatCa.Margin = new System.Windows.Forms.Padding(2);
            this.rdoTatCa.Name = "rdoTatCa";
            this.rdoTatCa.Size = new System.Drawing.Size(76, 17);
            this.rdoTatCa.TabIndex = 25;
            this.rdoTatCa.TabStop = true;
            this.rdoTatCa.Text = "Xem tất cả";
            this.rdoTatCa.UseVisualStyleBackColor = true;
           
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(373, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 22);
            this.label5.TabIndex = 80;
            this.label5.Text = "DANH MỤC TỒN KHO";
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Location = new System.Drawing.Point(11, 275);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.Size = new System.Drawing.Size(904, 154);
            this.dgvTonKho.TabIndex = 78;
      
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(-5, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 187);
            this.panel1.TabIndex = 100;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLamMoi.Location = new System.Drawing.Point(24, 192);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(2);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(77, 37);
            this.btnLamMoi.TabIndex = 101;
            this.btnLamMoi.Text = "Tải lại";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click_1);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnXuatExcel.Location = new System.Drawing.Point(810, 191);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(106, 52);
            this.btnXuatExcel.TabIndex = 134;
            this.btnXuatExcel.Text = "Xuất ra Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(740, 93);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(88, 32);
            this.btnXoa.TabIndex = 137;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
       
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(632, 93);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(86, 32);
            this.btnSua.TabIndex = 136;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
        
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(525, 93);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(88, 32);
            this.btnThem.TabIndex = 135;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click_1);
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(89, 46);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(157, 20);
            this.txtMaSP.TabIndex = 138;
           
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(89, 86);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(157, 20);
            this.txtTenSP.TabIndex = 139;
          
            // 
            // txtMaLoaiSP
            // 
            this.txtMaLoaiSP.Location = new System.Drawing.Point(348, 46);
            this.txtMaLoaiSP.Name = "txtMaLoaiSP";
            this.txtMaLoaiSP.Size = new System.Drawing.Size(157, 20);
            this.txtMaLoaiSP.TabIndex = 140;
        
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(348, 89);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(157, 20);
            this.txtSoLuong.TabIndex = 141;
        
            // 
            // txtDonGia
            // 
            this.txtDonGia.Location = new System.Drawing.Point(561, 46);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(267, 20);
            this.txtDonGia.TabIndex = 142;
         
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 143;
            this.label1.Text = "Mã sản phẩm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 144;
            this.label3.Text = "Tên sản phẩm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 145;
            this.label4.Text = "Mã loại sản phẩm";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(293, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 146;
            this.label6.Text = "Số lượng";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(511, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 147;
            this.label7.Text = "Đơn giá";
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(464, 201);
            this.btnTim.Margin = new System.Windows.Forms.Padding(2);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(77, 37);
            this.btnTim.TabIndex = 84;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click_1);
            // 
            // txtThongTin
            // 
            this.txtThongTin.Location = new System.Drawing.Point(463, 168);
            this.txtThongTin.Margin = new System.Windows.Forms.Padding(2);
            this.txtThongTin.Name = "txtThongTin";
            this.txtThongTin.Size = new System.Drawing.Size(207, 20);
            this.txtThongTin.TabIndex = 149;
         
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(461, 141);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 148;
            this.label8.Text = "Nhập thông tin";
            // 
            // frmQuanLyTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 448);
            this.Controls.Add(this.txtThongTin);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.txtMaLoaiSP);
            this.Controls.Add(this.txtTenSP);
            this.Controls.Add(this.txtMaSP);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDanhsach);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvTonKho);
            this.Controls.Add(this.panel1);
            this.Name = "frmQuanLyTonKho";
            this.Text = "frmQuanLyTonKho";
            this.Load += new System.EventHandler(this.frmQuanLyTonKho_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDanhsach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoSapHet;
        private System.Windows.Forms.RadioButton rdoTatCa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.TextBox txtMaLoaiSP;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtThongTin;
        private System.Windows.Forms.Label label8;
    }
}