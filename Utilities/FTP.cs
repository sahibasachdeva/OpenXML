using Assignment_4.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Assignment_4.Utilities
{
    public class FTP
    {
        public static string UploadFile(string sourceFilePath, string destinationFileUrl, string username = Constants.FTP.UserName, string password = Constants.FTP.Password)
        {
            string output;

            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(destinationFileUrl);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(username, password);

            // Copy the contents of the file to the request stream.
            byte[] fileContents = File.ReadAllBytes(sourceFilePath);

            //Get the length or size of the file
            request.ContentLength = fileContents.Length;

            //Write the file to the stream on the server
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            //Send the request
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                output = $"Upload File Complete, status {response.StatusDescription}";
            }
            Thread.Sleep(Constants.FTP.OperationPauseTime);

            return (output);
        }


        public static (List<Students>, string) SetInfoData()
        {
            string error = null;
            string path = Constants.Locations.StudentDataFolder;
            string[] getFileList = Directory.GetFiles(path);
            List<Students> studentlist = new List<Students>();
            foreach (string files in getFileList)
            {
                Students student = new Students();
                List<String> entries = new List<string>();
                entries = readCSV(files);
                try
                {
                    student.FromCSV(entries[1]);
                    studentlist.Add(student);
                }
                catch
                {
                    error = error + Path.GetFileName(files) + "\n";
                }
            }
            return (studentlist, error);
        }

        public static List<String> readCSV(String FilePath)
        {
            String fileContents;
            using (StreamReader stream = new StreamReader(FilePath))
            {
                fileContents = stream.ReadToEnd();
            }
            List<String> entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return entries;
        }
    }
}
