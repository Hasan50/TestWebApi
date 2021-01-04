using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CateringSystem.Business.Models
{
    public class UserCredientialDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserCredentialId { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public DateTime DathOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal Diabetes { get; set; }
        public bool Allergy { get; set; }
        public string AllergyDetail { get; set; }
        public int? BP { get; set; }
        public string NationalId { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string Designation { get; set; }
        public string ReferencePersonName { get; set; }
        public string ReferencePersonCellNumber { get; set; }
        public DateTime? LunchStartDate { get; set; }
        public string ReplacementMenu { get; set; }
        public string SpecialMenu { get; set; }
        public string LunchReceiveAddress { get; set; }
        public string WeekDayOff { get; set; }
        public string SalesExecutiveId { get; set; }
        public string DeliveryManId { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public decimal? RegistrationAmount { get; set; }
        public string Status { get; set; }
        public string AccountType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }


}
