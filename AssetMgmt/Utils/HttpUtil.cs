using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace AssetMgmt.Utils
{
    public static class HttpUtil
    {
        public static string GetResponse(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Clean up the streams and the response.
                reader.Close();
                response.Close();

                return responseFromServer;
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}