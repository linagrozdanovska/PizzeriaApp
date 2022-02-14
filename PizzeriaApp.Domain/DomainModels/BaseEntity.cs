using System;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaApp.Domain.DomainModels
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
