using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BellCroissantDesktop
{
    public partial class LoyaltyManagement : Form
    {
        private List<CustomerWithLoyalty> allCustomers = new List<CustomerWithLoyalty>();
        private List<CustomerWithLoyalty> filteredCustomers = new List<CustomerWithLoyalty>();
        private CustomerWithLoyalty? selectedCustomer = null;
        
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;
        private string sortColumn = "CustomerId";
        private bool sortAscending = false;

        public LoyaltyManagement()
        {
            InitializeComponent();
            dgvCustomers.AutoGenerateColumns = false;
            cmbPageSize.SelectedIndex = 0; // 10 items per page
        }

        private async void LoyaltyManagement_Load(object sender, EventArgs e)
        {
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // Fetch customers from API
                var customers = await client.GetFromJsonAsync<List<Customer>>("api/Customers");
                
                // Fetch loyalty data (we'll create this endpoint or use local database)
                var loyaltyData = await client.GetFromJsonAsync<List<LoyaltyProgram>>("api/LoyaltyPrograms");

                if (customers != null)
                {
                    allCustomers = customers.Select(c =>
                    {
                        var loyalty = loyaltyData?.FirstOrDefault(l => l.CustomerId == c.CustomerId);
                        return new CustomerWithLoyalty
                        {
                            CustomerId = c.CustomerId,
                            FirstName = c.FirstName,
                            LastName = c.LastName,
                            Email = c.Email,
                            MembershipStatus = c.MembershipStatus,
                            TotalSpending = c.TotalSpending,
                            Points = loyalty?.Points ?? 0,
                            MembershipTier = loyalty?.MembershipTier ?? "Basic",
                            JoinDate = c.JoinDate
                        };
                    }).ToList();

                    filteredCustomers = new List<CustomerWithLoyalty>(allCustomers);
                    SortCustomers();
                    UpdatePagination();
                    DisplayCurrentPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SortCustomers()
        {
            filteredCustomers = sortColumn switch
            {
                "CustomerId" => sortAscending 
                    ? filteredCustomers.OrderBy(c => c.CustomerId).ToList()
                    : filteredCustomers.OrderByDescending(c => c.CustomerId).ToList(),
                "FirstName" => sortAscending
                    ? filteredCustomers.OrderBy(c => c.FirstName).ToList()
                    : filteredCustomers.OrderByDescending(c => c.FirstName).ToList(),
                "LastName" => sortAscending
                    ? filteredCustomers.OrderBy(c => c.LastName).ToList()
                    : filteredCustomers.OrderByDescending(c => c.LastName).ToList(),
                "Email" => sortAscending
                    ? filteredCustomers.OrderBy(c => c.Email).ToList()
                    : filteredCustomers.OrderByDescending(c => c.Email).ToList(),
                "MembershipStatus" => sortAscending
                    ? filteredCustomers.OrderBy(c => c.MembershipStatus).ToList()
                    : filteredCustomers.OrderByDescending(c => c.MembershipStatus).ToList(),
                "Points" => sortAscending
                    ? filteredCustomers.OrderBy(c => c.Points).ToList()
                    : filteredCustomers.OrderByDescending(c => c.Points).ToList(),
                "TotalSpending" => sortAscending
                    ? filteredCustomers.OrderBy(c => c.TotalSpending).ToList()
                    : filteredCustomers.OrderByDescending(c => c.TotalSpending).ToList(),
                _ => filteredCustomers
            };
        }

        private void UpdatePagination()
        {
            totalPages = (int)Math.Ceiling(filteredCustomers.Count / (double)pageSize);
            if (totalPages == 0) totalPages = 1;
            if (currentPage > totalPages) currentPage = totalPages;
            if (currentPage < 1) currentPage = 1;

            lblPageInfo.Text = $"Page {currentPage} of {totalPages} ({filteredCustomers.Count} total)";
            btnPrevPage.Enabled = currentPage > 1;
            btnNextPage.Enabled = currentPage < totalPages;
        }

        private void DisplayCurrentPage()
        {
            var pageData = filteredCustomers
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = pageData;
            UpdatePagination();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                filteredCustomers = new List<CustomerWithLoyalty>(allCustomers);
            }
            else
            {
                filteredCustomers = allCustomers.Where(c =>
                    c.CustomerId.ToString().Contains(searchTerm) ||
                    (c.FirstName?.ToLower().Contains(searchTerm) ?? false) ||
                    (c.LastName?.ToLower().Contains(searchTerm) ?? false) ||
                    (c.Email?.ToLower().Contains(searchTerm) ?? false)
                ).ToList();
            }

            currentPage = 1;
            SortCustomers();
            DisplayCurrentPage();
        }

        private void dgvCustomers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string columnName = dgvCustomers.Columns[e.ColumnIndex].DataPropertyName;
            
            if (columnName == sortColumn)
            {
                sortAscending = !sortAscending;
            }
            else
            {
                sortColumn = columnName;
                sortAscending = true;
            }

            SortCustomers();
            DisplayCurrentPage();

            // Update sort glyph
            foreach (DataGridViewColumn col in dgvCustomers.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
            dgvCustomers.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = 
                sortAscending ? SortOrder.Ascending : SortOrder.Descending;
        }

        private void dgvCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                selectedCustomer = dgvCustomers.SelectedRows[0].DataBoundItem as CustomerWithLoyalty;
                LoadCustomerDetails();
            }
            else
            {
                selectedCustomer = null;
                pnlCustomerDetails.Enabled = false;
            }
        }

        private void LoadCustomerDetails()
        {
            if (selectedCustomer == null) return;

            pnlCustomerDetails.Enabled = true;
            txtCustomerId.Text = selectedCustomer.CustomerId.ToString();
            txtFirstName.Text = selectedCustomer.FirstName;
            txtLastName.Text = selectedCustomer.LastName;
            txtEmail.Text = selectedCustomer.Email;
            txtMembershipStatus.Text = selectedCustomer.MembershipStatus;
            numLoyaltyPoints.Value = selectedCustomer.Points;

            // Show redeem button if points >= 1000
            btnRedeemRewards.Visible = selectedCustomer.Points >= 1000;
        }

        private async void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null) return;

            int newPoints = (int)numLoyaltyPoints.Value;

            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                var loyaltyUpdate = new LoyaltyProgram
                {
                    CustomerId = selectedCustomer.CustomerId,
                    Points = newPoints,
                    MembershipTier = selectedCustomer.MembershipTier
                };

                // Try to check if loyalty record exists first
                var checkResponse = await client.GetAsync($"api/LoyaltyPrograms/{selectedCustomer.CustomerId}");

                HttpResponseMessage response;
                if (checkResponse.IsSuccessStatusCode)
                {
                    // Record exists, update it
                    response = await client.PutAsJsonAsync($"api/LoyaltyPrograms/{selectedCustomer.CustomerId}", loyaltyUpdate);
                }
                else
                {
                    // Record doesn't exist, create it
                    response = await client.PostAsJsonAsync("api/LoyaltyPrograms", loyaltyUpdate);
                }

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Loyalty points updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    selectedCustomer.Points = newPoints;
                    await LoadCustomers();
                    DisplayCurrentPage();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Failed to update points: {response.StatusCode}\n{error}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating points: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadCustomerDetails(); // Reload original values
        }

        private async void btnRecalculatePoints_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null) return;

            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // Fetch customer's orders
                var allOrders = await client.GetFromJsonAsync<List<Order>>("api/Orders");
                var customerOrders = allOrders?.Where(o => o.CustomerId == selectedCustomer.CustomerId).ToList() ?? new List<Order>();

                // Fetch promotions for bonus points
                var promotions = await client.GetFromJsonAsync<List<Promotion>>("api/Promotions");

                // Calculate points
                var calculation = CalculatePoints(selectedCustomer, customerOrders, promotions ?? new List<Promotion>());

                // Show breakdown dialog
                using var breakdownForm = new PointsCalculationDialog(calculation);
                if (breakdownForm.ShowDialog() == DialogResult.OK)
                {
                    // Update points
                    numLoyaltyPoints.Value = calculation.TotalPoints;
                    btnSaveChanges_Click(this, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recalculating points: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private PointsCalculation CalculatePoints(CustomerWithLoyalty customer, List<Order> orders, List<Promotion> promotions)
        {
            var calc = new PointsCalculation { CustomerName = $"{customer.FirstName} {customer.LastName}" };

            // Points per tier
            int pointsPerTenEuros = customer.MembershipStatus switch
            {
                "Silver" => 12,
                "Gold" => 15,
                _ => 10 // Basic
            };

            // Calculate points from orders
            foreach (var order in orders)
            {
                int orderPoints = (int)(order.TotalAmount / 10) * pointsPerTenEuros;
                
                // Check if order was during promotion
                bool duringPromotion = promotions.Any(p =>
                {
                    var orderDate = DateOnly.FromDateTime(order.OrderDate);
                    return orderDate >= p.StartDate && orderDate <= p.EndDate;
                });

                if (duringPromotion)
                {
                    orderPoints += 5; // Bonus points
                }

                calc.OrderBreakdown.Add(new OrderPointsBreakdown
                {
                    OrderId = order.TransactionId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    BasePoints = orderPoints - (duringPromotion ? 5 : 0),
                    PromotionBonus = duringPromotion ? 5 : 0,
                    TotalPoints = orderPoints
                });

                calc.TotalPoints += orderPoints;
            }

            // Anniversary bonus
            if (customer.JoinDate.HasValue)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                var joinDate = customer.JoinDate.Value;
                
                if (today.Month == joinDate.Month && today.Day == joinDate.Day && today.Year > joinDate.Year)
                {
                    calc.AnniversaryBonus = 25;
                    calc.TotalPoints += 25;
                }
            }

            return calc;
        }

        private async void btnRedeemRewards_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null || selectedCustomer.Points < 1000) return;

            using var rewardDialog = new RewardRedemptionDialog(selectedCustomer);
            if (rewardDialog.ShowDialog() == DialogResult.OK)
            {
                await ProcessReward(rewardDialog.SelectedReward, selectedCustomer);
            }
        }

        private async Task ProcessReward(RewardType reward, CustomerWithLoyalty customer)
        {
            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // Deduct 1000 points
                int newPoints = customer.Points - 1000;

                if (reward == RewardType.TierUpgrade)
                {
                    // Upgrade membership tier
                    string newTier = customer.MembershipStatus switch
                    {
                        "Basic" => "Silver",
                        "Silver" => "Gold",
                        _ => customer.MembershipStatus
                    };

                    if (newTier == customer.MembershipStatus)
                    {
                        MessageBox.Show("You are already at the highest tier!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Update customer membership
                    var customerUpdate = await client.GetFromJsonAsync<Customer>($"api/Customers/{customer.CustomerId}");
                    if (customerUpdate != null)
                    {
                        customerUpdate.MembershipStatus = newTier;
                        await client.PutAsJsonAsync($"api/Customers/{customer.CustomerId}", customerUpdate);
                    }

                    customer.MembershipStatus = newTier;
                    customer.MembershipTier = newTier;
                }
                else
                {
                    // Create promotion for discount
                    var promotion = new Promotion
                    {
                        PromotionName = $"Reward for {customer.FirstName} {customer.LastName}",
                        DiscountType = reward == RewardType.FiveEuroDiscount ? "FixedAmount" : "Percentage",
                        DiscountValue = reward == RewardType.FiveEuroDiscount ? 5 : 10,
                        ApplicableProducts = null, // All products
                        StartDate = DateOnly.FromDateTime(DateTime.Today),
                        EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                        MinimumOrderValue = null,
                        Priority = 99,
                        QuantityBasedRules = null
                    };

                    await client.PostAsJsonAsync("api/Promotions", promotion);
                }

                // Update loyalty points
                var loyaltyUpdate = new LoyaltyProgram
                {
                    CustomerId = customer.CustomerId,
                    Points = newPoints,
                    MembershipTier = customer.MembershipTier
                };

                // Check if loyalty record exists
                var checkResponse = await client.GetAsync($"api/LoyaltyPrograms/{customer.CustomerId}");

                if (checkResponse.IsSuccessStatusCode)
                {
                    // Record exists, update it
                    await client.PutAsJsonAsync($"api/LoyaltyPrograms/{customer.CustomerId}", loyaltyUpdate);
                }
                else
                {
                    // Record doesn't exist, create it
                    await client.PostAsJsonAsync("api/LoyaltyPrograms", loyaltyUpdate);
                }

                MessageBox.Show("Reward redeemed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                await LoadCustomers();
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error redeeming reward: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayCurrentPage();
            }
        }

        private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(cmbPageSize.SelectedItem?.ToString(), out int newSize))
            {
                pageSize = newSize;
                currentPage = 1;
                DisplayCurrentPage();
            }
        }

        private async void btnPopulateTestData_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "This will assign random loyalty points (50-1500) to 250 randomly selected customers.\n\n" +
                "This is a TEST DATA operation. Continue?",
                "Populate Test Data",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            btnPopulateTestData.Enabled = false;
            btnPopulateTestData.Text = "Processing...";

            using var client = new HttpClient { BaseAddress = new Uri("http://localhost:5000/") };
            client.DefaultRequestHeaders.Add("Authorization", "Basic c3RhZmY6QkNMeW9uMjAyNA==");

            try
            {
                // Fetch all customers
                var customers = await client.GetFromJsonAsync<List<Customer>>("api/Customers");
                if (customers == null || customers.Count == 0)
                {
                    MessageBox.Show("No customers found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Randomly select 250 customers (or all if less than 250)
                var random = new Random();
                int count = Math.Min(250, customers.Count);
                var selectedCustomers = customers.OrderBy(x => random.Next()).Take(count).ToList();

                int successCount = 0;
                int failCount = 0;

                foreach (var customer in selectedCustomers)
                {
                    try
                    {
                        // Generate random points between 50 and 1500
                        int randomPoints = random.Next(50, 1501);

                        var loyaltyUpdate = new LoyaltyProgram
                        {
                            CustomerId = customer.CustomerId,
                            Points = randomPoints,
                            MembershipTier = customer.MembershipStatus ?? "Basic"
                        };

                        // Check if loyalty record exists
                        var checkResponse = await client.GetAsync($"api/LoyaltyPrograms/{customer.CustomerId}");

                        HttpResponseMessage response;
                        if (checkResponse.IsSuccessStatusCode)
                        {
                            // Record exists, update it
                            response = await client.PutAsJsonAsync($"api/LoyaltyPrograms/{customer.CustomerId}", loyaltyUpdate);
                        }
                        else
                        {
                            // Record doesn't exist, create it
                            response = await client.PostAsJsonAsync("api/LoyaltyPrograms", loyaltyUpdate);
                        }

                        if (response.IsSuccessStatusCode)
                        {
                            successCount++;
                        }
                        else
                        {
                            failCount++;
                        }
                    }
                    catch
                    {
                        failCount++;
                    }
                }

                MessageBox.Show(
                    $"Test data population complete!\n\n" +
                    $"Successfully updated: {successCount} customers\n" +
                    $"Failed: {failCount} customers",
                    "Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Reload the customer list
                await LoadCustomers();
                DisplayCurrentPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating test data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPopulateTestData.Enabled = true;
                btnPopulateTestData.Text = "⚠ Populate Test Data (250)";
            }
        }
    }

    // Helper classes
    public class CustomerWithLoyalty
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string MembershipStatus { get; set; } = null!;
        public decimal TotalSpending { get; set; }
        public int Points { get; set; }
        public string MembershipTier { get; set; } = null!;
        public DateOnly? JoinDate { get; set; }
    }

    public class PointsCalculation
    {
        public string CustomerName { get; set; } = "";
        public List<OrderPointsBreakdown> OrderBreakdown { get; set; } = new List<OrderPointsBreakdown>();
        public int AnniversaryBonus { get; set; }
        public int TotalPoints { get; set; }
    }

    public class OrderPointsBreakdown
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int BasePoints { get; set; }
        public int PromotionBonus { get; set; }
        public int TotalPoints { get; set; }
    }

    public enum RewardType
    {
        FiveEuroDiscount,
        TenPercentDiscount,
        TierUpgrade
    }
}
