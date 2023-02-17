using Microsoft.AspNetCore.Mvc;
using dbFirst.Services;
using dbFirst.Models;

namespace dbFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        dbWorker db = new dbWorker();

        [HttpGet("getCountAirport")]
        public async Task<ActionResult<int>> GetCountAirport()
        {
            var countAirport =  db.GetCountAirport();
            return countAirport;
        }

        [HttpGet("GetCountTicketAirport/{id:minlength(3)}")]
        public async Task<ActionResult<int>> GetCountTicketAirport(string id)
        {
            var coutTicket = db.GetCountTicketAirport(id);
            return coutTicket;
        }

        [HttpPost("AddAirport/{name}")]
        public async Task<ActionResult> AddAirport(string name)
        {
            db.AddAirport(name);
            return new OkResult();
        }

        [HttpPost("addFlight")]
        public async Task<ActionResult> AddFlihgt()
        {
            
            db.AddFligt("LED");
            return new OkResult();
        }

        [HttpPost("UpdateAircraft/{name}")]
        public async Task<ActionResult> UpdateAircraft(string name)
        {
            db.UpdateAircraft(name);
            return new OkResult();
        }

        [HttpPost("SetStatus/{statusName}")]
        public async Task<ActionResult> SetStatus(string statusName)
        {
            db.SetStatus(statusName);
            return new OkResult();
        }

        [HttpGet("SerchName")]
        public async Task<ActionResult<IEnumerable<PassengFl4>>> SerchName()
        {
            var serch = db.SerchName();
            return serch;
        }

        [HttpGet("CountFlights")]
        public async Task<ActionResult<IEnumerable<CountFli>>> CountFlights()
        {
            var counFl = db.CountFlights();

            return counFl;
        }
    }
}
