using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please add a name...")]
        [MaxLength(35)]
        public string Name { get; set; }

        [Required]
        [StringLength(55, MinimumLength = 5)]
        public string Address { get; set; }

        [Display(Name = "Neighborhood")]
        [Required]
        public int NeighborhoodId { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
