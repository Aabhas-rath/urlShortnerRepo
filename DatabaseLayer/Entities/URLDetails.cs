using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutURL.Entities
{
    [Table("urlDetails")]
    public class URLDetails
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required]
        [Column("orignalurl")]
        [StringLength(1000)]
        public string OrignalUrl { get; set; }

        [Required]
        [Column("customurl")]
        [StringLength(50)]
        public string CustomUrl { get; set; }

        [Required]
        [Column("createdon")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Column("clicks")]
        public int ClickCounter { get; set; }
        [Required]
        [Column("ip")]
        public string Ip { get; set; }

        public Statistics[] statistics { get; set; }
    }
}
