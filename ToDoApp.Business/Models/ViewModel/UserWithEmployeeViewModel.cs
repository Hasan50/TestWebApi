using ToDoApp.Framework;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class UserWithEmployeeViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string LoginID { get; set; }
        public string NationalId { get; set; }
        public string LoginKey { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string Gender { get; set; }
        public string BloodGroup { get; set; }

        public string Address { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public int? DesignationId { get; set; }
        public string Designation { get; set; }
        public DateTime? DathOfBirth { get; set; }
        [DisplayName("DesignationName")]
        public string DesignationName
        {
            get
            {
                if (!DesignationId.HasValue)
                    return string.Empty;
                return EnumUtility.GetDescriptionFromEnumValue((DesignationEnum)DesignationId);
            }
        }
    }


}
