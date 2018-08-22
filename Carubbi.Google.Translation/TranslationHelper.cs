using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Carubbi.Google.Translation
{
    public class TranslationHelper : GoogleRestService
    {
        private const string URL_GOOGLE_TRANSLATE = "https://www.googleapis.com/language/translate/v2?key=AIzaSyDuoFfuqRd9AelEb0ZS1UsJLWQsy9xzVEA&q={0}&source={1}&target={2}&callback=handleResponse&prettyprint=false";
        private const string AUTODETECT_SOURCE_URL_GOOGLE_TRANSLATE = "https://www.googleapis.com/language/translate/v2?key=AIzaSyDuoFfuqRd9AelEb0ZS1UsJLWQsy9xzVEA&q={0}&target={1}&callback=handleResponse&prettyprint=false";
        
        
        public static string Translate(string text, CultureInfo sourceCulture, CultureInfo targetCulture)
        {
            Uri url;
            if (sourceCulture != null)
                url = new Uri(string.Format(URL_GOOGLE_TRANSLATE, text, sourceCulture.TwoLetterISOLanguageName, targetCulture.TwoLetterISOLanguageName));
            else
                url = new Uri(string.Format(AUTODETECT_SOURCE_URL_GOOGLE_TRANSLATE, text, targetCulture.TwoLetterISOLanguageName));
           
            Stream result = RequestStream(url);
            string json = string.Empty;
            using (StreamReader sr = new StreamReader(result))
            {
                json = sr.ReadToEnd();
            }

           return RetrieveFirstTranslationFromJson(json);

           
        }

        private static string RetrieveFirstTranslationFromJson(string json)
        {
            json = json.Replace("handleResponse(", string.Empty);
            string content = json.Substring(0, json.Length - 2);

            JObject jobject = JObject.Parse(content);
            JsonReader jr = jobject.CreateReader();
            string translateValue = string.Empty;
            while (jr.Read())
            {
                if (jr.TokenType == JsonToken.String)
                {
                    translateValue = jr.Value.ToString();
                    break;
                }
            }
            return translateValue;
        }

       

    }
}
