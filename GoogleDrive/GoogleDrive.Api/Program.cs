namespace GoogleDrive.Api
{
    using System;
    using Google.Apis.Drive.v2;
    using Google.Apis.Drive.v2.Data;

    public class Program
    {
        // Connect with Oauth2 Ask user for permission
        private const string ClienId = "371194979077-harcvjnc1d25clngrmvep3fcm9t4vv04.apps.googleusercontent.com";
        private const string ClientSecret = "kYBlBwST6u4XXLjVWQ3vLA3-";
        private const string RootDirectoryId = "0Bye2LwjDvhQGc2FiLU13blRUcms";

        public static void Main()
        {            
            DriveService service = Authentication.AuthenticateOauth(ClienId, ClientSecret, "petstore");

            if (service == null)
            {
                Console.WriteLine("Authentication error");
                Console.ReadLine();
            }

            try
            {
                // Test uploading file
                File currentFile = DaimtoGoogleDriveHelper.UploadFile(service, "some-cat.jpg", RootDirectoryId);

                // Find file
                var founded = DaimtoGoogleDriveHelper.GetFiles(service, "some-cat.jpg");
                
            }
            catch (Exception ex)
            {
                int i = 1;
            }

            Console.ReadLine();
        }
    }
}