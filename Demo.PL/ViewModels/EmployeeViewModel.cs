﻿using Demo.DAL.Entites;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Requeird")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length is 50 Chars")]
        public string Name { get; set; }

        [Range(22, 30, ErrorMessage = "Age Must Between 22 and 30")]
        public int Age { get; set; }

        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address Must be Like 123-Street-City-Countary")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        [Range(4000, 8000)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public int? DepartmentId { get; set; }

        public Department Department { get; set; }

        public string DepartmentName { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }
    }
}
