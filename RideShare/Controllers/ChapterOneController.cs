using Microsoft.AspNetCore.Mvc;
using RideShare.Models;
using RideShare.Models.Dto;

namespace RideShare.Controllers
{
    [ApiController]
    [Route("api/v1/RideShare/[Controller]")]
    public class ChapterOneController : Controller
    {
        public static List<User> users = new();
        public static List<Trip> trips = new();
        public static List<Travelers> travelers = new();

        private long UserRegister()
        {
            var userCount = users.Count;
            var user = new User { Id = ++userCount };
            users.Add(user);
            return user.Id;
        }

        /// <summary>
        /// Kullanıcı sisteme seyahat planını Nereden, Nereye, Tarih ve Açıklama, Koltuk Sayısı bilgileri ile ekleyebilmeli Kullanıcı Yoksa UserId 0 Geçilmeli
        /// </summary>
        /// <returns></returns>
        [HttpPost("TripRegister")]
        public IActionResult TripRegister([FromBody] Trip trip)
        {
            if (trip.NumberSeats < 1)
                return BadRequest("Koltuk Sayısı 1'den küçük olamaz.");
            if (string.IsNullOrEmpty(trip.From))
                return BadRequest("Nereden gidileceği boş geçilemez.");
            if (string.IsNullOrEmpty(trip.To))
                return BadRequest("Nereye gidileceği boş geçilemez.");

            if (trip.UserId == 0)
            {
                trip.UserId = UserRegister();
            }
            else
            {
                var user = users.FirstOrDefault(x => x.Id == trip.Id);
                if (user is null)
                    return BadRequest("Kullanıcı bulunmadı. Id: " + trip.UserId);
            }

            var tripCount = trips.Count;
            trip.Id = ++tripCount;
            trips.Add(trip);

            return Ok(trip);
        }

        /// <summary>
        /// Kullanıcı tanımladığı seyahat planını yayına alabilmeli ve yayından kaldırabilmeli
        /// </summary>
        /// <returns></returns>
        [HttpPut("TripPublishUpdate")]
        public IActionResult TripPublishUpdate([FromBody] DtoTripPublishChange dtoTripPublishChange)
        {
            var seyahat = trips.FirstOrDefault(x => x.Id == dtoTripPublishChange.TripId);
            if (seyahat is null)
                return BadRequest("Seyahat bulunmadı. Id: " + dtoTripPublishChange.TripId);
            seyahat.Publish = dtoTripPublishChange.Publish;

            return Ok();
        }

        /// <summary>
        /// Kullanıcı tanımladığı seyahat planını yayına alabilmeli ve yayından kaldırabilmeli
        /// </summary>
        /// <returns></returns>
        [HttpGet("TravelSearch/{from}/{to}")]
        public IActionResult TravelSearch(string from, string to)
        {
            var fromR = !string.IsNullOrEmpty(from);
            var toR = !string.IsNullOrEmpty(to);

            if (!fromR && !toR)
                return BadRequest("Nerden ve nereye bilgisi boş gönderilemez.");

            var result =
            trips.Where(x => x.Publish &&
                                    (!fromR || x.From.ToLower().Contains(from.ToLower())) &&
                                    (!toR || x.To.ToLower().Contains(to.ToLower()))).ToList();

            return Ok(result);
        }

        /// <summary>
        /// Kullanıcılar sistemdeki yayında olan seyahat planlarını Nereden ve Nereye bilgileri ile aratabilmeli
        /// </summary>
        /// <returns></returns>
        [HttpPost("TravelJoin")]
        public IActionResult TravelJoin([FromBody] DtoTripJoin dtoTripJoin)
        {
            var trip = trips.FirstOrDefault(x => x.Id == dtoTripJoin.TripId && x.Publish);
            if (trip == null)
                return BadRequest("Seyahat bulunamadı. Id: " + dtoTripJoin.TripId);

            if (dtoTripJoin.UserId == 0)
                dtoTripJoin.UserId = UserRegister();
            else
            {
                var isUser = users.Any(x => x.Id == dtoTripJoin.UserId);
                if (!isUser)
                    return BadRequest("Kullanıcı bulunamadı Id: " + dtoTripJoin.UserId);
                if (trip.UserId == dtoTripJoin.UserId)
                    return BadRequest("Size ait olan seyahate katılımcı olamazsınız.");
            }

            var existingParticipants = travelers.Where(x => x.TripId == dtoTripJoin.TripId).ToList();
            if (existingParticipants.Any(x => x.UserId == dtoTripJoin.UserId))
                return Ok("Seyahate zaten kayıtlısınız.");

            var numberOfOccupiedSeats = existingParticipants.Count;
            if (trip.NumberSeats == numberOfOccupiedSeats)
                return BadRequest("Seyahatteki tum koltuklar dolu.");

            var travelNumberOfAttendees = travelers.Count;
            var travelAgent = new Travelers
            {
                Id = ++travelNumberOfAttendees,
                UserId = dtoTripJoin.UserId,
                TripId = dtoTripJoin.TripId
            };

            travelers.Add(travelAgent);

            return Ok(travelAgent);

        }
    }
}
