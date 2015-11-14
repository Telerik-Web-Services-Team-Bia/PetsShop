namespace GoogleDrive.Validation
{
    using System;
    using Google.Apis.Drive.v2;
    using System.Security.Authentication;

    public static class Validator
    {
        public static void IsStringNullOrEmpty(string value, string message = "Value")
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("{0} cannot be null or empty!", message);
            }
        }

        public static bool IsFileExisting(string filePath, string message = "File")
        {
            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine("An Error occurred - {0} does not exist", message);
                return false;
            }

            return true;
        }

        public static void ValidHttpClientInitializer(DriveService service)
        {
            if (service.HttpClientInitializer == null)
            {
                throw new AuthenticationException("Authentication error! Please use the Authentication class to initialize the Google Drive service!");
            }
        }
    }
}
