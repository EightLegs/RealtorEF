using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtorEF.Entities
{
    [Table("EvaluationCriteria")]
    public class EvaluationCriteria
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}