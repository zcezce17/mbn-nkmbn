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

        public int OrderQuantity { get; set; } = 1;

        public List<string> drinksWithoutHot = new List<string>() { "Sea Salt Latte", "Salted Caramel", "Brown Sugar Latte", "Matcha Choco", "Strawberry Matcha", "Blueberry Matcha", "Strawberry Choco", "Strawberry Latte", "Blueberry Latte", "Mango Latte" };

        public Form3(string drinkName, string currentTemperature, decimal price, int quantity)
        {
            InitializeComponent();
            selectedDrinkName = drinkName;
            selectedDrinkPrice = price;
            SelectedTemperature = currentTemperature;  // Use the passed temperature value
            OrderQuantity = quantity; // Set the passed quantity
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedTemperature = "Cold";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 addOnsForm = new Form4(selectedDrinkName, SelectedTemperature, selectedDrinkPrice, OrderQuantity);
            addOnsForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 mainForm = (Form2)Application.OpenForms["Form2"];

            if (mainForm != null)
            {
                decimal totalPrice = selectedDrinkPrice * OrderQuantity;
                string orderString = $"DrinkName={selectedDrinkName}|Temperature={SelectedTemperature}|Price={selectedDrinkPrice}|Quantity={OrderQuantity}";
               
                Debug.WriteLine($"Order String (Form 3): {orderString}"); // Debugging

                mainForm.DisplayOrderString(orderString);  // Send the updated order back to Form2
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
