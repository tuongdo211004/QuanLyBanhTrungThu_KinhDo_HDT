namespace QLBHKINHDO
{
    partial class frmKhuyenMai
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhuyenMai));
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTenVoucher = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGiaTriGiam = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaVoucher = new System.Windows.Forms.TextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDanhsach = new System.Windows.Forms.Button();
            this.btnTim = new System.Windows.Forms.Button();
            this.txtThongTin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoTenVoucher = new System.Windows.Forms.RadioButton();
            this.rdoMaVoucher = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpNgayBatDau = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayKetThuc = new System.Windows.Forms.DateTimePicker();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(421, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 98;
            this.label8.Text = "Ngày bắt đầu";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(417, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 96;
            this.label7.Text = "Ngày kết thúc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 94;
            this.label6.Text = "Tên Voucher";
            // 
            // txtTenVoucher
            // 
            this.txtTenVoucher.Location = new System.Drawing.Point(187, 86);
            this.txtTenVoucher.Name = "txtTenVoucher";
            this.txtTenVoucher.Size = new System.Drawing.Size(212, 20);
            this.txtTenVoucher.TabIndex = 93;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 92;
            this.label4.Text = "Giá trị giảm";
            // 
            // txtGiaTriGiam
            // 
            this.txtGiaTriGiam.Location = new System.Drawing.Point(187, 122);
            this.txtGiaTriGiam.Name = "txtGiaTriGiam";
            this.txtGiaTriGiam.Size = new System.Drawing.Size(212, 20);
            this.txtGiaTriGiam.TabIndex = 91;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 90;
            this.label3.Text = "Mã Voucher";
            // 
            // txtMaVoucher
            // 
            this.txtMaVoucher.Location = new System.Drawing.Point(187, 46);
            this.txtMaVoucher.Name = "txtMaVoucher";
            this.txtMaVoucher.Size = new System.Drawing.Size(212, 20);
            this.txtMaVoucher.TabIndex = 89;
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(824, 183);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(77, 27);
            this.btnXoa.TabIndex = 88;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(722, 183);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(77, 27);
            this.btnSua.TabIndex = 87;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(449, 228);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 86;
            this.label2.Text = "Kết quả";
            // 
            // btnDanhsach
            // 
            this.btnDanhsach.Location = new System.Drawing.Point(221, 217);
            this.btnDanhsach.Margin = new System.Windows.Forms.Padding(2);
            this.btnDanhsach.Name = "btnDanhsach";
            this.btnDanhsach.Size = new System.Drawing.Size(74, 28);
            this.btnDanhsach.TabIndex = 85;
            this.btnDanhsach.Text = "Danh sách";
            this.btnDanhsach.UseVisualStyleBackColor = true;
            this.btnDanhsach.Click += new System.EventHandler(this.btnDanhSach_Click);
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(130, 217);
            this.btnTim.Margin = new System.Windows.Forms.Padding(2);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(62, 28);
            this.btnTim.TabIndex = 84;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // txtThongTin
            // 
            this.txtThongTin.Location = new System.Drawing.Point(357, 190);
            this.txtThongTin.Margin = new System.Windows.Forms.Padding(2);
            this.txtThongTin.Name = "txtThongTin";
            this.txtThongTin.Size = new System.Drawing.Size(207, 20);
            this.txtThongTin.TabIndex = 83;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(355, 163);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "Nhập thông tin";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoTenVoucher);
            this.groupBox1.Controls.Add(this.rdoMaVoucher);
            this.groupBox1.Location = new System.Drawing.Point(99, 163);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(238, 50);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm theo";
            // 
            // rdoTenVoucher
            // 
            this.rdoTenVoucher.AutoSize = true;
            this.rdoTenVoucher.Location = new System.Drawing.Point(125, 20);
            this.rdoTenVoucher.Margin = new System.Windows.Forms.Padding(2);
            this.rdoTenVoucher.Name = "rdoTenVoucher";
            this.rdoTenVoucher.Size = new System.Drawing.Size(87, 17);
            this.rdoTenVoucher.TabIndex = 26;
            this.rdoTenVoucher.TabStop = true;
            this.rdoTenVoucher.Text = "Tên Voucher";
            this.rdoTenVoucher.UseVisualStyleBackColor = true;
            // 
            // rdoMaVoucher
            // 
            this.rdoMaVoucher.AutoSize = true;
            this.rdoMaVoucher.Location = new System.Drawing.Point(16, 20);
            this.rdoMaVoucher.Margin = new System.Windows.Forms.Padding(2);
            this.rdoMaVoucher.Name = "rdoMaVoucher";
            this.rdoMaVoucher.Size = new System.Drawing.Size(83, 17);
            this.rdoMaVoucher.TabIndex = 25;
            this.rdoMaVoucher.TabStop = true;
            this.rdoMaVoucher.Text = "Mã Voucher";
            this.rdoMaVoucher.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(343, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(244, 22);
            this.label5.TabIndex = 80;
            this.label5.Text = "DANH MỤC KHUYẾN MÃI";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(611, 183);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(77, 27);
            this.btnThem.TabIndex = 79;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKetQua.Location = new System.Drawing.Point(12, 272);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.Size = new System.Drawing.Size(904, 166);
            this.dgvKetQua.TabIndex = 78;
            this.dgvKetQua.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKetQua_CellClick);
            this.dgvKetQua.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKetQua_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Location = new System.Drawing.Point(-4, 260);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(938, 196);
            this.panel1.TabIndex = 100;
            // 
            // dtpNgayBatDau
            // 
            this.dtpNgayBatDau.Location = new System.Drawing.Point(497, 46);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Size = new System.Drawing.Size(187, 20);
            this.dtpNgayBatDau.TabIndex = 105;
            // 
            // dtpNgayKetThuc
            // 
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(497, 86);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(187, 20);
            this.dtpNgayKetThuc.TabIndex = 106;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTaiLai.Location = new System.Drawing.Point(358, 218);
            this.btnTaiLai.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(48, 26);
            this.btnTaiLai.TabIndex = 137;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnXuatExcel.Location = new System.Drawing.Point(817, 114);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(84, 41);
            this.btnXuatExcel.TabIndex = 138;
            this.btnXuatExcel.Text = "Xuất ra Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(436, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 102;
            this.label10.Text = "Trạng thái";
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Location = new System.Drawing.Point(497, 121);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(145, 20);
            this.txtTrangThai.TabIndex = 103;
            this.txtTrangThai.TextChanged += new System.EventHandler(this.txtTrangThai_TextChanged);
            // 
            // frmKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 450);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnTaiLai);
            this.Controls.Add(this.dtpNgayKetThuc);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTenVoucher);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtGiaTriGiam);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMaVoucher);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDanhsach);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtThongTin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.panel1);
            this.Name = "frmKhuyenMai";
            this.Text = "frmKhuyenMai";
            this.Load += new System.EventHandler(this.frmKhuyenMai_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTenVoucher;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGiaTriGiam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaVoucher;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDanhsach;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtThongTin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoTenVoucher;
        private System.Windows.Forms.RadioButton rdoMaVoucher;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpNgayBatDau;
        private System.Windows.Forms.DateTimePicker dtpNgayKetThuc;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTrangThai;
    }
}