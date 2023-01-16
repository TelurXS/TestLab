using Microsoft.AspNetCore.Http;
using NuGet.DependencyResolver;
using System;
using System.IO;

namespace TestLab.Utils.Files
{
    public abstract class FileParser : IFileParser
    {
        protected const string ROOT = "wwwroot";

        public FileParser(string localDirectory)
        {
            LocalDirectory = localDirectory;
        }

        public string LocalDirectory { get; set; }

        public virtual string ExtensionOf(string file)
        {
            return Path.GetExtension(file).ToLower();
        }

        public virtual string FullPathOf(string localPath)
        {
            return Path.Combine(GetDirectory(), ROOT + localPath);
        }

        public virtual string GetDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public virtual string GetUniqueFileName()
        {
            return Guid.NewGuid().ToString();
        }

        public virtual bool Save(IFormFile file, out string path) 
        {
            if (file.Length > 0 && file.Length <= Config.Files.MaxFileLenght)
            {
                string fileName = GetUniqueFileName() + ExtensionOf(file.FileName);
                string localPath = LocalDirectory + fileName;
                string fullPath = FullPathOf(localPath);
                path = localPath;

                using (var stream = File.Create(fullPath))
                {
                    file.CopyTo(stream);
                }

                return true;
            }

            path = string.Empty;
            return false;
        }

        public bool Save(string content, string extension, out string path)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);

            if (bytes.Length > 0 && bytes.Length <= Config.Files.MaxFileLenght)
            {
                string fileName = GetUniqueFileName() + "." + extension;
                string localPath = LocalDirectory + fileName;
                string fullPath = FullPathOf(localPath);
                path = localPath;

                using (var stream = File.Create(fullPath))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                return true;
            }

            path = string.Empty;
            return false;
        }

        public virtual bool Delete(string localPath) 
        {
            string fullPath = FullPathOf(localPath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }

            return false;
        }

        public virtual bool DeleteOrIgnore(string localPath, string ignore)
        {
            if (localPath != ignore)
                return Delete(localPath);

            return false;
        }

        public virtual bool Replace(IFormFile file, string oldPath, out string newPath) 
        {
            Delete(oldPath);
            return Save(file, out newPath);
        }

        public virtual bool ReplaceOrIgnore(IFormFile file, string oldPath, out string newPath, string ignore) 
        {
            if (oldPath != ignore)
                return Replace(file, oldPath, out newPath);

            newPath = string.Empty;
            return false;
        }

        public virtual string Read(string localPath)
        {
            return File.ReadAllText(FullPathOf(localPath));
        }

        public virtual string GetFullLocalPath()
        {
            return Path.Combine(GetDirectory(), ROOT + LocalDirectory);
        }
    }
}
