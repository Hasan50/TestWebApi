using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string UserCredentialId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string ContactPersonTwoName { get; set; }
        public string ContactPersonTwoSection { get; set; }
        public string ContactPersonTwoNumber { get; set; }
        public string ContactPersonTwoDesignation { get; set; }
        public string ContactPersonOneName { get; set; }
        public string ContactPersonOneSection { get; set; }
        public string ContactPersonOneNumber { get; set; }
        public string ContactPersonOneDesignation { get; set; }
        public string Address { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
