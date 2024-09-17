﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CategoryCRUD.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Order")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
