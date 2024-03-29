﻿using Marka_Entity;

namespace Marka_WebUI.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }     
        public int ItemsPerPage { get; set; }  
        public int CurrentPage { get; set; }    
        public string CurrentCategory { get; set; } 

        public int TotalPages()
        {
            return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); 
        }
    }
    public class ProductListModel
    {
        public List<Product> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
