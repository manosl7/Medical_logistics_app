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
    public partial class CustomerForm : Form
    {
        public int num;
        

        String connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database1.mdb";
        OleDbConnection connection;

        public CustomerForm()
        {
            InitializeComponent();
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE user_Name FROM CurrentUser", connection);
                OleDbCommand cmd1 = new OleDbCommand("DELETE * FROM Busket", connection);
                cmd.ExecuteNonQuery();
                cmd1.ExecuteNonQuery();
                this.Close();
                //Application.Exit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
            tabControl1.SelectTab("tabpage1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
            tabControl1.SelectTab("tabpage2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
            tabControl1.SelectTab("tabpage3");
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            connection = new OleDbConnection(connectionString);

            try
            {
                connection.Open();
                OleDbCommand cmd = new OleDbCommand("select COUNT(product) from Busket ", connection);
                num = (int)cmd.ExecuteScalar();

                textBox2.Text = num.ToString();

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

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label2.Text + "', '"+ label3.Text +"') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();
                
                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label6.Text + "', '" + label5.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label9.Text + "', '" + label8.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "0")
            {
                MessageBox.Show("Δεν υπάρχει τίποτα στο καλαθι");
            }
            else
            {
                PaymentForm f3 = new PaymentForm();
                f3.Show();
                this.Dispose();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label15.Text + "', '" + label14.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label19.Text + "', '" + label18.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label12.Text + "', '" + label11.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label22.Text + "', '" + label21.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label25.Text + "', '" + label24.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label28.Text + "', '" + label27.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label31.Text + "', '" + label30.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void button16_Click(object sender, EventArgs e)
        {
            try
            {
                num += 1;
                connection.Open();
                String query = "INSERT INTO Busket(product, price)  VALUES ('" + label34.Text + "', '" + label33.Text + "') ";
                OleDbCommand cmd = new OleDbCommand(query, connection);
                cmd.ExecuteNonQuery();

                textBox2.Text = num.ToString();
                MessageBox.Show("Προστέθηκε στο καλάθι");

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

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
