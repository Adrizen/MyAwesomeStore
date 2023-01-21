using System.ComponentModel.DataAnnotations;

namespace AdminPresentationLayer.Models
{
    public class Category
    {
        [Key]
        public int idCategory { get; set; }

        [Required(ErrorMessage = "A description of the category is required.")]
        public string info { get; set; }

        public bool active { get; set; } = true;

    }
}




