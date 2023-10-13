using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using crud.Domain;
using Bogus;
using Microsoft.EntityFrameworkCore.Internal;
using Bogus.DataSets;

namespace crud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

       
       

        static ICollection<Order> orders = new List<Order>();

        [HttpGet("InitialiezeData")]
        public ActionResult<ICollection<Order>> InitialiezeData()
        {
            var _bookFaker = new Faker<Book>();
              var _orderFaker = new Faker<Order>()
                .RuleFor(o => o.OrderID, f => 1)
                .RuleFor(o => o.CustomerID, f => Guid.NewGuid())
                .RuleFor(o => o.OrderDate, f => f.Date.Past())
                .RuleFor(o => o.Books, f => new List<Book>
                {
                _bookFaker.Generate(),
                _bookFaker.Generate(),
                 _bookFaker.Generate(),
                    // Add more books as needed
                });

            _bookFaker
                .RuleFor(b => b.BookID, f => new Guid("447e6262-90c0-44f9-8810-c0077a63977c"))
                .RuleFor(b => b.Title, f => f.Random.Words(3)) // Example: Generate a random title with 3 words
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Genre, f => f.PickRandom("Science Fiction", "Mystery", "Romance", "Fantasy"))
                .RuleFor(b => b.PublicationYear, f => f.Random.Int(1950, 2022))
                .RuleFor(b => b.ISBN, f => f.Random.Guid().ToString())
                .RuleFor(b => b.Price, f => f.Random.Decimal(1, 100)) // Example: Generate a random price between 1 and 100
                .RuleFor(b => b.IsAvailableForRent, f => f.Random.Bool());


             var _orderFaker2 = new Faker<Order>()
                .RuleFor(o => o.OrderID, f => 2)
                .RuleFor(o => o.CustomerID, f => Guid.NewGuid())
                .RuleFor(o => o.OrderDate, f => f.Date.Past())
                .RuleFor(o => o.Books, f => new List<Book>
                {
                _bookFaker.Generate(),
                _bookFaker.Generate(),
                 _bookFaker.Generate(),
                    // Add more books as needed
                });

            _bookFaker = new Faker<Book>()
               .RuleFor(b => b.BookID, f => new Guid("457e6262-90c0-44f9-8810-c0077a63977c"))
                .RuleFor(b => b.Title, f => f.Random.Words(3)) // Example: Generate a random title with 3 words
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Genre, f => f.PickRandom("Science Fiction", "Mystery", "Romance", "Fantasy"))
                .RuleFor(b => b.PublicationYear, f => f.Random.Int(1950, 2022))
                .RuleFor(b => b.ISBN, f => f.Random.Guid().ToString())
                .RuleFor(b => b.Price, f => f.Random.Decimal(1, 100)) // Example: Generate a random price between 1 and 100
                .RuleFor(b => b.IsAvailableForRent, f => f.Random.Bool());

         
            orders.Add(_orderFaker.Generate());
            orders.Add(_orderFaker2.Generate());
            return Ok(orders);
        }

        [HttpGet()]
        public ActionResult<ICollection<Order>> Get()
        {
            
            return Ok(orders);
        }

        [HttpDelete()]
        public ActionResult<ICollection<Order>> Delete( int orderID)
        {
            Console.WriteLine(orderID);
       
            Order foundOrder = orders.FirstOrDefault(order => order.OrderID == orderID);
            if (foundOrder != null)
            {
                orders.Remove(foundOrder);
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
        
        }
    }

}
