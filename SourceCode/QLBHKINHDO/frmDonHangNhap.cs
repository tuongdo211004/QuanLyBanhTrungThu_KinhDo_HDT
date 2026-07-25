using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Globalization;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmDonHangNhap : Form
    {
        // GIẢ ĐỊNH: Mã nhân viên lập phiếu nhập mặc định
        private const string DefaultMaNV = "NV01"; // Đổi thành NV01 cho chắc chắn tồn tại

        public frmDonHangNhap()
        {
            InitializeComponent();
        }

        // 2. FORM LOAD
        private void frmDonHangNhap_Load(object sender, EventArgs e)
        {
            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.CustomFormat = "dd/MM/yyyy";

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Chờ xử lý");
            cboTrangThai.Items.Add("Đã nhập kho");
            cboTrangThai.Items.Add("Hoàn thành");
            cboTrangThai.Items.Add("Đã hủy");
            cboTrangThai.SelectedIndex = 0;

            LoadData();
        }

        // --- CÁC HÀM HỖ TRỢ DỮ LIỆU ---
        private void LoadData()
        {
            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    // SỬ DỤNG BẢNG PhieuNhap
                    string sql = "SELECT MaPN, MaKho, MaNCC, MaNV, NgayNhap, TongTienNhap FROM PhieuNhap";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    // Đổi tên cột hiển thị
                    if (dgvKetQua.Columns["MaPN"] != null) dgvKetQua.Columns["MaPN"].HeaderText = "Mã Phiếu Nhập";
                    if (dgvKetQua.Columns["MaKho"] != null) dgvKetQua.Columns["MaKho"].HeaderText = "Mã Kho";
                    if (dgvKetQua.Columns["MaNCC"] != null) dgvKetQua.Columns["MaNCC"].HeaderText = "Mã NCC";
                    if (dgvKetQua.Columns["MaNV"] != null) dgvKetQua.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                    if (dgvKetQua.Columns["NgayNhap"] != null) dgvKetQua.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                    if (dgvKetQua.Columns["TongTienNhap"] != null)
                    {
                        dgvKetQua.Columns["TongTienNhap"].HeaderText = "Tổng Tiền";
                        dgvKetQua.Columns["TongTienNhap"].DefaultCellStyle.Format = "N0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Header: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm tải Chi tiết Phiếu nhập
        private DataTable GetChiTietDonNhap(string maPN)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    // SỬ DỤNG BẢNG ChiTietPhieuNhap
                    string sql = @"
                        SELECT 
                            T1.MaSP, T2.TenSP, T1.SoLuongNhap, T1.DonGiaNhap, T1.ThanhTienNhap
                        FROM ChiTietPhieuNhap T1 
                        JOIN SanPham T2 ON T1.MaSP = T2.MaSP 
                        WHERE T1.MaPN = @MaPN";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaPN", maPN);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        private void ResetValues()
        {
            txtMaDN.Clear();
            txtMaNCC.Clear();
            txtMaSP.Clear();
            txtSoLuong.Text = "0";
            txtDonGia.Text = "0";
            txtTongTien.Text = "0";
            cboTrangThai.SelectedIndex = 0;
            txtThongTin.Clear();
            dtpNgayNhap.Value = DateTime.Now;

            txtMaDN.Enabled = true;
            txtMaDN.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtMaDN.Text) || string.IsNullOrEmpty(txtMaNCC.Text) || string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Phiếu nhập, Mã NCC và Mã Sản phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtSoLuong.Text.Replace(",", "").Replace(".", ""), out _) ||
                !decimal.TryParse(txtDonGia.Text.Replace(",", "").Replace(".", ""), out _))
            {
                MessageBox.Show("Số lượng và Đơn giá phải là số hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void TinhTongTien()
        {
            string soLuongStr = txtSoLuong.Text.Replace(",", "").Replace(".", "");
            string donGiaStr = txtDonGia.Text.Replace(",", "").Replace(".", "");

            if (decimal.TryParse(soLuongStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal sl) &&
                decimal.TryParse(donGiaStr, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal dg))
            {
                txtTongTien.Text = (sl * dg).ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e) { TinhTongTien(); }
        private void txtDonGia_TextChanged(object sender, EventArgs e) { TinhTongTien(); }

        // --- SỰ KIỆN CLICK LƯỚI ---
        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvKetQua.Rows.Count || dgvKetQua.Rows[e.RowIndex].IsNewRow) return;

            DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];
            string maPN = row.Cells["MaPN"].Value?.ToString();
            string maNCC = row.Cells["MaNCC"].Value?.ToString();

            if (string.IsNullOrEmpty(maPN)) return;

            txtMaDN.Text = maPN;
            txtMaNCC.Text = maNCC;

            if (decimal.TryParse(row.Cells["TongTienNhap"].Value?.ToString(), out decimal tt))
                txtTongTien.Text = tt.ToString("N0");
            else
                txtTongTien.Text = "0";

            try
            {
                if (row.Cells["NgayNhap"].Value != null)
                    dtpNgayNhap.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
            }
            catch { dtpNgayNhap.Value = DateTime.Now; }

            // TẢI CHI TIẾT
            try
            {
                DataTable dtChiTiet = GetChiTietDonNhap(maPN);

                if (dtChiTiet.Rows.Count > 0)
                {
                    DataRow detailRow = dtChiTiet.Rows[0];
                    txtMaSP.Text = detailRow["MaSP"]?.ToString() ?? "";
                    txtSoLuong.Text = detailRow["SoLuongNhap"]?.ToString() ?? "0";

                    if (decimal.TryParse(detailRow["DonGiaNhap"]?.ToString(), out decimal dg))
                        txtDonGia.Text = dg.ToString("N0");
                }
                else
                {
                    txtMaSP.Clear(); txtSoLuong.Text = "0"; txtDonGia.Text = "0";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải chi tiết phiếu nhập: " + ex.Message); }

            txtMaDN.Enabled = false;
        }

        // --- CÁC NÚT CHỨC NĂNG ---

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                int soLuong = int.Parse(txtSoLuong.Text.Replace(",", "").Replace(".", ""));
                decimal donGia = decimal.Parse(txtDonGia.Text.Replace(",", "").Replace(".", ""));
                decimal tongTien = decimal.Parse(txtTongTien.Text.Replace(",", "").Replace(".", ""));
                string maPN = txtMaDN.Text.Trim();
                string maNCC = txtMaNCC.Text.Trim();
                string maSP = txtMaSP.Text.Trim();

                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    SqlTransaction transaction = sqlCon.BeginTransaction();

                    try
                    {
                        // 1. INSERT HEADER
                        // CSDL mới yêu cầu cột MaKho (gán cứng 'K01')
                        string sqlHeader = "INSERT INTO PhieuNhap (MaPN, MaKho, MaNCC, MaNV, NgayNhap, TongTienNhap) VALUES (@MaPN, 'K01', @MaNCC, @MaNV, @NgayNhap, @TongTien)";
                        SqlCommand cmdHeader = new SqlCommand(sqlHeader, sqlCon, transaction);
                        cmdHeader.Parameters.AddWithValue("@MaPN", maPN);
                        cmdHeader.Parameters.AddWithValue("@MaNCC", maNCC);
                        cmdHeader.Parameters.AddWithValue("@MaNV", DefaultMaNV);
                        cmdHeader.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                        cmdHeader.Parameters.AddWithValue("@TongTien", tongTien);
                        cmdHeader.ExecuteNonQuery();

                        // 2. INSERT DETAIL
                        string sqlDetail = "INSERT INTO ChiTietPhieuNhap (MaPN, MaSP, SoLuongNhap, DonGiaNhap, ThanhTienNhap) VALUES (@MaPN, @MaSP, @SoLuong, @DonGia, @ThanhTien)";
                        SqlCommand cmdDetail = new SqlCommand(sqlDetail, sqlCon, transaction);
                        cmdDetail.Parameters.AddWithValue("@MaPN", maPN);
                        cmdDetail.Parameters.AddWithValue("@MaSP", maSP);
                        cmdDetail.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdDetail.Parameters.AddWithValue("@DonGia", donGia);
                        cmdDetail.Parameters.AddWithValue("@ThanhTien", tongTien); // Thành tiền = SL * Giá
                        cmdDetail.ExecuteNonQuery();

                        // Cập nhật số lượng vào bảng TonKho
                        string sqlTonKho = @"
                            IF EXISTS (SELECT 1 FROM TonKho WHERE MaSP=@MaSP AND MaKho='K01')
                                UPDATE TonKho SET SoLuongTon = SoLuongTon + @SoLuong WHERE MaSP=@MaSP AND MaKho='K01';
                            ELSE
                                INSERT INTO TonKho (MaKho, MaSP, SoLuongTon) VALUES ('K01', @MaSP, @SoLuong);";
                        SqlCommand cmdTonKho = new SqlCommand(sqlTonKho, sqlCon, transaction);
                        cmdTonKho.Parameters.AddWithValue("@MaSP", maSP);
                        cmdTonKho.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdTonKho.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Thêm Phiếu Nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetValues();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDN.Text))
            {
                MessageBox.Show("Vui lòng chọn Phiếu Nhập cần sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            if (MessageBox.Show("Bạn có chắc muốn cập nhật phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection sqlCon = KetNoi.GetConnection())
                    {
                        sqlCon.Open();
                        SqlTransaction transaction = sqlCon.BeginTransaction();

                        try
                        {
                            int soLuong = int.Parse(txtSoLuong.Text.Replace(",", "").Replace(".", ""));
                            decimal donGia = decimal.Parse(txtDonGia.Text.Replace(",", "").Replace(".", ""));
                            decimal tongTien = decimal.Parse(txtTongTien.Text.Replace(",", "").Replace(".", ""));

                            // 1. UPDATE HEADER
                            string sqlHeader = "UPDATE PhieuNhap SET MaNCC=@MaNCC, NgayNhap=@NgayNhap, TongTienNhap=@TongTien WHERE MaPN=@MaPN";
                            SqlCommand cmdHeader = new SqlCommand(sqlHeader, sqlCon, transaction);
                            cmdHeader.Parameters.AddWithValue("@MaPN", txtMaDN.Text.Trim());
                            cmdHeader.Parameters.AddWithValue("@MaNCC", txtMaNCC.Text.Trim());
                            cmdHeader.Parameters.AddWithValue("@NgayNhap", dtpNgayNhap.Value);
                            cmdHeader.Parameters.AddWithValue("@TongTien", tongTien);
                            cmdHeader.ExecuteNonQuery();

                            // Lấy số lượng cũ để điều chỉnh lại Tồn Kho (Nghiệp vụ nâng cao)
                            // Để đơn giản cho đồ án, phần sửa này sẽ chỉ update Detail, bạn có thể tự xử lý cộng trừ tồn kho sau nếu muốn

                            // 2. UPDATE DETAIL
                            string sqlDetail = "UPDATE ChiTietPhieuNhap SET MaSP=@MaSP, SoLuongNhap=@SoLuong, DonGiaNhap=@DonGia, ThanhTienNhap=@ThanhTien WHERE MaPN=@MaPN";
                            SqlCommand cmdDetail = new SqlCommand(sqlDetail, sqlCon, transaction);
                            cmdDetail.Parameters.AddWithValue("@MaPN", txtMaDN.Text.Trim());
                            cmdDetail.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                            cmdDetail.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmdDetail.Parameters.AddWithValue("@DonGia", donGia);
                            cmdDetail.Parameters.AddWithValue("@ThanhTien", tongTien);
                            cmdDetail.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message); }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDN.Text))
            {
                MessageBox.Show("Vui lòng chọn Phiếu Nhập cần xóa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa Phiếu Nhập '{txtMaDN.Text}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection sqlCon = KetNoi.GetConnection())
                    {
                        sqlCon.Open();
                        SqlTransaction transaction = sqlCon.BeginTransaction();

                        try
                        {
                            // 1. XÓA CHI TIẾT
                            string sqlDetail = "DELETE FROM ChiTietPhieuNhap WHERE MaPN=@MaPN";
                            SqlCommand cmdDetail = new SqlCommand(sqlDetail, sqlCon, transaction);
                            cmdDetail.Parameters.AddWithValue("@MaPN", txtMaDN.Text);
                            cmdDetail.ExecuteNonQuery();

                            // 2. XÓA HEADER
                            string sqlHeader = "DELETE FROM PhieuNhap WHERE MaPN=@MaPN";
                            SqlCommand cmdHeader = new SqlCommand(sqlHeader, sqlCon, transaction);
                            cmdHeader.Parameters.AddWithValue("@MaPN", txtMaDN.Text);
                            cmdHeader.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message); }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtThongTin.Text == "")
            {
                LoadData();
                return;
            }

            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    string sql = "SELECT MaPN, MaKho, MaNCC, MaNV, NgayNhap, TongTienNhap FROM PhieuNhap WHERE MaPN LIKE @TuKhoa OR MaNCC LIKE @TuKhoa";
                    SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
                    da.SelectCommand.Parameters.AddWithValue("@TuKhoa", "%" + txtThongTin.Text + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    if (dt.Rows.Count > 0)
                        MessageBox.Show($"Tìm thấy {dt.Rows.Count} phiếu nhập.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Không tìm thấy dữ liệu nào.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadData();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadData();
            MessageBox.Show("Đã tải lại danh sách!", "Thông báo");
        }

        // --- XUẤT EXCEL ---
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            int rowCount = dgvKetQua.Rows.Count;
            if (rowCount > 0 && dgvKetQua.Rows[rowCount - 1].IsNewRow) rowCount--;

            if (rowCount == 0)
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
                worksheet.Name = "Phieu_Nhap";

                int cotCuoi = dgvKetQua.Columns.Count;
                int dongBatDau = 5;

                Excel.Range head = worksheet.Range["A1", "C1"];
                head.MergeCells = true;
                head.Value2 = "CÔNG TY CỔ PHẦN MONDELEZ KINH ĐÔ VIỆT NAM";
                head.Font.Bold = true;
                head.Font.Color = System.Drawing.Color.Red;

                Excel.Range title = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, cotCuoi]];
                title.MergeCells = true;
                title.Value2 = "DANH SÁCH PHIẾU NHẬP HÀNG";
                title.Font.Bold = true;
                title.Font.Size = 16;
                title.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                worksheet.Cells[3, 1] = "Người lập: Đỗ Trí Tường";
                worksheet.Cells[3, 3] = "Ngày lập: " + DateTime.Now.ToString("dd/MM/yyyy");

                // Header Cột
                for (int i = 0; i < dgvKetQua.Columns.Count; i++)
                {
                    worksheet.Cells[dongBatDau, i + 1] = dgvKetQua.Columns[i].HeaderText;
                    Excel.Range cell = (Excel.Range)worksheet.Cells[dongBatDau, i + 1];
                    cell.Font.Bold = true;
                    cell.Interior.Color = System.Drawing.Color.Yellow;
                    cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                // Dữ liệu
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < dgvKetQua.Columns.Count; j++)
                    {
                        if (dgvKetQua.Rows[i].Cells[j].Value != null)
                        {
                            string cellValue = dgvKetQua.Rows[i].Cells[j].Value.ToString();
                            if (dgvKetQua.Columns[j].Name == "NgayNhap")
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
            catch (Exception ex) { MessageBox.Show("Lỗi xuất Excel: " + ex.Message); }
        }
    }
}