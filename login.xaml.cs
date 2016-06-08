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
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySql.Data.Entity;

namespace PizzaDis
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        private string conn;
        private MySqlConnection connect;

        public login()
        {
            InitializeComponent();
        }


        private void datab_connection()
        {
            try
            {
                conn = "server=localhost;user=root;database=snack_order_mgt;port=3306;password=root;";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            catch (MySqlException e)
            {
                throw;
            }

        }

        private bool validate_login(string user, string pass)
        {
            datab_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from admin_login where username=@username and password=@password";
            cmd.Parameters.AddWithValue("@username", user);
            cmd.Parameters.AddWithValue("@password", pass);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            string user = textBox.Text;
            string pass = textBox2.Text;
            if (user == "" || pass == "")
            {
                MessageBox.Show("The fields are empty. Please fill up the empty box");
                return;
            }
            bool r = validate_login(user, pass);
            if (r)
                MessageBox.Show("Login Succesful!");
            else
                MessageBox.Show("Incorrect login details");

        }












    }
}
