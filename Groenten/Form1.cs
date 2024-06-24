using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Resultset;
using System.Drawing;


namespace Groenten
{
    public partial class Form1 : Form
    {
        string plaatje123 = "C:\\Users\\Gewoo\\Documents\\dumps";
        private decimal totaal;
        string ConnectionString = "Server=localhost;Database=csv_db 9;user=root;";
        List<ReadDBOut> Producten = new List<ReadDBOut>();
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection();
        }

        private void connection()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM groenteboerr";
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Class aanroepen om de waardes mee te geven
                        ReadDBOut readDBOut = new ReadDBOut();
                        readDBOut.Product = reader["Product"].ToString();
                        readDBOut.Prijs = decimal.Parse(reader["Prijs"].ToString());

                        // Lijst updaten met de meegegeven producten
                        Producten.Add(readDBOut);
                        createButtons();
                    }
                    reader.Close();
                    // Functie 
                    //DisplayLijst(Producten);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void plaatjeProduct()
        {

        }

        private void createButtons()
        {
            flowLayoutPanel3.Controls.Clear();

            for (int i = 0; i < Producten.Count; i++)
            {
                ReadDBOut product = Producten[i];
                Button button = new Button();
                button.Text = product.Product;
                button.Tag = i;
                //Image image = Image.FromFile(product.Plaatje);
                //button.Image = image;
                //Image image = Image.FromFile(plaatje);
                //button.Image = new Bitmap(image, new Size(200, 200));
                button.Size = new System.Drawing.Size(163, 157);
                button.Click += new EventHandler(button1_Click);
                flowLayoutPanel3.Controls.Add(button);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productIndex = (int)btn.Tag;

            if (productIndex >= 0 && productIndex < Producten.Count)
            {
                ReadDBOut product = Producten[productIndex];
                bool productExists = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow && row.Cells["Product"].Value != null && row.Cells["Product"].Value.ToString() == product.Product)
                    {
                        int currentAantal = Convert.ToInt32(row.Cells["Aantal"].Value);
                        row.Cells["Aantal"].Value = currentAantal + 1;
                        productExists = true;
                        break;
                    }
                }

                if (!productExists)
                {
                    dataGridView1.Rows.Add(product.Product, product.Prijs, 1);
                }

                totaal += product.Prijs;
                label2.Text = totaal.ToString("C");
            }
            else
            {
                MessageBox.Show("Product index out of range.");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
