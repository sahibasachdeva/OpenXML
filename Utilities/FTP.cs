using Assignment_4.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Assignment_4.Utilities
{
    public class FTP
    {
        public static void uploadFile(string FileName, byte[] fileContents)
        {
            // Connect to FTP to upload the file and configure request, Also add the file you want to update
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Constants.FTP.BaseUrl + "/" + FileName);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(Constants.FTP.UserName, Constants.FTP.Password);

            request.ContentLength = fileContents.Length;
            //Upload the data via FTP
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"FTP Transaction for {FileName} Complete, status {response.StatusDescription}");
            }
        }
    }
}
