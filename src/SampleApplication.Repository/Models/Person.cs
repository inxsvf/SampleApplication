using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Repository.Models
{
    public class Person : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
