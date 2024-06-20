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
    public class Bill
    {
        public string ID {get;set;}
        public string CustomerName {get;set;}
        public string StaffName {get;set;}
        public DateTime Date{ get; set; }
        [Browsable(false)]
        public string CustomerID { get;set;}
        [Browsable(false)]
        public string StaffID {  get;set;}  

        public List<Bill> Billslst()
        {
            List<Bill> lstBill = new List<Bill>();
            string query = "SELECT Bill.ID, Customer.NameCustomer, Staff.NameStaff, Bill.Date, Bill.CustomerID, Bill.StaffID FROM Bill " +
                "INNER JOIN Customer ON Bill.CustomerID = Customer.IDCustomer " +
                "INNER JOIN Staff ON Bill.StaffID = Staff.IDStaff;";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        lstBill.Add(new Bill
                        {
                            ID = sqlData.GetString(0),
                            CustomerName = sqlData.GetString(1),
                            StaffName = sqlData.GetString(2),
                            Date = DateTime.Parse(sqlData.GetString(3)),
                            CustomerID = sqlData.GetString(4),
                            StaffID = sqlData.GetString(5),
                        });
                    }
                }
                return lstBill;
            }
            catch
            {
                return null;
            }
        }

        Modify modify = new Modify();
        public bool AddBill(Bill bill)
        {
            string query = $"INSERT INTO Bill VALUES (N'{bill.ID.ToUpper()}', N'{bill.CustomerID}', N'{bill.StaffID}', N'{bill.Date.ToString("dd/MM/yyyy")}');";
            int affected = modify.Excute(query);
            return affected == 1;
        }

        public bool DeleteBill(string id)
        {
            string query = $"DELETE FROM Bill WHERE ID = N'{id}';";
            int affected = modify.Excute(query);
            return affected == 1;
        }

        public bool UpdateBill(Bill bill) 
        {
            string query = $"UPDATE Bill SET " +
                $"ID = N'{bill.ID.ToUpper()}', " +
                $"CustomerID = N'{bill.CustomerID}', " +
                $"StaffID = N'{bill.StaffID}', " +
                $"Date = N'{bill.Date.ToString("dd/MM/yyyy")}' " +
                $"WHERE ID = N'{bill.ID}'";
            int affected = modify.Excute(query);
            return affected == 1;
        }
    }
}
