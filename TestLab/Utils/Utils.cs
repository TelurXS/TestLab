using System;
using System.Text.RegularExpressions;
using TestLab.Entities.Projects;

namespace TestLab.Utils
{
    public static class Utils
    {
        public static string ToHtmlDate(this DateTime dateTime) 
        {
            string year = dateTime.Year.ToString();
            string month = dateTime.Month <= 9 ? "0" + dateTime.Month.ToString() : dateTime.Month.ToString();
            string day = dateTime.Day <= 9 ? "0" + dateTime.Day.ToString() : dateTime.Day.ToString();

            return $"{year}-{month}-{day}";
        }
        
        public static string SplitWithSpaces(this string value) 
        {
            return Regex.Replace(
                Regex.Replace(value, 
                              "(\\P{Ll})(\\P{Ll})(\\p{Ll})",
                              "$1 $2"),
                "(\\p{Ll})(\\P{Ll})", 
                "$1 $2");
        }
    }
}
