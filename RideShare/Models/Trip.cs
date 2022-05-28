using RideShare.Models.Base;

namespace RideShare.Models
{
    public class Trip : ModelBase
    {
        public long UserId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int NumberSeats { get; set; }
        public bool Publish { get; set; }
    }
}
