using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPresentationLayer.Models
{
    public class Product
    {
        [Key]
        public int idProduct { get; set; }

        [Required(ErrorMessage = "A name is required for this product.")]
        public string nameProduct { get; set; }

        [Required(ErrorMessage = "A description is required for this product.")]
        public string info { get; set; }

        // Foreign key.
        public int idBrand { get; set; }

        [ForeignKey("idBrand")]
        public virtual Brand Brand { get; set; }

        // Foreign key.
        public int idCategory { get; set; }

        [ForeignKey("idCategory")]
        public virtual Category Category { get; set; }


        [Required(ErrorMessage = "A price is required for this product.")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be positive")]
        public int price { get; set; }

        public int stockQuantity { get; set; }

        public string imageURL { get; set; }

        public string imageName { get; set; }

        public bool active { get; set; } = true;

        public DateTime registerDate { get; set; } = DateTime.Now;

    }
}
