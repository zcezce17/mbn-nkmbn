using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        
        public string selectedDrinkName;
        public decimal selectedDrinkPrice;
        public string tempFrom2;
        public bool IsEditing { get; set; } = false;
        public bool addonsEditInitiated = false; // Flag to check if Form4 was opened during edit mode
        public int OrderQuantity { get; set; } = 1;
        public string ExistingAddons { get; set; } = ""; // To store existing addons
        public string UpdatedAddons { get; set; } = "";  // To store updated addons from Form4
        public decimal newDrinkPrice;
        public decimal currentTotalPrice { get; set; } // To store the current total price
        public List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

        public Form3(string drinkName, string currentTemperature, decimal price, decimal totalPrice, int quantity, string existingAddons)
        {
            InitializeComponent();
            selectedDrinkName = drinkName;
            selectedDrinkPrice = price;
            newDrinkPrice = price;
            SelectedTemperature = currentTemperature;  // Use the passed temperature value
            tempFrom2= currentTemperature;
            OrderQuantity = quantity; // Set the passed quantity
            ExistingAddons = existingAddons;
            currentTotalPrice = totalPrice;
            UpdateQuantityLabel();
        }

        private void UpdateQuantityLabel()
        {
            label2.Text = OrderQuantity.ToString(); // Update label text to reflect the quantity
        }

        public string SelectedTemperature { get; set; } = "Hot";

        private void Form3_Load(object sender, EventArgs e)
        {
            if (drinksWithoutHot.Contains(selectedDrinkName))
            {
                button2.Enabled = false; // Disable the "Hot" button
                button2.BackColor = Color.Gray; // Set background color to gray
                SelectedTemperature = "Cold"; // Default to Cold for these drinks
            }
            else
            {
                button2.Enabled = true; // Enable the "Hot" button
                button2.BackColor = Color.LightCoral; // Set background color to red
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedTemperature = "Hot";
            Debug.WriteLine($"Temperature set to (HOT): {SelectedTemperature}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedTemperature = "Cold";
            Debug.WriteLine($"Temperature set to(COLD): {SelectedTemperature}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string newTemp = SelectedTemperature; // Initialize newTemp with the current temperature
            if (IsEditing) // Only set the flag if we are in edit mode
            {
                addonsEditInitiated = true; // Set the flag when Form4 is opened during edit
                newTemp = SelectedTemperature;
            }
            Debug.WriteLine($"SelectedTemperature: {SelectedTemperature}, OrigTemp: {tempFrom2}");
            Form4 addOnsForm = new Form4(selectedDrinkName, newTemp, selectedDrinkPrice, OrderQuantity, tempFrom2, ExistingAddons, IsEditing);
            if (addOnsForm.ShowDialog() == DialogResult.OK)
            {
                UpdatedAddons = addOnsForm.SelectedAddons;
                button3.Text = "Edit Addons (Modified)";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 mainForm = (Form2)Application.OpenForms["Form2"];

            if (mainForm != null)
            {
                
                decimal newDrinkPrice = selectedDrinkPrice * OrderQuantity;
                string orderString = $"DrinkName={selectedDrinkName}|Temperature={SelectedTemperature}|Price={selectedDrinkPrice}|TotalPrice={newDrinkPrice}|Quantity={OrderQuantity}" + (string.IsNullOrEmpty(UpdatedAddons) ? "" : $"|Addons={UpdatedAddons}");
               
                if (!IsEditing)
                {
                    mainForm.DisplayOrderString(orderString); // Add to order list
                }
                else if (IsEditing && !addonsEditInitiated)
                { 
                    string addonsToPass = string.IsNullOrEmpty(UpdatedAddons) ? ExistingAddons : UpdatedAddons;
                    orderString = $"DrinkName={selectedDrinkName}|Temperature={SelectedTemperature}|Price={selectedDrinkPrice}|TotalPrice={newDrinkPrice}|Quantity={OrderQuantity}" + (string.IsNullOrEmpty(addonsToPass) ? "" : $"|Addons={addonsToPass}");
                    Debug.WriteLine($"Form3 OK - Original Temp: {tempFrom2}, ExistingAddons: '{ExistingAddons}'");
                    Debug.WriteLine($"Form3 OK - newDrinkPrice: {newDrinkPrice}");
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OrderQuantity++; // Increase quantity
            UpdateQuantityLabel();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (OrderQuantity > 1) // Prevent going below 1
            {
                OrderQuantity--; // Decrease quantity
                UpdateQuantityLabel();
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {
            // No changes needed here
        }
    }
}
