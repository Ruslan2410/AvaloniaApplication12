using AvaloniaApplication12.Models;
using Npgsql;
using System;
using System.Collections.ObjectModel;

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

            // Змініть це на ваші дані підключення до PostgreSQL
            string connString = "Host=localhost:5432;Username=postgres;Password=postgres;Database=Employee";
            using (NpgsqlConnection npgsql_conn = new NpgsqlConnection(connString))
            {
                npgsql_conn.Open();

                string sql = "SELECT * FROM Logs";

                using (NpgsqlCommand npgsql_cmd = new NpgsqlCommand(sql, npgsql_conn))
                {
                    try
                    {
                        using (NpgsqlDataReader reader = npgsql_cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int logid = reader.GetInt32(reader.GetOrdinal("LogID"));
                                string action = reader.GetString(reader.GetOrdinal("Action"));
                                DateTime timestamp = reader.GetDateTime(reader.GetOrdinal("Timestamp"));

                                AddLog(logid, action, timestamp);
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
