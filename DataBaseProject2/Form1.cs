using System;
using System.Data;
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

            string sql = "INSERT INTO Orders (Item_Type,Item_Name,Qty,Price_Each,Customer_ID) VALUES (@Item_Type,@Item_Name, @Qty, @Price_Each, @Customer_ID)";
            string filepath = "C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt";
            XmlSerializer serializer = new XmlSerializer(typeof(Orders));
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                Orders orders = (Orders)serializer.Deserialize(fs); 


                for (int i = 0; i < orders.orderlist.Count; i++)
                {

                    MessageBox.Show(orders.orderlist.ElementAt(i).Item_Type);
                }
                using (var connection = new SqlConnection(connectionString))
                {
                    for (int i = 0; i < orders.orderlist.Count; i++)
                    {
                        connection.Open();
                        _ = connection.Execute(sql, new { Item_Type = orders.orderlist.ElementAt(i).Item_Type, Item_Name = orders.orderlist.ElementAt(i).Item_Name, Qty = orders.orderlist.ElementAt(i).Qty, Price_Each = orders.orderlist.ElementAt(i).Price_Each, Customer_ID = orders.orderlist.ElementAt(i).Customer_ID });
                        connection.Close();
                    }
                }
            }


        }



        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "Select * from Orders ";
            string connectionstring = "C:\\Users\\chris\\source\\repos\\DataBaseProject2\\DatabaseProject2.xml.txt";
            XmlSerializer serializer = new XmlSerializer(typeof(Orders));
            using (TextWriter fs = new StreamWriter(connectionstring))
            {
                Orders orders = new Orders();


                serializer.Serialize(fs, orders);


                for (int i = 0; i < orders.orderlist.Count; i++)
                {

                    MessageBox.Show(orders.orderlist.ElementAt(i).Item_Type);
                }
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var orders1 = connection.Query<Orders>(sql);
                    SqlDataAdapter da = new SqlDataAdapter(sql, connection);



                    connection.Close();
                }
            }
        }
    }
}
