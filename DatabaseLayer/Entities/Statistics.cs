using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutURL.Entities
{
    [Table("statistics")]
    public class Statistics
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("clickdate")]
        public DateTime ClickDate { get; set; }

        public URLDetails ShortUrl { get; set; }

    }
}
