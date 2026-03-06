using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryInventoryTesting
{
    /// <summary>
    /// Represents a single item in the bakery inventory.
    /// </summary>
    internal class BakeryItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate {  get; set; }

        /// <summary>
        /// Determines if the bakery item is expired based on the current date and time.
        /// </summary>
        /// <returns>True if expired, false otherwise.</returns>
        public bool IsExpired()
        {
            // If the current time is past the expiration date, it is expired.
            return DateTime.Now > ExpirationDate;
        }

    }
}
