﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Rajmohol.Models.DTOs.Villa;

namespace Rajmohol.Models.DTOs.VillaNumber
{
    public class VillaNumberDTO
    {

        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string? SpecialDetails { get; set; }
        public VillaDTO Villa { get; set; }

    }
}
