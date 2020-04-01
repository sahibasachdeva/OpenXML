using Assignment_4.Model;
using Assignment_4.Utilities;


namespace Assignment_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Word.Data(Constants.Locations.BaseUrl, Constants.Locations.DocLoc);
            Excel.CreateSpreadsheetWorkbook( Constants.Locations.ExcelLoc);
            Excel.InsertText(Constants.Locations.ExcelLoc);
            Present.CreatePresentation(Constants.Locations.PptLoc);

            FTP.UploadFile(Constants.Locations.DocLoc, Constants.FTP.BaseUrl + Constants.FTP.remoteUploadFileDestination + "docx", Constants.FTP.UserName, Constants.FTP.Password);
            FTP.UploadFile(Constants.Locations.ExcelLoc, Constants.FTP.BaseUrl + Constants.FTP.remoteUploadFileDestination + "xlsx", Constants.FTP.UserName, Constants.FTP.Password);
            FTP.UploadFile(Constants.Locations.PptLoc, Constants.FTP.BaseUrl + Constants.FTP.remoteUploadFileDestination + "pptx", Constants.FTP.UserName, Constants.FTP.Password);


        }
    }
}
