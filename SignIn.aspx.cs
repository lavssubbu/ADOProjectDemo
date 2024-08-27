using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace WebApplication6
{
    public partial class SignIn : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("data source=LAPTOP-BQF0DTHQ\\SQLEXPRESS;database = samp2024; integrated security = true;");
        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {
            con.Open();
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            // Check if the user exists
            cmd = new SqlCommand("SELECT COUNT(*) FROM Users WHERE username = @username", con);
           
            cmd.Parameters.AddWithValue("@username", username);
            int count = (int)cmd.ExecuteScalar();
            if (count == 1)
            {
                cmd = new SqlCommand("SELECT password FROM Users WHERE username = @username", con);
               
                cmd.Parameters.AddWithValue("@username", username);
                string storedPassword = (string)cmd.ExecuteScalar();
                if (password.Equals(storedPassword))
                {
                    Response.Redirect("homepage.aspx?userName=" + txtUsername.Text);
                }
                else
                {
                    lblError.Text = "Invalid password.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Invalid username.";
                lblError.Visible = true;
            }
            con.Close();
        }

    }
}