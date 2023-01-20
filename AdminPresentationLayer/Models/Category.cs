using System.ComponentModel.DataAnnotations;

namespace AdminPresentationLayer.Models
{
    public class Category
    {
        [Key]
        public int idCategory { get; set; }

        public string info { get; set; }

        public bool active { get; set; }

    }
}




