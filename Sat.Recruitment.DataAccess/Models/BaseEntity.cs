using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sat.Recruitment.Models
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public DateTime ?DateDeleted { get; set; }
    }
}
