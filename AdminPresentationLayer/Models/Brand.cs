using System.ComponentModel.DataAnnotations;

namespace AdminPresentationLayer.Models
{
    public class Brand
    {
        [Key]
        public int idBrand { get; set; }

        public string info { get; set; }

        public bool active { get; set; }
    }
}
