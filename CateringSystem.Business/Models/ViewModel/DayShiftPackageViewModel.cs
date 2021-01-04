using System;
using System.ComponentModel.DataAnnotations;

namespace CateringSystem.Business.Models
{
    public class DayShiftPackageViewModel
    {
        public string Id { get; set; }
        public string PackageId { get; set; }
        public int DayShiftId { get; set; }
        public string Name { get; set; }
        public string PackageName { get; set; }
        public string PackageCode { get; set; }
        public decimal? Price { get; set; }
       
    }


}
