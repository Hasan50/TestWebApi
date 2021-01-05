using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class Package
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PackageCode { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime AffectiveDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
