using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IS_Domasna.Domain.DomainModels;
using IS_Domasna.Repository;
using IS_Domasna.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using IS_Domasna.Domain.Identity;
using IS_Domasna.Services.Interface;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using GemBox.Document;
using Microsoft.AspNetCore.Authorization;

namespace IS_Domasna.Controllers
{
    [Authorize(Roles = "Admin,Default")]
    public class OrdersController : Controller
    {
        
        private readonly IOrderService orderService;
        private readonly UserManager<ApplicationUser> userManager;

        public OrdersController(IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;  
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        // GET: Orders
        public IActionResult Index()
        {
            var orders = orderService.getAllOrders().Where(z => z.User.Id == userManager.GetUserAsync(HttpContext.User).Result.Id);
            return View(orders);
        }

        // GET: Orders/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderService.GetById(id.Value);
            var orderDetails = orderService.getOrderDetails(order);
            if (order == null)
            {
                return NotFound();
            }

            return View(orderDetails);
        }
       
        public IActionResult CreateInvoice(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderService.GetById(id);
            var orderDetails = orderService.getOrderDetails(order);
            if (order == null)
            {
                return NotFound();
            }

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");

            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", orderDetails.Id.ToString());
            document.Content.Replace("{{UserName}}", orderDetails.User.Email);

            StringBuilder sb = new StringBuilder();

            var total = 0.0;

            foreach (var item in order.TicketInOrders)
            {
                total += item.Quantity * item.Ticket.Price;
                sb.AppendLine(item.Ticket.MovieTitle + " with quantity of: " + item.Quantity + " and price of: $" + item.Ticket.Price);
            }

            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + total.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());


            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }

        

        /*// GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        /*private bool OrderExists(Guid id)
        {
            var order = order
            return orderService.getOrderDetails(id) != null;
        }*/
    }
}
