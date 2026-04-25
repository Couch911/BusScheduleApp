namespace BusScheduleApp
{
    [cite_start]// Клас, який описує поля вашої таблиці в БД 
    public class BusTrip
    {
        public int id { get; set; }
        public string routeNumber { get; set; }
        public string destination { get; set; }
        public System.TimeSpan departureTime { get; set; }
        public int freeSeats { get; set; }

        public BusTrip(int idNum, string rN, string dest, System.TimeSpan dT, int fS)
        {
            this.id = idNum;
            this.routeNumber = rN;
            this.destination = dest;
            this.departureTime = dT;
            this.freeSeats = fS;
        }
    }
}