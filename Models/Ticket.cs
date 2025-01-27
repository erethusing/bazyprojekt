using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
public class Ticket
{
    public int Id { get; set; } // Primary key for the ticket
    public string Name { get; set; } // Event related to the ticket
    public int AvailableTickets { get; set; } // Number of tickets available
    public decimal Price { get; set; } // Price of the ticket
}}