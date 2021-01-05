using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class UnitOfMeasurement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
    }
}
