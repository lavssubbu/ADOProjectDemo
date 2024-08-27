using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication6
{
    public partial class SignUp : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("data source=LAPTOP-BQF0DTHQ\\SQLEXPRESS;database = samp2024; integrated security = true;");
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            con.Open();
            string uname = txtUsername.Text;
            string pwd= txtPassword.Text;
            string email = txtEmail.Text;

            cmd = new SqlCommand("insert into Users(Username,Email,Password) values(@uname,@email,@pwd)", con);
            cmd.Parameters.AddWithValue("@uname", uname);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            cmd.Parameters.AddWithValue("@email",email);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Registered successfully");
            Response.Redirect("SignIn.aspx");
        }
    }
}