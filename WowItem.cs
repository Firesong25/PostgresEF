using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PostgresEF
{
    public partial class WowItem
    {

        public int ItemId { get; set; }        
        public string Name { get; set; }
        public string? BonusList { get; set; }
        public int? PetBreedId { get; set; }
        public int? PetLevel { get; set; }
        public int? PetQualityId { get; set; }
        public int? PetSpeciesId { get; set; }
        [Key]
        public Guid? Id { get; set; }

    }
}

