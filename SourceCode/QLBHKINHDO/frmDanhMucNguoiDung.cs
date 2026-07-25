using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using QLBHKINHDO; // Đảm bảo using namespace chứa lớp KetNoi

namespace QLBHKINHDO
{
    public partial class frmDanhMucNguoiDung : Form
    {
        public frmDanhMucNguoiDung()
        {
            InitializeComponent();
        }

        private void frmDanhMucNguoiDung_Load(object sender, EventArgs e)
        {
            LoadPhanQuyen();
            LoadData();
            rdoHoTenNguoiDung.Checked = true;
        }

        // --- HÀM TẢI DỮ LIỆU ---
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT TenDangNhap, MatKhau, HoTen, SoDienThoai, Email, Quyen FROM TaiKhoan";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;

                    // Đặt tên cột
                    if (dgvKetQua.Columns["TenDangNhap"] != null) dgvKetQua.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
                    if (dgvKetQua.Columns["MatKhau"] != null) dgvKetQua.Columns["MatKhau"].HeaderText = "Mật Khẩu";
                    if (dgvKetQua.Columns["HoTen"] != null) dgvKetQua.Columns["HoTen"].HeaderText = "Họ Tên";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetValues()
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            cboQuyen.SelectedIndex = -1;
            txtThongTin.Clear();

            txtTenDangNhap.Enabled = true;
            txtTenDangNhap.Focus();
        }

        private void LoadPhanQuyen()
        {
            cboQuyen.Items.Clear();
            cboQuyen.Items.Add("Quản trị viên");
            cboQuyen.Items.Add("Quản lý");
            cboQuyen.Items.Add("Nhân viên bán hàng");
            cboQuyen.Items.Add("Nhân viên kho");
            cboQuyen.Items.Add("Nhân viên kế toán");
            cboQuyen.SelectedIndex = 0;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập và Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                // Kiểm tra null trước khi gán
                if (row.Cells["MatKhau"].Value != null)
                    txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();

                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                cboQuyen.Text = row.Cells["Quyen"].Value.ToString();

                // Có thể khóa mã nếu muốn
                // txtTenDangNhap.Enabled = false; 
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
                    string sql = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, SoDienThoai, Email, Quyen) VALUES (@User, @Pass, @Name, @Phone, @Email, @Role)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@User", txtTenDangNhap.Text);
                    cmd.Parameters.AddWithValue("@Pass", txtMatKhau.Text);
                    cmd.Parameters.AddWithValue("@Name", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtSoDienThoai.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Role", cboQuyen.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues();
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm (Có thể trùng Tên đăng nhập): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- SỬA ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "UPDATE TaiKhoan SET MatKhau=@Pass, HoTen=@Name, SoDienThoai=@Phone, Email=@Email, Quyen=@Role WHERE TenDangNhap=@User";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Pass", txtMatKhau.Text);
                    cmd.Parameters.AddWithValue("@Name", txtHoTen.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtSoDienThoai.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Role", cboQuyen.Text);
                    cmd.Parameters.AddWithValue("@User", txtTenDangNhap.Text); // Điều kiện WHERE

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetValues();
                        LoadData();
                    }
                    else MessageBox.Show("Không tìm thấy tài khoản để sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // --- XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa tài khoản '{txtTenDangNhap.Text}' không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        // Cần xử lý khóa ngoại (nếu tài khoản đã liên kết với NhanVien)
                        // Cách đơn giản: Update NhanVien về NULL trước
                        string sqlUpdateFK = "UPDATE NhanVien SET TenDangNhap = NULL WHERE TenDangNhap = @User";
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdateFK, conn);
                        cmdUpdate.Parameters.AddWithValue("@User", txtTenDangNhap.Text);
                        cmdUpdate.ExecuteNonQuery();

                        // Sau đó xóa tài khoản
                        string sqlDelete = "DELETE FROM TaiKhoan WHERE TenDangNhap = @User";
                        SqlCommand cmdDelete = new SqlCommand(sqlDelete, conn);
                        cmdDelete.Parameters.AddWithValue("@User", txtTenDangNhap.Text);

                        int rows = cmdDelete.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetValues();
                            LoadData();
                        }
                        else MessageBox.Show("Không tìm thấy tài khoản để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        // --- ĐỔI MẬT KHẨU ---
        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đổi mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu MỚI vào ô Mật khẩu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhau.Focus();
                return;
            }

            if (MessageBox.Show($"Đổi mật khẩu cho '{txtTenDangNhap.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = KetNoi.GetConnection())
                    {
                        conn.Open();
                        string sql = "UPDATE TaiKhoan SET MatKhau=@Pass WHERE TenDangNhap=@User";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Pass", txtMatKhau.Text);
                        cmd.Parameters.AddWithValue("@User", txtTenDangNhap.Text);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                            LoadData();
                            ResetValues();
                        }
                        else MessageBox.Show("Thất bại!", "Lỗi");
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi"); }
            }
        }

        // --- TÌM KIẾM ---
        private void btnTim_Click(object sender, EventArgs e)
        {
            if (txtThongTin.Text == "") { LoadData(); return; }

            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    // Tìm theo Tên đăng nhập hoặc Họ tên
                    string sql = "SELECT * FROM TaiKhoan WHERE TenDangNhap LIKE @TuKhoa OR HoTen LIKE @TuKhoa";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@TuKhoa", "%" + txtThongTin.Text + "%");
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvKetQua.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tìm kiếm: " + ex.Message); }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadData();
            MessageBox.Show("Đã tải lại toàn bộ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ... Các hàm khác (Xuất Excel, Danh Sách...) giữ nguyên hoặc copy lại logic cũ ...
        private void btnDanhSach_Click(object sender, EventArgs e) { btnTaiLai_Click(sender, e); }
        private void btnXuatExcel_Click(object sender, EventArgs e) { }
    }
}