using System;
using System.Windows.Forms;
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTenDangNhap.Text.Trim().ToLower();
            string pass = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string quyenHan = "";
            bool loginSuccess = false;

            // Kiểm tra tài khoản cố định
            if (pass == "123")
            {
                switch (user)
                {
                    case "admin": quyenHan = "Admin"; loginSuccess = true; break;
                    case "quanly": quyenHan = "Quản lý"; loginSuccess = true; break;
                    case "banhang": quyenHan = "Bán hàng"; loginSuccess = true; break;
                    case "ketoan": quyenHan = "Kế toán"; loginSuccess = true; break;
                    case "kho": quyenHan = "Kho"; loginSuccess = true; break;
                }
            }

            if (loginSuccess)
            {
                MessageBox.Show("Đăng nhập thành công!\nVai trò: " + quyenHan, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmTrangChu main = new frmTrangChu(quyenHan);
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Clear();
                txtMatKhau.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Exit();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}