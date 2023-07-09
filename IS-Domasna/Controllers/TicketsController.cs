using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Repository;
using IS_Domasna.Services.Interface;
using IS_Domasna.Domain.DTO;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using IS_Domasna.Services.Implementation;
using System.IO;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;

namespace IS_Domasna.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService ticketService;
        private readonly ILogger<TicketsController> _logger;

        public TicketsController(ITicketService ticketService, ILogger<TicketsController> _logger)
        {
            this.ticketService = ticketService;
            this._logger = _logger;
        }

        // GET: Tickets
        public IActionResult Index()
        {
            return View(ticketService.GetAllTickets());
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetDetailsForTicket(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
        [Authorize(Roles = "Admin")]
        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("MovieTitle,MovieImage,MovieDescription,MovieAirTime,Id,Price")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Id = Guid.NewGuid();
                ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        [Authorize(Roles = "Admin")]
        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetDetailsForTicket(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("MovieTitle,MovieImage,MovieDescription,MovieAirTime,Id")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticketService.UpdateTicket(ticket);
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

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = ticketService.GetDetailsForTicket(id.Value);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid? id)
        {
            ticketService.DeleteTicket(id.Value);
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(Guid id)
        {
            return ticketService.GetDetailsForTicket(id) != null;
        }
        [Authorize(Roles = "Admin, Default")]

        public IActionResult AddTicketToCart(Guid id)
        {
            var result = this.ticketService.GetShoppingCartInfo(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Default")]

        public IActionResult AddTicketToCart(AddToShoppingCardDto model)
        {

            _logger.LogInformation("User Request -> Add Product in ShoppingCart and save changes in database!");


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this.ticketService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Tickets");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public FileContentResult ExportAllTickets()
        {
            var tickets = ticketService.GetAllTickets();
            string fileName = "Tickets.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workBook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workBook.Worksheets.Add("All Tickets");

                worksheet.Cell(1, 1).Value = "Ticket ID";
                worksheet.Cell(1, 2).Value = "Movie Title";
                worksheet.Cell(1, 3).Value = "Movie Description";
                worksheet.Cell(1, 4).Value = "Price";

                for (int i = 1; i <= tickets.Count(); i++)
                {
                    var item = tickets[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.MovieTitle;
                    worksheet.Cell(i + 1, 3).Value = item.MovieDescription;
                    worksheet.Cell(i + 1, 4).Value = item.Price.ToString();

                   

                }

                using (var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);

                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }

        }
    }
}
