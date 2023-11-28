using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Download : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["uniqueIdentifier"] != null)
            {
                Guid uniqueIdentifier = new Guid(Request.QueryString["uniqueIdentifier"]);
                DisplayUserData(uniqueIdentifier);
            }
        }
    }

    private void DisplayUserData(Guid uniqueIdentifier)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT * FROM UserMaster WHERE UniqueIdentifier = @UniqueIdentifier";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@UniqueIdentifier", uniqueIdentifier);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                TextBox1.Text = dt.Rows[0]["Name"].ToString();
                TextBox2.Text = dt.Rows[0]["Email"].ToString();
                TextBox3.Text = dt.Rows[0]["MobileNo"].ToString();

                string absolutePath = dt.Rows[0][5].ToString();
                string absolutePathNew = absolutePath.Replace("\\", "/");

                string rootPath = Server.MapPath("~").TrimEnd('/');
                string rootPathNew = rootPath.Replace("\\", "/");

                if (absolutePathNew.StartsWith(rootPathNew, StringComparison.OrdinalIgnoreCase))
                {
                    string relativePath = "~/" + absolutePathNew.Substring(rootPathNew.Length);
                    QRCodeImg.ImageUrl = relativePath;
                }
            }
            con.Close();
        }
    }
}