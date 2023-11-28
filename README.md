# aspnet-qrcode
Generating QR Code using ZXing.Net package in order to print the user info along with its QR code in view containing the source (print) page url to validate the print or data from the hard copy in government schemes

## Getting Started

### ZXing.Net Package

ZXing.Net is a popular and reliable library for generating QR codes in ASP.NET using C#. It provides a wide range of features and options for creating QR codes, 
and it is widely used in the development community. Some of its advantages include:
- Open Source: ZXing.Net is open-source software, which means you can use it freely in your projects and modify its source code if needed.
- Cross-Platform: It is compatible with various platforms, including ASP.NET, making it versatile for different development environments.
- Customization: ZXing.Net allows you to customize various aspects of the QR code, such as size, color, error correction level, and more.
- Support for Various Formats: Besides QR codes, ZXing.Net supports other 1D and 2D barcode formats, which can be beneficial if your application requires multiple barcode types.
- Active Community: Since it is widely used, you can find a supportive community and resources, including documentation and tutorials, to help you get started.

Install ZXing.Ne package 
`Install-Package ZXing.Net` in NuGet Package Manager console

Use name-spaces in .cs files
`using ZXing;`
`using ZXing.QrCode;`
`using System.Drawing;`

### Technology Used

The technology used is `ASP.NET C# web Forms Site` (Plain .Net)
Visual Studio version `Visual Studio Community Version 2017`

## Guide

### Base Code Snippet

The base asp.net code snippet is in the page `QRCode.aspx`
Here the basic code is written and explained in which we can create a QR Code png file in our server folder to be shown on view or user profile printing

### Application Code Snippet

the application has been done in `QRCodeGenerator.aspx` where
- first the user data is saved in database along with a GUID `GGenerate Unique Identifier` which is then used in the forward url after the base url to display the data along woth the user's QR Code ith it
- Here the RowCommand method is used to get the particular record's ID when clicked on a download link, to get its ID to fill the user data in next page `Download.aspx`
- during the QR Code generation itself the forward URL is generated eith key-value pair where value is dynamic based on the user's fetched GUID from database
- in Download.aspx, we get the uniquely fetched GUID from RowCommand which is then checked on Page Load and used that GUID to fetch user details from database and fill the data

## QR Code Scanning Output

Once the QR Code is generated in server folder and then displayed to user for that user, then once it get scanned it wil lead to that page from where it was getting displayed as while QR code .png file is generating its data is saved as the base url and the concatinated with the next unique url for that user nad that whole url is saved in database table in `QRCodeUrl` column and after scanned it will take us to that url

## NOTE

- During saving the generated QR Code and fetching the QR Code url from database to display it on browser, the url is first converted from double backward "\\" path to single forward "/" using .Replace Method as the browser only understand the path in single "\" or "/", but the string path is stored with "\\" as the "\" acts as escape character.
- Also while displaying an immage we cannot give the absolute path as the browser wont show the absolute path for secruity purposes eg: `C:\Users\DELL\source\repos\QR Code\WebSite5\QRCodes\Nitin Roman QRCode18-09-2023 5.32.40 PM.png`
- hence used the root folder path which starts from the project folder itself (Absolute path - root path) and use that remaining path (Relative path) to Img tag to display the image eg: `~/QRCodes/Nitin Roman QRCode18-09-2023 5.32.40 PM.png`

## Schema Details
<table>
  <thead>
    <tr>
      <th>Column Name</th>
      <th>Data Type</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>id</td>
      <td>int</td>
    </tr>
    <tr>
      <td>Name</td>
      <td>varchar(50)</td>
    </tr>
    <tr>
      <td>Email</td>
      <td>varchar(50)</td>
    </tr>
    <tr>
      <td>MobileNo</td>
      <td>varchar(15)</td>
    </tr>
    <tr>
      <td>Password</td>
      <td>varchar(50)</td>
    </tr>
    <tr>
      <td>QRCodeUrl</td>
      <td>varchar(200)</td>
    </tr>
    <tr>
      <td>UniqueIdentifier</td>
      <td>uniqueidentifier</td>
    </tr>
  </tbody>
</table>

