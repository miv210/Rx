using dbFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dbFirst.Services
{
    public class dbWorker
    {
        DemoContext demoContext;

        public async Task<int> GetCountAirport()
        {
            using(demoContext = new DemoContext())
            {
                int airports =  demoContext.Airports.Count();
                return airports;
            }
        }

        public async Task<AirportCrowded> GetCrowdedAirport()
        {
            using(demoContext = new DemoContext())
            {
                var countArrival = demoContext.Flights.GroupBy(f => f.ArrivalAirport).Select(p=> new {AirportCode = p.Key, Count = p.Count()}).ToList();
                var countDeparture = demoContext.Flights.GroupBy(f => f.DepartureAirport).Select(p => new { AirportCode = p.Key, Count = p.Count() }).ToList();

                List<AirportCrowded> crowList = new List<AirportCrowded>();


                foreach (var air in demoContext.Airports.ToList())
                {
                    AirportCrowded airportCrowded = new AirportCrowded
                    {
                        AirportCode = air.AirportCode,
                        Crowded = 0
                    };

                    var ct1 = countArrival.FirstOrDefault(p => p.AirportCode == airportCrowded.AirportCode);
                    var ct2 = countDeparture.FirstOrDefault(p => p.AirportCode == airportCrowded.AirportCode);


                    if (ct1 != null)
                    {
                        airportCrowded.Crowded += ct1.Count;
                    }
                    if(ct2 != null)
                    {
                        airportCrowded.Crowded += ct2.Count;
                    }

                    crowList.Add(airportCrowded);
                }
                crowList.Sort();
                var crowdedAirport = crowList[1] ;

                return crowdedAirport;
            }
        }

        public async Task<int> GetCountTicketAirport(string id)
        {
            using(demoContext = new DemoContext())
            {
                var ds = demoContext.Tickets.Include(p=> p.TicketFlights).ThenInclude(p=> p.Flight.DepartureAirport == id).Count();
                return ds;
            }
        }
        public void AddAirport(List<Airport> listAerports)
        {
            using(demoContext = new DemoContext())
            {
                demoContext.AddRange(listAerports);
            }
        }

        public void AddAirport(string Name)
        {
            using(demoContext = new DemoContext())
            {
                List<Airport> airports = new List<Airport>();
                Airport airport1 = new Airport
                {
                    AirportCode = "KYS",
                    AirportName = Name,
                    City = "Красноярск",
                    Timezone = "Eu/Красноярск"
                };
                airports.Add(airport1);

                Airport airport2 = new Airport
                {
                    AirportCode = "IJV",
                    AirportName = Name,
                    City = "Ижевск",
                    Timezone = "Eu/Ижевск"
                };
                airports.Add(airport2);

                demoContext.Airports.AddRange(airports);
                demoContext.SaveChanges();
            }           
        }

        public void AddFligt(string airportCode)
        {
            using (demoContext = new DemoContext()) 
            {
                Flight flight1 = new Flight
                {
                    FlightId = 23242342,
                    FlightNo = "PG9039",
                    ScheduledArrival = DateTime.UtcNow.AddHours(5),
                    ScheduledDeparture = DateTime.UtcNow,
                    DepartureAirport = airportCode,
                    ArrivalAirport = "LED",
                    Status = "Arrived",
                    AircraftCode = "321"
                };
                Ticket ticket1 = new Ticket
                {
                    BookRef = "8BBCD2",
                    TicketNo = "57347727423",
                    PassengerId = "4750 122452",
                    PassengerName = "VLADIMIR FROLOV"

                };

                TicketFlight ticketFlight1 = new TicketFlight
                {

                    FlightId = flight1.FlightId,
                    TicketNoNavigation = ticket1,
                    Flight = flight1,
                    TicketNo = ticket1.TicketNo,
                    FareConditions = "Business",
                    Amount = 18500
                };

                demoContext.TicketFlights.Add(ticketFlight1);
                demoContext.SaveChanges();
            }
        }
    }
}
