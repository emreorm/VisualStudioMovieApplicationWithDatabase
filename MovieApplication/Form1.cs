using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace MovieApplication
{
    
    
    public partial class Form1 : Form
    {
        public static BusinessManager bs = new BusinessManager();
        public Form1()
        {
            InitializeComponent();
            gridUpdate();
        }
        public void gridUpdate()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=EMRE;Initial Catalog=MovieApplication;Integrated Security=True");
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from MovieApplicationData", connection);
            DataSet ds = new DataSet();
            connection.Open();
            sqlDataAdapter.Fill(ds, "MovieApplicationData");
            dataGridView1.DataSource = ds.Tables[0];
            
            
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=EMRE;Initial Catalog=MovieApplication;Integrated Security=True");
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("insert into MovieApplicationData(ProducerName,MovieName,Year,Country,Spicy) values (@p1,@p2,@p3,@p4,@p5)", connection);
                cmd.Parameters.AddWithValue("@p1", textBox2.Text);
                cmd.Parameters.AddWithValue("@p2", textBox1.Text);
                cmd.Parameters.AddWithValue("@p3", textBox3.Text);
                cmd.Parameters.AddWithValue("@p4", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@p5", comboBox2.SelectedItem.ToString());

                //" ('" + textBox2.Text.ToUpper() + "', '" + textBox1.Text.ToUpper() + "','" + dateTimePicker1.Value + "','" + comboBox1.SelectedItem.ToString() + "','"+comboBox2.SelectedItem.ToString() + "')";

                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Successfully added", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridUpdate();

            }
            catch
            {
                connection.Close();
                MessageBox.Show("Unsuccessful operation!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            //string[] dates = dgvDoctors.CurrentRow.Cells[3].Value.ToString().Split('/');

            //DateTime dtime = new DateTime(Convert.ToInt32(dates[2]), Convert.ToInt32(dates[0]), Convert.ToInt32(dates[1]));

            comboBox1.SelectedItem = dataGridView1.CurrentRow.Cells[3].Value.ToString(); //dtime;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=EMRE;Initial Catalog=MovieApplication;Integrated Security=True"))
                {
                    string query = "SELECT * FROM MovieApplicationData";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds, "MovieApplicationData");
                        dataGridView3.DataSource = ds.Tables[0];
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                textBox4.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=EMRE;Initial Catalog=MovieApplication;Integrated Security=True"))
            {
                string searchText = textBox4.Text.ToUpper();
                string query = "SELECT * FROM MovieApplicationData WHERE UPPER(MovieName) LIKE UPPER(@moviename)";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    adapter.SelectCommand.Parameters.AddWithValue("@moviename", "%" + searchText + "%");

                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "MovieApplicationData");
                    dataGridView2.DataSource = ds.Tables[0];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dr == DialogResult.Yes)
            {
                SqlConnection connection = new SqlConnection(@"Data Source=EMRE;Initial Catalog=MovieApplication;Integrated Security=True");

                try
                {
                   connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "delete from MovieApplicationData where MovieName ='" + textBox5.Text.ToUpper() + "'";
                    cmd.Connection=connection;
                    int x = cmd.ExecuteNonQuery();
                    connection.Close() ;
                    if (x == 1)
                    {
                        MessageBox.Show("Successfully removed!", "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gridUpdate();
                    }
                    else
                        MessageBox.Show("Unsuccessful operation!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                catch
                {
                    connection.Close();
                    MessageBox.Show("Unsuccessful operation!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this);
            f.Visible = true;
            this.Enabled = false;
        }
    }
}
