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

        public bool Save(IFormFile file, out string name)
        {
            name = "";

            if (file.Length > 0)
            {    
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/users/images", ImageName);
                name = ImageName;

                using (var stream = File.Create(SavePath))
                {
                    file.CopyTo(stream);
                }

                return true;
            }

            return false;
        }
    }
}
