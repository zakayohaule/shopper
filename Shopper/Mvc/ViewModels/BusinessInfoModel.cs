using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shopper.Mvc.ViewModels
{
    public class BusinessInfoModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        [Required]
        [Remote(routeName:"ValidateTenantName", AdditionalFields = "Id")]
        public string Name { get; set; }

        [DisplayName("Phone 1")]
        public string Phone1 { get; set; }

        [DisplayName("Phone 2")]
        public string Phone2 { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public string LogoPath { get; set; }

        public string SubscriptionType { get; set; }

        public DateTime ValidTo { get; set; }

        public IFormFile Image { get; set; }
    }
}
