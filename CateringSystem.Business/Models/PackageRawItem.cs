using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class PackageRawItem
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public string RawItemId { get; set; }
        public string WeekDay { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
