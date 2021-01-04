using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class UserActiveStatus
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string CreatedById { get; set; }
        public string UserCredentialId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
