using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace LogisticsApp
{
    public partial class Form1 : Form
    {

        //Dim strHelpPath As String  = System.IO.Path.Combine(Application.StartupPath, "e-BookOnlineHelp.chm");

        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database1.mdb";
        OleDbConnection connection;
        OleDbDataAdapter da;
        OleDbDataAdapter da1;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();


        public Form1()
        {
            InitializeComponent();
            panel4.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel4.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                connection.Open();
                da = new OleDbDataAdapter("select * from Customer where user_Name='" + textBox1.Text + "' and password='" + textBox2.Text + "'", connection);
                da.Fill(dt);
                da1 = new OleDbDataAdapter("select * from Employee where user_Name='" + textBox1.Text + "' and password='" + textBox2.Text + "'", connection);
                da1.Fill(dt1);

                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Παρακαλώ συμπληρώστε όλα τα πεδία!");
                }
                else if (dt.Rows.Count > 0)
                {

                    String query = "INSERT INTO CurrentUser(user_Name)  VALUES ('" + textBox1.Text + "') ";
                    OleDbCommand cmd = new OleDbCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    CustomerForm f2 = new CustomerForm();
                    f2.Show();
                }
                else if(dt1.Rows.Count > 0 )
                {
                    EmployeeForm f = new EmployeeForm();
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Ο χρήστης δεν υπάρχει");
                }
            }
            catch(Exception ex)
            {               
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand check_User = new OleDbCommand("select COUNT(*) from Customer where user_Name = '" + textBox3.Text + "'", connection);
            int user_Exists = (int)check_User.ExecuteScalar();

            try
            {

                if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Παρακαλώ συμπληρώστε όλα τα πεδία!");
                }
                else
                {
                    if (user_Exists == 0)
                    {
                        if (textBox4.Text == textBox5.Text)
                        {
                            String query = "INSERT INTO Customer(user_Name , [password]) VALUES ('" + textBox3.Text + "','" + textBox4.Text + "')";
                            OleDbCommand cmd = new OleDbCommand(query, connection);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Επιτυχής δημιουργία λογαριασμού!!");
                        }
                        else
                        {
                            MessageBox.Show("Ο κωδικός που επαναλάβατε δε συμπίπτει!");

                        }

                    }
                    else
                    {
                        MessageBox.Show("Υπάρχει ήδη άλλος χρήστης με αυτό το όνομα!!" + Environment.NewLine + "Παρακαλώ επιλέξτε διαφορετικό όνομα.");

                    }
                }
                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel4.Hide();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ContactUs cu = new ContactUs();
            cu.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\LogisticsAppHelp\\LogisticsAppHelp.chm";
            Help.ShowHelp(this, filePath);
        }
    }
}
