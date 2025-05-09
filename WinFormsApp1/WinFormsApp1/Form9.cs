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
    public partial class Form9: Form
    {
        public int NewQuantity { get; private set; }
        private string _itemName;
        private Label itemLabel;
        private Label quantityLabel;
        private Button incrementButton;
        private Button decrementButton;
        private Button okButton;
        private Button cancelButton;
        private int _currentQuantity;
        public Form9(string itemName, int currentQuantity)
        {
            InitializeComponent();
            Debug.WriteLine($"current quantity in form9: {currentQuantity}");
            _itemName = itemName;
            _currentQuantity = currentQuantity;
            NewQuantity = currentQuantity;

            itemLabel = new Label();
            itemLabel.Text = $"Edit quantity for: {itemName}";
            itemLabel.AutoSize = true;
            itemLabel.Location = new Point(15, 15);
            Controls.Add(itemLabel);

            // Create and configure the decrement button
            decrementButton = new Button();
            decrementButton.Text = "-";
            decrementButton.Font = new Font(decrementButton.Font.FontFamily, 12, FontStyle.Bold);
            decrementButton.Size = new Size(30, 30);
            decrementButton.Location = new Point(40, itemLabel.Bottom + 25);
            decrementButton.Click += decrementButton_Click;
            Controls.Add(decrementButton);

            // Create and configure the quantity label
            quantityLabel = new Label();
            quantityLabel.Text = _currentQuantity.ToString();
            quantityLabel.AutoSize = true;
            quantityLabel.Font = new Font(quantityLabel.Font.FontFamily, 12, FontStyle.Regular);
            quantityLabel.TextAlign = ContentAlignment.MiddleCenter;
            quantityLabel.Size = new Size(40, 30);
            quantityLabel.Location = new Point(decrementButton.Right + 10, itemLabel.Bottom + 20);
            Controls.Add(quantityLabel);

            // Create and configure the increment button
            incrementButton = new Button();
            incrementButton.Text = "+";
            incrementButton.Font = new Font(incrementButton.Font.FontFamily, 12, FontStyle.Bold);
            incrementButton.Size = new Size(30, 30);
            incrementButton.Location = new Point(quantityLabel.Right + 30, itemLabel.Bottom + 25);
            incrementButton.Click += incrementButton_Click;
            Controls.Add(incrementButton);

            // Create and configure the OK button
            okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(15, incrementButton.Bottom + 15);
            okButton.Click += okButton_Click;
            Controls.Add(okButton);

            // Create and configure the Cancel button
            cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(okButton.Right + 10, okButton.Top);
            cancelButton.Click += cancelButton_Click;
            Controls.Add(cancelButton);

            // Set the form's AcceptButton and CancelButton properties
            this.AcceptButton = okButton;
            this.CancelButton = cancelButton;

            // Adjust form size
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(10);
        }
        private void incrementButton_Click(object sender, EventArgs e)
        {
            _currentQuantity++;
            quantityLabel.Text = _currentQuantity.ToString();
            NewQuantity = _currentQuantity;
        }

        private void decrementButton_Click(object sender, EventArgs e)
        {
            if (_currentQuantity > 1)
            {
                _currentQuantity--;
                quantityLabel.Text = _currentQuantity.ToString();
                NewQuantity = _currentQuantity;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
