using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using crud.Domain;
using Bogus;

namespace crud.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly Faker<Order> _orderFaker;
        private readonly Faker<Book> _bookFaker;


        public OrderController()
        {
            _orderFaker = new Faker<Order>()
                .RuleFor(o => o.OrderID, f => Guid.NewGuid())
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
                .RuleFor(b => b.BookID, f => Guid.NewGuid())
                .RuleFor(b => b.Title, f => f.Random.Words(3)) // Example: Generate a random title with 3 words
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Genre, f => f.PickRandom("Science Fiction", "Mystery", "Romance", "Fantasy"))
                .RuleFor(b => b.PublicationYear, f => f.Random.Int(1950, 2022))
                .RuleFor(b => b.ISBN, f => f.Random.Guid().ToString())
                .RuleFor(b => b.Price, f => f.Random.Decimal(1, 100)) // Example: Generate a random price between 1 and 100
                .RuleFor(b => b.IsAvailableForRent, f => f.Random.Bool());

            // You can configure additional properties as needed
        }

        [HttpGet()]
        public ActionResult<Order> Get()
        {
            var fakeOrder = _orderFaker.Generate();
            return Ok(fakeOrder);
        }
    }

}
