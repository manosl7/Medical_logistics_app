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

namespace LogisticsApp
{
    public partial class PaymentForm : Form
    {
        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database1.mdb";
        OleDbConnection connection;

        CustomerForm f2 = new CustomerForm();

        public PaymentForm()
        {
            InitializeComponent();

            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;
            textBox8.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT product from Busket", connection);
                adapter.Fill(ds);
                this.listBox1.DataSource = ds.Tables[0];
                this.listBox1.DisplayMember = "product";


                OleDbCommand cmd = new OleDbCommand("SELECT SUM(price) AS totalAmount FROM Busket ", connection)
                {
                    CommandType = CommandType.Text
                };

                OleDbDataReader dr = cmd.ExecuteReader();
                {
                    dr.Read();
                    textBox9.Text = (dr["totalAmount"].ToString() + "€");
                }

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

        private void button2_Click(object sender, EventArgs e)
        {
            CustomerForm f = new CustomerForm();
            f.Show();
            this.Dispose();
            
        }


        BindingList<int> data = new BindingList<int>();

        private void ShowData()
        {
            listBox1.DataSource = data;
        }


        DataSet AllPairs = new DataSet();
        private void button3_Click(object sender, EventArgs e)
        {
            
            String value = listBox1.GetItemText(listBox1.SelectedItem);
            

            try
            {  
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE  FROM (SELECT TOP 1 * FROM Busket WHERE product = '"+ value +"')", connection);
               
                cmd.ExecuteNonQuery();

                OleDbCommand cmd2 = new OleDbCommand("SELECT SUM(price) AS totalAmount FROM Busket ", connection)
                {
                    CommandType = CommandType.Text
                };

                OleDbDataReader dr = cmd2.ExecuteReader();
                {
                    dr.Read();
                    textBox9.Text = (dr["totalAmount"].ToString() + "€");
                }

                DataSet ds = new DataSet();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT product from Busket", connection);
                adapter.Fill(ds);
                this.listBox1.DataSource = ds.Tables[0];
                this.listBox1.DisplayMember = "product";

                


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

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0)
            {
                if (radioButton1.Checked is true)
                {
                    if (radioButton3.Checked is true)
                    {
                        if ((textBox1.Text == "") || (textBox2.Text == ""))
                        {
                            MessageBox.Show("Παρακαλούμε συμπληρώστε όλα τα προσωπικά σας στοιχεία");
                        }
                        else
                        {
                            
                            textBox9.Text = "";
                            try
                            {
                                connection.Open();

                                DataSet ds = new DataSet();
                                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT product from Busket", connection);
                                adapter.Fill(ds);
                                

                                StringBuilder output = new StringBuilder();
                                foreach (DataRow rows in ds.Tables[0].Rows)
                                {
                                    foreach (DataColumn col in ds.Tables[0].Columns)
                                    {
                                        output.AppendFormat("{0} ", rows[col]);
                                    }

                                    output.AppendLine();
                                }

                                String currentUser;
                                OleDbCommand cmd = new OleDbCommand("SELECT (user_Name) AS currentUser from CurrentUser", connection)
                                {
                                    CommandType = CommandType.Text
                                };

                                OleDbDataReader dr = cmd.ExecuteReader();
                                {
                                    dr.Read();
                                    currentUser = (dr["currentUser"].ToString()); 
                                }

                                String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
                                String orderCode = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                                OleDbCommand cmd2 = new OleDbCommand("UPDATE Customer SET Customer.orderContent= '" + output.ToString() + "',Customer.orderDate='"+dateTime+"'" +
                                    ",Customer.orderCode = '"+orderCode+"' " +
                                  "WHERE Customer.[user_Name]= '" + currentUser + "'", connection);

                                cmd2.ExecuteNonQuery();

                                OleDbCommand cmd1 = new OleDbCommand("DELETE * FROM Busket", connection);

                                cmd1.ExecuteNonQuery();

                                
                                String query = "INSERT INTO CustomerOrder(user_Name,orderContent,orderDate,orderCode) " +
                                    "VALUES('"+currentUser+"','"+output.ToString()+ "','" + dateTime + "','" + orderCode + "')";
                                OleDbCommand cmd3 = new OleDbCommand(query, connection);
                                cmd3.ExecuteNonQuery();

                                
                                MessageBox.Show("Η παραγγελία καταχωρήθηκε με επιτυχία." + Environment.NewLine +
                                "Κωδικός παραγγελίας: " + orderCode + Environment.NewLine +
                                "Παρακαλούμε περάστε απ'το ταμείο για την ολοκλήρωση της συναλλαγής");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }
                            f2.Show();
                            this.Dispose();

                        }
                    }
                    else if (radioButton4.Checked is true)
                    {
                        if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") ||
                            (textBox4.Text == "") || (comboBox1.SelectedItem is null) || (comboBox2.SelectedItem is null))
                        {
                            MessageBox.Show("Παρακαλούμε συμπληρώστε όλα τα προσωπικά σας στοιχεία");
                        }
                        else
                        {
                            if (textBox3.Text.Length != 16)
                            {
                                MessageBox.Show("Ο αριθμός κάρτας πρέπει να αποτελείται από 16 ακριβώς χαρακτήρες");
                            }
                            else if (textBox4.Text.Length != 3)
                            {
                                MessageBox.Show("Ο αριθμός ασφαλείας κάρτας πρέπει να αποτελείται από 3 ακριβώς χαρακτήρες");
                            }
                            else
                            {
                                
                                try
                                {
                                    connection.Open();

                                    DataSet ds = new DataSet();
                                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT product from Busket", connection);
                                    adapter.Fill(ds);


                                    StringBuilder output = new StringBuilder();
                                    foreach (DataRow rows in ds.Tables[0].Rows)
                                    {
                                        foreach (DataColumn col in ds.Tables[0].Columns)
                                        {
                                            output.AppendFormat("{0} ", rows[col]);
                                        }

                                        output.AppendLine();
                                    }

                                    String currentUser;
                                    OleDbCommand cmd = new OleDbCommand("SELECT (user_Name) AS currentUser from CurrentUser", connection)
                                    {
                                        CommandType = CommandType.Text
                                    };

                                    OleDbDataReader dr = cmd.ExecuteReader();
                                    {
                                        dr.Read();
                                        currentUser = (dr["currentUser"].ToString());
                                    }


                                    String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
                                    String orderCode = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                                    OleDbCommand cmd2 = new OleDbCommand("UPDATE Customer SET Customer.orderContent= '" + output.ToString() + "',Customer.orderDate='" + dateTime + "'" +
                                        ",Customer.orderCode = '" + orderCode + "' " +
                                      "WHERE Customer.[user_Name]= '" + currentUser + "'", connection);

                                    cmd2.ExecuteNonQuery();

                                    OleDbCommand cmd1 = new OleDbCommand("DELETE * FROM Busket", connection);

                                    cmd1.ExecuteNonQuery();


                                    String query = "INSERT INTO CustomerOrder(user_Name,orderContent,orderDate,orderCode) " +
                                        "VALUES('" + currentUser + "','" + output.ToString() + "','" + dateTime + "','" + orderCode + "')";
                                    OleDbCommand cmd3 = new OleDbCommand(query, connection);
                                    cmd3.ExecuteNonQuery();

                                    MessageBox.Show("Η παραγγελία καταχωρήθηκε με επιτυχία." + Environment.NewLine +
                                    "Κωδικός παραγγελίας: " + orderCode + Environment.NewLine +
                                    "Παρακαλούμε περάστε απ'το ταμείο για την παραλαβή!");
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    connection.Close();
                                }
                                f2.Show();
                                this.Dispose();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Παρακαλούμε επιλέξτε μέθοδο πληρωμής");
                    }
                }
                else if (radioButton2.Checked is true)
                {
                    if ((textBox5.Text == "") || (textBox6.Text == "") || (textBox7.Text == "") || (textBox8.Text == ""))
                    {
                        MessageBox.Show("Παρακαλούμε συμπληρώστε όλα τα πεδία στη διεύθυνση αποστολής");
                    }
                    else
                    {
                        if (textBox7.Text.Length != 5)
                        {
                            MessageBox.Show("Ο Τ.Κ. πρέπει να αποτελείται από 5 ακριβώς χαρακτήρες");
                        }
                        else
                        {

                            if (radioButton3.Checked is true)
                            {
                                if (textBox1.Text == "" || textBox2.Text == "")
                                {
                                    MessageBox.Show("Παρακαλούμε συμπληρώστε όλα τα προσωπικά σας στοιχεία");
                                }
                                else
                                {
                                    try
                                    {
                                        connection.Open();

                                        DataSet ds = new DataSet();
                                        OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT product from Busket", connection);
                                        adapter.Fill(ds);


                                        StringBuilder output = new StringBuilder();
                                        foreach (DataRow rows in ds.Tables[0].Rows)
                                        {
                                            foreach (DataColumn col in ds.Tables[0].Columns)
                                            {
                                                output.AppendFormat("{0} ", rows[col]);
                                            }

                                            output.AppendLine();
                                        }

                                        String currentUser;
                                        OleDbCommand cmd = new OleDbCommand("SELECT (user_Name) AS currentUser from CurrentUser", connection)
                                        {
                                            CommandType = CommandType.Text
                                        };

                                        OleDbDataReader dr = cmd.ExecuteReader();
                                        {
                                            dr.Read();
                                            currentUser = (dr["currentUser"].ToString());
                                        }


                                        String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
                                        String orderCode = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                                        OleDbCommand cmd2 = new OleDbCommand("UPDATE Customer SET Customer.orderContent= '" + output.ToString() + "',Customer.orderDate='" + dateTime + "'" +
                                            ",Customer.orderCode = '" + orderCode + "' " +
                                          "WHERE Customer.[user_Name]= '" + currentUser + "'", connection);

                                        cmd2.ExecuteNonQuery();

                                        OleDbCommand cmd1 = new OleDbCommand("DELETE * FROM Busket", connection);

                                        cmd1.ExecuteNonQuery();


                                        String query = "INSERT INTO CustomerOrder(user_Name,orderContent,orderDate,orderCode) " +
                                            "VALUES('" + currentUser + "','" + output.ToString() + "','" + dateTime + "','" + orderCode + "')";
                                        OleDbCommand cmd3 = new OleDbCommand(query, connection);
                                        cmd3.ExecuteNonQuery();

                                        MessageBox.Show("Η παραγγελία καταχωρήθηκε με επιτυχία." + Environment.NewLine +
                                        "Κωδικός παραγγελίας: " + orderCode + Environment.NewLine +
                                        "Η παράδοση θα πραγματοποιηθεί εντός 3 ημερών");
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                    finally
                                    {
                                        connection.Close();
                                    }
                                    f2.Show();
                                    this.Dispose();
                                }
                            }
                            else if (radioButton4.Checked is true)
                            {
                                if ((textBox1.Text == "") || (textBox2.Text == "") || (textBox3.Text == "") || (textBox4.Text == "") ||
                                (comboBox1.SelectedItem is null) || (comboBox2.SelectedItem is null))
                                {
                                    MessageBox.Show("Παρακαλούμε συμπληρώστε όλα τα προσωπικά σας στοιχεία");
                                }
                                else
                                {
                                    if (textBox3.Text.Length != 16)
                                    {
                                        MessageBox.Show("Ο αριθμός κάρτας πρέπει να αποτελείται από 16 ακριβώς χαρακτήρες");
                                    }
                                    else if (textBox4.Text.Length != 3)
                                    {
                                        MessageBox.Show("Ο αριθμός ασφαλείας κάρτας πρέπει να αποτελείται από 3 ακριβώς χαρακτήρες");
                                    }
                                    else
                                    {
                                        
                                        try
                                        {
                                            connection.Open();

                                            DataSet ds = new DataSet();
                                            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT product from Busket", connection);
                                            adapter.Fill(ds);


                                            StringBuilder output = new StringBuilder();
                                            foreach (DataRow rows in ds.Tables[0].Rows)
                                            {
                                                foreach (DataColumn col in ds.Tables[0].Columns)
                                                {
                                                    output.AppendFormat("{0} ", rows[col]);
                                                }

                                                output.AppendLine();
                                            }

                                            String currentUser;
                                            OleDbCommand cmd = new OleDbCommand("SELECT (user_Name) AS currentUser from CurrentUser", connection)
                                            {
                                                CommandType = CommandType.Text
                                            };

                                            OleDbDataReader dr = cmd.ExecuteReader();
                                            {
                                                dr.Read();
                                                currentUser = (dr["currentUser"].ToString());
                                            }


                                            String dateTime = DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
                                            String orderCode = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
                                            OleDbCommand cmd2 = new OleDbCommand("UPDATE Customer SET Customer.orderContent= '" + output.ToString() + "',Customer.orderDate='" + dateTime + "'" +
                                                ",Customer.orderCode = '" + orderCode + "' " +
                                              "WHERE Customer.[user_Name]= '" + currentUser + "'", connection);

                                            cmd2.ExecuteNonQuery();

                                            OleDbCommand cmd1 = new OleDbCommand("DELETE * FROM Busket", connection);

                                            cmd1.ExecuteNonQuery();


                                            String query = "INSERT INTO CustomerOrder(user_Name,orderContent,orderDate,orderCode) " +
                                                "VALUES('" + currentUser + "','" + output.ToString() + "','" + dateTime + "','" + orderCode + "')";
                                            OleDbCommand cmd3 = new OleDbCommand(query, connection);
                                            cmd3.ExecuteNonQuery();

                                            
                                            MessageBox.Show("Επιτυχής συναλλαγή!" + Environment.NewLine +
                                            "Κωδικός παραγγελίας: " + orderCode + Environment.NewLine +
                                            "Η παράδοση θα πραγματοποιηθεί εντός 3 ημερών");
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }
                                        finally
                                        {
                                            connection.Close();
                                        }
                                        f2.Show();
                                        this.Dispose();
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Παρακαλούμε επιλέξτε μέθοδο πληρωμής");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Παρακαλούμε επίλεξτε μέθοδο παραλαβής");
                }
            }
            else
            {
                MessageBox.Show("Δεν υπάρχει τίποτα στο καλάθι για να αγοράσετε");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}
