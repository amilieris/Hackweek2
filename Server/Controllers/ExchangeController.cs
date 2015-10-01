using Server.HackTestWPF;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Server.Controllers
{
   // [Authorize]
    public class ExchangeController : ApiController
    {
        Location[] locations = new Location[]
        {
            new Location {Id = 1, Name = "Montevideo, UY"},
            new Location {Id = 2,  Name = "Cranbury, US" },
            new Location {Id = 3,  Name = "Sofia, BG"}
        };

        Room[] rooms = new Room[]
        {
            new Room {Id = "1", Name = "Mon A", Location = "1"},
            new Room {Id = "2",  Name = "Mon B", Location = "1" },
            new Room {Id = "3",  Name = "Mon C (No lifesize)", Location = "1"},
            new Room {Id = "4",  Name = "Mon D (No lifesize)", Location = "1"}
        };

        /*
        [Route("api/exchange/login")]
        [HttpPost]
        public IHttpActionResult Login(LoginCredentials credentials)
        {
            var loginResult = new LoginResult() { UserName = "Pepe"  };
            return Ok(loginResult);
        }*/

        [Route("api/exchange/bookRoom")]
        [HttpPost]
        public IHttpActionResult BookRoom(BookRoomParam param)
        {
            var service = new HackExchangeService();
            HackExchangeContext context = new HackExchangeContext()
            {
                Credentials = new NetworkCredential("reportplususer@infragistics.com", "%baG7cadence"),
                Endpoint = "https://outlook.office365.com/EWS/Exchange.asmx"
            };
            var rooms = service.GetRooms(context).Where(r => r.Location == "Montevideo, Uruguay" || r.Location == "Montevideo");
            RoomAvailability availability = service.GetRoomAvailability(context, "MON-A@infragistics.com", null, 60, true);

            // If there is room available
            var bookResult = new BookResult() { Booked = true, Room = rooms.First(), Start = DateTime.Now, End = DateTime.Now.AddMinutes(param.Time)};

            // If there is not, then it will find a possible slot.
            var negativeResult = new BookResult() { Booked = false, Room = rooms.First(), Start = DateTime.Now.AddMinutes(10), End = DateTime.Now.AddMinutes(param.Time) };

            return Ok(negativeResult);
        }

        public IHttpActionResult BookThisRoom(BookResult book)
        {
            book.Booked = true;

            return Ok(book);
        }

        [Route("api/exchange/getrooms/{locationId}")]
        [HttpGet]
        public IEnumerable<Room> GetRoomsForLocation(string locationId)
        {
            return rooms.Where(r => r.Location == locationId);
        }
        
        [Route("api/exchange/getlocations")]
        [HttpGet]
        public IEnumerable<Location> GetLocations()
        {
            return locations;
        }
    }

    public class LoginCredentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class BookRoomParam
    {
        public bool LifeSize { get; set; }

        public int Time { get; set; }

        public string Location { get; set; }
    }

    public class LoginResult
    {
        public string UserName { get; set; }
    }

    public class BookResult
    {
        public bool Booked { get; set; }

        public Room Room { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }

}
