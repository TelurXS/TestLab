using System.ComponentModel.DataAnnotations.Schema;

namespace TestLab.Entities
{
    public class Worker
    {
        public int Id { get; set; }
        public string Profession { get; set; }
        public Account Account { get; set; }
    }
}
