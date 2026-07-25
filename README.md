# 🥮 HỆ THỐNG QUẢN LÝ BÁN BÁNH TRUNG THU TRỰC TUYẾN KINH ĐÔ

Đồ án môn học: Phân tích và thiết kế hướng đối tượng.  
Hệ thống hỗ trợ số hóa quy trình bán hàng trực tuyến, quản lý kho, nhân sự, đơn hàng và khách hàng cho thương hiệu Kinh Đô, đặc biệt tối ưu cho mùa vụ cao điểm.

## 👨‍💻 Thông tin đồ án
* **Sinh viên thực hiện:** Đỗ Trí Tường (MSSV: 2321004127).
* **Giảng viên hướng dẫn:** Th.S Lê Thị Kim Thoa.
* **Môn học:** Phân tích và thiết kế hướng đối tượng.
* **Trường:** Đại học Tài chính – Marketing (Khoa Khoa học Dữ liệu).

## 🛠 Công nghệ & Công cụ sử dụng
* **Nền tảng & Ngôn ngữ:** C# (WinForms).
* **Cơ sở dữ liệu:** SQL Server.
* **Phân tích thiết kế (UML & Data Modeling):** Enterprise Architect, PowerDesigner, Draw.io.
* **Thiết kế UI/UX:** Figma, Canva.
* **Báo cáo & Thống kê:** RDLC Report.

## 🚀 Chức năng chính

### 1. Phân hệ Khách hàng (Trải nghiệm Online)
* Đăng ký, đăng nhập và quản lý tài khoản cá nhân, khôi phục mật khẩu.
* Tìm kiếm, xem chi tiết sản phẩm và thêm vào giỏ hàng.
* Mua hàng, áp dụng mã khuyến mãi và thanh toán (Chuyển khoản Online / Thanh toán khi nhận hàng COD).
* Theo dõi trạng thái đơn hàng, gửi yêu cầu đổi trả, hủy đơn và đánh giá phản hồi.

### 2. Phân hệ Quản trị nội bộ (Hệ thống điều hành)
* **Admin / Quản lý:** Phân quyền tài khoản đa cấp, quản lý thông tin nhân viên, nhà cung cấp, sản phẩm, xem thống kê báo cáo doanh thu và thiết lập chương trình khuyến mãi.
* **Nhân viên Kho:** Lập phiếu nhập/xuất kho, kiểm tra số lượng tồn kho theo thời gian thực và quản lý phiếu chứng từ kho.
* **Nhân viên Kế toán:** Lập chứng từ, đối soát hóa đơn bán hàng, lập phiếu thanh toán và xuất báo cáo tài chính.
* **Nhân viên Bán hàng:** Xử lý và xác nhận đơn đặt hàng trực tuyến, quản lý thông tin khách hàng và lịch sử giao dịch.

## ⚙️ Điểm nổi bật về Cấu trúc Hệ thống & Cơ sở dữ liệu
Hệ thống được thiết kế theo kiến trúc 3 tầng và chuẩn hóa cơ sở dữ liệu đạt dạng chuẩn 3 (3NF) với các ràng buộc tự động hóa bằng Trigger:
* **Tự động hóa tính toán:** Tự động tính thành tiền hóa đơn và tổng tiền phiếu nhập/xuất (`trg_TinhThanhTien_HoaDon`, `trg_AutoCalculateInvoiceTotal`).
* **Kiểm soát Tồn kho (Real-time):** Tự động trừ số lượng tồn kho khi xuất hàng và chặn giao dịch nếu xuất vượt mức tồn kho (`trg_CheckStockBeforeExport`).
* **Chính sách Khách hàng:** Tự động quy đổi doanh thu thành điểm tích lũy và nâng hạng thẻ thành viên (Đồng, Bạc, Vàng...) khi giao dịch hoàn tất (`trg_AccumulatePoints_And_UpgradeTier`).
* **Bảo toàn dữ liệu kế toán:** Thiết lập ràng buộc toàn vẹn chống xóa nhân viên/tài khoản sai quy định để bảo vệ tính lịch sử của chứng từ (`trg_PreventDataLoss_NhanVien`).
