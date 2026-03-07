using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Session4_API_2.Models;
using System.Net.Http.Json; // <-- Added this so ReadFromJsonAsync works properly!

namespace Session4_API_2.Controller
{
    [Route("api/")]
    [ApiController]
    public class CustomerMatching : ControllerBase
    {
        private readonly Session4BellCroissantContext _context;

        public CustomerMatching(Session4BellCroissantContext context)
        {
            _context = context;
        }

        [HttpGet("customers")]
        [ProducesResponseType(typeof(CustomerMatchDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomerMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomerFromMainApi([FromQuery] string email)
        {
            // 1. Validate Input
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Please provide an email to match.");

            // 2. Setup HttpClient to talk to the Admin API
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");

            // Add the Basic Auth header required by the Admin API
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // 3. Fetch the list from localhost:5000/api/customers
                // Note: Check if the Admin API endpoint is "api/customers" or "api/Orders/customers"
                var response = await client.GetAsync($"api/customers/{email}");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Could not reach the Admin API.");
                }

                // 4. Parse the JSON list of customers
                var allCustomers = await response.Content.ReadFromJsonAsync<List<CustomerMatchDto>>();

                // 5. "Match" the customer by email (Case-Insensitive)
                var match = allCustomers?.FirstOrDefault(c =>
                    c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

                if (match == null)
                {
                    // Swapped anonymous object with the CustomerMessageResponse class
                    return NotFound(new CustomerMessageResponse { Message = "No customer found with that email in the main system." });
                }

                // 6. Return the matching data to your mobile app
                return Ok(match);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Integration Error: {ex.Message}");
            }
        }

        [HttpGet("orders/{email}")]
        [ProducesResponseType(typeof(Order[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOrdersByEmail(string email)
        {

            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Please provide an email to match.");

            // 2. Setup HttpClient to talk to the Admin API
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5000/");

            // Add the Basic Auth header required by the Admin API
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // 3. Fetch the list from localhost:5000/api/customers
                // Note: Check if the Admin API endpoint is "api/customers" or "api/Orders/customers"
                var response = await client.GetAsync($"api/orders/user/{email}");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Could not reach the Admin API.");
                }

                // 4. Parse the JSON list of customers
                var allOrdersOfCustomer = await response.Content.ReadFromJsonAsync<List<Order>>();

                // 6. Return the matching data to your mobile app
                return Ok(allOrdersOfCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Integration Error: {ex.Message}");
            }

        }

        // --- DTOS FOR DOCUMENTATION ---

        // Simple DTO to handle the incoming JSON from Admin API
        public class CustomerMatchDto
        {
            public int CustomerId { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal TotalSpending { get; set; }
        }

        public class Order
        {
            // Matches JSON sample: transactionId, customerId, orderDate, totalAmount, status, paymentMethod, channel, storeId, promotionId, discountAmount
            public int TransactionId { get; set; }
            public int CustomerId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string Status { get; set; }
            public string PaymentMethod { get; set; }
            public string Channel { get; set; }
            public int StoreId { get; set; }
            public int PromotionId { get; set; }
            public decimal DiscountAmount { get; set; }

            // Nested objects
            public List<OrderItem>? OrderItems { get; set; }

        }

        public class OrderItem
        {
            // Matches JSON sample: orderItemId, transactionId, productId, quantity, price, product, transaction
            public int OrderItemId { get; set; }
            public int TransactionId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }

            public Product? Product { get; set; }

        }

        public class Product
        {
            // Matches JSON sample inside orderItems -> product
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
            public decimal Cost { get; set; }
            public string? Description { get; set; }
            public bool Seasonal { get; set; }
            public bool Active { get; set; }
            public DateTime? IntroducedDate { get; set; }
            public string? Ingredients { get; set; }

        }



        // Added for Swagger/Scalar to read the 404 response
        public class CustomerMessageResponse
        {
            public string Message { get; set; }
        }
    }
}