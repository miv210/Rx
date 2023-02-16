using System;
using System.Collections.Generic;

namespace dbFirst.Models;

/// <summary>
/// Aircrafts (internal data)
/// </summary>
public partial class AircraftsDatum
{
    /// <summary>
    /// Aircraft code, IATA
    /// </summary>
    public string AircraftCode { get; set; } = null!;

    /// <summary>
    /// Aircraft model
    /// </summary>
    public string Model { get; set; } = null!;

    /// <summary>
    /// Maximal flying distance, km
    /// </summary>
    public int Range { get; set; }

    public virtual ICollection<Flight> Flights { get; } = new List<Flight>();

    public virtual ICollection<Seat> Seats { get; } = new List<Seat>();
}
