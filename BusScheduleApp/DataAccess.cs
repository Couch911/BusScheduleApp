using Npgsql; // Підключаємо драйвер PostgreSQL 
using System.Collections.Generic;
using System;

namespace BusScheduleApp
{
    public class DataAccess
    {
        [cite_start]// Рядок з'єднання (пароль введіть свій) 
        public string connStr = "Host=localhost; Username=postgres; Password=ВАШ_ПАРОЛЬ; Database=ROSKLADAVTOBUSIV";
        public List<BusTrip> bList = new List<BusTrip>();

        public DataAccess()
        {
            OpenDbFile();
        }

        private void OpenDbFile()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();
                    string sql = "SELECT * FROM bus_schedule"; // Ваша таблиця 
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bList.Add(new BusTrip(
                                (int)reader["id"],
                                reader["route_number"].ToString(),
                                reader["destination"].ToString(),
                                (TimeSpan)reader["departure_time"],
                                (int)reader["free_seats"]
                            ));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Помилка БД: " + ex.Message);
            }
        }
    }
}