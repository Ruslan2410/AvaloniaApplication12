using AvaloniaApplication12.Models;
using Npgsql;
using System;
using System.Collections.ObjectModel;

namespace AvaloniaApplication12
{
    public class People : ObservableCollection<Person>
    {
        public void AddPerson(int departmentNumber,
            int employeeNumber,
            string deskLocation,
            string firstName,
            string lastName)
        {
            this.Add(new Person
            {
                DepartmentNumber = departmentNumber,
                EmployeeNumber = employeeNumber,
                DeskLocation = deskLocation,
                FirstName = firstName,
                LastName = lastName
            });
        }

        public People()
        {
            FillPeople();
        }

        public ObservableCollection<Person> FillPeople()
        {
            this.Clear();

            // Змініть це на ваші дані підключення до PostgreSQL
            string connString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=Employee";
            using (NpgsqlConnection npgsql_conn = new NpgsqlConnection(connString))
            {
                npgsql_conn.Open();

                string sql = "SELECT * FROM Person ORDER BY LastName";

                using (NpgsqlCommand npgsql_cmd = new NpgsqlCommand(sql, npgsql_conn))
                {
                    try
                    {
                        using (NpgsqlDataReader reader = npgsql_cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int employeeNumber = reader.GetInt32(reader.GetOrdinal("EmployeeNumber"));
                                string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                                string lastName = reader.GetString(reader.GetOrdinal("LastName"));
                                int departmentNumber = reader.GetInt32(reader.GetOrdinal("DepartmentNumber"));
                                string deskLocation = reader.GetString(reader.GetOrdinal("DeskLocation"));

                                AddPerson(departmentNumber, employeeNumber, deskLocation, firstName, lastName);
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
