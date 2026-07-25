using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBHKINHDO
{
    public partial class frmTrangChu : Form
    {
        private Button currentButton;
        private string quyenHienTai;

        public frmTrangChu(string quyen = "Admin")
        {
            InitializeComponent();
            this.quyenHienTai = quyen;
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            this.Text = "PHẦN MỀM QUẢN LÝ BÁN HÀNG KINH ĐÔ - XIN CHÀO: " + quyenHienTai.ToUpper();
            PhanQuyenChucNang();
        }

        // --- HÀM PHÂN QUYỀN (ĐÃ SỬA LẠI TÊN NÚT CHUẨN) ---
        private void PhanQuyenChucNang()
        {
            // 1. Chuẩn hóa chuỗi quyền
            string q = quyenHienTai.Trim().ToLower();

            // 2. KHÓA TẤT CẢ TRƯỚC (Dùng tên button1, button2... cho khớp với giao diện của bạn)
            SetButtonState(btnDanhMucSanPham, false);          // Sản phẩm (Sửa từ btnDanhMucSanPham -> button1)
            SetButtonState(button2, false);          // Khách hàng
            SetButtonState(button3, false);          // Người dùng
            SetButtonState(btnNhaCungCap, false);    // NCC
            SetButtonState(btnKhuyenMai, false);     // KM
            SetButtonState(btnQuanLyDonHang, false); // Đơn hàng
            SetButtonState(btnQuanLyTonKho, false);  // Tồn kho
            SetButtonState(button4, false);          // Thống kê
            SetButtonState(buttonCT, false);

            // 3. MỞ KHÓA THEO TỪ KHÓA (Logic bao sân)

            // --- NHÓM QUẢN LÝ / ADMIN ---
            if (q.Contains("quản") || q.Contains("admin"))
            {
                SetButtonState(btnDanhMucSanPham, true);
                SetButtonState(button2, true);
                SetButtonState(button3, true);
                SetButtonState(btnNhaCungCap, true);
                SetButtonState(btnKhuyenMai, true);
                SetButtonState(btnQuanLyDonHang, true);
                SetButtonState(btnQuanLyTonKho, true);
                SetButtonState(button4, true);
                SetButtonState(buttonCT, true);
            }
            // --- NHÓM BÁN HÀNG ---
            else if (q.Contains("bán") || q.Contains("ban"))
            {
                SetButtonState(btnQuanLyDonHang, true);  // Bán hàng
                SetButtonState(button2, true);           // Khách hàng
                SetButtonState(btnKhuyenMai, true);      // Khuyến mãi
                SetButtonState(btnDanhMucSanPham, true);           // Sản phẩm
                SetButtonState(btnQuanLyDonHang, true);  // Nhập hàng
            }
            // --- NHÓM KHO ---
            else if (q.Contains("kho"))
            {
                SetButtonState(btnQuanLyTonKho, true);   // Tồn kho
                SetButtonState(btnDanhMucSanPham, true);           // Sản phẩm
                SetButtonState(btnNhaCungCap, true);     // NCC
                SetButtonState(btnQuanLyDonHang, true);  // Nhập hàng
            }
            // --- NHÓM KẾ TOÁN ---
            else if (q.Contains("kế") || q.Contains("ke"))
            {
                SetButtonState(btnDanhMucSanPham, true);
                SetButtonState(btnNhaCungCap, true);
                SetButtonState(btnQuanLyDonHang, true);
                SetButtonState(button4, true);
                SetButtonState(buttonCT, true);
            }
        }

        // 4. HÀM SET TRẠNG THÁI NÚT
        private void SetButtonState(Button btn, bool enable)
        {
            btn.Enabled = enable;
            if (enable)
            {
                btn.BackColor = Color.Gold;
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.DarkGray;
                btn.ForeColor = Color.DimGray;
            }
        }

        // 5. HÀM ĐỔI MÀU NÚT KHI CLICK
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton(); // Trả màu cũ cho nút trước
                    currentButton = (Button)btnSender;

                    // Đổi màu nền sang Cam Đậm, chữ Trắng
                    currentButton.BackColor = Color.OrangeRed;
                    currentButton.ForeColor = Color.White;

                    // Giữ nguyên Font chữ gốc, chỉ in đậm nếu muốn (hoặc bỏ dòng này nếu muốn giữ y nguyên)
                    // currentButton.Font = new Font(currentButton.Font, FontStyle.Bold);
                }
            }
        }

        // 6. HÀM TRẢ VỀ MÀU GỐC
        private void DisableButton()
        {
            if (currentButton != null)
            {
                // Trả về màu Vàng gốc, chữ Đen
                currentButton.BackColor = Color.Gold;
                currentButton.ForeColor = Color.Black;

                // Trả về Font thường (nếu bên trên có in đậm)
                // currentButton.Font = new Font(currentButton.Font, FontStyle.Regular);
            }
        }

        // 7. HÀM MỞ FORM CON
        private void OpenChildForm(Form childForm)
        {
            Panel pnlContainer = panel2;
            if (pnlContainer.Controls.Count > 0)
                pnlContainer.Controls[0].Dispose();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContainer.Controls.Add(childForm);
            pnlContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        // =======================================================================
        // CÁC SỰ KIỆN CLICK
        // =======================================================================

        private void button1_Click(object sender, EventArgs e) // Sản phẩm
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmDanhMucSanPham()); } catch { }
        }

        private void button2_Click(object sender, EventArgs e) // Khách hàng
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmDanhMucKhachHang()); } catch { }
        }

        private void button3_Click(object sender, EventArgs e) // Người dùng
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmDanhMucNguoiDung()); } catch { }
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmDanhMucNhaCungCap()); } catch { }
        }

        private void btnKhuyenMai_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmKhuyenMai()); } catch { }
        }

        private void btnQuanLyDonHang_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmQuanLyDonHang()); } catch { }
        }

        private void btnQuanLyTonKho_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmQuanLyTonKho()); } catch { }
        }

        private void button4_Click(object sender, EventArgs e) // Thống kê
        {
            ActivateButton(sender);
            try { OpenChildForm(new frmThongKe()); } catch { }
        }

        // --- MENU & NÚT KHÁC ---

        private void giớiThiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { OpenChildForm(new frmGioiThieu()); } catch { }
        }

        private void viếtLệnhTruyVấnSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyenHienTai != "Admin" && quyenHienTai != "Quản trị viên" && quyenHienTai != "Quản lý")
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Cảnh báo");
                return;
            }
            frmTruyVanSQL f = new frmTruyVanSQL();
            f.ShowDialog();
        }

        private void phânQuyềnTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (quyenHienTai != "Admin" && quyenHienTai != "Quản trị viên" && quyenHienTai != "Quản lý")
            {
                MessageBox.Show("Bạn không có quyền truy cập quản lý tài khoản!", "Thông báo");
                return;
            }
            try { OpenChildForm(new frmDanhMucNguoiDung()); } catch { }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button5_Click(sender, e); // Gọi lại nút Đăng xuất to
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",  // Nội dung thông báo
                "Xác nhận thoát",                                  // Tiêu đề hộp thoại
                MessageBoxButtons.YesNo,                           // Hiển thị nút Yes và No
                MessageBoxIcon.Question                            // Hiển thị Icon Dấu hỏi (?)
            );

            // Nếu người dùng bấm Yes (Có) thì mới thoát
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e) // Đăng xuất
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                frmDangNhap login = new frmDangNhap();
                login.ShowDialog();
                this.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (currentButton != null) DisableButton();
            if (panel2.Controls.Count > 0) panel2.Controls[0].Dispose();
        }

        // Các sự kiện thừa
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint_1(object sender, PaintEventArgs e) { }
        private void panel2_Paint_1(object sender, PaintEventArgs e) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình không?",  // Nội dung thông báo
                "Xác nhận thoát",                                  // Tiêu đề hộp thoại
                MessageBoxButtons.YesNo,                           // Hiển thị nút Yes và No
                MessageBoxIcon.Question                            // Hiển thị Icon Dấu hỏi (?)
            );

            // Nếu người dùng bấm Yes (Có) thì mới thoát
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonCT_Click(object sender, EventArgs e)
        {
            // 1. Đổi màu nút khi click (để người dùng biết đang chọn mục này)
            ActivateButton(sender);

            // 2. Mở form Chứng Từ vào vùng hiển thị (panel2)
            try
            {
                OpenChildForm(new frmChungTu());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở form Chứng Từ: " + ex.Message);
            }
        }
    }
}