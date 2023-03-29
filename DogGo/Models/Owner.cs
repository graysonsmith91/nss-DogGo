﻿using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [Display(Name = "Neighborhood")]
        public int NeighborhoodId { get; set; }
        public string Phone { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
