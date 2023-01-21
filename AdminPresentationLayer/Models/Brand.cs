using System.ComponentModel.DataAnnotations;

namespace AdminPresentationLayer.Models
{
    public class Brand
    {
        [Key]
        public int idBrand { get; set; }

        [Required(ErrorMessage = "A description of the brand is required.")]
        public string info { get; set; }

        public bool active { get; set; }
    }
}
