using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLBHKINHDO; // Namespace chứa lớp KetNoi

namespace QLBHKINHDO
{
    public partial class frmTruyVanSQL : Form
    {
        // Không cần khai báo KetNoi db = new KetNoi() nữa

        public frmTruyVanSQL()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra rỗng -> Dùng Icon WARNING (Tam giác vàng)
            if (txtQuery.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập câu lệnh SQL!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sql = txtQuery.Text.Trim();

            try
            {
                // SỬ DỤNG KetNoi.GetConnection()
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();

                    // --- TRƯỜNG HỢP 1: LỆNH SELECT (Lấy dữ liệu) ---
                    if (sql.ToUpper().StartsWith("SELECT"))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Hiển thị lên lưới
                        dgvKetQua.DataSource = dt;

                        // Thành công -> Dùng Icon INFORMATION (Chữ i xanh)
                        MessageBox.Show($"Truy vấn thành công!\nTìm thấy {dt.Rows.Count} dòng.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // --- TRƯỜNG HỢP 2: LỆNH INSERT, UPDATE, DELETE, CREATE... ---
                    else
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        int rows = cmd.ExecuteNonQuery(); // Thực thi lệnh

                        // Thành công -> Dùng Icon INFORMATION
                        MessageBox.Show($"Thực thi thành công!\nSố dòng bị ảnh hưởng: {rows}", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Xóa dữ liệu cũ trên lưới (vì lệnh này không trả về bảng)
                        dgvKetQua.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi cú pháp SQL -> Dùng Icon ERROR (Dấu X đỏ)
                MessageBox.Show("LỖI SQL: " + ex.Message, "Lỗi Cú Pháp",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmTruyVanSQL_Load(object sender, EventArgs e)
        {
            // Có thể thêm logic load mẫu câu lệnh nếu cần
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtQuery.Clear();
            dgvKetQua.DataSource = null;
            txtQuery.Focus();
        }
    }
}