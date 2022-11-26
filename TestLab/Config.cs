using System.Collections.Generic;
using System.IO;
using TestLab.Entities;

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

        public static class Account
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
            };

            public static readonly AccountState RequiredPermission = AccountState.Administrator;
        }

        public static class Files
        {
            public static readonly List<string> SupportedImageContentTypes = new List<string>
            {
                "image/jpeg",
            };
        }
    }
}
