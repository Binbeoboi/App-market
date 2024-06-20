using DoAn.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.DBconfig
{
    internal class WorkWithFile
    {

        public static List<UserPassword> ReadFileUser(string path)
        {
            List<UserPassword> lst = new List<UserPassword>();
            if (File.Exists(path))
            {
                string txt = File.ReadAllText(path);
                lst = JsonConvert.DeserializeObject<List<UserPassword>>(txt);
            }
            return lst;
        }

        public static void WriteFileUser(string path, List<UserPassword> lst)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(lst));
        }
    }
}
