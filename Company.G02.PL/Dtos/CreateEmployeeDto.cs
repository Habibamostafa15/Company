﻿using Company.DAL.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Dtos
{
   public class CreateEmployeeDto :BaseEntity
    {
       
        public int? DepartmentId { get; set; }


        [Required(ErrorMessage = "Name is Required !!")]
        public string Name { get; set; }

        [Range(minimum: 22, maximum: 60, ErrorMessage = "Age Must Be Between 22 and 60")]
        public int? Age { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Email is not valid !!")]
       
        public string? Email { get; set; }

        [RegularExpression(pattern: @"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        ErrorMessage = "Address Must Be like123-street-city-country " )]
        public string Address { get; set; }
        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Create")]
        public DateTime CreateAt { get; set; }

        public string? ImageName { get; set; }

        public IFormFile? Image { get; set; }


    }
}
