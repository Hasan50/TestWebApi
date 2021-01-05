using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class UserWithPackageAdvanceViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Package { get; set; }
        public decimal? PackagePrice { get; set; }
        public string ImagePath { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string Gender { get; set; }
        public int? Age { get; set; }
        public string BloodGroup { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Diabetes { get; set; }
        public bool Allergy { get; set; }
        public int? BP { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Designation { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public string PeriodType { get; set; }
        public int? PeriodCount { get; set; }
        public string ReferencePersonName { get; set; }
        public string ReferencePersonCellNumber { get; set; }
    }


}
