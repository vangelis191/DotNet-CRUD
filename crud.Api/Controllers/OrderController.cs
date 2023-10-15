using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using crud.Domain;
using Bogus;
using Microsoft.EntityFrameworkCore.Internal;
using Bogus.DataSets;

using Microsoft.EntityFrameworkCore;
using crud.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace crud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly UserDbContext _context;
        public OrderController(UserDbContext context)
        {
            _context = context;
        }


        static ICollection<Order> orders = new List<Order>();
       

  
        [HttpGet()]
        public async Task<ActionResult<ICollection<Order>>> Get()
        {

            _context.Orders
             .Include(a => a.Books)                  // Include the Order entity related to OrderHistory     // Include the Customer entity related to Order
              .Include(a => a.Customer)
              .ThenInclude(a => a.Address)
             .FirstOrDefault();
             var orders = await _context.Orders.ToListAsync();

            return Ok(orders);
        }

   

        [HttpPost]
        public void CreateOrder()
        {

           


            Order order = new Order()
            {
                OrderID = 1,
                CustomerID = 1,
                Books = new List<Book>()
                {
                   new Book()
                   {
                        OrderID = 1,
                        Title = "Harry Potter",
                        Author = "j.k. rowling",
                        Genre = "Fantasy",
                        Quantity = 4,
                        PublicationYear = 1997,
                        ISBN = "11123",
                        Price = 4,
                        IsAvailableForRent = true

                   }
                },
            };
            _context.Add(order);
            _context.SaveChanges();

        }


        [HttpDelete()]
        public async Task<ActionResult<ICollection<Order>>> Delete(int orderID)
        {
            Console.WriteLine(orderID);

            var  res =  _context.Orders.FirstOrDefault(x => x.OrderID == orderID); ;
            if (res != null)
            {
                _context.Orders.Remove(res);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return NotFound();
            }

        }
    }

}