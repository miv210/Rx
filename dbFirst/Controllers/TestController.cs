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

        [HttpGet("airport/count")]
        public async Task<ActionResult<int>> GetCountAirport()
        {
            var countAirport =  db.GetCountAirport();
            return countAirport;
        }
        [HttpPost("airport/{name}")]
        public async Task<ActionResult> AddAirport(string name)
        {
            db.AddAirport(name);
            return new OkResult();
        }


        [HttpGet("ticket/count/airport/{id:minlength(3)}")]
        public async Task<ActionResult<int>> GetCountTicketAirport(string id)
        {
            var coutTicket = db.GetCountTicketAirport(id);
            return coutTicket;
        }
        [HttpGet("ticket/flighted")]
        public async Task<ActionResult<IEnumerable<PassengFl4>>> SerchName()
        {
            var serch = db.SerchName();
            return serch;
        }


        [HttpPost("flight/add")]
        public async Task<ActionResult> AddFlihgt()
        {
            
            db.AddFligt("LED");
            return new OkResult();
        }
        [HttpPut("flight/setstatus/{statusName}")]
        public async Task<ActionResult> SetStatus(string statusName)
        {
            db.SetStatus(statusName);
            return new OkResult();
        }
        [HttpGet("flight/count")]
        public async Task<ActionResult<IEnumerable<CountFli>>> CountFlights()
        {
            var counFl = db.CountFlights();

            return counFl;
        }


        [HttpPut("aircraft/update/{name}")]
        public async Task<ActionResult> UpdateAircraft(string name)
        {
            db.UpdateAircraft(name);
            return new OkResult();
        }
    }
}
