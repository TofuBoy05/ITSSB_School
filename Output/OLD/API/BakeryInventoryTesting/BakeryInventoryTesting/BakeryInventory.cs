using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryInventoryTesting
{
    /// <summary>
    /// Manages a collection of bakery items.
    /// </summary>
    internal class BakeryInventory
    {
        private List<BakeryItem> _items = new List<BakeryItem>();

        /// <summary>
        /// Adds a new item to the inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddItem(BakeryItem item)
        {
            if(item == null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
        }

        /// <summary>
        /// Removes all the items from the inventory that match the given name.
        /// </summary>
        /// <param name="name"></param>
        public void RemoveItems(string name)
        {
            // Edge case: RemoveAll safely handles if the item does not exist (returns 0).
            // StringComparison.OrdinalIgnoreCase ensures "Croissant" and "croissant" both match.
            _items.RemoveAll(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Calculates the total financial value of all items currently in inventory.
        /// </summary>
        public decimal GetTotalValue()
        {
            return _items.Sum(i => i.Price * i.Quantity);
        }

        /// <summary>
        /// Helper to get item count.
        /// </summary>
        /// <returns></returns>
        public int GetItemCount()
        {
            return _items.Count; 
        }
    }
}
