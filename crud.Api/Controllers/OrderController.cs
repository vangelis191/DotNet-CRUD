using System;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using crud.Domain;
using Bogus;
using Microsoft.EntityFrameworkCore.Internal;
using Bogus.DataSets;
using crud.Domain.ModelsDto;
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
        static ICollection<OrderHistory> OrderHistory = new List<OrderHistory>();

        [HttpGet("InitialiezeData")]
        public ActionResult<ICollection<Order>> InitialiezeData()
        {
            var _customerFaker = new Faker<Customer>()
                .RuleFor(o => o.CustomerID, f => 1)
             .RuleFor(o => o.FirstName, f => "testName")
             .RuleFor(o => o.LastName, f => "testSurname")
             .RuleFor(o => o.Address, f => new Domain.Address()
             {
                 City = "Lim",
                 PostalCode = "111",
                 Street = "testStreet",
                 CustomerId = 1,
             });
            var _bookFaker = new Faker<Book>()
                .RuleFor(b => b.BookID, f => 1)
                .RuleFor(b => b.Title, f => f.Random.Words(3)) // Example: Generate a random title with 3 words
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Genre, f => f.PickRandom("Science Fiction", "Mystery", "Romance", "Fantasy"))
                .RuleFor(b => b.PublicationYear, f => f.Random.Int(1950, 2022))
                .RuleFor(b => b.ISBN, f => f.Random.Guid().ToString())
                .RuleFor(b => b.Price, f => f.Random.Decimal(1, 100)) // Example: Generate a random price between 1 and 100
                .RuleFor(b => b.IsAvailableForRent, f => f.Random.Bool());

            var _orderFaker = new Faker<Order>()
            .RuleFor(o => o.OrderID, f => 1)
            .RuleFor(o => o.CustomerID, f => 1)
            .RuleFor(o => o.Books, f => new List<Book>
            {
            _bookFaker.Generate(),
                // Add more books as needed
            })
            .RuleFor(o => o.Customer, f => _customerFaker);




            

            //Separete Fake
           

           var _bookFaker2 = new Faker<Book>()
               .RuleFor(b => b.BookID, f => 2)
                .RuleFor(b => b.Title, f => f.Random.Words(3)) // Example: Generate a random title with 3 words
                .RuleFor(b => b.Author, f => f.Name.FullName())
                .RuleFor(b => b.Genre, f => f.PickRandom("Science Fiction", "Mystery", "Romance", "Fantasy"))
                .RuleFor(b => b.PublicationYear, f => f.Random.Int(1950, 2022))
                .RuleFor(b => b.ISBN, f => f.Random.Guid().ToString())
                .RuleFor(b => b.Price, f => f.Random.Decimal(1, 100)) // Example: Generate a random price between 1 and 100
                .RuleFor(b => b.IsAvailableForRent, f => f.Random.Bool());

            var _orderFaker2 = new Faker<Order>()
              .RuleFor(o => o.OrderID, f => 2)
              .RuleFor(o => o.CustomerID, f => 1)
              .RuleFor(o => o.Books, f => new List<Book>
              {
                _bookFaker2.Generate(),
                _bookFaker2.Generate(),
                 _bookFaker2.Generate(),
                  // Add more books as needed
              });

            var orderHistory = new Faker<OrderHistory>()
               .RuleFor(o => o.OrderHistoryID, f => 1)
               .RuleFor(o => o.OrderID, f => 1)
               .RuleFor(o => o.Timestamp, f => System.DateTime.UtcNow)
               .RuleFor(o => o.order, f => _orderFaker);

            var orderHistory2 = new Faker<OrderHistory>()
               .RuleFor(o => o.OrderHistoryID, f => 2)
               .RuleFor(o => o.OrderID, f => 2)
               .RuleFor(o => o.Timestamp, f => System.DateTime.UtcNow)
               .RuleFor(o => o.order, f => _orderFaker2);

            orders.Add(_orderFaker.Generate());
           // orders.Add(_orderFaker2.Generate());

            OrderHistory.Add(orderHistory);
           // OrderHistory.Add(orderHistory2);

            return Ok(orders);
        }

        [HttpGet()]
        public async Task<ActionResult<ICollection<Order>>> Get()
        {
            _context.Orders.Include(a => a.Books).FirstOrDefault();
            _context.Orders.Include(a => a.Customer).FirstOrDefault();
        
            var orders =  await _context.Orders.ToListAsync();
            
            return Ok(orders);
        }

        [HttpGet("OrderHistory")]
        public async Task<ActionResult<ICollection<OrderOrderHistory>>> GetOrderHistory()
        {
             _context.OrderHistories
             .Include(a => a.order)
              .ThenInclude(a => a.Books)
              .Include(a => a.order)
             .ThenInclude(o => o.Customer)        // Include the Customer entity related to Order
             .ThenInclude(c => c.Address)     // Include the Address entity related to Customer
             .FirstOrDefault();
            var ordersHistory = await _context.OrderHistories.ToListAsync();
            return Ok(ordersHistory);
        }

        [HttpPost]
        public  void CreateOrder()
        {
            /*
             foreach (var order in orders)
             {
                 _context.Orders.Add(order);
             }
            */
            Order order = new Order()
            {
                OrderID = 11,
                CustomerID = 1,
                Books = new List<Book>()
                {
                   new Book()
                   {
                        OrderId = 11,
                        Title = "Harry Potter",
                        Author = "j.k. rowling",
                        Genre = "Fantasy",
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
