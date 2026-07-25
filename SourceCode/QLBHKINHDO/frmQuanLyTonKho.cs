using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmQuanLyTonKho : Form
    {
        public frmQuanLyTonKho()
        {
            InitializeComponent();
        }

        private void frmQuanLyTonKho_Load(object sender, EventArgs e)
        {
            rdoTatCa.Checked = true;
            LoadData();
        }

        // --- HÀM TẢI DỮ LIỆU & LỌC ---
        private void LoadData()
        {
            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    // Câu lệnh SQL chuẩn: Lấy thông tin SP, Tên Loại từ bảng LoaiSanPham và Tổng tồn từ bảng TonKho
                    string sql = @"
                        SELECT sp.MaSP, sp.TenSP, lsp.TenLoaiSP, 
                               ISNULL(SUM(tk.SoLuongTon), 0) AS SoLuongTon, sp.GiaBan 
                        FROM SanPham sp
                        LEFT JOIN LoaiSanPham lsp ON sp.MaLoaiSP = lsp.MaLoaiSP
                        LEFT JOIN TonKho tk ON sp.MaSP = tk.MaSP
                        WHERE 1=1 ";

                    if (!string.IsNullOrWhiteSpace(txtThongTin.Text))
                        sql += " AND sp.TenSP LIKE @Keyword ";

                    sql += " GROUP BY sp.MaSP, sp.TenSP, lsp.TenLoaiSP, sp.GiaBan ";

                    if (rdoSapHet.Checked)
                        sql += " HAVING ISNULL(SUM(tk.SoLuongTon), 0) < 50 ";

                    SqlCommand cmd = new SqlCommand(sql, sqlCon);
                    if (!string.IsNullOrWhiteSpace(txtThongTin.Text))
                        cmd.Parameters.AddWithValue("@Keyword", "%" + txtThongTin.Text.Trim() + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvTonKho.DataSource = dt;

                    // Định dạng cột hiển thị trên lưới
                    if (dgvTonKho.Columns["MaSP"] != null) dgvTonKho.Columns["MaSP"].HeaderText = "Mã SP";
                    if (dgvTonKho.Columns["TenSP"] != null) dgvTonKho.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                    if (dgvTonKho.Columns["TenLoaiSP"] != null) dgvTonKho.Columns["TenLoaiSP"].HeaderText = "Loại Sản Phẩm";
                    if (dgvTonKho.Columns["SoLuongTon"] != null) dgvTonKho.Columns["SoLuongTon"].HeaderText = "Số Lượng Tồn";
                    if (dgvTonKho.Columns["GiaBan"] != null)
                    {
                        dgvTonKho.Columns["GiaBan"].HeaderText = "Giá Bán";
                        dgvTonKho.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                    }

                    // Tô màu đỏ cho hàng có số lượng dưới 50
                    foreach (DataGridViewRow row in dgvTonKho.Rows)
                    {
                        if (row.IsNewRow) continue;
                        if (int.TryParse(row.Cells["SoLuongTon"].Value?.ToString(), out int sl) && sl < 50)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightPink;
                            row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message); }
        }

        // --- SỰ KIỆN CLICK LƯỚI: HIỆN DỮ LIỆU LÊN Ô NHẬP ---
        private void dgvTonKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTonKho.Rows[e.RowIndex];

                // Gán vào các TextBox dựa trên Tên Cột đã định nghĩa ở LoadData
                txtMaSP.Text = row.Cells["MaSP"].Value?.ToString() ?? "";
                txtTenSP.Text = row.Cells["TenSP"].Value?.ToString() ?? "";
                txtSoLuong.Text = row.Cells["SoLuongTon"].Value?.ToString() ?? "";
                txtMaLoaiSP.Text = row.Cells["TenLoaiSP"].Value?.ToString() ?? "";

                if (decimal.TryParse(row.Cells["GiaBan"].Value?.ToString(), out decimal gia))
                    txtDonGia.Text = gia.ToString("N0");
                else
                    txtDonGia.Text = row.Cells["GiaBan"].Value?.ToString() ?? "";

                txtMaSP.Enabled = false; // Khóa mã khi đang chọn để sửa
            }
        }

        // --- CHỨC NĂNG THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSP.Text)) { MessageBox.Show("Vui lòng nhập Mã SP"); return; }
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    // Thêm vào bảng SanPham và khởi tạo tồn kho ở kho K01
                    string sql = @"
                        INSERT INTO SanPham (MaSP, TenSP, MaLoaiSP, GiaBan) VALUES (@Ma, @Ten, @Loai, @Gia);
                        INSERT INTO TonKho (MaKho, MaSP, SoLuongTon) VALUES ('K01', @Ma, @SL);";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaSP.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ten", txtTenSP.Text.Trim());
                    cmd.Parameters.AddWithValue("@Loai", txtMaLoaiSP.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gia", decimal.Parse(txtDonGia.Text.Replace(",", "")));
                    cmd.Parameters.AddWithValue("@SL", int.Parse(txtSoLuong.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // --- CHỨC NĂNG XUẤT EXCEL ---
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.Rows.Count == 0) return;
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "TonKho";

                for (int i = 0; i < dgvTonKho.Columns.Count; i++)
                    worksheet.Cells[1, i + 1] = dgvTonKho.Columns[i].HeaderText;

                for (int i = 0; i < dgvTonKho.Rows.Count; i++)
                {
                    if (dgvTonKho.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvTonKho.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = "'" + dgvTonKho.Rows[i].Cells[j].Value?.ToString();
                    }
                }
                worksheet.Columns.AutoFit();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xuất Excel: " + ex.Message); }
        }

        // --- LÀM MỚI ---
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Enabled = true;
            txtMaSP.Clear(); txtTenSP.Clear(); txtMaLoaiSP.Clear();
            txtSoLuong.Clear(); txtDonGia.Clear(); txtThongTin.Clear();
            rdoTatCa.Checked = true;
            LoadData();
        }

        // --- NÚT TÌM KIẾM ---
        private void btnTim_Click(object sender, EventArgs e) { LoadData(); }

        // --- XỬ LÝ RADIO BUTTON ---
        private void rdoSapHet_Click(object sender, EventArgs e) { LoadData(); }
        private void rdoTatCa_Click(object sender, EventArgs e) { LoadData(); }

        // --- ĐIỀU HƯỚNG CÁC HÀM PHỤ DO DESIGNER TẠO RA ---
        private void btnThem_Click_1(object sender, EventArgs e) { btnThem_Click(sender, e); }
        private void btnTim_Click_1(object sender, EventArgs e) { btnTim_Click(sender, e); }
        private void btnLamMoi_Click_1(object sender, EventArgs e) { btnLamMoi_Click(sender, e); }
    }
}