using System;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Xml.Serialization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;


namespace DataBaseProject2
{
    public partial class Form1 : Form
    {
        readonly String connectionString = "Data Source=DESKTOP-8AR155P;Initial Catalog=DBProject1;Integrated Security=True;Trust Server Certificate=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double result;
            if (!double.TryParse(textBox5.Text, out result))
            {
                MessageBox.Show("Please enter a valid price for Price Each.");
                return;
            }

            string sql = "INSERT INTO Orders (Item_Type,Item_Name,Qty,Price_Each,Customer_ID) VALUES (@Item_Type,@Item_Name, @Qty, @Price_Each, @Customer_ID)";
            string filepath = "C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt";
            XmlSerializer serializer = new XmlSerializer(typeof(Orders));
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                Orders orders = (Orders)serializer.Deserialize(fs);
                orders.orderlist.Add(new order { Order_ID = Convert.ToInt32(textBox1.Text), Item_Type = textBox2.Text, Item_Name = textBox3.Text, Qty = Convert.ToInt32(textBox4.Text), Price_Each = Convert.ToDouble(textBox5.Text), Customer_ID = Convert.ToInt32(textBox6.Text) });



                using (var connection = new SqlConnection(connectionString))
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    MessageBox.Show("Are you sure you want to add this order?", "Confirmation", buttons);
                    if (buttons.Equals(DialogResult.Cancel))
                    {
                        return;
                        { connection.Close(); }

                    }
                    else
                    {
                        connection.Open();
                        _ = connection.Execute(sql, new { Item_Type = orders.orderlist.ElementAt(orders.orderlist.Count - 1).Item_Type, Item_Name = orders.orderlist.ElementAt(orders.orderlist.Count - 1).Item_Name, Qty = orders.orderlist.ElementAt(orders.orderlist.Count - 1).Qty, Price_Each = orders.orderlist.ElementAt(orders.orderlist.Count - 1).Price_Each, Customer_ID = orders.orderlist.ElementAt(orders.orderlist.Count - 1).Customer_ID });
                        connection.Close();
                    }



                }
            }
            String pulleddata = "select * from Orders for XML Auto, ROOT('Orders'),ELEMENTS";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(pulleddata, conn))
                {
                    conn.Open();
                    String Output = (String)cmd.ExecuteScalar();
                    File.WriteAllText("C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt", Output);

                    conn.Close();
                }

                MessageBox.Show("Textfile Created successfully!");
            }


        }



        private void button2_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            String pulleddata = "select * from Orders for XML Auto, ROOT('Orders'),ELEMENTS";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(pulleddata, conn))
                {
                    conn.Open();
                    String Output = (String)cmd.ExecuteScalar();
                    File.WriteAllText("C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt", Output);

                    conn.Close();
                }


            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true; // Prevents the character from being entered
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true; // Prevents the character from being entered
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true; // Prevents the character from being entered
            }
        }
    }
}
