using ToDoApp.Framework;
using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace ToDoApp.Business.Models
{
    public class DesignationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public enum DesignationEnum
    {
        [Description("Delivery Man")]
        DeliveryMan = 1,
        [Description("Sales Executive")]
        InProgress = 2,
        [Description("Accountant")]
        Pause = 3,
        
    }
}
