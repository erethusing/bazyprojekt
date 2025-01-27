using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Threading.Tasks;
using System.Linq;
using WebApplication1.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Threading.Tasks;
using Azure.Core;
using System.Net.Sockets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers
{
    [Authorize]
 
    public class TicketController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public TicketController(ApplicationDbContext context)
        {
            _context = context;
            

        }

        // Wyświetlanie listy biletów
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets.ToListAsync();
            return View(tickets);
        }

        // Widok dodawania nowego biletu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Dodanie nowego biletu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Bilet został dodany pomyślnie!";
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // Widok edytowania biletu
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // Edytowanie biletu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Bilet został zaktualizowany pomyślnie!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }
        // Widok usuwania biletu (potwierdzenie przed usunięciem)
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket); // Przekazujemy bilet do widoku Delete.cshtml
        }

        // Akcja usuwająca bilet
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Bilet został usunięty pomyślnie!";

            return RedirectToAction(nameof(Index)); // Przekierowanie do listy biletów
        }
        //dotego dobre i ostantie pamietaj
        // Akcja widoku zakupu biletu
        [HttpGet]
        public async Task<IActionResult> Purchase(int ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null)
            {
                return NotFound();
            }

            //var model = new Purchase
            //{
            //    TicketId = ticket.Id,
            //    Ticket = ticket,
            //    PurchaseDate = DateTime.Now
            //};

            return View(ticket);
        }

        // Akcja realizująca zakup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase(int TicketId, int Quantity)
        {
            var ticket = await _context.Tickets.FindAsync(TicketId);
            if (ticket == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // Sprawdzenie, czy dostępna jest wystarczająca liczba biletów
                if (ticket.AvailableTickets >= Quantity)
                {
                    // Zmniejszenie liczby dostępnych biletów
                    ticket.AvailableTickets -= Quantity;

                    // Dodanie zakupu do bazy
                    var purchase = new Purchase
                    {
                        TicketId = ticket.Id,
                        //UserId = _userManager.GetUserId(),
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Pobranie ID aktualnie zalogowanego użytkownika
                        Quantity = Quantity,
                        TotalPrice = Quantity * ticket.Price,
                        PurchaseDate = DateTime.Now
                    };

                    _context.Purchases.Add(purchase);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Zakup biletów zakończony pomyślnie!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Brak wystarczającej liczby biletów!";
                }

                return RedirectToAction("Index", "Ticket");

            }

            return View(ticket);
        }

        // Akcja do wyświetlania biletów zalogowanego użytkownika
        public async Task<IActionResult> MyTickets()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Pobranie ID zalogowanego użytkownika

            var purchases = await _context.Purchases
                .Where(p => p.UserId == userId)
                .Include(p => p.Ticket) // Załaduj powiązane bilety
                .ToListAsync();

            return View(purchases);
        }
        public IActionResult About()
        {
            return View();
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
