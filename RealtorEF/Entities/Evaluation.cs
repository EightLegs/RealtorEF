using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtorEF.Entities
{
    [Table("Evaluations")]
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }
        public RealEstate RealEstate { get; set; }
        [ForeignKey("RealEstate")]
        public int RealEstateId { get; set; }
        public DateTime Date { get; set; }
        public EvaluationCriteria EvaluationCriteria { get; set; }
        [ForeignKey("EvaluationCriteria")]
        public int EvaluationCriteriaId { get; set; }
        public int Value { get; set; }
    }
}