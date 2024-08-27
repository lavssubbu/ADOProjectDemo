using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication6
{
    public partial class EnrollmentList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
                // Retrieve query string parameters
                string userName = Request.QueryString["userName"];
                string courseId = Request.QueryString["courseId"];

                // Perform actions based on query string parameters
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(courseId))
                {
                    // For example, display the user name and course ID on the page
                    lblSuccess.Text = $"Enrolled Successfully!! User: {userName}, Course ID: {courseId}";
                }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle the submit logic here
            string courseProgress = txtCourseProgress.Text;
            string userName = Request.QueryString["userName"];
            string courseId = Request.QueryString["courseId"];

            // Save the course progress to the database
            SaveCourseProgress(userName, courseId, courseProgress);

         }

        private void SaveCourseProgress(string userName, string courseId, string courseProgress)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ADOConn"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Update query to set the course progress for the specified user and course
                string query = @"UPDATE Users SET courseprogress = @CourseProgress,courseid=@courseId WHERE Username = @Username";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CourseProgress", courseProgress);
                command.Parameters.AddWithValue("@Username", userName);
                command.Parameters.AddWithValue("@CourseId", courseId);

                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Progress updated");
                // Redirect to EnrollmentList.aspx
               Response.Redirect("EnrollmentList.aspx");
            }
        }
    }
}