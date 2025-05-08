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
    public partial class Form7 : Form
    {
        private decimal _totalPrice;
        private bool _isGCash = false;
        private List<string> _mergedOrderList;
        private decimal _discountAmount;
        private string _orderOption = "";
        private bool _orderOptionSelected = false;
        private bool _isCash = false;
        private Button cancelButton;
        private Form2 originalForm2;
        private string paymentMethod = "";
        public Form7(decimal totalPrice, decimal discountAmount, List<string> mergedOrderList, Form2 callingForm2)
        {
            InitializeComponent();
            InitializeCancelButton();
            _totalPrice = totalPrice;
            _mergedOrderList = mergedOrderList;
            _discountAmount = discountAmount;
            decimal finalTotalPrice = _totalPrice - _discountAmount;
            originalForm2 = callingForm2;
            button17.Enabled = false; // Initially disable the button

            label3.Text = $"Total: ₱{finalTotalPrice:N2}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            
            bool paymentProcessed = false;
            if (isGCash && orderOptionSelected)
            {
                // For Gcash, directly show the receipt using the total price
                // We might assume the amount tendered is the total price for the receipt
                if (decimal.TryParse(label3.Text.Replace("Total: ₱", ""), out decimal finaltotalPrice))
                {
                    ReceiptForm receiptForm = new ReceiptForm(_totalPrice, finaltotalPrice, 0, _mergedOrderList, _discountAmount, _orderOption, finaltotalPrice, paymentMethod); // Change is 0 for direct Gcash
                    this.Hide();
                    receiptForm.ShowDialog();
                    paymentProcessed = true;
                    this.DialogResult = DialogResult.OK;

                    receiptForm.Dispose();
                    isGCash = false; // Reset the flag
                    _orderOption = ""; // Reset the order option
                    orderOptionSelected = false; // Reset the order option selection    
                    paymentMethod = "";
                }
                else
                {
                    MessageBox.Show("Error parsing total price for Gcash receipt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (isCash && orderOptionSelected)
            {
                if (decimal.TryParse(textBox1.Text, out decimal amountTendered))
                {
                    if (decimal.TryParse(label3.Text.Replace("Total: ₱", ""), out decimal finalTotalPrice))
                    {
                        decimal change = amountTendered - finalTotalPrice;

                        if (change >= 0)
                        {
                            Form8 Form8 = new Form8(change, _totalPrice, _discountAmount, _mergedOrderList, _orderOption, finalTotalPrice, amountTendered, paymentMethod);
                            this.Hide();
                            Form8.ShowDialog();
                            paymentProcessed = true;
                            this.DialogResult = DialogResult.OK;
                            Form8.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Amount tendered is less than the total price.", "Payment Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error parsing total price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Invalid amount tendered.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                isCash = false; // Reset the flag
                paymentMethod = "";
            }
            if (paymentProcessed)
            {
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label4.Visible = true;
            textBox1.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button12.Visible = true;
            button13.Visible = true;
            button14.Visible = true;
            button16.Visible = true;
            button15.Visible = true;
            isCash = true;
            isGCash = false; // Reset the GCash flag
            paymentMethod = "Cash";
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            textBox1.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            button10.Visible = false;
            button11.Visible = false;
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button16.Visible = false;
            button15.Visible = false;
            isGCash = true;
            isCash = false; // Reset the cash flag
            paymentMethod = "GCash";
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _orderOption = "Dining In";
            orderOptionSelected = true;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _orderOption = "Takeaway";
            orderOptionSelected = true;
           
        }

        private void InitializeCancelButton()
        {
            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            // Set the location and size of the button as needed
            cancelButton.Location = new Point(10, this.ClientSize.Height - 40); // Example: Bottom-left
            cancelButton.Size = new Size(100, 30);
            cancelButton.Click += cancelButton_Click;
            this.Controls.Add(cancelButton);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {

            originalForm2.Show();
            this.Close(); // Close Form7
        }

        public bool orderOptionSelected
        {
            get { return _orderOptionSelected; }
            set
            {
                _orderOptionSelected = value;
                UpdateButton17Enabled(); // Call the update method when the value changes
            }
        }

        public bool isGCash
        {
            get { return _isGCash; }
            set
            {
                _isGCash = value;
                UpdateButton17Enabled(); // Call the update method when the value changes
            }
        }

        public bool isCash
        {
            get { return _isCash; }
            set
            {
                _isCash = value;
                UpdateButton17Enabled(); // Call the update method when the value changes
            }
        }

        private void UpdateButton17Enabled()
        {
            button17.Enabled = orderOptionSelected && (isGCash || isCash);
        }
    }
}
