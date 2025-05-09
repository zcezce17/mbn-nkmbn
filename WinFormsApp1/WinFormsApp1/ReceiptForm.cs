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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ExplorerBar;

namespace WinFormsApp1 // Replace with your actual namespace
{
    public partial class ReceiptForm : Form
    {
        private decimal _totalAmount;
        private decimal _amountTendered;
        private decimal _changeDue;
        private List<string> _orderedItems;

        // Labels
        private Label receiptTitleLabel;
        private Label dateTimeLabel;
        private Label orderNumberLabel;
        private Label companyNameLabel;
        private Label companyAddressLabel;
        
        private ListBox receiptItemsListBox;
        private Label subtotalLabel;
        private Label discountLabel;
        private Label totalAmountLabel;
        private Label amountTenderedLabel;
        private Label changeDueLabel;
        private Label orderTypeLabel;
        private Label thankYouLabel;
        private Button closeButton; 
        private decimal _discountAmount;
        private string _orderOption = "";
        private decimal _finalPrice;
        private string _paymentMethod;

        public ReceiptForm(decimal totalAmount, decimal finalPrice, decimal changeDue, List<string> orderedItems, decimal discountAmount, string orderOption, decimal amountTendered, string paymentMethod)
        {
            InitializeComponent();
            _totalAmount = totalAmount;
            _amountTendered = amountTendered;
            _changeDue = changeDue;
            _orderedItems = orderedItems;
            _discountAmount = discountAmount;
            _orderOption = orderOption;
            _finalPrice = finalPrice;
            _paymentMethod = paymentMethod;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.AutoScroll = false;
            this.MaximumSize = new Size(600, 900);
            InitializeLabels();
            LayoutControls();

            // Populate the receipt details
            PopulateReceipt();
            
        }
        private void InitializeLabels()
        {
            receiptTitleLabel = new Label() { Text = "Official Receipt", Font = new Font("Arial", 16, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };
            dateTimeLabel = new Label() { Text = "[Date and Time]", TextAlign = ContentAlignment.MiddleCenter };
            orderNumberLabel = new Label() { Text = "Order #: [Order Number]", TextAlign = ContentAlignment.MiddleCenter };
            companyNameLabel = new Label() { Text = "Cold Crew Coffee", Font = new Font("Arial", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };
            companyAddressLabel = new Label() { Text = "Metrogate 2 Heritage Homes, Block 15 Lot 43  \n Main Road, Phase 1, Loma De Gato, Marilao Bulacan", TextAlign = ContentAlignment.MiddleCenter, AutoSize = true };

            receiptItemsListBox = new ListBox() { BorderStyle = BorderStyle.FixedSingle, ScrollAlwaysVisible = false };
            subtotalLabel = new Label() { Text = "Subtotal: ₱0.00", TextAlign = ContentAlignment.MiddleRight };
            discountLabel = new Label() { Text = "Discount: ₱0.00", TextAlign = ContentAlignment.MiddleRight };
            totalAmountLabel = new Label() { Text = "Total: ₱0.00", Font = new Font("Arial", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };
            amountTenderedLabel = new Label() { Text = "Amount Tendered: ₱0.00", TextAlign = ContentAlignment.MiddleRight };
            changeDueLabel = new Label() { Text = "Change: ₱0.00", Font = new Font("Arial", 12, FontStyle.Bold), TextAlign = ContentAlignment.MiddleRight };
            orderTypeLabel = new Label() { Text = "", Font = new Font("Arial", 10, FontStyle.Bold), AutoSize = true };
            thankYouLabel = new Label() { Text = "Thank you for your purchase!", TextAlign = ContentAlignment.MiddleCenter };
            closeButton = new Button() { Text = "Close" };
            closeButton.Click += closeButton_Click;

            // Add controls to the form's Controls collection
            Controls.AddRange(new Control[] { receiptTitleLabel, dateTimeLabel, orderNumberLabel, companyNameLabel, companyAddressLabel, receiptItemsListBox, subtotalLabel, discountLabel, totalAmountLabel, amountTenderedLabel, changeDueLabel, orderTypeLabel, thankYouLabel, closeButton });
        }
        private void LayoutControls()
        {
            int yOffset = 10;
            int labelWidth = 200;
            int valueWidth = 100;
            int leftMargin = 10;
            int controlHeight = 25;
            int spacing = 5;
            int formWidth = leftMargin + labelWidth + spacing + valueWidth + leftMargin;

            // Center title
            receiptTitleLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, 30);
            yOffset += receiptTitleLabel.Height + spacing;

            dateTimeLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += dateTimeLabel.Height + spacing;

            orderNumberLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += orderNumberLabel.Height + 2 * spacing;

            // Company info
            companyNameLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += companyNameLabel.Height + spacing;

            companyAddressLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += companyAddressLabel.Height + 2 * spacing;

            orderTypeLabel.SetBounds(leftMargin, yOffset, labelWidth, controlHeight); // Position orderTypeLabel
            yOffset += orderTypeLabel.Height + spacing;

            int itemHeight = TextRenderer.MeasureText("A", receiptItemsListBox.Font).Height + 2; // Approximate item height
            int listBoxHeight = (receiptItemsListBox.Items.Count * itemHeight);
            receiptItemsListBox.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, listBoxHeight);
            yOffset += receiptItemsListBox.Height + 2 * spacing;

            // Financial details (right-aligned)
            subtotalLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += subtotalLabel.Height + spacing;

            discountLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += discountLabel.Height + spacing;

            totalAmountLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight * 2); // Make total a bit taller
            yOffset += totalAmountLabel.Height + 2 * spacing;

            amountTenderedLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += amountTenderedLabel.Height + spacing;

            changeDueLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight * 2); // Make change taller
            yOffset += changeDueLabel.Height + 2 * spacing;

            // Thank you message
            thankYouLabel.SetBounds(leftMargin, yOffset, formWidth - 2 * leftMargin, controlHeight);
            yOffset += thankYouLabel.Height + 2 * spacing;

            // Buttons at the bottom (side by side)
            int buttonWidth = 80;
            int buttonSpacing = 10;
            int buttonsStartX = (formWidth - 2 * buttonWidth - buttonSpacing) / 2; // Center buttons

            closeButton.SetBounds(buttonsStartX + buttonWidth + buttonSpacing, yOffset, buttonWidth, controlHeight);
            yOffset += closeButton.Height + spacing;

            // Set the form's initial size
            this.ClientSize = new Size(formWidth, yOffset + 10);
            this.StartPosition = FormStartPosition.CenterScreen; // Center the form on the screen
            this.AutoScroll = true;
        }

        private void PopulateReceipt()
        {
            dateTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            orderNumberLabel.Text = $"Order #: {GenerateOrderNumber()}";
            companyNameLabel.Text = "Cold Crew Coffee";
            companyAddressLabel.Text = "Metrogate 2 Heritage Homes, Block 15 Lot 43  \n Main Road, Phase 1, Loma De Gato, Marilao Bulacan";
           
            receiptItemsListBox.Items.Clear();
            receiptItemsListBox.Font = new Font("Courier New", 10, FontStyle.Regular);

            int itemColWidth = 25;
            int priceColWidth = 8;
            int spacing = 2;
            int itemHeight = TextRenderer.MeasureText("A", receiptItemsListBox.Font).Height + 2;
            int totalItemsHeight = 0;

            Func<string, int, string> leftAlign = (text, width) => text.PadRight(width);
            Func<string, int, string> rightAlign = (text, width) => text.PadLeft(width);

            string headerLine = $"{leftAlign("ITEM", itemColWidth)}{rightAlign("PRICE", priceColWidth)}";
            receiptItemsListBox.Items.Add(headerLine);
            totalItemsHeight += itemHeight;
            receiptItemsListBox.Items.Add(new string('-', itemColWidth + spacing + priceColWidth));
            totalItemsHeight += itemHeight;

            foreach (string orderString in _orderedItems)
            {
                string itemName = "";
                string temperature = "";
                int quantity = 1;
                decimal price = 0;
                decimal totalAddonPriceForItem = 0;
                List<string> addonsToPrint = new List<string>();
                string[] parts = orderString.Split('|');

                foreach (string part in parts)
                    {
                    string trimmedPart = part.Trim();
                    Debug.WriteLine($"Part (Receipt - Trimmed): {trimmedPart}");
                    if (trimmedPart.StartsWith("DrinkName=")) itemName = trimmedPart.Substring("DrinkName=".Length).Trim();
                    else if (trimmedPart.StartsWith("Temperature="))
                    {
                            temperature = trimmedPart.Substring("Temperature=".Length).Trim();
                    }
                    else if (trimmedPart.StartsWith("Quantity=")) int.TryParse(trimmedPart.Substring("Quantity=".Length).Trim(), out quantity);
                    else if (trimmedPart.StartsWith("Price=")) decimal.TryParse(trimmedPart.Substring("Price=".Length).Trim(), out price);
                    else if (trimmedPart.StartsWith("TotalPrice=")) decimal.TryParse(trimmedPart.Substring("TotalPrice=".Length).Trim(), out price);
                    else if (trimmedPart.StartsWith("Addons="))
                        {
                            string addonString = trimmedPart.Substring("Addons=".Length);
                            string[] individualAddons = addonString.Split(',');
                            foreach (string addon in individualAddons)
                            {
                                string trimmedAddon = addon.Trim();
                                if (trimmedAddon.Contains("("))
                                {
                                    int priceStartIndex = trimmedAddon.IndexOf("(₱");
                                    if (priceStartIndex > 0)
                                    {
                                        string addonNameWithPrefix = trimmedAddon.Substring(0, priceStartIndex).Trim();
                                        string addonName = RemoveAddonPrefix(addonNameWithPrefix);
                                        string pricePart = trimmedAddon.Substring(priceStartIndex + 2); // Skip "(₱"
                                        int priceEndIndex = pricePart.IndexOf(")");
                                        if (priceEndIndex > 0)
                                        {
                                            string addonPriceString = pricePart.Substring(0, priceEndIndex);
                                            if (decimal.TryParse(addonPriceString, out decimal addonPrice))
                                            {
                                                addonsToPrint.Add($"{addonName} (₱{addonPrice:N2})");
                                                totalAddonPriceForItem += addonPrice;
                                            }
                                        }
                                    }
                                    else if (trimmedAddon.Contains(":")) // Handle addons with quantity
                                    {
                                        string[] addonParts = trimmedAddon.Split(':');
                                        if (addonParts.Length == 2)
                                        {
                                            string addonNamePart = addonParts[0].Trim();
                                            string priceInfoPart = addonParts[1].Trim();
                                            int quantityEndIndex = addonNamePart.IndexOf(" (");
                                            int addonQuantity = 1;
                                            string actualAddonName = addonNamePart;
                                            if (quantityEndIndex > 0)
                                            {
                                                string quantityStr = addonNamePart.Substring(quantityEndIndex + 2, addonNamePart.IndexOf(")") - (quantityEndIndex + 2));
                                                if (int.TryParse(quantityStr, out int qty))
                                                {
                                                    addonQuantity = qty;
                                                    actualAddonName = addonNamePart.Substring(0, quantityEndIndex).Trim();
                                                }
                                            }

                                            int priceStartIndexInner = priceInfoPart.IndexOf("(₱");
                                            if (priceStartIndexInner > 0)
                                            {
                                                string pricePartInner = priceInfoPart.Substring(priceStartIndexInner + 2);
                                                int priceEndIndexInner = pricePartInner.IndexOf(")");
                                                if (priceEndIndexInner > 0)
                                                {
                                                    string addonUnitPriceString = pricePartInner.Substring(0, priceEndIndexInner);
                                                    if (decimal.TryParse(addonUnitPriceString, out decimal addonUnitPrice))
                                                    {
                                                        string addonPriceText = $"{(addonUnitPrice * addonQuantity):N2}";
                                                        receiptItemsListBox.Items.Add($"{leftAlign($"  + {actualAddonName}", itemColWidth)}{rightAlign(addonPriceText, priceColWidth)}");
                                                        totalItemsHeight += itemHeight;
                                                        if (addonQuantity > 1)
                                                        {
                                                            receiptItemsListBox.Items.Add(leftAlign($"    {addonQuantity} x {addonUnitPrice:N2}", itemColWidth));
                                                            totalItemsHeight += itemHeight;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string addonName = RemoveAddonPrefix(trimmedAddon);
                                        addonsToPrint.Add(addonName); // Addon without price
                                    }
                                }
                                else
                                {
                                    string addonName = RemoveAddonPrefix(trimmedAddon);
                                    addonsToPrint.Add(addonName); // Addon without price
                                }
                            }
                        }
                    else if (trimmedPart.StartsWith("Price: ₱"))
                    {
                        int priceIndex = trimmedPart.IndexOf("₱");
                        if (priceIndex != -1) decimal.TryParse(trimmedPart.Substring(priceIndex + 1).Trim(), out price);
                        // For simple orders, the name might be the first part if no DrinkName=
                        if (string.IsNullOrEmpty(itemName) && !trimmedPart.StartsWith("Price:"))
                        {
                            itemName = trimmedPart.Trim();
                        }
                    }
                    else if (!trimmedPart.StartsWith("Price:") && !trimmedPart.StartsWith("TotalPrice:") && !trimmedPart.StartsWith("Quantity:") && !trimmedPart.StartsWith("DrinkName:") && !trimmedPart.StartsWith("Temperature:") && !trimmedPart.StartsWith("Addons:"))
                    {
                        itemName = trimmedPart; // Handle simple order names
                    }
                }
                
                 if (!string.IsNullOrEmpty(itemName))
                {
                    string itemWithTemperature = itemName;
                    if (!string.IsNullOrEmpty(temperature))
                    {
                        itemWithTemperature += $"({temperature})";
                    }
                    string priceText = $"₱{price * quantity:N2}";
                    

                    receiptItemsListBox.Items.Add($"{leftAlign(itemWithTemperature, itemColWidth)}{rightAlign(priceText, priceColWidth)}");
                    totalItemsHeight += itemHeight;
                    
                    foreach (string addonText in addonsToPrint)
                    {
                        receiptItemsListBox.Items.Add(leftAlign($" +{addonText}", itemColWidth));
                        totalItemsHeight += itemHeight;
                    }
                    receiptItemsListBox.Items.Add(leftAlign($"{quantity} x ₱{price}", itemColWidth));
                    totalItemsHeight += itemHeight;
                   
                }
            }
            
            receiptItemsListBox.Items.Add(new string('-', itemColWidth + spacing + priceColWidth));
            totalItemsHeight += itemHeight;
            subtotalLabel.Text = $"Subtotal: ₱{_totalAmount:N2}";
            discountLabel.Text = $"Discount: ₱{_discountAmount:N2}";
            decimal finalTotal = _totalAmount - _discountAmount;
            totalAmountLabel.Text = $"Total: ₱{finalTotal:N2}";
            amountTenderedLabel.Text = $"Amount Tendered: ₱{_amountTendered:N2}";
            changeDueLabel.Text = $"Change: ₱{_changeDue:N2}";
            orderTypeLabel.Text = ($"{_orderOption} - {_paymentMethod}");
            thankYouLabel.Text = "Thank you for your purchase!";


            // Add controls to the form's Controls collection
            LayoutControls();
        }

        private string RemoveAddonPrefix(string addonTextWithPrefix)
        {
            
            if (addonTextWithPrefix.StartsWith("Milk: "))
            {
                return addonTextWithPrefix.Substring("Milk: ".Length).Trim();
            }
            if (addonTextWithPrefix.StartsWith("Cream: "))
            {
                return addonTextWithPrefix.Substring("Cream: ".Length).Trim();
            }
            // Add more prefixes as needed
            return addonTextWithPrefix; // Return original if no prefix matches
        }

        private string GenerateOrderNumber()
        {
            // Implement logic to generate a unique order number
            return DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 8);
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            // Show Form2
            this.Close();             // Close the ReceiptForm
                          // Example: form2.ResetOrder();
        }
    }
}