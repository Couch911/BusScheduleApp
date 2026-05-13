namespace BusScheduleApp
{
    public class BusTrip
    {
        public int reys_id { get; set; }
        public string punkt_vidpravku { get; set; }
        public string punkt_priznachennya { get; set; }
        public int kiltist_mists_v_salonu { get; set; }
        public System.DateTime vidpravku { get; set; }
        public System.DateTime pributtia { get; set; }

        public BusTrip(int id, string from, string to, int seats, System.DateTime dep, System.DateTime arr)
        {
            this.reys_id = id;
            this.punkt_vidpravku = from;
            this.punkt_priznachennya = to;
            this.kiltist_mists_v_salonu = seats;
            this.vidpravku = dep;
            this.pributtia = arr;
        }
    }
}