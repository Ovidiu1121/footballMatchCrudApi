using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballMatchCrudApi.Matches.Model
{
    [Table("footballmatch")]
    public class FootballMatch
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("stadium")]
        public string Stadium { get; set; }

        [Required]
        [Column("score")]
        public string Score { get; set; }

        [Required]
        [Column("country")]
        public string Country { get; set; }

    }
}
