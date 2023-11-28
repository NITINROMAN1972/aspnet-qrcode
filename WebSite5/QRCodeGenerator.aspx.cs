using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data;

public partial class QRCodeGenerator : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
    int k = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["QRCodeUrl"] = "";
        BindGrid();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Generate a unique identifier (GUID) for the user
        Guid uniqueIdentifier = Guid.NewGuid();

        int newUserId;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "INSERT INTO UserMaster (Name, Email, MobileNo, Password, QRCodeUrl, UniqueIdentifier) VALUES (@Name, @Email, @MobileNo, @Password, @QRCodeUrl, @UniqueIdentifier); SELECT SCOPE_IDENTITY();";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", TextBox1.Text.ToString()); 
            cmd.Parameters.AddWithValue("@Email", TextBox2.Text.ToString()); 
            cmd.Parameters.AddWithValue("@MobileNo", TextBox3.Text.ToString()); 
            cmd.Parameters.AddWithValue("@Password", TextBox4.Text.ToString());
            cmd.Parameters.AddWithValue("@QRCodeUrl", "");
            cmd.Parameters.AddWithValue("@UniqueIdentifier", uniqueIdentifier);
            newUserId = Convert.ToInt32(cmd.ExecuteScalar()); //to get the newly inserted user ID instantly
            con.Close();

            //Generating th edynamic url used after the base url
            string profileUrl = $"/Download?uniqueIdentifier={uniqueIdentifier}";
            Session["QRCodeUrl"] = profileUrl.ToString();
        }

        GenerateQRCode();
        UpdateUserQRUrl(newUserId);
        Response.Redirect("QRCodeGenerator.aspx"); 
    }

    protected void UpdateUserQRUrl(int newUserId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "update UserMaster set QRCodeUrl = @QRCodeUrl where id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@QRCodeUrl", Session["QRCodeDBPath"].ToString());
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(newUserId));
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    protected void GenerateQRCode()
    {
        try
        {
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            var encodingOptions = new QrCodeEncodingOptions
            {
                Width = 400,
                Height = 400,
                Margin = 1
            };
            barcodeWriter.Options = encodingOptions;

            string folderPath = Server.MapPath("~/QRCodes/");

            string folderPathNew = folderPath.Replace("\\", "/");

            string filename = TextBox1.Text.ToString() + " QRCode" + DateTime.Now.ToString() + ".png";
            string QRCodeFulPath = folderPathNew + filename;

            if (!Directory.Exists(folderPathNew))
            {
                Directory.CreateDirectory(folderPathNew);
            }

            string dataToEncode = "http://localhost:51563" + Session["QRCodeUrl"].ToString();
            Bitmap qrCodeBitmap = barcodeWriter.Write(dataToEncode);
            
            qrCodeBitmap.Save(QRCodeFulPath);

            Session["QRCodeUrl"] = "";
            Session["QRCodeDBPath"] = QRCodeFulPath;
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void GrdUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lnkDownload")
        {
            if (e.CommandName == "lnkDownload")
            {
                int editID = Convert.ToInt32(e.CommandArgument);
                Guid uniqueIdentifier = GetUniqueIdentifierFromDatabase(editID);

                if (uniqueIdentifier != Guid.Empty)
                {
                    Session["PrintUserId"] = editID;
                    string profileUrl = $"~/Download/?uniqueIdentifier={HttpUtility.UrlEncode(uniqueIdentifier.ToString())}";
                    Response.Redirect(profileUrl);
                }
            }
        }
    }

    private Guid GetUniqueIdentifierFromDatabase(int editID)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "SELECT UniqueIdentifier FROM UserMaster WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", editID);

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            return (Guid)dt.Rows[0][0];
        }
        
    }

    protected void BindGrid()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = "select id, Name, Email, MobileNo, UniqueIdentifier from UserMaster";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Close();

            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            GrdUser.DataSource = dt;
            GrdUser.DataBind();
        }
    }
}