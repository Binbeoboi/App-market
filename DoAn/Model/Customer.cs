using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    public class Customer
    {
         public string ID {get;set;}
         public string Name {get;set;}
         public string Gender{get;set;}
         public DateTime BirthDate{get;set;}
         public string Address{get;set;}
         public string PhoneNumber{get;set;}
         public string Email{get;set;}
         public int Score{ get; set; }

        public List<Customer> ListCustomer()
        {
            List<Customer> listCustomer = new List<Customer>();
            string query = "SELECT * FROM Customer";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listCustomer.Add(new Customer
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                            Gender = sqlData.GetString(2),
                            BirthDate = DateTime.Parse(sqlData.GetString(3)),
                            Address = sqlData.GetString(4),
                            PhoneNumber = sqlData.GetString(5),
                            Email = sqlData.GetString(6),
                            Score = sqlData.GetInt32(7)
                        });
                    }
                }
                return listCustomer;
            }
            catch
            {
                return null;
            }

        }
        Modify db = new Modify();
        public bool AddCustomer(Customer customer)
        {
            string query = $"INSERT INTO Customer VALUES (N'{customer.ID}',N'{customer.Name}'," +
                $"N'{customer.Gender}',N'{customer.BirthDate.ToString("dd/MM/yyyy")}'," +
                $"N'{customer.Address}','{customer.PhoneNumber}',N'{customer.Email}', N'{customer.Score}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveCustomer(string ID)
        {
            string query = $"DELETE FROM Customer WHERE IDCustomer = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditCustomer(Customer customer)
        {
            string query = $"UPDATE Customer SET " +
                $"IDCustomer = N'{customer.ID}'," +
                $"NameCustomer = N'{customer.Name}'," +
                $"GenderCustomer = N'{customer.Gender}'," +
                $"BirthdateCustomer = N'{customer.BirthDate.ToString("dd/MM/yyyy")}'," +
                $"AddressCustomer = N'{customer.Address}'," +
                $"PhoneNumberCustomer = '{customer.PhoneNumber}'," +
                $"EmailCustomer = N'{customer.Email}'," +
                $"ScoreCustomer = {customer.Score} " +
                $"WHERE IDCustomer = N'{customer.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditCustomerByScore(string id)
        {
            
            string query = $"UPDATE Customer SET " +
                $"ScoreCustomer = {FindByID(id).Score + 1} " +                
                $"WHERE IDCustomer = N'{id}'";

            int affected = db.Excute(query);
            return affected == 1;
        }
        public Customer FindByID(string id)
        {
            return ListCustomer().SingleOrDefault(x => x.ID == id);
        }
    }
}
