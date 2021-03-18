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

namespace NguyenHoangLong_5951071052
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        //Ket noi DB
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LE56HA7\LONGISHERE;
            Initial Catalog=DemoCRUD;User ID=sa;Password=1100101010");


        private void GetStudentsRecord()
        {

            //Truy van DB
            SqlCommand cmd = new SqlCommand("SELECT * FROM StudentsTb", con);
            DataTable dt = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordData.DataSource = dt;
        }

        private bool IsValidData()
        {
            if (txtHName.Text == string.Empty
                || txtNName.Text == string.Empty
                || txtAddress.Text == string.Empty
                || string.IsNullOrEmpty(txtPhone.Text)
                || string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Có chỗ chưa nhập dữ liệu !!!",
                    "Lỗi dữ liệu", MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return false;
            }
            return true;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES" +
                    "(@Name, @FatherName, @RollNumber, @Address, @Mobile)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtHName.Text);
                cmd.Parameters.AddWithValue("FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
            }
        }

        public int StudentID;
        private void StudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordData.SelectedRows[0].Cells[0].Value);
            txtHName.Text = StudentRecordData.SelectedRows[0].Cells[1].Value.ToString();
            txtNName.Text = StudentRecordData.SelectedRows[0].Cells[2].Value.ToString();
            txtRoll.Text = StudentRecordData.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordData.SelectedRows[0].Cells[4].Value.ToString();
            txtPhone.Text = StudentRecordData.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE StudentsTb SET "
                    + " Name = @name, FatherName = @FatherName, "
                    + " RollNumber = @RollNumber, Address = @Address, "
                    + " Mobile = @Mobile WHERE StudentID = @ID ", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@name", txtHName.Text);
                cmd.Parameters.AddWithValue("FatherName", txtNName.Text);
                cmd.Parameters.AddWithValue("@RollNumber", txtRoll.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtPhone.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
                //ResetData();
            }
            else
            {
                MessageBox.Show("Cập nhật bị lỗi!!!", "Lỗi !",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE "
                    + " StudentID= @ID", con);
                cmd.CommandType = CommandType.Text;

                
                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                GetStudentsRecord();
                //ResetData();
            }
            else
            {
                MessageBox.Show("Xoá bị lỗi!!!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddButton_Click_1(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
