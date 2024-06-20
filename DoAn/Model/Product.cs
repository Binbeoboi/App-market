using DoAn.DBconfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.Model
{
    public class Product
    {
         public string ID {get;set;}
         public string Name {get;set;}
         public DateTime EndTime{get;set;}
         public string DiscountID{get;set;}
         public int Quantity{get;set;}
         public double Price{get;set;}
         public string TypeID { get; set; }
         public string ProvideID { get; set; }
        [Browsable(false)] 
         
         public DateTime DiscountStartTime{get;set;}
        [Browsable(false)]
        public DateTime DiscountEndTime { get;set;}
        [Browsable(false)]
        public bool DiscountPercent{get;set;}
        [Browsable(false)]
        public bool DiscountMoney{ get; set; }
        [Browsable(false)]
        public int DiscountNumber { get; set; }
         public double TotalPrice
         {
            get
            {
                if(DiscountStartTime > DateTime.Now || DateTime.Now > DiscountEndTime)
                {
                    return Price;
                }
                else
                {
                    if (DiscountMoney)
                    {
                        return Price - DiscountNumber;
                    }
                    else if(DiscountPercent)
                    {
                        return Price - (Price * DiscountNumber) / 100; 
                    }
                    else
                    {
                        return Price;
                    }
                }
            }
         }
         public List<Product> ListProduct()
         {
            List<Product> listProduct = new List<Product>();
            string query = "SELECT Product.ProductID, Product.ProductName, Product.EndTime, Discount.DiscountName, Product.Quantity," +
                "Product.Price, TypeProduct.TypeName, Provide.NameProvide, Discount.StartTime, Discount.EndTime, Discount.DiscountPercent, Discount.DiscountMoney, Discount.DiscountNumber FROM Product INNER JOIN TypeProduct ON Product.TypeID = TypeProduct.TypeId " +
                "INNER JOIN Provide ON Product.ProvideID = Provide.IDProvide " +
                "INNER JOIN Discount ON Product.DiscountID = Discount.DiscountID;";

            try
            {
                using(SqlConnection cnn = Connection.GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(query, cnn);
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    while (sqlData.Read())
                    {
                        listProduct.Add(new Product
                        {
                            ID = sqlData.GetString(0),
                            Name = sqlData.GetString(1),
                            EndTime = DateTime.Parse(sqlData.GetString(2)),
                            DiscountID = sqlData.GetString(3),
                            Quantity = sqlData.GetInt32(4),
                            Price = sqlData.GetDouble(5),
                            TypeID = sqlData.GetString(6),
                            ProvideID = sqlData.GetString(7),
                            DiscountStartTime = DateTime.Parse(sqlData.GetString(8)),
                            DiscountEndTime = DateTime.Parse(sqlData.GetString(9)),
                            DiscountPercent = sqlData.GetBoolean(10),
                            DiscountMoney = sqlData.GetBoolean(11),
                            DiscountNumber = sqlData.GetInt32(12),
                        }) ;
                    }
                }
                return listProduct;
            }
            catch 
            {
                return null;
            }

         }

         Modify db = new Modify();
        public bool AddProduct(Product product)
        {
            string query = $"INSERT INTO Product VALUES (N'{product.ID}',N'{product.Name}',N'{product.EndTime.ToString("dd/MM/yyyy")}',N'{product.DiscountID}',{product.Quantity},{product.Price},N'{product.TypeID}', N'{product.ProvideID}');";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool RemoveProduct(string ID)
        {
            string query = $"DELETE FROM Product WHERE ProductID = N'{ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditProduct(Product product)
        {
            string query = $"UPDATE Product SET " +
                $"ProductID = N'{product.ID}'," +
                $"ProductName = N'{product.Name}'," +
                $"EndTime = N'{product.EndTime.ToString("dd/MM/yyyy")}'," +
                $"DiscountID = N'{product.DiscountID}'," +
                $"Quantity = {product.Quantity}," +
                $"Price = {product.Price}," +
                $"TypeID = N'{product.TypeID}', " +
                $"ProvideID = N'{product.ProvideID}' " +
                $"WHERE ProductID = N'{product.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }

        public bool EditProductByQuantity(Product product, int quantity)
        {
            
            string query = $"UPDATE Product SET " +
                $"Quantity = {product.Quantity - quantity} " +
                $"WHERE ProductID = N'{product.ID}'";

            int affected = db.Excute(query);
            return affected == 1;
        }
    }
}
