using Marka_BLL.Abstract;
using Marka_WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marka_WebUI.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoryListViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new CategoryListViewModel()
            {
                SelectedCategory = RouteData.Values["category"]?.ToString(),
                Categories=_categoryService.GetAll()
            });
        }
    }
}
