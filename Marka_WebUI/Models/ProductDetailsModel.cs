﻿using Marka_Entity;

namespace Marka_WebUI.Models
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
