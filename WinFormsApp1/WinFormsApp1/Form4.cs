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
        private int orderquantity;

        public Form4(string drinkName = null, string temperature = null, decimal price = 0, int quantity = 0)
        {
            InitializeComponent();
            currentDrink = drinkName;
            drinkTemperature = temperature;
            currentDrinkPrice = price;
            orderquantity = quantity;
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
            numericUpDown1.Maximum = 5;
            numericUpDown1.Value = 0;

            numericUpDown2.Minimum = 0;
            numericUpDown2.Maximum = 10;
            numericUpDown2.Value = 0;

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

            currentDrinkPrice += addonPrice; // Add addon prices to base drink price

            Form2 mainForm = Application.OpenForms.OfType<Form2>().FirstOrDefault();
            if (mainForm != null)
            {
                string orderString = $"DrinkName={currentDrink}|Temperature={drinkTemperature}|Price={currentDrinkPrice}|Quantity={orderquantity}";

                if (addons.Count > 0)
                {
                    orderString += "|Addons=" + string.Join(",", addons);
                }
               
                mainForm.DisplayOrderString(orderString);
            }
            Form3 drinkOptionsForm = Application.OpenForms.OfType<Form3>().FirstOrDefault();
            if (drinkOptionsForm != null)
            {
                drinkOptionsForm.Close();
            }
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
