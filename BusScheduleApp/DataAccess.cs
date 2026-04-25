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
                        // Усередині методу OpenDbFile змініть частину з reader:
                        while (reader.Read())
                        {
                            bList.Add(new BusTrip(
                                (int)reader["reys_id"],
                                reader["punkt_vidpravku"].ToString(),
                                reader["punkt_priznachennya"].ToString(),
                                (int)reader["kiltist_mists_v_salonu"],
                                (System.DateTime)reader["vidpravku"],
                                (System.DateTime)reader["pributtia"]
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