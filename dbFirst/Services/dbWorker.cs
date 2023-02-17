using dbFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace dbFirst.Services
{
    public class dbWorker
    {
        DemoContext demoContext;

        public int GetCountAirport()
        {
            using(demoContext = new DemoContext())
            {
                int airports =  demoContext.Airports.Count();
                return airports;
            }
        }

        public AirportCrowded GetCrowdedAirport()
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

        public int GetCountTicketAirport(string id)
        {
            using (demoContext = new DemoContext())
            {
                var ds = demoContext.Flights
                    .Include(p=> p.TicketFlights)
                    .ThenInclude(p=> p.TicketNoNavigation)
                    .Where(p=> p.DepartureAirport == id).SelectMany(p=> p.TicketFlights).Where(p=> p.TicketNoNavigation != null).Count();
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

        public void UpdateAircraft (string name)
        {
            using(demoContext = new DemoContext())
            {
                var aircraft = demoContext.Aircrafts.FirstOrDefault(p=> p.Model == name);
                if (aircraft != null)
                {
                    aircraft.Range = 8100;
                    demoContext.Aircrafts.Update(aircraft);
                    demoContext.SaveChanges();
                }
            }
        }

        public void SetStatus (string statusName)
        {
            using(demoContext = new DemoContext())
            {
                var flights = demoContext.Flights.
                    Include(p => p.DepartureAirportNavigation.AirportName == "Домодедово" && p.DepartureAirportNavigation.City == "Москва").
                    Where(p => p.Status == "Delayed").ToList();
                foreach(var flight in flights) 
                {
                    if (flight != null)
                    {
                        flight.Status= statusName;
                        demoContext.Flights.Update(flight);
                        demoContext.SaveChanges();
                    }
                }
            }
        }

        public List<PassengFl4> SerchName ()
        {
            using(demoContext = new DemoContext())
            {
                var passengers = demoContext.Tickets.GroupBy(p=> p.PassengerName).
                    Select(p => new PassengFl4 { Name = p.Key, ContFlight = p.Count ()}).
                    Where(p=> p.ContFlight > 3).ToList();
                return passengers;
            }
        }

        public List <CountFli> CountFlights()
        {
            using(demoContext = new DemoContext())
            {
                var countFligts = (from p in demoContext.Flights 
                                  join c in demoContext.Aircrafts on p.AircraftCode equals c.AircraftCode 
                                  where c.Range > 7500 
                                  group p by p.DepartureAirport into g
                                  select new CountFli {
                                    AirportCode = g.Key,
                                    CountFlight= g.Count()
                                  } ).OrderByDescending(f=> f.CountFlight).ToList();
                return countFligts;
            }
        }
    }
}
