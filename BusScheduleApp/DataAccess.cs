using System;
using System.Collections.Generic;
using Npgsql; // Обов'язкова бібліотека для PostgreSQL

namespace BusScheduleApp
{
    public class DataAccess
    {
        // Не забудьте вписати свій пароль від бази (наприклад, 1234)
        public string connStr = "Host=localhost; Username=postgres; Password=1234; Database=ROSKLADAVTOBUSIV";
        public List<BusTrip> bList = new List<BusTrip>();

        public DataAccess()
        {
            OpenDbFile();
        }

        // Завантаження всіх рейсів із БД
        private void OpenDbFile()
        {
            bList.Clear();
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM bus_schedule ORDER BY reys_id", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string from = reader.GetString(1);
                            string to = reader.GetString(2);
                            int seats = reader.GetInt32(3);
                            DateTime dep = reader.GetDateTime(4);
                            DateTime arr = reader.GetDateTime(5);

                            bList.Add(new BusTrip(id, from, to, seats, dep, arr));
                        }
                    }
                }
            }
        }

        // Метод збереження або оновлення рейсу в БД
        public void SaveTripToDb(BusTrip trip, bool isAdding)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                string query = "";

                if (isAdding)
                {
                    // Додавання нового рейсу
                    query = "INSERT INTO bus_schedule (reys_id, punkt_vidpravku, punkt_priznachennya, kiltist_mists_v_salonu, vidpravku, pributtia) VALUES (@id, @from, @to, @seats, @dep, @arr)";
                }
                else
                {
                    // Оновлення існуючого рейсу
                    query = "UPDATE bus_schedule SET punkt_vidpravku=@from, punkt_priznachennya=@to, kiltist_mists_v_salonu=@seats, vidpravku=@dep, pributtia=@arr WHERE reys_id=@id";
                }

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    // Безпечна передача параметрів
                    cmd.Parameters.AddWithValue("id", trip.reys_id);
                    cmd.Parameters.AddWithValue("from", trip.punkt_vidpravku);
                    cmd.Parameters.AddWithValue("to", trip.punkt_priznachennya);
                    cmd.Parameters.AddWithValue("seats", trip.kiltist_mists_v_salonu);
                    cmd.Parameters.AddWithValue("dep", trip.vidpravku);
                    cmd.Parameters.AddWithValue("arr", trip.pributtia);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}