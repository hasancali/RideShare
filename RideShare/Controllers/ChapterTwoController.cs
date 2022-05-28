using Microsoft.AspNetCore.Mvc;
using RideShare.Models;
using RideShare.Models.Dto;

namespace RideShare.Controllers
{
    [ApiController]
    [Route("api/v1/RideShare/[Controller]")]
    public class ChapterTwoController : ControllerBase
    {
        public static List<Pointer> pointers = new();
        public static List<User> users = new();
        public static List<Travelers> Travelers = new();

        private long UserRegister()
        {
            var userCount = users.Count;
            var user = new User { Id = ++userCount };
            users.Add(user);
            return user.Id;
        }

        /// <summary>
        /// Kullanıcı sisteme seyahat planını Nereden, Nereye, Tarih ve Açıklama, Koltuk Sayısı bilgileri ile ekleyebilmeli
        /// </summary>
        /// <returns></returns>
        [HttpPost("TripRegister")]
        public IActionResult TripRegister([FromBody] Pointer pointer)
        {
            if (pointer.From < 0 || pointer.From > 199)
                return BadRequest("Sehirler 0 ila 199 arasında olabilir.");
            if (pointer.To < 0 || pointer.To > 199)
                return BadRequest("Sehirler 0 ila 199 arasında olabilir.");
            if (pointer.NumberSeats < 1)
                return BadRequest("KoltukSayisi 1'den kucuk olamaz.");

            if (pointer.UserId == 0)
            {
                pointer.UserId = UserRegister();
            }
            else
            {
                var varOlanKullanici = users.FirstOrDefault(x => x.Id == pointer.UserId);
                if (varOlanKullanici is null)
                    return BadRequest("Boyle bir kullanici bulunamadi. Id: " + pointer.UserId);
            }

            var pointerCount = pointers.Count;
            pointer.Id = ++pointerCount;
            pointers.Add(pointer);

            pointer.CreateRoute();
            pointers.Add(pointer);
            return Ok(pointer);
        }

        /// <summary>
        // Kullanıcılar Nereden ve Nereye bilgileri ile seyahat aradığında bu güzergahtan geçen tüm yayında olan seyahat planlarını bulabilmeli
        /// </summary>
        /// <returns></returns>
        [HttpGet("FindRoute")]
        public IActionResult FindRoute([FromQuery] int From, [FromQuery] int To)
        {
            var pointesList = pointers.Where(x => x.Publish && x.RouteCity.Contains(From) && x.RouteCity.Contains(To)).ToList();
            return Ok(pointesList);
        }
    }
}
