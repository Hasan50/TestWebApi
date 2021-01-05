using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
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
