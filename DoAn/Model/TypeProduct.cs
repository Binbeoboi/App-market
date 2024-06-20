using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    public class TypeProduct
    {
        public string ID {get;set; }
        public string Name {get;set; }


        public List<TypeProduct> ListTypeProduct()
        {
            List<TypeProduct> listTypeProduct = new List<TypeProduct>();
            string query = "SELECT * FROM TypeProduct";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listTypeProduct.Add(new TypeProduct
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                        });
                    }
                }
                return listTypeProduct;
            }
            catch
            {
                return null;
            }

        }
        Modify db = new Modify();
        public bool AddType(TypeProduct type)
        {
            string query = $"INSERT INTO TypeProduct VALUES (N'{type.ID}',N'{type.Name}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveType(string ID)
        {
            string query = $"DELETE FROM TypeProduct WHERE TypeId = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditType(TypeProduct type)
        {
            string query = $"UPDATE TypeProduct SET " +
                $"TypeId = N'{type.ID}'," +
                $"TypeName = N'{type.Name}' " +
                $"WHERE TypeId = N'{type.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }
    }
}
