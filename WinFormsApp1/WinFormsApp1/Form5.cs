using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form5 : Form
    {
        public string SelectedOption { get; set; }
        public int SelectedQuantity { get; set; }
        private int currentQuantity = 1;
        private Label labelQuantity;
        private Button buttonIncrement;
        private Button buttonDecrement;
        private Button buttonConfirm;
        private string tempSelectedOption = null;
        public Form5(int initialQuantity = 1)
        {
            InitializeComponent();
            InitializeQuantityControls(); // Call this method to create controls
            currentQuantity = initialQuantity;
            labelQuantity.Text = currentQuantity.ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            // Set initial quantity if not provided in the constructor
            if (labelQuantity.Text == "0")
            {
                currentQuantity = 1;
                labelQuantity.Text = "1";
            }
        }
        private void buttonIncrement_Click(object sender, EventArgs e)
        {
            currentQuantity++;
            labelQuantity.Text = currentQuantity.ToString();
        }
        private void buttonDecrement_Click(object sender, EventArgs e)
        {
            if (currentQuantity > 1)
            {
                currentQuantity--;
                labelQuantity.Text = currentQuantity.ToString();
            }
        }

        private void InitializeQuantityControls()
        {
            // Create and configure the label
            labelQuantity = new Label
            {
                Name = "labelQuantity",
                Text = "1",
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true,
                Font = new Font(this.Font, FontStyle.Regular) // You can adjust the font
            };

            // Create and configure the increment button
            buttonIncrement = new Button
            {
                Name = "buttonIncrement",
                Text = "+",
                AutoSize = true
            };
            buttonIncrement.Click += buttonIncrement_Click;

            // Create and configure the decrement button
            buttonDecrement = new Button
            {
                Name = "buttonDecrement",
                Text = "-",
                AutoSize = true
            };
            buttonDecrement.Click += buttonDecrement_Click;

            buttonConfirm = new Button
            {
                Name = "buttonConfirm",
                Text = "Confirm",
                AutoSize = true
            };
            buttonConfirm.Click += buttonConfirm_Click;

            // Add the controls to the form's Controls collection
            this.Controls.Add(labelQuantity);
            this.Controls.Add(buttonIncrement);
            this.Controls.Add(buttonDecrement);
            this.Controls.Add(buttonConfirm);

            // You'll need to handle the layout of these controls.
            // For a simple layout, you can set their Location properties.
            // Consider using a TableLayoutPanel or FlowLayoutPanel for more robust layout.

            // Example simple layout (adjust coordinates as needed):
            int labelWidth = 30; // Approximate width of the label
            int buttonWidth = 30; // Approximate width of the buttons
            int startX = 10;
            int yOffset = 90; // Adjust vertical position

            buttonDecrement.Location = new Point(120, yOffset);
            labelQuantity.Location = new Point(220, yOffset);
            buttonIncrement.Location = new Point(330, yOffset);
            buttonConfirm.Location = new Point(360, 115);
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            if (tempSelectedOption != null)
            {
                this.SelectedOption = tempSelectedOption;
                this.SelectedQuantity = currentQuantity;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select an option before confirming.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tempSelectedOption = "Salt";
            // You might want to provide visual feedback that this option is selected
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tempSelectedOption = "Cheese";
            // You might want to provide visual feedback
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tempSelectedOption = "Barbecue";
            // You might want to provide visual feedback
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tempSelectedOption = "Sour Cream";
            // You might want to provide visual feedback
        }
    }
}
