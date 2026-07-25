using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLBHKINHDO
{
    public class KetNoi
    {
        // 1. Lưu chuỗi kết nối tại MỘT NƠI DUY NHẤT
        private static string strCon = @"Data Source=DESKTOP-8AHFSP3;Initial Catalog=QLBH_KINHDO_DTT;Integrated Security=True;TrustServerCertificate=True";

        // 2. Hàm trả về đối tượng Connection mới
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}
