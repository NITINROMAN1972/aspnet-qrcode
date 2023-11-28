using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.IO;

public partial class QRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateQRCode();
    }

    protected void GenerateQRCode()
    {
        //Install the ZXing.Net Package and copy the 3 name-spaces for the same
        //Install-Package ZXing.Net

        try
        {
            // Create a QR code writer instance
            BarcodeWriter barcodeWriter = new BarcodeWriter();

            // Set the barcode format to QR code - as ww have multiple QR & 1D or 2D barcode types
            barcodeWriter.Format = BarcodeFormat.QR_CODE;

            // Set encoding options (you can customize these as needed)
            var encodingOptions = new QrCodeEncodingOptions
            {
                Width = 400,
                Height = 400,
                Margin = 1
            };

            barcodeWriter.Options = encodingOptions;

            // Setting the virtual server folder to save QR Codes and getting the new path with single "/" in order to view them later
            string folderPath = Server.MapPath("~/QRCodes/");
            string folderPathNew = folderPath.Replace("\\", "/"); //converting double \\ to single /as browser will not read the file for security purposes

            //checking the Virtual folder existance or to create it if it dont exits
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // setting the file or QRCode .png image name
            string filename = "File Name QRCode" + DateTime.Now.ToString() + ".png";

            // The data you want to encode in the QR code
            //string dataToEncode = Session["QRCodeUrl"].ToString();
            //string dataToEncode = $"{TextBox1.Text},{TextBox2.Text},{TextBox3.Text}"; // for getting text data into QRcode string
            string dataToEncode = "sdsd";

            // Generate a QR code image as a Bitmap
            Bitmap qrCodeBitmap = barcodeWriter.Write(dataToEncode);

            //saving the file (not downloading) into the set server folder with some file name to it
            qrCodeBitmap.Save(folderPath + filename);

            Session["QRCodeUrl"] = folderPathNew + filename;

            // Save the QR code image or display it as needed
            // qrCodeBitmap.Save("path_to_save.png"); // Save to a file
            // qrCodeImageControl.Image = qrCodeBitmap; // Display in an image control

            // (Optional) Provide a download link to the user
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", $"attachment; filename={filename}");
            Response.TransmitFile(folderPathNew + filename);
            Response.End();
        }
        catch (Exception ex)
        {

        }
    }
}