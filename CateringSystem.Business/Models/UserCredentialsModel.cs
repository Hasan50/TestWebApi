using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem.Business.Models
{
    public class UserCredentials
    {
        public string Id { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string FileName { get; set; }
        public string FileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }


}
