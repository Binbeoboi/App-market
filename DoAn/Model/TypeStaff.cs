using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    public class TypeStaff
    {
         public string ID {get; set;}
         public string Name {get; set;}
         public int BaseSalary {get; set;}

        public List<TypeStaff> ListTypeStaff()
        {
            List<TypeStaff> listTypeStaff = new List<TypeStaff>();
            string query = "SELECT * FROM TypeStaff";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listTypeStaff.Add(new TypeStaff
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                            BaseSalary = sqlData.GetInt32(2),
                        });
                    }
                }
                return listTypeStaff;
            }
            catch
            {
                return null;
            }

        }
        Modify db = new Modify();
        public bool AddType(TypeStaff type)
        {
            string query = $"INSERT INTO TypeStaff VALUES (N'{type.ID}',N'{type.Name}', {type.BaseSalary});";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveType(string ID)
        {
            string query = $"DELETE FROM TypeStaff WHERE IDTypeStaff = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditType(TypeStaff type)
        {
            string query = $"UPDATE TypeStaff SET " +
                $"IDTypeStaff = N'{type.ID}'," +
                $"NameTypeStaff = N'{type.Name}', " +
                $"BaseSalary = N'{type.BaseSalary}' " +
                $"WHERE IDTypeStaff = N'{type.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }
    }    
}
