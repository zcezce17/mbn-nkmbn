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
    public partial class Form8: Form
    {
        private decimal _change;
        private decimal _originalTotalPrice;
        private decimal _discountAmount;
        private decimal _finalTotalPrice;
        private List<string> _orderList;
        private string _orderOption;
        private Label paymentSuccessfulLabel;
        private Label changeAmountLabel;
        private Button printReceiptButton;  
        private decimal _amountTendered;
        private string _paymentMethod = "";
        public Form8(decimal change, decimal originalTotalPrice, decimal discountAmount, List<string> orderList, string orderOption, decimal finaltotalprice, decimal amountTendered, string paymentMethod)
        {
            InitializeComponent();
            _change = change;
            _originalTotalPrice = originalTotalPrice;
            _discountAmount = discountAmount;
            _orderList = orderList;
            _orderOption = orderOption;
            _finalTotalPrice = finaltotalprice;
            _amountTendered = amountTendered;
            InitializeLabels();
            InitializeButtons();
            LayoutControls();
            Controls.Add(paymentSuccessfulLabel);
            Controls.Add(changeAmountLabel);
            Controls.Add(printReceiptButton);
            _paymentMethod = paymentMethod;
        }
        private void InitializeLabels()
        {
            paymentSuccessfulLabel = new Label
            {
                Text = "Payment\nSuccessful!",
                Font = new Font("Arial", 15, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true
            };

            changeAmountLabel = new Label
            {
                Text = $"Change: ₱{_change:N2}",
                Font = new Font("Arial", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = true
            };
        }

        private void InitializeButtons()
        {
            printReceiptButton = new Button
            {
                Text = "Print Receipt",
                Font = new Font("Arial", 10),
                AutoSize = true
            };
            printReceiptButton.Click += printReceiptButton_Click;
          
        }

        private void LayoutControls()
        {
            ClientSize = new Size(300, 250);
            int padding = 15;
            int yOffset = 30;
            int centerX = ClientRectangle.Width / 2;

            paymentSuccessfulLabel.Location = new Point(centerX - paymentSuccessfulLabel.Width / 2, yOffset);
            yOffset += paymentSuccessfulLabel.Height + padding;

            changeAmountLabel.Location = new Point(centerX - changeAmountLabel.Width / 2, yOffset + 20);
            yOffset += changeAmountLabel.Height + 2 * padding;

            printReceiptButton.Location = new Point(centerX - printReceiptButton.Width / 2, ClientRectangle.Height - printReceiptButton.Height - (padding + 35));

            StartPosition = FormStartPosition.CenterScreen;
            Text = "Payment Successful";
        }

        private void printReceiptButton_Click(object sender, EventArgs e)
        {
            ReceiptForm receiptForm = new ReceiptForm(_originalTotalPrice, _finalTotalPrice, _change, _orderList, _discountAmount, _orderOption, _amountTendered, _paymentMethod);
            this.Hide();
            receiptForm.ShowDialog();
            
            receiptForm.Dispose();
        }

    }
}
