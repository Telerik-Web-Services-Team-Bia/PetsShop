namespace GoogleDrive.Api
{
    using System;
    using System.Collections.Generic;
    using Google.Apis.Drive.v2;
    using Google.Apis.Drive.v2.Data;
    using System.Security.Authentication;
    using Validation;

    public class DaimtoGoogleDriveHelper
    {
        private const string ErrorMessage = "An error occurred: ";
        /// <summary>
        /// Download a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/get
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>
        /// <param name="fileResource">File resource of the file to download</param>
        /// <param name="saveTo">location of where to save the file including the file name to save it as.</param>
        /// <returns></returns>
        public static bool DownloadFile(DriveService service, File fileResource, string saveTo)
        {
            var downloadUrl = fileResource.DownloadUrl;
            if (!string.IsNullOrEmpty(downloadUrl))
            {
                try
                {
                    var x = service.HttpClient.GetByteArrayAsync(downloadUrl);
                    byte[] arrBytes = x.Result;
                    System.IO.File.WriteAllBytes(saveTo, arrBytes);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(ErrorMessage + e.Message);
                    return false;
                }
            }
            else
            {
                // The file doesn't have any content stored on Drive.
                return false;
            }
        }

        /// <summary>
        /// Download a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/get
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>
        /// <param name="id">Id of the file to download</param>
        /// <param name="saveTo">location of where to save the file including the file name to save it as.</param>
        /// <returns></returns>
        public static bool DownloadFileById(DriveService service, string id, string saveTo)
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(saveTo))
            {
                File fileResource = service.Files.Get(id).Execute();
                return DownloadFile(service, fileResource, saveTo);
            }
            else
            {
                // The file doesn't have any content stored on Drive.
                return false;
            }
        }

        /// <summary>
        /// Uploads a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/insert
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>
        /// <param name="uploadFile">path to the file to upload</param>
        /// <param name="parent">Collection of parent folders which contain this file. 
        ///                       Setting this field will put the file in all of the provided folders. root folder.</param>
        /// <returns>If upload succeeded returns the File resource of the uploaded file 
        ///          If the upload fails returns null</returns>
        public static File UploadFile(DriveService service, string uploadFile, string parent)
        {
            if (Validator.IsFileExisting(uploadFile))
            {
                File body = new File();
                body.Title = System.IO.Path.GetFileName(uploadFile);
                body.Description = "File uploaded by Pet Store api";
                body.MimeType = GetMimeType(uploadFile);
                body.Parents = new List<ParentReference>() { new ParentReference() { Id = parent } };

                // File's content.
                byte[] byteArray = System.IO.File.ReadAllBytes(uploadFile);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                try
                {
                    FilesResource.InsertMediaUpload request = service.Files.Insert(body, stream, GetMimeType(uploadFile));
                    //request.Convert = true;   // uncomment this line if you want files to be converted to Drive format
                    request.Upload();
                    return request.ResponseBody;
                }
                catch (Exception e)
                {
                    Console.WriteLine(ErrorMessage + e.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Updates a file
        /// Documentation: https://developers.google.com/drive/v2/reference/files/update
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>
        /// <param name="uploadFile">path to the file to upload</param>
        /// <param name="parent">Collection of parent folders which contain this file. 
        ///                       Setting this field will put the file in all of the provided folders. root folder.</param>
        /// <param name="fileId">the resource id for the file we would like to update</param>                      
        /// <returns>If upload succeeded returns the File resource of the uploaded file 
        ///          If the upload fails returns null</returns>
        public static File UpdateFile(DriveService service, string uploadFile, string parent, string fileId)
        {
            if (Validator.IsFileExisting(uploadFile))
            {
                File body = new File();
                body.Title = System.IO.Path.GetFileName(uploadFile);
                body.Description = "File updated by Pet Store api";
                body.MimeType = GetMimeType(uploadFile);
                body.Parents = new List<ParentReference>() { new ParentReference() { Id = parent } };

                // File's content.
                byte[] byteArray = System.IO.File.ReadAllBytes(uploadFile);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                try
                {
                    FilesResource.UpdateMediaUpload request = service.Files.Update(body, fileId, stream, GetMimeType(uploadFile));
                    request.Upload();
                    return request.ResponseBody;
                }
                catch (Exception e)
                {
                    Console.WriteLine(ErrorMessage + e.Message);
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Create a new Directory.
        /// Documentation: https://developers.google.com/drive/v2/reference/files/insert
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>
        /// <param name="title">The title of the file. Used to identify file or folder name.</param>
        /// <param name="description">A short description of the file.</param>
        /// <param name="parent">Collection of parent folders which contain this file. 
        ///                       Setting this field will put the file in all of the provided folders. root folder.</param>
        /// <returns></returns>
        public static File CreateDirectory(DriveService service, string title, string description, string parent)
        {
            File NewDirectory = null;

            // Create metaData for a new Directory
            File body = new File();
            body.Title = title;
            body.Description = description;
            body.MimeType = "application/vnd.google-apps.folder";
            body.Parents = new List<ParentReference>() { new ParentReference() { Id = parent } };
            try
            {
                FilesResource.InsertRequest request = service.Files.Insert(body);
                NewDirectory = request.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(ErrorMessage + e.Message);
            }

            return NewDirectory;
        }


        /// <summary>
        /// List all of the files and directories for the current user. 
        /// Documentation: https://developers.google.com/drive/v2/reference/files/list
        /// Documentation Search: https://developers.google.com/drive/web/search-parameters
        /// </summary>
        /// <param name="service">a Valid authenticated DriveService</param>        
        /// <param name="search">if Search is null will return all files</param>        
        /// <returns></returns>
        public static IList<File> GetFiles(DriveService service, string search)
        {
            IList<File> files = new List<File>();

            try
            {
                //List all of the files and directories for the current user.  
                // Documentation: https://developers.google.com/drive/v2/reference/files/list
                FilesResource.ListRequest list = service.Files.List();
                list.MaxResults = 1000;
                if (search != null)
                {
                    list.Q = search;
                }
                FileList filesFeed = list.Execute();

                //// Loop through until we arrive at an empty page
                while (filesFeed.Items != null)
                {
                    // Adding each item  to the list.
                    foreach (File item in filesFeed.Items)
                    {
                        files.Add(item);
                    }

                    // We will know we are on the last page when the next page token is
                    // null.
                    // If this is the case, break.
                    if (filesFeed.NextPageToken == null)
                    {
                        break;
                    }

                    // Prepare the next page of results
                    list.PageToken = filesFeed.NextPageToken;

                    // Execute and process the next page request
                    filesFeed = list.Execute();
                }
            }
            catch (Exception ex)
            {
                // In the event there is an error with the request.
                Console.WriteLine(ex.Message);
            }

            return files;
        }

        /// <summary>
        /// Search for file by given ID. 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="id"></param>
        /// <returns>If the file is found return it
        ///          if is not found return null.</returns>
        public static File GetFileById(DriveService service, string id)
        {
            Validator.IsStringNullOrEmpty(id, "File id");
            Validator.ValidHttpClientInitializer(service);
            return service.Files.Get(id).Execute();
        }

        public static string DeleteFileById(DriveService service, string id)
        {
            Validator.IsStringNullOrEmpty(id, "File id");
            Validator.ValidHttpClientInitializer(service);

            string request = service.Files.Delete(id).Execute();
            return request;
        }

        public static string GetDownloadUrlByFileId(DriveService service, string id)
        {
            Validator.IsStringNullOrEmpty(id);
            Validator.ValidHttpClientInitializer(service);

            string link = service.Files.Get(id).Execute().WebContentLink;
            return link;
        }

        private static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(extension);

            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }

            return mimeType;
        }
    }
}