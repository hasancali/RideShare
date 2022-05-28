using RideShare.Models.Base;

namespace RideShare.Models
{
    public class Trip2 : ModelBase
    {
        public long UserId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public DateTime Date { get; set; }
        public string Comments { get; set; }
        public int NumberSeats { get; set; }
        public bool Publish { get; set; }
    }
}
