using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Domain.Models
{
    public class BaseResponseModel
    {
        [Required]
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
