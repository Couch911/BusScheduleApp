namespace BusScheduleApp
{
    public class Authorization
    {
        public static int logUser { get; set; } = 0;

        public int LogCheck(string login, string password)
        {
            if (login == "admin" && password == "admin")
            {
                logUser = 2;
                return 2;
            }
            if (login == "user" && password == "user")
            {
                logUser = 1;
                return 1;
            }
            return 0;
        }
    }
}