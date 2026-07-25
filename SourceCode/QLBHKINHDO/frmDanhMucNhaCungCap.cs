using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmDanhMucNhaCungCap : Form
    {
        public frmDanhMucNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmDanhMucNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
            rdoTenNCC.Checked = true;
        }

        // =======================================================
        // PHẦN 1: LOGIC CODE (ĐÃ CẬP NHẬT CƠ SỞ DỮ LIỆU MỚI)
        // =======================================================

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM NhaCungCap";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    // Đặt tên tiêu đề cột theo đúng CSDL mới
                    if (dgvKetQua.Columns["MaNCC"] != null) dgvKetQua.Columns["MaNCC"].HeaderText = "Mã NCC";
                    if (dgvKetQua.Columns["TenNCC"] != null) dgvKetQua.Columns["TenNCC"].HeaderText = "Tên Nhà Cung Cấp";
                    if (dgvKetQua.Columns["DiaChiNCC"] != null) dgvKetQua.Columns["DiaChiNCC"].HeaderText = "Địa Chỉ";
                    if (dgvKetQua.Columns["SDTNCC"] != null) dgvKetQua.Columns["SDTNCC"].HeaderText = "Số Điện Thoại";
                    // CSDL mới không còn cột Email nên bỏ qua
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ResetValues()
        {
            txtMaNCC.Clear();
            txtTenNCC.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear(); // Giao diện còn thì cứ clear, không ảnh hưởng
            txtThongTin.Clear();

            txtMaNCC.Enabled = true;
            txtMaNCC.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text) || string.IsNullOrWhiteSpace(txtTenNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên nhà cung cấp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // --- SỰ KIỆN CLICK LƯỚI ---
        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                txtMaNCC.Text = row.Cells["MaNCC"].Value?.ToString() ?? "";
                txtTenNCC.Text = row.Cells["TenNCC"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChiNCC"].Value?.ToString() ?? "";
                txtSDT.Text = row.Cells["SDTNCC"].Value?.ToString() ?? "";

                txtEmail.Text = ""; // Email không còn lưu DB, gán rỗng
                txtMaNCC.Enabled = false; // Khóa không cho sửa Mã 
            }
        }

        // --- THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChiNCC, SDTNCC) VALUES (@Ma, @Ten, @DC, @SDT)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaNCC.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ten", txtTenNCC.Text.Trim());
                    cmd.Parameters.AddWithValue("@DC", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm (Có thể trùng Mã NCC): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE NhaCungCap SET TenNCC=@Ten, DiaChiNCC=@DC, SDTNCC=@SDT WHERE MaNCC=@Ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ten", txtTenNCC.Text.Trim());
                    cmd.Parameters.AddWithValue("@DC", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ma", txtMaNCC.Text.Trim());

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetValues();
                        LoadData();
                    }
                    else MessageBox.Show("Không tìm thấy Mã NCC để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNCC.Text))
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn chắc chắn muốn xóa Nhà cung cấp '{txtTenNCC.Text}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        string sql = "DELETE FROM NhaCungCap WHERE MaNCC=@Ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ma", txtMaNCC.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        else MessageBox.Show("Không tìm thấy Mã NCC để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa (Có thể do NCC này đã có Phiếu Nhập ràng buộc): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // --- TÌM KIẾM ---
        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtThongTin.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string field = rdoMaNCC.Checked ? "MaNCC" : "TenNCC";
                    string sql = $"SELECT * FROM NhaCungCap WHERE {field} LIKE @Keyword";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    if (dt.Rows.Count == 0) MessageBox.Show("Không tìm thấy kết quả nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- DANH SÁCH / TẢI LẠI ---
        private void btnDanhsach_Click(object sender, EventArgs e) { ResetValues(); LoadData(); }
        private void btnTaiLai_Click(object sender, EventArgs e) { ResetValues(); LoadData(); MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        // --- XUẤT EXCEL ---
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            string tenBaoCao = "DANH MỤC NHÀ CUNG CẤP";
            if (dgvKetQua.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "DS_NCC";

                int cotCuoi = dgvKetQua.Columns.Count;
                int dongBatDau = 5;

                Excel.Range head = worksheet.Range["A1", "C1"];
                head.MergeCells = true;
                head.Value2 = "DOANH NGHIỆP KINH ĐÔ";
                head.Font.Bold = true;
                head.Font.Color = System.Drawing.Color.Red;

                Excel.Range title = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, cotCuoi]];
                title.MergeCells = true;
                title.Value2 = tenBaoCao;
                title.Font.Bold = true;
                title.Font.Size = 16;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[3, 1] = "Người lập: Đỗ Trí Tường";
                worksheet.Cells[3, 3] = "Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy");

                for (int i = 0; i < dgvKetQua.Columns.Count; i++)
                {
                    worksheet.Cells[dongBatDau, i + 1] = dgvKetQua.Columns[i].HeaderText;
                    Excel.Range cell = (Excel.Range)worksheet.Cells[dongBatDau, i + 1];
                    cell.Font.Bold = true;
                    cell.Interior.Color = System.Drawing.Color.Yellow;
                    cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                for (int i = 0; i < dgvKetQua.Rows.Count; i++)
                {
                    if (dgvKetQua.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvKetQua.Columns.Count; j++)
                    {
                        if (dgvKetQua.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[dongBatDau + 1 + i, j + 1] = "'" + dgvKetQua.Rows[i].Cells[j].Value.ToString();
                            ((Excel.Range)worksheet.Cells[dongBatDau + 1 + i, j + 1]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        }
                    }
                }
                worksheet.Columns.AutoFit();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}