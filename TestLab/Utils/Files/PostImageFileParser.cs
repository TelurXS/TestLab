using Microsoft.AspNetCore.Http;

namespace TestLab.Utils.Files
{
    public class PostImageFileParser : FileParser
    {
        public PostImageFileParser()
            : base(Config.Files.PostsImageDirectory)
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
