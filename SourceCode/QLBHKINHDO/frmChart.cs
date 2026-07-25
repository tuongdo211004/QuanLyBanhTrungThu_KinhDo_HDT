using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLBHKINHDO
{
    public partial class frmChart : Form
    {
        // Khai báo 2 biến toàn cục để hứng dữ liệu từ frmThongKe truyền sang
        private DateTime _tuNgay;
        private DateTime _denNgay;

        // Hàm khởi tạo nhận 2 tham số
        public frmChart(DateTime tuNgay, DateTime denNgay)
        {
            InitializeComponent();
            _tuNgay = tuNgay;
            _denNgay = denNgay;
        }

        private void frmChart_Load(object sender, EventArgs e)
        {
            FormatGiaoDien(); // Gọi hàm làm đẹp giao diện trước
            LoadChartData();  // Gọi hàm load dữ liệu biểu đồ sau
        }

        private void LoadChartData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();

                    // Câu lệnh SQL: Nhóm hóa đơn theo ngày và tính tổng tiền
                    string sql = @"SELECT CAST(NgayLapHD AS DATE) AS Ngay, SUM(TongTien) AS TongDoanhThu
                                   FROM HoaDon 
                                   WHERE CAST(NgayLapHD AS DATE) >= @TuNgay 
                                     AND CAST(NgayLapHD AS DATE) <= @DenNgay 
                                     AND TrangThai = N'Đã hoàn thành' 
                                   GROUP BY CAST(NgayLapHD AS DATE)
                                   ORDER BY Ngay ASC";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@TuNgay", _tuNgay);
                    cmd.Parameters.AddWithValue("@DenNgay", _denNgay);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có dữ liệu doanh thu trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Xóa dữ liệu cũ trên chart (nếu có)
                    chartDoanhThu.Series.Clear();

                    // Cấu hình Series mới
                    Series series = new Series("DoanhThu");
                    series.ChartType = SeriesChartType.Column;
                    series.IsValueShownAsLabel = true;
                    series.LabelFormat = "{0:N0}";

                    // --- TÙY CHỈNH CỘT ---
                    // Chỉnh font chữ của các con số trên cột bé lại
                    series.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    // Chỉnh độ rộng của cột để cột ốm lại, giãn khoảng cách ra
                    series["PointWidth"] = "0.5";

                    chartDoanhThu.Series.Add(series);

                    // Đổ dữ liệu từ DataTable vào Chart
                    chartDoanhThu.DataSource = dt;
                    chartDoanhThu.Series["DoanhThu"].XValueMember = "Ngay";
                    chartDoanhThu.Series["DoanhThu"].YValueMembers = "TongDoanhThu";

                    // Định dạng trục X hiển thị ngày tháng cho đẹp
                    chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Format = "dd/MM/yyyy";
                    chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;

                    chartDoanhThu.DataBind();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm dùng để làm đẹp giao diện bằng code
        private void FormatGiaoDien()
        {
            // 1. LÀM ĐẸP FORM CƠ BẢN
            this.BackColor = Color.White;
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2. ĐỔI FONT CHỮ HIỆN ĐẠI CHO TOÀN BỘ CONTROLS TRÊN FORM
            Font fontHienDai = new Font("Segoe UI", 10, FontStyle.Regular);
            foreach (Control c in this.Controls)
            {
                c.Font = fontHienDai;
            }

            // 3. LÀM ĐẸP BIỂU ĐỒ (CHART)
            if (chartDoanhThu != null)
            {
                chartDoanhThu.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;

                // --- TÙY CHỈNH LƯỚI NỀN ---
                // Tắt kẻ dọc (trục X)
                chartDoanhThu.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

                // Bật kẻ ngang (trục Y), đổi màu xám nhạt và dùng nét đứt
                chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                chartDoanhThu.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;


                // Cho chữ ở trục X (Ngày tháng) xoay chéo 45 độ nếu bị quá sát nhau
                chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            }
        }

        // Các sự kiện UI rỗng (giữ nguyên để tránh lỗi báo Designer)
        private void label1_Click(object sender, EventArgs e) { }
        private void chartDoanhThu_Click(object sender, EventArgs e) { }
    }
}