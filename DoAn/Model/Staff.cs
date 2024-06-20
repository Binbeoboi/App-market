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
    public class Staff
    {
        public string ID{get; set;}
        public string Name{get; set;}
        public string Gender{get; set;}
        public DateTime BirthDate{get; set;}
        public int YOE{get; set;}
        public string Email{get; set;}
        public string PhoneNumber{get; set;}
        public string Type {get; set;}
        [Browsable(false)]
        public int BaseSalary { get; set; }
        public string Address { get; set; }
        public double CalculTotalSalary
        {
            get
            {
                return YOE * 10 * BaseSalary;
            }
        }

        public string ConvertNameStaff()
        {
            var fullName = Name.Split(' ');
            return fullName[0] + " " + fullName[2];

        }
        public List<Staff> ListStaff()
        {
            List<Staff> listStaff = new List<Staff>();
            string query = "SELECT Staff.IDStaff, Staff.NameStaff, Staff.GenderStaff, Staff.BirtdateStaff," +
                " Staff.AddressStaff, Staff.YOE, TypeStaff.NameTypeStaff, Staff.PhoneNumberStaff, Staff.Email, TypeStaff.BaseSalary FROM Staff " +
                "INNER JOIN TypeStaff ON Staff.TypeStaff = TypeStaff.IDTypeStaff;";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listStaff.Add(new Staff
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                            Gender = sqlData.GetString(2),
                            BirthDate = DateTime.Parse(sqlData.GetString(3)),
                            Address = sqlData.GetString(4),
                            YOE = sqlData.GetInt32(5),
                            Type = sqlData.GetString(6),
                            PhoneNumber = sqlData.GetString(7),
                            Email = sqlData.GetString(8),
                            BaseSalary = sqlData.GetInt32(9)
                        });
                    }
                }
                return listStaff;
            }
            catch
            {
                return null;
            }

        }

        public List<Staff> ListCashier()
        {
            List<Staff> listStaff = new List<Staff>();
            string query = "SELECT Staff.IDStaff, Staff.NameStaff, Staff.GenderStaff, Staff.BirtdateStaff," +
                " Staff.AddressStaff, Staff.YOE, TypeStaff.NameTypeStaff, Staff.PhoneNumberStaff, Staff.Email, TypeStaff.BaseSalary FROM Staff " +
                "INNER JOIN TypeStaff ON Staff.TypeStaff = TypeStaff.IDTypeStaff WHERE Staff.TypeStaff = N'TLNV02';";

            try
            {
                using (SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listStaff.Add(new Staff
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                            Gender = sqlData.GetString(2),
                            BirthDate = DateTime.Parse(sqlData.GetString(3)),
                            Address = sqlData.GetString(4),
                            YOE = sqlData.GetInt32(5),
                            Type = sqlData.GetString(6),
                            PhoneNumber = sqlData.GetString(7),
                            Email = sqlData.GetString(8),
                            BaseSalary = sqlData.GetInt32(9)
                        });
                    }
                }
                return listStaff;
            }
            catch
            {
                return null;
            }

        }

        Modify db = new Modify();
        public bool AddStaff(Staff staff)
        {
            string query = $"INSERT INTO Staff VALUES (N'{staff.ID}',N'{staff.Name}',N'{staff.Gender}',N'{staff.BirthDate.ToString("dd/MM/yyyy")}',N'{staff.Address}',{staff.YOE},N'{staff.Type}', N'{staff.PhoneNumber}', N'{staff.Email}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveStaff(string ID)
        {
            string query = $"DELETE FROM Staff WHERE IDStaff = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditStaff(Staff staff)
        {
            string query = $"UPDATE Staff SET " +
                $"IDStaff = N'{staff.ID}'," +
                $"NameStaff = N'{staff.Name}'," +
                $"GenderStaff = N'{staff.Gender}'," +
                $"BirtdateStaff = N'{staff.BirthDate.ToString("dd/MM/yyyy")}'," +
                $"AddressStaff = N'{staff.Address}'," +
                $"YOE = {staff.YOE}," +
                $"TypeStaff = N'{staff.Type}'," +
                $"PhoneNumberStaff = N'{staff.PhoneNumber}'," +
                $"Email = N'{staff.Email}' " +
                $"WHERE IDStaff = N'{staff.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }
    }
}
