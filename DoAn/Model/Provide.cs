using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    internal class Provide
    {
        public string ID{get; set;}
        public string Name{get; set;}
        public string Type{get; set;}
        public string Address{get; set;}
        public  int Quantity {get; set;}
        public long Price {get; set;}
        public string PhoneNumber{get; set;}
        public string Email{ get; set; }

        public List<Provide> ListProvide()
        {
            List<Provide> listProvide = new List<Provide>();
            string query = "SELECT Provide.IDProvide, Provide.NameProvide, TypeProduct.TypeName" +
                ", Provide.AddressProvide, Provide.QuantityProvide, Provide.PriceProvide, Provide.PhoneNumberProvide," +
                " Provide.EmailProvide FROM Provide INNER JOIN TypeProduct ON Provide.TypeProvide = TypeProduct.TypeId;";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listProvide.Add(new Provide
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                            Type = sqlData.GetString(2),
                            Address = sqlData.GetString(3),
                            Quantity = sqlData.GetInt32(4),
                            Price = sqlData.GetInt64(5),
                            PhoneNumber = sqlData.GetString(6),
                            Email = sqlData.GetString(7),
                        });
                    }
                }
                return listProvide;
            }
            catch
            {
                return null;
            }

        }

        Modify db = new Modify();
        public bool AddProvide(Provide provide)
        {
            string query = $"INSERT INTO Provide VALUES (N'{provide.ID}',N'{provide.Name}',N'{provide.Type}',N'{provide.Address}',{provide.Quantity},{provide.Price},N'{provide.PhoneNumber}', N'{provide.Email}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveProvide(string ID)
        {
            string query = $"DELETE FROM Provide WHERE IDProvide = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditProvide(Provide provide)
        {
            string query = $"UPDATE Provide SET " +
                $"IDProvide = N'{provide.ID}'," +
                $"NameProvide = N'{provide.Name}'," +
                $"TypeProvide = N'{provide.Type}'," +
                $"AddressProvide = N'{provide.Address}'," +
                $"QuantityProvide = {provide.Quantity}," +
                $"PriceProvide = {provide.Price}," +
                $"PhoneNumberProvide = N'{provide.PhoneNumber}'," +
                $"EmailProvide = N'{provide.Email}' " +
                $"WHERE IDProvide = N'{provide.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

       
    }
}
