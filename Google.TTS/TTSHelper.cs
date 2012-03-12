using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using Microsoft.DirectX.AudioVideoPlayback;

namespace Carubbi.Google.Translation
{
    public class TTSHelper : GoogleRestService
    {
        private static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        private const string URL_GOOGLE_TTS = "http://translate.google.com/translate_tts?tl=pt&q=";

    
    

        public static string GenerateFile(string text)
        {
            Uri url = new Uri(string.Concat(URL_GOOGLE_TTS, text));
            Stream fileContent = RequestStream(url);
            string tempPath = Path.ChangeExtension(Path.GetTempFileName(), ".mp3");
            WriteFile(fileContent, tempPath);
            return tempPath;
        }

        private static void WriteFile(Stream fileContent, string tempPath)
        {
            using (Stream file = File.OpenWrite(tempPath))
            {
                CopyStream(fileContent, file);
                file.Flush();
                file.Close();
            }
            fileContent.Close();
            fileContent.Dispose();
        }

        public static void PlayFile(string filePath, bool deleteAfter, bool isAsync)
        {
            Audio player = new Audio(filePath);
            player.Play();

            if (!isAsync)
            {
                while (player.CurrentPosition < player.Duration) ;

                if (deleteAfter)
                    File.Delete(filePath);
            }
        }

        public static string PlayAndSave(string text)
        {
           string tempPath = GenerateFile(text);
           PlayFile(tempPath, false, false);
           return tempPath;
        }

        public static void Play(string text)
        {
            PlayFile(GenerateFile(text), true, false);
        }

        public static void AsyncPlay(string text)
        {
            PlayFile(GenerateFile(text), false, true);
        }
    }
}
