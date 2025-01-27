using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models { 
public class Purchase
{
    public int Id { get; set; } // Primary key for the purchase
    public int TicketId { get; set; } // Foreign key for the ticket
    public DateTime PurchaseDate { get; set; } // Date of the purchase
    public int Quantity { get; set; } // Number of tickets bought
    public decimal TotalPrice { get; set; } // Total price for the purchase
    public string UserId { get; set; } // User who made the purchase, use IdentityUser ID
    public Ticket Ticket { get; set; } // Navigation property to Ticket

}


}