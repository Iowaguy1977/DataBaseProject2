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
    public partial class Form2 : Form
    {
        readonly String connectionString = "Data Source=DESKTOP-8AR155P;Initial Catalog=DBProject1;Integrated Security=True;Trust Server Certificate=True";
        private DataTable orderdatatable;
        private DataTable dt = new DataTable();
        string selected = string.Empty;
        private Form1 _parent;
        public Form2(Form1 parent)
        {
            InitializeComponent();
            _parent = parent;
            LoadOrders();
        }

        private void LoadOrders()
        {

            dt.Clear();
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
           
            const string sql = "SELECT * FROM Orders";
            using (var connection = new SqlConnection(connectionString))
            {

                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);


                    comboBox1.DisplayMember = "Order_ID";
                    comboBox1.DataSource = dt;
                    comboBox1.ValueMember = "Order_ID";

                }
            }

        }
        private void ReLoadCombo()
        {



            string sql = "SELECT * FROM Orders Where Order_ID = @OrderId";
            using (var connection = new SqlConnection(connectionString))
            {


                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    command.Parameters.AddWithValue("@OrderId", selected);


                    adapter.Fill(dt);
                    dt.Columns.Add("selectedValue", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {

                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(row["selectedValue".ToString()] = $"{row["Order_ID"]}", row["Item_Type"] = $"{row["Item_Type"]}", row["Item_Name"] = $"{row["Item_Name"]}", row["Qty"] = $"{row["Qty"]}", row["Price_Each"] = $"{row["Price_Each"]}", row["Customer_ID"] = $"{row["Customer_ID"]}");
                    }



                    dt.Columns.Remove("selectedValue".ToString());

                    LoadOrders();

                }
            }
        }


        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            selected = comboBox1.SelectedValue.ToString();
            var drv = comboBox1.SelectedItem as DataRowView;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (drv != null)
                {
                    _parent.textBox1.Text = drv.Row["Order_ID"].ToString();
                    _parent.textBox2.Text = drv.Row["Item_Type"].ToString();
                    _parent.textBox3.Text = drv.Row["Item_Name"].ToString();
                    _parent.textBox4.Text = drv.Row["Qty"].ToString();
                    _parent.textBox5.Text = drv.Row["Price_Each"].ToString();
                    _parent.textBox6.Text = drv.Row["Customer_ID"].ToString();


                }

                dt.Clear();
                comboBox1.DataSource = null;
                comboBox1.Items.Clear();
                ReLoadCombo();
            }

        }

        
    }
}
