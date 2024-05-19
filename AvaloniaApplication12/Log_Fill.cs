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
    public class Log_Fill : ObservableCollection<Log>
    {
        public void AddLog(
            int logid,
            string action,
            DateTime timestamp)
        {
            this.Add(new Log
            {
                LogID = logid,
                Action = action,
                Timestamp = timestamp
            });
        }

        public Log_Fill()
        {
            Fill_Logs();
        }

        public ObservableCollection<Log> Fill_Logs()
        {
            this.Clear();

            string connString = String.Format("Data Source={0};New=False;Version=3", MainWindow.mDBPath);
            SQLiteConnection sqlite_conn = new SQLiteConnection(connString);
            sqlite_conn.Open();

            string sql = String.Format("Select * from Logs;");

            SQLiteCommand sqlite_cmd = new SQLiteCommand(sql, sqlite_conn);
            try
            {
                SQLiteDataReader reader = (SQLiteDataReader)sqlite_cmd.ExecuteReader();
                while (reader.Read())
                {
                    int logid = Convert.ToInt32(reader["LogID"]);
                    string action = Convert.ToString(reader["Action"]);
                    DateTime timestamp = Convert.ToDateTime(reader["Timestamp"]);

                    AddLog(logid, action, timestamp);
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
