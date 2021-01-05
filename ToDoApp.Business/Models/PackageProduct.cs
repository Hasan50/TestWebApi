using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class PackageProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
