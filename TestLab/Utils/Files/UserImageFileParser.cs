using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace TestLab.Utils.Files
{
    public class UserImageFileParser : FileParser
    {
        public UserImageFileParser() : 
            base(Config.Files.UsersImageDirectory)
        {
        }

        public override bool Replace(IFormFile file, string oldPath, out string newPath)
        {
            if (ExtensionOf(file.FileName) == ".png" ||
                ExtensionOf(file.FileName) == ".jpg")
            {
                return base.Replace(file, oldPath, out newPath);
            }

            newPath = oldPath;
            return false;
        }
    }
}
