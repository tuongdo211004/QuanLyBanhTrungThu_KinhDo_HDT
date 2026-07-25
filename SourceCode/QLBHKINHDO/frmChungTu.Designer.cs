namespace QLBHKINHDO
{
    partial class frmChungTu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvChungTu = new System.Windows.Forms.DataGridView();
            this.txtMaCT = new System.Windows.Forms.TextBox();
            this.txtLoaiCT = new System.Windows.Forms.TextBox();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.txtTongGiaTri = new System.Windows.Forms.TextBox();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            this.dtpNgayLap = new System.Windows.Forms.DateTimePicker();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnXuatWord = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.buttonBC = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChungTu)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvChungTu
            // 
            this.dgvChungTu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChungTu.Location = new System.Drawing.Point(12, 230);
            this.dgvChungTu.Name = "dgvChungTu";
            this.dgvChungTu.Size = new System.Drawing.Size(776, 200);
            this.dgvChungTu.TabIndex = 14;
            this.dgvChungTu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChungTu_CellClick);
            // 
            // txtMaCT
            // 
            this.txtMaCT.Location = new System.Drawing.Point(100, 30);
            this.txtMaCT.Name = "txtMaCT";
            this.txtMaCT.Size = new System.Drawing.Size(150, 20);
            this.txtMaCT.TabIndex = 13;
            // 
            // txtLoaiCT
            // 
            this.txtLoaiCT.Location = new System.Drawing.Point(100, 59);
            this.txtLoaiCT.Name = "txtLoaiCT";
            this.txtLoaiCT.Size = new System.Drawing.Size(150, 20);
            this.txtLoaiCT.TabIndex = 12;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(350, 30);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(200, 60);
            this.txtNoiDung.TabIndex = 11;
            // 
            // txtTongGiaTri
            // 
            this.txtTongGiaTri.Location = new System.Drawing.Point(350, 110);
            this.txtTongGiaTri.Name = "txtTongGiaTri";
            this.txtTongGiaTri.Size = new System.Drawing.Size(200, 20);
            this.txtTongGiaTri.TabIndex = 10;
            // 
            // txtTrangThai
            // 
            this.txtTrangThai.Location = new System.Drawing.Point(100, 120);
            this.txtTrangThai.Name = "txtTrangThai";
            this.txtTrangThai.Size = new System.Drawing.Size(150, 20);
            this.txtTrangThai.TabIndex = 0;
            this.txtTrangThai.Text = "Hoàn thành";
            // 
            // dtpNgayLap
            // 
            this.dtpNgayLap.Location = new System.Drawing.Point(100, 85);
            this.dtpNgayLap.Name = "dtpNgayLap";
            this.dtpNgayLap.Size = new System.Drawing.Size(150, 20);
            this.dtpNgayLap.TabIndex = 9;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(600, 30);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 30);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnXuatExcel.Location = new System.Drawing.Point(600, 70);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(80, 30);
            this.btnXuatExcel.TabIndex = 7;
            this.btnXuatExcel.Text = "Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // btnXuatWord
            // 
            this.btnXuatWord.BackColor = System.Drawing.Color.LightBlue;
            this.btnXuatWord.Location = new System.Drawing.Point(686, 70);
            this.btnXuatWord.Name = "btnXuatWord";
            this.btnXuatWord.Size = new System.Drawing.Size(80, 30);
            this.btnXuatWord.TabIndex = 6;
            this.btnXuatWord.Text = "In (Word)";
            this.btnXuatWord.UseVisualStyleBackColor = false;
            this.btnXuatWord.Click += new System.EventHandler(this.btnXuatWord_Click);
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.Location = new System.Drawing.Point(600, 114);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(80, 30);
            this.btnTaiLai.TabIndex = 5;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mã CT:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Loại CT:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(280, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nội dung:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(280, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 23);
            this.label4.TabIndex = 1;
            this.label4.Text = "Giá trị:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(12, 150);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(776, 280);
            this.reportViewer1.TabIndex = 15;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // buttonBC
            // 
            this.buttonBC.Location = new System.Drawing.Point(686, 113);
            this.buttonBC.Name = "buttonBC";
            this.buttonBC.Size = new System.Drawing.Size(75, 23);
            this.buttonBC.TabIndex = 16;
            this.buttonBC.Text = "buttonXemBC";
            this.buttonBC.UseVisualStyleBackColor = true;
            this.buttonBC.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmChungTu
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonBC);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.txtTrangThai);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTaiLai);
            this.Controls.Add(this.btnXuatWord);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dtpNgayLap);
            this.Controls.Add(this.txtTongGiaTri);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.txtLoaiCT);
            this.Controls.Add(this.txtMaCT);
            this.Controls.Add(this.dgvChungTu);
            this.Name = "frmChungTu";
            this.Text = "Quản Lý Chứng Từ";
            this.Load += new System.EventHandler(this.frmChungTu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChungTu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.DataGridView dgvChungTu;
        private System.Windows.Forms.TextBox txtMaCT;
        private System.Windows.Forms.TextBox txtLoaiCT;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.TextBox txtTongGiaTri;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.DateTimePicker dtpNgayLap;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXuatWord;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button buttonBC;
    }
}