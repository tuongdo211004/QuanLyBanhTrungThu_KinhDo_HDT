using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO; // Bắt buộc phải có để dùng Path và Directory

namespace QLBHKINHDO
{
    public partial class frmXuatHoaDon : Form
    {
        private string _maHD;

        public frmXuatHoaDon(string maHD)
        {
            InitializeComponent();
            _maHD = maHD;
        }

        private void frmXuatHoaDon_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        // Bổ sung gọi LoadReport ở đây phòng trường hợp Form giao diện bị bind nhầm sự kiện
        private void frmXuatHoaDon_Load_1(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InHoaDonBanHang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaHD", _maHD);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Không tìm thấy dữ liệu cho hóa đơn: " + _maHD, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        reportViewer1.LocalReport.DataSources.Clear();

                        // QUAN TRỌNG: Tên "DataSet2" phải giống y hệt tên Dataset trong file RDLC của bạn
                        ReportDataSource rds = new ReportDataSource("DataSet2", dt);
                        reportViewer1.LocalReport.DataSources.Add(rds);

                        // SỬA TÊN FILE TẠI ĐÂY
                        string tenFileReport = "report_hoadon.rdlc";
                        string reportPath = Path.Combine(Application.StartupPath, tenFileReport);

                        // Thuật toán bẫy lỗi và tìm kiếm file thông minh
                        if (!File.Exists(reportPath))
                        {
                            // Thử tìm ngược ra ngoài thư mục gốc của project (đề phòng chưa chỉnh Copy to Output)
                            string altPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, tenFileReport);
                            if (File.Exists(altPath))
                            {
                                reportPath = altPath; // Dùng đường dẫn dự phòng
                            }
                            else
                            {
                                MessageBox.Show("Hệ thống không tìm thấy file báo cáo tại:\n" + reportPath +
                                                "\n\nCách sửa: Chuột phải vào file " + tenFileReport + " trong Solution Explorer > Chọn Properties > Đổi dòng 'Copy to Output Directory' thành 'Copy if newer'.",
                                                "Thiếu file báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        reportViewer1.LocalReport.ReportPath = reportPath;
                        this.reportViewer1.RefreshReport();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải hóa đơn:\n" + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}