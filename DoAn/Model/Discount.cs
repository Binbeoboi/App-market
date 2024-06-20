using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    internal class Discount
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsPercent { get; set; }
        public bool IsMoney { get; set; }
        public int Number { get; set; }

        public List<Discount> ListDiscount()
        {
            List<Discount> discounts = new List<Discount>();
            string query = "Select * from Discount";
            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        discounts.Add(new Discount
                        {
                            ID = sqlDataReader.GetString(0),
                            Name = sqlDataReader.GetString(1),
                            StartTime = DateTime.Parse(sqlDataReader.GetString(2)),
                            EndTime = DateTime.Parse(sqlDataReader.GetString(3)),
                            IsPercent = sqlDataReader.GetBoolean(4),
                            IsMoney = sqlDataReader.GetBoolean(5),
                            Number = sqlDataReader.GetInt32(6)
                        });
                    }
                }
                return discounts;
            }
            catch
            {
                return null;
            }
        }

        Modify db = new Modify();

        private int ConvertIsPercent()
        {
            if(IsPercent)
            {
                return 1;
            }
            return 0;
        }

        private int ConvertIsMoney()
        {
            if (IsMoney)
            {
                return 1;
            }
            return 0;
        }
        public bool AddDiscount(Discount discount)
        {
            string query = $"INSERT INTO Discount VALUES (N'{discount.ID}',N'{discount.Name}', N'{discount.StartTime.ToString("dd/MM/yyyy")}',N'{discount.EndTime.ToString("dd/MM/yyyy")}',{discount.ConvertIsPercent()},{discount.ConvertIsMoney()},N'{discount.Number}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveDiscount(string ID)
        {
            string query = $"DELETE FROM Discount WHERE DiscountID = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditDiscount(Discount discount)
        {
            string query = $"UPDATE Discount SET " +
                $"DiscountID = N'{discount.ID}'," +
                $"DiscountName = N'{discount.Name}'," +
                $"StartTime = N'{discount.StartTime.ToString("dd/MM/yyyy")}'," +
                $"EndTime = N'{discount.EndTime.ToString("dd/MM/yyyy")}'," +
                $"DiscountPercent = {discount.ConvertIsPercent()}," +
                $"DiscountMoney = {discount.ConvertIsMoney()}," +
                $"DiscountNumber = N'{discount.Number}' " +
                $"WHERE DiscountID = N'{discount.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }
    }
}
