using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPresentationLayer.Models.ViewModels
{
    public class ProductVM
    {
        public Product product { get; set; }

        public IEnumerable<SelectListItem>? categoryList { get; set; }

        public IEnumerable<SelectListItem>? brandList { get; set; }
    }
}
