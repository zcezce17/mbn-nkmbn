namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            int formWidth = this.ClientSize.Width;
            int labelWidth = this.label1.PreferredWidth;
            this.label1.Location = new Point((formWidth - labelWidth) / 2, 40);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox2.Text;
            string password = textBox1.Text;

            if (username == "admin" && password == "admin")
            {
                Form2 mainform = new Form2();
                this.Hide();
                mainform.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
