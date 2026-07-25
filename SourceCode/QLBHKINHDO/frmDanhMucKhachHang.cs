using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmDanhMucKhachHang : Form
    {
        public frmDanhMucKhachHang()
        {
            InitializeComponent();
        }

        private void frmDanhMucKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
            rdoTenKhachHang.Checked = true;
        }

        // --- HÀM TẢI DỮ LIỆU ---
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM KhachHang";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    // Đổi tên Header hiển thị cho khớp với CSDL của bạn
                    if (dgvKetQua.Columns["MaKH"] != null) dgvKetQua.Columns["MaKH"].HeaderText = "Mã KH";
                    if (dgvKetQua.Columns["TenKH"] != null) dgvKetQua.Columns["TenKH"].HeaderText = "Tên Khách Hàng";
                    if (dgvKetQua.Columns["SDTKH"] != null) dgvKetQua.Columns["SDTKH"].HeaderText = "Số Điện Thoại";
                    if (dgvKetQua.Columns["DiaChiKH"] != null) dgvKetQua.Columns["DiaChiKH"].HeaderText = "Địa Chỉ";
                    if (dgvKetQua.Columns["EmailKH"] != null) dgvKetQua.Columns["EmailKH"].HeaderText = "Email";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message); }
        }

        // --- SỰ KIỆN CLICK LƯỚI ĐỂ HIỆN DỮ LIỆU LÊN Ô NHẬP ---
        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                // Gán dữ liệu vào các TextBox dựa trên TÊN CỘT trong SQL
                txtMaKhachHang.Text = row.Cells["MaKH"].Value?.ToString() ?? "";
                txtTenKhachHang.Text = row.Cells["TenKH"].Value?.ToString() ?? "";
                txtSoDienThoai.Text = row.Cells["SDTKH"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChiKH"].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells["EmailKH"].Value?.ToString() ?? "";

                txtMaKhachHang.Enabled = false; // Khóa mã khi chọn để sửa
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhachHang.Text)) { MessageBox.Show("Vui lòng nhập Mã KH!"); return; }
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO KhachHang (MaKH, TenKH, DiaChiKH, SDTKH, EmailKH) VALUES (@Ma, @Ten, @DC, @SDT, @Email)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaKhachHang.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ten", txtTenKhachHang.Text.Trim());
                    cmd.Parameters.AddWithValue("@DC", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SDT", txtSoDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công!");
                    ResetValues(); LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE KhachHang SET TenKH=@Ten, DiaChiKH=@DC, SDTKH=@SDT, EmailKH=@Email WHERE MaKH=@Ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ten", txtTenKhachHang.Text.Trim());
                    cmd.Parameters.AddWithValue("@DC", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SDT", txtSoDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ma", txtMaKhachHang.Text.Trim());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thông tin khách hàng thành công!");
                    ResetValues(); LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        string sql = "DELETE FROM KhachHang WHERE MaKH=@Ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ma", txtMaKhachHang.Text.Trim());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa khách hàng!");
                        ResetValues(); LoadData();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string field = rdoMaKhachHang.Checked ? "MaKH" : "TenKH";
                    string sql = $"SELECT * FROM KhachHang WHERE {field} LIKE @Key OR SDTKH LIKE @Key";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@Key", "%" + txtThongTin.Text.Trim() + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        private void ResetValues()
        {
            txtMaKhachHang.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtThongTin.Clear();
            txtMaKhachHang.Enabled = true;
            txtMaKhachHang.Focus();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.Rows.Count == 0) return;
            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                for (int i = 0; i < dgvKetQua.Columns.Count; i++)
                    worksheet.Cells[1, i + 1] = dgvKetQua.Columns[i].HeaderText;

                for (int i = 0; i < dgvKetQua.Rows.Count; i++)
                {
                    if (dgvKetQua.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvKetQua.Columns.Count; j++)
                        worksheet.Cells[i + 2, j + 1] = "'" + dgvKetQua.Rows[i].Cells[j].Value?.ToString();
                }
                worksheet.Columns.AutoFit();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xuất Excel: " + ex.Message); }
        }

        // --- ĐIỀU HƯỚNG CÁC HÀM PHỤ DO DESIGNER TẠO RA ---
        private void btnDanhsach_Click(object sender, EventArgs e) { ResetValues(); LoadData(); }
        private void btnTaiLai_Click(object sender, EventArgs e) { btnDanhsach_Click(sender, e); }
        private void btnXuatExcel_Click_1(object sender, EventArgs e) { btnXuatExcel_Click(sender, e); }
        private void btnXoa_Click_1(object sender, EventArgs e) { btnXoa_Click(sender, e); }
        private void btnDanhsach_Click_1(object sender, EventArgs e) { btnDanhsach_Click(sender, e); }
        private void btnTaiLai_Click_1(object sender, EventArgs e) { btnTaiLai_Click(sender, e); }
        private void dgvKetQua_CellContentClick(object sender, DataGridViewCellEventArgs e) { dgvKetQua_CellClick(sender, e); }
    }
}