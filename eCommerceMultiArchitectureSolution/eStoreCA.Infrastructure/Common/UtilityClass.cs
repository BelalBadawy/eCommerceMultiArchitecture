
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace eStoreCA.Infrastructure.Common
{
    public static class UtilityClass
    {

        public static bool ValidateEmail(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        public static string UploadedFile(IWebHostEnvironment webHostEnvironment, IFormFile model)
        {
            string uniqueFileName = null;

            if (model != null)
            {
                var extension = Path.GetExtension(model.FileName);

                string uploadsFolder = webHostEnvironment.WebRootPath + "/uploadedfiles/";
                uniqueFileName = Guid.NewGuid().ToString() + extension;
                string filePath = uploadsFolder + uniqueFileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public static void WriteToFile(string text)
        {
            string directoryName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string logPath = string.Format(directoryName + "\\Logs\\DSOLog{0}.txt",
                DateTime.Now.Date.ToString("yyyy-MM-dd"));

            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                writer.WriteLine(DateTime.Now.ToString());
                writer.WriteLine(text);
                writer.Close();
            }
        }

    }
}
