using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // Cần thư viện này để dùng Color
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBHKINHDO
{
    public partial class frmQuanLyDonHang : Form
    {
        // 1. Khai báo biến lưu nút đang chọn (cho thanh menu con)
        private Button currentButton;

        public frmQuanLyDonHang()
        {
            InitializeComponent();
        }

        // --- HÀM ĐỔI MÀU NÚT (ACTIVE) ---
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton(); // Trả màu cũ cho nút trước đó
                    currentButton = (Button)btnSender;

                    // Đổi màu nút đang chọn (Màu Cam đậm / Chữ Trắng)
                    currentButton.BackColor = Color.OrangeRed;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font(currentButton.Font, FontStyle.Bold);
                }
            }
        }

        // --- HÀM TRẢ VỀ MÀU GỐC ---
        private void DisableButton()
        {
            if (currentButton != null)
            {
                // Trả về màu Vàng gốc
                currentButton.BackColor = Color.Gold;
                currentButton.ForeColor = Color.Black;
                currentButton.Font = new Font(currentButton.Font, FontStyle.Regular);
            }
        }

        // --- HÀM MỞ FORM CON (ĐÃ SỬA: Dùng panel2 trên form này) ---
        private void OpenChildForm(Form childForm)
        {
            Panel pnlContainer = panel2; // Panel chứa form con TRÊN FORM NÀY

            // Xóa form con cũ
            if (pnlContainer.Controls.Count > 0)
                pnlContainer.Controls[0].Dispose();

            // Cấu hình form mới
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm vào Panel
            pnlContainer.Controls.Add(childForm);
            pnlContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // ========================================================
        // SỰ KIỆN CLICK CÁC NÚT
        // ========================================================

        // 1. Nút Đơn Hàng Nhập (btnQuanLyDonHang / btnDonHangNhap)
        private void btnQuanLyDonHang_Click(object sender, EventArgs e)
        {
            ActivateButton(sender); // <--- Gọi hàm đổi màu
            try
            {
                frmDonHangNhap danhMucForm = new frmDonHangNhap();
                OpenChildForm(danhMucForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // 2. Nút Đơn Hàng Bán
        private void btnDonHangBan_Click(object sender, EventArgs e)
        {
            ActivateButton(sender); // <--- Gọi hàm đổi màu
            try
            {
                frmDonHangBan danhMucForm = new frmDonHangBan();
                OpenChildForm(danhMucForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}