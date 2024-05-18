using AvaloniaApplication12.Models;
using Npgsql;
using System;
using System.Collections.ObjectModel;

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

            // Змініть це на ваші дані підключення до PostgreSQL
            string connString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=Employee";
            using (NpgsqlConnection npgsql_conn = new NpgsqlConnection(connString))
            {
                npgsql_conn.Open();

                string sql = "SELECT * FROM Products";

                using (NpgsqlCommand npgsql_cmd = new NpgsqlCommand(sql, npgsql_conn))
                {
                    try
                    {
                        using (NpgsqlDataReader reader = npgsql_cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productid = reader.GetInt32(reader.GetOrdinal("productid"));
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                string category = reader.GetString(reader.GetOrdinal("category"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                                int quantity = reader.GetInt32(reader.GetOrdinal("quantity"));

                                AddProduct(productid, name, category, price, quantity);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return this;
        }
    }
}
