using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class PeriodType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
