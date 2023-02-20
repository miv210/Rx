using Microsoft.AspNetCore.Mvc;
using dbFirst.Services;
using dbFirst.Models;

namespace dbFirst.Controllers
{
    /// <summary>
    /// Тест контроллер
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        dbWorker db = new dbWorker();

        /// <summary>
        /// Возвращает количество аэропортов
        /// </summary>
        /// <returns></returns>
        [HttpGet("airport/count")]
        public async Task<ActionResult> GetCountAirport()
        {
            var countAirport =  db.GetCountAirport();
            var result = new Dictionary<string, int>
            {
                { "Count", countAirport }
            };

            return new JsonResult( result);
        }
        /// <summary>
        /// Добавление аэропорта
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("airport/{name}")]
        public async Task<ActionResult> AddAirport(string name)
        {
            db.AddAirport(name);
            return new OkResult();
        }

        /// <summary>
        /// Возвращает количество купленных билетов в аэропорту
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
            ReportService rep = new ReportService();
            rep.Create(counFl);
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
