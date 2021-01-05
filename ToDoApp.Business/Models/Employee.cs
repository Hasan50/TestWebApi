using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmployeeCode { get; set; }
        public string ContactNo { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? DesignationId { get; set; }
        public string LoginKey { get; set; }
    }
}
