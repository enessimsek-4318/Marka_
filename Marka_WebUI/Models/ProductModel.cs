using Marka_Entity;
using System.ComponentModel.DataAnnotations;

namespace Marka_WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public List<Image> Images { get; set; }
    }
}
