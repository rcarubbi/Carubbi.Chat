using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Carubbi.Google.Translation
{
    public class GoogleRestService
    {
        private const string MOZILLA = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";

        protected static Stream RequestStream(Uri url)
        {
            HttpWebRequest request;
            request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = MOZILLA;
            request.UseDefaultCredentials = true;
            WebResponse response = request.GetResponse();
            Stream fileContent = response.GetResponseStream();
            return fileContent;
        }
    }
}
