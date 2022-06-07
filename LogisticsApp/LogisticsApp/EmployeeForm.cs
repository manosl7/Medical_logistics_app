using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogisticsApp
{

   
    public partial class EmployeeForm : Form
    {
        

        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database1.mdb";
        OleDbConnection connection;

        public EmployeeForm()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;


            button5.Hide();
            radioButton1.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);
            connection.Open();
            String query = "SELECT * FROM CustomerOrder";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
            // creating a DataSet object
            DataSet ds = new DataSet();
            // filling table Order  
            dataAdapter.Fill(ds, "[CustomerOrder]");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
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

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            tabControl1.SelectTab("tabpage1");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new OleDbConnection(connectionString);
                connection.Open();

                if (radioButton1.Checked)
                {
                    String ordercode = textBox1.Text.ToString();
                    String my_querry = "UPDATE CustomerOrder SET CustomerOrder.orderState ='ok' " +
                                        "WHERE CustomerOrder.[orderCode]= '" + ordercode + "'";

                    OleDbCommand cmd = new OleDbCommand(my_querry, connection);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Τα δεδομένα αποθηκεύτηκαν με επιτυχία!");
                    String query = "SELECT * FROM CustomerOrder";
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
                    // creating a DataSet object
                    DataSet ds = new DataSet();
                    // filling table Order  
                    dataAdapter.Fill(ds, "[CustomerOrder]");
                    dataGridView1.DataSource = ds.Tables[0].DefaultView;

                    button5.Hide();
                    radioButton1.Hide();
                    textBox1.Text = "Εισάγετε κωδικό παραγγελίας";
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Αποτυχία λόγω" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            tabControl1.SelectTab("tabpage2");
            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream("LogisticsApp.track2.html"));
            webBrowser1.DocumentText = reader.ReadToEnd();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                connection = new OleDbConnection(connectionString);

                connection.Open();
                String query = "SELECT * FROM  CustomerOrder where orderCode like '%" + textBox1.Text.ToString() + "%'";

                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connection);
                // creating a DataSet object
                DataSet ds = new DataSet();
                // filling table Order  
                dataAdapter.Fill(ds, "[CustomerOrder]");

                if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("Δεν υπάρχει παραγγελία με αυτόν τον κωδικό!");
                }
                else
                {

                    dataGridView1.DataSource = ds.Tables[0].DefaultView;

                    button5.Show();
                    radioButton1.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connection.Close();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
