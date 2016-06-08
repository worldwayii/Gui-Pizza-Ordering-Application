using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;

namespace PizzaDis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class Order
        {
            public string name;
            public string phoneNo;
            public string address;
            public string orderCode;
            public int quantity;
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            Order myOrder = new Order();

            myOrder.name = textBox.Text;
            myOrder.phoneNo = textBox1.Text;
            myOrder.address = textBox2.Text;
            myOrder.orderCode = textBox3.Text;
            myOrder.quantity = Convert.ToInt32(textBox4.Text);

            string connectStr = "server=localhost;user=root;database=snack_order_mgt;port=3306;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);
            MySqlCommand mscmd = new MySqlCommand();

            try
            {
                Console.WriteLine("Connecting to MySql...");
                conn.Open();
                mscmd.Connection = conn;
                mscmd.CommandText = "INSERT INTO table_order(customer_name, phone_no, customer_address, cust_order_code, quantity) VALUES (@customer_name, @phone_no, @customer_address, @cust_order_code, @quantity)";
                mscmd.Prepare();

                mscmd.Parameters.AddWithValue("@customer_name", myOrder.name);
                mscmd.Parameters.AddWithValue("@phone_no", myOrder.phoneNo);
                mscmd.Parameters.AddWithValue("@customer_address", myOrder.phoneNo);
                mscmd.Parameters.AddWithValue("@cust_order_code", myOrder.orderCode);
                mscmd.Parameters.AddWithValue("@quantity", myOrder.quantity);
                mscmd.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Error " + ex.Number + " has occurred: " + ex.Message,
                   "Error", MessageBoxButton.OK);
            }
            conn.Close();
            Console.WriteLine("Done!");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            login lg = new login();
            lg.ShowDialog();
        }
    }
}
