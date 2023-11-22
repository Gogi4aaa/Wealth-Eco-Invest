﻿namespace Wealth_Eco_Invest.Web.ViewModels.Announce
{
    using System.ComponentModel.DataAnnotations;

    public class AllAnnouncesViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }
    }
}