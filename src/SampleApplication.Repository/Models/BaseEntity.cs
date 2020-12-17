using System;
using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Repository.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public BaseEntity()
        {
        }
    }
}
