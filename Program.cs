using Assignment_4.Model;
using Assignment_4.Utilities;


namespace Assignment_4
{
    class Program
    {
        static void Main(string[] args)
        {
             Word.Data("https://jsonplaceholder.typicode.com/users", @"D:\BDAT\InformationEncoding\tr.docx");
            Excel.CreateSpreadsheetWorkbook("https://jsonplaceholder.typicode.com/users", @"D:\BDAT\InformationEncoding\tr.xlsx");
            Present.CreatePresentation(@"D:\BDAT\InformationEncoding\tr.pptx");

            Utilities.FTP.uploadFile(, Constants.FTP.BaseUrl + Constants.FTP.remoteUploadFileDestination + "xml", Constants.FTP.Username, Constants.FTP.Password);

        }
    }
}
