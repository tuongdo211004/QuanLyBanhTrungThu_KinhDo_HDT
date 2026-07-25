using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmKhuyenMai : Form
    {
        public frmKhuyenMai()
        {
            InitializeComponent();
        }

        private void frmKhuyenMai_Load(object sender, EventArgs e)
        {
            // Định dạng hiển thị ngày tháng
            dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
            dtpNgayBatDau.CustomFormat = "dd/MM/yyyy";
            dtpNgayKetThuc.Format = DateTimePickerFormat.Custom;
            dtpNgayKetThuc.CustomFormat = "dd/MM/yyyy";

            LoadData();
            rdoTenVoucher.Checked = true;
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    // ĐÃ SỬA THÀNH BẢNG MỚI: ChuongTrinhKhuyenMai
                    string sql = "SELECT * FROM ChuongTrinhKhuyenMai";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    // Đổi tên cột hiển thị theo CSDL mới
                    if (dgvKetQua.Columns["MaCTKM"] != null) dgvKetQua.Columns["MaCTKM"].HeaderText = "Mã CTKM";
                    if (dgvKetQua.Columns["TenCTKM"] != null) dgvKetQua.Columns["TenCTKM"].HeaderText = "Tên CTKM";
                    if (dgvKetQua.Columns["NgayBatDau"] != null) dgvKetQua.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                    if (dgvKetQua.Columns["NgayKetThuc"] != null) dgvKetQua.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
                    if (dgvKetQua.Columns["MoTaCTKM"] != null) dgvKetQua.Columns["MoTaCTKM"].HeaderText = "Mô Tả";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetValues()
        {
            txtMaVoucher.Clear();
            txtTenVoucher.Clear();
            txtGiaTriGiam.Clear(); // Giữ lại trên giao diện nhưng bỏ qua không lưu
            // Giữ lại trên giao diện nhưng bỏ qua không lưu
            txtTrangThai.Clear();  // Dùng ô này để nhập dữ liệu cho cột Mô tả
            txtThongTin.Clear();

            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;

            txtMaVoucher.Enabled = true;
            txtMaVoucher.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtMaVoucher.Text) || string.IsNullOrEmpty(txtTenVoucher.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã và Tên chương trình khuyến mãi!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpNgayKetThuc.Value.Date < dtpNgayBatDau.Value.Date)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn Ngày bắt đầu!", "Lỗi Logic", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpNgayKetThuc.Focus();
                return false;
            }

            return true;
        }

        // --- SỰ KIỆN CELL CLICK ---
        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                // Đọc dữ liệu từ các cột mới
                txtMaVoucher.Text = row.Cells["MaCTKM"].Value?.ToString() ?? "";
                txtTenVoucher.Text = row.Cells["TenCTKM"].Value?.ToString() ?? "";
                txtTrangThai.Text = row.Cells["MoTaCTKM"].Value?.ToString() ?? "";

                try
                {
                    if (row.Cells["NgayBatDau"].Value != DBNull.Value)
                        dtpNgayBatDau.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);

                    if (row.Cells["NgayKetThuc"].Value != DBNull.Value)
                        dtpNgayKetThuc.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);
                }
                catch { }

                txtMaVoucher.Enabled = false; // Ngăn sửa mã
            }
        }

        // --- NÚT THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    // SỬA CÂU LỆNH INSERT CHO ĐÚNG BẢNG MỚI
                    string sql = "INSERT INTO ChuongTrinhKhuyenMai (MaCTKM, TenCTKM, NgayBatDau, NgayKetThuc, MoTaCTKM) VALUES (@Ma, @Ten, @BD, @KT, @MoTa)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaVoucher.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ten", txtTenVoucher.Text.Trim());
                    cmd.Parameters.AddWithValue("@BD", dtpNgayBatDau.Value);
                    cmd.Parameters.AddWithValue("@KT", dtpNgayKetThuc.Value);
                    cmd.Parameters.AddWithValue("@MoTa", txtTrangThai.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm Khuyến mãi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm (Có thể trùng mã): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- NÚT SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVoucher.Text))
            {
                MessageBox.Show("Vui lòng chọn CTKM cần sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            if (MessageBox.Show("Cập nhật khuyến mãi này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        // SỬA CÂU LỆNH UPDATE CHO ĐÚNG BẢNG MỚI
                        string sql = "UPDATE ChuongTrinhKhuyenMai SET TenCTKM=@Ten, NgayBatDau=@BD, NgayKetThuc=@KT, MoTaCTKM=@MoTa WHERE MaCTKM=@Ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ten", txtTenVoucher.Text.Trim());
                        cmd.Parameters.AddWithValue("@BD", dtpNgayBatDau.Value);
                        cmd.Parameters.AddWithValue("@KT", dtpNgayKetThuc.Value);
                        cmd.Parameters.AddWithValue("@MoTa", txtTrangThai.Text.Trim());
                        cmd.Parameters.AddWithValue("@Ma", txtMaVoucher.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        else MessageBox.Show("Cập nhật thất bại (Không tìm thấy Mã này).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // --- NÚT XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaVoucher.Text))
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần xóa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn chắc chắn muốn xóa '{txtMaVoucher.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        // SỬA CÂU LỆNH DELETE CHO ĐÚNG BẢNG MỚI
                        string sql = "DELETE FROM ChuongTrinhKhuyenMai WHERE MaCTKM=@Ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ma", txtMaVoucher.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        else MessageBox.Show("Xóa thất bại (Mã không tồn tại).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa (Mã này đang được dùng trong Phạm Vi Áp Dụng): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // --- NÚT TÌM KIẾM ---
        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtThongTin.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) { LoadData(); return; }

            string field = rdoMaVoucher.Checked ? "MaCTKM" : "TenCTKM";

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    // SỬA CÂU LỆNH TÌM KIẾM CHO ĐÚNG BẢNG MỚI
                    string sql = $"SELECT * FROM ChuongTrinhKhuyenMai WHERE {field} LIKE @Keyword";
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

        // --- NÚT DANH SÁCH / TẢI LẠI ---
        private void btnDanhSach_Click(object sender, EventArgs e) { ResetValues(); LoadData(); }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadData();
            MessageBox.Show("Đã tải lại toàn bộ danh sách Khuyến mãi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- XUẤT EXCEL ---
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            string tenBaoCao = "DANH MỤC CHƯƠNG TRÌNH KHUYẾN MÃI";
            if (dgvKetQua.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "DS_Khuyen_Mai";

                int cotCuoi = dgvKetQua.Columns.Count;
                int dongBatDau = 5;

                Excel.Range head = worksheet.Range["A1", "C1"];
                head.MergeCells = true;
                head.Value2 = "CÔNG TY CỔ PHẦN MONDELEZ KINH ĐÔ VIỆT NAM";
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
                            string cellValue = dgvKetQua.Rows[i].Cells[j].Value.ToString();
                            if (dgvKetQua.Columns[j].Name == "NgayBatDau" || dgvKetQua.Columns[j].Name == "NgayKetThuc")
                            {
                                if (DateTime.TryParse(cellValue, out DateTime d)) cellValue = d.ToString("dd/MM/yyyy");
                            }
                            worksheet.Cells[dongBatDau + 1 + i, j + 1] = "'" + cellValue;
                            ((Excel.Range)worksheet.Cells[dongBatDau + 1 + i, j + 1]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                        }
                    }
                }
                worksheet.Columns.AutoFit();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvKetQua_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtTrangThai_TextChanged(object sender, EventArgs e) { }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}