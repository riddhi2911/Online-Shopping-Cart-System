using System.ComponentModel.DataAnnotations;

namespace External.Models
{
    public class Prod
    {
        [Key]
        public int Pid { get; set; }
        public string Pname { get; set; }
        public int Pprice { get; set; }
    }
}
