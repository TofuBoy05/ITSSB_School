namespace BakeryInventoryTesting
{
    [TestFixture]
    public class InventoryTests
    {
        private BakeryInventory _inventory;

        [SetUp]
        public void Setup()
        {
            _inventory = new BakeryInventory();
        }

        [Test]
        public void BakeryItem_PropertiesAreSetCorrectly()
        {
            var item = new BakeryItem
            {
                Name = "Baguette",
                Price = 2.50m,
                Quantity = 10,
                ExpirationDate = new DateTime(2025, 1, 1)
            };

            Assert.AreEqual("Baguette", item.Name);
            Assert.AreEqual(2.50m, item.Price);
            Assert.AreEqual(10, item.Quantity);
            Assert.AreEqual(new DateTime(2025, 1, 1), item.ExpirationDate);
        }

        [Test]
        public void BakeryItem_IsExpired_ReturnsTrueForPastDate()
        {
            var item = new BakeryItem
            {
                ExpirationDate = DateTime.Now.AddDays(-1)
            };

            Assert.IsTrue(item.IsExpired(), "Item with past date should be expired.");
        }

        [Test]
        public void BakeryItem_IsExpired_ReturnsFalseForFutureDate()
        {
            var item = new BakeryItem
            {
                ExpirationDate = DateTime.Now.AddDays(1)
            };

            Assert.IsFalse(item.IsExpired(), "Item with future date should not be expired.");
        }

        [Test]
        public void BakeryInventory_RemoveItems_RemovesExistingItem()
        {
            // Arrange
            _inventory.AddItem(new BakeryItem { Name = "Muffin", Price = 2.00m, Quantity = 2 });
            _inventory.AddItem(new BakeryItem { Name = "Tart", Price = 4.00m, Quantity = 1 });

            // Act
            _inventory.RemoveItems("Muffin");

            // Assert
            Assert.AreEqual(1, _inventory.GetItemCount(), "Inventory should only have 1 item left.");


        }

        [Test]
        public void BakeryInventory_RemoveItems_EdgeCase_DoesNothingIfItemDoesNotExist()
        {
            // Arrange
            _inventory.AddItem(new BakeryItem { Name = "Muffin", Price = 2.00m, Quantity = 2 });

            // Act - Trying to remove something that isn't there
            _inventory.RemoveItems("Ghost Bread");

            // Assert
            Assert.AreEqual(1, _inventory.GetItemCount(), "Inventory should still only have 1 item.");

        }

        [Test]
        public void BakeryInventory_GetTotalValue_CalculatesCorrectly()
        {
            // Arrange
            _inventory.AddItem(new BakeryItem { Name = "Muffin", Price = 2.00m, Quantity = 2 });
            _inventory.AddItem(new BakeryItem { Name = "Bread", Price = 4.00m, Quantity = 5 });

            // Act
            decimal total = _inventory.GetTotalValue();

            // Assert
            Assert.AreEqual(24.00m, total);

        }
    }
}