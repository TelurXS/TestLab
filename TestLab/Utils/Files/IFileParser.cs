using Microsoft.AspNetCore.Http;

namespace TestLab.Utils.Files
{
    public interface IFileParser
    {
        public bool Save(IFormFile file, out string path);
        public bool Save(string content, string extension, out string path);

        public string Read(string localPath);

        public bool Delete(string localPath);
        public bool DeleteOrIgnore(string localPath, string ignore);

        public bool Replace(IFormFile file, string oldPath, out string newPath);
        public bool ReplaceOrIgnore(IFormFile file, string oldPath, out string newPath, string ignore);

        public string FullPathOf(string localPath);

        public string GetUniqueFileName();

        public string GetDirectory();
        public string GetFullLocalPath();

        public string ExtensionOf(string path);
    }
}
