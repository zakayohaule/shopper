﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Shared.Mvc.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shared.Mvc.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }
        
        [Required, DisplayName("Full Name"), BindRequired]
        public string FullName { get; set; }
        
        [Required, EmailAddress, DisplayName("Email Address"), BindRequired]
        [Remote("ValidateUserEmail", AdditionalFields = "Id")]
        public string Email { get; set; }

        [BindNever]
        public string InstitutionName { get; set; }
        
        public List<long> Roles { get; set; }
    }
}