namespace CarConnectAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string NameComprador { get; set; }
        public string PhoneComprador { get; set; }
        public DateTime FechaCita { get; set; }

    }
}
