using RideShare.Models.Base;

namespace RideShare.Models
{
    public class Travelers : ModelBase
    {
        public long TripId { get; set; }
        public long UserId { get; set; }
    }
}
