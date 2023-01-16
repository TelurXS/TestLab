﻿using System;
using TestLab.Entities.Projects.Executors;
using TestLab.Utils.Files;

namespace TestLab.Entities.Projects
{
    public enum ProjectType
    {
        PngToJpg,
        JpgToPng,
        TextCapitalize,
        TextToUpper,
        TextToLower,
    }

    public enum ProjectState 
    {
        Deleted,
        Created,
        Error,
        Completed,
    }

    public enum ProjectAccessability 
    {
        Private,
        Public,
    }

    public enum ProjectResultType 
    {
        Message,
        Image,
        File,
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreationDate { get; set; }

        public string Resource { get; set; }
        public string Result { get; set; }

        public ProjectType Type { get; set; }
        public ProjectState State { get; set; }
        public ProjectAccessability Accessability { get; set; }
        public ProjectResultType ResultType { get; set; }

        public static Project Create(string name, int authorId, string resource, ProjectType type, ProjectAccessability accessability) 
        {
            return new Project
            {
                Name = name,
                AuthorId = authorId,
                CreationDate = DateTime.Now,
                Resource = resource,
                Result = "",
                Type = type,
                State = ProjectState.Created,
                Accessability = accessability,
                ResultType = ProjectResultType.Message,
            };
        }

        public IProjectExecutor GetExecutor() 
        {
            switch (Type)
            {
                case ProjectType.PngToJpg: return new PngToJpgExecutor(this);
                case ProjectType.JpgToPng: return new JpgToPngExecutor(this);
                case ProjectType.TextCapitalize: return new TextCapitalizeFormatExecutor(this);
                case ProjectType.TextToUpper: return new TextToUpperFormatExecutor(this);
                case ProjectType.TextToLower: return new TextToLowerFormatExecutor(this);

                default: throw new Exception("No executor for this ProjectType");
            }
        }

        public void DeleteFiles() 
        {
            IFileParser resourceParser = new ProjectResourceFileParser();
            IFileParser resultParser = new ProjectResultFileParser();

            resourceParser.Delete(Resource);
            resultParser.DeleteOrIgnore(Result, string.Empty);
        }
    }
}
