using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmDanhMucSanPham : Form
    {
        public frmDanhMucSanPham()
        {
            InitializeComponent();
        }

        private void frmDanhMucSanPham_Load(object sender, EventArgs e)
        {
            LoadLoaiSanPham();
            LoadData();
            rdoTenSanPham.Checked = true;
        }

        // =======================================================
        // PHẦN 1: LOGIC CODE (ĐÃ CẬP NHẬT JOIN CSDL MỚI)
        // =======================================================

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        SELECT sp.MaSP, sp.TenSP, sp.MaLoaiSP, lsp.TenLoaiSP, 
                               ISNULL(SUM(tk.SoLuongTon), 0) AS SoLuongTon, sp.GiaBan 
                        FROM SanPham sp
                        LEFT JOIN LoaiSanPham lsp ON sp.MaLoaiSP = lsp.MaLoaiSP
                        LEFT JOIN TonKho tk ON sp.MaSP = tk.MaSP
                        GROUP BY sp.MaSP, sp.TenSP, sp.MaLoaiSP, lsp.TenLoaiSP, sp.GiaBan";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    if (dgvKetQua.Columns["MaSP"] != null) dgvKetQua.Columns["MaSP"].HeaderText = "Mã SP";
                    if (dgvKetQua.Columns["TenSP"] != null) dgvKetQua.Columns["TenSP"].HeaderText = "Tên Sản Phẩm";
                    if (dgvKetQua.Columns["TenLoaiSP"] != null) dgvKetQua.Columns["TenLoaiSP"].HeaderText = "Loại Sản Phẩm";
                    if (dgvKetQua.Columns["SoLuongTon"] != null) dgvKetQua.Columns["SoLuongTon"].HeaderText = "Tổng Tồn Kho";
                    if (dgvKetQua.Columns["GiaBan"] != null)
                    {
                        dgvKetQua.Columns["GiaBan"].HeaderText = "Giá Bán";
                        dgvKetQua.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                    }

                    if (dgvKetQua.Columns["MaLoaiSP"] != null) dgvKetQua.Columns["MaLoaiSP"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void LoadLoaiSanPham()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT MaLoaiSP, TenLoaiSP FROM LoaiSanPham";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboLoaiSanPham.DataSource = dt;
                    cboLoaiSanPham.DisplayMember = "TenLoaiSP";
                    cboLoaiSanPham.ValueMember = "MaLoaiSP";
                    cboLoaiSanPham.SelectedIndex = -1;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nạp danh mục Loại SP: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ResetValues()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            cboLoaiSanPham.SelectedIndex = -1;
            txtSoLuong.Clear();
            txtGiaBan.Clear();
            txtThongTin.Clear();
            txtMaSanPham.Enabled = true;
            txtMaSanPham.Focus();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtMaSanPham.Text) || string.IsNullOrEmpty(txtTenSanPham.Text) ||
                string.IsNullOrEmpty(txtSoLuong.Text) || string.IsNullOrEmpty(txtGiaBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã, Tên, Số lượng và Giá bán.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Replace(",", "").Replace(".", ""), out _))
            {
                MessageBox.Show("Giá bán phải là số hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return false;
            }

            if (!int.TryParse(txtSoLuong.Text, out _))
            {
                MessageBox.Show("Số lượng phải là số nguyên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return false;
            }
            return true;
        }

        private void dgvKetQua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKetQua.Rows[e.RowIndex];

                txtMaSanPham.Text = row.Cells["MaSP"].Value?.ToString() ?? "";
                txtTenSanPham.Text = row.Cells["TenSP"].Value?.ToString() ?? "";
                txtSoLuong.Text = row.Cells["SoLuongTon"].Value?.ToString() ?? "";

                if (decimal.TryParse(row.Cells["GiaBan"].Value?.ToString(), out decimal gia))
                    txtGiaBan.Text = gia.ToString("N0");
                else
                    txtGiaBan.Text = row.Cells["GiaBan"].Value?.ToString() ?? "";

                string maLoaiSP = row.Cells["MaLoaiSP"].Value?.ToString() ?? "";
                cboLoaiSanPham.SelectedValue = maLoaiSP;

                txtMaSanPham.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string maLoai = cboLoaiSanPham.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maLoai))
            {
                MessageBox.Show("Vui lòng chọn Loại Sản Phẩm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO SanPham (MaSP, TenSP, MaLoaiSP, DonViTinh, GiaBan) 
                        VALUES (@Ma, @Ten, @Loai, N'Hộp', @Gia);
                        
                        INSERT INTO TonKho (MaKho, MaSP, SoLuongTon) 
                        VALUES ('K01', @Ma, @SL);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaSanPham.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ten", txtTenSanPham.Text.Trim());
                    cmd.Parameters.AddWithValue("@Loai", maLoai);
                    cmd.Parameters.AddWithValue("@SL", int.Parse(txtSoLuong.Text));
                    cmd.Parameters.AddWithValue("@Gia", decimal.Parse(txtGiaBan.Text.Replace(",", "").Replace(".", "")));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm (Có thể trùng Mã SP): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            string maLoai = cboLoaiSanPham.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maLoai)) { MessageBox.Show("Vui lòng chọn Loại Sản Phẩm!", "Cảnh báo"); return; }

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = @"
                        UPDATE SanPham SET TenSP=@Ten, MaLoaiSP=@Loai, GiaBan=@Gia WHERE MaSP=@Ma;
                        
                        IF EXISTS (SELECT 1 FROM TonKho WHERE MaSP=@Ma AND MaKho='K01')
                            UPDATE TonKho SET SoLuongTon=@SL WHERE MaSP=@Ma AND MaKho='K01';
                        ELSE
                            INSERT INTO TonKho (MaKho, MaSP, SoLuongTon) VALUES ('K01', @Ma, @SL);";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ten", txtTenSanPham.Text.Trim());
                    cmd.Parameters.AddWithValue("@Loai", maLoai);
                    cmd.Parameters.AddWithValue("@SL", int.Parse(txtSoLuong.Text));
                    cmd.Parameters.AddWithValue("@Gia", decimal.Parse(txtGiaBan.Text.Replace(",", "").Replace(".", "")));
                    cmd.Parameters.AddWithValue("@Ma", txtMaSanPham.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSanPham.Text)) { MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!"); return; }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm '{txtTenSanPham.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        string sql = @"
                            DELETE FROM TonKho WHERE MaSP=@Ma;
                            DELETE FROM SanPham WHERE MaSP=@Ma;";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ma", txtMaSanPham.Text.Trim());

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo");
                            ResetValues();
                            LoadData();
                        }
                        else MessageBox.Show("Không tìm thấy Mã SP để xóa!", "Lỗi");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa (Có thể SP này đang nằm trong Hóa Đơn/Phiếu Nhập): " + ex.Message, "Lỗi"); }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string keyword = txtThongTin.Text.Trim();
            string searchField = rdoMaSanPham.Checked ? "sp.MaSP" : "sp.TenSP";

            if (string.IsNullOrEmpty(keyword)) { LoadData(); return; }

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = $@"
                        SELECT sp.MaSP, sp.TenSP, sp.MaLoaiSP, lsp.TenLoaiSP, 
                               ISNULL(SUM(tk.SoLuongTon), 0) AS SoLuongTon, sp.GiaBan 
                        FROM SanPham sp
                        LEFT JOIN LoaiSanPham lsp ON sp.MaLoaiSP = lsp.MaLoaiSP
                        LEFT JOIN TonKho tk ON sp.MaSP = tk.MaSP
                        WHERE {searchField} LIKE @Keyword
                        GROUP BY sp.MaSP, sp.TenSP, sp.MaLoaiSP, lsp.TenLoaiSP, sp.GiaBan";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    if (dt.Rows.Count == 0) MessageBox.Show("Không tìm thấy kết quả nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi"); }
        }

        private void btnDanhsach_Click(object sender, EventArgs e) { ResetValues(); LoadData(); }
        private void btnTaiLai_Click(object sender, EventArgs e) { ResetValues(); LoadData(); MessageBox.Show("Đã làm mới danh sách!", "Thông báo"); }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.Rows.Count == 0) { MessageBox.Show("Không có dữ liệu để xuất!"); return; }

            try
            {
                Excel.Application excelApp = new Excel.Application();
                excelApp.Visible = true;
                Excel.Workbook workbook = excelApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];
                worksheet.Name = "Danh_Muc";

                int cotCuoi = dgvKetQua.Columns.Count;
                int dongBatDau = 5;

                Excel.Range head = worksheet.Range["A1", "C1"];
                head.MergeCells = true;
                head.Value2 = "CÔNG TY CỔ PHẦN MONDELEZ KINH ĐÔ VIỆT NAM";
                head.Font.Bold = true;
                head.Font.Color = System.Drawing.Color.Red;

                Excel.Range title = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, cotCuoi]];
                title.MergeCells = true;
                title.Value2 = "DANH MỤC SẢN PHẨM";
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
            catch (Exception ex) { MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi"); }
        }

        // Các event thừa
        private void label5_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void txtMaSanPham_TextChanged(object sender, EventArgs e) { }
        private void cboLoaiSanPham_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}