﻿using System.ComponentModel.DataAnnotations;

namespace Micro.PlaformService.Models
{
    public class Platform
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Publisher { get; set; }
        public string? Cost { get; set; }
    }
}
