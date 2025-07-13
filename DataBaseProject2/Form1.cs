using System;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Runtime.Serialization.DataContracts;
using System.Xml;
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
        private DataTable orderdatatable;
        private DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
            LoadOrders();
            this.ActiveControl = textBox1;

        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
           
            
        }
        private void ReLoadCombo()
        {
           
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            textBox1.Focus();
            // Why is the code below in this form? All connections should be done in a connection object.
            const string sql = "SELECT * FROM Orders";
            using (var connection = new SqlConnection(connectionString))
            {
                List<order> orderlist = new List<order>();
    
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);

                    dt.Columns.Add("updateOrder", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["updateOrder"] = $"{row["Order_ID"]} {row["Item_Type"]} {row["Item_Name"]} {row["Qty"]} {row["Price_Each"]} {row["Customer_ID"]}";
                    }

                    comboBox1.DataSource = dt;
                    comboBox1.ValueMember = "updateOrder";
               
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty || textBox5.Text == string.Empty || textBox6.Text == string.Empty)
            {
                MessageBox.Show("Please fill in all fields before submitting.");
                textBox1.Focus();
                return;
            }
            double result;
            if (!double.TryParse(textBox5.Text, out result))
            {
                MessageBox.Show("Please enter a valid price for Price Each.");
                return;
            }

            string sql = "INSERT INTO Orders (Item_Type,Item_Name,Qty,Price_Each,Customer_ID) VALUES (@Item_Type,@Item_Name, @Qty, @Price_Each, @Customer_ID)";
            // Why is this code here? All file access code should be in a seperate class of its own.
            string filepath = "C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt";
            XmlSerializer serializer = new XmlSerializer(typeof(Orders));
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                Orders orders = (Orders)serializer.Deserialize(fs);
                orders.orderlist.Add(new order { Order_ID = Convert.ToInt32(textBox1.Text), Item_Type = textBox2.Text, Item_Name = textBox3.Text, Qty = Convert.ToInt32(textBox4.Text), Price_Each = Convert.ToDouble(textBox5.Text), Customer_ID = Convert.ToInt32(textBox6.Text) });



                using (var connection = new SqlConnection(connectionString))
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    MessageBox.Show("Are you sure you want to Update this order?", "Confirmation", buttons);
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
            // This code should be in its own class
            String pulleddata = "select * from Orders for XML PATH('order'), Root('Orders')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(pulleddata, conn))
                {
                    conn.Open();
                    using (XmlReader reader = cmd.ExecuteXmlReader())
                    {
                        XmlDocument doc = new XmlDocument();
                        List<order> orderlist = new List<order>();
                        doc.Load(reader);
                        doc.Save("C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt");
                    }
                }
            }


        }



        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty || textBox5.Text == string.Empty || textBox6.Text == string.Empty)
            {
                MessageBox.Show("Please fill in all fields before submitting.");
                textBox1.Focus();
                return;
            }
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
                    MessageBox.Show("Are you sure you want to Create this order?", "Confirmation", buttons);
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
            String pulleddata = "select * from Orders for XML PATH('order'), Root('Orders')";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(pulleddata, conn))
                {
                    conn.Open();
                    using (XmlReader reader = cmd.ExecuteXmlReader())
                    {
                        XmlDocument doc = new XmlDocument();
                        List<order> orderlist = new List<order>();
                        doc.Load(reader);
                        doc.Save("C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt");
                    }
                }
            }
          
            ReLoadCombo();
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
        private void LoadOrders()
        {
            textBox1.Focus();
            // This code should be in its own class
            const string sql = "SELECT * FROM Orders";
            using (var connection = new SqlConnection(connectionString))
            {
                
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);

                    dt.Columns.Add("fullorder", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        row["fullorder"] = $"{row["Order_ID"]} {row["Item_Type"]} {row["Item_Name"]} {row["Qty"]} {row["Price_Each"]} {row["Customer_ID"]}";
                    }
                    comboBox1.DataSource = dt;
                    comboBox1.ValueMember = "fullorder";
                }
            }
        }



       

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            var drv = comboBox1.SelectedItem as DataRowView;
            // Why is this not in its own class?
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Orders";
                using (var command = new SqlCommand(query, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);

                }
            }




            if (drv != null)
            {
                textBox1.Text = drv.Row["Order_ID"].ToString();
                textBox2.Text = drv.Row["Item_Type"].ToString();
                textBox3.Text = drv.Row["Item_Name"].ToString();
                textBox4.Text = drv.Row["Qty"].ToString();
                textBox5.Text = drv.Row["Price_Each"].ToString();
                textBox6.Text = drv.Row["Customer_ID"].ToString();


            }
        }
    }
}
