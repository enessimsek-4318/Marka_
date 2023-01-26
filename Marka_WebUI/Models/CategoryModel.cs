using Marka_Entity;

namespace Marka_WebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
