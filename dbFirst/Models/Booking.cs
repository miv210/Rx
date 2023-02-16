using System;
using System.Collections.Generic;

namespace dbFirst.Models;

/// <summary>
/// Bookings
/// </summary>
public partial class Booking
{
    /// <summary>
    /// Booking number
    /// </summary>
    public string BookRef { get; set; } = null!;

    /// <summary>
    /// Booking date
    /// </summary>
    public DateTime BookDate { get; set; }

    /// <summary>
    /// Total booking cost
    /// </summary>
    public decimal TotalAmount { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
