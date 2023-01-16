using System.Collections.Generic;
using System.IO;
using TestLab.Entities;
using TestLab.Entities.Projects;

namespace TestLab
{
    public static class Config
    {
        public static class DataBase
        {
            public static readonly string DataSource = "KOMPUTER\\SQLEXPRESS";
            public static readonly string InitialCatalog = "testlabdb";
        }

        public static class Pagination
        {
            public static readonly IEnumerable<int> PossibleCountPerPage = new List<int>() { 9, 3, 6, 20, 50 };
        }

        public static class Posts
        {
            public static readonly string DefaultPostImage = "/img/news.png";
        }

        public static class Accounts
        {
            public static readonly string DefaultProfileImage = "/img/profile.jpg";
        }

        public static class Dashboard
        {
            public static readonly IEnumerable<Navigation> Navigations = new List<Navigation>
            {
                new Navigation { Icon = "graph-up-arrow", Title = "Stats", Href = "/dashboard" },
                new Navigation { Icon = "person", Title = "Accounts", Href = "/dashboard/accounts" },
                new Navigation { Icon = "envelope", Title = "Posts", Href = "/dashboard/posts" },
                new Navigation { Icon = "file-code",Title = "Projects", Href = "/dashboard/projects" },
                new Navigation { Icon = "person-workspace",Title = "Workers", Href = "/dashboard/workers" },
                new Navigation { Icon = "link-45deg",Title = "Navigations", Href = "/dashboard/navigations" },
            };

            public static readonly AccountState RequiredPermission = AccountState.Administrator;
        }

        public static class Files
        {
            public static readonly int MaxFileLenght = 1000000000;
            public static readonly string UsersImageDirectory = "/users/images/";
            public static readonly string PostsImageDirectory = "/posts/images/";
            public static readonly string ProjectsResourcesDirectory = "/projects/resources/";
            public static readonly string ProjectsResultsDirectory = "/projects/results/";
        }

        public static class Projects
        {
            public static readonly Dictionary<ProjectType, string> PossibleTypes = new Dictionary<ProjectType, string>()
            {
                { ProjectType.PngToJpg, "(Image Convertation) Png To Jpg" },
                { ProjectType.JpgToPng, "(Image Convertation) Jpg To Png" },
                { ProjectType.TextCapitalize, "(Txt Formatting) Format Text Capitalize Letter" },
                { ProjectType.TextToUpper, "(Txt Formatting) Format Text Upper Case" },
                { ProjectType.TextToLower, "(Txt Formatting) Format Text Lower Case" },
            };
        }

        public static class Editor 
        {
            public static AccountState EnterPermission = AccountState.Redactor;
            public static AccountState EditPermission = AccountState.Administrator;
        }
    }
}
