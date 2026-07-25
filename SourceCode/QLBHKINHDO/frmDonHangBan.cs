using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace QLBHKINHDO
{
    public partial class frmDonHangBan : Form
    {
        public frmDonHangBan()
        {
            InitializeComponent();
        }

        private void frmDonHangBan_Load(object sender, EventArgs e)
        {
            dtpNgayDat.Format = DateTimePickerFormat.Custom;
            dtpNgayDat.CustomFormat = "dd/MM/yyyy";

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Chờ xử lý");
            cboTrangThai.Items.Add("Đang giao");
            cboTrangThai.Items.Add("Đã hoàn thành");
            cboTrangThai.Items.Add("Đã hủy");
            cboTrangThai.SelectedIndex = 0;

            LoadData();
        }

        // --- HÀM HỖ TRỢ ---

        private void LoadData()
        {
            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    string sql = "SELECT MaHD, MaKH, MaNV, NgayLapHD, TongTien, TrangThai FROM HoaDon";
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    if (dgvKetQua.Columns["MaHD"] != null) dgvKetQua.Columns["MaHD"].HeaderText = "Mã Hóa Đơn";
                    if (dgvKetQua.Columns["MaKH"] != null) dgvKetQua.Columns["MaKH"].HeaderText = "Mã Khách Hàng";
                    if (dgvKetQua.Columns["MaNV"] != null) dgvKetQua.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
                    if (dgvKetQua.Columns["NgayLapHD"] != null) dgvKetQua.Columns["NgayLapHD"].HeaderText = "Ngày Lập";
                    if (dgvKetQua.Columns["TongTien"] != null)
                    {
                        dgvKetQua.Columns["TongTien"].HeaderText = "Tổng Tiền";
                        dgvKetQua.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    }
                    if (dgvKetQua.Columns["TrangThai"] != null) dgvKetQua.Columns["TrangThai"].HeaderText = "Trạng Thái";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetChiTietDonHang(string maHD)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    string sql = @"SELECT T1.MaSP, T2.TenSP, T1.SoLuong, T1.DonGia, T1.ThanhTien 
                                   FROM ChiTietHoaDon T1 
                                   JOIN SanPham T2 ON T1.MaSP = T2.MaSP 
                                   WHERE T1.MaHD = @MaHD";

                    SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
                    adapter.SelectCommand.Parameters.AddWithValue("@MaHD", maHD);
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
            txtMaDH.Clear();
            txtMaKH.Clear();
            txtMaSP.Clear();
            txtSoLuong.Text = "0";
            txtDonGia.Text = "0";
            txtTongTien.Text = "0";
            cboTrangThai.SelectedIndex = 0;
            txtThongTin.Clear();
            dtpNgayDat.Value = DateTime.Now;
            txtMaDH.Enabled = true;
            txtMaDH.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtMaDH.Text) || string.IsNullOrEmpty(txtMaKH.Text) || string.IsNullOrEmpty(txtMaSP.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Mã HĐ, Mã KH, Mã SP)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtSoLuong_TextChanged_1(object sender, EventArgs e) { TinhTongTien(); }
        private void txtSoLuong_TextChanged(object sender, EventArgs e) { TinhTongTien(); }
        private void txtDonGia_TextChanged(object sender, EventArgs e) { TinhTongTien(); }

        // --- XỬ LÝ CLICK LƯỚI ---
        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvKetQua.Rows.Count && !dgvKetQua.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                string maHD = row.Cells["MaHD"].Value?.ToString();

                txtMaDH.Text = maHD;
                txtMaKH.Text = row.Cells["MaKH"].Value?.ToString() ?? "";

                if (decimal.TryParse(row.Cells["TongTien"].Value?.ToString(), out decimal tt))
                    txtTongTien.Text = tt.ToString("N0");

                string status = row.Cells["TrangThai"].Value?.ToString() ?? "";
                if (cboTrangThai.Items.Contains(status)) cboTrangThai.SelectedItem = status;
                else cboTrangThai.Text = status;

                try { dtpNgayDat.Value = Convert.ToDateTime(row.Cells["NgayLapHD"].Value); }
                catch { dtpNgayDat.Value = DateTime.Now; }

                DataTable dtChiTiet = GetChiTietDonHang(maHD);
                if (dtChiTiet.Rows.Count > 0)
                {
                    txtMaSP.Text = dtChiTiet.Rows[0]["MaSP"].ToString();
                    txtSoLuong.Text = dtChiTiet.Rows[0]["SoLuong"].ToString();
                    if (decimal.TryParse(dtChiTiet.Rows[0]["DonGia"].ToString(), out decimal dg))
                        txtDonGia.Text = dg.ToString("N0");
                }
                else
                {
                    txtMaSP.Clear(); txtSoLuong.Text = "0"; txtDonGia.Text = "0";
                }

                txtMaDH.Enabled = false; // Khóa mã khi xem
            }
        }

        // --- NÚT THÊM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

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
                        decimal thanhTienCT = soLuong * donGia;

                        string sqlHeader = "INSERT INTO HoaDon (MaHD, MaKH, MaNV, NgayLapHD, TongTien, TrangThai) VALUES (@MaHD, @MaKH, 'NV01', @NgayLapHD, @TongTien, @TrangThai)";
                        SqlCommand cmdHeader = new SqlCommand(sqlHeader, sqlCon, transaction);
                        cmdHeader.Parameters.AddWithValue("@MaHD", txtMaDH.Text.Trim());
                        cmdHeader.Parameters.AddWithValue("@MaKH", txtMaKH.Text.Trim());
                        cmdHeader.Parameters.AddWithValue("@NgayLapHD", dtpNgayDat.Value);
                        cmdHeader.Parameters.AddWithValue("@TongTien", tongTien);
                        cmdHeader.Parameters.AddWithValue("@TrangThai", cboTrangThai.Text);
                        cmdHeader.ExecuteNonQuery();

                        string sqlDetail = "INSERT INTO ChiTietHoaDon (MaHD, MaSP, SoLuong, DonGia, ThanhTien) VALUES (@MaHD, @MaSP, @SoLuong, @DonGia, @ThanhTien)";
                        SqlCommand cmdDetail = new SqlCommand(sqlDetail, sqlCon, transaction);
                        cmdDetail.Parameters.AddWithValue("@MaHD", txtMaDH.Text.Trim());
                        cmdDetail.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                        cmdDetail.Parameters.AddWithValue("@SoLuong", soLuong);
                        cmdDetail.Parameters.AddWithValue("@DonGia", donGia);
                        cmdDetail.Parameters.AddWithValue("@ThanhTien", thanhTienCT);
                        cmdDetail.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
        }

        // --- NÚT SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDH.Text)) return;
            if (!ValidateInputs()) return;

            if (MessageBox.Show("Cập nhật hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                            decimal thanhTienCT = soLuong * donGia;

                            string sqlHeader = "UPDATE HoaDon SET MaKH=@MaKH, TongTien=@TongTien, NgayLapHD=@NgayLapHD, TrangThai=@TrangThai WHERE MaHD=@MaHD";
                            SqlCommand cmdHeader = new SqlCommand(sqlHeader, sqlCon, transaction);
                            cmdHeader.Parameters.AddWithValue("@MaHD", txtMaDH.Text.Trim());
                            cmdHeader.Parameters.AddWithValue("@MaKH", txtMaKH.Text.Trim());
                            cmdHeader.Parameters.AddWithValue("@NgayLapHD", dtpNgayDat.Value);
                            cmdHeader.Parameters.AddWithValue("@TongTien", tongTien);
                            cmdHeader.Parameters.AddWithValue("@TrangThai", cboTrangThai.Text);
                            cmdHeader.ExecuteNonQuery();

                            string sqlDetail = "UPDATE ChiTietHoaDon SET MaSP=@MaSP, SoLuong=@SoLuong, DonGia=@DonGia, ThanhTien=@ThanhTien WHERE MaHD=@MaHD";
                            SqlCommand cmdDetail = new SqlCommand(sqlDetail, sqlCon, transaction);
                            cmdDetail.Parameters.AddWithValue("@MaHD", txtMaDH.Text.Trim());
                            cmdDetail.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                            cmdDetail.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmdDetail.Parameters.AddWithValue("@DonGia", donGia);
                            cmdDetail.Parameters.AddWithValue("@ThanhTien", thanhTienCT);
                            cmdDetail.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi cập nhật: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
            }
        }

        // --- NÚT XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaDH.Text)) return;

            if (MessageBox.Show("Xóa hóa đơn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection sqlCon = KetNoi.GetConnection())
                    {
                        sqlCon.Open();
                        SqlTransaction transaction = sqlCon.BeginTransaction();

                        try
                        {
                            SqlCommand cmdDetail = new SqlCommand("DELETE FROM ChiTietHoaDon WHERE MaHD=@MaHD", sqlCon, transaction);
                            cmdDetail.Parameters.AddWithValue("@MaHD", txtMaDH.Text);
                            cmdDetail.ExecuteNonQuery();

                            SqlCommand cmdHeader = new SqlCommand("DELETE FROM HoaDon WHERE MaHD=@MaHD", sqlCon, transaction);
                            cmdHeader.Parameters.AddWithValue("@MaHD", txtMaDH.Text);
                            cmdHeader.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi xóa: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi kết nối: " + ex.Message); }
            }
        }

        // --- TÌM KIẾM ---
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtThongTin.Text == "") { LoadData(); return; }

            try
            {
                using (SqlConnection sqlCon = KetNoi.GetConnection())
                {
                    sqlCon.Open();
                    string sql = "SELECT MaHD, MaKH, MaNV, NgayLapHD, TongTien, TrangThai FROM HoaDon WHERE MaHD LIKE @TuKhoa OR MaKH LIKE @TuKhoa";
                    SqlDataAdapter da = new SqlDataAdapter(sql, sqlCon);
                    da.SelectCommand.Parameters.AddWithValue("@TuKhoa", "%" + txtThongTin.Text + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        private void btnDanhSach_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadData();
        }

        // --- XUẤT EXCEL ---
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.Rows.Count == 0) return;

            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                // Code vẽ bảng Excel
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Excel: " + ex.Message); }
        }

        // --- IN HÓA ĐƠN ---
        private void button1_Click(object sender, EventArgs e)
        {
            string maHD = txtMaDH.Text.Trim();

            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng từ danh sách để in hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Gọi Form và truyền mã
            frmXuatHoaDon frm = new frmXuatHoaDon(maHD);
            frm.ShowDialog();
        }

        // Các sự kiện UI rỗng (nếu bạn lỡ click đúp tạo ra thì cứ để đây để khỏi báo lỗi Designer)
        private void btnXuatHoaDon_Click(object sender, EventArgs e) { }
        private void txtMaDH_TextChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
    }
}