using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace TestLab.Utils.Files
{
    public sealed class FileParser
    {
        public FileParser()
        {

        }

        public bool SaveUserImage(IFormFile file, out string name)
        {
            name = "";

            if (file.Length > 0)
            {    
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string localPath = "/users/images/" + imageName;
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + localPath);
                name = localPath;

                using (var stream = File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                return true;
            }

            return false;
        }

        public bool DeleteUserImage(string name) 
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + name);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);

                return true;
            }

            return false;
        }
    }
}
