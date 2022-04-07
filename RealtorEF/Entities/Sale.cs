using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealtorEF.Entities
{
    [Table("Sales")]
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public RealEstate RealEstate { get; set; }
        [ForeignKey("RealEstate")]
        public int RealEstateId { get; set; }
        public DateTime Date { get; set; }
        public Realtor Realtor { get; set; }
        [ForeignKey("Realtor")]
        public int RealtorId { get; set; }
        public int Price { get; set; }
    }
}