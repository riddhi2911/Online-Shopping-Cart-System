using System.ComponentModel.DataAnnotations;

namespace External.Models
{
    public class Card
    {
        [Key]
        public int carid { get; set; }
        public int Cid { get; set; }
        public int Pid { get; set; }
        public DateOnly Cdate { get; set; }
        public int price { get; set; }  
        public int Total { get; set; }

    }
}
