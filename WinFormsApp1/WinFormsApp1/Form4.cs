using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        
        private Dictionary<string, List<string>> disabledAddonsForDrink = new Dictionary<string, List<string>>()
        {
            { "Long Black", new List<string>() { "Full Cream Milk", "Oatmilk" } } // Example: Disable Soy and Almond Milk for Long Black
            // Add more drinks and disabled add-ons as needed.
            // Make sure the names here match the Text property of your checkboxes.
        };

        private HashSet<string> drinksWithoutExtraShot = new HashSet<string>()
        {
            "Chocolate",
            "Matcha",
            "Matcha Choco",
            "Strawberry Matcha",
            "Blueberry Matcha",
            "Strawberry Choco",
            "Strawberry Latte",
            "Blueberry Latte",
            "Mango Latte"
            // Add more drinks that cannot have extra shots
        };

        private HashSet<string> drinksWithOnlyMilk = new HashSet<string>()
        {
            "Strawberry Latte",
            "Blueberry Latte",
            "Mango Latte"
        };
        private string currentDrink;
        private string drinkTemperature;
        private decimal currentDrinkPrice;
        private decimal newDrinkPrice;
        private string newTemperature;
        private int orderquantity;
        public string ExistingAddons { get; set; } = "";
        public bool IsEditing { get; set; } = false;
        public string SelectedAddons { get; set; } = "";

        public Form4(string drinkName = null, string temperature = null, decimal price = 0, int quantity = 0, string origTemp = null, string existingAddons = "", bool isEditing = false)
        {
            InitializeComponent();
            currentDrink = drinkName;
            drinkTemperature = origTemp;
            newTemperature = temperature;
            currentDrinkPrice = price;
            orderquantity = quantity;
            ExistingAddons = existingAddons;
            IsEditing = isEditing;
            if (IsEditing && !string.IsNullOrEmpty(ExistingAddons))
            {
                PreselectAddons();
            }
            newDrinkPrice = 0;
            Debug.WriteLine($"Original Temp: {origTemp}, New Temp: {temperature}");
        }

        private void PreselectAddons()
        {
       
            string[] addonsArray = ExistingAddons.Split(',');
            foreach (string addonPart in addonsArray)
            {
                string trimmedAddon = addonPart.Trim();
        
                // Logic to find and check the corresponding UI elements
                // You'll need to adjust this based on the exact text of your checkboxes/controls

                // Example for Extra Shots:
                if (trimmedAddon.StartsWith("Extra Shots:"))
                {
                    string valueStr = trimmedAddon.Substring("Extra Shots:".Length).Trim().Split(' ')[0];
             
                    if (int.TryParse(valueStr, out int value))
                    {
                 
                        numericUpDown1.Value = value;
                    }
              
                }
                // Example for Extra Syrup:
                else if (trimmedAddon.StartsWith("Extra Syrup:"))
                {
                    string valueStr = trimmedAddon.Substring("Extra Syrup:".Length).Trim().Split(' ')[0];
                 
                    if (int.TryParse(valueStr, out int value))
                    {
                     
                        numericUpDown2.Value = value;
                    }
                 
                }
                // Example for Cream:
                else if (trimmedAddon.StartsWith("Cream:"))
                {
                    string creamType = trimmedAddon.Substring("Cream:".Length).Trim().Split(' ')[0];
                    foreach (Control control in panel2.Controls)
                    {
                        if (control is CheckBox checkBox && checkBox.Text.StartsWith(creamType))
                        {
                            checkBox.Checked = true;
                            break;
                        }
                    }
                }
                // Example for Milk:
                else if (trimmedAddon.StartsWith("Milk:"))
                {
                    string milkType = trimmedAddon.Substring("Milk:".Length).Trim().Split(' ')[0];
                    foreach (Control control in panel3.Controls)
                    {
                        if (control is CheckBox checkBox && checkBox.Text.StartsWith(milkType))
                        {
                            checkBox.Checked = true;
                            break;
                        }
                    }
                }
                // Example for Sugar Level:
                else if (trimmedAddon.StartsWith("Sugar Level:"))
                {
                    string sugarLevel = trimmedAddon.Substring("Sugar Level:".Length).Trim();
                    foreach (Control control in panel1.Controls)
                    {
                        if (control is RadioButton radioButton && radioButton.Text == sugarLevel)
                        {
                            radioButton.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        public string CurrentDrink
        {
            get { return currentDrink; }
            set { currentDrink = value; }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 0;
            numericUpDown1.Maximum = 10;
            if (!IsEditing)
            {
                numericUpDown1.Value = 0;
            }
            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 20;
            if (!IsEditing)
            {
                numericUpDown2.Value = 0;
            }
            // Disable extra shots based on the current drink
            if (drinksWithoutExtraShot.Contains(currentDrink))
            {
                numericUpDown1.Maximum = 0;
                numericUpDown1.Enabled = false; // Disable the control
                numericUpDown1.ForeColor = SystemColors.GrayText; // Set text color to gray
                numericUpDown1.BackColor = SystemColors.Control; // Set background color to control background
            }


            // Disable add-ons based on the current drink
            if (disabledAddonsForDrink.ContainsKey(currentDrink))
            {
                List<string> disabledAddons = disabledAddonsForDrink[currentDrink];
                foreach (string addon in disabledAddons)
                {
                    DisableAddonControl(addon);
                }
            }

            if (drinkTemperature == "Hot")
            {
                DisableCreamOptions();
            }

            if (drinksWithOnlyMilk.Contains(currentDrink))
            {
                numericUpDown1.Maximum = 0;
                numericUpDown1.Enabled = false; // Disable the control
                numericUpDown1.ForeColor = SystemColors.GrayText; // Set text color to gray
                numericUpDown1.BackColor = SystemColors.Control; // Set background color to control background

                numericUpDown2.Maximum = 0;
                numericUpDown2.Enabled = false; // Disable the control
                numericUpDown2.ForeColor = SystemColors.GrayText; // Set text color to gray
                numericUpDown2.BackColor = SystemColors.Control; // Set background color to control background

                DisableCreamOptions();

                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }
        }

        private void DisableCreamOptions()
        {
            // Assuming cream options are in panel2
            foreach (Control control in panel2.Controls)
            {
                if (control is CheckBox checkBox)
                {
                    checkBox.Enabled = false;
                    checkBox.Checked = false; // Optionally uncheck
                }
            }
        }

        private void DisableAddonControl(string addonText)
        {
            // Assuming milk options are in panel3
            foreach (Control control in panel3.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Text == addonText)
                {
                    checkBox.Enabled = false;
                    checkBox.Checked = false; // Optionally uncheck
                    break;
                }
            }
            // Add similar logic for other panels if needed (e.g., Cream options in panel2)
        }




        private void button1_Click(object sender, EventArgs e)
        {
            int extraShots = (int)numericUpDown1.Value;
            int extraSyrup = (int)numericUpDown2.Value;
            string sugarLevel = GetSelectedSugarLevel();
            string selectedCream = GetSelectedCream();
            string selectedMilk = GetSelectedMilk();

            decimal addonPrice = 0; // Initialize addon price

            List<string> addons = new List<string>(); // List to store addon strings with prices

            // Calculate addon prices and create addon strings

            if (extraShots > 0)

            {
                decimal shotPrice = extraShots * 60.00m;
                addonPrice += shotPrice;
                addons.Add($"Extra Shots: {extraShots} (₱{shotPrice})");
            }

            if (extraSyrup > 0)

            {
                decimal syrupPrice = extraSyrup * 30.00m;
                addonPrice += syrupPrice;
                addons.Add($"Extra Syrup: {extraSyrup} (₱{syrupPrice})");
            }

            if (!string.IsNullOrEmpty(selectedCream) && selectedCream != "None")

            {
                addonPrice += 35.00m;
                addons.Add($"Cream: {selectedCream} (₱35)");
            }

            if (!string.IsNullOrEmpty(selectedMilk) && selectedMilk != "None")
            {
                decimal milkPrice = 0;
                if (selectedMilk == "Full Cream Milk")
                {
                    milkPrice = 30.00m;
                }

                else if (selectedMilk == "Oatmilk")
                {
                    milkPrice = 50.00m;
                }
                addonPrice += milkPrice;
                addons.Add($"Milk: {selectedMilk} (₱{milkPrice})");
            }

            if (!string.IsNullOrEmpty(sugarLevel) && sugarLevel != "100%")
            {
                addons.Add($"Sugar Level: {sugarLevel}"); // sugar level has no price
            }

            newDrinkPrice = currentDrinkPrice + addonPrice; // Add the total addon price to the drink price

            SelectedAddons = string.Join(",", addons);
            
            Form2 mainForm = Application.OpenForms.OfType<Form2>().FirstOrDefault();
            if (mainForm != null && IsEditing)
            {
                  
                // Directly call HandleEditOrder in Form2 to update addons
                Debug.WriteLine($"Editing Order: {currentDrink}, New Temp: {newTemperature}, Price: {currentDrinkPrice}, Total Price: {newDrinkPrice}, Quantity: {orderquantity}, Updated Addons: {SelectedAddons}");
                mainForm.HandleEditOrder(currentDrink, newTemperature, orderquantity, currentDrinkPrice, orderquantity, drinkTemperature, newDrinkPrice, ExistingAddons, SelectedAddons);
            }

            else if (mainForm != null && !IsEditing)
            {
                // Handle adding a new order as before
                string orderString = $"DrinkName={currentDrink}|Temperature={newTemperature}|Price={currentDrinkPrice}|TotalPrice={newDrinkPrice}|Quantity={orderquantity}";
                if (addons.Count > 0)
                {
                    orderString = $"DrinkName={currentDrink}|Temperature={newTemperature}|Price={currentDrinkPrice}|TotalPrice={newDrinkPrice}|Quantity={orderquantity}|Addons=" + string.Join(",", addons);
                    Debug.WriteLine($"Order String: {orderString}"); // For debugging
                }
                mainForm.DisplayOrderString(orderString);
            }
            Form3 drinkOptionsForm = Application.OpenForms.OfType<Form3>().FirstOrDefault();
            if (drinkOptionsForm != null)
            {
                drinkOptionsForm.Close();
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private string GetSelectedSugarLevel()
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is RadioButton radioButton && radioButton.Checked)
                {
                    return radioButton.Text;
                }
            }
            return "100%";
        }

        private string GetSelectedCream()
        {
            foreach (Control control in panel2.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    return checkBox.Text;
                }
            }
            return "None";
        }

        private string GetSelectedMilk()
        {
            foreach (Control control in panel3.Controls)
            {
                if (control is CheckBox checkBox && checkBox.Checked)
                {
                    return checkBox.Text;
                }
            }
            return "None";
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox4.Checked = false;  
            }
        }
    }
}
