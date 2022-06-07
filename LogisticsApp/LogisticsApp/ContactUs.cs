using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogisticsApp
{
    public partial class ContactUs : Form
    {

        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database1.mdb";
        OleDbConnection connection;

        public ContactUs()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ContactUs_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == ""))
            {
                MessageBox.Show("Παρακαλούμε συμπληρώστε τα απαιτούμενα πεδία!");
            }
            else
            {
                if (textBox3.Text.Length != 10)
                {
                    MessageBox.Show("Ο αριθμός τηλεφώνου πρέπει να αποτελείται από 10 ακριβώς ψηφία!");
                }
                else
                {
                    try
                    {

                        connection.Open();



                        String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");

                        String query = "INSERT INTO CustomerContact(user_Name,email,emailContent,dateSent) " +
                            "VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + dateTime + "')";
                        OleDbCommand cmd3 = new OleDbCommand(query, connection);
                        cmd3.ExecuteNonQuery();


                        MessageBox.Show("Το μήνυμά σας παραδόθηκε επιτυχώς!");
                        textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch))
            {
                e.Handled = true;
                MessageBox.Show("Μόνο γράμματα επιτρέπονται!");
            }
        }

        private void textBox3_KeyPress(object sender,KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
                MessageBox.Show("Μόνο αριθμοί επιτρέπονται!");
            }
        }


    }
}
