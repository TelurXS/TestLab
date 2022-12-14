using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using TestLab.Entities;

namespace TestLab.Utils.Files
{
    public sealed class FileParser
    {
        public FileParser()
        {

        }

        public bool Save(IFormFile file, out string name, string midleDirectory)
        {
            name = "";

            if (file.Length > 0 && file.Length <= Config.Files.MaxFileLenght)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string localPath = midleDirectory + imageName;
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

        public bool Delete(string localFileName) 
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + localFileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);

                return true;
            }

            return false;
        }

        public bool DeleteOrIgnore(string localFileName, string ignoreName) 
        {
            if (localFileName != ignoreName)
                return Delete(localFileName);

            return false;
        }

        public bool Replace(IFormFile file, string previousName, out string newName, string ignoreName, string middleDirectory) 
        {
            if (previousName != ignoreName)
                Delete(previousName);

            return Save(file, out newName, middleDirectory);
        }

        public bool SaveUserImage(IFormFile file, out string name) => Save(file, out name, Config.Files.UsersImageDirectory);
        public bool SaveProjectResource(IFormFile file, out string name) => Save(file, out name, Config.Files.ProjectsResourcesDirectory);
        public bool SaveProjectResult(IFormFile file, out string name) => Save(file, out name, Config.Files.ProjectsResultsDirectory);

        public bool ReplaceUserImage(IFormFile file, string previousName, out string newName) =>
            Replace(file, previousName, out newName, Config.Accounts.DefaultProfileImage, Config.Files.UsersImageDirectory);
        public bool ReplacePostImage(IFormFile file, string previousName, out string newName) =>
            Replace(file, previousName, out newName, Config.Posts.DefaultPostImage, Config.Files.PostsImageDirectory);

        public string FullPathOf(string localFileName) 
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + localFileName);
        }

        public string UniqueFileName() 
        {
            return Guid.NewGuid().ToString();
        }

        public string GetDirectory() 
        {
            return Directory.GetCurrentDirectory();
        }

        public string ExtensionOf(string file) 
        {
            return Path.GetExtension(file).ToLower();
        }
    }
}
