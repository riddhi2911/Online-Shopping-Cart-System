using System.ComponentModel.DataAnnotations;

namespace External.Models
{
    public class Customer
    {
        [Key]
        public int Cid { get; set; }
        public string Cname { get; set; }
        public string Cemail { get; set; }
        public string CPassword { get; set; }
        public string Cadd { get; set; }
        public string Cgender { get; set; }
    }
}
