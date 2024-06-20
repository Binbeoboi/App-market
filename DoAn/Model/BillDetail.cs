using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    public class BillDetail
    {
        public int NumericOrther { get; set; }
        public string ProductName { get; set; }
        [Browsable(false)]
        public string ProductID { get; set; }   
        public int Quantity { get; set; }
        [Browsable(false)]
        public double Price { get; set; }
        public double TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
        public string BillID { get; set; }

        public List<BillDetail> BillList(string id)
        {
            List<BillDetail> billsDetail = new List<BillDetail>();
            string query = $"SELECT Product.ProductName, BillDetails.Quantity, Product.Price, BillDetails.BillID, BillDetails.ProductID FROM BillDetails " +
                $"INNER JOIN Product ON BillDetails.ProductID = Product.ProductID WHERE BillDetails.BillID = N'{id}';";
            try
            {
                using(SqlConnection cnn = Connection.GetSqlConnection())
                {
                    int i = 1;
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        billsDetail.Add(new BillDetail
                        {
                            NumericOrther = i++,
                            ProductName = reader.GetString(0),
                            Quantity = reader.GetInt32(1),
                            Price = reader.GetDouble(2),
                            BillID = reader.GetString(3),
                            ProductID = reader.GetString(4),
                        });

                    }
                }
                return billsDetail;
            }
            catch 
            {
                return null;
            }

        }

        public List<BillDetail> BillList()
        {
            List<BillDetail> billsDetail = new List<BillDetail>();
            string query = $"SELECT Product.ProductName, BillDetails.Quantity, Product.Price, BillDetails.BillID, BillDetails.ProductID FROM BillDetails " +
                $"INNER JOIN Product ON BillDetails.ProductID = Product.ProductID ;";
            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    int i = 1;
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        billsDetail.Add(new BillDetail
                        {
                            NumericOrther = i++,
                            ProductName = reader.GetString(0),
                            Quantity = reader.GetInt32(1),
                            Price = reader.GetDouble(2),
                            BillID = reader.GetString(3),
                            ProductID = reader.GetString(4),
                        });

                    }
                }
                return billsDetail;
            }
            catch
            {
                return null;
            }

        }
        Modify db = new Modify();
        public bool AddBillDetail(BillDetail bill)
        {
            string query = $"INSERT INTO BillDetails VALUES (N'{bill.ProductID}',{bill.Quantity},N'{bill.BillID}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveBillDetail(BillDetail bill)
        {
            string query = $"DELETE FROM BillDetails WHERE BillDetails.ProductID = N'{bill.ProductID}' AND BillDetails.BillID = N'{bill.BillID}';";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditBillDetail(BillDetail bill)
        {
            string query = $"UPDATE BillDetails SET " +
                $"ProductID = N'{bill.ProductID}'," +
                $"Quantity = N'{bill.Quantity}'," +
                $"BillID = N'{bill.BillID}' " +
                $"WHERE BillDetails.ProductID = N'{bill.ProductID}' AND BillDetails.BillID = N'{bill.BillID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

    }
}
