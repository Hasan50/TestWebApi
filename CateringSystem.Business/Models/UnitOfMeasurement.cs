using CateringSystem.Framework;
using System;
using System.Web.Script.Serialization;

namespace CateringSystem.Business.Models
{
    public class UnitOfMeasurement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
    }
}
