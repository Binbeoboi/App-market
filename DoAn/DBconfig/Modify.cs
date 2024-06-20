using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DBconfig
{
    public class Modify
    {
        private SqlCommand cmd;
        private SqlDataAdapter sqlDataAdapter;
        public DataTable GetDataTable(string query)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    // 1. Câu query -> câu truy vấn vd: Select * from SanPham
                    // 2. Đối tượng kết nối cnn
                    sqlDataAdapter = new SqlDataAdapter(query, cnn);
                    sqlDataAdapter.Fill(dataTable); //chuyển đổi dữ liệu thô sang DataTable
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return dataTable;
            }
        }
        public int Excute(string query)
        {
            try
            {
                using(SqlConnection cnn = Connection.GetSqlConnection())
                {
                    cmd = new SqlCommand(query, cnn);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
