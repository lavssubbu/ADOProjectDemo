using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebApplication6
{
    public partial class Course : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("data source=LAPTOP-BQF0DTHQ\\SQLEXPRESS;database=samp2024;integrated security=true;");
                con.Open();
            DisplayData();

            con.Close();
        }
        protected void DisplayData()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM courselist", con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string filePath = "";
           byte[] imageData = null;
            if (FileUpload1.HasFile)
            {
                string fileName =
               Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileExtension = Path.GetExtension(fileName);
                filePath = "Images/" + fileName;
                FileUpload1.SaveAs(Server.MapPath(filePath));
                // Read file data as binary
                using (BinaryReader reader = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    imageData = reader.ReadBytes(FileUpload1.PostedFile.ContentLength);
                }
            }
           
            string username = txtCourseName.Text;
            string description = txtCourseDesc.Text;
            int price = int.Parse(txtCoursePrice.Text);
            int duration = int.Parse(txtCourseDuration.Text);
            string category = txtCourseCat.Text;

            cmd = new SqlCommand("INSERT INTO courselist(coursetitle,courseimage,coursedescription,courseduration,courseprice,coursecategory) VALUES(@username,@imageData,@description,@duration,@price,@category)", con);
            cmd.Parameters.AddWithValue("@username", username);
           cmd.Parameters.AddWithValue("@imageData", (object)imageData ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@duration", duration);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@category", category);
            con.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Course Added");
            DisplayData();
            con.Close();
        }

        protected void FetchData_Click(object sender, EventArgs e)
        {
            int getdata = int.Parse(IDboxUD.Text);
            FetchRecordById(getdata);
        }
        private void FetchRecordById(int id)
        {
            con.Open();
            cmd = new SqlCommand("SELECT * FROM courselist WHERE courseid=@id",
           con);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtCourseName.Text = reader.GetString(reader.GetOrdinal("coursetitle"));
                    txtCourseDesc.Text = reader.GetString(reader.GetOrdinal("coursedescription"));
                    txtCoursePrice.Text = reader.GetDecimal(reader.GetOrdinal("courseprice")).ToString();
                    txtCourseDuration.Text = reader.GetInt32(reader.GetOrdinal("courseduration")).ToString();
                    txtCourseCat.Text = reader.GetString(reader.GetOrdinal("coursecategory"));

                    // Retrieve binary data
                    byte[] imageData = reader["courseimage"] as byte[];
                    if (imageData != null)
                    {
                        // Convert to Base64 string for display
                        string base64String = Convert.ToBase64String(imageData);
                        imgCourse.ImageUrl = "data:image/png;base64," + base64String; // Adjust MIME type if necessary
                    }
                }
            }
            else
            {
                // Show error message
            }
            reader.Close();
            con.Close();
        }


        protected void Updatelbl_Click(object sender, EventArgs e)
        {
            byte[] imageData = null;
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.SaveAs(Server.MapPath("Images/" + fileName));
                // Read file data as binary
                using (BinaryReader reader = new BinaryReader(FileUpload1.PostedFile.InputStream))
                {
                    imageData = reader.ReadBytes(FileUpload1.PostedFile.ContentLength);
                }
            }

            using (SqlConnection con = new SqlConnection("data source=LAPTOP-BQF0DTHQ\\SQLEXPRESS;database=samp2024;integrated security=true;"))
            {
                con.Open();
                cmd = new SqlCommand("UPDATE courselist SET coursetitle=@title, coursedescription=@description, courseprice=@price, courseduration=@duration, coursecategory=@category, courseimage=@imageData WHERE courseid=@id", con);
                cmd.Parameters.AddWithValue("@id", int.Parse(IDboxUD.Text));
                cmd.Parameters.AddWithValue("@title", txtCourseName.Text);
                cmd.Parameters.AddWithValue("@description", txtCourseDesc.Text);
                cmd.Parameters.AddWithValue("@price", decimal.Parse(txtCoursePrice.Text));
                cmd.Parameters.AddWithValue("@duration", int.Parse(txtCourseDuration.Text));
                cmd.Parameters.AddWithValue("@category", txtCourseCat.Text);
                cmd.Parameters.Add("@imageData", SqlDbType.VarBinary).Value = (object)imageData ?? DBNull.Value;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Course Updated");
                DisplayData();
            }
        }

        protected void Deletelbl_Click(object sender, EventArgs e)
        {
            con.Open();
            int id = int.Parse(IDboxUD.Text);
            // Delete related records in usertbl table

            cmd = new SqlCommand("UPDATE Users SET courseid = NULL WHERE courseid = @id", con);
           
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            // Delete record in courselist table
            cmd = new SqlCommand("DELETE FROM courselist WHERE courseid=@id",
           con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Course Deleted");
            DisplayData();
            con.Close();

        }
        // Add this method to your class
        public string GetImageUrl(object imageData)
        {
            if (imageData != DBNull.Value)
            {
                byte[] bytes = (byte[])imageData;
                string base64String = Convert.ToBase64String(bytes);
                return "data:image/jpeg;base64," + base64String;
            }
            return string.Empty;
        }
    }
}