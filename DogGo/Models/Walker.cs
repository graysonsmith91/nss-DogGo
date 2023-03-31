﻿using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int NeighborhoodId { get; set; }
        public string ImageUrl { get; set; }
        public string HoursMinutes { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}