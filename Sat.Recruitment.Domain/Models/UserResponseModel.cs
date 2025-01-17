﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Models
{
    [ModelBinder]
    public class UserResponseModel : BaseResponseModel
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "The address is required")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "The phone is required")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "The usertype is required")]
        public string UserType { get; set; }

        [Required(ErrorMessage = "The money is required")]
        public decimal Money { get; set; }

    }
}
