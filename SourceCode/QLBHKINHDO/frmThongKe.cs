using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms; // Bắt buộc phải có thư viện này
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmThongKe : Form
    {
        public frmThongKe()
        {
            InitializeComponent();
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            // Mặc định thiết lập ngày từ đầu tháng đến ngày hiện tại
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            this.reportViewer1.RefreshReport();
        }

        // =====================================================================
        // SỰ KIỆN NÚT "XEM BÁO CÁO" (NÚT MÀU VÀNG)
        // Lưu ý: Nếu cậu click đúp vào nút vàng mà nó ra tên hàm khác (ví dụ button1_Click)
        // thì cậu copy phần ruột bên trong hàm này dán qua đó nhé!
        // =====================================================================
        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();

                    // Sử dụng lại Proc báo cáo chi tiết mình đã tạo lúc trước
                    using (SqlCommand cmd = new SqlCommand("sp_BaoCaoDoanhThu_ChiTiet", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Truyền tham số ngày từ giao diện
                        cmd.Parameters.AddWithValue("@TuNgay", dtpTuNgay.Value.Date);
                        cmd.Parameters.AddWithValue("@DenNgay", dtpDenNgay.Value.Date);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Xử lý trường hợp khoảng thời gian đó không bán được hàng
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Không có doanh thu phát sinh trong khoảng thời gian này!",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reportViewer1.LocalReport.DataSources.Clear();
                            reportViewer1.RefreshReport();
                            return;
                        }

                        // Cấu hình hiển thị Report
                        reportViewer1.ProcessingMode = ProcessingMode.Local;

                        // Tên file rdlc báo cáo của cậu. (Nhớ set Properties -> Copy if newer)
                        reportViewer1.LocalReport.ReportPath = "Report_ThongKe.rdlc";

                        // Tên DataSet1 phải khớp hoàn toàn với tên DataSet trong file rdlc
                        ReportDataSource rds = new ReportDataSource("DataSet1", dt);

                        // Clear dữ liệu cũ, thêm dữ liệu mới và refresh
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rds);
                        reportViewer1.RefreshReport();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải báo cáo:\n" + ex.Message,
                                "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =====================================================================
        // SỰ KIỆN NÚT "TẢI LẠI"
        // =====================================================================
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            // Trả ngày về mặc định
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;

            // Xóa trắng màn hình báo cáo
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy giá trị ngày từ 2 DateTimePicker (Giả sử tên là dtpTuNgay và dtpDenNgay)
            // Dùng .Date để bỏ qua phần giờ phút giây, so sánh cho chính xác
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date;

            // Kiểm tra tính hợp lệ cơ bản
            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở frmChart và truyền 2 mốc thời gian sang
            frmChart fChart = new frmChart(tuNgay, denNgay);
            fChart.ShowDialog();
        }
    }
}