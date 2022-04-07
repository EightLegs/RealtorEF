using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealtorEF.Entities
{
    [Table("Districts")]
    public class District
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
