﻿using ToDoApp.Framework;
using System;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class RawItem
    {
        public string Id { get; set; }
        public int? WeightUomId { get; set; }
        public int? QuantityUomId { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
