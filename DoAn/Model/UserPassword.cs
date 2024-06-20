using DoAn.DBconfig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Model
{
    public class UserPassword
    {
        public string ID {  get; set; }
        public string UserName {get;set;}
        public string Password {get;set;}

        string file = "Users.txt";
        public List<UserPassword> ListUsers()
        {
            return WorkWithFile.ReadFileUser(file);
        }
        //public object[] PropertiesToArray()
        //{
        //    return new object[] { UserName, Password};
        //}
        
    }
}
