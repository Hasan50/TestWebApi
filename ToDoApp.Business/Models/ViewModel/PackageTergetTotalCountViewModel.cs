﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Business.Models
{
    public class PackageTergetTotalCountViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? TotalTergetCount { get; set; }
      
    }


}