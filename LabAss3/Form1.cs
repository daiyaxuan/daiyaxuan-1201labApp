using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabAss3
{
    public partial class Form1 : Form
    {
        private object dtgCustomer;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadCustomer();
        }
        private void loadCustomer()
        {

            string strConnection = "Data Source=DCT;Initial Catalog=CustomerDB;Persist Security Info=True;User ID=sa;Password=123";
            SqlConnection objConnection = new SqlConnection(strConnection);
            objConnection.Open();

            string strCommand = "Select * From Customer";
            SqlCommand objCommand = new SqlCommand(strCommand,objConnection );

            DataSet objDataSet = new DataSet();
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            objAdapter.Fill(objDataSet);
            //  dtgCustomer.DataSource = objDataSet.Tables[0];
            dataGridView1.DataSource = objDataSet.Tables[0];
            objConnection.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Gender, Hobby, Status = "";

            if (radioMale.Checked) Gender = "Male";
            else Gender = "Female";
            if (chkReading.Checked) Hobby = "Reading";
            else Hobby = "Painting";
            if (radioMarried.Checked) Status = "Married";
            else Status = "Unmarried";

            try
            {
                CustomerValidation objval = new CustomerValidation();
                objval.CheckCustomerName(txtName.Text);

                frmCustomerPreview objPreview = new frmCustomerPreview();
                objPreview.SetValues(txtName.Text, cmbCountry.Text, Gender, Hobby, Status);
                objPreview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
            
            private void checkBox2_CheckedChanged(object sender, EventArgs e)
            {

            }

        

            private void frmCustomerDataEntry_Load(object sender, EventArgs e)
            {

            }

        private void button2_Click(object sender, EventArgs e)
        {
            string Gender, Hobby, Status = "";
            if (radioMale.Checked) Gender = "Male";
            else Gender = "Female";
            if (chkReading.Checked) Hobby = "Reading";
            else Hobby = "Painting";
            if (radioMarried.Checked) Status = "1";

            string strConnection = "Data Source=DCT;Initial Catalog=CustomerDB;Persist Security Info=True;User ID=sa;Password=123";
            SqlConnection objConnection = new SqlConnection(strConnection);
            objConnection.Open();

            string strCommand = "insert into Customer(CustomerName,Country,Gender,Hobby,Married)values('" + txtName.Text + "','"
                + cmbCountry.Text + "','"
                + Gender + "','"
                + Hobby + ")";
            SqlCommand objCommand = new SqlCommand(strCommand, objConnection);
            objCommand.ExecuteNonQuery();
            objConnection.Close();
            loadCustomer();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clearForm();
            string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            displayCustomer(id);
        }

        private void clearForm()
        {
            txtName.Text = "";
            cmbCountry.Text = "";
            radioMale.Checked = false;
            radioFemale.Checked = false;
            chkReading.Checked = false;
            chkPainting.Checked = false;
            radioMarried.Checked = false;
            radioUnmarried.Checked = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void displayCustomer(string id)
        {
            string strConnection = "Data Source=DCT;Initial Catalog=CustomerDB;Persist Security Info=True;User ID=sa;Password=123";
            SqlConnection objConnection = new SqlConnection(strConnection);
            objConnection.Open();

            string strCommand = "Select * From Customer where id =" + id;
            SqlCommand objCommand = new SqlCommand(strCommand, objConnection);

            DataSet objDataSet = new DataSet();
            SqlDataAdapter objAdapter = new SqlDataAdapter(objCommand);
            objAdapter.Fill(objDataSet);
            objConnection.Close();
            lblID .Text = objDataSet.Tables[0].Rows[0][0].ToString().Trim();
            txtName.Text = objDataSet.Tables[0].Rows[0][1].ToString().Trim();
            cmbCountry.Text = objDataSet.Tables[0].Rows[0][2].ToString().Trim();
            string Gender = objDataSet.Tables[0].Rows[0][3].ToString().Trim();
            if (Gender.Equals("Male")) radioMale.Checked = true;
            else radioFemale.Checked = true;
            string Hobby = objDataSet.Tables[0].Rows[0][4].ToString().Trim();
            if (Hobby.Equals("Reading")) chkReading.Checked = true;
            else chkPainting.Checked = true;
            string Married = objDataSet.Tables[0].Rows[0][5].ToString().Trim();
            if (Married.Equals("True")) radioMarried.Checked = true;
            else radioUnmarried.Checked = true;
                    }

        private void button3_Click(object sender, EventArgs e)
        {
            string Gender, Hobby, Status = "";
            if (radioMale.Checked) Gender = "Male";
            else Gender = "Female";
            if (chkReading.Checked) Hobby = "Reading";
            else Hobby = "Painting";
            if (radioMarried.Checked) Status = "1";
            else Status = "0";

            string strConnection = "Data Source=DCT;Initial Catalog=CustomerDB;Persist Security Info=True;User ID=sa;Password=123";
            SqlConnection objConnection = new SqlConnection(strConnection);
            objConnection.Open();
            string strCommand = "UPDATE Customer SET CustomerName =@CustomerName,Country=@Country," +
                "Gender=@Gender,Hobby=@Hobby,Married= @Married WHERE id =@id";
            SqlCommand objCommand = new SqlCommand(strCommand, objConnection);
            objCommand.Parameters.AddWithValue("@CustomerName", txtName.Text.Trim());
            objCommand.Parameters.AddWithValue("@Country", cmbCountry.SelectedItem.ToString().Trim());
            objCommand.Parameters.AddWithValue("@Gender", Gender);
            objCommand.Parameters.AddWithValue("@Hobby", Hobby);
            objCommand.Parameters.AddWithValue("@Married", Status);
            objCommand.Parameters.AddWithValue("@id", lblID.Text.Trim());
            objCommand.ExecuteNonQuery();
            objConnection.Close();
            clearForm();
            loadCustomer();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strConnection = "Data Source=DCT;Initial Catalog=CustomerDB;Persist Security Info=True;User ID=sa;Password=123";
            SqlConnection objConnection = new SqlConnection(strConnection);
            objConnection.Open();
            string strCommand = "Delete from Customer where id ='" + lblID.Text + "'";
            SqlCommand objCommand = new SqlCommand(strCommand, objConnection);
            objCommand.ExecuteNonQuery();
            objConnection.Close();
            clearForm();
            loadCustomer();
        }
    }
}


