using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Resultset;


namespace Groenten
{
    public partial class Form1 : Form
    {
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

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int productIndex = -1;

            switch (btn.Name)
            {
                case "button1":
                    productIndex = 0;
                    break;
                case "button2":
                    productIndex = 1;
                    break;
                case "button3":
                    productIndex = 2;
                    break;
                case "button4":
                    productIndex = 3;
                    break;
                case "button5":
                    productIndex = 7;
                    break;
                case "button6":
                    productIndex = 6;
                    break;
                case "button7":
                    productIndex = 5;
                    break;
                case "button8":
                    productIndex = 4;
                    break;
                case "button9":
                    productIndex = 8;
                    break;
                case "button10":
                    productIndex = 9;
                    break;
                case "button11":
                    productIndex = 10;
                    break;
                case "button12":
                    productIndex = 11;
                    break;
                case "button13":
                    productIndex = 15;
                    break;
                case "button14":
                    productIndex = 14;
                    break;
                case "button15":
                    productIndex = 13;
                    break;
                case "button16":
                    productIndex = 12;
                    break;
                default:
                    MessageBox.Show("Fruit bestaat niet");
                    return;
            }

            if (Producten.Count > productIndex)
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
    }
}
