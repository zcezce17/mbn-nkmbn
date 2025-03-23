using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private Label orderSummaryLabel;
        private List<string> orderList = new List<string>();
        private Button confirmButton;
        public Form2()
        {
            InitializeComponent();
            panel4.VerticalScroll.Visible = false;
            panel4.VerticalScroll.Enabled = false;
            panel4.AutoScroll = false; // Temporarily disable to force refresh
            panel4.AutoScroll = true;  // Re-enable scrolling without scrollbar
            totalPrice = 0;

            InitializeOrderSummary();

        }

        private bool discountApplied = false;
        private decimal currentDiscountPercentage = 0;

        private Dictionary<string, decimal> drinkPrices = new Dictionary<string, decimal>
        {
            { "Long Black", 100.00m },
            { "Latte", 120.00m },
            { "Caramel", 140.00m },
            { "Spanish", 140.00m },
            { "Vanilla", 140.00m },
            { "Mocha" , 150.00m },
            { "White Mocha", 150.00m },
            { "Dirty Matcha", 180.00m},
            { "Crew's Coffee", 125.00m },
            { "Sea Salt Latte", 150.00m },
            { "Salted Caramel", 170.00m },
            { "Brown Sugar Latte", 150.00m },
            { "Chocolate", 130.00m },
            { "Matcha", 140.00m },
            { "Matcha Choco", 170.00m },
            { "Strawberry Matcha", 160.00m },
            { "Blueberry Matcha", 160.00m },
            { "Strawberry Choco", 170.00m },
            { "Strawberry Latte", 120.00m },
            { "Blueberry Latte", 120.00m },
            { "Mango Latte", 120.00m },
            { "Chocolate Frappe", 170.00m },
            { "Matcha Frappe", 170.00m },
            { "Cookies and Cream Frappe", 170.00m },
            { "Biscoff Frappe", 180.00m },
            { "Mango Frappe", 150.00m },
            { "Blueberry Frappe", 150.00m },
            { "Strawberry Frappe", 150.00m },
            { "Strawberry Choco Frappe", 200.00m },
            { "Lipton and Lemonade", 120.00m },
            { "Mango Ade", 100.00m },
            { "Blueberry Ade", 100.00m },
            { "Strawberry Ade", 100.00m },
            { "Tuna Pesto", 180.00m },
            { "Carbonara", 200.00m },
            { "Tuna and Cheese Toast", 170.00m },
            { "Grilled Cheese Toast", 150.00m },
            { "Chicken Overload Sandwich", 210.00m },
            { "Crew's Fried Chicken Sandwich", 220.00m },
            { "Butter Croissant", 110.00m },
            { "Pain Au Chocolat", 130.00m },
            { "Choco Chip Cookie", 80.00m },
            { "Brownies", 100.00m },
            { "Butterscotch", 100.00m },
            { "French Fries", 100.00m }
        };

        private Dictionary<string, decimal> addonPrices = new Dictionary<string, decimal>
{
    { "Extra Espresso Shot", 60.00m },
    { "Extra Syrup", 30.00m },
    { "Cream", 35.00m },
    { "Salted Cream", 35.00m },
    { "Oatmilk", 50.00m },
    { "Full Cream Milk", 30.00m }
};

        private decimal totalPrice = 0;
        private Label totalPriceLabel = new Label();

        private void InitializeOrderSummary()
        {
            if (orderSummaryLabel == null)
            {
                orderSummaryLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Century", 8, FontStyle.Bold),
                    Text = "Order Summary:",
                    Location = new Point(10, 10)
                };
                panel6.Controls.Add(orderSummaryLabel);
            }

            if (totalPriceLabel == null) // Ensure total label is created
            {
                totalPriceLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Century", 12, FontStyle.Bold),
                    Location = new Point(10, 50), // Temporary position
                    Text = "Total: 0.00" // Default text
                };
                panel6.Controls.Add(totalPriceLabel);
            }

            if (confirmButton == null)
            {
                confirmButton = new Button
                {
                    Text = "Confirm Order",
                    Size = new Size(120, 30),
                    Location = new Point(10, 80) // Adjust dynamically later
                };
                confirmButton.Click += ConfirmButton_Click;
                panel6.Controls.Add(confirmButton);
            }
        }



        public string SelectedDrinkName { get; set; } = "Drink";


        private void pictureBox1_Click(object sender, EventArgs e)
        {


        }




        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.AutoScrollPosition = new Point(339, 2234);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.AutoScrollPosition = new Point(411, 610);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Salted Caramel";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Vanilla";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel4.AutoScrollPosition = new Point(480, 10);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel4.AutoScrollPosition = new Point(490, 1054);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel4.AutoScrollPosition = new Point(501, 1508);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel4.AutoScrollPosition = new Point(320, 1777);
        }

        private void button8_Click(object sender, EventArgs e)
        {

            SelectedDrinkName = "Long Black";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }




        public void DisplayOrderFrappe(string drinkName)
        {
            if (drinkPrices.ContainsKey(drinkName))
            {
                decimal price = drinkPrices[drinkName];
                string orderString = $"{drinkName} | Price: ₱{price:N2}";
                orderList.Add(orderString);
                UpdateOrderDisplay();
            }
        }
        public void DisplayOrder(string drinkName, string temperature, int quantity = 1)
        {
            if (!drinkPrices.ContainsKey(drinkName)) return;

            decimal price = drinkPrices[drinkName];

            // ✅ Fix: Keep key-value format
            string orderString = $"DrinkName={drinkName}|Temperature={temperature}|Price={price:N2}|Quantity={quantity}";

            orderList.Add(orderString);
            UpdateOrderDisplay(); // Refresh order summary
        }

        private void UpdateOrderDisplay()
        {
            panel6.Controls.OfType<Label>()
       .Where(lbl => lbl != orderSummaryLabel && lbl != totalPriceLabel)
       .ToList()
       .ForEach(lbl => panel6.Controls.Remove(lbl));

            // Remove edit buttons as well
            panel6.Controls.OfType<Button>()
                .Where(btn => btn.Text == "Edit") // Assuming all edit buttons have the text "Edit"
                .ToList()
                .ForEach(btn => panel6.Controls.Remove(btn));
           

            int yOffset = orderSummaryLabel.Bottom + 10; // Start below "Order Summary:"
            decimal subTotalPrice = 0; // Calculate subtotal before discount
            totalPrice = 0;

            Dictionary<string, int> orderCounts = new Dictionary<string, int>();

            var updatedOrderCounts = new Dictionary<string, int>(); // Stores updated order strings

            foreach (string orderString in orderList)
            {
                // Extract order details
                string[] parts = orderString.Split('|');
                int orderQuantity = 1; // Default quantity
                string addonsValue = ""; // Store Addons (if any)

                foreach (string part in parts)
                {
                   
                    if (part.StartsWith("Quantity="))
                    {
                        int.TryParse(part.Substring("Quantity=".Length).Trim(), out orderQuantity);
                       
                    }
                    else if (part.StartsWith("Addons=")) // Capture Addons
                    {
                        addonsValue = part.Substring("Addons=".Length);
                    }
                }

                // ✅ Generate a unique key INCLUDING Addons (or empty if none)
                string orderKey = string.Join("|", parts.Where(p => !p.StartsWith("Quantity="))) + (string.IsNullOrEmpty(addonsValue) ? "" : $"|Addons={addonsValue}");

                if (orderCounts.ContainsKey(orderKey))
                {
                    orderCounts[orderKey] += orderQuantity; // Sum up quantity
                }
                else
                {
                    orderCounts[orderKey] = orderQuantity;
                }
            }

            // ✅ Now update the order strings with correct quantities
            foreach (var entry in orderCounts)
            {
                string updatedOrderString = $"{entry.Key}|Quantity={entry.Value}"; // Update quantity in string
                updatedOrderCounts[updatedOrderString] = entry.Value;
            }

         
            foreach (KeyValuePair<string, int> orderEntry in orderCounts)
            {
                string orderString = orderEntry.Key;
                int quantity = orderEntry.Value;
                decimal itemPrice = 0;
                List<string> addons = new List<string>();

                if (orderString.Contains("=")) // Handle complex orders (Form 3/Form 4)
                {
                    Dictionary<string, object> orderData = new Dictionary<string, object>();
                    try
                    {
                        string addonsString = "NoAddons"; // Default to "NoAddons"

                        string[] parts = orderString.Split('|');
                        foreach (string part in parts)
                        {
                            
                            if (part.StartsWith("Addons="))
                            {
                                string addonsPart = part.Substring("Addons=".Length).Trim(); // Trim whitespace
                                if (!string.IsNullOrEmpty(addonsPart))
                                {
                                    addonsString = addonsPart;
                                    addons = addonsPart.Split(',').ToList();
                                }
                            }
                            else if (part.StartsWith("Quantity="))
                            {
                                if (int.TryParse(part.Substring("Quantity=".Length), out quantity))
                                {
                                    
                                }
                            }
                            else if (part.StartsWith("Price="))
                            {
                                if (decimal.TryParse(part.Substring("Price=".Length), out itemPrice))
                                {
                                   
                                }
                            }
                            else
                            {
                                string[] kvp = part.Split('=');
                                if (kvp.Length == 2)
                                {
                                    orderData[kvp[0]] = kvp[1];
                                }
                            }
                        }

                        if (orderData.ContainsKey("DrinkName"))
                        {
                            string drinkName = orderData["DrinkName"].ToString();
                            string temperature = orderData["Temperature"].ToString();
                            orderData["Quantity"] = quantity;
                            orderData["Price"] = itemPrice;
                            string orderText = $"Drink: {drinkName} ({temperature})";
                          

                            if (addons.Any())
                            {
                                orderText += "\n" + string.Join("\n", addons);
                            }

                            orderText += $"\nPrice: ₱{itemPrice:N2}";

                            if (quantity > 1)
                            {
                                orderText += $" x{quantity}";
                            }
                            subTotalPrice += itemPrice * quantity;
                            // Create order label
                            Label orderLabel = new Label
                            {
                                AutoSize = true,
                                Text = orderText,
                                Location = new Point(10, yOffset),
                            };
                            panel6.Controls.Add(orderLabel);
                            yOffset = orderLabel.Bottom + 10;

                            // Create "Edit" button
                            Button editButton = new Button
                            {
                                Text = "Edit",
                                Location = new Point(panel6.Width - 80, yOffset + (orderLabel.Height / 2) - 12), // Position it on the right side
                                Size = new Size(60, 25)  // Set appropriate size for the button
                            };

                            // Store the orderData in the button's Tag property
                            editButton.Tag = orderData;

                            // Attach the EditOrder function to the button click event
                            editButton.Click += EditOrder;

                            // Add the button to the panel
                            panel6.Controls.Add(editButton);

                            yOffset = editButton.Bottom + 10;  // Update yOffset for next order

                           
                        }
                        else
                        {
                           
                            Label errorLabel = new Label
                            {
                                AutoSize = true,
                                Text = "Error displaying order",
                                Location = new Point(10, yOffset),
                            };
                            panel6.Controls.Add(errorLabel);
                            yOffset = errorLabel.Bottom + 10;
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        Label errorLabel = new Label
                        {
                            AutoSize = true,
                            Text = "Error displaying order",
                            Location = new Point(10, yOffset),
                        };
                        panel6.Controls.Add(errorLabel);
                        yOffset = errorLabel.Bottom + 10;
                    }
                }
                else  // Handle simple orders (like from DisplayOrderFrappe)
                {
                    string[] parts = orderString.Split('|');
                    if (parts.Length >= 2)
                    {
                        string drinkName = parts[0].Trim();
                        string pricePart = parts[1].Trim();
                        int priceIndex = pricePart.IndexOf("₱");
                        if (priceIndex != -1 && decimal.TryParse(pricePart.Substring(priceIndex + 1), out itemPrice))
                        {
                            string orderText = $"{drinkName}\nPrice: ₱{itemPrice:N2}";
                            if (quantity > 1)
                            {
                                orderText += $" x{quantity}";
                            }
                            Label orderLabel = new Label
                            {
                                AutoSize = true,
                                Text = orderText,
                                Location = new Point(10, yOffset),
                            };
                            panel6.Controls.Add(orderLabel);
                            yOffset = orderLabel.Bottom + 10;
                            subTotalPrice += itemPrice * quantity;
                        }
                        else
                        {
                           
                            Label errorLabel = new Label
                            {
                                AutoSize = true,
                                Text = "Error displaying order",
                                Location = new Point(10, yOffset),
                            };
                            panel6.Controls.Add(errorLabel);
                            yOffset = errorLabel.Bottom + 10;
                        }
                    }
                    else
                    {
                        
                        Label errorLabel = new Label
                        {
                            AutoSize = true,
                            Text = "Error displaying order",
                            Location = new Point(10, yOffset),
                        };
                        panel6.Controls.Add(errorLabel);
                        yOffset = errorLabel.Bottom + 10;
                    }
                }
            }

            // Apply discount if it has been applied
            if (discountApplied)
            {
                decimal discountPercentage = GetDiscountPercentage();
                decimal discountAmount = subTotalPrice * (discountPercentage / 100);
                subTotalPrice -= discountAmount;

                // Display the discount applied
                Label discountLabel = new Label();
                discountLabel.Text = $"Discount Applied: {discountPercentage}% (₱{discountAmount:N2})";
                discountLabel.Location = new Point(10, yOffset);
                discountLabel.Font = new Font(discountLabel.Font, FontStyle.Bold);
                discountLabel.AutoSize = true;
                panel6.Controls.Add(discountLabel);

                // Adjust yOffset to move the total price label further down
                yOffset = discountLabel.Bottom + 10; // Adjust the yOffset for the total price label
            }

            totalPrice = subTotalPrice; // Set the final total price

            // Update Total Price Label
            totalPriceLabel.Text = $"Total: ₱{totalPrice:N2}";
            totalPriceLabel.Location = new Point(10, yOffset);
            totalPriceLabel.Font = new Font(totalPriceLabel.Font, FontStyle.Bold);
            totalPriceLabel.AutoSize = true;
            panel6.Controls.Add(totalPriceLabel);

            // Move Confirm Button below total price
            confirmButton.Location = new Point(10, totalPriceLabel.Bottom + 10);
            panel6.ScrollControlIntoView(confirmButton);
            button52.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            // Calculate the button's position
            button52.Location = new Point(
                panel6.ClientSize.Width - button52.Width - 10, // 10 pixels from the right edge
                panel6.ClientSize.Height - button52.Height - 10 // 10 pixels from the bottom edge
            );

            if (orderList.Count > 0)
            {
                button1.Visible = true;
                button52.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button52.Visible = false;
            }
        }


        private decimal GetDiscountPercentage()
        {
            return currentDiscountPercentage;
        }



        public void DisplayOrder(string drinkName, string drinkTemperature, int quantity = 1, int extraShots = 0, int extraSyrup = 0, string sugarLevel = "100%", string selectedCream = "None", string selectedMilk = "None")
        {
            if (!drinkPrices.ContainsKey(drinkName)) return;

            decimal basePrice = drinkPrices[drinkName];

            for (int i = 0; i < quantity; i++) // ✅ Repeat based on quantity
            {
                decimal totalPrice = basePrice;
                var addonsList = new List<string>(); // ✅ Reset for each order instance

                // Handle Sugar Level
                if (sugarLevel != "100%")
                {
                    addonsList.Add($"Sugar Level: {sugarLevel}");
                }

                if (extraShots > 0)
                {
                    decimal addonPrice = extraShots * addonPrices["Extra Espresso Shot"];
                    totalPrice += addonPrice;
                    addonsList.Add($"Extra Shots: {extraShots} (₱{addonPrice:N2})");
                }
                if (extraSyrup > 0)
                {
                    decimal addonPrice = extraSyrup * addonPrices["Extra Syrup"];
                    totalPrice += addonPrice;
                    addonsList.Add($"Extra Syrup: {extraSyrup} (₱{addonPrice:N2})");
                }
                if (selectedCream != "None")
                {
                    decimal addonPrice = addonPrices[selectedCream];
                    totalPrice += addonPrice;
                    addonsList.Add($"Cream: {selectedCream} (₱{addonPrice:N2})");
                }
                if (selectedMilk != "None")
                {
                    decimal addonPrice = addonPrices[selectedMilk];
                    totalPrice += addonPrice;
                    addonsList.Add($"Milk: {selectedMilk} (₱{addonPrice:N2})");
                }

                Dictionary<string, object> newOrderItem = new Dictionary<string, object>
        {
            {"DrinkName", drinkName},
            {"Temperature", drinkTemperature},
            {"Price", totalPrice},
            {"Quantity", 1} // ✅ Each entry is a single order
        };

                string orderString = string.Join("|", newOrderItem.Select(kvp => $"{kvp.Key}={kvp.Value}"));

                // Add Addons part only if addons are selected
                if (addonsList.Any())
                {
                    string addonsString = string.Join(",", addonsList);
                    orderList.Add($"{orderString}|Addons={addonsString}");
                }
                else
                {
                    orderList.Add(orderString);
                }
            }

            UpdateOrderDisplay();
        }


        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (orderList.Count > 0)
            {
                MessageBox.Show("Order has been placed successfully!", "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                orderList.Clear();
                // Reset the discount variables
                discountApplied = false;
                currentDiscountPercentage = 0;
                UpdateOrderDisplay();
            }
            else
            {
                MessageBox.Show("No orders to confirm!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Latte";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Strawberry Matcha";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

            SelectedDrinkName = "Caramel";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Spanish";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Mocha";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName,currentTemperature,price3,1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "White Mocha";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Dirty Matcha";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Crew's Coffee";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Sea Salt Latte";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Brown Sugar Latte";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Chocolate";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Matcha";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Matcha Choco";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Strawberry Choco";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Blueberry Matcha";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Strawberry Latte";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3, 1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Blueberry Latte";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3,1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            SelectedDrinkName = "Mango Latte";
            decimal price3 = drinkPrices[SelectedDrinkName];

            // In Form2, we handle the logic of the drink temperature
            List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

            // Default to "Hot" unless the drink is in the list of drinks without hot options
            string currentTemperature = drinksWithoutHot.Contains(SelectedDrinkName) ? "Cold" : "Hot";

            // Pass the temperature along with the other details to Form3
            Form3 drinkoptionspopup = new Form3(SelectedDrinkName, currentTemperature, price3,1);

            if (drinkoptionspopup.ShowDialog() == DialogResult.OK)
            {
                string temperature = drinkoptionspopup.SelectedTemperature;
                // Now you can use the temperature for the order
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            string drinkName = "Lipton and Lemonade";
            DisplayOrderFrappe(drinkName);
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            string drinkName = "Chocolate Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string drinkName = "Matcha Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            string drinkName = "Cookies and Cream Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string drinkName = "Biscoff Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            string drinkName = "Mango Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            string drinkName = "Blueberry Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            string drinkName = "Strawberry Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            string drinkName = "Strawberry Choco Frappe";
            DisplayOrderFrappe(drinkName);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            string drinkName = "Mango Ade";
            DisplayOrderFrappe(drinkName);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            string drinkName = "Blueberry Ade";
            DisplayOrderFrappe(drinkName);
        }

        private void button40_Click(object sender, EventArgs e)
        {
            string drinkName = "Strawberry Ade";
            DisplayOrderFrappe(drinkName);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            string drinkName = "Tuna Pesto";
            DisplayOrderFrappe(drinkName);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            string drinkName = "Carbonara";
            DisplayOrderFrappe(drinkName);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            string drinkName = "Tuna and Cheese Toast";
            DisplayOrderFrappe(drinkName);
        }

        private void button41_Click(object sender, EventArgs e)
        {
            string drinkName = "Grilled Cheese Toast";
            DisplayOrderFrappe(drinkName);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            string drinkName = "Chicken Overload Sandwich";
            DisplayOrderFrappe(drinkName);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            string drinkName = "Crew's Fried Chicken Sandwich";
            DisplayOrderFrappe(drinkName);
        }

        private void button47_Click(object sender, EventArgs e)
        {
            string drinkName = "Butter Croissant";
            DisplayOrderFrappe(drinkName);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            string drinkName = "Pain Au Chocolat";
            DisplayOrderFrappe(drinkName);
        }

        private void button49_Click(object sender, EventArgs e)
        {
            string drinkName = "Choco Chip Cookie";
            DisplayOrderFrappe(drinkName);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            string drinkName = "Butterscotch";
            DisplayOrderFrappe(drinkName);
        }

        private void button50_Click(object sender, EventArgs e)
        {
            string drinkName = "Brownies";
            DisplayOrderFrappe(drinkName);
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            if (form5.ShowDialog() == DialogResult.OK)
            {
                string selectedOption = form5.SelectedOption;
                string drinkName = "French Fries"; // Assuming the drink is Long Black

                // Access the price from your dictionary
                if (drinkPrices.ContainsKey(drinkName))
                {
                    decimal price = drinkPrices[drinkName];

                    // Format the order string to include the selected option in parentheses
                    string orderString = $"{drinkName} ({selectedOption}) | Price: ₱{price:N2}";

                    orderList.Add(orderString);
                    UpdateOrderDisplay();
                }
                else
                {
                    // Handle the case where the drink name is not found in the dictionary
                    MessageBox.Show($"Price for {drinkName} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to cancel the entire order?", "Confirm Cancellation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                orderList.Clear();
                totalPrice = 0;
                discountApplied = false;
                currentDiscountPercentage = 0;
                UpdateOrderDisplay();
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (!discountApplied) // Apply Discount logic
            {
                // Prompt the user for the discount percentage
                string discountPercentageString = Microsoft.VisualBasic.Interaction.InputBox("Enter discount percentage (e.g., 10 for 10%):", "Apply Discount", "0");

                if (decimal.TryParse(discountPercentageString, out decimal discountPercentage))
                {
                    if (discountPercentage > 0 && discountPercentage <= 100)
                    {
                        // Calculate the discount amount
                        decimal discountAmount = totalPrice * (discountPercentage / 100);

                        // Apply the discount to the total price
                        totalPrice = totalPrice - discountAmount;
                        discountApplied = true;
                        currentDiscountPercentage = discountPercentage;

                        // Update the order display
                        MessageBox.Show($"Discount of ₱{discountAmount:N2} applied.", "Discount Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateOrderDisplay();
                    }
                    else
                    {
                        MessageBox.Show("Invalid discount percentage. Please enter a value between 1 and 100.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid number for the discount percentage.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else // Manage Discount logic
            {
                using (var customDialog = new Form6())
                {
                    DialogResult dialogResult = customDialog.ShowDialog();
                    if (dialogResult == DialogResult.Yes) // Cancel was clicked
                    {
                        discountApplied = false;
                        currentDiscountPercentage = 0;
                        // Recalculate total price without discount
                        totalPrice = 0;
                        foreach (string orderString in orderList)
                        {
                            decimal itemPrice = 0;
                            if (orderString.Contains("="))
                            {
                                try
                                {
                                    string[] parts = orderString.Split('|');
                                    foreach (string part in parts)
                                    {
                                        if (part.StartsWith("Price="))
                                        {
                                            if (decimal.TryParse(part.Substring("Price=".Length), out itemPrice))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    
                                }
                            }
                            else
                            {
                                string[] parts = orderString.Split('|');
                                if (parts.Length >= 2)
                                {
                                    string pricePart = parts[1].Trim();
                                    int priceIndex = pricePart.IndexOf("₱");
                                    if (priceIndex != -1 && decimal.TryParse(pricePart.Substring(priceIndex + 1), out itemPrice))
                                    {
                                        // Price extracted
                                    }
                                }
                            }
                            totalPrice += itemPrice;
                        }
                        UpdateOrderDisplay();
                        MessageBox.Show("Discount cancelled.", "Discount Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (dialogResult == DialogResult.No) // Change was clicked
                    {
                        string newDiscountPercentageString = Microsoft.VisualBasic.Interaction.InputBox("Enter new discount percentage (e.g., 10 for 10%):", "Change Discount", currentDiscountPercentage.ToString());

                        if (decimal.TryParse(newDiscountPercentageString, out decimal newDiscountPercentage))
                        {
                            if (newDiscountPercentage > 0 && newDiscountPercentage <= 100)
                            {
                                currentDiscountPercentage = newDiscountPercentage;
                                // Recalculate total price with new discount
                                totalPrice = 0;
                                foreach (string orderString in orderList)
                                {
                                    decimal itemPrice = 0;
                                    if (orderString.Contains("="))
                                    {
                                        try
                                        {
                                            string[] parts = orderString.Split('|');
                                            foreach (string part in parts)
                                            {
                                                if (part.StartsWith("Price="))
                                                {
                                                    if (decimal.TryParse(part.Substring("Price=".Length), out itemPrice))
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            
                                        }
                                    }
                                    else
                                    {
                                        string[] parts = orderString.Split('|');
                                        if (parts.Length >= 2)
                                        {
                                            string pricePart = parts[1].Trim();
                                            int priceIndex = pricePart.IndexOf("₱");
                                            if (priceIndex != -1 && decimal.TryParse(pricePart.Substring(priceIndex + 1), out itemPrice))
                                            {
                                                // Price extracted
                                            }
                                        }
                                    }
                                    totalPrice += itemPrice;
                                }
                                decimal discountAmount = totalPrice * (currentDiscountPercentage / 100);
                                totalPrice -= discountAmount;
                                UpdateOrderDisplay();
                                MessageBox.Show($"Discount changed to {currentDiscountPercentage}%", "Discount Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Invalid discount percentage. Please enter a value between 1 and 100.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid input. Please enter a valid number for the discount percentage.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        public void DisplayOrderString(string orderString)
        {
            Debug.WriteLine($"Received in Form2: {orderString}"); // Debugging
            
            string drinkName = "";
            string temperature = "";
            decimal price = 0;
            int quantity = 1; // Default to 1
            string addons = "";

            string[] parts = orderString.Split('|');
            foreach (string part in parts)
            {
                if (part.StartsWith("DrinkName="))
                    drinkName = part.Substring("DrinkName=".Length);
                else if (part.StartsWith("Temperature="))
                    temperature = part.Substring("Temperature=".Length);
                else if (part.StartsWith("Price=") && decimal.TryParse(part.Substring("Price=".Length), out decimal parsedPrice))
                    price = parsedPrice;
                else if (part.StartsWith("Quantity=") && int.TryParse(part.Substring("Quantity=".Length), out int parsedQuantity))
                    quantity = parsedQuantity;
                else if (part.StartsWith("Addons="))
                    addons = part.Substring("Addons=".Length);
            }

            Debug.WriteLine($"Final Parsed Order - Drink: {drinkName}, Temp: {temperature}, Quantity: {quantity}");

            for (int i = 0; i < quantity; i++) // ✅ Loop to create multiple orders
            {
                if (!string.IsNullOrEmpty(addons))
                {
                    int extraShots = 0, extraSyrup = 0;
                    string sugarLevel = "100%", selectedCream = "None", selectedMilk = "None";

                    string[] addonParts = addons.Split(',');
                    foreach (string addon in addonParts)
                    {
                        if (addon.StartsWith("Extra Shots:"))
                            int.TryParse(Regex.Match(addon, @"\d+").Value, out extraShots);
                        else if (addon.StartsWith("Extra Syrup:"))
                            int.TryParse(Regex.Match(addon, @"\d+").Value, out extraSyrup);
                        else if (addon.StartsWith("Sugar Level:"))
                            sugarLevel = addon.Replace("Sugar Level: ", "").Trim();
                        else if (addon.StartsWith("Cream:"))
                            selectedCream = addon.Replace("Cream: ", "").Split('(')[0].Trim();
                        else if (addon.StartsWith("Milk:"))
                            selectedMilk = addon.Replace("Milk: ", "").Split('(')[0].Trim();
                    }

                    DisplayOrder(drinkName, temperature, 1, extraShots, extraSyrup, sugarLevel, selectedCream, selectedMilk); // ✅ Each order is Quantity=1
                }
                else
                {
                    DisplayOrder(drinkName, temperature, 1); // ✅ Each order is added separately
                }
            }
        }


        private void EditOrder(object sender, EventArgs e)
        {
            Button editButton = sender as Button;
            if (editButton != null && editButton.Tag is Dictionary<string, object> orderData)
            {
                // Retrieve the order data from the Tag property
                string drinkName = orderData["DrinkName"].ToString();
                string temperature = orderData["Temperature"].ToString();
                decimal price = (decimal)orderData["Price"];  // Retrieve the price
                int quantity = (int)orderData["Quantity"];   // Retrieve the quantity

                // Open Form3 and pass all the necessary data (drink name, price, temperature, and quantity)
                Form3 editForm = new Form3(drinkName, temperature, price, quantity);  // Pass quantity here

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    // After editing, get the new temperature and quantity
                    string newTemperature = editForm.SelectedTemperature;  // Get the new temperature
                    int newQuantity = editForm.OrderQuantity;              // Get the new quantity

                    // Call HandleEditOrder to update the order
                    HandleEditOrder(drinkName, temperature, quantity, price, newQuantity, newTemperature);

                    // Update the order display
                    UpdateOrderDisplay();  // This should refresh the display with the updated order
                }
            }
        }


        public void HandleEditOrder(string drinkName, string temperature, int cQuantity, decimal price, int newQuantity, string newTemperature, string addons="")
        {
           

            // Identify change type
            bool isTemperatureChange = (newTemperature != null && newTemperature != temperature);
            bool isQuantityChange = (newQuantity !=cQuantity);

            // Find the original order
            string originalOrderKey = $"DrinkName={drinkName}|Temperature={temperature}" + (string.IsNullOrEmpty(addons) ? "" : $"|Addons={addons}");
            List<string> matchingOrders = orderList.Where(order => order.StartsWith(originalOrderKey)).ToList();

            int originalTotalQuantity = 0;
            decimal originalPrice = price; // Default to the passed price

            foreach (var order in matchingOrders)
            {
                string[] parts = order.Split('|');

                int currentQuantity = cQuantity;
                originalTotalQuantity = currentQuantity;

                // Extract the price from the existing order if available
                string pricePart = parts.FirstOrDefault(part => part.StartsWith("Price="));
                if (pricePart != null)
                {
                    originalPrice = decimal.Parse(pricePart.Split('=')[1]);
                }

                orderList.Remove(order);
            }
            string updatedOrderKey = $"DrinkName={drinkName}|Temperature={newTemperature}" + (string.IsNullOrEmpty(addons) ? "" : $"|Addons={addons}");
            string updatedOrderString = "";

            Debug.WriteLine($"Removed old order: {originalOrderKey}, Quantity Removed: {originalTotalQuantity}");

            // 1️⃣ **Case: Only Temperature Changed**
            if (isTemperatureChange && !isQuantityChange)
            {
                
                updatedOrderString = $"{updatedOrderKey}|Price={originalPrice}|Quantity={cQuantity}";

                Debug.WriteLine($"Temperature changed. Updated order: {updatedOrderString}");
            }
            // 2️⃣ **Case: Only Quantity Changed**
            else if (!isTemperatureChange && isQuantityChange)
            {
                updatedOrderString= $"{originalOrderKey}|Price={originalPrice}|Quantity={newQuantity}";
                Debug.WriteLine($"Quantity changed. Updated order: {updatedOrderString}");
            }
            // 3️⃣ **Case: Both Temperature and Quantity Changed**
            else if (isTemperatureChange && isQuantityChange)
            {
                updatedOrderString = $"{updatedOrderKey}|Price={originalPrice}|Quantity={newQuantity}";
                Debug.WriteLine($"Temperature & Quantity changed. Updated order: {updatedOrderString}");
            }
            orderList.RemoveAll(order => order.StartsWith(updatedOrderKey));
            orderList.Add(updatedOrderString);

            // Debugging Output Before Updating Display
            Debug.WriteLine("Order List Before Updating Display:");
            foreach (string order in orderList)
            {
                Debug.WriteLine(order);
            }

            // Refresh order display
            UpdateOrderDisplay();
        }




    }
}