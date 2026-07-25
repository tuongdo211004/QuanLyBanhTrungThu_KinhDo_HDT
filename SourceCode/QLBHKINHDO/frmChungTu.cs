using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Reporting.WinForms; // BẮT BUỘC PHẢI CÓ THƯ VIỆN NÀY ĐỂ CHẠY REPORT
using QLBHKINHDO;

namespace QLBHKINHDO
{
    public partial class frmChungTu : Form
    {
        public frmChungTu()
        {
            InitializeComponent();
        }

        private void frmChungTu_Load(object sender, EventArgs e)
        {
            dtpNgayLap.Format = DateTimePickerFormat.Custom;
            dtpNgayLap.CustomFormat = "dd/MM/yyyy";
            LoadData();
            this.reportViewer1.RefreshReport();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * FROM ChungTu";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvChungTu.DataSource = dt;

                    // Đặt tên tiêu đề cho khớp CSDL
                    if (dgvChungTu.Columns["MaCT"] != null) dgvChungTu.Columns["MaCT"].HeaderText = "Mã Chứng Từ";
                    if (dgvChungTu.Columns["LoaiCT"] != null) dgvChungTu.Columns["LoaiCT"].HeaderText = "Loại CT";
                    if (dgvChungTu.Columns["NoiDung"] != null) dgvChungTu.Columns["NoiDung"].HeaderText = "Nội Dung";
                    if (dgvChungTu.Columns["TongGiaTri"] != null)
                    {
                        dgvChungTu.Columns["TongGiaTri"].HeaderText = "Tổng Giá Trị";
                        dgvChungTu.Columns["TongGiaTri"].DefaultCellStyle.Format = "N0";
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message); }
        }

        private void dgvChungTu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChungTu.Rows[e.RowIndex];
                txtMaCT.Text = row.Cells["MaCT"].Value?.ToString();
                txtLoaiCT.Text = row.Cells["LoaiCT"].Value?.ToString();
                txtNoiDung.Text = row.Cells["NoiDung"].Value?.ToString();
                txtTongGiaTri.Text = row.Cells["TongGiaTri"].Value?.ToString();
                txtTrangThai.Text = row.Cells["TrangThai"].Value?.ToString();
                dtpNgayLap.Value = Convert.ToDateTime(row.Cells["NgayLap"].Value);
                txtMaCT.Enabled = false;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();
                    string sql = "INSERT INTO ChungTu (MaCT, LoaiCT, MaNV, NgayLap, NoiDung, TongGiaTri, TrangThai) VALUES (@Ma, @Loai, 'NV01', @Ngay, @ND, @Gia, @TT)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Ma", txtMaCT.Text);
                    cmd.Parameters.AddWithValue("@Loai", txtLoaiCT.Text);
                    cmd.Parameters.AddWithValue("@Ngay", dtpNgayLap.Value);
                    cmd.Parameters.AddWithValue("@ND", txtNoiDung.Text);
                    cmd.Parameters.AddWithValue("@Gia", decimal.Parse(txtTongGiaTri.Text));
                    cmd.Parameters.AddWithValue("@TT", txtTrangThai.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm chứng từ thành công!");
                    btnTaiLai_Click(null, null);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvChungTu.Rows.Count == 0) return;
            try
            {
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook wb = exApp.Workbooks.Add(Type.Missing);
                Excel.Worksheet ws = (Excel.Worksheet)wb.Sheets[1];
                ws.Name = "Danh Sach Chung Tu";

                // 1. Tạo Tiêu đề lớn phía trên
                Excel.Range headerRange = ws.get_Range("A1", "G1");
                headerRange.Merge();
                headerRange.Value = "DANH SÁCH CHỨNG TỪ KẾ TOÁN - KINH ĐÔ";
                headerRange.Font.Size = 16;
                headerRange.Font.Bold = true;
                headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                // 2. Tạo Header cho bảng
                for (int i = 0; i < dgvChungTu.Columns.Count; i++)
                {
                    ws.Cells[3, i + 1] = dgvChungTu.Columns[i].HeaderText;
                    Excel.Range cellHeader = (Excel.Range)ws.Cells[3, i + 1];
                    cellHeader.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gold);
                    cellHeader.Font.Bold = true;
                    cellHeader.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }

                // 3. Đổ dữ liệu và kẻ bảng
                for (int i = 0; i < dgvChungTu.Rows.Count; i++)
                {
                    if (dgvChungTu.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvChungTu.Columns.Count; j++)
                    {
                        ws.Cells[i + 4, j + 1] = "'" + dgvChungTu.Rows[i].Cells[j].Value?.ToString();
                        Excel.Range cellData = (Excel.Range)ws.Cells[i + 4, j + 1];
                        cellData.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    }
                }

                ws.Columns.AutoFit();
                exApp.Visible = true;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Excel: " + ex.Message); }
        }

        private void btnXuatWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCT.Text)) { MessageBox.Show("Hãy chọn 1 chứng từ!"); return; }
            try
            {
                Word.Application wApp = new Word.Application();
                Word.Document doc = wApp.Documents.Add();
                wApp.Visible = true;

                // 1. Quốc hiệu tiêu ngữ
                Word.Paragraph pQuocHieu = doc.Content.Paragraphs.Add();
                pQuocHieu.Range.Text = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM\nĐộc lập - Tự do - Hạnh phúc";
                pQuocHieu.Range.Font.Bold = 1;
                pQuocHieu.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                pQuocHieu.Range.InsertParagraphAfter();

                // 2. Tên chứng từ
                Word.Paragraph pTenCT = doc.Content.Paragraphs.Add();
                pTenCT.Range.Text = "\nCHỨNG TỪ THANH TOÁN";
                pTenCT.Range.Font.Size = 20;
                pTenCT.Range.Font.Color = Word.WdColor.wdColorRed;
                pTenCT.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                pTenCT.Range.InsertParagraphAfter();

                // 3. Tạo bảng thông tin chi tiết (Nhìn cho sang)
                Word.Table tbl = doc.Tables.Add(pTenCT.Range, 5, 2);
                tbl.Borders.Enable = 1; // Hiện khung bảng
                tbl.Cell(1, 1).Range.Text = "Mã chứng từ:";
                tbl.Cell(1, 2).Range.Text = txtMaCT.Text;
                tbl.Cell(2, 1).Range.Text = "Ngày lập:";
                tbl.Cell(2, 2).Range.Text = dtpNgayLap.Value.ToString("dd/MM/yyyy");
                tbl.Cell(3, 1).Range.Text = "Nội dung:";
                tbl.Cell(3, 2).Range.Text = txtNoiDung.Text;
                tbl.Cell(4, 1).Range.Text = "Tổng tiền:";
                tbl.Cell(4, 2).Range.Text = string.Format("{0:N0} VNĐ", decimal.Parse(txtTongGiaTri.Text));
                tbl.Cell(5, 1).Range.Text = "Trạng thái:";
                tbl.Cell(5, 2).Range.Text = txtTrangThai.Text;

                // Định dạng bảng
                tbl.Columns[1].Width = 100;
                tbl.Range.Font.Size = 12;
                tbl.Range.Font.Bold = 0;

                // 4. Phần chữ ký (Căn phải)
                Word.Paragraph pKyTen = doc.Content.Paragraphs.Add();
                pKyTen.Range.Text = $"\n\nTP. Hồ Chí Minh, ngày {DateTime.Now.Day} tháng {DateTime.Now.Month} năm {DateTime.Now.Year}\nNgười lập phiếu\n(Ký và ghi rõ họ tên)";
                pKyTen.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                pKyTen.Range.InsertParagraphAfter();

                MessageBox.Show("Đã tạo chứng từ Word thành công!");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Word: " + ex.Message); }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtMaCT.Clear(); txtLoaiCT.Clear(); txtNoiDung.Clear(); txtTongGiaTri.Clear();
            txtMaCT.Enabled = true;
            LoadData();
        }

        // =====================================================================
        // SỰ KIỆN NÚT BÁO CÁO (NÚT BUTTON 1) - TÍCH HỢP RDLC VÀ SQL SERVER
        // =====================================================================
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Sử dụng class KetNoi.GetConnection() để lấy chuỗi kết nối
                using (SqlConnection conn = KetNoi.GetConnection())
                {
                    conn.Open();

                    // Gọi Stored Procedure thống kê doanh thu thực tế
                    using (SqlCommand cmd = new SqlCommand("sp_BaoCaoDoanhThu_ChiTietToanDien", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                      

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Kiểm tra nếu không có dữ liệu
                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Không có doanh thu phát sinh trong khoảng thời gian này!",
                                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reportViewer1.LocalReport.DataSources.Clear();
                            reportViewer1.RefreshReport();
                            return;
                        }

                        // Cấu hình Report Viewer
                        reportViewer1.ProcessingMode = ProcessingMode.Local;

                        // Đường dẫn tới file report
                        reportViewer1.LocalReport.ReportPath = "Report_ThongKe.rdlc";

                        // Tên DataSet1 khớp 100% với file RDLC của cậu
                        ReportDataSource rds = new ReportDataSource("DataSet1", dt);

                        // Xóa dữ liệu cũ và nạp dữ liệu mới vào
                        reportViewer1.LocalReport.DataSources.Clear();
                        reportViewer1.LocalReport.DataSources.Add(rds);

                        // Hiển thị báo cáo
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

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}