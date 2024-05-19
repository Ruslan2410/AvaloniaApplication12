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

            string connString = String.Format("Data Source={0};New=False;Version=3", MainWindow.mDBPath);
            SQLiteConnection sqlite_conn = new SQLiteConnection(connString);
            sqlite_conn.Open();

            string sql = String.Format("Select * from Person order by LastName;");

            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_conn);
            try
            {
                SQLiteDataReader reader = (SQLiteDataReader)sqlite_cmd.ExecuteReader();
                while (reader.Read())
                {
                    int EmployeeNumber = Convert.ToInt32(reader["EmployeeNumber"]);
                    string firstName = Convert.ToString(reader["firstName"]);
                    string lastName = Convert.ToString(reader["lastName"]);
                    int departmentNumber = Convert.ToInt32(reader["departmentNumber"]);
                    string deskLocation = Convert.ToString(reader["deskLocation"]);

                    AddPerson(departmentNumber, EmployeeNumber, deskLocation, firstName, lastName);
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
