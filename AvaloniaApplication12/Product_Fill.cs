using AvaloniaApplication12.Models;
using AvaloniaApplication12.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication12
{
    public class Product_Fill : ObservableCollection<Product>
    {
        public void AddProduct(
            int productid,
            string name,
            string category,
            decimal price,
            int quantity)
        {
            this.Add(new Product
            {
                ProductID = productid,
                Name = name,
                Category = category,
                Price = price,
                Quantity = quantity
            });
        }

        public Product_Fill()
        {
            Fill_Product();
        }

        public ObservableCollection<Product> Fill_Product()
        {
            this.Clear();

            string connString = String.Format("Data Source={0};New=False;Version=3", MainWindow.mDBPath);
            SQLiteConnection sqlite_conn = new SQLiteConnection(connString);
            sqlite_conn.Open();

            string sql = String.Format("Select * from Products;");

            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_conn);
            try
            {
                SQLiteDataReader reader = (SQLiteDataReader)sqlite_cmd.ExecuteReader();
                while (reader.Read())
                {
                    int productid = Convert.ToInt32(reader["productid"]);
                    string name = Convert.ToString(reader["name"]);
                    string category = Convert.ToString(reader["category"]);
                    decimal price = Convert.ToDecimal(reader["price"]);
                    int quantity = Convert.ToInt32(reader["quantity"]);

                    AddProduct(productid, name, category, price, quantity);
                }
                reader.Close();
                sqlite_conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return this;
        }
    }

}
